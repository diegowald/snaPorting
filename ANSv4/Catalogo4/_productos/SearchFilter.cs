using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Catalogo.Funciones.emitter_receiver;


namespace Catalogo._productos
{
    public partial class SearchFilter : UserControl, 
        Funciones.emitter_receiver.IEmisor<string>, // Para emitir la condicion de filtrado
        Funciones.emitter_receiver.IEmisor2<float>, // Para emitir el porcentaje
        Funciones.emitter_receiver.IReceptor<util.Pair<int, int>> // Para recibir la cantidad de registros encontrados
    {

        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        private DataTable useTable = new DataTable();
   
        private string strComando = "SELECT " +
               "mid(c.C_Producto,5) as C_Producto, c.Linea, c.Precio, c.PrecioOferta, c.Precio as PrecioLista, c.Familia, c.Marca, c.Modelo, c.N_Producto, c.Motor, c.Año, c.O_Producto as Medidas, c.ReemplazaA, c.Contiene, c.Equivalencia, c.Original, c.Abc, c.Alerta, " +
               "c.LineaPorcentaje, c.ID, c.Control, c.C_Producto as CodigoAns,  c.MiCodigo,  c.Suspendido, c.OfertaCantidad, c.Tipo, DateDiff('d',c.Vigencia,Date()) as Vigencia " +
               "FROM v_CatVehProdLin AS c";

        private Funciones.BackgroundReader.BackgroundDataLoader backgroundWorker;

        private string filterString = string.Empty;

        // Dropdown Filter Collections
        private System.Collections.Specialized.OrderedDictionary Filter_Linea =
                   new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Familia =
                          new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Marca =
                         new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Modelo =
                        new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Otros =
                      new System.Collections.Specialized.OrderedDictionary();


        public SearchFilter()
        {
          
            InitializeComponent();
            
            backgroundWorker = new Funciones.BackgroundReader.BackgroundDataLoader(Catalogo.Funciones.BackgroundReader.BackgroundDataLoader.JOB_TYPE.Asincronico,
                Global01.strConexionUs);
            backgroundWorker.onWorkFinishedHandler += dataReady;
            
            xCargarDataControl();

        }

        private static DataTable xGetData(string strConn, string sqlCommand)
        {

            //DataSet oDS = new DataSet();

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlCommand, strConn);

            DataTable table = new DataTable("dtProducts");
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);

            //dataAdapter.Fill(oDS, "dtProducts");

            return table;

        }


        private void xCargarDataControl()
        {

            Cursor.Current = Cursors.WaitCursor;

            backgroundWorker.executeQuery(strComando);
            
        }

        void dataReady(System.Data.DataTable dataTable)
        {
            dtProducts = dataTable;

            // Load the Combo Filters
            SetFilters();

            Cursor.Current = Cursors.Default;
        }

        
        private void SetFilters()
        {
            // Load the dropdowns
            // Note: you will want to call this function any time you have added or deleted
            //       rows in the datatable

            FilterBuilder fb = new FilterBuilder();

            if (cboLinea.Items.Count < 1)
            {
                fb.PopulateFilter(ref Filter_Linea, dtProducts, "Linea");
                String[] filterProductArray = new String[Filter_Linea.Count];
                Filter_Linea.Keys.CopyTo(filterProductArray, 0);
                cboLinea.Items.Clear();
                cboLinea.Items.AddRange(filterProductArray);
                cboLinea.SelectedIndex = 0;
            };

            if (cboFamilia.Items.Count < 1)
            {
                fb.PopulateFilter(ref Filter_Familia, dtProducts, "Familia");
                String[] filterCatagoryArray = new String[Filter_Familia.Count];
                Filter_Familia.Keys.CopyTo(filterCatagoryArray, 0);
                cboFamilia.Items.Clear();
                cboFamilia.Items.AddRange(filterCatagoryArray);
                cboFamilia.SelectedIndex = 0;
            };

            if (cboMarca.Items.Count < 1)
            {
                fb.PopulateFilter(ref Filter_Marca, dtProducts, "Marca");
                String[] filterQuantityArray = new String[Filter_Marca.Count];
                Filter_Marca.Keys.CopyTo(filterQuantityArray, 0);
                cboMarca.Items.Clear();
                cboMarca.Items.AddRange(filterQuantityArray);
                cboMarca.SelectedIndex = 0;
            };

            cboOtros.SelectedIndex = 0;

            //if (cboModelo.Items.Count < 1)
            //{
            //    fb.PopulateFilter(ref Filter_Modelo, dtProducts, "Modelo");
            //    String[] filterPriceArray = new String[Filter_Modelo.Count];
            //    Filter_Modelo.Keys.CopyTo(filterPriceArray, 0);
            //    cboModelo.Items.Clear();
            //    cboModelo.Items.AddRange(filterPriceArray);
            //    cboModelo.SelectedIndex = 0;

            //};

        }

        private void showItemCounts(int currentRowCount, int dataRowCount)
        {

            string _filterMsg = String.Format("#Prod. {0} de {1}", currentRowCount, dataRowCount);
            string _totalMsg = String.Format("#Prod. {0}", dataRowCount);
            if (dataRowCount != currentRowCount)
            {
                tslItems.Text = _filterMsg;
                tslItems.Visible = true;
            }
            else
            {
                if (dataRowCount > 0)
                {
                    tslItems.Text = _totalMsg;
                    tslItems.Visible = true;
                }
                else
                {
                    tslItems.Visible = false;
                }
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            xCargarDataControl();           
        }


        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cboMarca.SelectedItem != null)
            {

                FilterBuilder fb = new FilterBuilder();

                if (cboMarca.SelectedItem.ToString() != "(todos)")
                {

                    fb.PopulateFilter(ref Filter_Modelo, dtProducts, "_mm" + cboMarca.SelectedItem.ToString());
                    String[] filterPriceArray = new String[Filter_Modelo.Count];
                    Filter_Modelo.Keys.CopyTo(filterPriceArray, 0);
                    cboModelo.Items.Clear();
                    cboModelo.Items.AddRange(filterPriceArray);
                    cboModelo.SelectedIndex = 0;

                }
                else
                {
                    fb.PopulateFilter(ref Filter_Modelo, dtProducts, "Modelo");
                    String[] filterPriceArray = new String[Filter_Modelo.Count];
                    Filter_Modelo.Keys.CopyTo(filterPriceArray, 0);
                    cboModelo.Items.Clear();
                    cboModelo.Items.AddRange(filterPriceArray);
                    cboModelo.SelectedIndex = 0;

                };

                fb = null;
            };
        }



        private void btnApply0_Click(object sender, EventArgs e)
        {
            if (dtProducts != null && dtProducts.Rows.Count > 0)
            {
                filterString = string.Empty;
                FilterBuilder fb = new FilterBuilder();

                fb.ApplyFilter(ref filterString, "Linea", cboLinea.SelectedItem.ToString());
                fb.ApplyFilter(ref filterString, "Familia", cboFamilia.SelectedItem.ToString());
                fb.ApplyFilter(ref filterString, "Marca", cboMarca.SelectedItem.ToString());
                fb.ApplyFilter(ref filterString, "Modelo", cboModelo.SelectedItem.ToString());

                string discontinuedValue = string.Empty;
                string controlValue = string.Empty;

                if (cboOtros.SelectedItem.ToString() == "(todos)") { controlValue = null; }

                if (cboOtros.SelectedItem.ToString() == "Ofertas") { controlValue = "O"; }

                if (!string.IsNullOrEmpty(controlValue))
                {
                    fb.ApplyFilter(ref filterString, "Control", controlValue);
                }

                if (!string.IsNullOrEmpty(txtBuscar.Text))
                {
                    fb.ApplyFilter(ref filterString, "(txtBuscar)", txtBuscar.Text.ToUpper());
                }

                if (cboOtros.SelectedItem.ToString() == "Nuevos")
                {
                    fb.ApplyFilter(ref filterString, "Vigencia", "30");
                }

                fb = null;
                this.emitir(filterString);
                this.emitir2(float.Parse(txtPorcentajeLinea.Text));
            }
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            if (dtProducts != null && dtProducts.Rows.Count > 0)
            {
                filterString = string.Empty;

                if (cboLinea.Items.Count > 0) cboLinea.SelectedIndex = 0;
                if (cboFamilia.Items.Count > 0) cboFamilia.SelectedIndex = 0;
                if (cboMarca.Items.Count > 0) cboMarca.SelectedIndex = 0;
                if (cboModelo.Items.Count > 0) cboModelo.SelectedIndex = 0;

                cboOtros.SelectedIndex = 0;

                this.emitir(filterString);
                this.emitir2(0);
            }
        }

        emisorHandler<string> _emisor;
        public emisorHandler<string> emisor
        {
            get
            {
                return _emisor;
            }
            set
            {
                _emisor = value;
            }
        }

        emisorHandler<float> _emisor2;
        emisorHandler<float> IEmisor2<float>.emisor2
        {
            get
            {
                return _emisor2;
            }
            set
            {
                _emisor2 = value;
            }
        }

        public void onRecibir(util.Pair<int, int> dato)
        {
            showItemCounts(dato.first, dato.second);
        }
    }
}
