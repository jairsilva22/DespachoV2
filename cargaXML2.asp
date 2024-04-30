<!--#include file="Connections/Conecta1.asp" -->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
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


<%'response.write(request.QueryString("archivo"))%>
<!-- InstanceEndEditable -->
</head>

<body style="background-color:white;">
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
     <tr>
         <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
<form action="nombreXML.asp" method="post" name="form1" id="form1">
  <p align="center">CARGAR ARCHIVO PARA RECUPERAR FACTURA</p>
  <p align="center">
    <label for="textfield"></label>
  </p>
  <table width="217" border="0" align="center">
    <tr>
      <td>FOLIO
        <label for="textfield2"></label>
      <input type="text" name="folio" id="folio" /></td>
    </tr>
    <tr>
      <td><input type="submit" name="Submit" id="button" value="Enviar" /></td>
    </tr>
  </table>
  <p>&nbsp;</p>
</form>
<!-- InstanceEndEditable -->
 <p>&nbsp;</p>
      <%=footerPage%>
    </td>
  </tr>
 
  
</table>
      
</body>
<!-- InstanceEnd --></html>
<script language="javascript">
function enviar(archivo)
{
	alert("el archivo ha sido cargado correctamente");
	document.form1.action = "cargaXML.asp?archivo="+archivo.value
    document.form1.submit(); 
	}
</script>