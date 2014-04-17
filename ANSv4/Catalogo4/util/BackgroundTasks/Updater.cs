using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.util.BackgroundTasks
{
    public class Updater : BackgroundTaskBase,
        Funciones.emitter_receiver.IReceptor<util.Pair<string, float>>, // Para recibir mensajes de Clientes
        Funciones.emitter_receiver.ICancellableEmitter, // Para chequear si es necesario cancelar
        Funciones.emitter_receiver.ICancellableReceiver // Para recibir notificaciones de recepcion de cancelacion
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
 
        private string resultMessage;
        private bool result;

        public override void execute()
        {
            if (Global01.ipSettingIni)
            {
                ipPrivado = Global01.URL_ANS;
                ipIntranet = Global01.URL_ANS2;
                //ipCatalogo = Global01.URL_ANS;
            }
            else
            {
                IPPrivado ipPriv = new IPPrivado(Global01.URL_ANS, Global01.IDMaquina);
                ipPrivado = ipPriv.GetIP();
                ipIntranet = ipPriv.GetIpIntranet();
                //ipCatalogo = ipPriv.GetIPCatalogo();
            }
            
            if (ipPrivado.Length == 0 || ipIntranet.Length == 0)
            {
                //  Hide
                return;
            }

            //if (ipPrivado.Length == 0)
            //{
            //    if (_modo != UpdateType.UpdateAppConfig)
            //    {
            //        resultMessage = "El DNS no puede resolver la direccion IP";
            //    }
            //    return;
            //}

            switch (_modo)
            {
                case UpdateType.UpdateCuentas:
                    {
                        Catalogo._clientes.UpdateClientes envio = new _clientes.UpdateClientes(Global01.IDMaquina, ipPrivado, ipIntranet);
                        envio.attachReceptor(this);
                        envio.attachCancellableReceptor(this);
                        if (envio.inicializado)
                        {
                            envio.sincronizarClientes();
                        }
                    }
                    break;
                case UpdateType.UpdateAppConfig:
                    {
                        Catalogo._application.UpdateAppConfig envio = new _application.UpdateAppConfig(Global01.IDMaquina, ipPrivado, ipIntranet);
                        if (envio.Inicializado)
                        {
                            envio.sincronizarApp();
                        }
                    }
                    break;
                case UpdateType.ActivarApp:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, ipPrivado, ipIntranet);
                        if (app.Inicializao)
                        {
                            app.activar();
                        }
                    }
                    break;
                case UpdateType.EstadoActual:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, ipPrivado, ipIntranet);
                        if (app.Inicializao)
                        {
                            app.estadoActual();
                        }
                    }
                    break;
                case UpdateType.ListaPrecio:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, ipPrivado, ipIntranet);
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
            //throw new NotImplementedException();
        }

        public override void finished()
        {
            //System.Windows.Forms.MessageBox.Show("hasta acá todo va bien");
        }

        public Updater(JOB_TYPE jobType, UpdateType modo)
            : base(jobType)
        {
            _modo = modo;
            result = true;
            resultMessage = "";
        }

        private void enviarAuditoria2EnBloques()
        {
            try
            {
                if (!Global01.ipSettingIni)
                {
                    IPPrivado ipPriv = new IPPrivado(Global01.URL_ANS, Global01.IDMaquina);
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

                Catalogo._auditor.EnvioAuditoria envAudit = new _auditor.EnvioAuditoria(Global01.IDMaquina, ipPrivado, ipIntranet);
                if (envAudit.Inicializado)
                {
                    if (Global01.TranActiva == null)
                    {
                        Global01.TranActiva = Global01.Conexion.BeginTransaction();
                    }

                    System.Data.OleDb.OleDbDataReader reader = Catalogo.Funciones.oleDbFunciones.Comando(Global01.Conexion,
                        "SELECT * FROM tblAuditor WHERE F_Transmision is null");

                    List<string> Fechas = new List<string>();
                    List<string> Descripciones = new List<string>();
                    List<long> IDs = new List<long>();

                    while (reader.Read())
                    {
                        Fechas.Add(((DateTime)reader["Fecha"]).ToString("dd/MM/yyyy hh:mm:ss"));
                        Descripciones.Add(reader["Detalle"].ToString());
                        IDs.Add(Int32.Parse(reader["ID"].ToString()));

                        if (Fechas.Count == 10)
                        {
                            if (envAudit.enviarAuditoriaEnBloques(Fechas.ToArray(), Descripciones.ToArray(), IDs.ToArray()))
                            {
                                foreach (long ID in IDs)
                                {
                                    Funciones.oleDbFunciones.Comando(Global01.Conexion, "EXEC usp_Auditor_Transmision_Upd " + ID.ToString());
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
                                Funciones.oleDbFunciones.Comando(Global01.Conexion, "EXEC usp_Auditor_Transmision_Upd " + ID.ToString());
                            }
                        }
                    }
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Commit();
                        Global01.TranActiva = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Rollback();
                    Global01.TranActiva = null;
                }
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;
            }
        }

        public void onRecibir(Pair<string, float> dato)
        {
            Catalogo.varios.NotificationCenter.instance.notificar(dato.first, dato.second);
        }


        public Funciones.emitter_receiver.onRequestCancel requestCancel
        {
            get;
            set;
        }

        public void onRequestCancel(ref bool cancel)
        {
            Catalogo.varios.NotificationCenter.instance.requestCancel(ref cancel);
        }
    }
}
