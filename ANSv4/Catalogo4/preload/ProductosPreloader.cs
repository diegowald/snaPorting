using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.preload
{
    public class ProductosPreloader
    {
        /// Esta clase hace un preload de los productos.
        /// 

        private enum LOAD_STATUS
        {
            UNLOADED,
            LOADING,
            LOADED
        }

        private LOAD_STATUS _status;
        private Object obj;
      //   Format([c].[Precio]+(([c].[Precio]*[c].[LineaPorcentaje])/100),"fixed")
        private string strComando = "SELECT " +
       "mid(c.C_Producto,5) as C_Producto, c.Linea, " +
       "Format(c.Precio + ((c.Precio*c.LineaPorcentaje)/100),'fixed') AS Precio, " +
       "c.PrecioOferta, " +
       "Format(c.Precio + ((c.Precio*c.LineaPorcentaje)/100),'fixed') AS PrecioLista, " + 
       "c.Familia, c.Marca, c.Modelo, c.N_Producto, c.Motor, c.Año, c.O_Producto, c.ReemplazaA, c.Contiene, c.Equivalencia, c.Original, c.Abc, c.Alerta, " +
       "c.LineaPorcentaje, c.ID, c.Control, c.C_Producto as CodigoAns,  c.MiCodigo,  c.Suspendido, c.OfertaCantidad, c.Tipo, DateDiff('d',c.Vigencia,Date()) as Vigencia " +
       "FROM v_CatVehProdLin AS c";

        private Funciones.BackgroundReader.BackgroundDataLoader backgroundWorker;

        private System.Data.DataTable table;

        public delegate void WorkFinishedHandler(System.Data.DataTable dataTable);

        public WorkFinishedHandler onWorkFinished;

        public ProductosPreloader()
        {
            backgroundWorker = new Funciones.BackgroundReader.BackgroundDataLoader(Catalogo.Funciones.BackgroundReader.BackgroundDataLoader.JOB_TYPE.Asincronico, Global01.strConexionUs);
            backgroundWorker.onWorkFinishedHandler += dataReady;
            _status = LOAD_STATUS.UNLOADED;
            obj = new Object();
            table = null;
            execute();
        }

        private void dataReady(System.Data.DataTable dataTable)
        {
            lock (obj)
            {
                _status = LOAD_STATUS.LOADED;
                table = dataTable;
            }
     
            if (onWorkFinished != null)
            {
                onWorkFinished(table);
            }
        }

        public void execute()
        {
            if (table == null)
            {
                if (_status == LOAD_STATUS.UNLOADED)
                {
                    lock (obj)
                    {
                        switch (_status)
                        {
                            case LOAD_STATUS.UNLOADED:
                                {
                                    _status = LOAD_STATUS.LOADING;
                                    System.Diagnostics.Debug.WriteLine("CARGANDO PRODUCTO");
                                    backgroundWorker.executeQuery(strComando);
                                }
                                break;
                            case LOAD_STATUS.LOADED:
                                System.Diagnostics.Debug.WriteLine("LOADED...");
                                break;
                            case LOAD_STATUS.LOADING:
                                System.Diagnostics.Debug.WriteLine("LOADING...");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                if (onWorkFinished != null)
                {
                    onWorkFinished(table);
                }
            }
        }


        internal void clear()
        {
            lock (obj)
            {
                table = null;
                _status = LOAD_STATUS.UNLOADED;
            }
        }
    }
}
