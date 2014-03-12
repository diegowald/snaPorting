using System;
using System.Management;

namespace Catalogo._registro
{
    static class AppRegistro
    {

        private const string m_sMODULENAME_ = "AppRegistro";

        public struct WMIPair
        {
            public string first;
            public string second;
        }

        public static string ObtenerIDMaquina()
        {
            const string PROCNAME_ = "ObtenerIDMaquina";

            string functionReturnValue = null;

            string[] varSerial = new string[5];
            WMIPair[] varObjectToID = new WMIPair[5];

            //HackerScan
            varObjectToID[1].first = "Win32_BIOS";
            varObjectToID[1].second = "SerialNumber";

            varObjectToID[2].first = "Win32_BaseBoard";
            varObjectToID[2].second = "SerialNumber";

            varObjectToID[3].first = "Win32_Processor";
            varObjectToID[3].second = "ProcessorId";

            varObjectToID[4].first = "Win32_OperatingSystem";
            varObjectToID[4].second = "SerialNumber";

            varSerial[1] = TomarInfoWMI(ref varObjectToID[1]);
            varSerial[2] = TomarInfoWMI(ref varObjectToID[2]);
            varSerial[3] = TomarInfoWMI(ref varObjectToID[3]);
            varSerial[4] = TomarInfoWMI(ref varObjectToID[4]);

            string xParam = varSerial[1].ToString() + varSerial[2].ToString() + varSerial[3].ToString() + varSerial[4].ToString();
            functionReturnValue = ((int)(Global01.miSABOR)).ToString() + ObtenerCRC(ref xParam);

            return functionReturnValue.Trim().ToUpper();

        }

        private static string TomarInfoWMI(ref WMIPair clave)
        {
            try
            {
                const string PROCNAME_ = "TomarInfoWMI";

                string functionReturnValue = null;

                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select " + clave.second + " from " + clave.first);

                foreach (ManagementObject MO in MOS.Get())
                {
                    functionReturnValue = MO[clave.second].ToString();
                }

                return functionReturnValue;
            }
            catch 
            {
                return "";
            }
        }

        public static string ObtenerCRC(ref string s)
        {
            const string PROCNAME_ = "ObtenerCRC";

            string functionReturnValue = null;

            try
            {
                System.Security.Cryptography.MD5 m = System.Security.Cryptography.MD5.Create();
                byte[] data = m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s));

                System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
                foreach (byte b_loopVariable in data)
                {
                    sBuilder.Append(b_loopVariable.ToString("x2"));
                }

                functionReturnValue = sBuilder.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }
            finally
            {
            }
            return functionReturnValue.ToUpper();
        }

        public static bool ValidateMachineId(string s)
        {
            if (s.Trim().Length == 0) { return false; };

            bool resultado = false;

            if (s == ObtenerCRC(ref Global01.IDMaquina)) { resultado = true; };

            return resultado;
        }



        public static bool ValidateRegistration(string s)
        {
            const string PROCNAME_ = "ValidateRegistration";

            bool functionReturnValue = false;

            try
            {
                if (s.Trim().Length == 0)
                {
                    functionReturnValue = false;
                }
                else
                {
                    string xParam = Global01.IDMaquina.ToString() + Global01.IDMaquinaCRC.ToString();
                    functionReturnValue = s == ObtenerCRC(ref xParam);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }
            finally
            {
            }

            return functionReturnValue;
        }

        public static bool ValidateLLaveViajante(string s)
        {
            if (s.Trim().Length == 0) { return false; };

            bool resultado = false;

            if (s == Global01.LLaveViajante) { resultado = true; };

            return resultado;
        }

    }
}

