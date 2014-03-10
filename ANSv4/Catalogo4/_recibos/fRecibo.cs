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
    public partial class fRecibo : Form
    {
        private string strSQLCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Catalogo ANS\\datos\\catalogo.mdb;Persist Security Info=True;Password=video80min;User ID=inVent;Jet OLEDB:System database=C:\\Windows\\Help\\kbappcat.hlp";

        public fRecibo()
        {
            InitializeComponent();
           
            Catalogo.Funciones.util.CargarCombo(ref cboCliente, "tblClientes", "RazonSocial", "ID", strSQLCon, "Activo<>1", "RazonSocial", true, true);

        }


    }
}
