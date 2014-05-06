using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Catalogo._productos
{
    public partial class PorcentajeLinea : Form
    {

        private System.Collections.Specialized.OrderedDictionary Filter_Linea = new System.Collections.Specialized.OrderedDictionary();

        public PorcentajeLinea()
        {
            InitializeComponent();
            
            _productos.FilterBuilder fb = new _productos.FilterBuilder();

            if (cboLineas.Items.Count < 1)
            {
                DataTable dtLineas = new DataTable();
                dtLineas = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblLineas");
                fb.PopulateFilter(ref Filter_Linea, dtLineas, "Linea");
                String[] filterQuantityArray = new String[Filter_Linea.Count];
                Filter_Linea.Keys.CopyTo(filterQuantityArray, 0);
                cboLineas.Items.Clear();
                cboLineas.Items.AddRange(filterQuantityArray);
                cboLineas.SelectedIndex = 0;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (cboLineas.SelectedIndex <= 0)
            {
                errormessage.Text = "Ingrese Línea";
                cboLineas.Focus();
                return;
            }

            if (float.Parse(PorcentajeLineaTxt.Text.ToString()) == 0)
            {
                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblLineasPorcentaje WHERE Linea='" + this.cboLineas.Text.ToString().Trim().ToUpper() + "'");
                if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0) { plistView.Items.Remove(plistView.SelectedItems[0]); }
                return;
            }

            if (plistView.Tag.ToString() == "upd")
            {
                if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblLineasPorcentaje SET Porcentaje='" + PorcentajeLineaTxt.Text.Replace(",",".") + "' WHERE Linea='" + this.cboLineas.Text.ToString().Trim().ToUpper() + "'");

                    plistView.SelectedItems[0].SubItems[1].Text =  PorcentajeLineaTxt.Text;
                    plistView.Tag = "add";
                }
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "INSERT INTO tblLineasPorcentaje (Linea, Porcentaje) VALUES ('" + this.cboLineas.Text.ToString().Trim().ToUpper() + "', '" + PorcentajeLineaTxt.Text.Replace(",", ".") + "')");
                ListViewItem ItemX = new ListViewItem(this.cboLineas.Text.ToString().Trim().ToUpper());
                ItemX.BackColor = ((plistView.Items.Count % 2 == 0) ? System.Drawing.Color.White : ItemX.BackColor = System.Drawing.SystemColors.Control);
                ItemX.SubItems.Add(string.Format("{0:N2}",PorcentajeLineaTxt.Text));
                plistView.Items.Add(ItemX);
            }

            cboLineas.SelectedIndex = -1;
            PorcentajeLineaTxt.Text = "0,00";
            plistView.Enabled = true;        
        }     
          
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void fPorcentajeLinea_Load(object sender, EventArgs e)
        {
            cboLineas.Focus();
        }

        private void PorcentajeLineaTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void plistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                  Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblLineasPorcentaje WHERE Linea='" +  plistView.SelectedItems[0].Text.Trim().ToUpper() + "'");
                  plistView.Items.Remove(plistView.SelectedItems[0]);
                  plistView.SelectedItems.Clear();
                }
            }
        }

        private void plistView_DoubleClick(object sender, EventArgs e)
        {
            if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0)
            {
                Funciones.util.BuscarIndiceEnCombo(ref cboLineas, plistView.SelectedItems[0].Text, false);
                PorcentajeLineaTxt.Text = plistView.SelectedItems[0].SubItems[1].Text;
                plistView.Tag = "upd";
                PorcentajeLineaTxt.Focus();                
            }
        }

    }
}
