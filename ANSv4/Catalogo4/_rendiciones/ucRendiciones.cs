using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; 
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Catalogo._rendiciones
{
    public partial class ucRendiciones : UserControl

    {
        private const string m_sMODULENAME_ = "ucRendiciones";

        // - Definiciones Globales -----------
        private enum tAccion : byte
        {
            Nuevo = 1,
            Modificar = 2,
            Guardar = 4,
            Cancelar = 5,
            Buscar = 7,
            Imprimir = 8,
            Eliminar = 10,
            Cerrar = 12,
            Neutro = 15,
            Listas = 17
        }

        private enum tEstado : byte
        {
            Neutro = 1,
            Nuevo = 2,
            Modificar = 3,
            Buscar = 4,
            Imprimir = 5,
            Eliminar = 6,
            Vista = 7
        }

        private struct TVariablesDelFormulario
        {
            public bool miError;
            public int ID;
            public bool escape;
            public System.Data.OleDb.OleDbDataReader DR;
            public System.Data.DataSet DS;
            public ListViewItem ItemX;
            public int i; 
            public tAccion Accion;
        }
        //Variables Generales
        private TVariablesDelFormulario m;
    
        // - Fin Def. Globales ---------------

        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();

        public ucRendiciones()
        {
            InitializeComponent();

            //_ToolTip.SetToolTip(btnIniciar, "INICIAR Devolución");
            //_ToolTip.SetToolTip(btnImprimir, "Graba e Imprime Devolución ...");
            //_ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            };

            //if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            //{
            //    Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            //}
            //else
            //{
            //    Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            //}

            //Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref devMfDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");
            //Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref devMnDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");
        }

//----------------------------------------------------------

	private bool vAcceso(int Usuario, int Acceso, string Formulario)
	{
		return true;
	}

	private bool DatosValidos(string pCampo)
	{

		bool sResultado = false;

		sResultado = true;

		if (!(m.escape)) {


		if (pCampo.ToLower() == "grabar") {

			if (lblEfectivoC.ForeColor == Color.Red | lblDivDolarC.ForeColor == Color.Red | lblDivEuroC.ForeColor == Color.Red | lblChequesTotalC.ForeColor == Color.Red | lblChequesCantidadC.ForeColor == Color.Red | lblCertificadosTotalC.ForeColor == Color.Red | lblCertificadosCantidadC.ForeColor == Color.Red | lvRecibos.Items.Count < 1) 
            {	//Or lvValores.ListItems.Count < 1 Then
				sResultado = false;
				MessageBox.Show("Error! Debe consolidar la rendición actual", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

		}


		if (pCampo.ToLower() == "valores") {

			if (Strings.Len(Strings.Trim(txtBd_Nro.Text)) > 0) {
				if (Convert.ToDouble(txtBd_Nro.Text) <= 0) {
					sResultado = false;
					MessageBox.Show("Debe ingresar Nro. de Boleta de Depósito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			} else {
				sResultado = false;
				MessageBox.Show("Debe ingresar Nro. de Boleta de Depósito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (Convert.ToDouble(txtBd_Monto.Text) <= 0) {
				sResultado = false;
				MessageBox.Show("Debe ingresar monto del comprobante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (opTipoDeposito(1).Checked == true) {

				if (cboCheques.SelectedIndex < 0) {
					sResultado = false;
					MessageBox.Show("Debe elegir el cheque a rendir", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}

				if (Convert.ToByte(this.txtBdCh_Cantidad.Text) <= 0) {
					MessageBox.Show("Debe ingresar cantidad de cheques", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					sResultado = false;
				}
			}

		}


		return sResultado;

	}

	private void tsbNuevo_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Nuevo);
	}

	private void tsbModificar_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Modificar);
	}

	private void tsbGrabar_Click(System.Object sender, System.EventArgs e)
	{
		if (DatosValidos("all")) {
			Accion_Click(tAccion.Guardar);
		}
	}

	private void tsbCancelar_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Cancelar);
	}

	private void tsbBuscar_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Buscar);
	}

	private void tsbImprimir_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Imprimir);
	}

	private void tsbBorrar_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Eliminar);
	}

	private void tsbCerrar_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Cerrar);
	}

	private void Accion_Click(tAccion Accion)
	{
		
        switch (Accion) {
			case tAccion.Nuevo:

				m.Accion = tAccion.Nuevo;
				CambiarA(tEstado.Nuevo);
				Habilita(m.Accion);

                m.iD = 0;

                cboRecibos.Items.Clear();
                Funciones.util.CargaCombo(Global01.Conexion, ref cboRecibos, "v_Recibo_Enc_noR_cbo", "Descrip", "ID");

                if (cboRecibos.Items.Count > 0) 
                {
                    cboRecibos.SelectedIndex = cboRecibos.Items.Count-1; 
	                this.txtRecDesde.Text = cboRecibos.SelectedText; 
	                //cboRecibos.RemoveItem cboRecibos.ListCount - 1
                }

                //Recibos
                if (sTAB.SelectedIndex == 0) {
	                // dtF_Rendicion.SetFocus
	                txtDescripcion.Focus();
                } else {
	                //Valores
	                if (sTAB.SelectedIndex == 1) {

		                dtBd_Fecha.Focus();
	                }
                }

				break;
			//m.id = 0
			//mskN_Despacho.SetFocus

			case tAccion.Modificar:

            //    CambiarA(tEstado.Nuevo);
            //    m.Accion = tAccion.Modificar;
            //    Habilita(m.Accion);

            //    break;
            ////cboClientes.SetFocus

			case tAccion.Guardar:
	            
                if (DatosValidos("grabar")) {

		            Cursor.Current = Cursors.WaitCursor;

                    string wOper;
		            if (m.Accion == tAccion.Nuevo) {
			            wOper = "add";
		            } else if (m.Accion == tAccion.Modificar) {
			            wOper = "upd";
		            }

		            //Iniciar Transaccion
                    if (Global01.TranActiva==null)
                    {
                        Global01.TranActiva = Global01.Conexion.BeginTransaction();
                    }

                    try
                    {
		                addupdRendicion(wOper, Global01.Conexion,  
                            dtF_Rendicion.Value, 
                            txtDescripcion.Text, 
                            float.Parse(txtLatEfectivo_Monto.Text), 
                            float.Parse(txtLatDiv_dolar.Text),
                            float.Parse(txtLatDiv_euro.Text), 
                            byte.Parse(txtLatCh_Cantidad.Text), 
                            float.Parse(txtLatCh_Monto.Text), 
                            byte.Parse(txtLatCert_Cantidad.Text),
		                    float.Parse(txtLatCert_Monto.Text), 
                            m.ID);

		                lblNroRendicion.Text = Global01.NroUsuario + "-" + m.ID.ToString().PadLeft(8,'0');

		                //-- Actualiza tblRecibos_Enc -> Nro Rendicion --
		                for (m.i = 0; m.i < lvRecibos.Items.Count; m.i++) {
			                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblRecibo_Enc SET NroRendicion=" + m.ID + " WHERE NroRecibo='" + lvRecibos.Items[m.i].Text + "'");
		                }

		                for (m.i = 0; m.i < lvValores.Items.Count; m.i++) {
			                
			                if (Int16.Parse(lvValores.Items[m.i].Text.ToString())==0) {
				                //- Agrega Registro a la Base de Datos -
				                wOper = "add";
				                //UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				                //UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				                addupdValores(wOper, Global01.Conexion, m.ID, 
                                    lvValores.Items[m.i].SubItems[1].Text, 
                                    DateTime.Parse(lvValores.Items[m.i].SubItems[2].Text), 
                                    Int16.Parse(lvValores.Items[m.i].SubItems[3].Text), 
                                    float.Parse(lvValores.Items[m.i].SubItems[4].Text), 
                                    byte.Parse(lvValores.Items[m.i].SubItems[5].Text), 
                                    lvValores.Items[m.i].SubItems[7].Text);
			                }
		                }

                        if (Global01.TranActiva != null)
                        {
                            Global01.TranActiva.Commit();
                        };

		                Cursor.Current = Cursors.Default;

				        if (!(m.miError)) 
                        {
			                if (wOper == "add") 
                            {
                                auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Rendicion,auditoria.Auditor.AccionesAuditadas.EXITOSO,"viajante:" + Global01.NroUsuario  + " rc:" + lblNroRendicion.Text + " tot:" + string.Format("{0:N2}",lblRecibosTotal.Text);
				                MessageBox.Show("Rendición Grabada Con Éxito ! -> N°=" + lblNroRendicion.Text, "Datos Grabados", MessageBoxButtons.OK, MessageBoxIcon.Information);
			                } 
                            else 
                            {
				                MessageBox.Show("La Rendición fue Actualizada con Éxito", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
			                }
					        m.Accion = tAccion.Guardar;
					        CambiarA(tEstado.Neutro);
					        Habilita(tAccion.Neutro);

		                    m.ID  = 0;
		                    Rendicion_Imprimir(Convert.ToString(Convert.ToInt32(VB.Right(lblNroRendicion.Text, 8))));

		                    m.Accion = tAccion.Cancelar;
		                    CambiarA(tEstado.Neutro);
		                    Habilita(m.Accion);
				        }
                    }
                    catch (Exception e)
                    {

                        if (Global01.TranActiva != null)
                        {
                            Global01.TranActiva.Rollback();
                        }
                        //throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                    }
                    finally
                    {
                        Global01.TranActiva = null;
                    };

		            Cursor.Current = Cursors.Default;
	            }			

  			    break;
			case tAccion.Cancelar:

				CambiarA(tEstado.Neutro);
				m.escape = true;
				m.Accion = tAccion.Cancelar;
				Habilita(m.Accion);

				break;
			case tAccion.Buscar:

				CambiarA(tEstado.Buscar);
				m.Accion = tAccion.Buscar;
				Habilita(m.Accion);
		        
                sTAB.SelectedIndex = 2;

				break;
			case tAccion.Imprimir:
			
                Rendicion_Imprimir(lblNroRendicion.Text.Substring(7,8));
				
				m.Accion = tAccion.Cancelar;
                CambiarA(tEstado.Neutro);
				Habilita(m.Accion);

				break;
			case tAccion.Eliminar:
                if (DatosValidos("eliminar")) {
	                m.Accion = tAccion.Eliminar;
	                CambiarA(tEstado.Eliminar);
	                Habilita(m.Accion);

	                if (m.ID > 0) {
                        if (MessageBox.Show("¿Desea eliminar la Rendición?", "Actualizar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)		                
                        {
			                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblRendicion WHERE NroRendicion=" + m.ID);
			                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblRendicionValores WHERE NroRendicion=" + m.ID);
			                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblRecibo_Enc SET NroRendicion=0 WHERE NroRendicion=" + m.ID);

			                LimpiarPantalla("all");

                            if (!(m.miError))
                            {
                                MessageBox.Show("La Rendición fue eliminada con Éxito", "Actualizar Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CambiarA(tEstado.Neutro);
				                Habilita(tAccion.Neutro);
			                }
		                }
	                }
                }
				break;
			case tAccion.Neutro:
                if (lvBuscar.Items.Count > 0) {

	                sTAB.SelectedIndex = 0;

	                m.ID = Convert.ToInt32(m.ItemX.Text);
	                m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblRendicion", "NroRendicion=" + m.ID.ToString());

                    if (!(m.miError))
                    {
		                if (m.DR.HasRows) {
			                m.Accion = tAccion.Neutro;
                            CambiarA(tEstado.Vista);
			                Habilita(m.Accion);
			                AsignarDatos();
		                }
	                } else {
                        MessageBox.Show("Errores al abrie la Rendición", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	                }
	                m.DR = null;
                }
				break;
			case tAccion.Listas:
				break;
			case tAccion.Cerrar:

				m.Accion = tAccion.Cerrar;
				CambiarA(tEstado.Neutro);
                //this.Close();
				this.Dispose();
				break;
		}

	}

	private void CambiarA(tEstado Estado)
	{
		switch (Estado) {

			case tEstado.Neutro:

				if (!(vAcceso(1, 1, "frmDespachos1"))) {
					tsbNuevo.Enabled = false;
					tsbModificar.Enabled = false;
					tsbGrabar.Enabled = false;
					tsbCancelar.Enabled = false;
					tsbBuscar.Enabled = true;
					tsbImprimir.Enabled = true;
					tsbBorrar.Enabled = false;
					tsbCerrar.Enabled = true;
				} else {
					tsbNuevo.Enabled = true;
					tsbModificar.Enabled = false;
					tsbGrabar.Enabled = false;
					tsbCancelar.Enabled = false;
					tsbBuscar.Enabled = true;
					tsbImprimir.Enabled = true;
					tsbBorrar.Enabled = true;
					tsbCerrar.Enabled = true;
				}

				break;
			case tEstado.Nuevo:

				tsbNuevo.Enabled = false;
				tsbModificar.Enabled = false;
				tsbGrabar.Enabled = true;
				tsbCancelar.Enabled = true;
				tsbBuscar.Enabled = false;
				tsbImprimir.Enabled = false;
				tsbBorrar.Enabled = false;
				tsbCerrar.Enabled = true;

				break;
			case tEstado.Modificar:

				tsbNuevo.Enabled = true;
				tsbModificar.Enabled = true;
				tsbGrabar.Enabled = false;
				tsbCancelar.Enabled = true;
				tsbBuscar.Enabled = true;
				tsbImprimir.Enabled = true;
				tsbBorrar.Enabled = true;
				tsbCerrar.Enabled = true;

				break;
			case tEstado.Buscar:

				tsbNuevo.Enabled = false;
				tsbModificar.Enabled = false;
				tsbGrabar.Enabled = false;
				tsbCancelar.Enabled = true;
				tsbBuscar.Enabled = false;
				tsbImprimir.Enabled = true;
				tsbBorrar.Enabled = false;
				tsbCerrar.Enabled = true;

				break;
			case tEstado.Imprimir:

				tsbNuevo.Enabled = false;
				tsbModificar.Enabled = false;
				tsbGrabar.Enabled = false;
				tsbCancelar.Enabled = true;
				tsbBuscar.Enabled = false;
				tsbImprimir.Enabled = true;
				tsbBorrar.Enabled = false;
				tsbCerrar.Enabled = true;

				break;
			case tEstado.Eliminar:

				tsbNuevo.Enabled = false;
				tsbModificar.Enabled = false;
				tsbGrabar.Enabled = false;
				tsbCancelar.Enabled = true;
				tsbBuscar.Enabled = false;
				tsbImprimir.Enabled = false;
				tsbBorrar.Enabled = false;
				tsbCerrar.Enabled = true;

				break;
			case tEstado.Vista:

				tsbNuevo.Enabled = true;
				tsbModificar.Enabled = true;
				tsbGrabar.Enabled = false;
				tsbCancelar.Enabled = false;
				tsbBuscar.Enabled = true;
				tsbImprimir.Enabled = true;
				tsbBorrar.Enabled = true;
				tsbCerrar.Enabled = true;

				break;
		}

	}

	private void Habilita(tAccion Accion)
	{
		switch (Accion) {
			case tAccion.Nuevo:
                fraClave.Enabled = true;
                fraDatos.Enabled = true;
                fraDatos1.Enabled = true;
                fraLVRecibos.Enabled = true;
                fraLVValores.Enabled = true;
                fraBuscar.Enabled = false;

                LimpiarPantalla("all");

				sTAB.SelectTab(0);

				break;
			case tAccion.Modificar:
                fraClave.Enabled = true;
                fraDatos.Enabled = true;
                fraDatos1.Enabled = true;
                fraLVRecibos.Enabled = true;
                fraLVValores.Enabled = true;
                fraBuscar.Enabled = false;

				sTAB.SelectTab(0);

				break;
			case tAccion.Buscar:
                fraClave.Enabled = false;
                fraDatos.Enabled = false;
                fraDatos1.Enabled = false;
                fraLVRecibos.Enabled = false;
                fraLVValores.Enabled = false;

                fraBuscar.Enabled = true;

				LimpiarPantalla("buscar");
				sTAB.SelectTab(2);

				break;
			case tAccion.Imprimir:
                //gbClave.Enabled = false;
                //gbDatos.Enabled = false;
                //gbRecurrentes.Enabled = false;
                //gbRecurrenteBuscar.Enabled = false;
                //gbIntervienen.Enabled = false;
                //gbIntervienenBuscar.Enabled = false;

                //gbBuscar.Enabled = true;
                //LimpiarPantalla("imprimir");

				break;
			case tAccion.Neutro:
                fraClave.Enabled = false;
                fraDatos.Enabled = false;
                fraDatos1.Enabled = false;
                fraLVRecibos.Enabled = false;
                fraLVValores.Enabled = false;
                fraBuscar.Enabled = false;

				break;
			case tAccion.Cancelar:
				LimpiarPantalla("all");

				break;
		}

	}

	private void LimpiarPantalla(string Modo)
	{
		if ((Modo == "clave" | Modo == "all")) 
        {
		    lblNroRendicion.Text = Global01.NroUsuario + "-00000000";
		    dtF_Rendicion.Value = DateTime.Today.Date;
		    txtDescripcion.Text = "";
		    m.ID = 0;
		    lblEfectivoC.Text = "0,00";
		    lblChequesTotalC.Text = "0,00";
		    lblChequesCantidadC.Text = "0,00";
		    lblDivEuroC.Text = "0,00";
		    lblDivDolarC.Text = "0,00";
		    lblCertificadosCantidadC.Text = "0,00";
		    lblCertificadosTotalC.Text = "0,00";
		};

		if ((Modo == "datos" | Modo == "all")) 
        {
		    this.txtRecDesde.Text = "0";
		    this.txtRecHasta.Text = "0";
		    cboRecibos.SelectedIndex = -1;
		    lblIdValor.Text = "0";
		}

		if ((Modo == "valores" | Modo == "all")) 
        {
		    dtBd_Fecha.Value = DateTime.Today.Date;

		    txtBd_Monto.Text = "0,00";

		    if (this.opTipoDeposito[0].Checked == true) 
            {
			    txtBdCh_Cantidad.Text = "0";
			    txtBd_Nro.Text = "0";
		    } 
            else {
			    txtBdCh_Cantidad.Text = "1";
		    }

		//Me.opTipoDeposito.Item(0).value = True
	    }

		if ((Modo == "buscar")) {
		    txtBuscar.Text = "";
		    mskFbuscar.Value = DateTime.Today.Date;
		    lvBuscar.Items.Clear();
            //gbBuscar.Text = " Buscar ... ";
			cmdBuscar.Text = "Buscar";

			optBuscarTramite.Checked = true;
			//mskFbuscar.Text = "__/__/____";
			txtBuscar.Text = "";
			//lvBuscar.ListItems.Clear;
		}

		if ((Modo == "imprimir")) {
            //gbBuscar.Text = " Imprimir ... ";
			cmdBuscar.Text = "Imprimir";

			optBuscarTramite.Checked = true;
			//mskFbuscar.Text = "__/__/____"
			txtBuscar.Text = "";
			//lvBuscar.ListItems.Clear
		}

		if ((Modo == "all")) {
	        lblEfectivoC.ForeColor = System.Drawing.Color.Red;
	        lblDivDolarC.ForeColor = System.Drawing.Color.Red;
	        lblDivEuroC.ForeColor = System.Drawing.Color.Red;
	        lblChequesTotalC.ForeColor = System.Drawing.Color.Red;
	        lblChequesCantidadC.ForeColor = System.Drawing.Color.Red;
	        lblCertificadosTotalC.ForeColor = System.Drawing.Color.Red;
	        lblCertificadosCantidadC.ForeColor = System.Drawing.Color.Red;

	        txtLatEfectivo_Monto.Text = "0,00";
	        txtLatCh_Cantidad.Text = "0";
	        txtLatCh_Monto.Text = "0,00";
	        txtLatCert_Cantidad.Text = "0";
	        txtLatCert_Monto.Text = "0,00";
	        txtLatDiv_dolar.Text = "0,00";
	        txtLatDiv_euro.Text = "0,00";

	        lvRecibos.Items.Clear();
	        lvValores.Items.Clear();
	        TotalRecibos();
	        TotalValores();
	        TotalControles();
        }

	}

private void txtBuscar_KeyPress(System.Object eventSender, System.Windows.Forms.KeyPressEventArgs eventArgs)
{
	short KeyAscii = Strings.Asc(eventArgs.KeyChar);

	if (optBuscar(0).Checked == true) {
		SoloDigitos(KeyAscii);
	} else {
		KeyAscii = Strings.Asc(Strings.UCase(Strings.Chr(KeyAscii)));
	}

	eventArgs.KeyChar = Strings.Chr(KeyAscii);
	if (KeyAscii == 0) {
		eventArgs.Handled = true;
	}
}

private bool ValidarBuscar()
{
	bool functionReturnValue = false;

	 // ERROR: Not supported in C#: OnErrorStatement


	functionReturnValue = true;

	if (optBuscar(0).Checked) {
		if (Strings.Len(txtBuscar.Text) == 0) {
			Interaction.MsgBox("Debe ingresarse el valor a buscar!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Datos Válidos");
			functionReturnValue = false;
			txtBuscar.Focus();
		}
	} else if (optBuscar(1).Checked) {
		//            If Len(mskFbuscar.Text) = 0 Or Trim(mskFbuscar.Text) = "__/__/____" Then
		//                MsgBox "Debe ingresarse el valor a buscar!", vbOKOnly + vbInformation, "Datos Válidos"
		//                ValidarBuscar = False
		//                mskFbuscar.SetFocus
		//            End If
	}
	return functionReturnValue;

}

private void addupdRendicion(string pOper, ref ADODB.Connection Conexion, System.DateTime pF_Rendicion, string pDescripcion, float pEfectivo_Monto, float pDolar_Cantidad, float pEuros_Cantidad, byte pCheques_Cantidad, float pCheques_Monto, byte pCertificados_Cantidad,
float pCertificados_Monto, ref int pID)
{

	 // ERROR: Not supported in C#: OnErrorStatement


	ADODB.Command adoCMD = new ADODB.Command();
	ADODB.Parameter adoPRM = new ADODB.Parameter();


	if (pOper == "add") {
		adoPRM = adoCMD.CreateParameter("pF_Rendicion", ADODB.DataTypeEnum.adDate, ADODB.ParameterDirectionEnum.adParamInput, , pF_Rendicion);
		adoCMD.Parameters.Append(adoPRM);

		adoPRM = adoCMD.CreateParameter("pDescripcion", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 128, Strings.Trim(pDescripcion));
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pEfectivo_Monto", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pEfectivo_Monto);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pDolar_Cantidad", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pDolar_Cantidad);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pEuros_Cantidad", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pEuros_Cantidad);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pCheques_Monto", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pCheques_Monto);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pCheques_Cantidad", ADODB.DataTypeEnum.adTinyInt, ADODB.ParameterDirectionEnum.adParamInput, , pCheques_Cantidad);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pCertificados_Monto", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pCertificados_Monto);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pCertificados_Cantidad", ADODB.DataTypeEnum.adTinyInt, ADODB.ParameterDirectionEnum.adParamInput, , pCertificados_Cantidad);
		adoCMD.Parameters.Append(adoPRM);

		adoCMD.let_ActiveConnection(Conexion);
		adoCMD.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc;
		adoCMD.CommandText = "usp_Rendicion_add";
		adoCMD.Execute();

		m.adoREC = xGetRSMDB(vg.Conexion, "tblRendicion", "@@identity", "NONE");
		pID = m.adoREC.Fields("ID").Value;
		//UPGRADE_NOTE: Object m.adoREC may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		m.adoREC = null;

		adoCMD.Parameters.Delete(("pF_Rendicion"));
		adoCMD.Parameters.Delete(("pDescripcion"));
		adoCMD.Parameters.Delete(("pEfectivo_Monto"));
		adoCMD.Parameters.Delete(("pDolar_Cantidad"));
		adoCMD.Parameters.Delete(("pEuros_Cantidad"));
		adoCMD.Parameters.Delete(("pCheques_Monto"));
		adoCMD.Parameters.Delete(("pCheques_Cantidad"));
		adoCMD.Parameters.Delete(("pCertificados_Monto"));
		adoCMD.Parameters.Delete(("pCertificados_Cantidad"));

	} else if (pOper == "upd") {

		adoPRM = adoCMD.CreateParameter("pF_Rendicion", ADODB.DataTypeEnum.adDate, ADODB.ParameterDirectionEnum.adParamInput, , pF_Rendicion);
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pDescripcion", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 128, Strings.Trim(pDescripcion));
		adoCMD.Parameters.Append(adoPRM);
		adoPRM = adoCMD.CreateParameter("pID", ADODB.DataTypeEnum.adInteger, ADODB.ParameterDirectionEnum.adParamInputOutput, , pID);
		adoCMD.Parameters.Append(adoPRM);

		adoCMD.let_ActiveConnection(Conexion);
		adoCMD.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc;
		adoCMD.CommandText = "usp_Rendicion_upd";
		adoCMD.Execute();

		adoCMD.Parameters.Delete(("pF_Rendicion"));
		adoCMD.Parameters.Delete(("pDescripcion"));
		adoCMD.Parameters.Delete(("pID"));
	}

	//UPGRADE_NOTE: Object adoPRM may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoPRM = null;
	//UPGRADE_NOTE: Object adoCMD may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoCMD = null;
	return;
	ErrorHandler:

	//UPGRADE_NOTE: Object adoPRM may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoPRM = null;
	//UPGRADE_NOTE: Object adoCMD may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoCMD = null;
	Err().Raise(Err().Number, Err().Source, Err().Description);

}

private void addupdValores(string pOper, ref ADODB.Connection Conexion, int pNroRendicion, string pBco_Dep_Tipo, System.DateTime pBco_Dep_Fecha, int pBco_Dep_Numero, float pBco_Dep_Monto, byte pBco_Dep_Ch_Cantidad, string pDetalle)
{

	 // ERROR: Not supported in C#: OnErrorStatement


	ADODB.Command adoCMD = new ADODB.Command();
	ADODB.Parameter adoPRM = new ADODB.Parameter();

	adoPRM = adoCMD.CreateParameter("pNroRendicion", ADODB.DataTypeEnum.adInteger, ADODB.ParameterDirectionEnum.adParamInput, , pNroRendicion);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pBco_Dep_Tipo", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 1, pBco_Dep_Tipo);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pBco_Dep_Fecha", ADODB.DataTypeEnum.adDate, ADODB.ParameterDirectionEnum.adParamInput, , pBco_Dep_Fecha);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pBco_Dep_Numero", ADODB.DataTypeEnum.adInteger, ADODB.ParameterDirectionEnum.adParamInput, , pBco_Dep_Numero);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pBco_Dep_Monto", ADODB.DataTypeEnum.adSingle, ADODB.ParameterDirectionEnum.adParamInput, , pBco_Dep_Monto);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pBco_Dep_Ch_Cantidad", ADODB.DataTypeEnum.adTinyInt, ADODB.ParameterDirectionEnum.adParamInput, , pBco_Dep_Ch_Cantidad);
	adoCMD.Parameters.Append(adoPRM);
	adoPRM = adoCMD.CreateParameter("pDetalle", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 128, pDetalle);
	adoCMD.Parameters.Append(adoPRM);


	if (pOper == "add") {
		adoCMD.let_ActiveConnection(Conexion);
		adoCMD.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc;
		adoCMD.CommandText = "usp_RendicionValores_add";
		adoCMD.Execute();
		//    ElseIf pOper = "upd" Then
		//        Set adoPRM = adoCMD.CreateParameter("pID", adInteger, adParamInput, , pID)
		//        adoCMD.Parameters.Append adoPRM
		//
		//        adoCMD.ActiveConnection = Conexion
		//        adoCMD.CommandType = adCmdStoredProc
		//        adoCMD.CommandText = "usp_RendicionValores_upd"
		//        adoCMD.Execute
		//        adoCMD.Parameters.Delete ("pID")
	}

	adoCMD.Parameters.Delete(("pBco_Dep_Tipo"));
	adoCMD.Parameters.Delete(("pBco_Dep_Fecha"));
	adoCMD.Parameters.Delete(("pBco_Dep_Numero"));
	adoCMD.Parameters.Delete(("pBco_Dep_Monto"));
	adoCMD.Parameters.Delete(("pBco_Dep_Ch_Cantidad"));
	adoCMD.Parameters.Delete(("pDetalle"));

	//UPGRADE_NOTE: Object adoPRM may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoPRM = null;
	//UPGRADE_NOTE: Object adoCMD may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoCMD = null;
	return;
	ErrorHandler:

	//UPGRADE_NOTE: Object adoPRM may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoPRM = null;
	//UPGRADE_NOTE: Object adoCMD may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
	adoCMD = null;
	Err().Raise(Err().Number, Err().Source, Err().Description);

}

private void SoloMontos(ref System.Windows.Forms.TextBox miTextbox, ref short KeyAscii)
{

	 // ERROR: Not supported in C#: OnErrorStatement


	if (KeyAscii == 44 | KeyAscii == 46) {
		if (Strings.InStr(1, miTextbox.Text, ",", CompareMethod.Text) > 0 | Strings.InStr(1, miTextbox.Text, ".", CompareMethod.Text) > 0) {
			KeyAscii = 0;
		} else {
			SoloDigitos(KeyAscii);
		}
	} else {
		SoloDigitos(KeyAscii);
	}

}

	private void CerrarToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
	{
		Accion_Click(tAccion.Cerrar);
	}


	private void AccionMenu(string menuItemText)
	{
		switch (menuItemText) {
			case "&Nuevo":
				// Simulate creating a new document by merely clearing the existing text.
				MessageBox.Show("nuevo");
				break; // TODO: might not be correct. Was : Exit Select

				break;
			case "&Open":

				break; // TODO: might not be correct. Was : Exit Select

				break;
			case "&Save":
				break; // TODO: might not be correct. Was : Exit Select

				break;
			case "C&errar":
				this.Close();
				this.Dispose();
				break;
		}

	}

	private void MenuStrip1_ItemClicked(System.Object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
	{
		AccionMenu(((ToolStripItem)e.ClickedItem).Text);
	}

	
	private void cmdBuscar_Click(System.Object sender, System.EventArgs e)
	{
		bool tOk = false;

		tOk = true;


		//Select Case True
		//    Case optBuscar(0) Or optBuscar(1) Or optBuscar(4) Or optBuscar(5) Or _
		//         optBuscar(8) Or optBuscar(9) Or optBuscar(10)

		//        If Len(Trim(txtBuscar.Text)) = 0 Then
		//            MsgBox("Ingrese valor a buscar", vbExclamation, "Atención")
		//            tOk = False
		//        End If

		//    Case optBuscar(2) Or optBuscar(6) ' Por Domicilio

		//        If cboBuscar.ListIndex <= 0 Then
		//            MsgBox("Ingrese valor a buscar", vbExclamation, "Atención")
		//            tOk = False
		//        End If

		//    Case optBuscar(3) Or optBuscar(7) ' por Fecha

		//        If mskFbuscar.Text = "__/__/____" Then
		//            MsgBox("Ingrese Fecha", vbExclamation, "Atención")
		//            tOk = False
		//        End If

		//End Select

		//Screen.MousePointer = vbHourglass
		if (tOk) {
			//If optBuscar(0).Value Then  'buscar por Nº Reclamo
			//    m.adoREC = xGetRS(vm.Conexion, "vReclamos1", "ID=" & UCase(Trim(txtBuscar.Text)), "NONE")
			//ElseIf optBuscar(1).Value Then  'buscar por Descripción
			//    m.adoREC = xGetRS(vm.Conexion, "vReclamos1", "upper(ltrim(rtrim(Descripcion))) LIKE '%" & UCase(Trim(txtBuscar.Text)) & "%'", "NONE")
			//ElseIf optBuscar(2).Value Then  'buscar por Domicilio de Reclamo
			//    m.adoREC = xGetRS(vm.Conexion, "vReclamos1", "D_Calle1= " & CStr(cboBuscar.ItemData(cboBuscar.ListIndex)) & " OR D_Calle2=" & CStr(cboBuscar.ItemData(cboBuscar.ListIndex)), "D_Calle1, D_Altura")
			//ElseIf optBuscar(3).Value Then  'buscar por Fecha de Reclamo
			//    m.adoREC = xGetRS(vm.Conexion, "vReclamos1", "F_Origen >= '" & mskFbuscar.Text & "' and F_Origen <= '" & CStr(CDate(mskFbuscar.Text) + 180) & "'", "F_Origen")
			//ElseIf optBuscar(4).Value Then  'buscar por Nº Documento
			//    m.adoREC = xGetRS(vm.Conexion, "vRecurrente1", "upper(ltrim(rtrim(N_Documento))) LIKE '%" & UCase(Trim(txtBuscar.Text)) & "%'", "NONE")
			//ElseIf optBuscar(5).Value Then  'buscar por Apellido y Nombre
			//    m.adoREC = xGetRS(vm.Conexion, "vRecurrente1", "upper(ltrim(rtrim(ApellidoNombre))) LIKE '%" & UCase(Trim(txtBuscar.Text)) & "%'", "NONE")
			//ElseIf optBuscar(6).Value Then  'buscar por Domicilio del Recurrente
			//    m.adoREC = xGetRS(vm.Conexion, "vRecurrente1", "D_Calle1= " & CStr(cboBuscar.ItemData(cboBuscar.ListIndex)), "D_Calle1, D_Altura")
			//ElseIf optBuscar(7).Value Then  'buscar por Fecha de Reclamo Recurrente
			//    m.adoREC = xGetRS(vm.Conexion, "vRecRec1", "F_Reclamo >= '" & mskFbuscar.Text & "' and F_Reclamo <= '" & CStr(CDate(mskFbuscar.Text) + 180) & "'", "F_Reclamo")
			//ElseIf optBuscar(8).Value Then  'buscar por Email
			//    m.adoREC = xGetRS(vm.Conexion, "vRecurrente1", "upper(ltrim(rtrim(email))) LIKE '%" & UCase(Trim(txtBuscar.Text)) & "%'", "NONE")
			//ElseIf optBuscar(9).Value Then  'buscar por Email
			//    m.adoREC = xGetRS_usp5(vm.Conexion, "usp_Reclamos_x_Oficina2", UCase(Trim(txtBuscar.Text)), " ", "E", " ", "N")
			//ElseIf optBuscar(10).Value Then  'buscar por Email
			//    If mskFbuscar.Text = "__/__/____" Then
			//        m.adoREC = xGetRS(vm.Conexion, "vPases1", "C_Receptora='" & UCase(Trim(txtBuscar.Text)) & "'", "NONE")
			//    Else
			//        m.adoREC = xGetRS(vm.Conexion, "vPases1", "C_Receptora='" & UCase(Trim(txtBuscar.Text)) & "' and F_Movimiento >= '" & mskFbuscar.Text & "'", "NONE")
			//    End If

			//End If


			string wCondicion = "F_Origen >= '" + "01/09/2009" + "' and F_Origen <= '" + "30/09/2009" + "'";

			m.DS = Funciones.adoModulo.xGetDS(Funciones.adoModulo.GetConn(My.Settings.cnn0800), "vReclamos1", wCondicion, "NONE");

			if (m.DS.Tables[0].Rows.Count > 0) {

				dgBuscar.DataSource = m.DS;
				dgBuscar.DataMember = m.DS.Tables[0].TableName;


			} else {
				MessageBox.Show("No se encontraron coincidencias con el valor buscado", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbInformation, "Busqueda");
			}

			m.DS = null;
		}


		return;

	}


	private void mskFBuscar_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
	{
		this.ToolTip1.SetToolTip(mskFbuscar, "Ingrese Fecha");

		if ((!e.IsValidInput)) {
			this.ToolTip1.ToolTipTitle = "Fecha NO Válida";
			this.ToolTip1.Show("Debe ingresar una fecha válida en el siguiente formato dd/mm/aaaa.", this.mskFbuscar, 5000);
		} else {
			// Now that the type has passed basic type validation, enforce more specific type rules.
			DateTime UserDate = Convert.ToDateTime(e.ReturnValue);
			if ((UserDate > DateTime.Now)) {
				this.ToolTip1.ToolTipTitle = "Fecha NO Válida";
				this.ToolTip1.Show("Debe ingresar una fecha NO mayor al día de hoy", this.mskFbuscar, 5000);
				e.Cancel = true;
			}
		}

	}


	private void cboCalle_KeyUp1(object sender, System.Windows.Forms.KeyEventArgs e)
	{
		Funciones.Util.AutoCompleteCombo_KeyUp(cboCalle, e);

		//Funciones.Util.AutoComplete(ComboBox4, e)

		//Static cadena As String
		//Dim i As Long

		//With ComboBox4

		//    ' si pesionamos las teclas de las flechas sale de la rutina   
		//    If e.KeyCode = Keys.Up Then Exit Sub
		//    If e.KeyCode = Keys.Down Then Exit Sub
		//    If e.KeyCode = Keys.Left Then Exit Sub
		//    If e.KeyCode = Keys.Right Then Exit Sub

		//    ' verifica qu no se presionó la tecla backspace   
		//    If e.KeyCode <> Keys.Back Then
		//        cadena = Mid(.Text, 1, Len(.Text) - .SelectionLength)
		//    Else
		//        '...tecla backspace   
		//        If cadena <> "" Then
		//            cadena = Mid(cadena, 1, Len(cadena) - 1)
		//        End If
		//    End If

		//    For i = 0 To .Items.Count - 1
		//        If UCase(cadena) = UCase(Mid(.Items(i).ToString, 1, Len(cadena))) Then
		//            .SelectedIndex = i
		//            Exit For
		//        End If
		//    Next

		//    ' Seelecciona   
		//    .SelectionStart = Len(cadena)
		//    .SelectionLength = Len(.Text)

		//    If .SelectedIndex = -1 Then
		//        ' color de fondo del combo en caso de que no hay coincidencias   
		//        .BackColor = Color.Red 'COLOR_NO_ENCONTRADO
		//    Else
		//        ' Backcolor normal cuando hay coincidencia   
		//        .BackColor = Color.White  'COLOR_NORMAL
		//    End If

		//End With

	}


	private void cmdRecurrenteBuscar_Click(System.Object sender, System.EventArgs e)
	{
		if (this.txtRecurrenteBuscar.Text.Trim.Length == 0) {
			MessageBox.Show("Ingrese un valor a buscar", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbInformation, "Búsqueda");
			this.txtRecurrenteBuscar.Focus();

		} else {
			string wCondicion = "upper(ltrim(rtrim(ApellidoNombre)) + ' ' + ltrim(rtrim(Telefono)) + ' ' + ltrim(rtrim(email))) LIKE  '%" + this.txtRecurrenteBuscar.Text + "%'";

			m.DR = Funciones.adoModulo.xGetDR(Funciones.adoModulo.GetConn(My.Settings.cnn0800), "vRecurrente1", wCondicion, "NONE");

			if (m.DR.HasRows) {
				Funciones.Util.CargarLV(lvRecurrentesBuscar, m.DR);
			} else {
				MessageBox.Show("No se encontraron coincidencias con el valor buscado", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbInformation, "Búsqueda");
			}

			m.DR = null;
		}

	}


	private void cmdIntervieneBuscar_Click(System.Object sender, System.EventArgs e)
	{
		if (this.txtIntervieneBuscar.Text.Trim.Length == 0) {
			MessageBox.Show("Ingrese un valor a buscar", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbInformation, "Búsqueda");
			this.txtIntervieneBuscar.Focus();

		} else {
			string wCondicion = "upper(ltrim(rtrim(Codigo)) + ' ' + ltrim(rtrim(Descrip))) LIKE  '%" + this.txtIntervieneBuscar.Text + "%'";

			m.DR = Funciones.adoModulo.xGetDR(Funciones.adoModulo.GetConn(My.Settings.cnn0800), "vCboOficinas", wCondicion, "NONE");

			if (m.DR.HasRows) {
				Funciones.Util.CargarLV(lvIntervienenBuscar, m.DR);
			} else {
				MessageBox.Show("No se encontraron coincidencias con el valor buscado", (MsgBoxStyle)Constants.vbOKOnly + Constants.vbInformation, "Búsqueda");
			}

			m.DR = null;
		}

	}


	private void cmdRecurrenteAdd_Click(System.Object sender, System.EventArgs e)
	{
		bool wExiste = false;


		if ((lvRecurrentesBuscar.SelectedItems.Count > 0)) {
			if (lvRecurrentes.Items.Count > 0) {
				foreach (ListViewItem itm in lvRecurrentes.Items) {
					if (itm.SubItems(2).Text == lvRecurrentesBuscar.SelectedItems(0).SubItems(8).Text) {
						wExiste = true;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}

			if (!wExiste) {
				m.itmListItem = new ListViewItem();
				m.itmListItem.Text = lvRecurrentesBuscar.SelectedItems(0).SubItems(1).Text + " - " + lvRecurrentesBuscar.SelectedItems(0).SubItems(2).Text + " " + lvRecurrentesBuscar.SelectedItems(0).SubItems(3).Text + " - " + lvRecurrentesBuscar.SelectedItems(0).SubItems(5).Text + " - " + lvRecurrentesBuscar.SelectedItems(0).SubItems(7).Text;

				m.itmListItem.SubItems.Add(Convert.ToString((this.chkReservarIdentidad.Checked ? "si" : "no")));
				//Reserva Identidad
				m.itmListItem.SubItems.Add(lvRecurrentesBuscar.SelectedItems(0).SubItems(8).Text);
				//ID

				if (lvRecurrentes.Items.Count % 2 == 0) {
					m.itmListItem.BackColor = Color.LightYellow;
				} else {
					m.itmListItem.BackColor = Color.White;
				}

				lvRecurrentes.Items.Add(m.itmListItem);
			}

		}

	}


	private void lvIntervienenBuscar_SelectedIndexChanged(System.Object sender, System.EventArgs e)
	{
		if (lvIntervienenBuscar.SelectedIndices.Count > 0) {
			//m.itmListItem = lvIntervienenBuscar.SelectedItems(0)
			this.lblPaseOficina.Text = lvIntervienenBuscar.SelectedItems(0).Text.Trim + " - " + lvIntervienenBuscar.SelectedItems(0).SubItems(1).Text.Trim;
		}

		//Me.lblPaseOficina.Text = m.itmListItem.Text.Trim & " - " & m.itmListItem.SubItems(1).Text.Trim

	}


	private void cmdPaseAgregar_Click(System.Object sender, System.EventArgs e)
	{
		bool wExiste = false;
		string sMensaje = "";

		if (this.txtPaseMotivo.Text.Length > 0) {
			if (this.cboPaseTipo.SelectedIndex > 0) {

				if ((lvIntervienenBuscar.SelectedItems.Count > 0)) {
					if (lvIntervienen.Items.Count > 0) {
						foreach (ListViewItem itm in lvIntervienen.Items) {
							if (itm.SubItems(4).Text == lvIntervienenBuscar.SelectedItems(0).Text) {
								wExiste = true;
								break; // TODO: might not be correct. Was : Exit For
							}
						}
					}

					if (!wExiste) {
						m.itmListItem = new ListViewItem();
						m.itmListItem.Text = Strings.Format(System.DateTime.Today, "dd-MM-yyyy");

						m.itmListItem.SubItems.Add(lvIntervienenBuscar.SelectedItems(0).Text + " - " + lvIntervienenBuscar.SelectedItems(0).SubItems(1).Text);
						//Oficina
						m.itmListItem.SubItems.Add(this.txtPaseMotivo.Text);
						//Motivo
						//m.itmListItem.SubItems.Add(Me.cboPaseTipo.SelectedText) 'Tipo Pase
						m.itmListItem.SubItems.Add(((ListItemComboBox)this.cboPaseTipo.SelectedItem).Descrip());
						m.itmListItem.SubItems.Add(lvIntervienenBuscar.SelectedItems(0).Text);
						//IdOficina
						//m.itmListItem.SubItems.Add(Me.cboPaseTipo.SelectedValue.ToString)   'IdTipoPase

						m.itmListItem.SubItems.Add(Convert.ToString(((ListItemComboBox)this.cboPaseTipo.SelectedItem).ID()));

						m.itmListItem.SubItems.Add("0");
						//IdPase

						if (lvIntervienen.Items.Count % 2 == 0) {
							m.itmListItem.BackColor = Color.LightYellow;
						} else {
							m.itmListItem.BackColor = Color.White;
						}

						lvIntervienen.Items.Add(m.itmListItem);

					} else {
						sMensaje += "* Oficina YA ingresada*";
					}
				} else {
					sMensaje += "* Seleccione Oficina *";
				}
			} else {
				sMensaje += "* Ingrese tipo de pase *";
			}
		} else {
			sMensaje += "* Ingrese Motivo *";
		}

		if (sMensaje.Trim().Length > 0) {
			MessageBox.Show(sMensaje);
		}

	}

    private void AsignarDatos()
    {
        const string PROCNAME_ = "AsignarDatos";
  
        lblNroRendicion.Text = m.DR["NroRendicion"].ToString();
        txtDescripcion.Text = "" + m.DR["Descripcion"].ToString();
        dtF_Rendicion.value = m.DR["F_Rendicion"].ToString();

        txtLatEfectivo_Monto.Text = m.DR["Efectivo_Monto"].ToString();
        txtLatCh_Cantidad.Text = m.DR["Cheques_Cantidad"].ToString();
        txtLatCh_Monto.Text = m.DR["Cheques_Monto"].ToString();
        txtLatCert_Cantidad.Text = m.DR["Certificados_Cantidad"].ToString();
        txtLatCert_Monto.Text = m.DR["Certificados_Monto"].ToString();
        txtLatDiv_dolar.Text = m.DR["Dolar_Cantidad"].ToString();
        txtLatDiv_euro.Text = m.DR["Euros_Cantidad"].ToString();

        lvRecibos.Items.Clear();
        m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "v_Recibo_Enc_noR_lv", "NroRendicion=" + lblNroRendicion.Text, "NroRecibo");
        if (m.DR.HasRows)
        {
            Funciones.util.CargarLV(lvRecibos,m.DR);
            TotalRecibos();
            TotalControles();
        }

        lvValores.Items.Clear();
        m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "v_RendicionValores1", "NroRendicion=" + lblNroRendicion.Text, "ID");
        if (m.DR.HasRows)
        {
            Funciones.util.CargarLV(lvValores,m.DR);
            CargarLV(lvValores, m.itemX, m.adoREC);
            TotalValores();
            TotalControles();
        }

       m.DR = null;

    }

    private void cmdReciboAdd_Click(object sender, EventArgs e)
    {
        //-------- ErrorGuardian Begin -----------------------

        {
            const string PROCNAME_ = "cmdReciboAdd_Click";
            // ERROR: Not supported in C#: OnErrorStatement

            //-------- ErrorGuardian End -------------------------

            //If cboRecibos.ListIndex < 0 Then
            if (Convert.ToInt32(this.txtRecDesde.Text) > Convert.ToInt32(this.txtRecHasta.Text))
            {
                //cboObraSoc.SetFocus
            }
            else
            {

                //Set m.adoREC = xGetRSMDB(vg.Conexion, "v_Recibo_Enc_noR_lv", "NroRecibo LIKE '%" & padLR(cboRecibos.ItemData(cboRecibos.ListIndex), 8, "0", 1) & "'", "NONE")

                m.adoREC = xGetRSMDB(vg.Conexion, "v_Recibo_Enc_noR_lv", "NroRecibo >='" + vg.NroUsuario + "-" + padLR(txtRecDesde.Text, 8, "0", 1) + "' and NroRecibo<='" + vg.NroUsuario + "-" + padLR(txtRecHasta.Text, 8, "0", 1) + "'", "NONE");
                if (!m.adoREC.EOF())
                {
                    lvRecibos.Items.Clear();
                    CargarLV(lvRecibos, m.itemX, m.adoREC);

                    cboCheques.Items.Clear();
                    m.adoREC = xGetRSMDB(vg.Conexion, "v_Recibo_Det_Cheques_Detalle", "NroRecibo >='" + vg.NroUsuario + "-" + padLR(txtRecDesde.Text, 8, "0", 1) + "' and NroRecibo<='" + vg.NroUsuario + "-" + padLR(txtRecHasta.Text, 8, "0", 1) + "'", "NONE");
                    CargarCombo2(cboCheques, "Descrip", "NewIndex", m.adoREC, false);

                    TotalRecibos();
                    TotalControles();
                }


                //LimpiarPantalla "datos"

            }

            //-------- ErrorGuardian Begin --------
            // ERROR: Not supported in C#: OnErrorStatement

            return;
        ErrorGuardianLocalHandler:

            //Tomo una accion particular
            if (Err().Number == 99999999)
            {
                // ERROR: Not supported in C#: ResumeStatement

                //Tomo una accion general
            }
            else
            {
                //UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
                {
                    case MsgBoxResult.Retry:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Ignore:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Abort:
                        break;
                    //Err.Raise Err.Number
                }
            }
        }
        //-------- ErrorGuardian End ----------

    }

    private void cmdValorAdd_Click(object sender, EventArgs e)
    {
        //-------- ErrorGuardian Begin -----------------------

        {
            const string PROCNAME_ = "cmdValorAdd_Click";
            // ERROR: Not supported in C#: OnErrorStatement

            //-------- ErrorGuardian End -------------------------

            if (lvRecibos.Items.Count < 1)
            {
                MessageBox.Show("Debe ingresar recibos a rendir", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (!DatosValidos("valores"))
                {
                    //cboObraSoc.SetFocus
                }
                else
                {
                    m.itemX = lvValores.Items.Add(lblIdValor.Text);
                    //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                    if (m.itemX.SubItems.Count > 1)
                    {
                        m.itemX.SubItems(1).Text = (opTipoDeposito(0).Checked == true ? "E" : "C");
                    }
                    else
                    {
                        m.itemX.SubItems.Insert(1, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, (opTipoDeposito(0).Checked == true ? "E" : "C")));
                    }
                    //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                    if (m.itemX.SubItems.Count > 2)
                    {
                        m.itemX.SubItems(2).Text = dtBd_Fecha.value;
                    }
                    else
                    {
                        m.itemX.SubItems.Insert(2, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, dtBd_Fecha.value));
                    }
                    //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                    if (m.itemX.SubItems.Count > 3)
                    {
                        m.itemX.SubItems(3).Text = txtBd_Nro.Text;
                    }
                    else
                    {
                        m.itemX.SubItems.Insert(3, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, txtBd_Nro.Text));
                    }
                    //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                    if (m.itemX.SubItems.Count > 4)
                    {
                        m.itemX.SubItems(4).Text = txtBd_Monto.Text;
                    }
                    else
                    {
                        m.itemX.SubItems.Insert(4, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, txtBd_Monto.Text));
                    }
                    //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                    if (m.itemX.SubItems.Count > 5)
                    {
                        m.itemX.SubItems(5).Text = txtBdCh_Cantidad.Text;
                    }
                    else
                    {
                        m.itemX.SubItems.Insert(5, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, txtBdCh_Cantidad.Text));
                    }
                    //m.itemX.SubItems(6) = 0 ' NroRendicion

                    if (opTipoDeposito(1).Checked == true)
                    {
                        //UPGRADE_WARNING: Lower bound of collection m.itemX has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                        if (m.itemX.SubItems.Count > 7)
                        {
                            m.itemX.SubItems(7).Text = VB6.GetItemString(cboCheques, cboCheques.SelectedIndex);
                        }
                        else
                        {
                            m.itemX.SubItems.Insert(7, new System.Windows.Forms.ListViewItem.ListViewSubItem(null, VB6.GetItemString(cboCheques, cboCheques.SelectedIndex)));
                        }
                        cboCheques.Items.RemoveAt(cboCheques.SelectedIndex);
                    }

                    TotalValores();
                    TotalControles();

                    LimpiarPantalla("valores");
                }
            }
            //-------- ErrorGuardian Begin --------
            // ERROR: Not supported in C#: OnErrorStatement

            return;
        ErrorGuardianLocalHandler:

            //Tomo una accion particular
            if (Err().Number == 99999999)
            {
                // ERROR: Not supported in C#: ResumeStatement

                //Tomo una accion general
            }
            else
            {
                //UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
                {
                    case MsgBoxResult.Retry:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Ignore:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Abort:
                        break;
                    //Err.Raise Err.Number
                }
            }
        }
        //-------- ErrorGuardian End ----------

    }

    private void cmdBuscar_Click_1(object sender, EventArgs e)
    {
        //-------- ErrorGuardian Begin -----------------------

        {
            const string PROCNAME_ = "cmdBuscar_Click";
            // ERROR: Not supported in C#: OnErrorStatement

            //-------- ErrorGuardian End -------------------------

            if (ValidarBuscar)
            {

                lvBuscar.Items.Clear();

                if (m.Accion == tAccion.Imprimir)
                {

                }
                else
                {

                    switch (true)
                    {
                        case optBuscar(0).Checked:
                            // por nº de remito
                            m.adoREC = xGetRSMDB(vg.Conexion, "tblRendicion", "NroRendicion= " + Strings.UCase(Strings.Trim(txtBuscar.Text)), "NONE");
                            break;
                        case optBuscar(1).Checked:
                            // por fecha
                            m.adoREC = xGetRSMDB(vg.Conexion, "tblRendicion", "F_Rendicion >= #" + VB6.Format(mskFbuscar.value, "yyyy/mm/dd") + "# and F_Rendicion <= #" + VB6.Format(mskFbuscar.value + 60, "yyyy/mm/dd") + "#", "F_Rendicion");
                            break;
                    }

                    if (!m.adoREC.EOF)
                    {
                        CargarLV(lvBuscar, m.itemX, m.adoREC);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron coincidencias con el valor buscado", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Busqueda");
                        //txtBuscar.SetFocus
                    }

                    m.adoREC.Close();
                    //UPGRADE_NOTE: Object m.adoREC may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    m.adoREC = null;

                }
            }

            //-------- ErrorGuardian Begin --------
            // ERROR: Not supported in C#: OnErrorStatement

            return;
        ErrorGuardianLocalHandler:

            //Tomo una accion particular
            if (Err().Number == 99999999)
            {
                // ERROR: Not supported in C#: ResumeStatement

                //Tomo una accion general
            }
            else
            {
                //UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
                {
                    case MsgBoxResult.Retry:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Ignore:
                        // ERROR: Not supported in C#: ResumeStatement

                        break;
                    case MsgBoxResult.Abort:
                        break;
                    //Err.Raise Err.Number
                }
            }
        }
        //-------- ErrorGuardian End ----------

    }

        private void ucRendiciones_Load(object sender, EventArgs e)
        {
		    Funciones.Util.SizeLastColumn(lvRecibos);
            Funciones.Util.SizeLastColumn(lvValores);
		
		    sTAB.SelectedIndex = 0;

		    LimpiarPantalla("all");

		    CambiarA(tEstado.Neutro);        
        }	

private void TotalRecibos()
{

	//-------- ErrorGuardian Begin --------
	const string PROCNAME_ = "TotalRecibo";
	 // ERROR: Not supported in C#: OnErrorStatement

	//-------- ErrorGuardian End ----------

	lblEfectivo.Text = VB6.Format(0, "##,###,##0.00");
	lblDivDolarCantidad.Text = VB6.Format(0, "##,###,##0.00");
	lblDivEuroCantidad.Text = VB6.Format(0, "##,###,##0.00");
	lblChequesTotal.Text = VB6.Format(0, "##,###,##0.00");
	lblChequesCantidad.Text = "0";
	lblCertificadosTotal.Text = VB6.Format(0, "##,###,##0.00");
	lblCertificadosCantidad.Text = "0";
	lblRecibosTotal.Text = VB6.Format(0, "##,###,##0.00");

	if (lvRecibos.Items.Count < 1) {
		return;
	}

	for (m.I = 1; m.I <= lvRecibos.Items.Count; m.I++) {
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblEfectivo.Text = Convert.ToString(Convert.ToSingle("0" + lblEfectivo.Text) + Convert.ToSingle(lvRecibos.Items.Item(m.I).SubItems(4).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblDivDolarCantidad.Text = Convert.ToString(Convert.ToSingle("0" + lblDivDolarCantidad.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(5).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblDivEuroCantidad.Text = Convert.ToString(Convert.ToSingle("0" + lblDivEuroCantidad.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(6).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblChequesTotal.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesTotal.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(7).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblChequesCantidad.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesCantidad.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(8).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblCertificadosTotal.Text = Convert.ToString(Convert.ToSingle("0" + lblCertificadosTotal.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(9).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblCertificadosCantidad.Text = Convert.ToString(Convert.ToSingle("0" + lblCertificadosCantidad.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(10).Text));
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvRecibos.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblRecibosTotal.Text = Convert.ToString(Convert.ToSingle("0" + lblRecibosTotal.Text) + Convert.ToSingle("0" + lvRecibos.Items.Item(m.I).SubItems(11).Text));

	}

	lblEfectivo.Text = VB6.Format(lblEfectivo.Text, "##,###,##0.00");
	lblDivDolarCantidad.Text = VB6.Format(lblDivDolarCantidad.Text, "##,###,##0.00");
	lblDivEuroCantidad.Text = VB6.Format(lblDivEuroCantidad.Text, "##,###,##0.00");
	lblChequesTotal.Text = VB6.Format(lblChequesTotal.Text, "##,###,##0.00");
	lblChequesCantidad.Text = VB6.Format(lblChequesCantidad.Text, "00");
	lblCertificadosTotal.Text = VB6.Format(lblCertificadosTotal.Text, "##,###,##0.00");
	lblCertificadosCantidad.Text = VB6.Format(lblCertificadosCantidad.Text, "00");

	lblRecibosTotal.Text = VB6.Format(lblRecibosTotal.Text, "##,###,##0.00");

	//-------- ErrorGuardian Begin --------
	return;
	ErrorGuardianLocalHandler:

	switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)) {
		case MsgBoxResult.Retry:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
		case MsgBoxResult.Ignore:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
	}
	//-------- ErrorGuardian End ----------

}

private void TotalValores()
{

	//-------- ErrorGuardian Begin --------
	const string PROCNAME_ = "TotalValores";
	 // ERROR: Not supported in C#: OnErrorStatement

	//-------- ErrorGuardian End ----------


	lblEfectivoV.Text = VB6.Format(0, "##,###,##0.00");
	lblChequesTotalV.Text = VB6.Format(0, "##,###,##0.00");
	lblChequesCantidadV.Text = "0";
	lblTotalV.Text = VB6.Format(0, "##,###,##0.00");

	//       If lvValores.ListItems.Count < 1 Then
	//            Exit Sub
	//       End If
	//
	for (m.I = 1; m.I <= lvValores.Items.Count; m.I++) {
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		if (lvValores.Items.Item(m.I).SubItems(1).Text == "E") {
			//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			lblEfectivoV.Text = Convert.ToString(Convert.ToSingle("0" + lblEfectivoV.Text) + Convert.ToSingle("0" + lvValores.Items.Item(m.I).SubItems(4).Text));
		} else {
			//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			lblChequesTotalV.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesTotalV.Text) + Convert.ToSingle("0" + lvValores.Items.Item(m.I).SubItems(4).Text));
		}
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblChequesCantidadV.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesCantidadV.Text) + Convert.ToSingle("0" + lvValores.Items.Item(m.I).SubItems(5).Text));
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		//UPGRADE_WARNING: Lower bound of collection lvValores.ListItems() has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		lblTotalV.Text = Convert.ToString(Convert.ToSingle("0" + lblTotalV.Text) + Convert.ToSingle("0" + lvValores.Items.Item(m.I).SubItems(4).Text));
	}

	lblEfectivoV.Text = VB6.Format(lblEfectivoV.Text, "##,###,##0.00");
	lblChequesTotalV.Text = VB6.Format(lblChequesTotalV.Text, "##,###,##0.00");
	lblChequesCantidadV.Text = VB6.Format(lblChequesCantidadV.Text, "00");
	lblTotalV.Text = VB6.Format(lblTotalV.Text, "##,###,##0.00");

	//-- OJO ACA EMPIEZAN LOS CONTROLES -----

	//TotalControles
	//-- acá terminan los CONTROLES ---------

	//-------- ErrorGuardian Begin --------
	return;
	ErrorGuardianLocalHandler:

	switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)) {
		case MsgBoxResult.Retry:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
		case MsgBoxResult.Ignore:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
	}
	//-------- ErrorGuardian End ----------

}

private void TotalControles()
{

	//-------- ErrorGuardian Begin --------
	const string PROCNAME_ = "TotalControles";
	 // ERROR: Not supported in C#: OnErrorStatement

	//-------- ErrorGuardian End ----------

	lblEfectivoC.ForeColor = System.Drawing.Color.Black;
	lblDivDolarC.ForeColor = System.Drawing.Color.Black;
	lblDivEuroC.ForeColor = System.Drawing.Color.Black;
	lblChequesTotalC.ForeColor = System.Drawing.Color.Black;
	lblChequesCantidadC.ForeColor = System.Drawing.Color.Black;
	lblCertificadosTotalC.ForeColor = System.Drawing.Color.Black;
	lblCertificadosCantidadC.ForeColor = System.Drawing.Color.Black;


	lblEfectivoC.Text = Convert.ToString(Convert.ToSingle("0" + lblEfectivo.Text) - Convert.ToSingle("0" + lblEfectivoV.Text) - Convert.ToSingle(txtLatEfectivo_Monto.Text));
	lblChequesTotalC.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesTotal.Text) - Convert.ToSingle("0" + lblChequesTotalV.Text) - Convert.ToSingle(txtLatCh_Monto.Text));
	lblChequesCantidadC.Text = Convert.ToString(Convert.ToSingle("0" + lblChequesCantidad.Text) - Convert.ToSingle("0" + lblChequesCantidadV.Text) - Convert.ToSingle(txtLatCh_Cantidad.Text));
	lblCertificadosCantidadC.Text = Convert.ToString(Convert.ToSingle("0" + lblCertificadosCantidad.Text) - Convert.ToSingle(txtLatCert_Cantidad.Text));
	lblCertificadosTotalC.Text = Convert.ToString(Convert.ToSingle("0" + lblCertificadosTotal.Text) - Convert.ToSingle(txtLatCert_Monto.Text));

	lblDivDolarC.Text = Convert.ToString(Convert.ToSingle("0" + lblDivDolarCantidad.Text) - Convert.ToSingle(txtLatDiv_dolar.Text));
	lblDivEuroC.Text = Convert.ToString(Convert.ToSingle("0" + lblDivEuroCantidad.Text) - Convert.ToSingle(txtLatDiv_euro.Text));

	lblEfectivoC.Text = VB6.Format(lblEfectivoC.Text, "##,###,##0.00");
	lblChequesTotalC.Text = VB6.Format(lblChequesTotalC.Text, "##,###,##0.00");
	lblChequesCantidadC.Text = VB6.Format(lblChequesCantidadC.Text, "00");
	lblCertificadosTotalC.Text = VB6.Format(lblCertificadosTotalC.Text, "##,###,##0.00");
	lblCertificadosCantidadC.Text = VB6.Format(lblCertificadosCantidadC.Text, "00");

	lblDivDolarC.Text = VB6.Format(lblDivDolarC.Text, "##,###,##0.00");
	lblDivEuroC.Text = VB6.Format(lblDivEuroC.Text, "##,###,##0.00");

	if (Convert.ToDouble(lblEfectivoC.Text) != 0)
		lblEfectivoC.ForeColor = System.Drawing.Color.Red;
	if (System.Math.Abs(Convert.ToDouble(lblChequesTotalC.Text)) > 0.05)
		lblChequesTotalC.ForeColor = System.Drawing.Color.Red;
	if (Convert.ToDouble(lblChequesCantidadC.Text) != 0)
		lblChequesCantidadC.ForeColor = System.Drawing.Color.Red;
	if (Convert.ToDouble(lblCertificadosTotalC.Text) != 0)
		lblCertificadosTotalC.ForeColor = System.Drawing.Color.Red;
	if (Convert.ToDouble(lblCertificadosCantidadC.Text) != 0)
		lblCertificadosCantidadC.ForeColor = System.Drawing.Color.Red;

	if (Convert.ToDouble(lblDivDolarC.Text) != 0)
		lblDivDolarC.ForeColor = System.Drawing.Color.Red;
	if (Convert.ToDouble(lblDivEuroC.Text) != 0)
		lblDivEuroC.ForeColor = System.Drawing.Color.Red;

	//-------- ErrorGuardian Begin --------
	return;
	ErrorGuardianLocalHandler:

	switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)) {
		case MsgBoxResult.Retry:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
		case MsgBoxResult.Ignore:
			 // ERROR: Not supported in C#: ResumeStatement

			break;
	}
	//-------- ErrorGuardian End ----------

}
//----------------------------------------------------------
    } //fin clase
} //fin namespace