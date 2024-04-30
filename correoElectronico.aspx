<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001" Debug="true"%>
<%@ import Namespace="System.Data" %> 
<%@ import Namespace="System.Data.Odbc" %> 
<%@ Import Namespace="System.Data.SqlClient" %>
<%	
    dim archivopdf as string'Varaible par aguardar la ruta del archivo PDF
    dim archivoxml as string'Varaible par aguardar la ruta del archivo xml
    dim cadena as string 'Variable par aguardar la candena de conexion
    dim pathconfig as string'Variable para guardar le path de la empresa
    dim filepdf as string'Variable para valdiar si tiene arhcivo pdf
    dim credencialsmtp as string 'Varaible para guardar el smtp
    dim credencialcorreo as string'Variable para guardar el correo de las credenciales
    dim credencialpassword as string'Variable para guardar el passworde de las crdenciales
    dim filexml as string'Variable para valdiar si tiene arhcivo xml

    Dim correo As New System.Net.Mail.MailMessage()'Variable para guardar el correo
    Dim smtp As New System.Net.Mail.SmtpClient'Vataible para gaurdar el smtp
    Dim MyConn As System.Data.SqlClient.SqlConnection'Variable para conectarse al albase de datos
    Dim MyCommand As System.Data.SqlClient.SqlCommand'Variable para el comando sql
    Dim MyReader As System.Data.SqlClient.SqlDataReader'Variable para leer el comando
    Dim conn As System.Data.SqlClient.SqlConnection'Variable para conectarse al albase de datos
    Dim comm As System.Data.SqlClient.SqlCommand'Variable para el comando sql
    Dim reader As System.Data.SqlClient.SqlDataReader'Variable para leer el comando

    'cadena = Request.QueryString("cadena")
cadena = ConfigurationManager.ConnectionStrings("cnx").ConnectionString
Try


    MyConn = New System.Data.SqlClient.SqlConnection(cadena)
    '----------Recordse para consultar el path del confimenor---------------------------------------------
    MyConn.Open
    MyCommand = New System.Data.SqlClient.SqlCommand("SELECT * FROM dbo.configmenor where idempresa="&request.QueryString("idempresa"), MyConn)
    MyReader = MyCommand.ExecuteReader()
    'if para validar que hay datos
    if MyReader.read then'MyReader
    archivopdf = MyReader.Item("path")
    archivoxml = MyReader.Item("path")
    credencialsmtp = MyReader.Item("smtp")
    credencialcorreo = MyReader.Item("correo")
    credencialpassword = MyReader.Item("password")
    end if'MyReader
    MyConn.close

    '----------Recordse para consultar la carpeta de la empresa---------------------------------------------
    MyConn.Open
    MyCommand = New System.Data.SqlClient.SqlCommand("SELECT * FROM dbo.sucursales where id="&request.QueryString("idempresa"), MyConn)
    MyReader = MyCommand.ExecuteReader()
    'if para validar que hay datos
    if MyReader.read then 'MyReader
    archivopdf &= "xmlTimbrado\"
    archivoxml &= "xmlTimbrado\"
    end if'MyReader
    MyConn.close

    MyConn.Open
    MyCommand = New System.Data.SqlClient.SqlCommand("SELECT * FROM dbo.log_idCO where id_factura="&request.QueryString("idfactura"), MyConn)
    MyReader = MyCommand.ExecuteReader()
    'if para valdiar que hay datos
    if MyReader.read then 'MyReader
    archivopdf &= MyReader.Item("archivo_pdf")
    archivoxml &= MyReader.Item("nombre_archivo")
    filepdf = MyReader.Item("archivo_pdf").tostring
    filexml = MyReader.Item("nombre_archivo").tostring
    end if'MyReader
    MyConn.close

	correo.From = New System.Net.Mail.MailAddress(credencialcorreo)
    correo.To.Add(Request.form("Para"))
    'if para valdiar si se va ha enviar copia
	if Request.form("C.C") <> "" then
		correo.CC.Add(Request.form("C.C"))
	end if
    
    correo.Subject = Request.form("Asunto")
    'txtTexto.Text &= vbCrLf & vbCrLf & "Fecha y hora GMT: " & DateTime.Now.ToUniversalTime.ToString("dd/MM/yyyy HH:mm:ss")
    correo.Body = Request.form("Comentario") 
    correo.IsBodyHtml = True
    correo.Priority = System.Net.Mail.MailPriority.Normal
	'if para valida si hay archivo PDF
	if filepdf <> "" then'filepdf
	    correo.Attachments.Add(New System.Net.Mail.Attachment(archivopdf))
	end if'filepdf

	''if para valdiar si hay archivo XML
     if filexml <> "" then'filexml
	    correo.Attachments.Add(New System.Net.Mail.Attachment(archivoxml))
	 end if'filexml
		
     '---------------------------------------------
     ' Estos datos debes rellanarlos correctamente
     '---------------------------------------------
     smtp.host=credencialsmtp
     smtp.Credentials = New System.Net.NetworkCredential(credencialcorreo, credencialpassword)
     smtp.Port = 587    
     smtp.EnableSsl = False
     smtp.Send(correo)

     'Se actualiza la factura
     MyConn.Open()
     MyCommand = New SqlCommand("UPDATE factura SET estatusCorreo =  'Enviado' WHERE idfactura = " & Request.QueryString("idfactura"), MyConn)
     MyCommand.ExecuteNonQuery()
    MyConn.close

     Response.Write("Mensaje enviado satisfactoriamente")
Catch ex As Exception		
     'actualizamos el estatus de la cita
    MyConn.close()
     MyConn.Open()
     response.write(ex.Message)
    Dim sqlE as string 
    sqlE = "UPDATE factura SET estatusCorreo =  'Error' WHERE idfactura = " & Request.QueryString("idfactura")
     MyCommand = New SqlCommand(sqlE, MyConn)
     MyCommand.ExecuteNonQuery()
     MyConn.Close()
     Response.Write(ex.ToString())      
End Try

response.redirect("factura.asp")
			
%>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body>
</body>
</html>
