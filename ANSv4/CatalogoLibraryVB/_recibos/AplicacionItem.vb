Option Explicit On

Public Class AplicacionItem

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsAplicacionItem"

    'variables locales para almacenar los valores de las propiedades
    Private mvarConcepto As String 'copia local
    Private mvarImporte As Single 'copia local
    Private mvarObservaciones As String 'copia local

    Public Property Observaciones() As String
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
        Get
            Return mvarObservaciones
        End Get
    End Property

    Public Property Importe() As Single
        Set(ByVal value As Single)
            mvarImporte = value
        End Set
        Get
            Return mvarImporte
        End Get
    End Property

    Public Property Concepto() As String
        Set(ByVal value As String)        
            mvarConcepto = value
        End Set
        Get
            Return mvarConcepto
        End Get
    End Property

End Class
