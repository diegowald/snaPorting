using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Catalogo.util.emitter_receiver;


namespace Catalogo._productos
{
    public partial class SearchFilter : UserControl, util.emitter_receiver.IEmisor<string>
    {


      
        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        private DataTable useTable = new DataTable();
   
        private string strComando = "SELECT " +
               "mid(c.C_Producto,5) as C_Producto, c.Linea, c.Precio, c.PrecioOferta, c.Precio as PrecioLista, c.Familia, c.Marca, c.Modelo, c.N_Producto, c.Motor, c.Año, c.O_Producto as Medidas, c.ReemplazaA, c.Contiene, c.Equivalencia, c.Original, c.Abc, c.Alerta, " +
               "c.LineaPorcentaje, c.ID, c.Control, c.C_Producto as CodigoAns,  c.MiCodigo,  c.Suspendido, c.OfertaCantidad, c.Tipo, DateDiff('d',c.Vigencia,Date()) as Vigencia " +
               "FROM v_CatVehProdLin AS c";

        private string strSQLCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Catalogo ANS\\datos\\catalogo.mdb;Persist Security Info=True;Password=video80min;User ID=inVent;Jet OLEDB:System database=C:\\Windows\\Help\\kbappcat.hlp";

        private util.BackgroundReader.BackgroundDataLoader backgroundWorker;

        private string filterString = string.Empty;

        private int currentRowCount = 0;
        private int dataRowCount = 0;

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
            
            backgroundWorker = new util.BackgroundReader.BackgroundDataLoader(Catalogo.util.BackgroundReader.BackgroundDataLoader.JOB_TYPE.Asincronico,
                strSQLCon);
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

        private void showItemCounts()
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

        private void btnApply0_Click(object sender, EventArgs e)
        {
            this.emitir(filterString);
        }
    }
}
