Option Explicit On

Public Class Devolucion

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsDevolucion"

    Private Conexion1 As System.Data.OleDb.OleDbConnection '    Private Conexion As OleDbConnection

    Private mvarNroDevolucion As String 'copia local
    Private mvarF_Devolucion As Date 'copia local
    Private mvarIdCliente As Long 'copia local
    Private mvarNroImpresion As Byte 'copia local
    Private mvarObservaciones As String 'copia local

    Private DetalleDevolucion As Collection

    Public Event GuardoOK(ByVal NroDevolucion As String)

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
            Return DetalleDevolucion.Count
        End Get
    End Property

    Public Sub Nuevo(Optional ByVal IDdeCliente As Long = 0)
        DetalleDevolucion = Nothing
        DetalleDevolucion = New Collection
        mvarNroDevolucion = 0
        mvarF_Devolucion = Date.Today
        If IDdeCliente > 0 Then mvarIdCliente = IDdeCliente
    End Sub

    ' FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
    Public Function Renglon(ByVal Numero As Integer) As DevolucionItem
        Return DetalleDevolucion("_" & Numero)
    End Function

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    Public Property NroImpresion() As Byte
        Get
            Return mvarNroImpresion
        End Get
        Set(ByVal value As Byte)
            mvarNroImpresion = value
        End Set
    End Property

    Public ReadOnly Property IdCliente() As Long
        Get
            Return mvarIdCliente
        End Get
    End Property

    Public ReadOnly Property F_Devolucion() As Date
        Get
            Return mvarF_Devolucion
        End Get
    End Property

    Public ReadOnly Property NroDevolucion() As String
        Get
            Return mvarNroDevolucion
        End Get
    End Property

    Private Function ValidarConexion() As Boolean
        Return Not (Conexion1 Is Nothing)
    End Function

    Public Sub Guardar(Optional ByVal Origen As String = vbNullString)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim adoCMD As New System.Data.OleDb.OleDbCommand

        If DetalleDevolucion.Count < 1 Then Exit Sub

        If UCase(Origen) = "VER" Then
            Funciones.adoModulo.adoComandoIU(Conexion1, "DELETE FROM tblDevolucion_Enc WHERE NroDevolucion='09999-99999999'")
            mvarNroDevolucion = "09999-99999999"
        Else
            rec = Funciones.adoModulo.adoComando(Conexion1, "SELECT TOP 1 right(NroDevolucion,8) AS NroDevolucion FROM tblDevolucion_Enc WHERE left(NroDevolucion,5)=" & vg.NroUsuario & " ORDER BY NroDevolucion DESC")
            If Not rec.HasRows Then
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroDevolucion = Trim(vg.NroUsuario) & "-C0000001"
                Else
                    mvarNroDevolucion = Trim(vg.NroUsuario) & "-00000001"
                End If
            Else
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroDevolucion = Trim(vg.NroUsuario) & "-C" & Format(CLng(Right(rec("NroDevolucion"), 7)) + 1, "0000000")
                Else
                    mvarNroDevolucion = Trim(vg.NroUsuario) & "-" & Format(CLng(Right(rec("NroDevolucion"), 7)) + 1, "00000000")
                End If
            End If
            rec = Nothing
        End If

        adoCMD.Parameters.Add("pNroDevolucion", SqlDbType.VarChar, 14).Value = mvarNroDevolucion
        adoCMD.Parameters.Add("pF_Devolucion", SqlDbType.Date).Value = mvarF_Devolucion
        adoCMD.Parameters.Add("pIdCliente", SqlDbType.Int).Value = mvarIdCliente
        adoCMD.Parameters.Add("pNroImpresion", SqlDbType.Int).Value = 0
        adoCMD.Parameters.Add("pObservaciones", SqlDbType.VarChar, 200).Value = mvarObservaciones

        adoCMD.Connection = Conexion1
        adoCMD.CommandType = CommandType.StoredProcedure
        adoCMD.CommandText = "usp_Devolucion_Enc_add"
        adoCMD.ExecuteNonQuery()

        adoCMD = Nothing

        ' Ahora GUARDO los ITEMS de ESTE Devolucion

        Dim I As Integer

        For I = 1 To DetalleDevolucion.Count
            GuardarItem(Renglon(I).IDCatalogo, _
                        Renglon(I).cantidad, _
                        Renglon(I).Deposito, _
                        Renglon(I).Factura, _
                        Renglon(I).TipoDev, _
                        Renglon(I).Vehiculo, _
                        Renglon(I).Modelo, _
                        Renglon(I).Motor, _
                        Renglon(I).KM, _
                        Renglon(I).Observaciones)
        Next I

        vg.NroImprimir = mvarNroDevolucion

        ' Por POLITICA NUESTRA  -- >  SE BORRA
        If UCase(Origen) = "VER" Then
            ' Actualizo Fecha de Transmicion para que no lo mande
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" & mvarNroDevolucion & "'")
        Else
            vg.auditor.Guardar(ObjetosAuditados.Devoluciones, AccionesAuditadas.EXITOSO, "cli:" & Format(mvarIdCliente, "000000") & " ped:" & mvarNroDevolucion & " tot:")
            Nuevo()
            RaiseEvent GuardoOK(vg.NroImprimir)
        End If

        Exit Sub

ErrorHandler:

        rec = Nothing
        adoCMD = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public Sub ADDItem(ByVal IDCatalogo As String, _
                       ByVal cantidad As Integer, _
                       ByVal Deposito As Byte, _
                       ByVal Factura As String, _
                       ByVal TipoDev As Byte, _
                       ByVal Vehiculo As String, _
                       ByVal Modelo As String, _
                       ByVal Motor As String, _
                       ByVal KM As String, _
                       ByVal Observaciones As String)

        Dim mvarItem As New DevolucionItem

        mvarItem.IDCatalogo = IDCatalogo
        mvarItem.cantidad = cantidad
        mvarItem.Deposito = Deposito
        mvarItem.Factura = Factura
        mvarItem.TipoDev = TipoDev
        mvarItem.Vehiculo = Vehiculo
        mvarItem.Modelo = Modelo
        mvarItem.Motor = Motor
        mvarItem.KM = KM
        mvarItem.Observaciones = Observaciones

        DetalleDevolucion.Add(mvarItem, "_" & DetalleDevolucion.Count + 1)

        mvarItem = Nothing

    End Sub

    Private Sub GuardarItem(ByVal IDCatalogo As String, _
                            ByVal cantidad As Integer, _
                            ByVal Deposito As Byte, _
                            ByVal Factura As String, _
                            ByVal TipoDev As Byte, _
                            ByVal Vehiculo As String, _
                            ByVal Modelo As String, _
                            ByVal Motor As String, _
                            ByVal KM As String, _
                            ByVal Observaciones As String)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim adoCMD As New System.Data.OleDb.OleDbCommand

        adoCMD.Parameters.Add("pNroDevolucion", SqlDbType.VarChar, 14).Value = mvarNroDevolucion
        adoCMD.Parameters.Add("pIdCatalogo", SqlDbType.VarChar, 38).Value = IDCatalogo
        adoCMD.Parameters.Add("pCantidad", SqlDbType.Int).Value = cantidad
        adoCMD.Parameters.Add("pDeposito", SqlDbType.TinyInt).Value = Deposito
        adoCMD.Parameters.Add("pFactura", SqlDbType.VarChar, 15).Value = Factura
        adoCMD.Parameters.Add("pTipoDev", SqlDbType.TinyInt).Value = TipoDev
        adoCMD.Parameters.Add("pVehiculo", SqlDbType.VarChar, 32).Value = Vehiculo
        adoCMD.Parameters.Add("pModelo", SqlDbType.VarChar, 32).Value = Modelo
        adoCMD.Parameters.Add("pMotor", SqlDbType.VarChar, 32).Value = Motor
        adoCMD.Parameters.Add("pKm", SqlDbType.VarChar, 10).Value = KM
        adoCMD.Parameters.Add("pObservaciones", SqlDbType.VarChar, 128).Value = Observaciones

        adoCMD.Connection = Conexion1
        adoCMD.CommandType = CommandType.StoredProcedure
        adoCMD.CommandText = "usp_Devolucion_Det_add"
        adoCMD.ExecuteNonQuery()

        rec = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT ID FROM CatalogoBAK WHERE ID='" & IDCatalogo & "'")
        If Not rec.HasRows Then
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_AnexaItemCatalogoBak '" & IDCatalogo & "'")
        Else
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_CambiaCodigoCatalogoBak '" & IDCatalogo & "'")
        End If

        rec = Nothing
        adoCMD = Nothing
        Exit Sub

ErrorHandler:

        rec = Nothing
        adoCMD = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

End Class
