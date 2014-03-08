Option Explicit On

Public Class VerExistencia


    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsVerExistencia"

    'Public Event SincronizarActivarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

    Private Cliente As WSVerExistencia.VerExistenciaSoapClient
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ipAddress As String

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Public Sub Inicializar(ByVal MacAddress As String, _
                           ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String)

        Dim Conectado As Boolean

        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo errhandler
        If Not WebServiceInicializado Then
            If Conectado Then
                Cliente = New WSVerExistencia.VerExistenciaSoapClient("", "http://" & ipAddress & "/wsOracle/VerExistencia.asmx?wsdl")
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
            Cliente = New WSVerExistencia.VerExistenciaSoapClient("", "http://" & ipAddressIntranet & "/wsOracle/VerExistencia.asmx?wsdl")
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

    Public Sub ExistenciaSemaforo(ByVal pIdProducto As String, ByRef pSemaforo As String)

        Dim Cancel As Boolean

        Cancel = False

        If Not WebServiceInicializado Then
            Cancel = True
        End If

        If Not Cancel Then
            pSemaforo = ObtenerSemaforo(Cancel, vg.NroUsuario, pIdProducto)
        End If

    End Sub

    Private Function ObtenerSemaforo(ByRef Cancel As Boolean, ByVal IDAns As String, ByVal IdProducto As String) As String

        ObtenerSemaforo = "x"

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
        Else
            ObtenerSemaforo = Cliente.ObtenerExistencia(m_MacAddress, IDAns, IdProducto)
        End If

    End Function



End Class
