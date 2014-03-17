Option Explicit On

Public Class IPPrivado
    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsIPPrivado"

    Private Cliente As CatalogoWS.Info
    Private WebServiceInicializado As Boolean

    Private m_MacAddress As String

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Public Function GetIP() As String

        On Error GoTo AtajarError

        Dim s As String

        If Not WebServiceInicializado Then
            'Err.Raise 9999, , "Falta inicializar"
            GetIP = ""
            Exit Function
        End If
        s = Cliente.GetIp(m_MacAddress)
        GetIP = Trim(s)

        Exit Function

AtajarError:
        If Err.Number = -2147467259 Then
            Err.Clear()
            GetIP = ""
            Exit Function
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Function

    Public Function GetIpIntranet() As String

        On Error GoTo AtajarError

        Dim s As String

        If Not WebServiceInicializado Then
            'Err.Raise 9999, , "Falta inicializar"
            GetIpIntranet = ""
            Exit Function
        End If

        s = Cliente.GetIpIntranet(m_MacAddress)
        GetIpIntranet = Trim(s)

        Exit Function

AtajarError:
        If Err.Number = -2147467259 Then
            Err.Clear()
            GetIpIntranet = ""
            Exit Function
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Function

    Public Function GetIPCatalogo() As String

        On Error GoTo AtajarError

        Dim s As String

        If Not WebServiceInicializado Then
            'Err.Raise 9999, , "Falta Inicializar"
            GetIPCatalogo = ""
            Exit Function
        End If

        s = Cliente.GetIpCatalogo(m_MacAddress)
        GetIPCatalogo = Trim(s)

        Exit Function

AtajarError:
        If Err.Number = -2147467259 Then
            Err.Clear()
            GetIPCatalogo = ""
            Exit Function
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Function

    Public Sub New(ByVal ipAddress As String, ByVal MacAddress As String, ByVal usaProxy As Boolean, _
                   ByVal URLProxyServer As String)
        On Error GoTo ErrorHandler

        Dim Conectado As Boolean

        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True
        If Not WebServiceInicializado Then
            If Conectado Then
                'Cliente = New WSCatalogo.InfoSoapClient("", "http://" & ipAddress & "/Catalogo/Info.asmx?wsdl")
                Cliente = New CatalogoWS.Info()
                ' DIEGO -> Implementar proxy!
                If usaProxy Then
                    Cliente.Proxy = New System.Net.WebProxy(URLProxyServer)
                End If

                WebServiceInicializado = True
                m_MacAddress = MacAddress
            Else
                WebServiceInicializado = False
            End If
        End If

        Exit Sub

ErrorHandler:

        If Err.Number = -2147024809 Then
            ' Este error se da debido
            WebServiceInicializado = False
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub


End Class
