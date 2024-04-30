<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="stylo2.asp"-->
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

If Request.Form("cliente") <> "" Then
	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "SELECT idCliente, nombreCliente FROM dbo.clientesFacturacion WHERE nombreCliente COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%"&Trim(Request.Form("cliente"))&"%' ORDER BY nombreCliente ASC"
	Recordset2_cmd.Prepared = true

	Set Recordset2 = Recordset2_cmd.Execute
	Recordset2_numRows = 0
End If
%>
<!Doctype html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>Buscar Clientes</title>
		<script type="text/javascript">
		  function enviar(id, nombre)
		  {
		  	window.opener.form1.clientes.value = id
		  	window.opener.document.getElementById("cliente").innerHTML = "<strong>"+ nombre +"</strong>&nbsp;<a href='javascript:borrar()'><img src='imagenes/tacha.gif' title='Borrar' width='20' height='20' border='0' /></a>"
		  	window.close()
		  }
		</script>
	</head>
	<body>
		<p>&nbsp;</p>
		<h2 align="center">Buscar Clientes</h2>

		<form action="clienteFactura.asp" name="form1" method="post">
			<table align="center">
				<tr>
					<td align="right">Nombre del Cliente:</td>
				    <td><input type="text" name="cliente" size="32" value="<%=Request.Form("cliente")%>"></td>
				</tr>
				<tr>
					<td align="center" colspan="2"><input type="submit" name="boton" value="Buscar" onclick="valida()" /></td>
				</tr>
			</table>
		</form>
		<p>&nbsp;</p>
		<%If Request.Form("cliente") <> "" Then
			If Not Recordset2.EOF Then %>
		<table align="center" width="90%" border="0">
			<tr align="center"bgcolor="<%=ctabla%>">
				<td>Seleccionar</td>
				<td>Nombre</td>
			</tr>
			<%While(Not Recordset2.EOF)
				'if para validar el cambio del color
				if color = cgrid2 then'color
					color = cgrid1
				else'color
					color = cgrid2
				end if'color%>
			<tr bgcolor="<%=color%>">
				<td align="center"><a href="javascript:enviar(<%=Recordset2.Fields.Item("idCliente").Value%>, '<%=Replace(Recordset2.Fields.Item("nombreCliente").Value, "'", "")%>')"><img src="imagenes/Arrow-right.png" width="20" height="20" alt=""></a></td>
				<td><%=Recordset2.Fields.Item("nombreCliente").Value%></td>
			</tr>
			<%	Recordset2.MoveNext
			Wend%>
		</table>
		<%End If
		End If%>
	</body>
</html>