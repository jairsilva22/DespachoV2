<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="stylo2.asp" -->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idCliente, nombreCliente FROM dbo.clientesFacturacion WHERE idCliente > 0 AND idEmpresa = "&Request.Cookies("login")("idSucursal")&" ORDER BY nombreCliente ASC"
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<!Doctype html>
<html>
	<head>
		<title></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
		<style type="text/css">
		<!--
		body {
			background-color: #CCC;
			margin-left: 0px;
			margin-top: 0px;
			margin-right: 0px;
			margin-bottom: 0px;
		}
		.Estilo1 {color: #000000}
		.Estilo7 {font-size: 9px}
		.Estilo11 {
			font-weight: bold;
			color: #000000;
			font-size: 14px;
		}
		.template1 {
			font-size: 12px;
		}
		.inicio {
			background-position: center;
			height: auto;
			width:800px;
		}
		-->
		</style>
	</head>
	<body style="background-color:white;">
		<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
	     <tr>
		<td>
	      <p>&nbsp;</p>
	      <form action="reporteAdeudos.asp" method="POST" target="_blank" name="form1", id="form1">
	      	<table align="center">
	      		<tr>
	      			<td>Cliente:</td>
	      			<td><select name="cliente" id="cliente">
	      				<option value="">Seleccionar</option>
	      				<%While(Not Recordset1.EOF)%>
	      				<option value="<%=Recordset1.Fields.Item("idCliente").Value%>"><%=Recordset1.Fields.Item("nombreCliente").Value%></option>
	      				<%	Recordset1.MoveNext()
	      				Wend%>
	      			</select>
	      			</td>
	      		</tr>
	      		<tr align="center">
	      			<td colspan="2"><input type="submit" value="Enviar"></td>
	      		</tr>
	      	</table>
	      </form>
	      <!-- InstanceEndEditable -->
		  	  <p>&nbsp;</p>
		      <%=footerPage%>
		    </td>
		  </tr>  
		</table>
	</body>
</html>