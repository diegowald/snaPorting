Option Explicit On

Public Class ReciboItem


    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsReciboItem"

    'variables locales para almacenar los valores de las propiedades
    Private mvarTipoValor As Byte
    Private mvarImporte As Single
    Private mvarT_Cambio As Single
    Private mvarF_EmiCheque As Date
    Private mvarF_CobroCheque As Date
    Private mvarN_Cheque As String
    Private mvarNrodeCuenta As String
    Private mvarBanco As Integer
    Private mvarCpa As String
    Private mvarChequePropio As Boolean

    Public Property TipoValor() As Byte
        Set(ByVal value As Byte)
            mvarTipoValor = value
        End Set
        Get
            Return mvarTipoValor
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

    Public Property T_Cambio() As Single
        Get
            Return mvarT_Cambio
        End Get

        Set(ByVal value As Single)
            mvarT_Cambio = value
        End Set
    End Property

    Public Property F_EmiCheque() As Date
        Set(ByVal value As Date)
            mvarF_EmiCheque = value
        End Set
        Get
            Return mvarF_EmiCheque
        End Get
    End Property

    Public Property F_CobroCheque() As Date
        Set(ByVal value As Date)
            mvarF_CobroCheque = value
        End Set
        Get
            Return mvarF_CobroCheque
        End Get
    End Property

    Public Property N_Cheque() As String
        Set(ByVal value As String)
            mvarN_Cheque = value
        End Set
        Get
            Return mvarN_Cheque
        End Get
    End Property

    Public Property NrodeCuenta() As String
        Set(ByVal value As String)
            mvarNrodeCuenta = value
        End Set

        Get
            Return mvarNrodeCuenta
        End Get
    End Property

    Public Property Banco() As Integer
        Set(ByVal value As Integer)
            mvarBanco = value
        End Set
        Get
            Return mvarBanco
        End Get
    End Property

    Public Property Cpa() As String
        Set(ByVal value As String)
            mvarCpa = value
        End Set
        Get
            Return mvarCpa
        End Get
    End Property

    Public Property ChequePropio() As Boolean
        Set(ByVal value As Boolean)
            mvarChequePropio = value
        End Set
        Get
            Return mvarChequePropio
        End Get
    End Property


End Class
