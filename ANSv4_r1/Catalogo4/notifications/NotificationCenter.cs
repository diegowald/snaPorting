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
        Funciones.emitter_receiver.ICancellableEmitter,
        Funciones.emitter_receiver.IEmisor2<short> // Para emitir cambio de usuario
    {


        public delegate void refreshNovedadesDelegate();
        public delegate void updateBannerDelegate(string filename);

        public refreshNovedadesDelegate refreshNovedades;
        public updateBannerDelegate updateBanner;

        internal void notificar(complexMessage msg)
        {
            this.emitir(msg);
        }

        internal void requestRefreshNovedades()
        {
            if (refreshNovedades != null)
            {
                refreshNovedades();
            }
        }

        private short _idClienteSeleccionado = 0;
        public short ClienteSeleccionado
        {
            get
            {
                return _idClienteSeleccionado;
            }
            set
            {
                if (value != _idClienteSeleccionado)
                {
                    _idClienteSeleccionado = value;
                    this.emitir2(_idClienteSeleccionado);
                }
            }
        }

        

        internal void requestUpdateBanner(string filename)
        {
            if (updateBanner != null)
            {
                updateBanner(filename);
            }
        }

        internal void requestCancel(ref bool cancel)
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

        public emisorHandler<short> emisor2
        {
            get;
            set;
        }
    }
}