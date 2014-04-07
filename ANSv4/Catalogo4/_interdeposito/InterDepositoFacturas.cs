using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._interdeposito
{
    public class InterDepositoFacturas
    {
        private string mvarT_Comprobante;
        private string mvarN_Comprobante;
        private float mvarImporte;

        public string T_Comprobante
        {
            get { return mvarT_Comprobante; }
            set { mvarT_Comprobante = value; }
        }

        public string N_Comprobante
        {
            get { return mvarN_Comprobante; }
            set { mvarN_Comprobante = value; }
        }

        public float Importe
        {
            get { return mvarImporte; }
            set { mvarImporte = value; }
        }
    }
}