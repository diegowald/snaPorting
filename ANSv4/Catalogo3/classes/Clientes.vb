Option Explicit On

Public Class Clientes

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsClientes"

    'variables locales para almacenar los valores de las propiedades
    Private mvarIdCliente As Long 'copia local
    Private mvarRazonSocial As String 'copia local
    Private mvarCuit As String 'copia local
    Private mvarDomicilio As String 'copia local
    Private mvarTelefono As String 'copia local
    Private mvarEmail As String 'copia local
    Private mvarCiudad As String 'copia local
    Private mvarObservaciones As String 'copia local
    'diego    Private mvarNovedades As Novedades

    'diego    Private Sub Class_Terminate()
    'diego  mvarNovedades = Nothing
    'diego    End Sub

    'diego Public Property Get Novedades() As Novedades
    'diego     If mvarNovedades Is Nothing Then
    'diego  Set mvarNovedades = New Novedades
    'diego     End If

    'diego     Set Novedades = mvarNovedades
    'diego  End Property

    'diego Public Property Set Novedades(vData As Novedades)
    'diego     Set mvarNovedades = vData
    'diego End Property

    Public Property Observaciones() As String
        Get
            Return mvarObservaciones
        End Get
        Set(ByVal value As String)
            mvarObservaciones = value
        End Set
    End Property

    Public Property Ciudad() As String
        Set(ByVal value As String)
            mvarCiudad = value
        End Set

        Get
            Return mvarCiudad
        End Get
    End Property

    Public Property EMail() As String
        Set(ByVal value As String)
            mvarEmail = value
        End Set
        Get
            Return mvarEmail
        End Get
    End Property

    Public Property Telefono() As String
        Set(ByVal value As String)
            mvarTelefono = value
        End Set
        Get
            Return mvarTelefono
        End Get
    End Property

    Public Property Domiciliio() As String
        Set(ByVal value As String)
            mvarDomicilio = value
        End Set
        Get
            'se usa al recuperar un valor de una propiedad, en la parte derecha de una asignación.
            'Syntax: Debug.Print X.Domicilio
            Return mvarDomicilio
        End Get
    End Property

    Public Property Cuit() As String
        Set(ByVal value As String)
            mvarCuit = value
        End Set
        Get
            Return mvarCuit
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

    Public Property IdCliente() As Long
        Set(ByVal value As Long)
            mvarIdCliente = value
        End Set

        Get
            Return mvarIdCliente
        End Get
    End Property

    Public Sub Leer(ByVal IdCliente As Long)

        On Error GoTo ErrorHandler

        'diego     Dim adoREC As New ADODB.Recordset

        'diego     adoREC = adoModulo.xGetRSMDB(vg.Conexion, "tblClientes", "ID=" & CStr(IdCliente), "NONE")
        'diego  If Not (adoREC.BOF) And Not (adoREC.EOF) Then
        'diego  mvarRazonSocial = IIf(IsNull(adoREC!RazonSocial), " ", Trim(adoREC!RazonSocial))
        'diego         mvarCuit = IIf(IsNull(adoREC!Cuit), " ", Format(adoREC!Cuit, "00-00000000-0"))
        'diego  mvarDomicilio = IIf(IsNull(adoREC!Domicilio), " ", Trim(adoREC!Domicilio))
        'diego         mvarTelefono = IIf(IsNull(adoREC!Telefono), " ", Trim(adoREC!Telefono))
        'diego         mvarEmail = IIf(IsNull(adoREC!Email), " ", Trim(adoREC!Email))
        'diego         mvarCiudad = IIf(IsNull(adoREC!Ciudad), " ", Trim(adoREC!Ciudad))
        'diego         mvarObservaciones = IIf(IsNull(adoREC!Observaciones), " ", Trim(adoREC!Observaciones))

        'diego  adoREC = adoModulo.xGetRSMDB(vg.Conexion, "tblClientesNovedades", "IdCliente=" & CStr(IdCliente), "NONE")
        'diego         If Not (adoREC.BOF) And Not (adoREC.EOF) Then
        'diego While Not adoREC.EOF
        'diego         Novedades.Add("_" & Novedades.Count + 1, adoREC!F_Carga, adoREC!Novedad, adoREC!ID)
        'diego  adoREC.MoveNext()
        'diego         End While
        'diego  End If
        'diego         End If
        'diego  adoREC = Nothing

        Exit Sub

ErrorHandler:

        'diego         adoREC = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Sub

End Class
