using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.preload
{
    public class ProductosPreloader
    {
        /// Esta clase hace un preload de los productos.

        private string strComando = "SELECT " +
       "mid(c.C_Producto,5) as C_Producto, c.Linea, c.Precio, c.PrecioOferta, c.Precio as PrecioLista, c.Familia, c.Marca, c.Modelo, c.N_Producto, c.Motor, c.Año, c.O_Producto, c.ReemplazaA, c.Contiene, c.Equivalencia, c.Original, c.Abc, c.Alerta, " +
       "c.LineaPorcentaje, c.ID, c.Control, c.C_Producto as CodigoAns,  c.MiCodigo,  c.Suspendido, c.OfertaCantidad, c.Tipo, DateDiff('d',c.Vigencia,Date()) as Vigencia " +
       "FROM v_CatVehProdLin AS c";

        private Funciones.BackgroundReader.BackgroundDataLoader backgroundWorker;

        private System.Data.DataTable table;

        public delegate void WorkFinishedHandler(System.Data.DataTable dataTable);

        public WorkFinishedHandler onWorkFinished;


        public ProductosPreloader()
        {
            backgroundWorker = new Funciones.BackgroundReader.BackgroundDataLoader(Catalogo.Funciones.BackgroundReader.BackgroundDataLoader.JOB_TYPE.Asincronico,
        Global01.strConexionUs);
            backgroundWorker.onWorkFinishedHandler += dataReady;

            table = null;
            execute();
        }

        private void dataReady(System.Data.DataTable dataTable)
        {
            table = dataTable;
     
            if (onWorkFinished != null)
            {
                onWorkFinished(table);
            }
        }

        public void execute()
        {
            if (table == null)
            {
                backgroundWorker.executeQuery(strComando);
            }
            else
            {
                if (onWorkFinished != null)
                {
                    onWorkFinished(table);
                }
            }
        }

    }
}
