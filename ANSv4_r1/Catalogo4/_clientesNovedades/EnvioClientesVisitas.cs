using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._clientesNovedades
{
    internal class EnvioClientesVisitas
    {

        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.
        private System.Data.OleDb.OleDbTransaction _TranActiva = null;
        private ClientesVisitasWS.ClientesVisitas Cliente;
        private bool WebServiceInicializado;
        private string m_MacAddress;

        private string m_ip;
        private string m_NroVisita = "0";
        private System.Data.OleDb.OleDbDataReader m_dr_Visitas;

        private bool DatosObtenidos;

        private System.Data.OleDb.OleDbConnection Conexion1;

        public EnvioClientesVisitas(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string MacAddress)
        {
            Inicializar(ipAddress, MacAddress);
            Conexion1 = Conexion;
        }

        public bool Inicializado
        {
            get { return WebServiceInicializado; }
        }

        internal void ObtenerDatos(string NroVisita)
        {
            DatosObtenidos = false;

            m_dr_Visitas = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_ClientesVisitas_rpt '" + NroVisita.Substring(6,8) + "'");
            if (!m_dr_Visitas.HasRows)
            {
                return;
            }
            m_NroVisita = NroVisita;

            DatosObtenidos = true;
        }

        public long EnviarVisitacliente()
        {
            bool Cancel = false;
            long resultado = 0;

            if (!WebServiceInicializado)
            {
                Cancel = true;
            }

            if (!DatosObtenidos)
            {
                Cancel = true;
            }

            try
            {
                if (!Cancel)
                {
                    if (_TranActiva== null)
                    {
                        //@ _TranActiva =Conexion1.BeginTransaction();
                        util.errorHandling.ErrorLogger.LogMessage("9");
                    }


                    while (m_dr_Visitas.Read() & resultado==0)
                    {
                        //resultado = Cliente.EnviarDatos(m_MacAddress, 
                        //                                m_dr_Visitas["Nro"].ToString(),
                        //                                (int)m_dr_Visitas["IDCliente"],
                        //                                DateTime.Parse(m_dr_Visitas["F_Carga"].ToString()),
                        //                                m_dr_Visitas["QRecibe"].ToString().Trim(),
                        //                                m_dr_Visitas["CCompras"].ToString().Trim(),
                        //                                m_dr_Visitas["CPagos"].ToString().Trim(),
                        //                                (bool)m_dr_Visitas["RamoLiviano"],
                        //                                (bool)m_dr_Visitas["RamoPesado"],
                        //                                (bool)m_dr_Visitas["RamoAgricola"],
                        //                                (bool)m_dr_Visitas["EsMonomarca"],
                        //                                m_dr_Visitas["Marca"].ToString().Trim(),
                        //                                (bool)m_dr_Visitas["CatRepGral"],
                        //                                (bool)m_dr_Visitas["CatLubricentro"],
                        //                                (bool)m_dr_Visitas["CatEstServicio"],
                        //                                (bool)m_dr_Visitas["CatMotos"],
                        //                                (bool)m_dr_Visitas["CatEspecialista"],
                        //                                (bool)m_dr_Visitas["EspMotor"],
                        //                                (bool)m_dr_Visitas["EspFrenos"],
                        //                                (bool)m_dr_Visitas["EspSuspension"],
                        //                                (bool)m_dr_Visitas["EspElectricidad"],
                        //                                (bool)m_dr_Visitas["EspAccesorios"],
                        //                                m_dr_Visitas["Detalle1"].ToString().Trim(),
                        //                                "",
                        //                                m_dr_Visitas["IdViajante"].ToString().Trim(),
                        //                                m_dr_Visitas["RazonSocial"].ToString().Trim(),
                        //                                m_dr_Visitas["Cuit"].ToString().Trim(),
                        //                                m_dr_Visitas["Email"].ToString().Trim(),
                        //                                m_dr_Visitas["Domicilio"].ToString().Trim(),
                        //                                m_dr_Visitas["Ciudad"].ToString().Trim(),
                        //                                m_dr_Visitas["Telefono"].ToString().Trim());
                        resultado = Cliente.EnviarDatos(m_MacAddress,
                                                        m_dr_Visitas["Nro"].ToString(),
                                                        m_dr_Visitas["IDCliente"].ToString(),
                                                        m_dr_Visitas["F_Carga"].ToString(),
                                                        m_dr_Visitas["QRecibe"].ToString().Trim(),
                                                        m_dr_Visitas["CCompras"].ToString().Trim(),
                                                        m_dr_Visitas["CPagos"].ToString().Trim(),
                                                        m_dr_Visitas["RamoLiviano"].ToString(),
                                                        m_dr_Visitas["RamoPesado"].ToString(),
                                                        m_dr_Visitas["RamoAgricola"].ToString(),
                                                        m_dr_Visitas["EsMononarca"].ToString(),
                                                        m_dr_Visitas["Marca"].ToString().Trim(),
                                                        m_dr_Visitas["CatRepGral"].ToString(),
                                                        m_dr_Visitas["CatLubricentro"].ToString(),
                                                        m_dr_Visitas["CatEstServicio"].ToString(),
                                                        m_dr_Visitas["CatMotos"].ToString(),
                                                        m_dr_Visitas["CatEspecialista"].ToString(),
                                                        m_dr_Visitas["EspMotor"].ToString(),
                                                        m_dr_Visitas["EspFrenos"].ToString(),
                                                        m_dr_Visitas["EspSuspension"].ToString(),
                                                        m_dr_Visitas["EspElectricidad"].ToString(),
                                                        m_dr_Visitas["EspAccesorios"].ToString(),
                                                        m_dr_Visitas["Detalle1"].ToString().Trim(),
                                                        "",
                                                        m_dr_Visitas["IdViajante"].ToString().Trim(),
                                                        m_dr_Visitas["RazonSocial"].ToString().Trim(),
                                                        m_dr_Visitas["Cuit"].ToString().Trim(),
                                                        m_dr_Visitas["Email"].ToString().Trim(),
                                                        m_dr_Visitas["Domicilio"].ToString().Trim(),
                                                        m_dr_Visitas["Ciudad"].ToString().Trim(),
                                                        m_dr_Visitas["Telefono"].ToString().Trim());
                        if (resultado == 0)
                        {
                            Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_ClientesVisitas_Transmicion_upd '" + m_NroVisita.Substring(6, 8) + "'");
                            if (_TranActiva!= null)
                            {
                                _TranActiva.Commit();
                                _TranActiva = null;
                            }
                        }
                        else
                        {
                            if (_TranActiva!= null)
                            {
                                _TranActiva.Rollback();
                                _TranActiva = null;
                            }
                        }

                    }

                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                return -1;
            }
        }

        internal void Inicializar(string ipAddress, string MacAddress)
        {
            bool Conectado = util.network.IPCache.instance.conectado;
   
            try
            {
                if (Conectado)
                {
                    if (!WebServiceInicializado)
                    {
                        Cliente = new ClientesVisitasWS.ClientesVisitas();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/ClientesVisitas.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                        }

                        m_MacAddress = MacAddress;
                        m_ip = ipAddress;
                        WebServiceInicializado = true;
                    }
                }
                else
                {
                    WebServiceInicializado = false;
                }

            }
            catch (Exception ex)
            {
                //if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                //{
                //    //	if (Err().Number == -2147024809) {
                //    // Intento con el ip interno
                //    Cliente = new InterDepositoWS.InterDeposito();
                //    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/InterDeposito.asmx?wsdl";
                //    if (Global01.proxyServerAddress != "0.0.0.0")
                //    {
                //        Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                //    }

                //    m_MacAddress = MacAddress;
                //    m_ip = ipAddressIntranet;
                //    WebServiceInicializado = true;
                //}
                //else
                //{
                    throw ex;
                //}
            }
        }


    }
}
