<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT DISTINCT Year(fechacfd) as yearemiso FROM dbo.factura WHERE estatus = 'Facturada' ORDER BY Year(fechacfd) DESC" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT DISTINCT (vendedor) FROM dbo.factura WHERE estatus = 'Facturada' AND ASN <> '' ORDER BY vendedor" 
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
    <script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
<!-- InstanceBeginEditable name="doctitle" -->
<title><%=titlePage%></title>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
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
.selectColor {
    background-color: white;
    color: black;
}
-->
</style>
<!-- InstanceBeginEditable name="head" -->

<!-- InstanceEndEditable -->
</head>

<body style="background-color:white;">
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
      <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <table width="50%" border="0" align="center">
        <tr align="center">
          <td>Seleccione el mes y el año de emision y vendedor</td>
        </tr>
      </table>
      <form action="repComision.asp" method="post" name="form1" target="_blank" id="form1">
        <p>&nbsp;</p>
        <table width="43%" border="0" align="center">
          <tr>
            <td width="45%" align="right">Mes de emisión </td>
            <td width="11%">&nbsp;</td>
            <td width="44%"><select name="mes" id="mes" class="selectColor">
              <%for i = 1 to 12%>
              <option value="<%=i%>" <%If i = Month(Date()) Then Response.Write("selected") End If%>><%=MonthName(i)%></option>
              <%next%>
            </select></td>
          </tr>
          <tr>
            <td align="right">Año de emisión </td>
            <td>&nbsp;</td>
            <td><select name="yearemiso" id="yearemiso" class="selectColor">
              <%While (NOT Recordset1.EOF)%>
              <option value="<%=(Recordset1.Fields.Item("yearemiso").Value)%>"><%=(Recordset1.Fields.Item("yearemiso").Value)%></option>
              <%
  Recordset1.MoveNext()
Wend
If (Recordset1.CursorType > 0) Then
  Recordset1.MoveFirst
Else
  Recordset1.Requery
End If
%>
            </select></td>
          </tr>
          <tr>
            <td align="right">Vendedor</td>
            <td align="center">&nbsp;</td>
            <td align="left" class="Estilo1">
            <select name="vendedor" id="vendedor" class="selectColor">
              <%While(Not Recordset2.EOF)%>
              <option value="<%=Recordset2.Fields.Item("vendedor").Value%>"><%=Recordset2.Fields.Item("vendedor").Value%></option>
              <%  Recordset2.MoveNext
              Wend%>
            </select></td>
          </tr>
          <tr>
            <td height="52" colspan="3" align="center"><label>
              <input type="submit" name="button" id="button" value="Enviar" />
            </label></td>
          </tr>
        </table>
        <p>&nbsp;</p>
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
