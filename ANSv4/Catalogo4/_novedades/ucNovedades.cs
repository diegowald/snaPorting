using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._novedades
{
    public partial class ucNovedades : UserControl
    {
        private int dataRowCount = 0;
        private bool dataGridIdle = false;
        private DataTable dtNovedades = new DataTable();
        private DataView dvNovedades = new DataView();

        private Catalogo.varios.FlashControl flash;
        public ucNovedades()
        {
            InitializeComponent();
            flash = new varios.FlashControl();
            this.splitC1.Panel1.Controls.Add(this.flash);

            pictureBox.Dock = DockStyle.Fill;
            webBrowser.Dock = DockStyle.Fill;
            flash.Dock = DockStyle.Fill;

            //util.BackgroundTasks.ChequeoNovedades checker = new util.BackgroundTasks.ChequeoNovedades(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
            //checker.run();
            downloadFiles = new Dictionary<string, DownloadStatus>();

            // al iniciar las noveedades se debe ver el pictureBox con la imaggn

            pictureBox.Visible = true;
            webBrowser.Visible = false;
            flash.Visible = false;
        }
         
        private void ucNovedades_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void loadDataGridView()
        {
            dataGridIdle = false;

            // If rows in grid already exist, clear them
            dgvNovedades.DataSource = null;
            dgvNovedades.Rows.Clear();

            // For this example, make it a read-only datagrid
            dgvNovedades.ReadOnly = true;
            dgvNovedades.AllowUserToAddRows = false;
            dgvNovedades.AllowUserToDeleteRows = false;

            dgvNovedades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNovedades.MultiSelect = false;

            dgvNovedades.RowHeadersWidth = 4;
            dgvNovedades.RowHeadersVisible = true;

            //Set AutoGenerateColumns False
            dgvNovedades.AutoGenerateColumns = false;


//SELECT n.Descripcion, n.F_Inicio, n.F_Fin, n.N_Archivo, n.url, n.zonas, n.Fecha, n.Origen, n.Tipo, n.ID FROM ansNovedades AS n;

            //Set Columns Count 
            dgvNovedades.ColumnCount = 11;

            //Add Columns
            //"Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID";

            dgvNovedades.Columns[0].Name = "Descripcion";
            dgvNovedades.Columns[0].HeaderText = "Descripción";
            dgvNovedades.Columns[0].DataPropertyName = "Descripcion";
            //dgvNovedades.Columns[0].Visible = true;

            dgvNovedades.Columns[1].Name = "F_Inicio";
            dgvNovedades.Columns[1].HeaderText = "Desde";
            dgvNovedades.Columns[1].DataPropertyName = "F_Inicio";
            //dgvNovedades.Columns[1].Visible = false;

            dgvNovedades.Columns[2].Name = "F_Fin";
            dgvNovedades.Columns[2].HeaderText = "Hasta";
            dgvNovedades.Columns[2].DataPropertyName = "F_Fin";
            //dgvNovedades.Columns[2].Visible = false;

            dgvNovedades.Columns[3].Name = "N_Archivo";
            dgvNovedades.Columns[3].HeaderText = "N_Archivo";
            dgvNovedades.Columns[3].DataPropertyName = "N_Archivo";
            //dgvNovedades.Columns[3].Visible = false;

            dgvNovedades.Columns[4].Name = "url";
            dgvNovedades.Columns[4].HeaderText = "url";
            dgvNovedades.Columns[4].DataPropertyName = "url";
            //dgvNovedades.Columns[4].Visible = false;

            dgvNovedades.Columns[5].Name = "zonas";
            dgvNovedades.Columns[5].HeaderText = "zonas";
            dgvNovedades.Columns[5].DataPropertyName = "zonas";
            //dgvNovedades.Columns[5].Visible = false;

            dgvNovedades.Columns[6].Name = "fecha";
            dgvNovedades.Columns[6].HeaderText = "fecha";
            dgvNovedades.Columns[6].DataPropertyName = "fecha";
            //dgvNovedades.Columns[6].Visible = false;

            dgvNovedades.Columns[7].Name = "origen";
            dgvNovedades.Columns[7].HeaderText = "origen";
            dgvNovedades.Columns[7].DataPropertyName = "origen";
            //dgvNovedades.Columns[7].Visible = false;

            dgvNovedades.Columns[8].Name = "tipo";
            dgvNovedades.Columns[8].HeaderText = "tipo";
            dgvNovedades.Columns[8].DataPropertyName = "tipo";
            //dgvNovedades.Columns[8].Visible = false;

            dgvNovedades.Columns[9].Name = "id";
            dgvNovedades.Columns[9].HeaderText = "id";
            dgvNovedades.Columns[9].DataPropertyName = "id";
            //dgvNovedades.Columns[9].Visible = false;

            dgvNovedades.Columns[10].Name = "FLeido";
            dgvNovedades.Columns[10].HeaderText = "FLeido";
            dgvNovedades.Columns[10].DataPropertyName = "F_Leido";
            //dgvNovedades.Columns[10].Visible = false;

            string wDestino2 = "cliente"; //valores posibles "ambos;cliente;viajante;catalogo"
            if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente)
            {
                wDestino2 = "viajante";
            };

            string wCondicion = "activo=1 and (destino='ambos' or destino='" + wDestino2 + "') and origen<>'catalogo' "; //+
                               // " and f_inicio<=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "# and f_fin>=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "#";
            string wOrden = "F_Inicio DESC, Tipo";
            string wCampos = "Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID";

            dtNovedades  = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "v_Novedades1", wCondicion, wOrden, wCampos);

            // Save the row count in the original datatable
            dataRowCount = dtNovedades.Rows.Count;
            
            dtNovedades = filter(dtNovedades);

            procesarYBorrarRegistrosCatalogo(dtNovedades);
            /*---DIEGO ---
             traer primero registros con origen="catalogo" (ojo tener en cta siempre el destino )
             * y ejecutar procesos acorde al tipo ws=webservice mdb=actualizacion
             * 
             * aca.....ejemplo llamar a void.ejecutarNovedad....(implementar)
             *         if (pTipo == "mdb")
                        {
                            MainMod.update_productos();
                            //Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                            //existencia.getExistencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);
                        }
             * 
             * otros tipos updateAppConfig.ObtenerComandos, etc, etc
            */

            procesarInfoNovedades(dtNovedades);

            // Create a dataview of the datatable
            dvNovedades.Table = dtNovedades;

            //DIEGO Acá FILTRAR Global01.Zona completado con ceros a la izquierda string de long 3---
           //ejemplo: en BD 204;205;3*;08* ver si Global01.Zona está incluido en el string "zona del DT" OJO con los '*'
            
            // Filter the dataview
            dvNovedades.RowFilter = ""; //filterString;

            // Bind Datagrid view to the DataView
            dgvNovedades.DataSource = dvNovedades;
            dgvNovedades.Refresh();

            dataGridIdle = true;

            if (dataRowCount > 0)
            {
                dgvNovedades.Rows[0].Selected = true;

                DataGridViewCell cell = dgvNovedades[0, 0];
                /*if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;

                    mostrarNovedad(row.Cells["Descripcion"].Value.ToString(),
                                   row.Cells["N_Archivo"].Value.ToString(),
                                   row.Cells["url"].Value.ToString(),
                                   row.Cells["Origen"].Value.ToString(),
                                   row.Cells["Tipo"].Value.ToString(), 
                                   int.Parse(row.Cells["id"].Value.ToString()));
                }*/
            }
 
        }

        //private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dataGridIdle)
        //    {
        //        DataGridViewCell cell = null;
        //        foreach (DataGridViewCell selectedCell in dgvNovedades.SelectedCells)
        //        {
        //            cell = selectedCell;
        //            break;
        //        }
        //        if (cell != null)
        //        {
        //            DataGridViewRow row = cell.OwningRow;

        //            mostrarNovedad(row.Cells["Descripcion"].Value.ToString(),
        //                           row.Cells["N_Archivo"].Value.ToString(),
        //                           row.Cells["url"].Value.ToString(),
        //                           row.Cells["Origen"].Value.ToString(),
        //                           row.Cells["Tipo"].Value.ToString());
        //        }
        //    }
        //}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    DataGridViewCell cell = dgvNovedades[e.ColumnIndex, e.RowIndex];

                    if (cell != null)
                    {
                        DataGridViewRow row = cell.OwningRow;

                        mostrarNovedad(row.Cells["Descripcion"].Value.ToString(),
                                       row.Cells["N_Archivo"].Value.ToString(),
                                       row.Cells["url"].Value.ToString(),
                                       row.Cells["Origen"].Value.ToString(),
                                       row.Cells["Tipo"].Value.ToString(),
                                       int.Parse(row.Cells["id"].Value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }
        
        private void mostrarNovedad(string pDescripcion, string pArchivo, string pUrl, string pOrigen, string pTipo, int id)
        {
            if (pTipo == "url")
            {
                System.Diagnostics.Process.Start(pUrl + pArchivo);
            }
            else if (pTipo == "texto")
            {
                this.pictureBox.Visible = false;
                this.webBrowser.Visible = true;
                flash.Visible = false;
                webBrowser.DocumentText = pUrl;
            }
            else if (pTipo == "pdf")
            {
                System.Diagnostics.Process.Start(pUrl + pArchivo);
            }
            else if (pTipo == "imagen")
            {
                this.pictureBox.Visible = true;
                this.webBrowser.Visible = false;
                flash.Visible = false;
                string dest = String.Format("{0}\\imagenes\\Novedades\\{1}_{2}", Global01.AppPath, id, pArchivo);
                pictureBox.ImageLocation = dest;
            }
            else if (pTipo == "flash")
            {
                pictureBox.Visible = false;
                webBrowser.Visible = false;
                flash.Visible = true;
                flash.file = String.Format("{0}\\imagenes\\Novedades\\{1}_{2}", Global01.AppPath, id, pArchivo);
                flash.play();
            }

////solo a Los efectos de un ejemplo
//            string sUrl = "file://" + Global01.AppPath + @"\reportes\htmldocs\Viajantes\scroller_newstic.html";
//            string sUrl2 = "file://" + Global01.AppPath + @"\reportes\htmldocs\Clientes\scroller_newstic.html";
//            //webBrowser1.Navigate(new Uri(sUrl));
//            webBrowser.Navigate(new Uri(sUrl2));
        }

        private void ejecutarNovedad(string pDescripcion, string pArchivo, string pUrl, string pOrigen, string pTipo)
        {
            if (pTipo == "mdb")
            {
                MainMod.update_productos();
                //Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                //existencia.getExistencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);         
            }
            else if (pTipo == "UpdateAppConfig")
            {
                //chequea comandos y mensajes desde el servidor
                if (Funciones.modINIs.ReadINI("DATOS", "INFO", "0") == "1") //Or vg.RecienRegistrado Or vg.NoConn
                {
                    util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico, util.BackgroundTasks.Updater.UpdateType.UpdateAppConfig);
                    updater.run();
                }
            }
        }


        private bool perteneceAZona(string zona)
        {
            string txtZona = zona.Trim();
            if (txtZona.Length == 0)
            {
                return false;
            }
            //204;205;3*;08* ver si Global01.Zona
            if (txtZona.Contains('*'))
            {
                return Global01.Zona.StartsWith(txtZona.Replace("*", ""));
            }
            else
            {
                return Global01.Zona == txtZona;
            }
        }

        private bool filtra(System.Data.DataRow row)
        {
            if (DBNull.Value.Equals(row["zonas"]))
            {
                return true;
            }
            string zonas = (string)row["zonas"];
            bool pasaFiltro = false;
            if (zonas.Length == 0)
            {
                return true;
            }
            else
            {
                string[] splitted = zonas.Split(';');
                
                foreach (string zona in splitted)
                {
                    pasaFiltro |= perteneceAZona(zona);
                    if (pasaFiltro)
                    {
                        break;
                    }
                }
                //204;205;3*;08* ver si Global01.Zona
            }
            return pasaFiltro;
        }

        private System.Data.DataTable filter(System.Data.DataTable table)
        {

            for (int i =0; i < table.Rows.Count;i++)
            {
                System.Data.DataRow row = table.Rows[i];
                if (!filtra(row))
                {
                    row.Delete();
                }
            }
            table.AcceptChanges();
            return table;
        }

        private void procesarRegistro(System.Data.DataRow row)
        {
            string tipo = DBNull.Value.Equals(row["pTipo"]) ? "" : (string)row["pTipo"];
            tipo = tipo.ToUpper();
            switch (tipo)
            {
                case "MDB":
                    {
                        MainMod.update_productos();
                        //Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                        //existencia.getExistencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);
                    }
                    break;
                case "UPDATECONFIG":
                    {
                    }
                    break;
                case "UPDATEPRODUCTOS":
                    {
                    }
                    break;
                default:
                    break;
            }
        }

        private void procesarYBorrarRegistrosCatalogo(DataTable tabla)
        {
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                System.Data.DataRow row = tabla.Rows[i];
                if (row["origen"].ToString().ToUpper() == "CATALOGO")
                {
                    procesarRegistro(row);
                    row.Delete();
                }
                /*---DIEGO ---
                 traer primero registros con origen="catalogo" (ojo tener en cta siempre el destino )
                 * y ejecutar procesos acorde al tipo ws=webservice mdb=actualizacion
                 * 
                 * aca.....ejemplo llamar a void.ejecutarNovedad....(implementar)
                 *         if (pTipo == "mdb")
                            {
                                MainMod.update_productos();
                                //Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                                //existencia.getExistencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);
                            }
                 * 
                 * otros tipos updateAppConfig.ObtenerComandos, etc, etc
                */
            }
            tabla.AcceptChanges();
        }

        private void procesarInfoNovedades(DataTable dtNovedades)
        {
            foreach (System.Data.DataRow row in dtNovedades.Rows)
            {
                switch (row["Tipo"].ToString())
                {
                    case "url":
                        // Viene en la base, no se hace nada.
                        break;
                    case "texto":
                        // Viene en la base, no se hace nada
                        break;
                    case "pdf":
                        {
                            // Se descarga
                            if (!DBNull.Value.Equals(row["url"]))
                            {
                                download((string)row["url"], (string) row["N_Archivo"], (int)row["id"]);
                            }
                        }
                        break;
                    case "imagen":
                        {
                            // Se descarga
                            if (!DBNull.Value.Equals(row["url"]))
                            {
                                download((string)row["url"], (string)row["N_Archivo"], (int)row["id"]);
                            }
                        }
                        break;
                    case "flash":
                        {
                            // Se descarga
                            if (!DBNull.Value.Equals(row["url"]))
                            {
                                download((string)row["url"], (string)row["N_Archivo"], (int)row["id"]);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void dgvNovedades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvNovedades.Rows[e.RowIndex].Cells["FLeido"].Value == null)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            }
            else
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
            }
        }

        private enum DownloadStatus
        {
            Downloading,
            DownloadError,
            DownloadOK
        };

        private System.Collections.Generic.Dictionary<string, DownloadStatus> downloadFiles;

        private void download(string url, string archivo, int id)
        {
            string dest = String.Format("{0}\\imagenes\\Novedades\\{1}_{2}", Global01.AppPath, id, archivo);
            bool doDownload = true;
            if (downloadFiles.ContainsKey(dest))
            {
                switch (downloadFiles[dest])
                {
                    case DownloadStatus.DownloadError:
                        // aca hay que repetir
                        break;
                    case DownloadStatus.Downloading:
                        // Ya se esta descargando no es necesario repetir.
                        doDownload = false;
                        break;
                    case DownloadStatus.DownloadOK:
                        // Ya se descargo. No es necesario repetir
                        doDownload = false;
                        break;
                    default:
                        // Aca no llegaria nunca... Just in case.
                        doDownload = false;
                        break;
                }
            }

            if (doDownload)
            {
                downloadFiles[dest] = DownloadStatus.Downloading;
                util.FileDownloader download = new util.FileDownloader(url+ archivo, dest, id, util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                download.onFileDownloaded += onFileDownloaded;
                download.onFileDownloading += onFileDownloading;
                download.onFileProblem += onFileProblem;
                download.run();
            }
        }

        private void onFileDownloaded(object Tag, string Destino)
        {
            downloadFiles[Destino] = DownloadStatus.DownloadOK;
        }

        private void onFileProblem(object Tag, string Destino, string cause)
        {
            downloadFiles[Destino] = DownloadStatus.DownloadError;
        }

        private void onFileDownloading(object Tag, string Destino, int progress)
        {
        }

    }
}
