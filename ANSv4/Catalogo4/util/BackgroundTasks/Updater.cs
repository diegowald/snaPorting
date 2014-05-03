using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.util.BackgroundTasks
{
    public class Updater : BackgroundTaskBase,
        Funciones.emitter_receiver.IReceptor<Catalogo.varios.complexMessage>, // Para recibir mensajes de Clientes
        Funciones.emitter_receiver.IReceptor<string>, // Para recibir un refresh de novedades
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
            ListaPrecio = 7,
            UpdateNovedadesCatalogo = 8
        }

        private UpdateType _modo;
 
        private string resultMessage;
        private bool result;

        public override void execute(ref bool cancel)
        {
            if ((worker!=null) && (worker.CancellationPending))
            {
                cancel = true;
                worker.CancelAsync();
                return;
            }
            switch (_modo)
            {
                case UpdateType.UpdateCuentas:
                    {
                        Catalogo._clientes.UpdateClientes envio = new _clientes.UpdateClientes(Global01.IDMaquina, Global01.URL_ANS2);
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
                        Catalogo._application.UpdateAppConfig envio = new _application.UpdateAppConfig(Global01.IDMaquina, Global01.URL_ANS2);
                        if (envio.Inicializado)
                        {
                            envio.sincronizarApp();
                        }
                    }
                    break;
                case UpdateType.ActivarApp:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, Global01.URL_ANS2);
                        if (app.Inicializao)
                        {
                            app.activar();
                        }
                    }
                    break;
                case UpdateType.EstadoActual:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, Global01.URL_ANS2);
                        if (app.Inicializao)
                        {
                            app.estadoActual();
                        }
                    }
                    break;
                case UpdateType.ListaPrecio:
                    {
                        Catalogo._Application.ActivarAplicacion app = new _Application.ActivarAplicacion(Global01.IDMaquina, Global01.URL_ANS2);
                        if (app.Inicializao)
                        {
                            app.listaPrecio();
                        }
                    }
                    break;
                case UpdateType.UpdateNovedadesCatalogo:
                    {
                        Catalogo._novedades.UpdateNovedades envio = new _novedades.UpdateNovedades(Global01.IDMaquina, Global01.URL_ANS2);
                        envio.attachReceptor(this);
                        envio.attachCancellableReceptor(this);
                        envio.attachReceptor2(this);
                        if (envio.inicializado)
                        {
                            envio.sincronizarNovedades();
                        }
                    }
                    break;
                default:
                    break;
            }

            if (Global01.EnviarAuditoria)
            {
                enviarAuditoria2EnBloques();
                enviarClientesNovedades();
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
            : base("Updater", jobType)
        {
            _modo = modo;
            result = true;
            resultMessage = "";
        }

        private void enviarAuditoria2EnBloques()
        {
            try
            {
                Catalogo._auditor.EnvioAuditoria envAudit = new _auditor.EnvioAuditoria(Global01.IDMaquina, Global01.URL_ANS2);
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
                        Fechas.Add(((DateTime)reader["Fecha"]).ToString("dd/MM/yyyy HH:mm:ss"));
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

        private void enviarClientesNovedades()
        {
            try
            {
                Catalogo._clientesNovedades.EnvioClientesNovedades envNovedades = new _clientesNovedades.EnvioClientesNovedades(Global01.IDMaquina, Global01.URL_ANS2);
                if (envNovedades.inicializado)
                {
                    if (Global01.TranActiva == null)
                    {
                        Global01.TranActiva = Global01.Conexion.BeginTransaction();
                    }

                    System.Data.OleDb.OleDbDataReader reader = Catalogo.Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_ClientesNovedades WHERE F_Transmicion is null");

                    List<string> fechas = new List<string>();
                    List<string> novedades = new List<string>();
                    List<string> IDsClientes = new List<string>();
                    List<string> IDs = new List<string>();

                    while (reader.Read())
                    {
                        fechas.Add(reader["F_Carga"].ToString());
                        novedades.Add(reader["Novedad"].ToString());
                        IDsClientes.Add(reader["IdCliente"].ToString());
                        IDs.Add(reader["ID"].ToString());

                        if (fechas.Count == 10)
                        {
                            if (envNovedades.enviarNovedadesEnBloques(fechas, novedades, IDsClientes))
                            {
                                for (int i = 0; i < fechas.Count; i++)
                                {
                                    Catalogo.Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_ClienteNovedades_Transmicion_upd " + IDs[i]);
                                }
                            }
                            fechas.Clear();
                            novedades.Clear();
                            IDsClientes.Clear();
                            IDs.Clear();
                        }
                    }

                    //   Envio el resto de las auditorias que pueden haber quedado
                    if (fechas.Count != 0)
                    {
                        if (envNovedades.enviarNovedadesEnBloques(fechas, novedades, IDsClientes))
                        {
                            for (int i = 0; i < fechas.Count; i++)
                            {
                                Catalogo.Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_ClienteNovedades_Transmicion_upd  " + IDs[i]);
                            }
                        }

                        if (Global01.TranActiva != null)
                        {
                            Global01.TranActiva.Commit();
                            Global01.TranActiva = null;
                        }
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
                
        
        public void onRecibir(Catalogo.varios.complexMessage msg)
        {
            //Catalogo.varios.NotificationCenter.instance.notificar(dato.first, dato.second);
            Catalogo.varios.NotificationCenter.instance.notificar(msg);
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

        public void onRecibir(string dato)
        {
            Catalogo.varios.NotificationCenter.instance.requestRefreshNovedades();
        }
    }
}
