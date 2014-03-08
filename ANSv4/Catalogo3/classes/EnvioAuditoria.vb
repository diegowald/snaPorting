Option Explicit On

Public Class EnvioAuditoria

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsAuditoria"

    Private m_MacAddress As String
    Private m_ipAddress As String
    Private Cliente As WSAudit.audit_v_304SoapClient
    Private WebServiceInicializado As Boolean

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
                Cliente = New WSAudit.audit_v_304SoapClient("", "http://" & ipAddress & "/wsCatalogo3/audit_v_304.asmx?wsdl")
                If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                    '    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
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
            Cliente = New WSAudit.audit_v_304SoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/audit_v_304.asmx?wsdl")
            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
            End If

            m_MacAddress = MacAddress
            WebServiceInicializado = True
            m_ipAddress = ipAddressIntranet
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub

    Public Function EnviarAuditoriaEnBloques(ByRef Fecha() As String, ByRef Descripcion() As String, ByRef AuditIDs() As Long) As Boolean

        Dim Fechas As String
        Dim Descripciones As String
        Dim IDs As String

        Fechas = ""
        Descripciones = ""
        IDs = ""

        Dim I As Long
        For I = LBound(Fecha) To UBound(Fecha)
            Fechas = Fechas & Fecha(I) & ";"
            Descripciones = Descripciones & Descripcion(I) & ";"
            IDs = IDs & Format(AuditIDs(I), "00000") & ";"
        Next I

        If Len(Fechas) > 0 Then
            Fechas = Left(Fechas, Len(Fechas) - 1)
            Descripciones = Left(Descripciones, Len(Descripciones) - 1)
            IDs = Left(IDs, Len(IDs) - 1)

            Dim resultado As Long
            resultado = Cliente.AuditInBlock304(m_MacAddress, Fechas, Descripciones, IDs)

            EnviarAuditoriaEnBloques = (resultado = 0)
        Else
            EnviarAuditoriaEnBloques = True
        End If

    End Function


End Class
