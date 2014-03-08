Option Explicit On
Public Class DeducirItem

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsDeducirItem"

    'variables locales para almacenar los valores de las propiedades
    Private mvarConcepto As String
    Private mvarImporte As Single
    Private mvarPorcentaje As Boolean
    Private mvarDeduAlResto As Boolean

    Public Property DeduAlResto() As Boolean
        Set(ByVal value As Boolean)
            mvarDeduAlResto = value
        End Set
        Get
            Return mvarDeduAlResto
        End Get
    End Property

    Public Property Porcentaje() As Boolean
        Set(ByVal value As Boolean)
            mvarPorcentaje = value
        End Set

        Get
            Return mvarPorcentaje
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
