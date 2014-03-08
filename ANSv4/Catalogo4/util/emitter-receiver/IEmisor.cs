using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo
{
    namespace util
    {
        namespace emitter_receiver
        {
            public delegate void emisorHandler<T>(T dato);
            
            public interface IEmisor<T>
            {
                emisorHandler<T> emisor {get; set;}
            }

            public static class emisorExtension
            {
                public static void attachReceptor<T>(this IEmisor<T> baseEmisor, IReceptor<T> receptor)
                {
                    baseEmisor.emisor+=receptor.onRecibir;
                }

                public static void emitir<T>(this IEmisor<T> baseEmisor, T dato)
                {
                    if (baseEmisor.emisor != null)
                    {
                        baseEmisor.emisor(dato);
                    }
                }
            }

/*            public abstract class EmisorUserControl<T>
            {
                protected emisorHandler emisor;
                protected delegate void emisorHandler(T dato);

                public void attachReceptor(IReceptor<T> receptor)
                {
                    emisor += receptor.onRecibir;
                }

                public void emitir(T dato)
                {
                    if (emisor != null)
                    {
                        emisor(dato);
                    }
                }
            }

            public abstract class Emisor<T>
            {
                protected emisorHandler emisor;
                protected delegate void emisorHandler(T dato);

                public void attachReceptor(IReceptor<T> receptor)
                {
                    emisor += receptor.onRecibir;
                }

                public void emitir(T dato)
                {
                    if (emisor != null)
                    {
                        emisor(dato);
                    }
                }
            }

            public abstract class Emisor2<T1, T2> : Emisor<T1>
            {
                protected emisorHandler2 emisor2;
                protected delegate void emisorHandler2(T2 dato);

                public void attachReceptor(IReceptor<T2> receptor)
                {
                    emisor2 += receptor.onRecibir;
                }

                public void emitir2(T2 dato)
                {
                    if (emisor2 != null)
                    {
                        emisor2(dato);
                    }
                }
            }

            public abstract class Emisor3<T1, T2, T3> : Emisor2<T1, T2>
            {
                protected emisorHandler3 emisor3;
                protected delegate void emisorHandler3(T3 dato);

                public void attachReceptor(IReceptor<T3> receptor)
                {
                    emisor3 += receptor.onRecibir;
                }

                public void emitir3(T3 dato)
                {
                    if (emisor3 != null)
                    {
                        emisor3(dato);
                    }
                }
            }

  */          /// y asi sucesivamente si fuera necesario
        }
    }
}