using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.preload
{
    public class Preloader
    {
        private static Preloader _instance = null;

        public static Preloader instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Preloader();
                    _instance.initialize();
                }
                return _instance;
            }
        }
        

        private ProductosPreloader _productos;

        private Preloader()
        {
            _productos = new ProductosPreloader();
        }

        private void initialize()
        {
            _productos.execute();
        }

        public ProductosPreloader productos
        {
            get
            {
                return _productos;
            }
        }

        internal void refresh()
        {
            productos.execute();
        }
    }
}
