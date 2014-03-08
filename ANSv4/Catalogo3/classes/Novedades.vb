Option Explicit On

Public Class Novedades

    'variable local para contener colección
    Private mCol As Collection

    Public Function Add(ByVal Key As String, ByVal F_Carga As Date, ByVal Novedad As String, ByVal ID As Long, Optional ByVal sKey As String = "") As ClientesNovedades
        'crear un nuevo objeto
        Dim objNewMember As ClientesNovedades
        objNewMember = New ClientesNovedades


        'establecer las propiedades que se transfieren al método
        objNewMember.Key = Key
        objNewMember.F_Carga = F_Carga
        objNewMember.Novedad = Novedad
        objNewMember.ID = ID
        If Len(sKey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, sKey)
        End If

        'devolver el objeto creado
        Add = objNewMember
        objNewMember = Nothing

    End Function

    Public Function Item(ByVal vntIndexKey As Object) As ClientesNovedades
        'se usa al hacer referencia a un elemento de la colección
        'vntIndexKey contiene el índice o la clave de la colección,
        'por lo que se declara como un Variant
        'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
        Return mCol(vntIndexKey)
    End Function


    Public ReadOnly Property Count() As Long
        Get
            Return mCol.Count
        End Get
    End Property


    Public Sub Remove(ByVal vntIndexKey As Object)
        'se usa al quitar un elemento de la colección
        'vntIndexKey contiene el índice o la clave, por lo que se
        'declara como un Variant
        'Sintaxis: x.Remove(xyz)


        mCol.Remove(vntIndexKey)
    End Sub



    'diego    Private Sub Class_Initialize()
    'diego 'crea la colección cuando se crea la clase
    'diego mCol = New Collection
    'diego End Sub

    'diego     Private Sub Class_Terminate()
    'diego 'destruye la colección cuando se termina la clase
    'diego mCol = Nothing
    'diego End Sub


End Class
