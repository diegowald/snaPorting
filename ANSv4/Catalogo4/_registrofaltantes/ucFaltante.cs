using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._registrofaltantes
{
    public partial class ucFaltante : UserControl,
        Funciones.emitter_receiver.IReceptor<short> // Para recibir una notificacion de cambio del cliente seleccionado
    {

        private ToolTip _ToolTip = new System.Windows.Forms.ToolTip();
        private string wCondicion = "Tipo='faltante'";

        public ucFaltante()
        {
            InitializeComponent();
        
            _ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

            InhabilitarNovedadCliente();

            cboCliente.SelectedIndexChanged -= cboCliente_SelectedIndexChanged;
            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            }
            cboCliente.SelectedIndexChanged += cboCliente_SelectedIndexChanged;

            Catalogo.varios.NotificationCenter.instance.attachReceptor2(this);
            cboCliente.SelectedValue = Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado;
            CargarClienteNovedades();                
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {                
                toolStripStatusLabel1.Text = "Faltante a Pedido de: " + this.cboCliente.Text.ToString();
                HabilitarClienteNovedades();  
                wCondicion = "Tipo='faltante' and IDCliente=" + cboCliente.SelectedValue.ToString();
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Faltante a Pedido de ..."; }
                InhabilitarNovedadCliente();
                wCondicion = "Tipo='faltante'";
            }
            Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = (short)cboCliente.SelectedValue;
            CargarClienteNovedades();                
        }

        private void CargarClienteNovedades()
        {
            DataTable dt = Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "v_ClientesNovedades", wCondicion, "F_Carga, IdCliente DESC");

            RFlistView.Visible = false;
            RFlistView.Items.Clear();
            RFlistView.Tag = "add";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem ItemX = new ListViewItem(string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["F_Carga"].ToString())));
                ItemX.BackColor = ((RFlistView.Items.Count % 2 == 0) ? Color.White : ItemX.BackColor = System.Drawing.SystemColors.Control);

                ItemX.SubItems.Add(dr["Novedad"].ToString());
                ItemX.SubItems.Add(dr["ID"].ToString());
                ItemX.SubItems.Add(dr["RazonSocial"].ToString() + " (" + dr["IdCliente"].ToString().PadLeft(5,'0') + ")");
                RFlistView.Items.Add(ItemX);
            }

            //if (dt.Rows.Count > 0) Funciones.util.AutoSizeLVColumnas(ref RFlistView);

            RFlistView.Visible = true;
            dt = null;

            CliNPnlMain.Enabled = true;
            CliNPnlTop.Enabled = true;
            CliNFechaDtp.Value = DateTime.Today.Date;
            CliNidLbl.Text = "0";
            CliNNovedadTxt.Text = "";
        }

        private void HabilitarClienteNovedades()
        {
            CliNPnlMain.Enabled = true;
            CliNPnlTop.Enabled = true;
            CliNFechaDtp.Value = DateTime.Today.Date;
            CliNidLbl.Text = "0";
            CliNNovedadTxt.Text = "";
        }

        private void InhabilitarNovedadCliente()
        {
            CliNPnlMain.Enabled = false;
            CliNPnlTop.Enabled = false;
            CliNFechaDtp.Value = DateTime.Today.Date;
            CliNidLbl.Text = "0";
            CliNNovedadTxt.Text = "";
        }

        private bool datosvalidos(string pCampo)
        {
            bool wDatosValidos = true;
            if (pCampo.ToLower()=="clientenovedad") // | pCampo.ToLower()=="all" | pCampo.ToLower()=="recibo" )
            {
                if (CliNNovedadTxt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese Detalle", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    CliNNovedadTxt.Focus();
                }
            }
        
            return wDatosValidos;
        }

        public Funciones.emitter_receiver.emisorHandler<int> emisor
        {
            get;
            set;
        }

        public void onRecibir(short dato)
        {
                cboCliente.SelectedValue = dato;
        }

        private void RFlistView_DoubleClick(object sender, EventArgs e)
        {
            if (RFlistView.SelectedItems != null & RFlistView.SelectedItems.Count > 0)
            {
                CliNFechaDtp.Value = DateTime.Parse(RFlistView.SelectedItems[0].Text);
                CliNNovedadTxt.Text = RFlistView.SelectedItems[0].SubItems[1].Text;
                CliNidLbl.Text = RFlistView.SelectedItems[0].SubItems[2].Text;

                RFlistView.Tag = "upd";                
                RFlistView.Enabled = false;
                CliNNovedadTxt.Focus();
            }
        }

        private void RFlistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (RFlistView.SelectedItems != null & RFlistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                    if (Int16.Parse(RFlistView.SelectedItems[0].SubItems[2].Text) > 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblClientesNovedades WHERE ID=" + RFlistView.SelectedItems[0].SubItems[2].Text);
                    }
                    RFlistView.Items.Remove(RFlistView.SelectedItems[0]);
                    RFlistView.SelectedItems.Clear();
                }
            }
        }

        private void CliNAgregarBtn_Click(object sender, EventArgs e)
        {
           
            if (datosvalidos("clientenovedad"))
            {
                int xID = Int16.Parse(CliNidLbl.Text);   
                if (RFlistView.Tag.ToString() == "upd")
                {
                    if (RFlistView.SelectedItems != null & RFlistView.SelectedItems.Count > 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblClientesNovedades SET Novedad='" + CliNNovedadTxt.Text.ToUpper() + "', F_Carga=#" + string.Format("{0:dd/MM/yyyy}", CliNFechaDtp.Value) + "# WHERE ID=" + xID);

                        RFlistView.SelectedItems[0].Text = string.Format("{0:dd/MM/yyyy}", CliNFechaDtp.Value);
                        RFlistView.SelectedItems[0].SubItems[1].Text = CliNNovedadTxt.Text.ToUpper();
                        RFlistView.Tag = "add";
                    }
                }
                else
                {
                    ClientesNovedades_add(Int16.Parse(cboCliente.SelectedValue.ToString()), CliNFechaDtp.Value, CliNNovedadTxt.Text.ToUpper(), ref xID);

                    ListViewItem ItemX = new ListViewItem(string.Format("{0:dd/MM/yyyy}", CliNFechaDtp.Value));                 
                    ItemX.BackColor = ((RFlistView.Items.Count % 2 == 0) ? Color.White : ItemX.BackColor = System.Drawing.SystemColors.Control);
                    ItemX.SubItems.Add(CliNNovedadTxt.Text.ToUpper());
                    ItemX.SubItems.Add(xID.ToString());
                    ItemX.SubItems.Add(this.cboCliente.Text.ToString());
                    RFlistView.Items.Add(ItemX);
                }

                CliNNovedadTxt.Text = "";
                CliNidLbl.Text = "0";
                RFlistView.Enabled = true;
            }
        }

        private void ClientesNovedades_add(int pIdCliente,DateTime pFecha, string pNovedad, ref int pID)
        {
            try
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = pIdCliente;
                cmd.Parameters.Add("pF_Carga", System.Data.OleDb.OleDbType.Date).Value = pFecha;
                cmd.Parameters.Add("pNovedad", System.Data.OleDb.OleDbType.VarChar, 64).Value = pNovedad;
                cmd.Parameters.Add("pTipo", System.Data.OleDb.OleDbType.VarChar, 10).Value = "faltante";
                
                cmd.Connection = Global01.Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ClientesNovedades_add";

                cmd.ExecuteNonQuery();
                cmd = null;
                
                System.Data.OleDb.OleDbDataReader rec = null;
                rec = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblClientesNovedades", "@@identity");
                rec.Read();
                pID = Int16.Parse(rec["ID"].ToString());
                rec = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }

        }

    } //fin clase
} //fin namespace