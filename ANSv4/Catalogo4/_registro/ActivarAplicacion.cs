using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._Application
{
    class ActivarAplicacion
    {

//    Public Event SincronizarActivarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private LLaveClienteWS.LLaveCliente cliente;
        private bool webServiceInicializado;
        private string _MacAddress;
        private string _ipAddress;

        public ActivarAplicacion(string MacAddress, string ipAddress)
        {
            webServiceInicializado = false;
            inicializar(MacAddress, ipAddress);
        }

        public bool Inicializao
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public void inicializar(string MacAddress, string ipAddress)
        {
            bool conectado = util.network.IPCache.instance.conectado;

 
            try
            {
                if (!webServiceInicializado)
                {
                    if (conectado)
                    {
                        cliente = new LLaveClienteWS.LLaveCliente();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/LLaveCliente.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
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
            catch (Exception ex)
            {
                //if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                //{
                //    //errhandler:
                //    //        If Err.Number = -2147024809 Then
                //    // Intento con el ip interno
                //    cliente = new LLaveClienteWS.LLaveCliente();
                //    cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/LLaveCliente.asmx?wsdl";
                //    if (Global01.proxyServerAddress != "0.0.0.0")
                //    {
                //        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                //    }
                //    _MacAddress = MacAddress;
                //    webServiceInicializado = true;
                //    _ipAddress = ipAddressIntranet;
                //}
                //else
                //{
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                //}
            }
        }

        private string obtenerLlaveViajante(string ZonaViajante)
        {
            string llaveViajante = string.Empty;
            try
            {
                if (util.network.IPCache.instance.conectado)
                {
                    string s = cliente.ObtenerLLaveViajante(ZonaViajante);
                    if (s.Trim().Length > 0)
                    {
                        string xParam = s + Global01.IDMaquinaCRC;
                        llaveViajante = ZonaViajante + Global01.NroUsuario + Global01.Cuit.Replace("-", "") + _registro.AppRegistro.ObtenerCRC(xParam);
                    }
                }
                return llaveViajante;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                return llaveViajante;
            }
        }

        public void activar()
        {
            bool cancel = false;
            int estado;
            string sResultado = String.Empty;

//RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)

            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
//RaiseEvent SincronizarActivarAppProgress("Verificando Estado Viajante/Cliente", 40, Cancel)
            }

            if (!cancel)
            {
                //zzzzz iiiii ccccccccccc
                //12345 67890 12345678901
                //wZonaViajante = Mid(vg.LLaveViajante, 1, 5)

                string wZonaviajante = Global01.LLaveViajante.Substring(0, 5);
                string wLLaveViajante;

                Global01.NroUsuario = Global01.LLaveViajante.Substring(5, 5);
                Global01.Cuit = Global01.LLaveViajante.Substring(10, 11);

                wLLaveViajante = obtenerLlaveViajante(wZonaviajante);

                if (_registro.AppRegistro.ValidateLLaveViajante(wLLaveViajante))
                {
                    estado = obtenerEstado(ref cancel, Global01.Cuit, Global01.NroUsuario, wZonaviajante, Global01.IDMaquina);

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
                            Funciones.modINIs.DeleteKeyINI("DATOS", "LLaveViajante");
                            
                           string xParam = Global01.IDMaquina + Global01.IDMaquinaCRC;
                           Global01.IDMaquinaREG = _registro.AppRegistro.ObtenerCRC(xParam);

                           Funciones.modINIs.WriteINI("DATOS", "RegistrationKey", Global01.IDMaquinaREG);

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
                    Funciones.modINIs.DeleteKeyINI("DATOS", "LLaveViajante");
                    sResultado = "Error en la llave ingresada por el viajante";
                }
            }

            if (sResultado != String.Empty)
            {
                MessageBox.Show(sResultado, "Resultado de la Activación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int obtenerEstado(ref bool cancel, string Cuit, string IDAns, string IDViajante, string LLaveCliente)
        {
            if (!util.network.IPCache.instance.conectado)
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

            //RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)
            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
              //RaiseEvent SincronizarActivarAppProgress("Estado Actual del Catálogo", 40, Cancel)
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
                    //RaiseEvent SincronizarActivarAppProgress("Un momento por favor...", 70, Cancel)
                }
            }

            if (sResultado != String.Empty)
            {
                //RaiseEvent SincronizarActivarAppProgress(sResultado, 100, Cancel)
                MessageBox.Show(sResultado + "\n\nID: " + _MacAddress, "Estado Actual del Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private byte obtenerEstadoActual(ref bool cancel, string Cuit, string IDAns)
        {
            if (!util.network.IPCache.instance.conectado)
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
            if (!util.network.IPCache.instance.conectado)
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
