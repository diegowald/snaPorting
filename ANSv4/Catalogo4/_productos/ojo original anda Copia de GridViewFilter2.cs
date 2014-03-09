﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Catalogo._productos
{
    public partial class GridViewFilter2 : UserControl
    {


        private enum CCol
        {
            cSemaforo = 0,
            cCodigo = 1,
            cLinea = 2,
            cPrecio = 3,
            cFamilia = 4,
            cMarca = 5,
            cModelo = 6,
            cDescripcion = 7,
            cMotor = 8,
            cAño = 9,
            cMedidas = 10,
            cReemplazaA = 11,
            cContiene = 12,
            cEquivalencia = 13,
            cOriginal = 14,
            cPlista = 15,
            cPoferta = 16,
            cRotacion = 17,
            cEvolucion = 18,
            cPorclinea = 19,
            cId = 20,
            cControl = 21,
            cCodigoAns = 22,
            cMiCodigo = 23,
            cSuspendido = 24,
            cOfertaCantidad = 25,
            cTipo = 26,
            cVigencia = 27
        }

        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        private DataTable useTable = new DataTable();
        private bool xAplicoPorcentajeLinea = false;
 
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


        public GridViewFilter2()
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

            // Load the DataGridView
            loadDataGridView();
            // Load the Combo Filters
            SetFilters();

            Cursor.Current = Cursors.Default;
        }

        private void loadDataGridView()
        {
            // If rows in grid already exist, clear them
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            
            // For this example, make it a read-only datagrid
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.RowHeadersWidth = 4;
            dataGridView1.RowHeadersVisible = true;

            //Set AutoGenerateColumns False
            dataGridView1.AutoGenerateColumns = false;

            //Set Columns Count 
            dataGridView1.ColumnCount = 28;

            //Add Columns

            //DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            //buttonColumn.HeaderText = "Semáforo";
            //buttonColumn.Name = "Semáforo";
            //buttonColumn.Text = "Semáforo";
            ////buttonColumn.UseColumnTextForButtonValue = true;

            //dataGridView1.Columns.Add(buttonColumn);

            //// Add a CellClick handler to handle clicks in the button column.
            //dataGridView1.CellClick +=
            //    new DataGridViewCellEventHandler(dataGridView1_CellClick);

            dataGridView1.Columns[(int)CCol.cCodigo].Name = "C_Producto";
            dataGridView1.Columns[(int)CCol.cCodigo].HeaderText = "Código";
            dataGridView1.Columns[(int)CCol.cCodigo].DataPropertyName = "C_Producto";

            dataGridView1.Columns[(int)CCol.cLinea].Name = "Linea";
            dataGridView1.Columns[(int)CCol.cLinea].HeaderText = "Línea";
            dataGridView1.Columns[(int)CCol.cLinea].DataPropertyName = "Linea";

            dataGridView1.Columns[(int)CCol.cFamilia].Name = "Familia";
            dataGridView1.Columns[(int)CCol.cFamilia].HeaderText = "Familia";
            dataGridView1.Columns[(int)CCol.cFamilia].DataPropertyName = "Familia";

            dataGridView1.Columns[(int)CCol.cMarca].Name = "Marca";
            dataGridView1.Columns[(int)CCol.cMarca].HeaderText = "Marca";
            dataGridView1.Columns[(int)CCol.cMarca].DataPropertyName = "Marca";

            dataGridView1.Columns[(int)CCol.cModelo].Name = "cModelo";
            dataGridView1.Columns[(int)CCol.cModelo].HeaderText = "Modelo";
            dataGridView1.Columns[(int)CCol.cModelo].DataPropertyName = "Modelo";

            dataGridView1.Columns[(int)CCol.cDescripcion].Name = "N_Producto";
            dataGridView1.Columns[(int)CCol.cDescripcion].HeaderText = "Descripción";
            dataGridView1.Columns[(int)CCol.cDescripcion].DataPropertyName = "N_Producto";

            dataGridView1.Columns[(int)CCol.cMotor].Name = "Motor";
            dataGridView1.Columns[(int)CCol.cMotor].HeaderText = "Motor";
            dataGridView1.Columns[(int)CCol.cMotor].DataPropertyName = "Motor";

            dataGridView1.Columns[(int)CCol.cAño].Name = "Año";
            dataGridView1.Columns[(int)CCol.cAño].HeaderText = "Año";
            dataGridView1.Columns[(int)CCol.cAño].DataPropertyName = "Año";

            dataGridView1.Columns[(int)CCol.cMedidas].Name = "Medidas";
            dataGridView1.Columns[(int)CCol.cMedidas].HeaderText = "Medidas";
            dataGridView1.Columns[(int)CCol.cMedidas].DataPropertyName = "Medidas";

            dataGridView1.Columns[(int)CCol.cReemplazaA].Name = "ReemplazaA";
            dataGridView1.Columns[(int)CCol.cReemplazaA].HeaderText = "Reemplaza A";
            dataGridView1.Columns[(int)CCol.cReemplazaA].DataPropertyName = "ReemplazaA";

            dataGridView1.Columns[(int)CCol.cContiene].Name = "Contiene";
            dataGridView1.Columns[(int)CCol.cContiene].HeaderText = "Contiene";
            dataGridView1.Columns[(int)CCol.cContiene].DataPropertyName = "Contiene";

            dataGridView1.Columns[(int)CCol.cEquivalencia].Name = "Equivalencia";
            dataGridView1.Columns[(int)CCol.cEquivalencia].HeaderText = "Equivalencia";
            dataGridView1.Columns[(int)CCol.cEquivalencia].DataPropertyName = "Equivalencia";

            dataGridView1.Columns[(int)CCol.cOriginal].Name = "Original";
            dataGridView1.Columns[(int)CCol.cOriginal].HeaderText = "Original";
            dataGridView1.Columns[(int)CCol.cOriginal].DataPropertyName = "Original";

            dataGridView1.Columns[(int)CCol.cPrecio].Name = "Precio";
            dataGridView1.Columns[(int)CCol.cPrecio].HeaderText = "Precio";
            dataGridView1.Columns[(int)CCol.cPrecio].DataPropertyName = "Precio";

            dataGridView1.Columns[(int)CCol.cPlista].Name = "PrecioLista";
            dataGridView1.Columns[(int)CCol.cPlista].HeaderText = "$ Lista";
            dataGridView1.Columns[(int)CCol.cPlista].DataPropertyName = "PrecioLista";

            dataGridView1.Columns[(int)CCol.cPoferta].Name = "PrecioOferta";
            dataGridView1.Columns[(int)CCol.cPoferta].HeaderText = "$ Oferta";
            dataGridView1.Columns[(int)CCol.cPoferta].DataPropertyName = "PrecioOferta";

            dataGridView1.Columns[(int)CCol.cPorclinea].Name = "LineaPorcentaje";
            dataGridView1.Columns[(int)CCol.cPorclinea].HeaderText = "% Línea";
            dataGridView1.Columns[(int)CCol.cPorclinea].DataPropertyName = "LineaPorcentaje";

            dataGridView1.Columns[(int)CCol.cRotacion].Name = "Abc";
            dataGridView1.Columns[(int)CCol.cRotacion].HeaderText = "Rotación";
            dataGridView1.Columns[(int)CCol.cRotacion].DataPropertyName = "Rotacion";

            dataGridView1.Columns[(int)CCol.cEvolucion].Name = "Alerta";
            dataGridView1.Columns[(int)CCol.cEvolucion].HeaderText = "Evolución";
            dataGridView1.Columns[(int)CCol.cEvolucion].DataPropertyName = "Evolucion";
            dataGridView1.Columns[(int)CCol.cEvolucion].Visible = false;

            dataGridView1.Columns[(int)CCol.cVigencia].Name = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].HeaderText = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].DataPropertyName = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].Visible = false;

            dataGridView1.Columns[(int)CCol.cId].Name = "ID";
            dataGridView1.Columns[(int)CCol.cId].HeaderText = "ID";
            dataGridView1.Columns[(int)CCol.cId].DataPropertyName = "ID";
            dataGridView1.Columns[(int)CCol.cId].Visible = false;

            dataGridView1.Columns[(int)CCol.cControl].Name = "Control";
            dataGridView1.Columns[(int)CCol.cControl].HeaderText = "Control";
            dataGridView1.Columns[(int)CCol.cControl].DataPropertyName = "Control";
            dataGridView1.Columns[(int)CCol.cControl].Visible = false;

            dataGridView1.Columns[(int)CCol.cCodigoAns].Name = "CodigoAns";
            dataGridView1.Columns[(int)CCol.cCodigoAns].HeaderText = "C_Producto";
            dataGridView1.Columns[(int)CCol.cCodigoAns].DataPropertyName = "CodigoAns";
            dataGridView1.Columns[(int)CCol.cCodigoAns].Visible = false;

            dataGridView1.Columns[(int)CCol.cMiCodigo].Name = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].HeaderText = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].DataPropertyName = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].Visible = false;

            dataGridView1.Columns[(int)CCol.cSuspendido].Name = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].HeaderText = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].DataPropertyName = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].Visible = false;

            dataGridView1.Columns[(int)CCol.cOfertaCantidad].Name = "OfertaCantidad";
            dataGridView1.Columns[(int)CCol.cOfertaCantidad].HeaderText = "Oferta.Ca.Mín.";
            dataGridView1.Columns[(int)CCol.cOfertaCantidad].DataPropertyName = "OfertaCantidad";

            dataGridView1.Columns[(int)CCol.cTipo].Name = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].HeaderText = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].DataPropertyName = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].Visible = false;

            // Save the row count in the original datatable
            dataRowCount = dtProducts.Rows.Count;
            // Create a dataview of the datatable
            dvProducts.Table = dtProducts;
            // Filter the dataview
            dvProducts.RowFilter = filterString;


            if (!(txtPorcentajeLinea.Text==""))
                if  (Convert.ToDecimal("0"+txtPorcentajeLinea.Text) != 0)
                {
                    xAplicoPorcentajeLinea = true;

                    foreach (DataRowView drv in dvProducts)
                    {
                        drv["Precio"] = (float)drv["PrecioLista"] + ((float)(drv["PrecioLista"]) * float.Parse(txtPorcentajeLinea.Text)) / 100;
                    };
                }
                else
                {
                    if (xAplicoPorcentajeLinea)
                    {
                        xAplicoPorcentajeLinea = false;
                        foreach (DataRowView drv in dvProducts)
                        {
                            drv["Precio"] = drv["PrecioLista"];
                        };
                    };
                }
            else
            {
                txtPorcentajeLinea.Text = "0";
            };


            // Bind Datagrid view to the DataView
            dataGridView1.DataSource = dvProducts;
            // Save the row count in the datagridview
            currentRowCount = dataGridView1.Rows.Count;
            // Show the counts in the toolstrip
            showItemCounts();
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

                loadDataGridView();
            }
        }

        // Apply the filters
        private void btnApply_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

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
                loadDataGridView();
            }

            Cursor.Current = Cursors.Default;
      
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                lblProductoDetalle1.Text = row.Cells["C_Producto"].Value.ToString() + " - ";
                lblProductoDetalle1.Text += row.Cells["N_Producto"].Value.ToString();
                // etc.
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}