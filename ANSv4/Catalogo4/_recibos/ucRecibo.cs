using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._recibos
{

    public partial class ucRecibo : UserControl
    {

        //public System.Data.OleDb.OleDbConnection Conexion
        //{
        //    set { oConexion = value; }
        //}

        //public System.Data.OleDb.OleDbConnection Conexion
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //        oConexion = value;
        //    }
        //}

        public ucRecibo()
        {
            InitializeComponent();

            Catalogo.Funciones.util.CargaCombo(ref Global01.Conexion , ref cboCliente, "tblClientes", "RazonSocial", "ID", "Activo<>1", "RazonSocial", true, true);

        }


        // diable Ordenar columna para todo el grid
        private void ccDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.ccDataGridView.AutoGenerateColumns = true;

            if (cboCliente.SelectedIndex > 0)
            {
                this.Parent.Text = "Recibo para el cliente: " + this.cboCliente.Text.ToString() + " (" + this.cboCliente.ComboBox.SelectedValue + ")";
                this.ccDataGridView.DataSource = Catalogo.Funciones.oleDbFunciones.xGetDt(ref Global01.Conexion, "v_CtaCte", "IDCliente=" + cboCliente.ComboBox.SelectedValue.ToString(), "Orden, Fecha");
            }
            else
            {

                if (!(this.Parent == null)) { this.Parent.Text = "Recibo para el cliente ..."; }

                this.ccDataGridView.DataSource = null;
            };

            //Set m.adoREC = xGetRSMDB(vg.Conexion, "v_ctacte", "IDCliente=" & CStr(cboCliente.ItemData(cboCliente.ListIndex)), "Orden, Fecha") 
            //If Not m.adoREC.EOF Then
            //    wSaldo = 0
            //    While Not m.adoREC.EOF
            //      Set m.ItemX = lvEstado.ListItems.Add(, , m.adoREC!Fecha)
            //      m.ItemX.SubItems(1) = m.adoREC!Comprobante
            //      m.ItemX.SubItems(2) = m.adoREC!Importe
            //      m.ItemX.SubItems(3) = m.adoREC!Saldo

            //      wSaldo = wSaldo + CSng(m.adoREC!SaldoS)
            //      m.ItemX.SubItems(4) = wSaldo

            //      m.ItemX.SubItems(5) = m.adoREC!Det_Comprobante
            //      m.ItemX.SubItems(6) = m.adoREC!ImpOferta
            //      m.ItemX.SubItems(7) = m.adoREC!TextoOferta
            //      m.ItemX.SubItems(8) = m.adoREC!Vencida
            //      m.ItemX.SubItems(9) = m.adoREC!ImpPercep       'XJXJXJ
            //      m.ItemX.SubItems(10) = m.adoREC!IdCliente
            //      m.ItemX.SubItems(11) = IIf(IsNull(m.adoREC!EstaAplicada), "N", "S")
            //      m.ItemX.SubItems(12) = IIf(IsNull(m.adoREC!EsContado), "0", m.adoREC!EsContado)

            //      If Mid(m.ItemX.SubItems(1), 1, 3) = "DEB" Then
            //          m.ItemX.ListSubItems(1).ForeColor = vbBlue
            //          m.ItemX.ListSubItems(1).Bold = vbBlue
            //      End If

            //      m.adoREC.MoveNext
            //    Wend

            //    'lvEstado.ColumnHeaders(3).Alignment = lvwColumnRight
            //    'lvEstado.ColumnHeaders(4).Alignment = lvwColumnRight
            //    'lvEstado.ColumnHeaders(5).Alignment = lvwColumnRight

            //End If
            //Set m.adoREC = Nothing

        }


    }

}