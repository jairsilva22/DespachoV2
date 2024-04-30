<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.usuarios ORDER BY Nombre ASC" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.tipodepago ORDER BY rango ASC" 
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.bancos ORDER BY nombreBanco ASC" 
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<script language="JavaScript" src="overlib_mini.js"></script>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
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
.selectColor {
    background-color: white;
    color: black;
}
-->
</style>
<!-- InstanceBeginEditable name="head" -->

<style type="text/css">
#form1 pss {
	font-weight: bold;
}
#form1 p {
	font-size: 16pt;
	text-align: left;
}
</style>
    <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
<!-- InstanceEndEditable -->
</head>

<body style="background-color:white">
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td>
      <!-- InstanceBeginEditable name="EditRegion1" -->
      <p>&nbsp;</p>
      <form action="repcaptura.asp" method="post" name="form1" target="_blank" id="form1">
        <div id="overDiv" style="position:absolute; visibility:hidden; z-index:10;"></div>               
        
        <table width="600" border="0" cellspacing="0" cellpadding="0" align="center">
          <tr>
            <td align="center"><p>Filtro  para reporte de captura de fichas de deposito</p></td>
          </tr>
        </table>
        <p>&nbsp;</p>
        <table width="411" border="1" align="center">
          <tr>
            <td width="90" align="right">Folio:</td>
            <td width="248"><label>
              <input type="text" name="folio" id="folio" />
            </label></td>
          </tr>
          <tr>
            <td align="right">Banco:</td>
            <td><label>
              <select name="banco" id="banco" class="selectColor">
                <option value="">Todos</option>
                <%
While (NOT Recordset3.EOF)
%>
                <option value="<%=(Recordset3.Fields.Item("idbanco").Value)%>"><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
                <%
  Recordset3.MoveNext()
Wend
If (Recordset3.CursorType > 0) Then
  Recordset3.MoveFirst
Else
  Recordset3.Requery
End If
%>
              </select>
            </label></td>
          </tr>
          <tr>
            <td align="right">Tipo de pago:</td>
            <td><label>
              <select name="pago" id="pago" class="selectColor">
                <option value="">Todos</option>
                <%
While (NOT Recordset2.EOF)
%>
                <option value="<%=(Recordset2.Fields.Item("idtipo").Value)%>"><%=(Recordset2.Fields.Item("Tipo").Value)%></option>
                <%
  Recordset2.MoveNext()
Wend
If (Recordset2.CursorType > 0) Then
  Recordset2.MoveFirst
Else
  Recordset2.Requery
End If
%>
              </select>
            </label></td>
          </tr>
          <tr>
            <td align="right">Usuario:</td>
            <td><label>
              <select name="usuario" id="usuario" class="selectColor">
                <option value="">Todos</option>
                <%
While (NOT Recordset1.EOF)
%>
                <option value="<%=(Recordset1.Fields.Item("id").Value)%>"><%=(Recordset1.Fields.Item("Nombre").Value)%></option>
                <%
  Recordset1.MoveNext()
Wend
If (Recordset1.CursorType > 0) Then
  Recordset1.MoveFirst
Else
  Recordset1.Requery
End If
%>
              </select>
            </label></td>
          </tr>
          <tr>
            <td align="right">Papel:</td>
            <td><label>
              <select name="papel" id="papel" class="selectColor">
                <option value="">Todos</option>
                <option value="si">si</option>
                <option value="no">no</option>
              </select>
            </label></td>
          </tr>
          <tr>
            <td height="43" colspan="2" align="center" valign="middle">Fechas de<label>
    <input name="captura" type="radio" id="captura_0" value="Deposito"  />
    Deposito</label>
  <label>
    <input type="radio" name="captura" value="Captura" id="captura_1" />
    Captura</label>
    <label>
    <input type="radio" name="captura" value="sinfecha" id="captura_2" checked="checked"/>
    Todas</label>
    
    </td>
          </tr>
          <tr>
            <td align="right">Fecha inicial:</td>
            <td><label>
              <input name="finicial" type="text" id="finicial" value="<%=Date()%>" />
              <a href="javascript:show_calendar('form1.finicial');" onMouseOver="window.status='Elige fecha'; overlib('Pulsa para elegir fecha del mes actual en el calendario emergente.'); return true;" onMouseOut="window.status=''; nd(); return true;"><img src= "imagenes/cal.gif" alt="" width="24" height="22" border="0" /></a></label></td>
          </tr>
          <tr>
            <td align="right">Fecha final:</td>
            <td><label>
              <input name="ffinal" type="text" id="ffinal" value="<%=Date()%>" />
              <a href="javascript:show_calendar('form1.ffinal');" onMouseOver="window.status='Elige fecha'; overlib('Pulsa para elegir fecha del mes actual en el calendario emergente.'); return true;" onMouseOut="window.status=''; nd(); return true;"><img src= "imagenes/cal.gif" alt="" width="24" height="22" border="0" /></a></label></td>
          </tr>
          <tr>
            <td align="right">Ordenado Por:</td>
            <td><select name="orden" id="orden" class="selectColor">
              <option value="Fecha">Fecha</option>
              <option value="folio">Folio</option>
              <option value="Banco">Banco</option>   
                                      
            </select></td>
          </tr>
          <tr>
            <td align="right">&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td align="right">&nbsp;</td>
            <td><label>
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
<%
Recordset2.Close()
Set Recordset2 = Nothing
%>
<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
