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
        Funciones.emitter_receiver.IEmisor3<Keys>, // Para emitir el tab en la grilla
        Funciones.emitter_receiver.IReceptor<util.Pair<int, int>> // Para recibir la cantidad de registros encontrados
    {
        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        private DataTable useTable = new DataTable();
   
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
            
            preload.Preloader.instance.productos.onWorkFinished += dataReady;

            xCargarDataControl();
            
        }

        private void xCargarDataControl()
        {
            preload.Preloader.instance.productos.execute();          
        }

        private void dataReady(System.Data.DataTable dataTable)
        {
            dtProducts = dataTable;
            
            showItemCounts(dtProducts.Rows.Count, dtProducts.Rows.Count);

            // Load the Combo Filters
            SetFilters();

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
            }

            if (cboFamilia.Items.Count < 1)
            {
                fb.PopulateFilter(ref Filter_Familia, dtProducts, "Familia");
                String[] filterCatagoryArray = new String[Filter_Familia.Count];
                Filter_Familia.Keys.CopyTo(filterCatagoryArray, 0);
                cboFamilia.Items.Clear();
                cboFamilia.Items.AddRange(filterCatagoryArray);
                cboFamilia.SelectedIndex = 0;
            }

            if (cboMarca.Items.Count < 1)
            {
                fb.PopulateFilter(ref Filter_Marca, dtProducts, "Marca");
                String[] filterQuantityArray = new String[Filter_Marca.Count];
                Filter_Marca.Keys.CopyTo(filterQuantityArray, 0);
                cboMarca.Items.Clear();
                cboMarca.Items.AddRange(filterQuantityArray);
                cboMarca.SelectedIndex = 0;
            }

            cboOtros.SelectedIndex = 0;

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
                    //tslItems.Visible = false;
                }
            }
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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

                    }

                    fb = null;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }


        private void btnApply0_Click(object sender, EventArgs e)
        {
            using (new varios.WaitCursor())
            {
                try
                {
                    if (dtProducts != null && dtProducts.Rows.Count > 0)
                    {
                        filterString = string.Empty;
                        FilterBuilder fb = new FilterBuilder();

                        System.Diagnostics.Debug.WriteLine(cboLinea.SelectedText);
                        System.Diagnostics.Debug.WriteLine(cboMarca.SelectedText);
                        System.Diagnostics.Debug.WriteLine(cboModelo.SelectedText);

                        string sLinea = ((cboLinea.SelectedItem != null) ? cboLinea.SelectedItem.ToString() : "");
                        string sFamilia = ((cboFamilia.SelectedItem != null) ? cboFamilia.SelectedItem.ToString() : "");
                        string sMarca = ((cboMarca.SelectedItem != null) ? cboMarca.SelectedItem.ToString() : "");
                        string sModelo = ((cboModelo.SelectedItem != null) ? cboModelo.SelectedItem.ToString() : "");
                        string sOtros = ((cboOtros.SelectedItem != null) ? cboOtros.SelectedItem.ToString() : "");

                        fb.ApplyFilter(ref filterString, "Linea", sLinea);
                        fb.ApplyFilter(ref filterString, "Familia", sFamilia);
                        fb.ApplyFilter(ref filterString, "Marca", sMarca);
                        fb.ApplyFilter(ref filterString, "Modelo", sModelo);

                        string discontinuedValue = string.Empty;
                        string controlValue = string.Empty;

                        if (sOtros == "(todos)") { controlValue = null; }

                        if (sOtros == "Ofertas") { controlValue = "O"; }

                        if (!string.IsNullOrEmpty(controlValue))
                        {
                            fb.ApplyFilter(ref filterString, "Control", controlValue);
                        }

                        if (!string.IsNullOrEmpty(txtBuscar.Text))
                        {
                            fb.ApplyFilter(ref filterString, "(txtBuscar)", txtBuscar.Text.ToUpper());
                        }

                        if (sOtros == "Nuevos")
                        {
                            fb.ApplyFilter(ref filterString, "Vigencia", "30");
                        }

                        fb = null;
                        this.emitir(filterString);

                        float xPorcentaje = 0;
                        if (txtPorcentajeLinea.Text.Trim().Length > 0) xPorcentaje = float.Parse(txtPorcentajeLinea.Text);

                        this.emitir2(xPorcentaje);
                    }
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                    throw ex;  //util.errorHandling.ErrorForm.show();
                }
            }
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    filterString = string.Empty;

                    txtBuscar.Text = string.Empty;

                    if (cboLinea.Items.Count > 0) cboLinea.SelectedIndex = 0;
                    if (cboFamilia.Items.Count > 0) cboFamilia.SelectedIndex = 0;
                    if (cboMarca.Items.Count > 0) cboMarca.SelectedIndex = 0;
                    if (cboModelo.Items.Count > 0) cboModelo.SelectedIndex = 0;

                    cboOtros.SelectedIndex = 0;

                    this.emitir(filterString);
                    this.emitir2(0);
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);                
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        public emisorHandler<string> emisor
        {
            get;
            set;
        }

        public emisorHandler<float> emisor2
        {
            get;
            set;
        }

        public void onRecibir(util.Pair<int, int> dato)
        {
            showItemCounts(dato.first, dato.second);
        }

        private void txtPorcentajeLinea_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtPorcentajeLinea.Text, ref e);
        }

        internal delegate void FocusDelegate();
        internal void FocusOnSearch()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new FocusDelegate(FocusOnSearch));
                return;
            }
            this.ActiveControl = txtBuscar.Control;
            txtBuscar.Clear();
        }

        internal void FocusOnLinea()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new FocusDelegate(FocusOnLinea));
                return;
            }
            this.ActiveControl = cboLinea.Control;
        }

        internal void ResetOnFilter()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new FocusDelegate(ResetOnFilter));
                return;
            }
            btnClearFilters.PerformClick();

            if (float.Parse(txtPorcentajeLinea.Text.ToString()) != 0)
            {
                btnApply0.PerformClick();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.Tab) && (ActiveControl == txtBuscar.Control))
            {
                this.emitir3(keyData);
                return true;
            }
            else if ((keyData == Keys.Tab) && ((ActiveControl == cboLinea.Control) | (ActiveControl == cboFamilia.Control) | (ActiveControl == cboMarca.Control) | (ActiveControl == cboModelo.Control)))
            {
                btnApply0.PerformClick();
                return true;
            }
            else if ((keyData == Keys.Enter) && ((ActiveControl == txtBuscar.Control) | (ActiveControl == txtPorcentajeLinea.Control)))
            {
                btnApply0.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public emisorHandler<Keys> emisor3
        {
            get;
            set;
        }

    }
}
