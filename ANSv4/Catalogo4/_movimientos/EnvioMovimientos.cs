﻿using System;
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
            System.Data.OleDb.OleDbDataReader movs = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS);

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
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Pedido,
                                         auditoria.Auditor.AccionesAuditadas.TRANSMITE, "P1 : " + movs["Nro"]);
                                    if (envio.enviarPedido() != 0)
                                    {
                                        fallaEnvioPedido = true;
                                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Pedido,
                                             auditoria.Auditor.AccionesAuditadas.FALLO, "P1 " + movs["Nro"]);
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
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Recibo,
                                         auditoria.Auditor.AccionesAuditadas.TRANSMITE, "R1 " + movs["Nro"]);
                                    if (envio.EnviarRecibo() != 0)
                                    {
                                        fallaEnvioRecibo = true;
                                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Recibo,
                                             auditoria.Auditor.AccionesAuditadas.FALLO, "R1 " + movs["Nro"]);
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
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Devoluciones,
                                         auditoria.Auditor.AccionesAuditadas.TRANSMITE, "D1 " + movs["Nro"]);
                                    if (envio.EnviarDevolucion() != 0)
                                    {
                                        fallaEnvioDevolucion = true;
                                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Devoluciones,
                                             auditoria.Auditor.AccionesAuditadas.FALLO, "D1 " + movs["Nro"]);
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
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.InterDeposito,
                                         auditoria.Auditor.AccionesAuditadas.TRANSMITE, "ID1= " + movs["Nro"]);
                                    if (envio.EnviarInterDeposito() != 0)
                                    {
                                        fallaEnvioInterDeposito = true;
                                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.InterDeposito,
                                             auditoria.Auditor.AccionesAuditadas.FALLO, "ID1 " + movs["Nro"]);
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
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Rendicion,
                                         auditoria.Auditor.AccionesAuditadas.TRANSMITE, "RC1 " + movs["Nro"]);
                                    if (envio.EnviarRendicion() != 0)
                                    {
                                        fallaEnvioRendicion = true;
                                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Rendicion,
                                             auditoria.Auditor.AccionesAuditadas.FALLO, "RC1 " + movs["Nro"]);
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