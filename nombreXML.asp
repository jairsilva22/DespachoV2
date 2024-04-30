<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<%
folio = request.form("folio") + 1
response.Write(folio)
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.factura WHERE folio = " & folio
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
</head>

<body>
    <% 
IF recordset1.EOF THEN
response.write("EL FOLIO NO EXISTE")
    else
Dim parte1
dim parte2
dim parte3
dim parte4
parte1="2011"
parte2=request.form("folio")
parte3=recordset1.fields.item("idfactura").value
parte4="A"
parte5="FA"
'response.write(request.form("folio"))

Dim archivo
archivo=parte1&"_"&parte2&parte4&"_"&parte5&"_"&parte3&"CO.XML"

response.write(archivo)
response.redirect("leerXML.asp?nombre="&archivo)
    END IF
    %>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
