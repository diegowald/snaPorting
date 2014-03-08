Option Explicit On


Public Class MovimientoItem


    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsMisTextBoxes"

    'variables locales para almacenar los valores de las propiedades
    Private mvarNumero As Single 'copia local
    Private mvarFecha As Date 'copia local
    Private mvarRazonSocial As String 'copia local
    Private mvarNroImpresion As Byte 'copia local
    Private mvarFechaTrasmision As Date
    Private mvarOrigen As String
    Private mvarIdCliente As Long

    Public Property IdCliente() As Long
        Get
            Return mvarIdCliente
        End Get
        Set(ByVal value As Long)
            mvarIdCliente = value
        End Set
    End Property

    Public Property Origen() As String
        Set(ByVal value As String)
            mvarOrigen = value
        End Set
        Get
            Return mvarOrigen
        End Get
    End Property

    Public Property FechaTransmision() As Date
        Set(ByVal value As Date)
            mvarFechaTrasmision = value
        End Set

        Get
            Return mvarFechaTrasmision
        End Get
    End Property

    Public Property NroImpresion() As Byte
        Set(ByVal value As Byte)
            mvarNroImpresion = value
        End Set
        Get
            Return mvarNroImpresion
        End Get
    End Property

    Public Property RazonSocial() As String
        Set(ByVal value As String)
            mvarRazonSocial = value
        End Set
        Get
            Return mvarRazonSocial
        End Get
    End Property

    Public Property Fecha() As Date
        Set(ByVal value As Date)
            mvarFecha = value
        End Set
        Get
            Return mvarFecha
        End Get
    End Property

    Public Property Numero() As Single
        Set(ByVal value As Single)
            mvarNumero = value
        End Set
        Get
            Return mvarNumero
        End Get
    End Property


End Class
