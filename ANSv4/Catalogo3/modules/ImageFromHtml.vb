Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.Drawing
Imports System.Windows.Forms

Public Class ImageFromHtml
    Private PageUrl As String
    Private ConvertedImage As Bitmap

    Private m_intHeight As Integer
    Public Property Height() As Integer
        Get
            Return m_intHeight
        End Get
        Set(ByVal value As Integer)
            m_intHeight = value
        End Set
    End Property

    Private m_intWidth As Integer
    Public Property Width() As Integer
        Get
            Return m_intWidth
        End Get
        Set(ByVal value As Integer)
            m_intWidth = value
        End Set
    End Property

    Public Function ConvertPage(ByVal PageUrl As String) As Bitmap
        Me.PageUrl = PageUrl
        Dim thrCurrent As New Thread(New ThreadStart(AddressOf CreateImage))
        thrCurrent.SetApartmentState(ApartmentState.STA)
        thrCurrent.Start()
        thrCurrent.Join()
        Return ConvertedImage
    End Function
    Private Sub CreateImage()

        Dim BrowsePage As New WebBrowser()
        BrowsePage.ScrollBarsEnabled = False
        BrowsePage.Navigate(PageUrl)
        AddHandler BrowsePage.DocumentCompleted, AddressOf _
WebBrowser_DocumentCompleted

        While BrowsePage.ReadyState <> WebBrowserReadyState.Complete
            Application.DoEvents()
        End While
        BrowsePage.Dispose()
    End Sub

    Private Sub WebBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As  _
WebBrowserDocumentCompletedEventArgs)
        Dim BrowsePage As WebBrowser = DirectCast(sender, WebBrowser)
        BrowsePage.ClientSize = New Size(Width, Height)
        BrowsePage.ScrollBarsEnabled = False
        ConvertedImage = New Bitmap(Width, Height)
        BrowsePage.BringToFront()
        BrowsePage.DrawToBitmap(ConvertedImage, BrowsePage.Bounds)
    End Sub

End Class