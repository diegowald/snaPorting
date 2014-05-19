using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Catalogo._pedidos
{
    public partial class EstadoPedidoMostrar : Form
    {
        public string EstadoMsg;
        public string EstadoActivo;

        public EstadoPedidoMostrar()
        {
            InitializeComponent();           
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EstadoPedidoMostrar_Load(object sender, EventArgs e)
        {
            switch (EstadoActivo)
            {
                case "1":
                    gris_a_procesar.Visible = false;
                    l_a_procesar.Visible = true;
                    break;
                case "2":
                    gris_en_espera.Visible = false;
                    l_en_espera.Visible = true;
                    break;
                case "3":
                    gris_en_proceso.Visible = false;
                    l_en_proceso.Visible = true;
                    break;
                case "4":
                    gris_para_despacho.Visible = false;
                    l_para_despacho.Visible = true;
                    break;
                case "5":
                    gris_en_transporte.Visible = false;
                    l_en_transporte.Visible = true;
                    break;
            }
            errormessage.Text = EstadoMsg;

        }
    }
}
