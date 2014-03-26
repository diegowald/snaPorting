
namespace Catalogo._pedidos
{
    public class PedidoItem
    {

        // Define como se llama este modulo para el control de errores

        private const string m_sMODULENAME_ = "clsPedidoItem";
        //variables locales para almacenar los valores de las propiedades

        private string mvarIDCatalogo;
        private int mvarCantidad;
        private bool mvarSimilar;
        private bool mvarBahia;
        private bool mvarOferta;
        private byte mvarDeposito;
        private float mvarPrecio;

        private string mvarObservaciones;

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

        public bool Similar
        {
            get { return mvarSimilar; }
            set { mvarSimilar = value; }
        }

        public bool Bahia
        {
            get { return mvarBahia; }
            set { mvarBahia = value; }
        }

        public bool Oferta
        {

            get { return mvarOferta; }
            set { mvarOferta = value; }
        }

        public byte Deposito
        {
            get { return mvarDeposito; }
            set { mvarDeposito = value; }
        }

        public float Precio
        {
            get { return mvarPrecio; }
            set { mvarPrecio = value; }
        }

        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

    }
}