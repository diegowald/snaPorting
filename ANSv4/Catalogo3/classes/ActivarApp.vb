Option Explicit On

Public Class ActivarApp

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsActivarApp"

    Public Event SincronizarActivarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

    Private Cliente As WSLlaveCliente.LLaveClienteSoapClient
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
                Cliente = New WSLlaveCliente.LLaveClienteSoapClient("", "http://" & ipAddress & "/wsCatalogo3/LLaveCliente.asmx?wsdl")
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
            Cliente = New WSLlaveCliente.LLaveClienteSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/LLaveCliente.asmx?wsdl")
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

    'Private Function ObtenerLLaveViajante(ByVal ZonaViajante As String) As String

    '    ObtenerLLaveViajante = vbNullString

    '    If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
    '        ' Conexion no valida
    '        'Cancel = True
    '    Else
    '        Dim s As String
    '        s = Cliente.ObtenerLLaveViajante(ZonaViajante)

    '        If Len(Trim(s)) > 0 Then
    '            ObtenerLLaveViajante = ZonaViajante & vg.NroUsuario & vg.Cuit & ObtenerCRC(s & vg.IDMaquinaCRC)
    '        End If

    '    End If

    'End Function

    'Public Sub Activar()

    '    Dim Cancel As Boolean
    '    Dim Estado As Integer
    '    Dim sResultado As String

    '    sResultado = vbNullString

    '    Cancel = False

    '    RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)
    '    If Not WebServiceInicializado Then
    '        Cancel = True
    '    End If
    '    Application.DoEvents()

    '    If Not Cancel Then
    '        RaiseEvent SincronizarActivarAppProgress("Verificando Estado Viajante/Cliente", 40, Cancel)
    '    End If
    '    Application.DoEvents()

    '    If Not Cancel Then

    '        Dim wZonaViajante As String
    '        Dim wLlaveViajante As String

    '        'zzzzz iiiii ccccccccccc
    '        '12345 67890 12345678901

    '        wZonaViajante = Mid(vg.LLaveViajante, 1, 5)
    '        vg.NroUsuario = Mid(vg.LLaveViajante, 6, 5)
    '        vg.Cuit = Mid(vg.LLaveViajante, 11, 11)

    '        wLlaveViajante = ObtenerLLaveViajante(wZonaViajante)

    '        If ValidateLLaveViajante(wLlaveViajante) Then

    '            Estado = ObtenerEstado(Cancel, vg.Cuit, vg.NroUsuario, wZonaViajante, vg.IDMaquina)

    '            Select Case Estado
    '                Case 1
    '                    'El Cliente NO está Activo
    '                    sResultado = "ERROR: El Cliente NO está Activo"
    '                Case 2
    '                    'No Existe la Tupla Viajante Cliente
    '                    sResultado = "ERROR: Cuit, Nº de Cuenta, y Zona de Viajante, No Coinciden"
    '                Case 3
    '                    'Esa PC ya está Registrada Comunicarse con auto náutica sur
    '                    sResultado = "ERROR: Esa PC ya está Registrada, Comunicarse con auto náutica sur"
    '                Case 4
    '                    'El viajante NO esta Autorizado a registrar
    '                    sResultado = "ERROR: El viajante NO esta Autorizado a registrar el catálogo"
    '                Case 5
    '                    'Usado en el web service para verificar si los datos enviados estaban OK
    '                Case 6
    '                    sResultado = "(6). El Cliente Existe en la tabla de Usuarios, Comunicarse con auto náutica sur"
    '                Case 7
    '                    sResultado = "(7). Fallo en tabla de Usuarios Habilitados, Comunicarse con auto náutica sur"
    '                Case 8
    '                    sResultado = "(8). Error, Comunicarse con auto náutica sur"
    '                Case 9
    '                    'Por Aca Bien
    '                    'sResultado = "Registro Exitoso, copie los datos obtenidos en la PC del Cliente"


    '                    DeleteKeyINI("DATOS", "LLaveViajante")

    '                    vg.IDMaquinaREG = ObtenerCRC(vg.IDMaquina & vg.IDMaquinaCRC)

    '                    WriteINI("DATOS", "RegistrationKey", vg.IDMaquinaREG)

    '                    vg.AppActiva = True

    '                    sResultado = "¡Aplicación ACTIVADA CON EXITO!"

    '                Case Else
    '                    'Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados (0 ó -1)
    '                    sResultado = "Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados"
    '            End Select
    '        Else
    '            DeleteKeyINI("DATOS", "LLaveViajante")
    '            sResultado = "Error en la llave ingresada por el viajante"
    '        End If

    '    End If

    '    If sResultado <> vbNullString Then
    '        MsgBox(sResultado, vbInformation, "Resultado Activación")
    '    End If

    'End Sub

    Private Function ObtenerEstado(ByRef Cancel As Boolean, ByVal Cuit As String, ByVal IDAns As String, ByVal IDViajante As String, ByVal LLaveCliente As String) As Integer

        ObtenerEstado = -1

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
        Else
            ObtenerEstado = Cliente.ObtenerLLaveCatalogo(m_MacAddress, Cuit, IDAns, IDViajante)
        End If

    End Function

    Public Sub EstadoActual()

        Dim Cancel As Boolean
        Dim Estado As Byte
        Dim sResultado As String

        sResultado = vbNullString

        Cancel = False

        RaiseEvent SincronizarActivarAppProgress("Iniciando Servicio Web", 0, Cancel)
        If Not WebServiceInicializado Then
            Cancel = True
        End If
        Application.DoEvents()

        If Not Cancel Then
            RaiseEvent SincronizarActivarAppProgress("Estado Actual del Catálogo", 40, Cancel)
        End If
        Application.DoEvents()

        If Not Cancel Then

            Estado = ObtenerEstadoActual(Cancel, vg.Cuit, vg.NroUsuario)

            Select Case Estado
                Case 1
                    sResultado = "ERROR: El Cliente NO está Activo"
                Case 2
                    sResultado = "ERROR: Esa PC está Registrada con otro Cuit y Nº de Cuenta"
                Case 3
                    sResultado = "ERROR: Esa PC NO está Registrada, Comunicarse con auto náutica sur"
                Case 4
                    sResultado = "El estado de registro del catálogo está correcto"
                Case Else
                    sResultado = "Error de Comunicacion ó Los datos de Entrada/Salida NO son los esperados"
            End Select

            If Not Cancel Then
                RaiseEvent SincronizarActivarAppProgress("Un momento por favor...", 70, Cancel)
            End If
            Application.DoEvents()

        End If

        If sResultado <> vbNullString Then

            RaiseEvent SincronizarActivarAppProgress(sResultado, 100, Cancel)
            Application.DoEvents()
            MsgBox(sResultado & vbCrLf & vbCrLf & "ID: " & m_MacAddress, vbInformation, "Estado Actual del Catálogo")

        End If

    End Sub

    Private Function ObtenerEstadoActual(ByRef Cancel As Boolean, ByVal Cuit As String, ByVal IDAns As String) As Byte

        ObtenerEstadoActual = 0

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
        Else
            ObtenerEstadoActual = Cliente.ObtenerEstadoActual(m_MacAddress, Cuit, IDAns)
        End If

    End Function

    Public Sub ListaPrecio()

        Dim wListaPrecio As Byte
        Dim Cancel As Boolean

        Cancel = False

        If Not WebServiceInicializado Then
            Cancel = True
        End If

        If Not Cancel Then

            wListaPrecio = ObtenerListaPrecio(Cancel, vg.Cuit, vg.NroUsuario)

            If Not Cancel Then

                If wListaPrecio > 0 Then
                    vg.ListaPrecio = wListaPrecio
                    '                    adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET ListaPrecio=" & CStr(vg.ListaPrecio))
                End If

            End If

        End If

    End Sub

    Private Function ObtenerListaPrecio(ByRef Cancel As Boolean, ByVal Cuit As String, ByVal IDAns As String) As Byte

        ObtenerListaPrecio = 0

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
        Else
            ObtenerListaPrecio = Cliente.ObtenerListaPrecio(m_MacAddress, Cuit, IDAns)
        End If

    End Function



End Class
