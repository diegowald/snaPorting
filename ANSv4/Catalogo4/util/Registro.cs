using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class Registro
    {

        public struct WMIPair
        {
            public string first;
            public string second;
        };

        public static bool validateRegistration(string s)
        {
            try
            {
                if (s.Trim().Length == 0)
                {
                    return false;
                }
                else
                {
                    return s == obtenerCRC(Global01.IDMaquina + Global01.IDMaquinaCRC);
                }
            }
            catch
            {
                // Analizar esto!!!
                /*        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                            Case vbRetry
                                Resume
                            Case vbIgnore
                                Resume Next
                        End Select*/
                return false;
            }
        }



        // Modulo Para tomar y comprobar el IDMaquina
        public static string obtenerIDMaquina()
        {
            try
            {
                string[] varSerial = new string[4];
                WMIPair[] varObjectToID = new WMIPair[4];

                // HackerScan
                varObjectToID[0].first = "Win32_BIOS";
                varObjectToID[0].second = "SerialNumber";
                varObjectToID[1].first = "Win32_BaseBoard";
                varObjectToID[1].second = "SerialNumber";
                varObjectToID[2].first = "Win32_Processor";
                varObjectToID[2].second = "ProcessorId";
                varObjectToID[3].first = "Win32_OperatingSystem";
                varObjectToID[3].second = "SerialNumber";

                varSerial[0] = tomarInfoWMI(varObjectToID[0]);
                varSerial[1] = tomarInfoWMI(varObjectToID[1]);
                varSerial[2] = tomarInfoWMI(varObjectToID[2]);
                varSerial[3] = tomarInfoWMI(varObjectToID[3]);

                return Global01.miSABOR + obtenerCRC(varSerial[0] + varSerial[1] + varSerial[2] + varSerial[3]);
            }
            catch
            {
                /*       Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                           Case vbRetry
                               Resume
                           Case vbIgnore
                               Resume Next
                       End Select
                       '-------- ErrorGuardian End ----------
                       */
                return "";
            }
        }

        public static string tomarInfoWMI(WMIPair clave)
        {
            try
            {
                System.Management.ManagementObjectSearcher obj = new System.Management.ManagementObjectSearcher("Select " + clave.second + " from " + clave.first);
                var col = obj.Get();
                foreach (var mgn in col)
                {
                    return mgn[clave.second].ToString();
                }
                return "";
            }
            catch
            {
                /*
                        Select ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                            Case vbRetry
                                Resume
                            Case vbIgnore
                                Resume Next
                        End Select
                */
                return "";
            }
        }

        public static string obtenerCRC(string s)
        {
            try
            {
                System.Security.Cryptography.MD5 m = System.Security.Cryptography.MD5.Create();
                byte[] data = m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s));

                System.Text.StringBuilder sBuilder = new StringBuilder();
                foreach (var b in data)
                {
                    sBuilder.Append(b.ToString("x2"));
                }
                return sBuilder.ToString();
            }
            catch
            {
                /*        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                            Case vbRetry
                                Resume
                            Case vbIgnore
                                Resume Next
                        End Select
                        '-------- ErrorGuardian End ----------
                                */
                return "";
            }
        }


        public static bool validateMachineId(string s)
        {
            try
            {
                return s == obtenerCRC(Global01.IDMaquina);
            }
            catch
            {
                /*    Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                        Case vbRetry
                            Resume
                        Case vbIgnore
                            Resume Next
                    End Select*/
                return false;
            }
        }

        public static bool validateLlaveViajante(string s)
        {
            try
            {
                if (s.Trim().Length == 0)
                {
                    return false;
                }
                else
                {
                    return s == Global01.LLaveViajante;
                }
            }
            catch
            {
                /*Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                    Case vbRetry
                        Resume
                    Case vbIgnore
                        Resume Next
                End Select*/
                return false;
            }
        }

    }
}
