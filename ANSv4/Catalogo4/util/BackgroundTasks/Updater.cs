using System;
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

        public Updater(JOB_TYPE jobType, UpdateType modo, bool usarSettingsIni)
            : base(jobType)
        {
            _modo = modo;
            _usarSettingsIni = usarSettingsIni;
            result = true;
            resultMessage = "";
        }

        private void enviarAuditoria2EnBloques()
        {
            try
            {
                if (!_usarSettingsIni)
                {
                    IPPrivado ipPriv = new IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
                    ipPrivado = ipPriv.GetIP();
                    ipIntranet = ipPriv.GetIpIntranet();
                }
                else
                {
                    ipPrivado = Global01.URL_ANS;
                    ipIntranet = Global01.URL_ANS2;
                }

                if (ipPrivado.Length == 0 || ipIntranet.Length == 0)
                {
                    //  Hide
                    return;
                }
                Catalogo._audit.EnvioAuditoria envAudit = new _audit.EnvioAuditoria(Global01.IDMaquina,
                    Global01.URL_ANS, Global01.URL_ANS2, false, "");
                if (envAudit.Inicializado)
                {

                    //  If vg.TranActiva = False Then
                    //        vg.Conexion.BeginTrans
                    //        vg.TranActiva = True
                    //  End If

                    System.Data.OleDb.OleDbDataReader reader = Catalogo.Funciones.oleDbFunciones.Comando(ref Global01.Conexion,
                        "SELECT * FROM tblAuditor WHERE F_Transmision is null");

                    List<string> Fechas = new List<string>();
                    List<string> Descripciones = new List<string>();
                    List<long> IDs = new List<long>();

                    while (reader.Read())
                    {
                        Fechas.Add(((DateTime)reader["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss"));
                        Descripciones.Add(reader["Detalle"].ToString());
                        IDs.Add((long)reader["ID"]);

                        if (Fechas.Count == 10)
                        {
                            if (envAudit.enviarAuditoriaEnBloques(Fechas.ToArray(), Descripciones.ToArray(), IDs.ToArray()))
                            {
                                foreach (long ID in IDs)
                                {
                                    Funciones.oleDbFunciones.Comando(ref Global01.Conexion, "EXEC usp_Auditor_Transmision_Upd " + ID.ToString());
                                }
                            }
                            Fechas.Clear();
                            Descripciones.Clear();
                            IDs.Clear();
                        }
                    }

                    // Envio el resto de las auditorias que pueden haber quedado
                    if (Fechas.Count > 0)
                    {
                        if (envAudit.enviarAuditoriaEnBloques(Fechas.ToArray(), Descripciones.ToArray(), IDs.ToArray()))
                        {
                            foreach (long ID in IDs)
                            {
                                Funciones.oleDbFunciones.Comando(ref Global01.Conexion, "EXEC usp_Auditor_Transmision_Upd " + ID.ToString());
                            }
                        }
                    }
                    /*  If vg.TranActiva = True Then
                          vg.Conexion.CommitTrans
                          vg.TranActiva = False
                      End If*/

                }
            }
            catch
            {
                /*ErrorGuardianLocalHandler:
                  Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                  Case vbAbort
                      Err.Raise Err.Number
                  Case vbRetry
                      Resume
                  Case vbIgnore
                      Resume Next
                  End Select
                '-------- ErrorGuardian End ----------*/
            }
        }
    }
}
