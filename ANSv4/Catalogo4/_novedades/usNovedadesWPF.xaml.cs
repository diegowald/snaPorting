using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Catalogo.util;

namespace Catalogo._novedades
{
    /// <summary>
    /// Interaction logic for usNovedadesWPF.xaml
    /// </summary>
    public partial class usNovedadesWPF : UserControl
    {
        private int dataRowCount = 0;
        private bool dataGridIdle = false;
        private System.Data.DataTable dtNovedades;
        private System.Data.DataView dvNovedades;

        public usNovedadesWPF()
        {
            InitializeComponent();
            dtNovedades = new System.Data.DataTable();
            dvNovedades = new System.Data.DataView();
        }


        private void loadDataGridView()
        {
            dataGridIdle = false;

            // If rows in grid already exist, clear them
            dgNovedades.ItemsSource = null;
//            dgNovedades.Rows.Clear();

            // For this example, make it a read-only datagrid
            dgNovedades.IsReadOnly = true;
            //dgNovedades.AllowUserToAddRows = false;
            //dgNovedades.AllowUserToDeleteRows = false;

            dgNovedades.SelectionMode = Microsoft.Windows.Controls.DataGridSelectionMode.Single;
//            dgNovedades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            dgNovedades.MultiSelect = false;

//            dgNovedades.RowHeadersWidth = 4;
            dgNovedades.HeadersVisibility = Microsoft.Windows.Controls.DataGridHeadersVisibility.All;

            //Set AutoGenerateColumns False
            dgNovedades.AutoGenerateColumns = false;


            //SELECT n.Descripcion, n.F_Inicio, n.F_Fin, n.N_Archivo, n.url, n.zonas, n.Fecha, n.Origen, n.Tipo, n.ID FROM ansNovedades AS n;

            //Set Columns Count 
            dgNovedades.Columns.Clear();

            //Add Columns
            //"Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID";

            string wDestino2 = "cliente"; //valores posibles "ambos;cliente;viajante;catalogo"
            if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente)
            {
                wDestino2 = "viajante";
            };

            /*---DIEGO ---
             traer primero registros con origen="catalogo" (ojo tener en cta siempre el destino )
             * y ejecutar procesos acorde al tipo ws=webservice mdb=actualizacion
             * 
             * aca.....ejemplo llamar a void.ejecutarNovedad....(implementar)
             *         if (pTipo == "mdb")
                        {
                            MainMod.update_productos();
                            //Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                            //existencia.   Existencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);
                        }
             * 
             * otros tipos updateAppConfig.ObtenerComandos, etc, etc
            */


            string wCondicion = "activo=1 and (destino='ambos' or destino='" + wDestino2 + "') and origen<>'catalogo' "; //+
            // " and f_inicio<=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "# and f_fin>=#" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "#";
            string wOrden = "F_Inicio DESC, Tipo";
            string wCampos = "Descripcion, F_Inicio, F_Fin, N_Archivo, url, zonas, Fecha, Origen, Tipo, ID";

            dtNovedades = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "ansNovedades", wCondicion, wOrden, wCampos);

            // Save the row count in the original datatable
            dataRowCount = dtNovedades.Rows.Count;
            // Create a dataview of the datatable
            dvNovedades.Table = dtNovedades;

            //DIEGO Acá FILTRAR Global01.Zona completado con ceros a la izquierda string de long 3---
            //ejemplo: en BD 204;205;3*;08* ver si Global01.Zona está incluido en el string "zona del DT" OJO con los '*'

            // Filter the dataview
            dvNovedades.RowFilter = ""; //filterString;

            // Bind Datagrid view to the DataView
            dgNovedades.ItemsSource = dvNovedades;
//            dgNovedades.Refresh();

            dataGridIdle = true;

            if (dataRowCount > 0)
            {
                dgNovedades.CurrentCell = new Microsoft.Windows.Controls.DataGridCellInfo(dgNovedades.Items[0], dgNovedades.Columns[0]);
                dgNovedades.SelectedCells.Add(dgNovedades.CurrentCell);
//                dgNovedades.Rows[0].Selected = true;


                Microsoft.Windows.Controls.DataGridCell cell = dgNovedades.GetCell(dgNovedades.GetSelectedRow(), 0);
                if (cell != null)
                {
                    Microsoft.Windows.Controls.DataGridRow row = dgNovedades.GetSelectedRow();

/*                    mostrarNovedad(row.Cells["Descripcion"].Value.ToString(),
                                   row.Cells["N_Archivo"].Value.ToString(),
                                   row.Cells["url"].Value.ToString(),
                                   row.Cells["Origen"].Value.ToString(),
                                   row.Cells["Tipo"].Value.ToString());*/
                }
            }

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadDataGridView();
        }

    }
}
