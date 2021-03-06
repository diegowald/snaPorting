﻿using System;
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

        private enum CONTROL
        {
            IMAGEN,
            BROWSER,
            FLASH,
            TIPS
        }

        private int dataRowCount = 0;
        private DataTable dtNovedades = new DataTable();
        private DataView dvNovedades = new DataView();

        private Catalogo.varios.FlashControl flash;
        private Catalogo._novedades.ucTips tips;

        public ucNovedades()
        {
            InitializeComponent();
            flash = new varios.FlashControl();
            flash.AutoScroll = false;
            //flash.Dock = System.Windows.Forms.DockStyle.Fill
            //flash.Location = new System.Drawing.Point(0, 0);
            flash.Name = "novflash";

            this.splitC1.Panel1.Controls.Add(this.flash);

            tips = new ucTips();
            this.splitC1.Panel1.Controls.Add(tips);

            pictureBox.Dock = DockStyle.Fill;
            webBrowser.Dock = DockStyle.Fill;
            //flash.Dock = DockStyle.Fill;
            tips.Dock = DockStyle.Fill;

            downloadFiles = new Dictionary<string, DownloadStatus>();

            // al iniciar las noveedades se debe ver el pictureBox con la imaggn

            pictureBox.Visible = true;
            webBrowser.Visible = false;
            flash.Visible = false;
            tips.Visible = false;
            
            Catalogo.varios.NotificationCenter.instance.refreshNovedades += refreshNovedades;
        }

        
        private delegate void refreshNovedadesDelegate();
        private void refreshNovedades()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new refreshNovedadesDelegate(refreshNovedades), null);
                return;
            }
            loadDataGridView();
        }
         
        private void ucNovedades_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void loadDataGridView()
        {

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
            dgvNovedades.ColumnCount = 12;

            //Add Columns
            //"Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID";

            dgvNovedades.Columns[0].Name = "Descripcion";
            dgvNovedades.Columns[0].HeaderText = "Descripción";
            dgvNovedades.Columns[0].DataPropertyName = "Descripcion";
            dgvNovedades.Columns[0].Width = 360;

            dgvNovedades.Columns[1].Name = "F_Inicio";
            dgvNovedades.Columns[1].HeaderText = "Desde";
            dgvNovedades.Columns[1].DataPropertyName = "F_Inicio";
            dgvNovedades.Columns[1].Visible = false;

            dgvNovedades.Columns[2].Name = "F_Fin";
            dgvNovedades.Columns[2].HeaderText = "Fecha";
            dgvNovedades.Columns[2].DataPropertyName = "F_Fin";
            dgvNovedades.Columns[2].Visible = false;

            dgvNovedades.Columns[3].Name = "N_Archivo";
            dgvNovedades.Columns[3].HeaderText = "N_Archivo";
            dgvNovedades.Columns[3].DataPropertyName = "N_Archivo";
            dgvNovedades.Columns[3].Width = 1440;
            dgvNovedades.Columns[3].Visible = false;

            dgvNovedades.Columns[4].Name = "url";
            dgvNovedades.Columns[4].HeaderText = "url";
            dgvNovedades.Columns[4].DataPropertyName = "url";
            dgvNovedades.Columns[4].Visible = false;

            dgvNovedades.Columns[5].Name = "zonas";
            dgvNovedades.Columns[5].HeaderText = "zonas";
            dgvNovedades.Columns[5].DataPropertyName = "zonas";
            dgvNovedades.Columns[5].Visible = false;

            dgvNovedades.Columns[6].Name = "fecha";
            dgvNovedades.Columns[6].HeaderText = "fecha Publicación";
            dgvNovedades.Columns[6].DataPropertyName = "fecha";
            dgvNovedades.Columns[6].Visible = false;

            dgvNovedades.Columns[7].Name = "origen";
            dgvNovedades.Columns[7].HeaderText = "origen";
            dgvNovedades.Columns[7].DataPropertyName = "origen";
            dgvNovedades.Columns[7].Visible = false;

            dgvNovedades.Columns[8].Name = "tipo";
            dgvNovedades.Columns[8].HeaderText = "tipo";
            dgvNovedades.Columns[8].DataPropertyName = "tipo";
            dgvNovedades.Columns[8].Visible = false;

            dgvNovedades.Columns[9].Name = "id";
            dgvNovedades.Columns[9].HeaderText = "id";
            dgvNovedades.Columns[9].DataPropertyName = "id";
            dgvNovedades.Columns[9].Visible = false;

            dgvNovedades.Columns[10].Name = "FLeido";
            dgvNovedades.Columns[10].HeaderText = "FLeido";
            dgvNovedades.Columns[10].DataPropertyName = "F_Leido";
            dgvNovedades.Columns[10].Visible = false;

            dgvNovedades.Columns[11].Name = "Destino";
            dgvNovedades.Columns[11].HeaderText = "Destino";
            dgvNovedades.Columns[11].DataPropertyName = "Destino";
            dgvNovedades.Columns[11].Visible = false;

            string wDestino2 = "cliente"; //valores posibles "ambos;cliente;viajante;catalogo"
            if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente)
            {
                wDestino2 = "viajante";
            };

            string wCondicion = "activo=1 and (destino='ambos' or destino='" + wDestino2 + "') and origen<>'catalogo' " +
                                " and f_inicio<=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "# and f_fin>=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "#";

            string wOrden = "F_Inicio DESC, Tipo";
            string wCampos = "Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID, F_Leido, Destino";

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

            if (dataRowCount > 0)
            {
                dgvNovedades.Rows[0].Selected = true;
            }
 
        }
        private void dgvNovedades_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                                   int.Parse(row.Cells["id"].Value.ToString()),
                                   row.Cells["FLeido"].Value == null ? " " : row.Cells["FLeido"].Value.ToString());
                }
             
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex >= 0)
        //        {
        //            DataGridViewCell cell = dgvNovedades[e.ColumnIndex, e.RowIndex];

        //            if (cell != null)
        //            {
        //                DataGridViewRow row = cell.OwningRow;

        //                mostrarNovedad(row.Cells["Descripcion"].Value.ToString(),
        //                               row.Cells["N_Archivo"].Value.ToString(),
        //                               row.Cells["url"].Value.ToString(),
        //                               row.Cells["Origen"].Value.ToString(),
        //                               row.Cells["Tipo"].Value.ToString(),
        //                               int.Parse(row.Cells["id"].Value.ToString()),
        //                               row.Cells["FLeido"].Value == null ? "" : row.Cells["FLeido"].Value.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        util.errorHandling.ErrorLogger.LogMessage(ex);
        //        util.errorHandling.ErrorForm.show();
        //    }
        //}
        
        private void mostrarNovedad(string pDescripcion, string pArchivo, string pUrl, string pOrigen, string pTipo, int id, string FLeido)
        {
            bool tOk = false;
            string localFile = String.Format("{0}\\imagenes\\Novedades\\{1}", Global01.AppPath, pArchivo);
            
            if (System.IO.File.Exists(localFile))  tOk = true;

            switch (pTipo)
            {
                case "url":
                    {
                        System.Diagnostics.Process.Start(pUrl + pArchivo);
                    }
                    break;
                case "texto":
                    {
                        if (tOk)
                        {
                            mostrarControl(CONTROL.TIPS);
                            //webBrowser.DocumentText = System.IO.File.ReadAllText(localFile);
                            tips.mostrarTips(localFile);
                        }
                    }
                    break;
                case "pdf":
                    {
                        if (tOk) System.Diagnostics.Process.Start(localFile);
                    }
                    break;
                case "imagen":
                    {
                        if (tOk)
                        {
                            mostrarControl(CONTROL.IMAGEN);
                            pictureBox.ImageLocation = localFile;
                        }
                    }
                    break;
                case "flash":
                    {
                        if (tOk)
                        {
                            varios.SwfParser swfParser = new varios.SwfParser();
                            Rectangle rectangle = swfParser.GetDimensions(localFile);
                            flash.Width = rectangle.Width;
                            flash.Height = rectangle.Height;

                            int miX = (this.splitC1.Panel1.Width / 2) - (flash.Width / 2);
                            int miY = (this.splitC1.Panel1.Height / 2) - (flash.Height / 2);
                            flash.Location = new System.Drawing.Point(miX, miY);

                            flash.Anchor = System.Windows.Forms.AnchorStyles.None;

                            mostrarControl(CONTROL.FLASH);

                            flash.file = localFile;
                            flash.play();
                        }
                    }
                    break;
                default: 
                    break;
            }

            if (tOk && FLeido.Trim().Length == 0)
            {
                marcarComoLeido(id);
            }
        }

        private void marcarComoLeido(int id)
        {
            //System.Diagnostics.Debug.Assert(Global01.TranActiva == null);
            string sql = "UPDATE tblNovedadLeido SET F_Leido=Now() WHERE IdNovedad=" + id.ToString();
            Catalogo.Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, sql);

            System.Data.DataRow[] rows = this.dtNovedades.Select("ID = " + id.ToString());
            foreach (System.Data.DataRow row in rows)
            {
                row["F_Leido"] = System.DateTime.Now;
            }
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
                    util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico, util.BackgroundTasks.Updater.UpdateType.UpdateAppConfig);
                    updater.run();
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
                        {
                            // Se descarga
                            if (!DBNull.Value.Equals(row["url"]))
                            {
                                download((string)row["url"], (string) row["N_Archivo"], (int)row["id"]);
                            }
                        }
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
            if (e.ColumnIndex == 0)
            {
                if (this.dgvNovedades.Rows[e.RowIndex].Cells["FLeido"].Value.ToString().Length == 0)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Regular);
                }
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
            string dest = String.Format("{0}\\imagenes\\Novedades\\{1}", Global01.AppPath, archivo);

            if (ExisteNovedadfile(dest)) return;

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
                util.FileDownloader download = new util.FileDownloader(url + archivo, dest, id, util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                if (archivo == "banner.swf")
                {
                    download.onFileDownloaded += onBannerDownloaded;
                }
                else
                {
                    download.onFileDownloaded += onFileDownloaded;
                }
                download.onFileDownloading += onFileDownloading;
                download.onFileProblem += onFileProblem;
                download.run();
            }
        }

        private void onBannerDownloaded(object Tag, string Destino)
        {
            downloadFiles[Destino] = DownloadStatus.DownloadOK;
            Catalogo.varios.NotificationCenter.instance.requestUpdateBanner(Destino);
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

        private void mostrarControl(CONTROL controlAMostrar)
        {
            switch (controlAMostrar)
            {
                case CONTROL.BROWSER:
                    {
                        flash.Visible = false;
                        flash.stop();
                        pictureBox.Visible = false;
                        webBrowser.Visible = true;
                        tips.Visible = false;
                    }
                    break;
                case CONTROL.FLASH:
                    {
                        flash.Visible = true;
                        pictureBox.Visible = false;
                        webBrowser.Visible = false;
                        tips.Visible = false;
                    }
                    break;
                case CONTROL.IMAGEN:
                    {
                        flash.Visible = false;
                        flash.stop();
                        pictureBox.Visible = true;
                        webBrowser.Visible = false;
                        tips.Visible = false;
                    }
                    break;
                case CONTROL.TIPS:
                    {
                        flash.Visible = false;
                        flash.stop();
                        pictureBox.Visible = false;
                        webBrowser.Visible = false;
                        tips.Visible = true;
                    }
                    break;
                default:
                    break;
            }
        }
       
        private void dgvNovedades_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete  && e.Modifiers == Keys.Control)
            {
                if (dgvNovedades.SelectedRows != null)
                {
                    foreach (DataGridViewRow row in dgvNovedades.SelectedRows)
                    {
                        if (row.Cells["Tipo"].Value.ToString() != "url")
                        {
                            string dest = String.Format("{0}\\imagenes\\Novedades\\{1}", Global01.AppPath, row.Cells["N_Archivo"].Value.ToString());
                            borrarNovedadfile(dest);
                        }
                        dgvNovedades.ClearSelection();
                    }
                }
            }
        }
        
        private void borrarNovedadfile(string fileName)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                    //if (fi.Length == 0)
                    //{
                    //    fi.Delete();
                    //}
                    fi.Delete();
                }
            }
            catch (System.IO.IOException ex)
            {
                //throw ex;
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
            catch (Exception ex)
            {
                //throw ex;
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }

        private bool ExisteNovedadfile(string fileName)
        {
            bool sResultado = false;

            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                    if (fi.Length == 0 | fileName=="banner.swf")
                    {
                        fi.Delete();
                    }
                    else
                    {
                        sResultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
            
            return sResultado;
        }

    }
}
