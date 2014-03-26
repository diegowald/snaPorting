using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._devoluciones
{
    public class DevolucionItem
    {
        //variables locales para almacenar los valores de las propiedades

        private string mvarIDCatalogo;
        private int mvarCantidad;
        private byte mvarDeposito;
        private string mvarFactura;
        private byte mvarTipoDev;
        private string mvarVehiculo;
        private string mvarModelo;
        private string mvarMotor;
        private string mvarKm;

        private string mvarObservaciones;
        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

        public string IDCatalogo
        {
            get { return mvarIDCatalogo; }
            set { mvarIDCatalogo = value; }
        }

        public int cantidad
        {
            get { return mvarCantidad; }
            set { mvarCantidad = value; }
        }

        public byte Deposito
        {
            get { return mvarDeposito; }
            set { mvarDeposito = value; }
        }

        public string Factura
        {

            get { return mvarFactura; }
            set { mvarFactura = value; }
        }

        public string Vehiculo
        {
            get { return mvarVehiculo; }
            set { mvarVehiculo = value; }
        }

        public string Modelo
        {
            get { return mvarModelo; }
            set { mvarModelo = value; }
        }

        public string Motor
        {
            get { return mvarMotor; }
            set { mvarMotor = value; }
        }

        public string KM
        {
            get { return mvarKm; }
            set { mvarKm = value; }
        }

        public byte TipoDev
        {
            get { return mvarTipoDev; }
            set { mvarTipoDev = value; }
        }
    }
}