using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.notifications
{
    public class NotificationCenter : Catalogo.util.singleton<NotificationCenter>, Funciones.emitter_receiver.IEmisor<util.Pair<string, float>>
    {
        public void notificar(string mensaje, float porcentaje)
        {
            this.emitir(new util.Pair<string,float>(mensaje, porcentaje));
        }

        public Funciones.emitter_receiver.emisorHandler<util.Pair<string, float>> emisor
        {
            get;
            set;
        }
    }
}
