using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._recibos
{
    public class ReciboItem
    {

        // Define como se llama este modulo para el control de errores

        //variables locales para almacenar los valores de las propiedades
        private byte mvarTipoValor;
        private float mvarImporte;
        private float mvarT_Cambio;
        private System.DateTime mvarF_EmiCheque;
        private System.DateTime mvarF_CobroCheque;
        private string mvarN_Cheque;
        private string mvarNrodeCuenta;
        private int mvarBanco;
        private string mvarCpa;

        private bool mvarChequePropio;
        public byte TipoValor
        {
            get { return mvarTipoValor; }
            set { mvarTipoValor = value; }
        }

        public float Importe
        {
            get { return mvarImporte; }
            set { mvarImporte = value; }
        }

        public float T_Cambio
        {
            get { return mvarT_Cambio; }
            set { mvarT_Cambio = value; }
        }

        public System.DateTime F_EmiCheque
        {
            get { return mvarF_EmiCheque; }
            set { mvarF_EmiCheque = value; }
        }

        public System.DateTime F_CobroCheque
        {
            get { return mvarF_CobroCheque; }
            set { mvarF_CobroCheque = value; }
        }

        public string N_Cheque
        {
            get { return mvarN_Cheque; }
            set { mvarN_Cheque = value; }
        }

        public string NrodeCuenta
        {

            get { return mvarNrodeCuenta; }
            set { mvarNrodeCuenta = value; }
        }

        public int Banco
        {
            get { return mvarBanco; }
            set { mvarBanco = value; }
        }

        public string Cpa
        {
            get { return mvarCpa; }
            set { mvarCpa = value; }
        }

        public bool ChequePropio
        {
            get { return mvarChequePropio; }
            set { mvarChequePropio = value; }
        }
    }
}
