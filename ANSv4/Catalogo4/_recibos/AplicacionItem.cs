using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._recibos
{
    public class AplicacionItem
    {

        //variables locales para almacenar los valores de las propiedades
        //copia local
        private string mvarConcepto;
        //copia local
        private float mvarImporte;
        //copia local
        private string mvarObservaciones;

        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
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
