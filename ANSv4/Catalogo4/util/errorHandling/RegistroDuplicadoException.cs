using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.errorHandling
{
    class RegistroDuplicadoException : Exception
    {
        public RegistroDuplicadoException(Exception ex)
            : base("Numero de Boleta de deposito duplicada", ex)
        {
        }
    }
}
