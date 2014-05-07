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

        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();

        public ucFaltante()
        {
            InitializeComponent();
        
            _ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

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
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
//            paEnviosCbo.SelectedIndex = 2;

            if (cboCliente.SelectedIndex > 0)
            {                
                toolStripStatusLabel1.Text = "Faltante a Pedido de: " + this.cboCliente.Text.ToString();
                CargarClienteNovedades();                
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Recibo para el cliente ..."; }
                LimpiarNovedadCliente();
            }
            Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = (short)cboCliente.SelectedValue;
        }

        private void CargarClienteNovedades()
        {
            DataTable dt = Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblClientesNovedades", "IDCliente=" + cboCliente.SelectedValue.ToString(), "F_Carga DESC");

            RFlistView.Visible = false;
            RFlistView.Items.Clear();
            RFlistView.Tag = "add";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem ItemX = new ListViewItem(string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["F_Carga"].ToString())));

                //alternate row color
                if (i % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else
                {
                    ItemX.BackColor = System.Drawing.SystemColors.Control;  //System.Drawing.Color.FromArgb(255, 255, 192);
                }

                ItemX.SubItems.Add(dr["Novedad"].ToString());
                ItemX.SubItems.Add(dr["ID"].ToString());
                RFlistView.Items.Add(ItemX);
            }

            if (dt.Rows.Count > 0) Funciones.util.AutoSizeLVColumnas(ref RFlistView);

            RFlistView.Visible = true;
            dt = null;

            CliNPnlMain.Enabled = true;
            CliNPnlTop.Enabled = true;
            CliNFechaDtp.Value = DateTime.Today.Date;
            CliNidLbl.Text = "0";
            CliNNovedadTxt.Text = "";
        }

        private void LimpiarNovedadCliente()
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
                    MessageBox.Show("Ingrese Observación", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    CliNNovedadTxt.Focus();
                }
            }
        
            return wDatosValidos;
        }

        public static void Faltante_Imprimir(string NroFaltante)
        {
            //string sReporte =  @"D:\Users\pablo\Documents\Visual Studio 2012\Projects\wf_35c_pruebas1\Recibo_Enc4.rpt";

            string sReporte = Global01.AppPath + "\\Reportes\\Recibo_Enc3.rpt";
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroRecibo", NroFaltante);

            varios.fReporte f = new varios.fReporte();
            f.Text = "Recibo n° " + NroFaltante;
            f.DocumentoNro = "RE-" + NroFaltante;           
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
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
                
                cmd.Connection = Global01.Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ClientesNovedades_add";
    
                //if (Global01.TranActiva != null)
                //{
                //    cmd.Transaction = Global01.TranActiva;
                //}
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

        private void btnVer_Click(object sender, EventArgs e)
        {

        }

    } //fin clase
} //fin namespace