using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._movimientos
{
    public partial class ucMovimientos : UserControl
     {
       
        private const string m_sMODULENAME_ = "ucMovimientos";

        public ucMovimientos()
        {
            InitializeComponent();

            if (!Global01.AppActiva)
            {
                this.Dispose();
            };

            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            }

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Movimientos para el cliente: " + this.cboCliente.Text.ToString();
            }
            else
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Movimientos para el cliente ..."; }
            };
            ObtenerMovimientos();
           // EnviosCbo.SelectedIndex = 0;
        }

         private void ucMovimientos_Load(object sender, EventArgs e)
        {         
            DocTipoCbo.SelectedIndex = 0;
            paEnviosCbo.SelectedIndex = 0;
            ObtenerMovimientos();
        }

        private void EnviosCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerMovimientos();
        }

        private void DocTipoCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerMovimientos();
        }

        private void ObtenerMovimientos()
        {
            movDataGridView.Visible = false;

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
            System.Data.OleDb.OleDbDataReader dr = null;

            if (paEnviosCbo.SelectedIndex == 0)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, DocTipoCbo.Text.ToString());
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, DocTipoCbo.Text.ToString());
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, DocTipoCbo.Text.ToString());
            }

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Selec", System.Type.GetType("System.Boolean"));  
   
                    dt.Load(dr);

                    movDataGridView.AutoGenerateColumns = true;
                    movDataGridView.DataSource = dt;
                    
                    if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                    {
                        movDataGridView.Columns["Selec"].Visible = true;
                        //if (movDataGridView.Columns.Count > 0) // Add Checkbox column only when records are present.
                        //    AddCheckBoxColumn();
                    }
                    else
                    {
                        movDataGridView.Columns["Selec"].Visible = false;
                    };

                    movDataGridView.Refresh();
                    movDataGridView.Visible = true;
                }
            }
        }


        //private void AddCheckBoxColumn()
        //{
        //    DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
        //    doWork.Name = "Select";
        //    doWork.HeaderText = "Selec.";
        //    doWork.FalseValue = 0;
        //    doWork.TrueValue = 1;
        //    movDataGridView.Columns.Insert(0, doWork);
        //}

        private void movDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (Global01.AppActiva)
            {

                DataGridViewCell cell = movDataGridView[e.ColumnIndex, e.RowIndex];

                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper()=="NOTA")
                    {
                        _pedidos.ucPedido.Pedido_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                    {
                        _recibos.ucRecibo.Recibo_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                    {
                        _devoluciones.ucDevolucion.Devolucion_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                    {
                        //IntDep_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "REND")
                    {
                        //Rendicion_Imprimir(row.Cells["Nro"].Value.ToString());
                    };
                };
            };
        }

        private void movDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (Global01.AppActiva)
            {
                //[CTRL + M] ' Marcado de Pedidos, Recibos y Devoluciones como enviadas en forma manual
                if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
                {
                    if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                    {
                        if (movDataGridView.SelectedRows != null)
                        {
                            if (MessageBox.Show("CUIDADO!! a los Items marcados NO podrá enviarlos electrónicamente, ¿Está Seguro?", "Marcando como: ENVIADO EN FORMA MANUAL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (DataGridViewRow row in movDataGridView.SelectedRows)
                                {
                                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                                    {
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Pedido_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblPedido_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroPedido='" + row.Cells["Nro"].Value.ToString() + "'");
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                                    {
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblDevolucion_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroDevolucion='" + row.Cells["Nro"].Value.ToString() + "'");
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                                    {
                                        MessageBox.Show("opción no disponible para recibos", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Recibo_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                        //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblRecibo_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroRecibo='" + row.Cells["Nro"].Value.ToString() + "'");
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                                    {
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_InterDeposito_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                        //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblInterDepotito SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroInterDeposito='" & row.Cells["Nro"].Value.ToString() + "'");
                                    }
                                    //paDataGridView.Rows.Remove(row);
                                    movDataGridView.ClearSelection();
                                }
                                ObtenerMovimientos();
                            }
                        }
                    }
                };

            }
        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            if (Global01.AppActiva)
            {

                if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                {
                    if (movDataGridView.SelectedRows != null)
                    {
                        if (MessageBox.Show("Tiene movimientos que aun no ha enviado. ¿QUIERE ENVIARLOS AHORA?", "Envio de Movimientos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow row in movDataGridView.Rows)
                            {
                                if (row.Cells["Selec"].Value != null && row.Cells["Selec"].Value.ToString() != "" && (bool)row.Cells["Selec"].Value)
                                {
                                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                                    {
                                        MessageBox.Show(row.Cells["Origen"].Value.ToString() + " - " + row.Cells["Nro"].Value.ToString(), "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                                    {
                                        MessageBox.Show(row.Cells["Origen"].Value.ToString() + " - " + row.Cells["Nro"].Value.ToString(), "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                                    {
                                        MessageBox.Show(row.Cells["Origen"].Value.ToString() + " - " + row.Cells["Nro"].Value.ToString(), "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                                    {
                                        MessageBox.Show(row.Cells["Origen"].Value.ToString() + " - " + row.Cells["Nro"].Value.ToString(), "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    row.Cells["Selec"].Value = false;
                                }
                            }

                            ObtenerMovimientos();
                        }
                    }
                }
            }
        }

    } //fin clase
} //fin namespace