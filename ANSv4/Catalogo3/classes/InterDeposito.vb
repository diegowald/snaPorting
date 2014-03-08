Option Explicit On

Public Class InterDeposito

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsInterDeposito"

    Private Conexion1 As System.Data.OleDb.OleDbConnection  'diego     Private Conexion1 As OleDbConnection

    Private mvarNroInterDeposito As String
    Private mvarBco_Dep_Tipo As String
    Private mvarBco_Dep_Fecha As Date
    Private mvarBco_Dep_Numero As Long
    Private mvarBco_Dep_Monto As Single
    Private mvarBco_Dep_Ch_Cantidad As Byte
    Private mvarBco_Dep_IdCta As Byte
    Private mvarIdCliente As Long

    Private IntDepFacturas As Collection

    Public Event GuardoOK(ByVal NroInterDeposito As String)

    Public Property NroInterDeposito() As String
        Set(ByVal value As String)
            mvarNroInterDeposito = value
        End Set
        Get
            Return mvarNroInterDeposito
        End Get
    End Property

    Public Property Bco_Dep_Tipo() As String

        Set(ByVal value As String)
            mvarBco_Dep_Tipo = value
        End Set
        Get
            Return mvarBco_Dep_Tipo
        End Get
    End Property

    Public Property Bco_Dep_Fecha() As Date
        Set(ByVal value As Date)
            mvarBco_Dep_Fecha = value
        End Set
        Get
            Return mvarBco_Dep_Fecha
        End Get
    End Property

    Public Property Bco_Dep_Numero() As Long
        Set(ByVal value As Long)
            mvarBco_Dep_Numero = value
        End Set
        Get
            Return mvarBco_Dep_Numero
        End Get
    End Property

    Public Property Bco_Dep_Monto() As Single
        Set(ByVal value As Single)
            mvarBco_Dep_Monto = value
        End Set

        Get
            Return mvarBco_Dep_Monto
        End Get
    End Property


    Public Property Bco_Dep_Ch_Cantidad() As Byte
        Set(ByVal value As Byte)
            mvarBco_Dep_Ch_Cantidad = value
        End Set
        Get
            Return mvarBco_Dep_Ch_Cantidad
        End Get
    End Property


    Public Property Bco_Dep_IdCta() As Byte
        Set(ByVal value As Byte)
            mvarBco_Dep_IdCta = value
        End Set

        Get
            Return mvarBco_Dep_IdCta
        End Get
    End Property

    Public ReadOnly Property IdCliente() As Long
        Get
            Return mvarIdCliente
        End Get
    End Property

    Public ReadOnly Property CantidadItems() As Long
        Get
            Return IntDepFacturas.Count
        End Get
    End Property

    'diego    Private Sub Class_Terminate()
    'diego         IntDepFacturas = Nothing
    'diego     End Sub

    Public Sub Nuevo(Optional ByVal IDdeCliente As Long = 0)

        IntDepFacturas = Nothing
        IntDepFacturas = New Collection
        mvarNroInterDeposito = 0

        If IDdeCliente > 0 Then mvarIdCliente = IDdeCliente

    End Sub


    Public Function Renglon(ByVal Numero As Integer) As InterDepositoFacturas
        Return IntDepFacturas("_" & Numero)
    End Function

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    Private Function ValidarConexion() As Boolean
        Return Not (Conexion1 Is Nothing)
    End Function

    Public Sub Guardar(Optional ByVal Origen As String = vbNullString)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim adoCMD As New System.Data.OleDb.OleDbCommand

        If UCase(Origen) = "VER" Then
            Funciones.adoModulo.adoComandoIU(Conexion1, "DELETE FROM tblInterDeposito WHERE NroInterDeposito='09999-99999999'")
            mvarNroInterDeposito = "09999-99999999"
        Else
            rec = Funciones.adoModulo.adoComando(Conexion1, "SELECT TOP 1 right(NroInterdeposito,8) AS NroInterDeposito FROM tblInterDeposito WHERE left(NroInterDeposito,5)=" & vg.NroUsuario & " ORDER BY NroInterDeposito DESC")
            If Not rec.HasRows Then
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroInterDeposito = Trim(vg.NroUsuario) & "-C0000001"
                Else
                    mvarNroInterDeposito = Trim(vg.NroUsuario) & "-00000001"
                End If
            Else
                If vg.miSABOR = 2 Then ' ES UN CLIENTE
                    mvarNroInterDeposito = Trim(vg.NroUsuario) & "-C" & Format(CLng(Right(rec("NroInterDeposito"), 7)) + 1, "0000000")
                Else
                    mvarNroInterDeposito = Trim(vg.NroUsuario) & "-" & Format(CLng(Right(rec("NroInterDeposito"), 7)) + 1, "00000000")
                End If
            End If
            rec = Nothing

        End If

        adoCMD.Parameters.Add("pNroInterDeposito", SqlDbType.VarChar, 14).Value = mvarNroInterDeposito
        adoCMD.Parameters.Add("pIdCliente", SqlDbType.Int).Value = mvarIdCliente
        adoCMD.Parameters.Add("pBco_Dep_Tipo", SqlDbType.VarChar, 1).Value = mvarBco_Dep_Tipo
        adoCMD.Parameters.Add("pBco_Dep_Fecha", SqlDbType.Date).Value = mvarBco_Dep_Fecha
        adoCMD.Parameters.Add("pBco_Dep_Numero", SqlDbType.Int).Value = mvarBco_Dep_Numero
        adoCMD.Parameters.Add("pBco_Dep_Monto", SqlDbType.Float).Value = mvarBco_Dep_Monto
        adoCMD.Parameters.Add("pBco_Dep_Ch_Cantidad", SqlDbType.Int).Value = mvarBco_Dep_Ch_Cantidad
        adoCMD.Parameters.Add("pBco_Dep_IdCta", SqlDbType.TinyInt).Value = mvarBco_Dep_IdCta

        adoCMD.Connection = Conexion1
        adoCMD.CommandType = CommandType.StoredProcedure
        adoCMD.CommandText = "usp_InterDeposito_add"
        adoCMD.ExecuteNonQuery()


        adoCMD = Nothing

        Dim I As Integer

        For I = 1 To IntDepFacturas.Count
            GuardarItem(Renglon(I).T_Comprobante, Renglon(I).N_Comprobante, CSng(Renglon(I).Importe))
        Next I

        vg.NroImprimir = mvarNroInterDeposito
        If UCase(Origen) = "VER" Then
            ' Actualizo Fecha de Transmicion para que no lo mande
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Pedido_Transmicion_Upd '" & mvarNroInterDeposito & "'")
        Else
            Nuevo()
            RaiseEvent GuardoOK(vg.NroImprimir)
        End If

        Exit Sub

ErrorHandler:

        rec = Nothing
        adoCMD = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public Sub ADDFacturas(ByVal T_Comprobante As String, ByVal N_Comprobante As String, ByVal Importe As Single)

        Dim mvarItem As New InterDepositoFacturas

        mvarItem.T_Comprobante = T_Comprobante
        mvarItem.N_Comprobante = N_Comprobante
        mvarItem.Importe = Importe

        IntDepFacturas.Add(mvarItem, "_" & IntDepFacturas.Count + 1)

        mvarItem = Nothing

    End Sub

    Private Sub GuardarItem(ByVal pT_Comprobante As String, ByVal pN_Comprobante As String, ByVal pImporte As Single)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pNroInterDeposito", SqlDbType.VarChar, 14).Value = mvarNroInterDeposito
        cmd.Parameters.Add("pT_Comprobante", SqlDbType.VarChar, 3).Value = pT_Comprobante
        cmd.Parameters.Add("pN_Comprobante", SqlDbType.VarChar, 12).Value = pN_Comprobante
        cmd.Parameters.Add("pImporte", SqlDbType.Float).Value = pImporte

        cmd.Connection = Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_InterDeposito_Fac_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

End Class
