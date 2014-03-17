Option Explicit On

Public Class EnvioDevolucion

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioDevolucion"



    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.
    Private Cliente As DevolucionWS.Devolucion
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ip As String

    Private m_NroDevolucion As String
    Private m_CodCliente As String
    Private m_Fecha As String
    Private m_Observaciones As String
    Private m_Detalle As String

    Private DatosObtenidos As Boolean

    Public Sub New(ByVal conexion As System.Data.OleDb.OleDbConnection, _
                   ByVal ipAddress As String, _
                   ByVal ipAddressIntranet As String, _
                   ByVal MacAddress As String, _
                   ByVal usaProxy As Boolean, _
                   ByVal proxyServerAddress As String)
        Inicializar(ipAddress, ipAddressIntranet, MacAddress,
                    usaProxy, proxyServerAddress)
        Conexion1 = conexion
    End Sub

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Private Conexion1 As OleDb.OleDbConnection

    Public Sub ObtenerDatos(ByVal NroDevolucion As String)

        DatosObtenidos = False

        Dim Enc As System.Data.OleDb.OleDbDataReader
        Dim Det As System.Data.OleDb.OleDbDataReader

        Enc = Funciones.adoModulo.adoComando(vg.Conexion, "EXECUTE v_Devolucion_Enc '" & NroDevolucion & "'")
        Det = Funciones.adoModulo.adoComando(vg.Conexion, "EXECUTE v_Devolucion_Det '" & NroDevolucion & "'")

        m_NroDevolucion = NroDevolucion
        m_CodCliente = Format(Trim(Enc("IDCliente")), "000000")
        m_Fecha = Enc("F_Devolucion")
        m_Observaciones = Replace(Enc("Observaciones"), ",", " ") & ""

        If Det.HasRows Then
            m_Detalle = ""
            Do While Det.Read
0:              m_Detalle = m_Detalle & Det("C_Producto") & ","                                '-00-
1:              m_Detalle = m_Detalle & Format(Trim(Det("Cantidad")), "00000000") & "00,"      '-01-
2:              m_Detalle = m_Detalle & "NO" & ","                                                '-02-
3:              m_Detalle = m_Detalle & "NO" & ","                                                '-03-
4:              m_Detalle = m_Detalle & "NO" & ","                                                '-04-
                '            'm_Detalle = m_Detalle & IIf(Len(Trim(rs_Det("miDeposito"))) = 0, "   ", rs_Det("miDeposito")) & ","                                '-05-
5:              m_Detalle = m_Detalle & Det("miDeposito") & ","
6:              m_Detalle = m_Detalle & Det("Factura") & ","                                   '-06-
7:              m_Detalle = m_Detalle & Format(Det("TipoDev"), "00") & ","                     '-07-
8:              m_Detalle = m_Detalle & Det("Vehiculo") & ","                                  '-08-
9:              m_Detalle = m_Detalle & Det("Modelo") & ","                                    '-09-
10:             m_Detalle = m_Detalle & Det("Motor") & ","                                     '-10-
11:             m_Detalle = m_Detalle & Det("Km") & ","                                        '-11-
12:             m_Detalle = m_Detalle & Det("Observaciones") & ";"                             '-12-
            Loop
        End If

        Enc = Nothing
        Det = Nothing

        DatosObtenidos = True

    End Sub

    Public Function EnviarDevolucion() As Long

        On Error GoTo ErrorHandler

        Dim Cancel As Boolean

        Cancel = False

        Dim resultado As Long

        If Not WebServiceInicializado Then
            Cancel = True
        End If

        If Not Cancel Then

            If Not (vg.TranActiva Is Nothing) Then
                vg.TranActiva.Rollback()
                vg.TranActiva = Nothing
            End If

            resultado = Cliente.EnviarDevolucion(m_MacAddress, m_NroDevolucion, m_CodCliente, m_Fecha, m_Observaciones, m_Detalle)

            If resultado = 0 Then
                '                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" & m_NroDevolucion & "'")
                If Not (vg.TranActiva Is Nothing) Then
                    vg.TranActiva.Commit()
                    vg.TranActiva = Nothing
                End If
            Else
                If Not (vg.TranActiva Is Nothing) Then
                    vg.TranActiva.Rollback()
                    vg.TranActiva = Nothing
                End If
            End If

            EnviarDevolucion = resultado
        Else
            EnviarDevolucion = -1
        End If

        Exit Function

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function

    Public Sub Inicializar(ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String, _
                           ByVal MacAddress As String, _
                           ByVal usaProxy As Boolean, _
                           ByVal proxyServerAddress As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo ErrorHandler

        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New DevolucionWS.Devolucion
                Cliente.Url = "http://" & ipAddress & "/wsCatalogo3/Devolucion.asmx?wsdl"
                If usaProxy Then
                    Cliente.Proxy = New System.Net.WebProxy(proxyServerAddress)
                End If

                m_MacAddress = MacAddress
                m_ip = ipAddress
                WebServiceInicializado = True
            End If
        Else
            WebServiceInicializado = False
        End If

        Exit Sub

ErrorHandler:

        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New DevolucionWS.Devolucion
            Cliente.Url = "http://" & ipAddressIntranet & "/wsCatalogo3/Devolucion.asmx?wsdl"
            If usaProxy Then
                Cliente.Proxy = New System.Net.WebProxy(proxyServerAddress)
            End If

            m_MacAddress = MacAddress
            m_ip = ipAddressIntranet
            WebServiceInicializado = True
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub

End Class
