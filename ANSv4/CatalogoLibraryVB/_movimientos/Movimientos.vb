Option Explicit On
Public Class Movimientos

    Public Enum DATOS_MOSTRAR
        TODOS = 0
        ENVIADOS
        NO_ENVIADOS
    End Enum

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsMovimientos"

    Private Conexion1 As System.Data.OleDb.OleDbConnection  'diego Private Conexion1 As OleDbConnection
    Private Items As Collection

    Public Sub New(conexion As System.Data.OleDb.OleDbConnection)
        Conexion1 = conexion
    End Sub

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    Public Function Leer(ByVal queMostrar As DATOS_MOSTRAR, ByVal IDCliente As Long) As System.Data.OleDb.OleDbDataReader

        If Not ValidarConexion() Then
            Return Nothing
        End If

        On Error GoTo ErrorHandler

        Dim wCond2 As String

        If IDCliente > 0 Then
            wCond2 = " and IDcliente=" & IDCliente.ToString
        Else
            wCond2 = ""
        End If

        Select Case queMostrar
            Case DATOS_MOSTRAR.TODOS
                Return Funciones.adoModulo.xGetDR(Conexion1, "v_Movimientos", "left(Nro,5)='" & IDCliente & "' " & wCond2, "IDcliente, Origen, Fecha DESC")
            Case DATOS_MOSTRAR.ENVIADOS
                Return Funciones.adoModulo.xGetDR(Conexion1, "v_Movimientos", "left(Nro,5)='" & vg.NroUsuario & "' and not (F_Transmicion is null)" & wCond2, "IDcliente, Origen, Fecha DESC")
            Case DATOS_MOSTRAR.NO_ENVIADOS
                Return Funciones.adoModulo.xGetDR(Conexion1, "v_Movimientos", "left(Nro,5)='" & vg.NroUsuario & "' and F_Transmicion is null" & wCond2, "IDcliente, Origen, Fecha DESC")
        End Select

        Return Nothing

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function


    Public Function PreguntoAlSalir() As Boolean
        ' Devuelvo true si es que HAY PENDIENTE de ALGUNOS de los cliente

        If Not (ValidarConexion()) Then Return False

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
