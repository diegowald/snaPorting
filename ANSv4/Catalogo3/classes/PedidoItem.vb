Option Explicit On


Public Class PedidoItem

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsPedidoItem"

    'variables locales para almacenar los valores de las propiedades

    Private mvarIDCatalogo As String
    Private mvarCantidad As Integer
    Private mvarSimilar As Boolean
    Private mvarBahia As Boolean
    Private mvarOferta As Boolean
    Private mvarDeposito As Byte
    Private mvarPrecio As Single
    Private mvarObservaciones As String


    Public Property IDCatalogo() As String
        Set(ByVal value As String)
            mvarIDCatalogo = value
        End Set
        Get
            Return mvarIDCatalogo
        End Get
    End Property

    Public Property cantidad() As Integer
        Set(ByVal value As Integer)
            mvarCantidad = value
        End Set
        Get
            Return mvarCantidad
        End Get
    End Property

    Public Property Similar() As Boolean
        Set(ByVal value As Boolean)
            mvarSimilar = value
        End Set
        Get
            Return mvarSimilar
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

    Public Property Oferta() As Boolean
        Set(ByVal value As Boolean)
            mvarOferta = value
        End Set

        Get
            Return mvarOferta
        End Get
    End Property

    Public Property Deposito() As Byte
        Set(ByVal value As Byte)
            mvarDeposito = value
        End Set
        Get
            Return mvarDeposito
        End Get
    End Property

    Public Property Precio() As Single
        Set(ByVal value As Single)
            mvarPrecio = value
        End Set
        Get
            Return mvarPrecio
        End Get
    End Property

    Public Property Observaciones() As String
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
        Get
            Return mvarObservaciones
        End Get
    End Property

End Class
