Option Explicit On

Public Class ClientesNovedades

    Public Key As String

    'variables locales para almacenar los valores de las propiedades
    Private mvarF_Carga As Date 'copia local
    Private mvarNovedad As String 'copia local
    Private mvarID As Long 'copia local

    Public Property ID() As Long
        Set(ByVal value As Long)
            mvarID = value
        End Set

        Get
            Return mvarID
        End Get
    End Property

    Public Property Novedad() As String
        Set(ByVal value As String)
            mvarNovedad = value
        End Set
        Get
            Return mvarNovedad
        End Get
    End Property

    Public Property F_Carga() As Date
        Set(ByVal value As Date)
            mvarF_Carga = value
        End Set
        Get
            Return mvarF_Carga
        End Get
    End Property


End Class
