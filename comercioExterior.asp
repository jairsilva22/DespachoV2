<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp" -->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfdadmin.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
<!-- InstanceEndEditable -->
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
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
     
  <tr>
  
    <td>
 

<!-- InstanceBeginEditable name="EditRegion3" -->
       <%If Request.QueryString("pagina") = "fac" Then %>
      <iframe src="ComercioExtFactura.aspx?id=<%=Request.QueryString("id")%>&idempresa=<%=Request.QueryString("idempresa")%>&idcliente=<%=Request.QueryString("idcliente")%>"
          width="1200" height="800" frameborder="0"></iframe>
       <%Else %>
      <iframe src="UnidadComExt.aspx"
          width="1200" height="800" frameborder="0"></iframe>
       <% End If %>
      <!-- InstanceEndEditable --></div>
  	</td>
  </tr>
</table>
<%=footerPage%>
</body>
<!-- InstanceEnd --></html>