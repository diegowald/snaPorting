﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public class Updater : BackgroundTaskBase
    {
        public enum UpdateType
        {
            UpdateCatalogo = 1,
            UpdateCuentas = 2,
            UpdateAppConfig = 3,
            UpdateGeneral = 4,
            ActivarApp = 5,
            EstadoActual = 6,
            ListaPrecio = 7
        }

        private UpdateType _modo;
        private String ipPrivado;
        private String ipIntranet;
        private String ipCatalogo;
        private bool _usarSettingsIni;

        private string resultMessage;
        private bool result;

        public override void execute()
        {
            if (_usarSettingsIni)
            {
                ipPrivado = Global01.URL_ANS;
                ipIntranet = Global01.URL_ANS2;
                ipCatalogo = Global01.URL_ANS;
            }
            else
            {
                IPPrivado ipPriv = new IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
                ipPrivado = ipPriv.GetIP();
                ipIntranet = ipPriv.GetIpIntranet();
                ipCatalogo = ipPriv.GetIPCatalogo();
            }

            if (ipCatalogo.Length == 0)
            {
                if (_modo != UpdateType.UpdateAppConfig)
                {
                    resultMessage = "El DNS no puede resolver la direccion IP";
                }
                return;
            }

            switch (_modo)
            {
                case UpdateType.UpdateCuentas:
                    {
                        Catalogo._clientes.UpdateClientes envio = new _clientes.UpdateClientes(Global01.Conexion,
                            Global01.IDMaquina, ipPrivado, ipIntranet, false, "");
                        if (envio.inicializado)
                        {
                            envio.sincronizarClientes();
                        }
                    }
                    break;
                case UpdateType.UpdateAppConfig:
                    {
                        Catalogo._appConfig.UpdateAppConfig envio = new _appConfig.UpdateAppConfig(Global01.IDMaquina,
                            Global01.URL_ANS, Global01.URL_ANS2, false, "", Global01.Conexion);
                        if (envio.Inicializado)
                        {
                            envio.sincronizarApp();
                        }
                    }
                    break;
                case UpdateType.ActivarApp:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina,
                            Global01.URL_ANS, Global01.URL_ANS2, false, "");
                        if (app.Inicializao)
                        {
                            app.activar();
                        }
                    }
                    break;
                case UpdateType.EstadoActual:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina,
                            Global01.URL_ANS, Global01.URL_ANS2, false, "");
                        if (app.Inicializao)
                        {
                            app.estadoActual();
                        }
                    }
                    break;
                case UpdateType.ListaPrecio:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina,
                            Global01.URL_ANS, Global01.URL_ANS2, false, "");
                        if (app.Inicializao)
                        {
                            app.listaPrecio();
                        }
                    }
                    break;
                default:
                    break;
            }

            if (Global01.EnviarAuditoria)
            {
                enviarAuditoria2EnBloques();
            }
        }

        public override void cancelled()
        {
            throw new NotImplementedException();
        }

        public override void finished()
        {
            throw new NotImplementedException();
        }

        public Updater(JOB_TYPE jobType, UpdateType modo, bool usarSettingsIni) : base(jobType)
        {
            _modo = modo;
            _usarSettingsIni = usarSettingsIni;
            result = true;
            resultMessage = "";
        }

        private void enviarAuditoria2EnBloques()
        {
            throw new NotImplementedException();
        }
    }
}
