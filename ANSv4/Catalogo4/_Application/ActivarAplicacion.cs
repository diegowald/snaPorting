using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._Application
{
    class ActivarAplicacion
    {

//    Public Event SincronizarActivarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private LLaveClienteWS.LLaveCliente cliente;
        private bool webServiceInicializado;
        private string _MacAddress;
        private string _ipAddress;

        public ActivarAplicacion(string MacAddress,
            string ipAddress, string ipAddressIntranet,
            bool usaProxy, string proxyServerAddress)
        {
            webServiceInicializado = false;
            inicializar(MacAddress, ipAddress, ipAddressIntranet, usaProxy, proxyServerAddress);
        }

        public bool Inicializao
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public void inicializar(string MacAddress,
            string ipAddress, string ipAddressIntranet, bool usaProxy, string proxyServerAddress)
        {
            bool conectado = util.SimplePing.ping(ipAddress, 5000);
            if (!conectado)
            {
                conectado = util.SimplePing.ping(ipAddressIntranet, 5000);
            }


            //        On Error GoTo errhandler
            try
            {
                if (!webServiceInicializado)
                {
                    if (conectado)
                    {
                        cliente = new LLaveClienteWS.LLaveCliente();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/LLaveCliente.asmx?wsdl";
                        if (usaProxy)
                        {
                            cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                        }

                        _MacAddress = MacAddress;
                        _ipAddress = ipAddress;
                        webServiceInicializado = true;
                    }
                    else
                    {
                        webServiceInicializado = false;
                    }
                }
            }
            catch
            {
                //errhandler:
                //        If Err.Number = -2147024809 Then
                // Intento con el ip interno
                cliente = new LLaveClienteWS.LLaveCliente();
                cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/LLaveCliente.asmx?wsdl";
                if (usaProxy)
                {
                    cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                }
                _MacAddress = MacAddress;
                webServiceInicializado = true;
                _ipAddress = ipAddressIntranet;
                //        Else
                //            Err.Raise(Err.Number, Err.Source, Err.Description)
                //        End If
            }
        }


        private string obtenerLlaveViajante(string ZonaVianajante)
        {
            string llaveViajante = string.Empty;

            if (util.SimplePing.ping(_ipAddress, 5000))
            {
                string s = cliente.ObtenerLLaveViajante(ZonaVianajante);
                if (s.Trim().Length > 0)
                {
                    llaveViajante = ZonaVianajante + Global01.NroUsuario + Global01.Cuit + util.Registro.obtenerCRC(s + Global01.IDMaquinaCRC);
                }
            }
            return llaveViajante;
        }

        public void activar()
        {
            bool cancel = false;
            int estado;
            string sResultado = String.Empty;

            //    '    RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)

            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
                //    '        RaiseEvent SincronizarActivarAppProgress("Verificando Estado Viajante/Cliente", 40, Cancel)
            }

            if (!cancel)
            {
                string wZonaviajante = Global01.LLaveViajante.Substring(1, 5);
                string wLLaveViajante;
                //    '        Dim wZonaViajante As String
                //    '        Dim wLlaveViajante As String

                //    '        'zzzzz iiiii ccccccccccc
                //    '        '12345 67890 12345678901

                //    '        wZonaViajante = Mid(vg.LLaveViajante, 1, 5)
                Global01.NroUsuario = Global01.LLaveViajante.Substring(6, 5);
                Global01.Cuit = Global01.LLaveViajante.Substring(11, 11);

                wLLaveViajante = obtenerLlaveViajante(wZonaviajante);

                if (util.Registro.validateLlaveViajante(wLLaveViajante))
                {
                    estado = obtenerEstado(ref cancel, Global01.Cuit,
                        Global01.NroUsuario, wZonaviajante, Global01.IDMaquina);

                    switch (estado)
                    {
                        case 1: // El Cliente NO está Activo
                            sResultado = "ERROR: El Cliente NO está Activo";
                            break;
                        case 2: // No Existe la Tupla Viajante Cliente
                            sResultado = "ERROR: Cuit, Nº de Cuenta, y Zona de Viajante, No Coinciden";
                            break;
                        case 3: // Esa PC ya está Registrada Comunicarse con auto náutica sur
                            sResultado = "ERROR: Esa PC ya está Registrada, Comunicarse con auto náutica sur";
                            break;
                        case 4: // 'El viajante NO esta Autorizado a registrar
                            sResultado = "ERROR: El viajante NO esta Autorizado a registrar el catálogo";
                            break;
                        case 5: // Usado en el web service para verificar si los datos enviados estaban OK
                            break;
                        case 6:
                            sResultado = "(6). El Cliente Existe en la tabla de Usuarios, Comunicarse con auto náutica sur";
                            break;
                        case 7:
                            sResultado = "(7). Fallo en tabla de Usuarios Habilitados, Comunicarse con auto náutica sur";
                            break;
                        case 8:
                            sResultado = "(8). Error, Comunicarse con auto náutica sur";
                            break;
                        case 9: // Por Aca Bien
                            sResultado = "Registro Exitoso, copie los datos obtenidos en la PC del Cliente";
                        //    '                    DeleteKeyINI("DATOS", "LLaveViajante")
                            Global01.IDMaquinaREG = util.Registro.obtenerCRC(Global01.IDMaquina + Global01.IDMaquinaCRC);

                        //    '                    WriteINI("DATOS", "RegistrationKey", vg.IDMaquinaREG)

                            Global01.AppActiva = true;

                            sResultado = "¡Aplicación ACTIVADA CON EXITO!";
                            break;
                        default: // Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados (0 ó -1)
                            sResultado = "Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados";
                            break;
                    }
                }
                else
                {
                    //    '            DeleteKeyINI("DATOS", "LLaveViajante")
                    sResultado = "Error en la llave ingresada por el viajante";
                }
            }

            if (sResultado != String.Empty)
            {
                //    '        MsgBox(sResultado, vbInformation, "Resultado Activación")
            }
        }


        private int obtenerEstado(ref bool cancel, string Cuit,
            string IDAns, string IDViajante, string LLaveCliente)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000))
            {
                // Conexion no valida
                cancel = true;
                return -1;
            }
            else
            {
                return cliente.ObtenerLLaveCatalogo(_MacAddress, Cuit, IDAns, IDViajante);
            }
        }

        public void estadoActual()
        {
            bool cancel = false;
            byte estado;
            string sResultado = String.Empty;

            //        RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)
            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
                //            RaiseEvent SincronizarActivarAppProgress("Estado Actual del Catálogo", 40, Cancel)
            }

            if (!cancel)
            {
                estado = obtenerEstadoActual(ref cancel, Global01.Cuit, Global01.NroUsuario);
                switch (estado)
                {
                    case 1:
                        sResultado = "ERROR: El Cliente NO está Activo";
                        break;
                    case 2:
                        sResultado = "ERROR: Esa PC está Registrada con otro Cuit y Nº de Cuenta";
                        break;
                    case 3:
                        sResultado = "ERROR: Esa PC NO está Registrada, Comunicarse con auto náutica sur";
                        break;
                    case 4:
                        sResultado = "El estado de registro del catálogo está correcto";
                        break;
                    default:
                        sResultado = "Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados";
                        break;
                }


                if (!cancel)
                {
                    //                RaiseEvent SincronizarActivarAppProgress("Un momento por favor...", 70, Cancel)
                }
            }

            if (sResultado != String.Empty)
            {
                //            RaiseEvent SincronizarActivarAppProgress(sResultado, 100, Cancel)
                //            MsgBox(sResultado & vbCrLf & vbCrLf & "ID: " & m_MacAddress, vbInformation, "Estado Actual del Catálogo")
            }
        }



        private byte obtenerEstadoActual(ref bool cancel, string Cuit, string IDAns)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000))
            {
                return 0;
            }
            else
            {
                return cliente.ObtenerEstadoActual(_MacAddress, Cuit, IDAns);
            }
        }


        public void listaPrecio()
        {
            byte wListaPrecio;
            bool cancel = false;

            cancel = !webServiceInicializado;

            if (!cancel)
            {
                wListaPrecio = obtenerListaPrecio(ref cancel, Global01.Cuit, Global01.NroUsuario);
                if (!cancel)
                {
                    if (wListaPrecio > 0)
                    {
                        Global01.ListaPrecio = wListaPrecio;
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE AppConfig SET ListaPrecio=" + Global01.ListaPrecio.ToString());
                    }
                }
            }
        }


        private byte obtenerListaPrecio(ref bool cancel, string Cuit, string IDAns)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000))
            {
                // Conexion no valida
                cancel = true;
                return 0;
            }
            else
            {
                return cliente.ObtenerListaPrecio(_MacAddress, Cuit, IDAns);
            }
        }
    }
}
