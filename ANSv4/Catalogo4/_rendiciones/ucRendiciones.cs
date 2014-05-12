using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; 
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;

namespace Catalogo._rendiciones
{
    public partial class ucRendiciones : UserControl

    {
        //private //const string m_sMODULENAME_ = "ucRendiciones";
        private System.Data.OleDb.OleDbTransaction _TranActiva = null;

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
            //public bool miError;
            public int ID;
            public bool escape;
            public System.Data.OleDb.OleDbDataReader DR;
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

            _ToolTip.SetToolTip(btnIniciar, "INICIAR Rendición");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime la Rendición ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

        }

        private bool DatosValidos(string pCampo)
        {

            bool sResultado = true;

            if (!(m.escape))
            {
                if (pCampo.ToLower() == "grabar")
                {
                    if (lblEfectivoC.ForeColor == Color.Red | lblDivDolarC.ForeColor == Color.Red | lblDivEuroC.ForeColor == Color.Red | lblChequesTotalC.ForeColor == Color.Red | lblChequesCantidadC.ForeColor == Color.Red | lblCertificadosTotalC.ForeColor == Color.Red | lblCertificadosCantidadC.ForeColor == Color.Red | lvRecibos.Items.Count < 1)
                    {	//Or lvValores.ListItems.Count < 1 Then
                        sResultado = false;
                        MessageBox.Show("Error! Debe consolidar la rendición actual", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (pCampo.ToLower() == "valores")
                {
                    if (txtBd_Nro.Text.ToString().Trim().Length > 0)
                    {
                        if (float.Parse("0" + txtBd_Nro.Text) <= 0)
                        {
                            sResultado = false;
                            MessageBox.Show("Debe ingresar Nro. de Boleta de Depósito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        sResultado = false;
                        MessageBox.Show("Debe ingresar Nro. de Boleta de Depósito", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (float.Parse("0" + txtBd_Monto.Text) <= 0)
                    {
                        sResultado = false;
                        MessageBox.Show("Debe ingresar monto del comprobante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (_opTipoDeposito_1.Checked == true)
                    {
                        if (cboCheques.SelectedIndex < 0)
                        {
                            sResultado = false;
                            MessageBox.Show("Debe elegir el cheque a rendir", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        if (byte.Parse("0" + txtBdCh_Cantidad.Text) <= 0)
                        {
                            MessageBox.Show("Debe ingresar cantidad de cheques", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            sResultado = false;
                        }
                    }
                }
            }

            return sResultado;
        }
	    
	    private void Accion_Click(tAccion Accion)
	    {		
            switch (Accion) {
			    case tAccion.Nuevo:
                    m.ID = 0;

                    //if (cboRecibos.Items.Count > 0) cboRecibos.Items.Clear();
                    Funciones.util.CargaCombo(Global01.Conexion, ref cboRecibos, "v_Recibo_Enc_noR_cbo", "Descrip", "ID");

                    if (cboRecibos.Items.Count > 0) 
                    {
                        cboRecibos.SelectedIndex = cboRecibos.Items.Count-1; 
	                    this.txtRecDesde.Text = cboRecibos.SelectedValue.ToString(); 
	                    //cboRecibos.RemoveItem cboRecibos.ListCount - 1
                    };
                    
                    if (sTAB.SelectedIndex == 0)
                    {//Recibos
	                    // dtF_Rendicion.SetFocus
	                    txtDescripcion.Focus();
                    } 
                    else if (sTAB.SelectedIndex == 1)
                    {//Valores
		                dtBd_Fecha.Focus();
                    };
				    
                    m.Accion = tAccion.Nuevo;
				    CambiarA(tEstado.Nuevo);
				    Habilita(m.Accion);

				    break;
			    case tAccion.Modificar:

                //    CambiarA(tEstado.Nuevo);
                //    m.Accion = tAccion.Modificar;
                //    Habilita(m.Accion);

                //    break;
                ////cboClientes.SetFocus

			    case tAccion.Guardar:
	            
                    if (DatosValidos("grabar")) {

		                Cursor.Current = Cursors.WaitCursor;

                        string wOper = "add";
                        if (m.Accion == tAccion.Modificar) wOper = "upd";

		                //Iniciar Transaccion
                        if (_TranActiva==null)
                        {
                            //@ _TranActiva =Global01.Conexion.BeginTransaction();
                            util.errorHandling.ErrorLogger.LogMessage("5");
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
                                ref m.ID);

		                    lblNroRendicion.Text = Global01.NroUsuario + "-" + m.ID.ToString().PadLeft(8,'0');

		                    //-- Actualiza tblRecibos_Enc -> Nro Rendicion --
		                    for (m.i = 0; m.i < lvRecibos.Items.Count; m.i++) {
			                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblRecibo_Enc SET NroRendicion=" + m.ID + " WHERE NroRecibo='" + lvRecibos.Items[m.i].Text + "'");
		                    }

		                    for (m.i = 0; m.i < lvValores.Items.Count; m.i++) {
			                
			                    if (Int16.Parse(lvValores.Items[m.i].Text.ToString())==0) {
				                    //- Agrega Registro a la Base de Datos -
				                    wOper = "add";
				                    addupdValores(wOper, Global01.Conexion, m.ID, 
                                        lvValores.Items[m.i].SubItems[1].Text, 
                                        DateTime.Parse(lvValores.Items[m.i].SubItems[2].Text), 
                                        Int16.Parse(lvValores.Items[m.i].SubItems[3].Text), 
                                        float.Parse(lvValores.Items[m.i].SubItems[4].Text), 
                                        byte.Parse(lvValores.Items[m.i].SubItems[5].Text), 
                                        lvValores.Items[m.i].SubItems[7].Text);
			                    }
		                    }

                            if (_TranActiva!= null)
                            {
                                _TranActiva.Commit();
                            }

		                    Cursor.Current = Cursors.Default;

                            //if (!(m.miError)) 
                            //{
			                    if (wOper == "add") 
                                {
                                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Rendicion,_auditor.Auditor.AccionesAuditadas.EXITOSO,"viajante:" + Global01.NroUsuario  + " rc:" + lblNroRendicion.Text + " tot:" + string.Format("{0:N2}",float.Parse(lblRecibosTotal.Text)));
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
                                Rendicion_Imprimir(lblNroRendicion.Text.Substring(lblNroRendicion.Text.Length - 8));

		                        m.Accion = tAccion.Cancelar;
		                        CambiarA(tEstado.Neutro);
		                        Habilita(m.Accion);
                            //}
                        }
                        catch (Exception ex)
                        {
                            if (_TranActiva!= null)
                            {
                                _TranActiva.Rollback();
                            }
                            util.errorHandling.ErrorLogger.LogMessage(ex);

                            throw ex; 
                        }
                        finally
                        {
                            _TranActiva = null;
                        }

		                Cursor.Current = Cursors.Default;
	                }			

  			        break;
			    case tAccion.Cancelar:

                    m.escape = true;
                    CambiarA(tEstado.Neutro);
				    m.Accion = tAccion.Cancelar;
				    Habilita(m.Accion);

				    break;
			    case tAccion.Buscar:

				    CambiarA(tEstado.Buscar);
				    m.Accion = tAccion.Buscar;
				    Habilita(m.Accion);

				    break;
			    case tAccion.Imprimir:

                    if (lblNroRendicion.Text.Trim().Length > 0)
                    {
                        m.Accion = tAccion.Cancelar;
                        CambiarA(tEstado.Neutro);
				        Habilita(m.Accion);                    
                        Rendicion_Imprimir(lblNroRendicion.Text.Substring(lblNroRendicion.Text.Length - 8));
                    };

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

                                //if (!(m.miError))
                                //{
                                    MessageBox.Show("La Rendición fue eliminada con Éxito", "Actualizar Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CambiarA(tEstado.Neutro);
				                    Habilita(tAccion.Neutro);
                                //}
		                    }
	                    }
                    }
				    break;
			    case tAccion.Neutro:
                    if (lvBuscar.Items.Count > 0) {

	                    sTAB.SelectedIndex = 0;
	                    m.ID = Int16.Parse(m.ItemX.Text);
                        
	                    m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblRendicion", "NroRendicion=" + m.ID.ToString());

                        //if (!(m.miError))
                        //{
		                    if (m.DR.HasRows) {
			                    m.Accion = tAccion.Neutro;
                                CambiarA(tEstado.Vista);
			                    Habilita(m.Accion);
                                m.DR.Read();
			                    AsignarDatos();
                                btnVer.Enabled = true;
                            }
                        //} else {
                        //    MessageBox.Show("Errores al abrir la Rendición", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}
	                    m.DR = null;
                        m.ItemX = null;
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

					btnIniciar.Enabled = true;
					btnImprimir.Enabled = false;
					btnBuscar.Enabled = true;
                    btnVer.Enabled = false;
					btnEliminar.Enabled = false;
                    sTAB.Visible = false;

				    break;
			    case tEstado.Nuevo:

                    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = true;
				    btnBuscar.Enabled = false;
				    btnVer.Enabled = false;
				    btnEliminar.Enabled = false;

				    break;
			    case tEstado.Modificar:

				    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = false;
				    btnBuscar.Enabled = true;
				    btnVer.Enabled = true;
				    btnEliminar.Enabled = true;

				    break;
			    case tEstado.Buscar:

				    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = false;
				    btnBuscar.Enabled = false;
                    btnVer.Enabled = false;
				    btnEliminar.Enabled = false;

				    break;
			    case tEstado.Imprimir:

                    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = false;
				    btnBuscar.Enabled = false;
                    btnVer.Enabled = false;
				    btnEliminar.Enabled = false;

				    break;
			    case tEstado.Eliminar:

                    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = false;
				    btnBuscar.Enabled = false;
				    btnVer.Enabled = false;
				    btnEliminar.Enabled = false;

				    break;
			    case tEstado.Vista:

				    btnIniciar.Enabled = true;
				    btnImprimir.Enabled = false;
				    btnBuscar.Enabled = true;
                    btnVer.Enabled = false;
				    btnEliminar.Enabled = true;

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
                    sTAB.Visible = true;

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
                    sTAB.Visible = true;

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
                    lvBuscar.Visible = false;

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
		        lblChequesCantidadC.Text = "0";
		        lblDivEuroC.Text = "0,00";
		        lblDivDolarC.Text = "0,00";
		        lblCertificadosCantidadC.Text = "0";
		        lblCertificadosTotalC.Text = "0,00";
		    }

		    if ((Modo == "datos" | Modo == "all")) 
            {
                //this.txtRecDesde.Text = "0";
		        this.txtRecHasta.Text = "0";
		        cboRecibos.SelectedIndex = -1;
		        lblIdValor.Text = "0";
		    }

		    if ((Modo == "valores" | Modo == "all")) 
            {
		        dtBd_Fecha.Value = DateTime.Today.Date;

		        txtBd_Monto.Text = "0,00";

		        if (_opTipoDeposito_0.Checked == true) 
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

			    _optBuscar_0.Checked = true;
			    //mskFbuscar.Text = "__/__/____";
			    txtBuscar.Text = "";
			    //lvBuscar.ListItems.Clear;
		    }

		    if ((Modo == "imprimir")) {
                //gbBuscar.Text = " Imprimir ... ";
			    cmdBuscar.Text = "Imprimir";

			    _optBuscar_0.Checked = true;
			    //mskFbuscar.Text = "__/__/____"
			    txtBuscar.Text = "";
			    //lvBuscar.ListItems.Clear
		    }

		    if ((Modo == "all")) {
	            lblEfectivoC.ForeColor = Color.Red;
	            lblDivDolarC.ForeColor = Color.Red;
	            lblDivEuroC.ForeColor = Color.Red;
	            lblChequesTotalC.ForeColor = Color.Red;
	            lblChequesCantidadC.ForeColor = Color.Red;
	            lblCertificadosTotalC.ForeColor = Color.Red;
	            lblCertificadosCantidadC.ForeColor = Color.Red;

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

        private bool ValidarBuscar()
        {
	        bool functionReturnValue = false;

	        functionReturnValue = true;

	        if (_optBuscar_0.Checked) {
		        if (txtBuscar.Text.ToString().Trim().Length == 0) 
                {
			        MessageBox.Show("Debe ingresarse el valor a buscar!", "Datos Válidos", MessageBoxButtons.OK, MessageBoxIcon.Information );
			        functionReturnValue = false;
			        txtBuscar.Focus();
		        }
	        } 
            else if (_optBuscar_1.Checked) 
            {
		        //            If Len(mskFbuscar.Text) = 0 Or Trim(mskFbuscar.Text) = "__/__/____" Then
		        //                MsgBox "Debe ingresarse el valor a buscar!", vbOKOnly + vbInformation, "Datos Válidos"
		        //                ValidarBuscar = False
		        //                mskFbuscar.SetFocus
		        //            End If
	        }
	        return functionReturnValue;

        }

        private void addupdRendicion(string pOper, System.Data.OleDb.OleDbConnection Conexion, DateTime pF_Rendicion, string pDescripcion, float pEfectivo_Monto, float pDolar_Cantidad, float pEuros_Cantidad, byte pCheques_Cantidad, float pCheques_Monto, byte pCertificados_Cantidad, float pCertificados_Monto, ref int pID)
        {

            System.Data.OleDb.OleDbCommand adoCMD = new System.Data.OleDb.OleDbCommand();

            if (pOper == "add") 
            {
                adoCMD.Parameters.Add("pF_Rendicion", System.Data.OleDb.OleDbType.Date).Value = pF_Rendicion;
                adoCMD.Parameters.Add("pDescripcion", System.Data.OleDb.OleDbType.VarChar, 128).Value = pDescripcion.Trim();
                adoCMD.Parameters.Add("pEfectivo_Monto",System.Data.OleDb.OleDbType.Single).Value =  pEfectivo_Monto;
                adoCMD.Parameters.Add("pDolar_Cantidad", System.Data.OleDb.OleDbType.Single).Value =  pDolar_Cantidad;
                adoCMD.Parameters.Add("pEuros_Cantidad", System.Data.OleDb.OleDbType.Single).Value =  pEuros_Cantidad;
                adoCMD.Parameters.Add("pCheques_Monto", System.Data.OleDb.OleDbType.Single).Value =  pCheques_Monto;
                adoCMD.Parameters.Add("pCheques_Cantidad", System.Data.OleDb.OleDbType.TinyInt).Value =  pCheques_Cantidad;
                adoCMD.Parameters.Add("pCertificados_Monto", System.Data.OleDb.OleDbType.Single).Value =  pCertificados_Monto;
                adoCMD.Parameters.Add("pCertificados_Cantidad", System.Data.OleDb.OleDbType.TinyInt).Value =  pCertificados_Cantidad;

                adoCMD.Connection = Global01.Conexion;
                adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
                adoCMD.CommandText = "usp_Rendicion_add";

                if (_TranActiva!= null)
                {
                    adoCMD.Transaction = _TranActiva;
                }
                try
                {
                    adoCMD.ExecuteNonQuery();

                    System.Data.OleDb.OleDbDataReader rec = null;
                    rec = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblRendicion", "@@identity");
                    rec.Read();
                    pID = Int16.Parse(rec["ID"].ToString());
                    rec = null;

                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                }              

	        } 
            else if (pOper == "upd") 
            {
		        adoCMD.Parameters.Add("pF_Rendicion", System.Data.OleDb.OleDbType.Date).Value =  pF_Rendicion;
		        adoCMD.Parameters.Add("pDescripcion", System.Data.OleDb.OleDbType.VarChar, 128).Value = pDescripcion.Trim();
		        adoCMD.Parameters.Add("pID", System.Data.OleDb.OleDbType.Integer).Value =  pID;

                adoCMD.Connection = Global01.Conexion;
                adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
                adoCMD.CommandText = "usp_Rendicion_upd";

                if (_TranActiva!= null)
                {
                    adoCMD.Transaction = _TranActiva;
                }
                try
                {
                    adoCMD.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;  //new Exception(ex.Message.ToString());
                }                   

	        }

	        adoCMD = null;

        }

        private void addupdValores(string pOper, System.Data.OleDb.OleDbConnection Conexion, int pNroRendicion, string pBco_Dep_Tipo, System.DateTime pBco_Dep_Fecha, int pBco_Dep_Numero, float pBco_Dep_Monto, byte pBco_Dep_Ch_Cantidad, string pDetalle)
        {
            System.Data.OleDb.OleDbCommand adoCMD = new System.Data.OleDb.OleDbCommand();

            if (pOper == "add") 
            {		
	            adoCMD.Parameters.Add("pNroRendicion", System.Data.OleDb.OleDbType.Integer).Value =  pNroRendicion;
	            adoCMD.Parameters.Add("pBco_Dep_Tipo", System.Data.OleDb.OleDbType.VarChar, 1).Value = pBco_Dep_Tipo;
	            adoCMD.Parameters.Add("pBco_Dep_Fecha",  System.Data.OleDb.OleDbType.Date).Value =  pBco_Dep_Fecha;
	            adoCMD.Parameters.Add("pBco_Dep_Numero", System.Data.OleDb.OleDbType.Integer).Value =  pBco_Dep_Numero;
	            adoCMD.Parameters.Add("pBco_Dep_Monto", System.Data.OleDb.OleDbType.Single).Value =  pBco_Dep_Monto;
	            adoCMD.Parameters.Add("pBco_Dep_Ch_Cantidad", System.Data.OleDb.OleDbType.TinyInt).Value =  pBco_Dep_Ch_Cantidad;
	            adoCMD.Parameters.Add("pDetalle", System.Data.OleDb.OleDbType.VarChar, 128).Value = pDetalle;

                adoCMD.Connection = Global01.Conexion;
                adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
                adoCMD.CommandText = "usp_RendicionValores_add";
                
                if (_TranActiva!= null)
                {
                    adoCMD.Transaction = _TranActiva;
                }
                try
                {
                    adoCMD.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;  //new Exception(ex.Message.ToString());
                }   
            }
            adoCMD = null;
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
			    case "&Open":
				    break; // TODO: might not be correct. Was : Exit Select
			    case "&Save":
				    break; // TODO: might not be correct. Was : Exit Select
			    case "C&errar":
                    //this.Close();
				    this.Dispose();
				    break;
		    }

	    }

	    private void MenuStrip1_ItemClicked(System.Object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
	    {
		    AccionMenu(((ToolStripItem)e.ClickedItem).Text);
	    }

	
        private void AsignarDatos()
        {
            //const string PROCNAME_ = "AsignarDatos";
  
            lblNroRendicion.Text = m.DR["NroRendicion"].ToString();
            txtDescripcion.Text = "" + m.DR["Descripcion"].ToString();
            dtF_Rendicion.Value = DateTime.Parse(m.DR["F_Rendicion"].ToString());

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
                Funciones.util.CargarLV(ref lvRecibos,m.DR);
                TotalRecibos();
                TotalControles();
            }

            lvValores.Items.Clear();
            m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "v_RendicionValores1", "NroRendicion=" + lblNroRendicion.Text, "ID");
            if (m.DR.HasRows)
            {
                Funciones.util.CargarLV(ref lvValores,m.DR);
                TotalValores();
                TotalControles();
            }

           m.DR = null;

        }

        private void cmdReciboAdd_Click(object sender, EventArgs e)
        {
            //const string PROCNAME_ = "cmdReciboAdd_Click";
            //If cboRecibos.ListIndex < 0 Then
            if (Convert.ToInt32(this.txtRecDesde.Text) > Convert.ToInt32(this.txtRecHasta.Text))
            {
                //cboObraSoc.SetFocus
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                CargarRecibosLV();
                               
                cboCheques.Items.Clear();
                Funciones.util.CargaCombo(Global01.Conexion, ref cboCheques,"v_Recibo_Det_Cheques_Detalle","Descrip", "NewIndex","NroRecibo >='" + Global01.NroUsuario + "-" + txtRecDesde.Text.PadLeft(8, '0') + "' and NroRecibo<='" + Global01.NroUsuario + "-" + txtRecHasta.Text.PadLeft(8, '0') + "'","NONE",false,false,"Descrip");

                TotalRecibos();
                TotalControles();

                //LimpiarPantalla "datos"
            }
        }

        private void CargarRecibosLV()
        {
            //Set m.adoREC = xGetRSMDB(vg.Conexion, "v_Recibo_Enc_noR_lv", "NroRecibo LIKE '%" & padLR(cboRecibos.ItemData(cboRecibos.ListIndex), 8, "0", 1) & "'", "NONE")
            DataTable dt = Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "v_Recibo_Enc_noR_lv", "NroRecibo >='" + Global01.NroUsuario + "-" + txtRecDesde.Text.PadLeft(8, '0') + "' and NroRecibo<='" + Global01.NroUsuario + "-" + txtRecHasta.Text.PadLeft(8, '0') + "'");

            lvRecibos.Visible = false;
            lvRecibos.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem ItemX = new ListViewItem(dr["NroRecibo"].ToString());

                //alternate row color
                if (i % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else
                {
                    ItemX.BackColor = System.Drawing.SystemColors.Control;  //System.Drawing.Color.FromArgb(255, 255, 192);
                }


                ItemX.SubItems.Add(string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["F_Recibo"].ToString())));
                ItemX.SubItems.Add(dr["Nro_Cuenta"].ToString());
                ItemX.SubItems.Add(dr["Cliente"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["Efectivo"].ToString())));
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["Divisas_Dolares"].ToString())));
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["Divisas_Euros"].ToString())));
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["Cheques_Total"].ToString())));       
                ItemX.SubItems.Add(dr["Cheques_Cantidad"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["Certificados_Total"].ToString())));
                ItemX.SubItems.Add(dr["Certificados_Cantidad"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(dr["TotalRecibo"].ToString())));
                ItemX.SubItems.Add(dr["NroRendicion"].ToString());     

                lvRecibos.Items.Add(ItemX);
            }

            if (dt.Rows.Count > 0) Funciones.util.AutoSizeLVColumnas(ref lvRecibos);

            lvRecibos.Visible = true;
            dt = null;

        }

        private void cmdValorAdd_Click(object sender, EventArgs e)
        {
            //const string PROCNAME_ = "cmdValorAdd_Click";
    
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
                    m.ItemX = new ListViewItem(lblIdValor.Text);
   
                    //alternate row color
                    if (lvValores.Items.Count % 2 == 0)
                    {
                        m.ItemX.BackColor = Color.White;
                    }
                    else
                    {
                        m.ItemX.BackColor = System.Drawing.SystemColors.Control; //System.Drawing.Color.FromArgb(255, 255, 192);
                    }

                    m.ItemX.Tag = "";
                    m.ItemX.SubItems.Add(((_opTipoDeposito_0.Checked == true) ? "E" : "C"));
                    m.ItemX.SubItems.Add(dtBd_Fecha.Value.ToString());
                    m.ItemX.SubItems.Add(txtBd_Nro.Text);
                    m.ItemX.SubItems.Add(txtBd_Monto.Text);
                    m.ItemX.SubItems.Add(txtBdCh_Cantidad.Text);
                    m.ItemX.SubItems.Add("0"); //NroRendicion

                    if (_opTipoDeposito_1.Checked == true)
                    {
                        m.ItemX.SubItems.Add(cboCheques.SelectedText);
                        // PABLO // cboCheques.Items.RemoveAt(cboCheques.SelectedIndex);
                    }
                    else
                    {
                        m.ItemX.SubItems.Add("");
                    }

                    lvValores.Items.Add(m.ItemX);
                    Funciones.util.AutoSizeLVColumnas(ref lvValores);
      
                    TotalValores();
                    TotalControles();

                    LimpiarPantalla("valores");
                }
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            //const string PROCNAME_ = "cmdBuscar_Click";

            if (ValidarBuscar())
            {
                lvBuscar.Items.Clear();

                if (m.Accion == tAccion.Imprimir)
                {

                }
                else
                {
                    if (_optBuscar_0.Checked)
                    {
                        // por nº de remito
                        m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblRendicion", "NroRendicion= " + txtBuscar.Text.Trim());
                    }
                    else if (_optBuscar_1.Checked)
                    {
                        // por fecha
                        m.DR = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblRendicion", "F_Rendicion >= #" + mskFbuscar.Value.ToString("yyyy/MM/dd") + "# and F_Rendicion <= #" + mskFbuscar.Value.AddDays(60).ToString("yyyy/MM/dd") + "#", "F_Rendicion");
                    }

                    if (m.DR.HasRows)
                    {
                        Funciones.util.CargarLV(ref lvBuscar, m.DR);
                        lvBuscar.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron coincidencias con el valor buscado","Busqueda",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        //txtBuscar.SetFocus
                    }

                    m.DR = null;
                }
            }
        }

        private void ucRendiciones_Load(object sender, EventArgs e)
        {
            //Funciones.util.SizeLastColumn(lvRecibos);
            //Funciones.util.SizeLastColumn(lvValores);
  
            cboRecibos.SelectedIndex = -1;    
            txtBuscar.Text = "";
            mskFbuscar.Value = DateTime.Today.Date;
    
		    LimpiarPantalla("all");

            m.Accion = tAccion.Neutro;
            CambiarA(tEstado.Neutro);        
            Habilita(m.Accion);
		    
        }	

        private void TotalRecibos()
        {
	        //const string PROCNAME_ = "TotalRecibo";
	 
	        lblEfectivo.Text = string.Format("{0:N2}",0);
	        lblDivDolarCantidad.Text = string.Format("{0:N2}",0);
	        lblDivEuroCantidad.Text = string.Format("{0:N2}",0);
	        lblChequesTotal.Text = string.Format("{0:N2}",0);
	        lblChequesCantidad.Text = "0";
	        lblCertificadosTotal.Text = string.Format("{0:N2}",0);
	        lblCertificadosCantidad.Text = "0";
	        lblRecibosTotal.Text = string.Format("{0:N2}",0);

	        if (lvRecibos.Items.Count < 1) {
		        return;
	        }

	        for (m.i = 0; m.i < lvRecibos.Items.Count; m.i++) 
            {
		        lblEfectivo.Text = string.Format("{0:N2}",float.Parse("0" + lblEfectivo.Text) + float.Parse(lvRecibos.Items[m.i].SubItems[4].Text));
		        lblDivDolarCantidad.Text = string.Format("{0:N2}",float.Parse("0" + lblDivDolarCantidad.Text) + float.Parse("0" + lvRecibos.Items[m.i].SubItems[5].Text));
		        lblDivEuroCantidad.Text = string.Format("{0:N2}",float.Parse("0" + lblDivEuroCantidad.Text) + float.Parse("0" + lvRecibos.Items[m.i].SubItems[6].Text));
		        lblChequesTotal.Text = string.Format("{0:N2}",float.Parse("0" + lblChequesTotal.Text) + float.Parse("0" + lvRecibos.Items[m.i].SubItems[7].Text));
		        lblChequesCantidad.Text = string.Format("{0:N0}",Int16.Parse("0" + lblChequesCantidad.Text) + Int16.Parse("0" + lvRecibos.Items[m.i].SubItems[8].Text));
		        lblCertificadosTotal.Text = string.Format("{0:N2}",float.Parse("0" + lblCertificadosTotal.Text) + float.Parse("0" + lvRecibos.Items[m.i].SubItems[9].Text));
		        lblCertificadosCantidad.Text = string.Format("{0:N0}",Int16.Parse("0" + lblCertificadosCantidad.Text) + Int16.Parse("0" + lvRecibos.Items[m.i].SubItems[10].Text));
		        lblRecibosTotal.Text = string.Format("{0:N2}",float.Parse("0" + lblRecibosTotal.Text) + float.Parse("0" + lvRecibos.Items[m.i].SubItems[11].Text));
	        }
        }

        private void TotalValores()
        {
	        //const string PROCNAME_ = "TotalValores";

	        lblEfectivoV.Text = string.Format("{0:N2}",0);
	        lblChequesTotalV.Text = string.Format("{0:N2}",0);
	        lblChequesCantidadV.Text = "0";
	        lblTotalV.Text = string.Format("{0:N2}",0);

            //If lvValores.ListItems.Count < 1 Then
            //    Exit Sub
            //End If
	    
	        for (m.i = 0; m.i < lvValores.Items.Count; m.i++) {
		        if (lvValores.Items[m.i].SubItems[1].Text == "E") 
                {
			        lblEfectivoV.Text = string.Format("{0:N2}",float.Parse("0" + lblEfectivoV.Text) + float.Parse("0" + lvValores.Items[m.i].SubItems[4].Text));
		        } 
                else 
                {
			        lblChequesTotalV.Text = string.Format("{0:N2}",float.Parse("0" + lblChequesTotalV.Text) + float.Parse("0" + lvValores.Items[m.i].SubItems[4].Text));
		        }
		        lblChequesCantidadV.Text = string.Format("{0:N2}",float.Parse("0" + lblChequesCantidadV.Text) + float.Parse("0" + lvValores.Items[m.i].SubItems[5].Text));
		        lblTotalV.Text = string.Format("{0:N2}",float.Parse("0" + lblTotalV.Text) + float.Parse("0" + lvValores.Items[m.i].SubItems[4].Text));
	        }

	        //-- OJO ACA EMPIEZAN LOS CONTROLES -----

	        //TotalControles
	        //-- acá terminan los CONTROLES ---------

        }

        private void TotalControles()
        {
	        //const string PROCNAME_ = "TotalControles";

	        lblEfectivoC.ForeColor = Color.Black;
	        lblDivDolarC.ForeColor = Color.Black;
	        lblDivEuroC.ForeColor = Color.Black;
	        lblChequesTotalC.ForeColor = Color.Black;
	        lblChequesCantidadC.ForeColor = Color.Black;
	        lblCertificadosTotalC.ForeColor = Color.Black;
	        lblCertificadosCantidadC.ForeColor = Color.Black;

	        lblEfectivoC.Text = string.Format("{0:N2}",float.Parse("0" + lblEfectivo.Text) - float.Parse("0" + lblEfectivoV.Text) - float.Parse(txtLatEfectivo_Monto.Text));
	        lblChequesTotalC.Text = string.Format("{0:N2}",float.Parse("0" + lblChequesTotal.Text) - float.Parse("0" + lblChequesTotalV.Text) - float.Parse(txtLatCh_Monto.Text));
	        lblChequesCantidadC.Text = string.Format("{0:N0}",float.Parse("0" + lblChequesCantidad.Text) - float.Parse("0" + lblChequesCantidadV.Text) - float.Parse(txtLatCh_Cantidad.Text));
	        lblCertificadosCantidadC.Text = string.Format("{0:N0}",float.Parse("0" + lblCertificadosCantidad.Text) - float.Parse(txtLatCert_Cantidad.Text));
	        lblCertificadosTotalC.Text = string.Format("{0:N2}",float.Parse("0" + lblCertificadosTotal.Text) - float.Parse(txtLatCert_Monto.Text));

	        lblDivDolarC.Text = string.Format("{0:N2}",float.Parse("0" + lblDivDolarCantidad.Text) - float.Parse(txtLatDiv_dolar.Text));
	        lblDivEuroC.Text = string.Format("{0:N2}",float.Parse("0" + lblDivEuroCantidad.Text) - float.Parse(txtLatDiv_euro.Text));

	        if (float.Parse(lblEfectivoC.Text) != 0) lblEfectivoC.ForeColor = Color.Red;
	        if (System.Math.Abs(float.Parse(lblChequesTotalC.Text)) > 0.05) lblChequesTotalC.ForeColor = Color.Red;
	        if (Int16.Parse(lblChequesCantidadC.Text) != 0) lblChequesCantidadC.ForeColor = Color.Red;
	        if (float.Parse(lblCertificadosTotalC.Text) != 0) lblCertificadosTotalC.ForeColor = Color.Red;
	        if (Int16.Parse(lblCertificadosCantidadC.Text) != 0) lblCertificadosCantidadC.ForeColor = Color.Red;

	        if (float.Parse(lblDivDolarC.Text) != 0) lblDivDolarC.ForeColor = Color.Red;
	        if (float.Parse(lblDivEuroC.Text) != 0) lblDivEuroC.ForeColor = Color.Red;

        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
	        if (_optBuscar_0.Checked == true) 
            {
		       e.Handled = Funciones.util.SoloDigitos(e);
	        } 
            else 
            {
		        e.KeyChar = char.ToUpper(e.KeyChar);
	        }       
        }

        public static void Rendicion_Imprimir(string NroRendicion)
        {
            Cursor.Current = Cursors.WaitCursor;
           
            string sReporte = Global01.AppPath + "\\Reportes\\Rendicion1.rpt";
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroRendicion", NroRendicion);

            oReport.DataDefinition.FormulaFields["fZona"].Text = "'" + Global01.NroUsuario + "'";
            oReport.DataDefinition.FormulaFields["fViajante"].Text = "'" + Global01.ApellidoNombre + "'"; 

            varios.fReporte f = new varios.fReporte();
            f.Text = "Rendición n° " + NroRendicion;
            f.DocumentoNro = "RC-" + NroRendicion;
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
        }

        private void cboCheques_Leave(System.Object eventSender, System.EventArgs eventArgs)
        {
            string s = cboCheques.SelectedText;
            if (s.Length > 0 ) txtBd_Monto.Text = s.Substring(s.IndexOf("$") + 2).Replace(")", "");
        }

        private void cboRecibos_Leave(System.Object eventSender, System.EventArgs eventArgs)
        {

            if (cboRecibos.SelectedIndex >= 0)
            {
                this.txtRecHasta.Text = cboRecibos.SelectedValue.ToString();
            }

        }

        private void cboRecibos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboRecibos.SelectedIndex >= 0)
            {
                this.txtRecHasta.Text = cboRecibos.SelectedValue.ToString();
            }
        }

        private void lvValores_DoubleClick(System.Object eventSender, System.EventArgs eventArgs)
        {

            if (m.Accion == tAccion.Nuevo | m.Accion == tAccion.Modificar)
            {

                if (lvValores.SelectedItems != null & lvValores.SelectedItems.Count > 0)
                {

                    lblIdValor.Text = lvValores.SelectedItems[0].Text;

                    _opTipoDeposito_0.Checked = ((lvValores.SelectedItems[0].SubItems[1].Text=="E") ? true : false);
                    //_opTipoDeposito_1.Checked = ((lvValores.SelectedItems[0].SubItems[1].Text == "C") ? true : false);
                    dtBd_Fecha.Value = DateTime.Parse(lvValores.SelectedItems[0].SubItems[2].Text);
                    txtBd_Nro.Text = lvValores.SelectedItems[0].SubItems[3].Text;
                    txtBd_Monto.Text = lvValores.SelectedItems[0].SubItems[4].Text;
                    txtBdCh_Cantidad.Text = lvValores.SelectedItems[0].SubItems[5].Text;

                    // PABLO // cboCheques.Items.Add(new VB6.ListBoxItem((lvValores.FocusedItem.SubItems(7).Text), cboCheques.NewIndex));

                    cboCheques.Items.Insert(0,lvValores.SelectedItems[0].SubItems[7].Text);
                    cboCheques.SelectedIndex = 0;

                    //Funciones.util.BuscarIndiceEnCombo(ref cboCheques, lvValores.SelectedItems[0].SubItems[7].Text, false);

                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblRendicionValores WHERE ID=" + lvValores.SelectedItems[0].Text);

                    lvValores.Items.Remove(lvValores.SelectedItems[0]);
                    lvValores.SelectedItems.Clear(); 
                    
                    TotalValores();
                    TotalControles();
                };
            };
        }

        private void _optBuscar_0_CheckedChanged(object sender, EventArgs e)
        {
            if (_optBuscar_0.Checked)
            {//TxtBuscar
                txtBuscar.Enabled = true;
                mskFbuscar.Enabled = false;
                txtBuscar.Focus();
            }
            else
            {//Fecha
                txtBuscar.Enabled = false;
                mskFbuscar.Enabled = true;
                mskFbuscar.Focus();
            }
        }

        private void _opTipoDeposito_0_CheckedChanged(object sender, EventArgs e)
        {
            //Me.txtBdCh_Cantidad.Enabled = Me.opTipoDeposito.Item(1).value
            txtBd_Monto.Enabled = _opTipoDeposito_0.Checked;
            cboCheques.Enabled = _opTipoDeposito_1.Checked;

            if (_opTipoDeposito_0.Checked)
            {
                this.txtBdCh_Cantidad.Text = "0";
            }
            else
            {
                this.txtBdCh_Cantidad.Text = "1";
            }
        }

        private void txtLatCert_Monto_Leave(object sender, EventArgs e)
        {
            txtLatCert_Monto.Text = string.Format("{0:N2}", float.Parse("0" + txtLatCert_Monto.Text));
            TotalControles();
        }

        private void txtLatCert_Monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtLatCert_Monto.Text, ref e);
        }

        private void txtLatCert_Cantidad_Leave(object sender, EventArgs e)
        {
                txtLatCert_Cantidad.Text = string.Format("{0:N0}", float.Parse("0" + this.txtLatCert_Cantidad.Text));
                TotalControles();
        }

        private void txtLatCh_Cantidad_Leave(object sender, EventArgs e)
        {
                txtLatCh_Cantidad.Text = string.Format("{0:N0}", float.Parse("0" + txtLatCh_Cantidad.Text));
                TotalControles();
        }

        private void txtLatCh_Monto_Leave(object sender, EventArgs e)
        {
            txtLatCh_Monto.Text = string.Format("{0:N2}", float.Parse("0" + txtLatCh_Monto.Text));
            TotalControles();
        }

        private void txtLatCh_Monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtLatCh_Monto.Text, ref e);
        }

        private void txtLatEfectivo_Monto_Leave(object sender, EventArgs e)
        {
            txtLatEfectivo_Monto.Text = string.Format("{0:N2}", float.Parse("0" + txtLatEfectivo_Monto.Text));
            TotalControles();
        }

        private void txtLatEfectivo_Monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtLatEfectivo_Monto.Text, ref e);
        }

        private void txtBd_Monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtBd_Monto.Text, ref e);
        }

        private void txtBdCh_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

        private void txtBd_Monto_Leave(object sender, EventArgs e)
        {
            txtBd_Monto.Text = string.Format("{0:N2}", float.Parse("0" + txtBd_Monto.Text));
        }

        private void txtBd_Nro_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

        private void txtLatDiv_dolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtLatDiv_dolar.Text, ref e);
        }

        private void txtLatDiv_euro_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(txtLatDiv_euro.Text, ref e);
        }
        private void txtLatDiv_dolar_Leave(object sender, EventArgs e)
        {
            txtLatDiv_dolar.Text = string.Format("{0:N2}", float.Parse("0" + txtLatDiv_dolar.Text));
            TotalControles();
        }

        private void txtLatDiv_euro_Leave(object sender, EventArgs e)
        {
            txtLatDiv_euro.Text = string.Format("{0:N2}", float.Parse("0" + txtLatDiv_euro.Text));
            TotalControles();
        }

        private void txtLatCh_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

        private void txtLatCert_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

   
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Accion_Click(tAccion.Eliminar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Accion_Click(tAccion.Buscar);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            Accion_Click(tAccion.Imprimir);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (DatosValidos("all"))
            {
                Accion_Click(tAccion.Guardar);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (btnIniciar.Tag.ToString() == "INICIAR")
            {
                _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Rendicion,_auditor.Auditor.AccionesAuditadas.INICIA);

                btnIniciar.Text = "CANCELAR";
                btnIniciar.Tag = "CANCELAR";
                _ToolTip.SetToolTip(btnIniciar, "CANCELAR la Rendición");

                Accion_Click(tAccion.Nuevo);               

            }
            else
            {
                if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR la Rendición?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Rendicion, _auditor.Auditor.AccionesAuditadas.CANCELA);
                    
                    btnIniciar.Text = "Iniciar";
                    btnIniciar.Tag = "INICIAR";
                    _ToolTip.SetToolTip(btnIniciar, "INICIAR Recibo Nuevo");

                    Accion_Click(tAccion.Cancelar);
                    
                    //ObtenerMovimientos();
                }
            }                       
        }

        private void lvBuscar_DoubleClick(object sender, EventArgs e)
        {
            m.ItemX = lvBuscar.SelectedItems[0];
            Accion_Click(tAccion.Neutro);
        }

    } //fin clase
} //fin namespace