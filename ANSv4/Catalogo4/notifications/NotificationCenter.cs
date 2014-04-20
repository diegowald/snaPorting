using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.varios
{
    public struct complexMessage
    {
        public util.Pair<string, float> progress1;
        public util.Pair<string, float> progress2;
    }

    public class NotificationCenter : Catalogo.util.singleton<NotificationCenter>,
        Funciones.emitter_receiver.IEmisor<complexMessage>,
        Funciones.emitter_receiver.ICancellableEmitter
    {
        public void notificar(complexMessage msg)
        {
            this.emitir(msg);
        }

        public void requestCancel(ref bool cancel)
        {
            this.askCancel(ref cancel);
        }

        onRequestCancel ICancellableEmitter.requestCancel
        {
            get;
            set;
        }

        public emisorHandler<complexMessage> emisor
        {
            get;
            set;
        }
    }
}