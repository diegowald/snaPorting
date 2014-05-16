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

namespace Catalogo._clientesNovedades
{
    public partial class ucVisita : UserControl,
        Funciones.emitter_receiver.IReceptor<short> // Para recibir una notificacion de cambio del cliente seleccionado
    {
        private ToolTip _ToolTip = new System.Windows.Forms.ToolTip();
        private string wCondicion = "ALL"; //"Tipo='faltante'";

        public ucVisita()
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

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref rfTipoCbo, "ansTipoNovedades", "Descrip", "ID", "Activo<>1", "ID", true, false, "NONE");

            CargarClienteNovedades();                
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {                
                toolStripStatusLabel1.Text = "Novedad de: " + this.cboCliente.Text.ToString();
                HabilitarClienteNovedades();
                wCondicion = "IDCliente=" + cboCliente.SelectedValue.ToString();    //"Tipo='faltante' and IDCliente=" + cboCliente.SelectedValue.ToString();
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Novedad de ..."; }
                InhabilitarNovedadCliente();
                wCondicion = "ALL";
            }
            Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = (short)cboCliente.SelectedValue;
            CargarClienteNovedades();                
        }

        private void CargarClienteNovedades()
        {
            DataTable dt = Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "v_ClientesNovedades", wCondicion, "F_Carga DESC, IdCliente, Tipo");

            rflistView.Visible = false;
            rflistView.Items.Clear();
            rflistView.Tag = "add";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem ItemX = new ListViewItem(string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["F_Carga"].ToString())));
                ItemX.BackColor = ((rflistView.Items.Count % 2 == 0) ? Color.White : ItemX.BackColor = System.Drawing.SystemColors.Control);

                ItemX.SubItems.Add(dr["Novedad"].ToString());
                ItemX.SubItems.Add(dr["ID"].ToString());
                ItemX.SubItems.Add(dr["RazonSocial"].ToString() + " (" + dr["IdCliente"].ToString().PadLeft(5,'0') + ")");
                ItemX.SubItems.Add(dr["Tipo"].ToString());
                ItemX.SubItems.Add(dr["idTipo"].ToString());
                ItemX.SubItems.Add(dr["idCliente"].ToString());
                ItemX.SubItems.Add((DBNull.Value.Equals(dr["F_Transmicion"]) ? "0" : "1"));                
                rflistView.Items.Add(ItemX);
            }

            //if (dt.Rows.Count > 0) Funciones.util.AutoSizeLVColumnas(ref RFlistView);

            rflistView.Visible = true;
            dt = null;

            CliNPnlMain.Enabled = true;
            CliNPnlTop.Enabled = true;
            rfFechaDtp.Value = DateTime.Today.Date;
            rfIDLbl.Text = "0";
            rfNovedadTxt.Text = "";
        }

        private void HabilitarClienteNovedades()
        {
            CliNPnlMain.Enabled = true;
            CliNPnlTop.Enabled = true;
            rfFechaDtp.Value = DateTime.Today.Date;
            rfIDLbl.Text = "0";
            rfNovedadTxt.Text = "";
            rfTipoCbo.SelectedIndex = 0;
        }

        private void InhabilitarNovedadCliente()
        {
            CliNPnlMain.Enabled = false;
            CliNPnlTop.Enabled = false;
            rfFechaDtp.Value = DateTime.Today.Date;
            rfIDLbl.Text = "0";
            rfNovedadTxt.Text = "";
        }

        private bool datosvalidos(string pCampo)
        {
            bool wDatosValidos = true;
            if (pCampo.ToLower()=="clientenovedad") // | pCampo.ToLower()=="all" | pCampo.ToLower()=="recibo" )
            {
                if ( cboCliente.SelectedIndex <= 0)
                {
                    MessageBox.Show("Ingrese Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    cboCliente.Focus();
                }

                if (rfNovedadTxt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese Detalle", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    rfNovedadTxt.Focus();
                }
                
                if (rfTipoCbo.SelectedIndex <= 0)
                {
                    MessageBox.Show("Ingrese Tipo de Novedad", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    rfTipoCbo.Focus();
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

        private void rflistView_DoubleClick(object sender, EventArgs e)
        {
            if (rflistView.SelectedItems != null & rflistView.SelectedItems.Count > 0)
            {
                if (Int16.Parse(cboCliente.SelectedValue.ToString()) == Int16.Parse(rflistView.SelectedItems[0].SubItems[6].Text))
                {
                    if (rflistView.SelectedItems[0].SubItems[7].Text == "0" | rflistView.SelectedItems[0].SubItems[5].Text=="4")
                    {
                        rfFechaDtp.Value = DateTime.Parse(rflistView.SelectedItems[0].Text);
                        rfNovedadTxt.Text = rflistView.SelectedItems[0].SubItems[1].Text;
                        rfIDLbl.Text = rflistView.SelectedItems[0].SubItems[2].Text;
                        Funciones.util.BuscarIndiceEnCombo(ref rfTipoCbo, rflistView.SelectedItems[0].SubItems[4].Text, false);
                        //rfTipoCbo.SelectedValue = rflistView.SelectedItems[0].SubItems[5].Text;
                        rflistView.Tag = "upd";
                        rflistView.Enabled = false;
                        rfNovedadTxt.Focus();
                    }
                }
            }
        }

        private void rflistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (rflistView.SelectedItems != null & rflistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                    if (Int16.Parse(rflistView.SelectedItems[0].SubItems[2].Text) > 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblClientesNovedades WHERE ID=" + rflistView.SelectedItems[0].SubItems[2].Text);
                    }
                    rflistView.Items.Remove(rflistView.SelectedItems[0]);
                    rflistView.SelectedItems.Clear();
                }
            }
        }

        private void rfAgregarBtn_Click(object sender, EventArgs e)
        {
           
            if (datosvalidos("clientenovedad"))
            {
                int xID = Int16.Parse(rfIDLbl.Text);   
                if (rflistView.Tag.ToString() == "upd")
                {
                    if (rflistView.SelectedItems != null & rflistView.SelectedItems.Count > 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblClientesNovedades SET Novedad='" + rfNovedadTxt.Text.ToUpper() + "', F_Carga=#" + string.Format("{0:dd/MM/yyyy}", rfFechaDtp.Value) + "# WHERE ID=" + xID);

                        rflistView.SelectedItems[0].Text = string.Format("{0:dd/MM/yyyy}", rfFechaDtp.Value);
                        rflistView.SelectedItems[0].SubItems[1].Text = rfNovedadTxt.Text.ToUpper();
                        rflistView.Tag = "add";
                    }
                }
                else
                {
                    ClientesNovedades_add(Int16.Parse(cboCliente.SelectedValue.ToString()), rfFechaDtp.Value, rfNovedadTxt.Text.ToUpper(), byte.Parse(rfTipoCbo.SelectedValue.ToString()), ref xID);

                    ListViewItem ItemX = new ListViewItem(string.Format("{0:dd/MM/yyyy}", rfFechaDtp.Value));                 
                    ItemX.BackColor = ((rflistView.Items.Count % 2 == 0) ? Color.White : ItemX.BackColor = System.Drawing.SystemColors.Control);
                    ItemX.SubItems.Add(rfNovedadTxt.Text.ToUpper());
                    ItemX.SubItems.Add(xID.ToString());
                    ItemX.SubItems.Add(cboCliente.Text.ToString());
                    ItemX.SubItems.Add(rfTipoCbo.Text.ToString());
                    ItemX.SubItems.Add(rfTipoCbo.SelectedValue.ToString());
                    ItemX.SubItems.Add(cboCliente.SelectedValue.ToString());
                    ItemX.SubItems.Add("0");
                    rflistView.Items.Add(ItemX);
                }

                rfNovedadTxt.Text = "";
                rfIDLbl.Text = "0";
                rflistView.Enabled = true;
            }
        }

        private void ClientesNovedades_add(int pIdCliente,DateTime pFecha, string pNovedad, byte pTipo, ref int pID)
        {
            try
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = pIdCliente;
                cmd.Parameters.Add("pF_Carga", System.Data.OleDb.OleDbType.Date).Value = pFecha;
                cmd.Parameters.Add("pNovedad", System.Data.OleDb.OleDbType.VarChar, 64).Value = pNovedad;
                cmd.Parameters.Add("pTipo", System.Data.OleDb.OleDbType.TinyInt).Value = pTipo;
                
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

        private void rfTipoCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rfTipoCbo.SelectedValue.ToString())
            {
                case "1": // Faltante
                    rfDetalleLbl.Text = "Producto (Línea; Marca; Modelo; Código; Observaciones;)";
                    break;
                case "2": // Dif. Precio
                    rfDetalleLbl.Text = "Producto (Línea; Código; Precio; Observaciones)";
                    break;
                case "3": // Preventa
                    rfDetalleLbl.Text = "Producto (Código; Cantidad; Observaciones)";
                    break;
                case "4": // Novedad Cliente
                    rfDetalleLbl.Text = "Novedad";
                    break;
                default: 
                    rfDetalleLbl.Text = "Descripción";
                    break;
            }            
        }

    } //fin clase
} //fin namespace