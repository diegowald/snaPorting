Imports System.Windows.Forms

Public Class frmErrorGuardian

    Private mModuloName As String
    Private mErrCompleto As String

    Public Property ModuloName() As String
        Get
            Return mModuloName
        End Get
        Set(ByVal value As String)
            mModuloName = value
        End Set
    End Property

    Public Property ErrCompleto() As String
        Get
            Return mErrCompleto
        End Get
        Set(ByVal value As String)
            mErrCompleto = value
        End Set
    End Property

    Public WriteOnly Property ErrDetails() As String
        Set(ByVal value As String)
            TextBox1.Text = value
            TextBox2.Text = vbNullString
        End Set
    End Property

    Private Sub btnAbortar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbortar.Click

        Dim sRep As String

        ErrorGuardianUserReply = DialogResult.Abort
        sRep = "Abortar Pulsado..."

        'On Error Resume Next
        'vg.Auditor.Guardar ErrordePrograma, EXITOSO, sRep
        'On Error GoTo 0

        ErrLog(sRep)

        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.Close()

    End Sub

    Private Sub btnIgnorar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIgnorar.Click

        Dim sRep As String

        ErrorGuardianUserReply = System.Windows.Forms.DialogResult.Ignore
        sRep = "Ignorar Pulsado..."

        'On Error Resume Next
        'vg.Auditor.Guardar ErrordePrograma, EXITOSO, sRep
        'On Error GoTo 0

        ErrLog(sRep)

        Me.DialogResult = System.Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub

    Private Sub btnReintentar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReintentar.Click

        Dim sRep As String

        ErrorGuardianUserReply = System.Windows.Forms.DialogResult.Retry
        sRep = "Reintentar Pulsado..."

        'On Error Resume Next
        'vg.Auditor.Guardar ErrordePrograma, EXITOSO, sRep
        'On Error GoTo 0

        ErrLog(sRep)

        Me.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub ErrLog(ByVal sRep As String)

        Dim fn As String
        Static Dim Separator As String

        Separator = Strings.Space(78).Replace(" ", "-")

        fn = vg.Path & "\ErrorGuardianLog.txt"
        Dim file As System.IO.TextWriter = New System.IO.StreamWriter(fn)

        file.WriteLine(mErrCompleto)
        file.WriteLine("")
        If TextBox2.Text.Trim.Length > 0 Then
            file.WriteLine(TextBox2.Text)
            file.WriteLine("")
        End If
        'Print #FF, sRep
        file.WriteLine(Separator)
        file.Flush()
        file.Close()

    End Sub

End Class
