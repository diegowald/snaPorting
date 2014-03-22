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

        MODOS_TRANSMISION _modoTransmision;
        private bool _resultado;
        int _idCliente;
        bool _useSettingIni;

        public EnvioMovimientos(JOB_TYPE jobType, int idCliente, bool useSettingsIni, MODOS_TRANSMISION modoTransmision)
            : base(jobType)
        {
            _idCliente = idCliente;
            _useSettingIni = useSettingsIni;
            _modoTransmision = modoTransmision;
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
            IPPrivado ipPriv;
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
                ipPriv = new IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
                ipPrivado = ipPriv.GetIP();
                ipIntranet = ipPriv.GetIpIntranet();
            }

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, Global01.IDMaquina);
            System.Data.OleDb.OleDbDataReader movs = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, _idCliente);

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
                                _pedidos.EnvioPedido envio = new _pedidos.EnvioPedido(Global01.Conexion, ipPrivado, ipIntranet, _idCliente.ToString(), false, "");
                                if (envio.Inicializado)
                                {
                                    envio.obtenerDatos(Nro);
                                    //vg.auditor.guardar pedido, transmite, "P1 : " + movs["Nro"]
                                    if (envio.enviarPedido() != 0)
                                    {
                                        fallaEnvioPedido = true;
                                        //vg.auditor.guardar pedido, fallo, "P1 " + movs["Nro"]
                                    }
                                }
                                else
                                {
                                    fallaEnvioPedido = true;
                                }
                                envio = null;
                            }
                            break;
                        case "RECIBO":
                            {
                                _recibos.EnvioRecibo envio = new _recibos.EnvioRecibo(Global01.Conexion,
                                Global01.URL_ANS, Global01.URL_ANS2, Global01.IDMaquina, false, "");
                                if (envio.Inicializado)
                                {
                                    envio.obtenerDatos(Nro);
                                    // vg.auditor.guardar Recibo, Transmite, "R1 " + movs["Nro"]
                                    if (envio.EnviarRecibo() != 0)
                                    {
                                        fallaEnvioRecibo = true;
                                        //vg.auditor.guardar Recibo, Fallo, "R1 " + movs["Nro"]
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
                            break;
                        case "DEVOLUCION":
                            {
                                _devolucion.EnvioDevolucion envio = new _devolucion.EnvioDevolucion(Global01.Conexion,
                                     Global01.URL_ANS, Global01.URL_ANS2, Global01.IDMaquina, false, "");
                                if (envio.Inicializado)
                                {
                                    envio.ObtenerDatos(Nro);
                                    // vg.auditor.guardar devoluciones, TRANSMITE, "D1 " + movs["Nro"]
                                    if (envio.EnviarDevolucion() != 0)
                                    {
                                        fallaEnvioDevolucion = true;
                                        // vg.auditor.guardar DEvoluciones, FALLO, "D1 " + movs["Nro"]
                                    }
                                }
                                else
                                {
                                    fallaEnvioDevolucion = true;
                                }
                                envio = null;
                            }
                            break;
                        case "INTERDEPOSITO":
                            {
                                _interdeposito.EnvioInterDeposito envio = new _interdeposito.EnvioInterDeposito(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                if (envio.Inicializado)
                                {
                                    envio.ObtenerDatos(Nro);
                                    // vg.auditor.guardar Interdeposito, TRANSMITE, "ID1= " + movs["Nro"]
                                    if (envio.EnviarInterDeposito() != 0)
                                    {
                                        fallaEnvioInterDeposito = true;
                                        //vg.auditor.guardar interdeposito, fallo, "ID1 " + movs["Nro"]
                                    }
                                }
                                else
                                {
                                    fallaEnvioInterDeposito = true;
                                }
                                envio = null;
                            }
                            break;
                        case "RENDICION":
                            {
                                _rendicion.EnvioRendicion envio = new _rendicion.EnvioRendicion(Global01.Conexion, ipPrivado, ipIntranet, Global01.IDMaquina, false, "");
                                if (envio.Inicializado)
                                {
                                    envio.ObtenerDatos(Nro);
                                    // vg.auditor.guardar Rendicion, TRANSMITE, "RC1 " + movs["Nro"]
                                    if (envio.EnviarRendicion() != 0)
                                    {
                                        fallaEnvioRendicion = true;
                                        // vg.auditor.guardar rendicion, FALLO, "RC1 " + movs["Nro"]
                                    }
                                }
                                else
                                {
                                    fallaEnvioRendicion = true;
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

    }
}
