Option Explicit On

Public Class Pedido

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsPedido"

    Private Conexion1 As System.Data.OleDb.OleDbConnection  ' diego    Private Conexion1 As OleDbConnection

    Private mvarNroPedido As String 'copia local
    Private mvarF_Pedido As Date 'copia local
    Private mvarIdCliente As Long 'copia local
    Private mvarNroImpresion As Byte 'copia local
    Private mvarObservaciones As String 'copia local
    Private mvarTransporte As String 'copia local

    Private DetallePedido As Collection

    Public Event GuardoOK(ByVal NroPedido As String)

    Public Property Transporte() As String
        Set(ByVal value As String)
            mvarTransporte = value
        End Set

        Get
            Return mvarTransporte
        End Get
    End Property

    Public Property Observaciones() As String
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
        Get
            Return mvarObservaciones
        End Get
    End Property

    Public ReadOnly Property CantidadItems() As Long
        Get
            Return DetallePedido.Count
        End Get
    End Property

    Public Sub Nuevo(Optional ByVal IDdeCliente As Long = 0)
        DetallePedido = Nothing
        DetallePedido = New Collection
        mvarNroPedido = 0
        mvarF_Pedido = Date.Today
        If IDdeCliente > 0 Then mvarIdCliente = IDdeCliente
    End Sub

    ' FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
    Public Function Renglon(ByVal Numero As Integer) As PedidoItem
        Return DetallePedido("_" & Numero)
    End Function

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    Public Property NroImpresion() As Byte
        Set(ByVal value As Byte)
            mvarNroImpresion = value
        End Set
        Get
            Return mvarNroImpresion
        End Get
    End Property

    Public ReadOnly Property IdCliente() As Long
        Get
            Return mvarIdCliente
        End Get
    End Property

    Public ReadOnly Property F_Pedido() As Date
        Get
            Return mvarF_Pedido
        End Get
    End Property

    Public ReadOnly Property NroPedido() As String
        Get
            Return mvarNroPedido
        End Get
    End Property

    'diego     Private Sub Class_Terminate()
    'diego         DetallePedido = Nothing
    'diego     End Sub

    Private Function ValidarConexion() As Boolean
        Return Not (Conexion1 Is Nothing)
    End Function

    Public Sub Guardar(Optional ByVal Origen As String = vbNullString)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim cmd As New System.Data.OleDb.OleDbCommand

        If DetallePedido.Count < 1 Then Exit Sub

        If UCase(Origen) = "VER" Then
            Funciones.adoModulo.adoComandoIU(Conexion1, "DELETE FROM tblPedido_Enc WHERE NroPedido='09999-99999999'")
            mvarNroPedido = "09999-99999999"
        Else

            If Len(Trim(vg.NroPDR)) > 0 Then
                Funciones.adoModulo.adoComandoIU(Conexion1, "DELETE FROM tblPedido_Enc WHERE NroPedido='" & vg.NroPDR & "'")
                vg.NroPDR = ""
            End If

            rec = Funciones.adoModulo.adoComando(Conexion1, "SELECT TOP 1 right(NroPedido,8) AS NroPedido FROM tblPedido_Enc WHERE left(NroPedido,5)=" & vg.NroUsuario & " ORDER BY NroPedido DESC")
            If Not rec.HasRows Then
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroPedido = Trim(vg.NroUsuario) & "-C0000001"
                Else
                    mvarNroPedido = Trim(vg.NroUsuario) & "-00000001"
                End If
            Else
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroPedido = Trim(vg.NroUsuario) & "-C" & Format(CLng(Right(rec("NroPedido"), 7)) + 1, "0000000")
                Else
                    mvarNroPedido = Trim(vg.NroUsuario) & "-" & Format(CLng(Right(rec("NroPedido"), 7)) + 1, "00000000")
                End If
            End If
            rec = Nothing

        End If

        cmd.Parameters.Add("pNroPedido", SqlDbType.VarChar, 14).Value = mvarNroPedido
        cmd.Parameters.Add("pF_Pedido", SqlDbType.Date).Value = mvarF_Pedido
        cmd.Parameters.Add("pIdCliente", SqlDbType.Int).Value = mvarIdCliente
        cmd.Parameters.Add("pNroImpresion", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("pObservaciones", SqlDbType.VarChar, 128).Value = mvarObservaciones
        cmd.Parameters.Add("pTransporte", SqlDbType.VarChar, 128).Value = mvarTransporte

        cmd.Connection = Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Pedido_Enc_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing

        ' Ahora GUARDO los ITEMS de ESTE pedido

        Dim I As Integer
        Dim wTotal As Double
        wTotal = 0

        For I = 1 To DetallePedido.Count
            GuardarItem(Renglon(I).IDCatalogo, _
                        Renglon(I).cantidad, _
                        Renglon(I).Similar, _
                        Renglon(I).Bahia, _
                        Renglon(I).Oferta, _
                        Renglon(I).Deposito, _
                        Renglon(I).Precio, _
                        Renglon(I).Observaciones)
            wTotal = wTotal + (Renglon(I).cantidad * Renglon(I).Precio)
        Next I

        vg.NroImprimir = mvarNroPedido

        ' Por POLITICA NUESTRA  -- >  SE BORRA
        If UCase(Origen) = "VER" Then
            ' Actualizo Fecha de Transmicion para que no lo mande
            'diego             adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Pedido_Transmicion_Upd '" & mvarNroPedido & "'")
        Else
            vg.auditor.Guardar(ObjetosAuditados.Pedido, AccionesAuditadas.EXITOSO, "cli:" & Format(mvarIdCliente, "000000") & " ped:" & mvarNroPedido & " tot:" & Format(wTotal, "fixed"))
            Nuevo()
            RaiseEvent GuardoOK(vg.NroImprimir)
        End If

        Exit Sub

ErrorHandler:

        'diego         adoREC = Nothing
        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public Sub ADDItem(ByVal IDCatalogo As String, _
                       ByVal cantidad As Integer, _
                       ByVal Similar As Boolean, _
                       ByVal Bahia As Boolean, _
                       ByVal Oferta As Boolean, _
                       ByVal Deposito As Byte, _
                       ByVal Precio As Single, _
                       ByVal Observaciones As String)

        'OJO chkEsOfertaBahia===Bahia

        Dim mvarItem As New PedidoItem

        mvarItem.IDCatalogo = IDCatalogo
        mvarItem.cantidad = cantidad
        mvarItem.Similar = Similar
        mvarItem.Bahia = Bahia
        mvarItem.Oferta = Oferta
        mvarItem.Deposito = Deposito
        mvarItem.Precio = Precio
        mvarItem.Observaciones = Observaciones

        DetallePedido.Add(mvarItem, "_" & DetallePedido.Count + 1)

        mvarItem = Nothing

    End Sub

    Private Sub GuardarItem(ByVal IDCatalogo As String, _
                            ByVal cantidad As Integer, _
                            ByVal Similar As Boolean, _
                            ByVal Bahia As Boolean, _
                            ByVal Oferta As Boolean, _
                            ByVal Deposito As Byte, _
                            ByVal Precio As Single, _
                            ByVal Observaciones As String)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pNroPedido", SqlDbType.VarChar, 14).Value = mvarNroPedido
        cmd.Parameters.Add("pIdCatalogo", SqlDbType.VarChar, 38).Value = IDCatalogo
        cmd.Parameters.Add("pCantidad", SqlDbType.Int).Value = cantidad
        cmd.Parameters.Add("pSimilar", SqlDbType.Bit).Value = Similar
        cmd.Parameters.Add("pOferta", SqlDbType.Bit).Value = Oferta
        cmd.Parameters.Add("pBahia", SqlDbType.Bit).Value = Bahia
        cmd.Parameters.Add("pDeposito", SqlDbType.TinyInt).Value = Deposito
        cmd.Parameters.Add("pPrecio", SqlDbType.Float).Value = Precio
        cmd.Parameters.Add("pObservaciones", SqlDbType.VarChar, 80).Value = Observaciones

        cmd.Connection = Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Pedido_Det_add"
        cmd.ExecuteNonQuery()

        rec = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT ID FROM CatalogoBAK WHERE ID='" & IDCatalogo & "'")
        If Not rec.HasRows Then
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_AnexaItemCatalogoBak '" & IDCatalogo & "'")
        Else
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_CambiaCodigoCatalogoBak '" & IDCatalogo & "'")
        End If
        rec = Nothing
        cmd = Nothing
        Exit Sub

ErrorHandler:

        rec = Nothing
        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub


End Class
