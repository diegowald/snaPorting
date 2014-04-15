using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public sealed class EnvioMovimientos : BackgroundTaskBase
    {
        public enum MODOS_TRANSMISION
        {
            TRANSMITIR_LISTVIEW = 1,
            TRANSMITIR_RECORDSET = 2,
            TRANSMITIR_RECORDSET_OCULTO = 3
        }

        public struct MOVIMIENTO_SELECCIONADO
        {
            public string nro;
            public string origen;
        }

        MODOS_TRANSMISION _modoTransmision;
        private bool _resultado;
        int _idCliente;
        bool _useSettingIni;
        System.Collections.Generic.List<string> filtroNotaVenta;
        System.Collections.Generic.List<string> filtroRecibo;
        System.Collections.Generic.List<string> filtroDevolucion;
        System.Collections.Generic.List<string> filtroInterdeposito;
        System.Collections.Generic.List<string> filtroRendicion;

        public EnvioMovimientos(JOB_TYPE jobType, int idCliente, bool useSettingsIni, MODOS_TRANSMISION modoTransmision,
            System.Collections.Generic.List<MOVIMIENTO_SELECCIONADO> filtro)
            : base(jobType)
        {
            _idCliente = idCliente;
            _useSettingIni = useSettingsIni;
            _modoTransmision = modoTransmision;
            filtroNotaVenta = new List<string>();
            filtroRecibo = new List<string>();
            filtroDevolucion = new List<string>();
            filtroInterdeposito = new List<string>();
            filtroRendicion = new List<string>();

            if (filtro != null)
            {
                foreach (MOVIMIENTO_SELECCIONADO ms in filtro)
                {
                    switch (ms.origen.ToUpper())
                    {
                        case "NOTA DE VENTA":
                            filtroNotaVenta.Add(ms.nro);
                            break;
                        case "RECIBO":
                            filtroRecibo.Add(ms.nro);
                            break;
                        case "DEVOLUCION":
                            filtroDevolucion.Add(ms.nro);
                            break;
                        case "INTERDEPOSITO":
                            filtroInterdeposito.Add(ms.nro);
                            break;
                        case "RENDICION":
                            filtroRendicion.Add(ms.nro);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public override void execute()
        {
            enviarMovimientos();
        }

        public delegate void FinishedHandler(bool OK);
        public delegate void CancelledHandler();

        public FinishedHandler onFinished;
        public CancelledHandler onCancelled;

        public override void cancelled()
        {
            if (onCancelled != null)
            {
                onCancelled();
            }
        }

        public override void finished()
        {
            if (onFinished != null)
            {
                onFinished(_resultado);
            }
        }

        private void enviarMovimientos()
        {
            Catalogo.util.IPPrivado ipPriv;
            string ipPrivado;
            string ipIntranet;
            bool fallaEnvioPedido = false;
            bool fallaEnvioRecibo = false;
            bool fallaEnvioDevolucion = false;
            bool fallaEnvioInterDeposito = false;
            bool fallaEnvioRendicion = false;

            if (_useSettingIni)
            {
                ipPrivado = Global01.URL_ANS;
                ipIntranet = Global01.URL_ANS2;
            }
            else
            {
                ipPriv = new Catalogo.util.IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
                ipPrivado = ipPriv.GetIP();
                ipIntranet = ipPriv.GetIpIntranet();
            }

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, _idCliente);
            System.Data.OleDb.OleDbDataReader movs = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "(todos)");

            if (movs.HasRows)
            {
                while (movs.Read())
                {
                    string origen = movs["Origen"].ToString().Trim().ToUpper();
                    string Nro = movs["Nro"].ToString();
                    switch (origen)
                    {
                        case "NOTA DE VENTA":
                            {
                                bool enviar = usarFiltros && filtroNotaVenta.Contains(Nro);
                                if (enviar)
                                {
                                    _pedidos.EnvioPedido envio = new _pedidos.EnvioPedido(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                    if (envio.Inicializado)
                                    {
                                        envio.obtenerDatos(Nro);
                                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Pedido,
                                             _auditor.Auditor.AccionesAuditadas.TRANSMITE, "P1 : " + movs["Nro"]);
                                        if (envio.enviarPedido() != 0)
                                        {
                                            fallaEnvioPedido = true;
                                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Pedido,
                                                 _auditor.Auditor.AccionesAuditadas.FALLO, "P1 " + movs["Nro"]);
                                        }
                                    }
                                    else
                                    {
                                        fallaEnvioPedido = true;
                                    }
                                    envio = null;
                                }
                            }
                            break;
                        case "RECIBO":
                            {
                                bool enviar = usarFiltros && filtroRecibo.Contains(Nro);
                                if (enviar)
                                {
                                    _recibos.EnvioRecibo envio = new _recibos.EnvioRecibo(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                    if (envio.Inicializado)
                                    {
                                        envio.obtenerDatos(Nro);
                                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Recibo, _auditor.Auditor.AccionesAuditadas.TRANSMITE, "R1 " + movs["Nro"]);
                                        if (envio.EnviarRecibo() != 0)
                                        {
                                            fallaEnvioRecibo = true;
                                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Recibo, _auditor.Auditor.AccionesAuditadas.FALLO, "R1 " + movs["Nro"]);
                                        }
                                        else
                                        {
                                        }
                                    }
                                    else
                                    {
                                        fallaEnvioRecibo = true;
                                    }
                                    envio = null;
                                }
                            }
                            break;
                        case "DEVOLUCION":
                            {
                                bool enviar = usarFiltros && filtroDevolucion.Contains(Nro);
                                if (enviar)
                                {
                                    _devoluciones.EnvioDevolucion envio = new _devoluciones.EnvioDevolucion(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                    if (envio.Inicializado)
                                    {
                                        envio.ObtenerDatos(Nro);
                                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Devoluciones, _auditor.Auditor.AccionesAuditadas.TRANSMITE, "D1 " + movs["Nro"]);
                                        if (envio.EnviarDevolucion() != 0)
                                        {
                                            fallaEnvioDevolucion = true;
                                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Devoluciones, _auditor.Auditor.AccionesAuditadas.FALLO, "D1 " + movs["Nro"]);
                                        }
                                    }
                                    else
                                    {
                                        fallaEnvioDevolucion = true;
                                    }
                                    envio = null;
                                }
                            }
                            break;
                        case "INTERDEPOSITO":
                            {
                                bool enviar = usarFiltros && filtroInterdeposito.Contains(Nro);
                                if (enviar)
                                {
                                    _interdeposito.EnvioInterDeposito envio = new _interdeposito.EnvioInterDeposito(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                    if (envio.Inicializado)
                                    {
                                        envio.ObtenerDatos(Nro);
                                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.InterDeposito, _auditor.Auditor.AccionesAuditadas.TRANSMITE, "ID1= " + movs["Nro"]);
                                        if (envio.EnviarInterDeposito() != 0)
                                        {
                                            fallaEnvioInterDeposito = true;
                                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.InterDeposito, _auditor.Auditor.AccionesAuditadas.FALLO, "ID1 " + movs["Nro"]);
                                        }
                                    }
                                    else
                                    {
                                        fallaEnvioInterDeposito = true;
                                    }
                                    envio = null;
                                }
                            }
                            break;
                        case "RENDICION":
                            {
                                bool enviar = usarFiltros && filtroRendicion.Contains(Nro);
                                if (enviar)
                                {
                                    _rendiciones.EnvioRendicion envio = new _rendiciones.EnvioRendicion(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                    if (envio.Inicializado)
                                    {
                                        envio.ObtenerDatos(Nro);
                                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Rendicion, _auditor.Auditor.AccionesAuditadas.TRANSMITE, "RC1 " + movs["Nro"]);
                                        if (envio.EnviarRendicion() != 0)
                                        {
                                            fallaEnvioRendicion = true;
                                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Rendicion, _auditor.Auditor.AccionesAuditadas.FALLO, "RC1 " + movs["Nro"]);
                                        }
                                    }
                                    else
                                    {
                                        fallaEnvioRendicion = true;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (fallaEnvioPedido || fallaEnvioRecibo || fallaEnvioDevolucion ||
                fallaEnvioInterDeposito || fallaEnvioRendicion)
            {
                _resultado = false;
            }
            else
            {
                _resultado = true;
            }
        }

        private bool usarFiltros
        {
            get
            {
                return (filtroNotaVenta.Count > 0)
                    || (filtroRecibo.Count > 0)
                    || (filtroDevolucion.Count > 0)
                    || (filtroInterdeposito.Count > 0)
                    || (filtroRendicion.Count > 0);
            }
        }
    }
}
