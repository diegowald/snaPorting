using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._recibos
{
    public class DeducirItem
    {
        //variables locales para almacenar los valores de las propiedades
        private string mvarConcepto;
        private float mvarImporte;
        private bool mvarPorcentaje;

        private bool mvarDeduAlResto;
        public bool DeduAlResto
        {
            get { return mvarDeduAlResto; }
            set { mvarDeduAlResto = value; }
        }

        public bool Porcentaje
        {

            get { return mvarPorcentaje; }
            set { mvarPorcentaje = value; }
        }

        public float Importe
        {
            get { return mvarImporte; }
            set { mvarImporte = value; }
        }

        public string Concepto
        {
            get { return mvarConcepto; }
            set { mvarConcepto = value; }
        }
    }
}