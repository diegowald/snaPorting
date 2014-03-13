using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public class ExistenciaProducto
    {
        public ExistenciaProducto()
        {
        }

        public string getExistencia(string idProducto, string idUsuario)
        {
            // Toda esta clase hay que hacerla para que funcione como un background worker.
            CatalogoLibraryVB.IPPrivado ipPriv = new CatalogoLibraryVB.IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
            // TODO: agregar la configuracion del proxy
            string ipPrivado = ipPriv.GetIP();
            string ipIntranet = ipPriv.GetIpIntranet();
            string ipCatalogo = ipPriv.GetIPCatalogo();

            CatalogoLibraryVB.VerExistencia existencia = new CatalogoLibraryVB.VerExistencia();
            existencia.Inicializar(Global01.IDMaquina,  ipPrivado,ipIntranet, false, "");
            string pSemaforo = "";
            existencia.ExistenciaSemaforo(idProducto, ref pSemaforo);
            return pSemaforo;
        }

    }
}
