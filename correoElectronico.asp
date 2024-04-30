<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idCliente") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idCliente")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idCliente = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
<!-- InstanceEndEditable -->
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
<!-- InstanceBeginEditable name="head" --><!-- InstanceEndEditable -->
</head>

<body>
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    
  <tr>
  
    <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <form name="form1" method="post" action="correoElectronico.aspx?idfactura=<%=request.querystring("idfactura")%>&idempresa=<%=Request.Cookies("login")("idSucursal")%>&cadena=<%=cadena%>">
        <table width="50" border="1" align="center" cellspacing="1">
          <tr>
            <th colspan="2" align="center" scope="col">Correo Electronico</th>
          </tr>
          <tr>
            <td width="141" align="right">Para:</td>
            <td width="212"><label>
              <input name="Para" type="text" id="Para" value="<%=(Recordset1.Fields.Item("correo").Value)%>" size="50">
            </label></td>
          </tr>
          <tr>
            <td align="right">C.C:</td>
            <td width="212"><label>
              <input name="C.C" type="text" id="C.C" size="50">
            </label></td>
          </tr>
          <tr>
            <td align="right">Asunto: </td>
            <td width="212"><label>
              <input name="Asunto" type="text" id="Asunto" size="50" value="Facturacion">
            </label></td>
          </tr>
          <tr>
            <td align="right">Comentarios:</td>
            <td width="212"><label>
              <textarea name="Comentario" id="Comentario" cols="45" rows="5"></textarea>
            </label></td>
          </tr>
          <tr>
            <td colspan="2" align="center"><label>
              <input type="submit" name="bttnEnviar" id="bttnEnviar" value="Enviar">
            </label><input name="cadena" type="hidden" value="<%=cadena%>" /></td>
          </tr>
        </table>
      </form>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <!-- InstanceEndEditable -->
 <p>&nbsp;</p>
      <%=footerPage%>
    </td>
  </tr>
 
  
</table>
      
</body>
<!-- InstanceEnd --></html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
