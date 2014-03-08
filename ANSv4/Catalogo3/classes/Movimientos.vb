Option Explicit On
Public Class Movimientos

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsMovimientos"

    Private Conexion1 As System.Data.OleDb.OleDbConnection  'diego Private Conexion1 As OleDbConnection
    Private Items As Collection

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    'diego Public Function Leer(ByVal queMostrar As Byte, _
    'diego                      ByVal IdCliente As Long) As ADODB.Recordset

    'diego         If Not (ValidarConexion()) Then
    'diego             Exit Function
    'diego         End If

    'diego  On Error GoTo ErrorHandler

    'diego     Dim wCond2 As String

    'diego         If IdCliente > 0 Then
    'diego      wCond2 = " and IDcliente=" & CStr(IdCliente)
    'diego Else
    'diego             wCond2 = ""
    'diego  End If

    'diego         Select Case queMostrar
    'diego      Case 0 'Todos
    'diego   Leer = adoModulo.xGetRSMDB(Conexion1, "v_Movimientos", "left(Nro,5)='" & vg.NroUsuario & "' " & wCond2, "IDcliente, Origen, Fecha DESC")
    'diego             Case 1 'Enviados
    'diego          Leer = adoModulo.xGetRSMDB(Conexion1, "v_Movimientos", "left(Nro,5)='" & vg.NroUsuario & "' and not (F_Transmicion is null)" & wCond2, "IDcliente, Origen, Fecha DESC")
    'diego             Case 2 'No Enviados
    'diego          Leer = adoModulo.xGetRSMDB(Conexion1, "v_Movimientos", "left(Nro,5)='" & vg.NroUsuario & "' and F_Transmicion is null" & wCond2, "IDcliente, Origen, Fecha DESC")
    'diego         End Select

    'diego  Exit Function

    'diego ErrorHandler:
    'diego  Err.Raise(Err.Number, Err.Source, Err.Description)

    'diego     End Function

    'diego     Private Sub Class_Initialize()
    'diego   Items = New Collection
    'diego     End Sub

    Public Function PreguntoAlSalir() As Boolean
        ' Devuelvo true si es que HAY PENDIENTE de ALGUNOS de los cliente

        If Not (ValidarConexion()) Then Exit Function

        On Error GoTo ErrorHandler

        'diego         Dim adoREC As ADODB.Recordset
        'diego   adoREC = New ADODB.Recordset
        'diego         adoREC = adoModulo.adoComando(Conexion1, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE (F_Transmicion is null) and left(Nro,5)='" & vg.NroUsuario & "'")

        'diego         If Not adoREC.EOF Then
        'diego If adoREC!cantidad > 0 Then
        'diego         PreguntoAlSalir = True
        'diego  Else
        'diego         PreguntoAlSalir = False
        'diego  End If
        'diego         End If

        'diego         adoREC = Nothing

        Exit Function

ErrorHandler:
        'diego         adoREC = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function

    Public Function PreguntoAlCerrarVisita(ByVal IdCliente As Long) As Boolean
        ' Devuelvo true si es que HAY PENDIENTE para ESTE cliente.

        If Not (ValidarConexion()) Then Exit Function

        On Error GoTo ErrorHandler

        'diego         Dim adoREC As ADODB.Recordset
        'diego  adoREC = New ADODB.Recordset
        'diego         adoREC = adoModulo.adoComando(Conexion1, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE UnionPedidoRecibo.IdCliente=" & CStr(IdCliente) & " and (f_Transmicion is NULL) and left(Nro,5)='" & vg.NroUsuario & "'")

        'diego         If Not adoREC.EOF Then
        'diego If adoREC!cantidad > 0 Then
        'diego  PreguntoAlCerrarVisita = True
        'diego         Else
        'diego         PreguntoAlCerrarVisita = False
        'diego  End If
        'diego         End If

        'diego         adoREC = Nothing

        Exit Function

ErrorHandler:
        'diego         adoREC = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function

    Private Function ValidarConexion() As Boolean
        Return Not (Conexion1 Is Nothing)
    End Function


End Class
