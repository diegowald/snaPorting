Option Explicit On

Public Class Recibo

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsRecibo"

    Private Conexion1 As System.Data.OleDb.OleDbConnection

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

    Public Sub Nuevo(Optional ByVal IDdeCliente As Long = 0)

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
        rec = Funciones.adoModulo.adoComando(Conexion1, "EXECUTE v_Recibo_Det " & NrodeRecibo)
        If rec.HasRows Then
            While rec.Read
                ADDItem(rec("TipoValor"), rec("Importe"), rec("F_EmiCheque"), rec("F_CobroCheque"), _
                rec("N_Cheque"), rec("NrodeCuenta"), rec("Banco"), rec("Cpa"), rec("ChequePropio"), _
                rec("T_Cambio"))
            End While
            rec = Funciones.adoModulo.adoComando(Conexion1, "EXECUTE v_Recibo_Enc " & NrodeRecibo)
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
        Return Not (Conexion1 Is Nothing)
    End Function

    Public Sub Guardar(Optional ByVal Origen As String = vbNullString)

        If Not (ValidarConexion()) Then Exit Sub

        On Error GoTo ErrorHandler

        Dim rec As System.Data.OleDb.OleDbDataReader
        Dim cmd As New System.Data.OleDb.OleDbCommand

        If DetalleRecibo.Count < 1 Then Exit Sub

        If UCase(Origen) = "VER" Then
            Funciones.adoModulo.adoComandoIU(Conexion1, "DELETE FROM tblRecibo_Enc WHERE NroRecibo='09999-99999999'")
            mvarNroRecibo = "09999-99999999"
        Else
            rec = Funciones.adoModulo.adoComando(Conexion1, "SELECT TOP 1 right(NroRecibo,8) AS NroRecibo FROM tblRecibo_Enc WHERE left(NroRecibo,5)=" & vg.NroUsuario & " ORDER BY NroRecibo DESC")
            If Not rec.HasRows Then
                mvarNroRecibo = Trim(vg.NroUsuario) & "-00000001"
            Else
                mvarNroRecibo = Trim(vg.NroUsuario) & "-" & Format(CLng(rec("nroRecibo") + 1), "00000000")
            End If
            rec = Nothing
            End If

            cmd.Parameters.Add("pNroRecibo", SqlDbType.VarChar, 14).Value = mvarNroRecibo
            cmd.Parameters.Add("pF_Recibo", SqlDbType.Date).Value = mvarF_Recibo
            cmd.Parameters.Add("pIdCliente", SqlDbType.Int).Value = mvarIdCliente
            cmd.Parameters.Add("pBahia", SqlDbType.Bit).Value = mvarBahia
            cmd.Parameters.Add("pTotal", SqlDbType.Float).Value = mvarTotal
            cmd.Parameters.Add("pNroImpresion", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("pObservaciones", SqlDbType.VarChar, 200).Value = mvarObservaciones
            cmd.Parameters.Add("pPercepciones", SqlDbType.Float).Value = mvarPercepciones

            cmd.Connection = Conexion1
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

            vg.NroImprimir = mvarNroRecibo

        ' Por POLITICA NUESTRA  -- >  SE BORRA
        If UCase(Origen) = "VER" Then
            ' Actualizo Fecha de Transmicion para que no lo mande
            Funciones.adoModulo.adoComandoIU(Conexion1, "EXEC usp_Recibo_Transmicion_Upd '" & mvarNroRecibo & "'")
        Else
            vg.auditor.Guardar(ObjetosAuditados.Recibo, AccionesAuditadas.EXITOSO, "cli:" & Format(mvarIdCliente, "000000") & " ped:" & mvarNroRecibo & " tot:" & Format(mvarTotal, "fixed"))
            Nuevo()
            RaiseEvent GuardoOK(vg.NroImprimir)
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

        cmd.Parameters.Add("pNroRecibo", SqlDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pTipoValor", SqlDbType.TinyInt).Value = TipoValor
        cmd.Parameters.Add("pImporte", SqlDbType.Float).Value = Importe
        cmd.Parameters.Add("pF_EmiCheque", SqlDbType.Date).Value = F_EmiCheque
        cmd.Parameters.Add("pF_CobroCheque", SqlDbType.Date).Value = F_CobroCheque
        cmd.Parameters.Add("pN_Cheque", SqlDbType.VarChar, 50).Value = N_Cheque
        cmd.Parameters.Add("pN_Cuenta", SqlDbType.VarChar, 50).Value = NrodeCuenta
        cmd.Parameters.Add("pIdBanco", SqlDbType.Int).Value = Banco
        cmd.Parameters.Add("pCpa", SqlDbType.VarChar, 10).Value = Cpa
        cmd.Parameters.Add("pChequePropio", SqlDbType.Bit).Value = ChequePropio
        cmd.Parameters.Add("pT_Cambio", SqlDbType.Float).Value = T_Cambio

        cmd.Connection = Conexion1
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

        cmd.Parameters.Add("pNroRecibo", SqlDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pConcepto", SqlDbType.VarChar, 50).Value = Concepto
        cmd.Parameters.Add("pImporte", SqlDbType.Float).Value = Importe

        cmd.Connection = Conexion1
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

        cmd.Parameters.Add("pNroRecibo", SqlDbType.VarChar, 14).Value = mvarNroRecibo
        cmd.Parameters.Add("pConcepto", SqlDbType.VarChar, 50).Value = Concepto
        cmd.Parameters.Add("pImporte", SqlDbType.Float).Value = Importe
        cmd.Parameters.Add("pPorcentaje", SqlDbType.Bit).Value = Porcentaje
        cmd.Parameters.Add("pDeduAlResto", SqlDbType.Bit).Value = DeduAlResto

        cmd.Connection = Conexion1
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
