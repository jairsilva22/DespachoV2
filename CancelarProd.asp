<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="stylo2.asp"-->
<%
    if Request.Querystring("folio") <> "" then
    folio = Request.Querystring("folio")
    else 
    folio = 0
    end if
'validamos si el folio es de una factura o no
Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT clave, descripcion, cantidad, fechaDev FROM dbo.cancelarProductos WHERE folio = "&folio&" AND tipo = '"&Request.Querystring("tipo")&"'"
Recordset2_cmd.Prepared = true
Set Recordset2 = Recordset2_cmd.Execute

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
<!-- InstanceBeginEditable name="head" -->

<!-- InstanceEndEditable -->
</head>

<body>
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
   
  <tr>
  
    <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
        <p>
          <h3 align="center">Éstos productos se devolvieron al inventario</h3>
        </p>
        <table align="center" width="90%">
          <tr bgcolor="<%=ctabla%>" align="center">
            <td>Clave</td>
            <td>Descripcion</td>
            <td>Cantidad</td>
            <td>Fecha</td>
          </tr>
          <%While(Not Recordset2.EOf)%>
          <tr>
            <td><%=Recordset2.Fields.Item("clave").Value%></td>
            <td><%=Recordset2.Fields.Item("descripcion").Value%></td>
            <td align="right"><%=Recordset2.Fields.Item("cantidad").Value%></td>
            <td align="center"><%=Recordset2.Fields.Item("fechaDev").Value%></td>
          </tr>
          <%  Recordset2.MoveNext()
            Wend%>
        </table>
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
Recordset2.Close()
Set Recordset2 = Nothing
%>
