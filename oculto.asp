<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows
color = cgrid2

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
  Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
  Recordset1_cmd.CommandText = "SELECT idCliente, nombreCliente FROM dbo.clientesFacturacion WHERE nombreCliente COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%"&Trim(Request.Form("elCliente"))&"%' ORDER BY clientesFacturacion.idempresa, nombreCliente ASC" 
  'response.Write(Recordset1_cmd.CommandText)
  Recordset1_cmd.Prepared = true

  Set Recordset1 = Recordset1_cmd.Execute
%>
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>&nbsp;</title>
</head>

<body>
<% If Not Recordset1.EOF Then%>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
  <tr bgcolor="<%=ctabla%>">
  	<td>Nombre</td>
    <td>Seleccionar</td>
  </tr>
<%  While(Not Recordset1.EOF)
		'if para validar el cambio del color
		if color = cgrid2 then
			color = cgrid1
		else
			color = cgrid2
		end if
%>
  <tr bgcolor="<%=color%>">
    <td><%=(Recordset1.Fields.Item("nombreCliente").Value)%></td>
    <td align="center"><input name="ok" type="radio" id="id" value="<%=(Recordset1.Fields.Item("idCliente").Value)%>"></td>
  </tr>
  	<%	Recordset1.MoveNext()
	 Wend%>
</table>
<%End If%>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
