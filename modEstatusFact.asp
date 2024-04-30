<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<%

'volver a procesar'
Set recordsetProcesar = Server.CreateObject ("ADODB.Command")
recordsetProcesar.ActiveConnection = MM_Conecta1_STRING

recordsetProcesar.CommandText = "UPDATE dbo.Factura set estatus='Pendiente' WHERE idFactura = '"&Request.queryString("idfac")&"'"
recordsetProcesar.Execute
recordsetProcesar.ActiveConnection.Close

response.redirect("Factura.asp")
%>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title></title>    
</head>
<body>
      <button>ssss</button>
</body>
</html>

<%
recordsetProcesar.Close()
Set recordsetProcesar = Nothing
%>
