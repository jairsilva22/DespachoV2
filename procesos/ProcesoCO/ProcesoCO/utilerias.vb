Imports System.IO
Imports System.Data.SqlClient

Public Class utilerias
    Dim cadena As String
    Dim vTiempo As String 'Variable para guardar el tiempo de los timer

    Public Property tiempo() As String 'tiempo del timer
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return vTiempo
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                vTiempo = Value
            End If
        End Set
    End Property

    Public Property cadenaConexion() As String 'cadena de conexion
        ' la parte Get es la que devuelve el valor de la propiedad
        Get
            Return cadena
        End Get
        ' la parte Set es la que se usa al asignar el nuevo valor
        Set(ByVal Value As String)
            If Value <> "" Then
                cadena = Value
            End If
        End Set
    End Property

    'para truncar a 6 decimales
    Function truncarAseis(ByVal valor)
        Dim digitos As String 'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 6)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAseis = valor
    End Function

    'truncar a 2 digitos
    Function truncarAdos(ByVal valor)
        Dim digitos As String 'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 2)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAdos = valor
    End Function

    'funcion redondear a 2 digitos
    Function redondear(ByVal val As Double)

        'if para validar si tiene digitos
        If InStr(val, ".") <> 0 Then
            'redondear la funcion
            val = Math.Round(val, 2)
        Else
            val = val & ".00"
        End If
        redondear = val
    End Function


    'Ejecuta una consulta SSQL contra la Base de datos, INSERT UPDATE DELETE
    Public Function EjecutaQuery(ByVal sSQL As String, ByVal sStringConexion As String) As Boolean
        Dim oConn As New SqlConnection(sStringConexion)
        Dim iCant As Integer
        Try
            oConn.Open()
            Dim oCmd As New SqlCommand(sSQL, oConn)
            iCant = oCmd.ExecuteNonQuery()
            Return False
        Catch ex As Exception
            'ErrorPopUp(Now & vbCrLf & ex.Message.ToString & vbCrLf & sSQL)
            Return True
        Finally
            oConn.Close()
        End Try
    End Function

    Public Function consultas(ByVal sSQL As String, ByVal sStringConexion As String) As DataTable
        Dim datos As New DataTable 'Variable para guardar los registros    
        Dim consulta As New SqlDataAdapter(sSQL, sStringConexion) 'Variable para ejecutar la consulta
        Try
            consulta.Fill(datos)
            If datos.Rows.Count > 0 Then
                Return datos
            Else
                Return datos
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            consulta = Nothing
            datos = Nothing
        End Try
    End Function

    Public Function configuraciones(ByVal comparacion As String) As String
        Dim objReader As New StreamReader(My.Application.Info.DirectoryPath & "\config.txt") 'Variable para guardar el archivo
        Dim sLine As String 'Variable para guardar la linea
        Dim arrText As New ArrayList() 'Array para guardar las lineas
        'Llenar el array con las lineas del archivo de configuraciones
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
        'for para recorrer el array con las lineas del archivo de configuraciones
        For i As Integer = 0 To arrText.Count - 1 'arrText
            'if para validar que tiene cadena de conexion
            If InStr(1, arrText(i), comparacion) > 0 Then 'CADENA
                Return arrText(i + 1)
            End If 'CADENA
            'if para validar que tiene ruta de archivo de facturas
            If InStr(1, arrText(i), comparacion) > 0 Then 'RUTA
                Return arrText(i + 1)
            End If 'RUTA
        Next 'arrText    
    End Function
End Class
