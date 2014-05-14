using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.Funciones.emitter_receiver
{
    public delegate void onRequestCancel(ref bool cancel);

    public interface ICancellableEmitter
    {
        onRequestCancel requestCancel { get; set; }
    }

    public interface ICancellableReceiver
    {
        void onRequestCancel(ref bool cancel);
    }

    public static class CancellableExtension
    {
        public static void attachCancellableReceptor(this ICancellableEmitter baseEmisor, ICancellableReceiver receptor)
        {
            baseEmisor.requestCancel += receptor.onRequestCancel;
        }

        public static void askCancel(this ICancellableEmitter baseEmisor, ref bool cancel)
        {
            if (baseEmisor.requestCancel != null)
            {
                baseEmisor.requestCancel(ref cancel);
            }
        }
    }
}
