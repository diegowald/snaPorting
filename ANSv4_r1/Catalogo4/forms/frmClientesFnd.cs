using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.varios
{
    public partial class frmClientesFnd : Form
    {
        public frmClientesFnd()
        {
            InitializeComponent();            
        }

        //private void plistView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0)
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            this.Close();
        //        }
        //    }
        //}

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            plistView.Enabled = false;
            plistView.Items.Clear();

            if (txtBuscar.Text.Trim().Length > 0)
            {
                OleDbDataReader dr = null;
                Int16 Num;
                bool isNum = Int16.TryParse(txtBuscar.Text.Trim(), out Num);
                
                string sqlSelect = "SELECT Format([ID],'00000') as ID, Trim([RAZONSOCIAL]) AS razonsocial, mid(Trim([CUIT]),1,2) & '-' & mid(Trim([CUIT]),3,8) & '-' & mid(Trim([CUIT]),11,1) AS cuit, Telefono, Domicilio, Ciudad, Email FROM tblClientes ";
                string sqlWhere = "Activo<>1";
               
                if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", Global01.setDef_EsGerente) == "0") sqlWhere += " and (IdViajante=" + Global01.NroUsuario.ToString() + ")";                      

                if (isNum) 
                {
                    sqlWhere += " and ID=" + txtBuscar.Text.Trim();
                }
                else
                {
                    sqlWhere += " and RazonSocial LIKE '%" + txtBuscar.Text.Trim() + "%'";
                }
        
                dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, sqlSelect + " WHERE " + sqlWhere);
        
                if (dr.HasRows) 
                {
                    Funciones.util.CargarLV(ref plistView,dr);
                    
                    Funciones.util.AutoSizeLVColumnas(ref plistView);

                    if (plistView.SelectedItems != null & plistView.SelectedItems.Count > 0)
                    {
                        plistView.Items[0].Selected = true;
                    }

                    plistView.Enabled = true;
                    plistView.Focus();
                }
        
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void plistView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (plistView.SelectedItems.Count > 0)
            {
                Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = short.Parse(plistView.SelectedItems[0].Text);
            }
        }

        private void plistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
                this.Close();
            };
        }

    }
}
