using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.notifications
{
    public class NotificationCenter : Catalogo.util.singleton<NotificationCenter>,
        Funciones.emitter_receiver.IEmisor<util.Pair<string, float>>,
        Funciones.emitter_receiver.ICancellableEmitter
    {
        public void notificar(string mensaje, float porcentaje)
        {
            this.emitir(new util.Pair<string, float>(mensaje, porcentaje));
        }

        public void requestCancel(ref bool cancel)
        {
            this.askCancel(ref cancel);
        }

        public Funciones.emitter_receiver.emisorHandler<util.Pair<string, float>> emisor
        {
            get;
            set;
        }

        onRequestCancel ICancellableEmitter.requestCancel
        {
            get;
            set;
        }
    }
}