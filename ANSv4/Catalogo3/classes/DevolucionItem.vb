Option Explicit On
Public Class DevolucionItem

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsDevolucionItem"

    'variables locales para almacenar los valores de las propiedades

    Private mvarIDCatalogo As String
    Private mvarCantidad As Integer
    Private mvarDeposito As String
    Private mvarFactura As String
    Private mvarTipoDev As Byte
    Private mvarVehiculo As String
    Private mvarModelo As String
    Private mvarMotor As String
    Private mvarKm As String
    Private mvarObservaciones As String

    Public Property Observaciones() As String
        Get
            Return mvarObservaciones
        End Get
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
    End Property

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

    Public Property Deposito() As Byte
        Set(ByVal value As Byte)
            mvarDeposito = value
        End Set
        Get
            Return mvarDeposito
        End Get
    End Property

    Public Property Factura() As String
        Set(ByVal value As String)
            mvarFactura = value
        End Set

        Get
            Return mvarFactura
        End Get
    End Property

    Public Property Vehiculo() As String
        Set(ByVal value As String)
            mvarVehiculo = value
        End Set
        Get
            Return mvarVehiculo
        End Get
    End Property

    Public Property Modelo() As String
        Set(ByVal value As String)
            mvarModelo = value
        End Set
        Get
            Return mvarModelo
        End Get
    End Property

    Public Property Motor() As String
        Get
            Return mvarMotor
        End Get
        Set(ByVal value As String)
            mvarMotor = value
        End Set
    End Property

    Public Property KM() As String
        Set(ByVal value As String)
            mvarKm = value
        End Set
        Get
            Return mvarKm
        End Get
    End Property

    Public Property TipoDev() As Byte
        Set(ByVal value As Byte)
            mvarTipoDev = value
        End Set
        Get
            Return mvarTipoDev
        End Get
    End Property


End Class
