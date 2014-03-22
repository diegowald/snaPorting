﻿Option Explicit On

Public Class Recibo

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsRecibo"

    Private _Conexion1 As System.Data.OleDb.OleDbConnection

    Private mvarNroRecibo As String
    Private mvarF_Recibo As Date
    Private mvarIdCliente As Long
    Private mvarBahia As Boolean
    Private mvarTotal As Single
    Private mvarPercepciones As Single
    Private mvarNroImpresion As Byte
    Private mvarF_Transmicion As Date
    Private mvarObservaciones As String

    'BarCode: lo genero dinamicamente en v_Recibo_Enc con el Alias CRC
    'BarCode:   Recortar(CCadena(tblRecibo_Enc!F_Recibo)) & _
    '        Recortar(CCadena(tblRecibo_Enc!NroRecibo)) & _
    '        Recortar(CCadena(tblRecibo_Enc!Total)) & _
    '        Recortar(CCadena(v_Recibo_cItems!Items)) & _
    '        Recortar(CCadena(tblRecibo_Enc!IDCliente)) & _
    '        Recortar(CCadena(tblRecibo_Enc!NroImpresion))

    Private DetalleRecibo As Collection
    Private AplicacionRecibo As Collection
    Private DeducirRecibo As Collection

    Public Event GuardoOK(ByVal nroRecibo As String)

    Public Property Observaciones() As String
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
        Get
            Return mvarObservaciones
        End Get
    End Property

    Public Property F_Transmision() As Date
        Set(ByVal value As Date)
            mvarF_Transmicion = value
        End Set

        Get
            Return mvarF_Transmicion
        End Get
    End Property

    Public Property Bahia() As Boolean
        Set(ByVal value As Boolean)
            mvarBahia = value
        End Set

        Get
            Return mvarBahia
        End Get
    End Property

    Public Property Total() As Single
        Set(ByVal value As Single)
            mvarTotal = value
        End Set
        Get
            Return mvarTotal
        End Get
    End Property

    Public Property Percepciones() As Single
        Set(ByVal value As Single)
            mvarPercepciones = value
        End Set
        Get
            Return mvarPercepciones
        End Get
    End Property

    Public ReadOnly Property CantidadItemsDedu() As Long
        Get
            Return DeducirRecibo.Count
        End Get
    End Property

    Public ReadOnly Property CantidadItemsApli() As Long
        Get
            Return AplicacionRecibo.Count
        End Get
    End Property

    Public ReadOnly Property CantidadItems() As Long
        Get
            Return DetalleRecibo.Count
        End Get
    End Property

    Public Sub New(ByVal IDdeCliente As Long)
        Nuevo(IDdeCliente)
    End Sub

    Private Sub Nuevo(Optional ByVal IDdeCliente As Long = 0)

        DetalleRecibo = Nothing
        AplicacionRecibo = Nothing
        DeducirRecibo = Nothing

        DetalleRecibo = New Collection
        AplicacionRecibo = New Collection
        DeducirRecibo = New Collection

        mvarNroRecibo = ""
        mvarF_Recibo = Date.Today
        If IDdeCliente > 0 Then mvarIdCliente = IDdeCliente
    End Sub

    ' FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
    Public Function RenglonDedu(ByVal Numero As Integer) As DeducirItem
        Return DeducirRecibo("_" & Numero)
    End Function

    Public Function RenglonApli(ByVal Numero As Integer) As AplicacionItem
        Return AplicacionRecibo("_" & Numero)
    End Function

    Public Function Renglon(ByVal Numero As Integer) As ReciboItem
        Return DetalleRecibo("_" & Numero)
    End Function

    Public Sub Leer(ByVal NrodeRecibo As Long)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        rec = Funciones.adoModulo.adoComando(_Conexion1, "EXECUTE v_Recibo_Det " & NrodeRecibo)
        If rec.HasRows Then
            While rec.Read
                ADDItem(rec("TipoValor"), rec("Importe"), rec("F_EmiCheque"), rec("F_CobroCheque"), _
                rec("N_Cheque"), rec("NrodeCuenta"), rec("Banco"), rec("Cpa"), rec("ChequePropio"), _
                rec("T_Cambio"))
            End While
            rec = Funciones.adoModulo.adoComando(_Conexion1, "EXECUTE v_Recibo_Enc " & NrodeRecibo)
            If rec.HasRows Then
                rec.Read()
                mvarNroRecibo = NrodeRecibo
                mvarF_Recibo = rec("F_Recibo")
                mvarIdCliente = rec("IdCliente")
                mvarBahia = rec("Bahia")
                mvarTotal = rec("Total")
                mvarNroImpresion = rec("NroImpresion")
                mvarF_Transmicion = rec("F_Transmicion")
                mvarObservaciones = rec("Observaciones")
                mvarPercepciones = rec("Percepciones")
            End If
        End If
        rec = Nothing

        Exit Sub

ErrorHandler:

        rec = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            _Conexion1 = value
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

    Public ReadOnly Property F_Recibo() As Date
        Get
            Return mvarF_Recibo
        End Get
    End Property

    Public ReadOnly Property nroRecibo() As String
        Get
            Return mvarNroRecibo
        End Get
    End Property

    'diego     Private Sub Class_Terminate()
    'diego         DetalleRecibo = Nothing
    'diego     End Sub

    Private Function ValidarConexion() As Boolean
        Return Not (_Conexion1 Is Nothing)
    End Function

    Public Sub Guardar(Optional ByVal Origen As String = vbNullString)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim cmd As New System.Data.OleDb.OleDbCommand

        If DetalleRecibo.Count < 1 Then Exit Sub

        If UCase(Origen) = "VER" Then
            Funciones.adoModulo.adoComandoIU(_Conexion1, "DELETE FROM tblRecibo_Enc WHERE NroRecibo='09999-99999999'")
            mvarNroRecibo = "09999-99999999"
        Else
            rec = Funciones.adoModulo.adoComando(_Conexion1, "SELECT TOP 1 right(NroRecibo,8) AS NroRecibo FROM tblRecibo_Enc WHERE left(NroRecibo,5)=" & mvarIdCliente & " ORDER BY NroRecibo DESC")
            If Not rec.HasRows Then
                mvarNroRecibo = Trim(mvarIdCliente) & "-00000001"
            Else
                mvarNroRecibo = Trim(mvarIdCliente) & "-" & Format(CLng(rec("nroRecibo") + 1), "00000000")
            End If
            rec = Nothing
        End If

        cmd.Parameters.Add("pNroRecibo", OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pF_Recibo", OleDb.OleDbType.Date).Value = mvarF_Recibo
        cmd.Parameters.Add("pIdCliente", OleDb.OleDbType.Integer).Value = mvarIdCliente
        cmd.Parameters.Add("pBahia", OleDb.OleDbType.Boolean).Value = mvarBahia
        cmd.Parameters.Add("pTotal", OleDb.OleDbType.Single).Value = mvarTotal
        cmd.Parameters.Add("pNroImpresion", OleDb.OleDbType.Integer).Value = mvarNroImpresion
        cmd.Parameters.Add("pObservaciones", OleDb.OleDbType.VarChar, 200).Value = mvarObservaciones
        cmd.Parameters.Add("pPercepciones", OleDb.OleDbType.Single).Value = mvarPercepciones

        cmd.Connection = _Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Recibo_Enc_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing

        ' Ahora GUARDO los ITEMS de ESTE Recibo
        Dim I As Integer

        For I = 1 To DetalleRecibo.Count
            GuardarItem(Renglon(I).TipoValor, _
                        Renglon(I).Importe, _
                        Renglon(I).F_EmiCheque, _
                        Renglon(I).F_CobroCheque, _
                        Renglon(I).N_Cheque, _
                        Renglon(I).NrodeCuenta, _
                        Renglon(I).Banco, _
                        Renglon(I).Cpa, _
                        Renglon(I).ChequePropio, _
                        Renglon(I).T_Cambio)
        Next I

        For I = 1 To AplicacionRecibo.Count
            GuardarItemApli(RenglonApli(I).Concepto, _
                            RenglonApli(I).Importe)
        Next I

        For I = 1 To DeducirRecibo.Count
            GuardarItemDedu(RenglonDedu(I).Concepto, _
                            RenglonDedu(I).Importe, _
                            RenglonDedu(I).Porcentaje, _
                            RenglonDedu(I).DeduAlResto)
        Next I

        ' Por POLITICA NUESTRA  -- >  SE BORRA
        If UCase(Origen) = "VER" Then
            ' Actualizo Fecha de Transmicion para que no lo mande
            Funciones.adoModulo.adoComandoIU(_Conexion1, "EXEC usp_Recibo_Transmicion_Upd '" & mvarNroRecibo & "'")
        Else
            'pablo
            'vg.auditor.Guardar(ObjetosAuditados.Recibo, AccionesAuditadas.EXITOSO, "cli:" & Format(mvarIdCliente, "000000") & " ped:" & mvarNroRecibo & " tot:" & Format(mvarTotal, "fixed"))
            Nuevo()
            RaiseEvent GuardoOK(mvarNroRecibo)
        End If

        Exit Sub

ErrorHandler:

        rec = Nothing
        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Public Sub ADDItem(ByVal TipoValor As Byte, _
                       ByVal Importe As Single, _
                       ByVal F_EmiCheque As Date, _
                       ByVal F_CobroCheque As Date, _
                       ByVal N_Cheque As String, _
                       ByVal NrodeCuenta As String, _
                       ByVal Banco As Integer, _
                       ByVal Cpa As String, _
                       ByVal ChequePropio As Boolean, _
                       ByVal T_Cambio As Single)

        Dim mvarItem As New ReciboItem

        mvarItem.TipoValor = TipoValor
        mvarItem.Importe = Importe
        mvarItem.T_Cambio = T_Cambio
        mvarItem.F_EmiCheque = F_EmiCheque
        mvarItem.F_CobroCheque = F_CobroCheque
        mvarItem.N_Cheque = N_Cheque
        mvarItem.NrodeCuenta = NrodeCuenta
        mvarItem.Banco = Banco
        mvarItem.Cpa = Cpa
        mvarItem.ChequePropio = ChequePropio

        DetalleRecibo.Add(mvarItem, "_" & DetalleRecibo.Count + 1)

        mvarItem = Nothing

    End Sub

    Public Sub ADDItemDedu(ByVal Concepto As String, _
                            ByVal Importe As Single, _
                            ByVal Porcentaje As Boolean, _
                            ByVal DeduAlResto As Boolean)

        Dim mvarItemDedu As New DeducirItem

        mvarItemDedu.Concepto = Concepto
        mvarItemDedu.Importe = Importe
        mvarItemDedu.Porcentaje = Porcentaje
        mvarItemDedu.DeduAlResto = DeduAlResto

        DeducirRecibo.Add(mvarItemDedu, "_" & DeducirRecibo.Count + 1)

        mvarItemDedu = Nothing

    End Sub

    Public Sub ADDItemApli(ByVal Concepto As String, _
                            ByVal Importe As Single)

        Dim mvarItemApli As New AplicacionItem

        mvarItemApli.Concepto = Concepto
        mvarItemApli.Importe = Importe

        AplicacionRecibo.Add(mvarItemApli, "_" & AplicacionRecibo.Count + 1)

        mvarItemApli = Nothing

    End Sub

    Private Sub GuardarItem(ByVal TipoValor As Byte, _
                              ByVal Importe As Single, _
                              ByVal F_EmiCheque As Date, _
                              ByVal F_CobroCheque As Date, _
                              ByVal N_Cheque As String, _
                              ByVal NrodeCuenta As String, _
                              ByVal Banco As Integer, _
                              ByVal Cpa As String, _
                              ByVal ChequePropio As Boolean, _
                              ByVal T_Cambio As Single)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pNroRecibo", OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pTipoValor", OleDb.OleDbType.TinyInt).Value = TipoValor
        cmd.Parameters.Add("pImporte", OleDb.OleDbType.Single).Value = Importe
        cmd.Parameters.Add("pF_EmiCheque", OleDb.OleDbType.Date).Value = F_EmiCheque
        cmd.Parameters.Add("pF_CobroCheque", OleDb.OleDbType.Date).Value = F_CobroCheque
        cmd.Parameters.Add("pN_Cheque", OleDb.OleDbType.VarChar, 50).Value = N_Cheque
        cmd.Parameters.Add("pN_Cuenta", OleDb.OleDbType.VarChar, 50).Value = NrodeCuenta
        cmd.Parameters.Add("pIdBanco", OleDb.OleDbType.Integer).Value = Banco
        cmd.Parameters.Add("pCpa", OleDb.OleDbType.VarChar, 10).Value = Cpa
        cmd.Parameters.Add("pChequePropio", OleDb.OleDbType.Boolean).Value = ChequePropio
        cmd.Parameters.Add("pT_Cambio", OleDb.OleDbType.Single).Value = T_Cambio

        cmd.Connection = _Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Recibo_Det_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        cmd = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Private Sub GuardarItemApli(ByVal Concepto As String, _
                                ByVal Importe As Single)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pNroRecibo", OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pConcepto", OleDb.OleDbType.VarChar, 50).Value = Concepto
        cmd.Parameters.Add("pImporte", OleDb.OleDbType.Single).Value = Importe

        cmd.Connection = _Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Recibo_App_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        cmd = Nothing

        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

    Private Sub GuardarItemDedu(ByVal Concepto As String, _
                                ByVal Importe As Single, _
                                ByVal Porcentaje As Boolean, _
                                ByVal DeduAlResto As Boolean)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim cmd As New System.Data.OleDb.OleDbCommand

        cmd.Parameters.Add("pNroRecibo", OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pConcepto", OleDb.OleDbType.VarChar, 50).Value = Concepto
        cmd.Parameters.Add("pImporte", OleDb.OleDbType.Single).Value = Importe
        cmd.Parameters.Add("pPorcentaje", OleDb.OleDbType.Boolean).Value = Porcentaje
        cmd.Parameters.Add("pDeduAlResto", OleDb.OleDbType.Boolean).Value = DeduAlResto

        cmd.Connection = _Conexion1
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "usp_Recibo_Deducir_add"
        cmd.ExecuteNonQuery()

        cmd = Nothing
        Exit Sub

ErrorHandler:

        cmd = Nothing

        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub


End Class
