Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim x As New IPPrivado("www.autonauticasur-r.com.ar", "")
        System.Diagnostics.Debug.Print(x.GetIP)
        System.Diagnostics.Debug.Print(x.GetIPCatalogo)
        System.Diagnostics.Debug.Print(x.GetIpIntranet)
    End Sub
End Class
