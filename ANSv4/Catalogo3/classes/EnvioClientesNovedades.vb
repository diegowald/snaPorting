Option Explicit On

Public Class EnvioClientesNovedades

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioClientesNovedades"

    Private m_MacAddress As String
    Private m_ipAddress As String
    Private Cliente As WSClientesNovedades.ClientesNovedadesSoapClient
    Private WebServiceInicializado As Boolean

    'Public Event SincronizarClientesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
    'Public Event SincronizarClientesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Public Sub Inicializar(ByVal MacAddress As String, _
                           ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String)

        Dim Conectado As Boolean = Conectado = My.Computer.Network.Ping(ipAddress, 5000)

        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo errhandler

        If Not WebServiceInicializado Then
            If Conectado Then
                Cliente = New WSClientesNovedades.ClientesNovedadesSoapClient("", "http://" & ipAddress & "/wsCatalogo3/ClientesNovedades.asmx?wsdl")
                If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                    '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
                End If

                m_MacAddress = MacAddress
                m_ipAddress = ipAddress
                WebServiceInicializado = True
            Else
                WebServiceInicializado = False
            End If
        End If

        Exit Sub

errhandler:
        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New WSClientesNovedades.ClientesNovedadesSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/ClientesNovedades.asmx?wsdl")
            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                '                Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
            End If

            m_MacAddress = MacAddress
            WebServiceInicializado = True
            m_ipAddress = ipAddressIntranet
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub

    Public Function EnviarNovedadesEnBloques(ByRef aFechas() As String, ByRef aNovedades() As String, ByRef aClientes() As String) As Boolean

        Dim sFechas As String
        Dim sNovedades As String
        Dim sClientes As String

        sFechas = ""
        sNovedades = ""
        sClientes = ""

        Dim I As Long
        For I = LBound(aFechas) To UBound(aFechas)
            sFechas = sFechas & aFechas(I) & ";"
            sNovedades = sNovedades & aNovedades(I) & ";"
            sClientes = sClientes & aClientes(I) & ";"
        Next I

        If Len(sFechas) > 0 Then
            sFechas = Left(sFechas, Len(sFechas) - 1)
            sNovedades = Left(sNovedades, Len(sNovedades) - 1)
            sClientes = Left(sClientes, Len(sClientes) - 1)

            Dim resultado As Long
            resultado = Cliente.ClientesNovedades(m_MacAddress, sFechas, sNovedades, sClientes)

            EnviarNovedadesEnBloques = (resultado = 0)
        Else
            EnviarNovedadesEnBloques = True
        End If

    End Function

End Class
