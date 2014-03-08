Option Explicit On

Public Class InterDepositoFacturas

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsInterDepositoFacturas"

    Private mvarT_Comprobante As String
    Private mvarN_Comprobante As String
    Private mvarImporte As Single

    Public Property T_Comprobante() As String
        Get
            Return mvarT_Comprobante
        End Get
        Set(ByVal value As String)
            mvarT_Comprobante = value
        End Set
    End Property

    Public Property N_Comprobante() As String
        Get
            Return mvarN_Comprobante
        End Get
        Set(ByVal value As String)
            mvarN_Comprobante = value
        End Set
    End Property

    Public Property Importe() As Single
        Set(ByVal value As Single)
            mvarImporte = value
        End Set
        Get
            Return mvarImporte
        End Get
    End Property


End Class
