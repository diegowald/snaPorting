using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo
{
    namespace Funciones
    {
        namespace emitter_receiver
        {
            public interface IReceptor<T>
            {
                void onRecibir(T dato);
            }
        }
    }
}