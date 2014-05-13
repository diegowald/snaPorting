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
            public delegate void emisorHandler<T>(T dato);

            public interface IEmisor<T>
            {
                emisorHandler<T> emisor {get; set;}
            }

            public interface IEmisor2<T>
            {
                emisorHandler<T> emisor2 { get; set; }
            }

            public interface IEmisor3<T>
            {
                emisorHandler<T> emisor3 { get; set; }
            }

            public interface IEmisor4<T>
            {
                emisorHandler<T> emisor4 { get; set; }
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

                public static void attachReceptor2<T>(this IEmisor2<T> baseEmisor, IReceptor<T> receptor)
                {
                    baseEmisor.emisor2 += receptor.onRecibir;
                }

                public static void emitir2<T>(this IEmisor2<T> baseEmisor, T dato)
                {
                    if (baseEmisor.emisor2 != null)
                    {
                        baseEmisor.emisor2(dato);
                    }
                }

                public static void attachReceptor3<T>(this IEmisor3<T> baseEmisor, IReceptor<T> receptor)
                {
                    baseEmisor.emisor3 += receptor.onRecibir;
                }

                public static void emitir3<T>(this IEmisor3<T> baseEmisor, T dato)
                {
                    if (baseEmisor.emisor3 != null)
                    {
                        baseEmisor.emisor3(dato);
                    }
                }

                public static void attachReceptor4<T>(this IEmisor4<T> baseEmisor, IReceptor<T> receptor)
                {
                    baseEmisor.emisor4 += receptor.onRecibir;
                }

                public static void emitir4<T>(this IEmisor4<T> baseEmisor, T dato)
                {
                    if (baseEmisor.emisor4 != null)
                    {
                        baseEmisor.emisor4(dato);
                    }
                }

            }

/*            public abstract class EmisorUserControl<T>
            {
                protected emisorHandler emisor;
                protected delegate void emisorHandler(T dato);

                internal void attachReceptor(IReceptor<T> receptor)
                {
                    emisor += receptor.onRecibir;
                }

                internal void emitir(T dato)
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

                internal void attachReceptor(IReceptor<T> receptor)
                {
                    emisor += receptor.onRecibir;
                }

                internal void emitir(T dato)
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

                internal void attachReceptor(IReceptor<T2> receptor)
                {
                    emisor2 += receptor.onRecibir;
                }

                internal void emitir2(T2 dato)
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

                internal void attachReceptor(IReceptor<T3> receptor)
                {
                    emisor3 += receptor.onRecibir;
                }

                internal void emitir3(T3 dato)
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