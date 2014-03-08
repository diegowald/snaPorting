Option Explicit On


Public Class UpdateClientes

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsUpdateClientes"

    Private Cliente As WSUpdateClientes.UpdateClientesSoapClient
    Private WebServiceInicializado As Boolean
    Public Event SincronizarClientesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
    Public Event SincronizarClientesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
    Private m_MacAddress As String
    Private m_ipAddress As String

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Private Sub Inicializar(ByVal MacAddress As String, _
                           ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True

        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo errhandler
        If Not WebServiceInicializado Then
            If Conectado Then
                Cliente = New WSUpdateClientes.UpdateClientesSoapClient("", "http://" & ipAddress & "/wsCatalogo3/UpdateClientes.asmx?wsdl")
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
            Cliente = New WSUpdateClientes.UpdateClientesSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/UpdateClientes.asmx?wsdl")
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

    Private Sub SincroClientesCompletada(ByRef Cancel As Boolean)

        On Error GoTo ErrorHandler

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
            Exit Sub
        End If

        Cliente.SincronizacionClientesCompletada(m_MacAddress)
        Cliente.SincronizacionCtasCtesCompletada(m_MacAddress)

        Exit Sub

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public Sub SincronizarClientes()

        On Error GoTo ErrorHandler

        Dim Cancel As Boolean
        Cancel = False

        If vg.TranActiva Is Nothing Then
            vg.TranActiva = vg.Conexion.BeginTransaction
        End If

        RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 0, Cancel)

        If Not WebServiceInicializado Then
            Cancel = True
        End If

        If Not Cancel Then
            RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 30, Cancel)
        End If

        '        adoModulo.adoComandoIU(vg.Conexion, "DELETE FROM tblClientes")

        If Not Cancel Then
            '            SincronizarTodosLosClientes(Cancel)
        End If

        If Not Cancel Then
            '           RaiseEvent SincronizarClientesProgress("Sincronizando Cuentas Corrientes", 60, Cancel)
        End If

        If Not Cancel Then
            '          SincronizarTodasLasCtasCtes(Cancel)
        End If

        If Not Cancel Then
            RaiseEvent SincronizarClientesProgress("Finalizando Sincronización de Clientes", 90, Cancel)
        End If

        If Not Cancel Then
            SincroClientesCompletada(Cancel)
        End If

        If Cancel Then
            If Not (vg.TranActiva Is Nothing) Then
                vg.TranActiva.Rollback()
                vg.TranActiva = Nothing
            End If
            RaiseEvent SincronizarClientesProgress("Sincronización de Clientes con Errores", 100, Cancel)
            Application.DoEvents()
        Else
            If Not (vg.TranActiva Is Nothing) Then
                vg.TranActiva.Commit()
                vg.TranActiva = Nothing
            End If
            '            adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_appConfig_FActClientes_Upd")
            RaiseEvent SincronizarClientesProgress("Sincronización de Clientes Finalizada", 100, Cancel)
            Application.DoEvents()
        End If

        Exit Sub

ErrorHandler:
        If Not (vg.TranActiva Is Nothing) Then
            vg.TranActiva.Rollback()
            vg.TranActiva = Nothing
        End If

        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Private Sub Clientes_Add(ByRef Conexion As System.Data.OleDb.OleDbConnection, _
                              ByVal ID As Integer, _
                              ByVal RazonSocial As String, _
                              ByVal Cuit As String, _
                              ByVal Email As String, _
                              ByVal IDViajante As Integer, _
                              ByVal Domicilio As String, _
                              ByVal Ciudad As String, _
                              ByVal Telefono As String, _
                              ByVal Observaciones As String, _
                              ByVal Activo As Byte, _
                              ByVal F_Actualizacion As Date, _
                              ByVal Cascara As String)

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pID", SqlDbType.Int).Value = ID
        cmd.Parameters.Add("pRazonSocial", SqlDbType.VarChar, 40).Value = RazonSocial
        cmd.Parameters.Add("pCuit", SqlDbType.VarChar, 13).Value = Cuit
        cmd.Parameters.Add("pEmail", SqlDbType.VarChar, 40).Value = Email
        cmd.Parameters.Add("pIDViajante", SqlDbType.Int).Value = IDViajante
        cmd.Parameters.Add("pDomicilio", SqlDbType.VarChar, 40).Value = Domicilio
        cmd.Parameters.Add("pCiudad", SqlDbType.VarChar, 40).Value = Ciudad
        cmd.Parameters.Add("pTelefono", SqlDbType.VarChar, 40).Value = Telefono
        cmd.Parameters.Add("pObservaciones", SqlDbType.VarChar, 200).Value = Observaciones
        cmd.Parameters.Add("pActivo", SqlDbType.TinyInt).Value = Activo
        cmd.Parameters.Add("pCascara", SqlDbType.VarChar, 3).Value = Cascara
        cmd.Parameters.Add("pF_Actualizacion", SqlDbType.Date).Value = F_Actualizacion

        cmd.Connection = Conexion
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Clientes_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        If Err.Number = -2147467259 Then
            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
            ' El error dice así:
            ' Los cambios solicitados en la tabla no se realizaron correctamente
            '  porque crearían valores duplicados en el índice, clave principal o relación.
            ' Cambie los datos en el campo o los campos que contienen datos duplicados,
            ' quite el índice o vuelva a definir el índice para permitir entradas duplicadas e inténtelo de nuevo.
            'diego            adoModulo.adoComandoIU(vg.Conexion, "DELETE FROM tblClientes WHERE ID = " & CStr(ID))
            Err.Clear()
            Resume
        End If

        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Private Sub CtaCte_Add(ByRef Conexion As System.Data.OleDb.OleDbConnection, _
                              ByVal IdCliente As Integer, _
                              ByVal F_Comprobante As Date, _
                              ByVal T_Comprobante As String, _
                              ByVal N_Comprobante As String, _
                              ByVal Det_Comprobante As String, _
                              ByVal Importe As Single, _
                              ByVal Saldo As Single, _
                              ByVal ImpOferta As Single, _
                              ByVal TextoOferta As String, _
                              ByVal Vencida As Byte, _
                              ByVal ImpPercep As Single, _
                              ByVal EsContado As Byte)

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pIDcliente", SqlDbType.Int).Value = IdCliente
        cmd.Parameters.Add("pF_Comprobante", SqlDbType.Date).Value = F_Comprobante
        cmd.Parameters.Add("pT_Comprobante", SqlDbType.VarChar, 3).Value = T_Comprobante
        cmd.Parameters.Add("pN_Comprobante", SqlDbType.VarChar, 12).Value = N_Comprobante
        cmd.Parameters.Add("pDet_Comprobante", SqlDbType.VarChar, 100).Value = Det_Comprobante
        cmd.Parameters.Add("pImporte", SqlDbType.Float).Value = Importe
        cmd.Parameters.Add("pSaldo", SqlDbType.Float).Value = Saldo
        cmd.Parameters.Add("pImpOferta", SqlDbType.Float).Value = ImpOferta
        cmd.Parameters.Add("pTextoOferta", SqlDbType.VarChar, 100).Value = TextoOferta
        cmd.Parameters.Add("pVencida", SqlDbType.TinyInt).Value = Vencida
        cmd.Parameters.Add("pImpPercep", SqlDbType.Float).Value = ImpPercep
        cmd.Parameters.Add("pEsContado", SqlDbType.TinyInt).Value = EsContado

        cmd.Connection = Conexion
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_CtaCte_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    'diego    Private Sub SincronizarTodosLosClientes(ByRef Cancel As Boolean)

    'diego        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
    'diego    ' Conexion no valida
    'diego            Cancel = True
    'diego            Exit Sub
    'diego        End If

    'diego    'RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 40, Cancel)

    'diego        RaiseEvent SincronizarClientesProgresoParcial("Importando Mis Clientes", 0, Cancel)

    'diego        If Cancel Then
    'diego            Exit Sub
    'diego End If

    'diego    ' Obtengo la cantidad de modificaciones a importar
    'diego    Dim CantidadAImportar As Long
    'diego    Dim RestanImportar As Long
    'diego CantidadAImportar = Cliente.GetTodosLosClientes_Cantidad(m_MacAddress)
    'diego         RestanImportar = CantidadAImportar

    'diego     Dim LastID As Long
    'diego     Dim I As Long
    'diego  LastID = 0
    'diego  I = 0
    'diego         Do While RestanImportar > 0

    'diego     'RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", (CantidadAImportar - RestanImportar) / CantidadAImportar * 100, Cancel)

    'diego     Dim s As String
    'diego     Application.DoEvents()

    'diego     'Debug.Print "Inicio: " & Now;
    'diego      s = Cliente.GetTodosLosClientes_Datos(m_MacAddress, LastID)
    'diego     'Debug.Print " Fin: " & Now & " Restan: " & CStr(RestanImportar)

    'diego     'DoEvents
    'diego   Dim fs As New Scripting.FileSystemObject
    'diego     Dim ts As Scripting.TextStream

    'diego     Dim FileName As String
    'diego      FileName = tmpFileFolders.GetTempFileName

    'diego             ts = fs.CreateTextFile(FileName, True, True)
    'diego      ts.Write(s)
    'diego  ts.Close()

    'diego     Dim rs As ADODB.Recordset
    'diego      rs = New ADODB.Recordset
    'diego   rs.Open(FileName, "Provider=MSPersist;", adOpenStatic, adLockReadOnly)

    'diego             If rs.RecordCount > 0 Then
    'diego          RestanImportar = RestanImportar - rs.RecordCount
    'diego   rs.MoveLast() ' Can find little errors that can crop up.
    'diego                 rs.MoveFirst()

    'diego          On Error GoTo Proximo

    'diego                 Do Until rs.EOF
    'diego              Clientes_Add(vg.Conexion, CInt(rs!ID), _
    'diego                    CStr(rs!RazonSocial), CStr(rs!Cuit), _
    'diego             CStr(rs!Email), CInt(rs!IDViajante), _
    'diego      CStr(rs!Domicilio), CStr(rs!Ciudad), _
    'diego                             CStr(rs!Telefono), CStr(rs!Observaciones), _
    'diego                      CByte(rs!Activo), CDate(rs!F_ActCliente), CStr(rs!Cascara))

    'diego  I = I + 1
    'diego                     If I Mod 31 = 0 Then
    'diego                  RaiseEvent SincronizarClientesProgresoParcial("Importando Mis Clientes", I / CantidadAImportar * 100, Cancel)
    'diego           If Cancel Then
    'diego        Exit Sub
    'diego                         End If
    'diego                  DoEvents()
    'diego       End If

    'diego Proximo:
    'diego              LastID = CLng(rs!ID)
    'diego       rs.MoveNext()
    'diego       Loop
    'diego             End If
    'diego      On Error GoTo 0

    'diego             rs.Close()
    'diego      Kill(FileName)
    'diego         Loop


    'diego     End Sub

    '    Private Sub SincronizarTodasLasCtasCtes(ByRef Cancel As Boolean)

    '        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
    '            ' Conexion no valida
    '            Cancel = True
    '            Exit Sub
    '        End If

    '        'RaiseEvent SincronizarClientesProgress("Sincronizando de Clientes ...", 60, Cancel)

    '        RaiseEvent SincronizarClientesProgresoParcial("Importando Cuentas Corrientes", 0, Cancel)

    '        If Cancel Then
    '            Exit Sub
    '        End If

    '        ' Obtengo la cantidad de modificaciones a importar
    '        Dim CantidadAImportar As Long
    '        Dim RestanImportar As Long
    '        CantidadAImportar = Cliente.GetTodasLasCtasCtes_Cantidad(m_MacAddress)
    '        RestanImportar = CantidadAImportar

    '        Dim LastID As Long
    '        Dim I As Long
    '        LastID = 0
    '        I = 0
    '        Do While RestanImportar > 0

    '            ' RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", (CantidadAImportar - RestanImportar) / CantidadAImportar * 100, Cancel)

    '            Dim s As String
    '            Application.DoEvents()

    '            'Debug.Print "Inicio: " & Now;
    '            s = Cliente.GetTodasLasCtasCtes_Datos(m_MacAddress, LastID)
    '            'Debug.Print " Fin: " & Now & " Restan: " & CStr(RestanImportar)

    '            'DoEvents
    '            Dim fs As New Scripting.FileSystemObject
    '            Dim ts As Scripting.TextStream

    '            Dim FileName As String
    '            FileName = tmpFileFolders.GetTempFileName

    '            ts = fs.CreateTextFile(FileName, True, True)
    '            ts.Write(s)
    '            ts.Close()

    '            Dim rs As ADODB.Recordset
    '            rs = New ADODB.Recordset
    '            rs.Open(FileName, "Provider=MSPersist;", adOpenStatic, adLockReadOnly)

    '            If rs.RecordCount > 0 Then
    '                RestanImportar = RestanImportar - rs.RecordCount
    '                rs.MoveLast() ' Can find little errors that can crop up.
    '                rs.MoveFirst()

    '                On Error GoTo Proximo

    '                Do Until rs.EOF
    '                    CtaCte_Add(vg.Conexion, _
    '                         CInt(rs!IdCliente), _
    '                         CDate(rs!F_Comprobante), _
    '                         CStr(rs!T_Comprobante), _
    '                         CStr(rs!N_Comprobante), _
    '                         CStr(rs!Det_Comprobante), _
    '                         CSng(rs!Importe) / 100, _
    '                         CSng(rs!Saldo) / 100, _
    '                         CSng(rs!ImpOferta) / 100, _
    '                         CStr(rs!TextoOferta), _
    '                         CStr(rs!Vencida), _
    '                         CSng(rs!ImpPercep) / 100, _
    '                         CByte("0" & rs!EsContado))

    '                    I = I + 1
    '                    If I Mod 31 = 0 Then
    '                        RaiseEvent SincronizarClientesProgresoParcial("Importando Cuentas Corrientes", I / CantidadAImportar * 100, Cancel)
    '                        If Cancel Then
    '                            Exit Sub
    '                        End If
    '                        DoEvents()
    '                    End If

    'Proximo:
    '                    LastID = CLng(rs!ID)
    '                    rs.MoveNext()
    '                Loop
    '            End If
    '            On Error GoTo 0

    '            rs.Close()
    '            Kill(FileName)
    '        Loop

    '    End Sub


End Class
