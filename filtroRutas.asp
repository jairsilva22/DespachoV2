<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.rutascobro" 
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.documento ORDER BY descripcion ASC" 
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = 10
Repeat1__index = 0
Recordset2_numRows = Recordset2_numRows + Repeat1__numRows
%>
<%
dim desactivar'Variable para desactivar el menu
dim sql'Variable para guardar el comando de la buesqueda
dim rutaf'Variable para guardar la ruta que se filtra

'Incializar variables

'/////////////////////////////////

'if para validar que se filtra una ruta
if request.Form("Ruta")<>"" THEN'Ruta
rutaf = " AND LogRutas.idrutascobro="&request.Form("ruta")
END IF'Ruta

'if para validar el select
if Request.Form("RadioGroup1") = "Cliente" then'sql
desactivar = "disabled=disabled"
sql = "SELECT * FROM dbo.clientesFacturacion WHERE clientesFacturacion.idCliente > 0 ORDER BY nombreCliente ASC"
else'sql
sql = "SELECT * FROM dbo.clientesFacturacion, dbo.LogRutas WHERE clientesFacturacion.idCliente > 0 AND clientesFacturacion.idCliente = LogRutas.idcliente " & rutaf & " ORDER BY nombreCliente ASC"
end if'sql

Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = sql

Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0

Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT DISTINCT(YEAR(fechaAlta)) AS fecha FROM factura WHERE fechaAlta IS NOT NULL ORDER BY fecha DESC"
Recordset4_cmd.Prepared = true

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0

%>
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp" -->
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

<!-- InstanceEndEditable -->
</head>

<body style="background-color:white;">
<table width="1024" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
     <tr>
    <td>

<form id="form1" name="form1" method="post" action="reporteRutas2.asp">
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <table width="703" border="0" align="center">
    <tr>
      <td align="right">Buscar por</td>
      <td><p>
        <label>
          <input <%If (CStr(Request.Form("RadioGroup1")) = CStr("Ruta")) Then Response.Write("checked=""checked""") : Response.Write("")%> name="RadioGroup1" type="radio" id="RadioGroup1_0" value="Ruta" checked="checked" onClick="filtro()"/>
          Ruta</label>
        <label>
<input <%If (CStr(Request.Form("RadioGroup1")) = CStr("Cliente")) Then Response.Write("checked=""checked""") : Response.Write("")%> type="radio" name="RadioGroup1" value="Cliente" id="RadioGroup1_1" onClick="filtro()"/>
          Cliente</label>
        <br />
      </p></td>
    </tr>
    <tr>
      <td align="right">RUTA:</td>
      <td><select name="Ruta" onChange="filtro(form1)" <%=desactivar%>>
      <option value="">Todas</option>
        <%
While (NOT Recordset1.EOF)
%>
        <option value="<%=(Recordset1.Fields.Item("id").Value)%>" <%If (Not isNull(request.form("Ruta"))) Then If (CStr(Recordset1.Fields.Item("id").Value) = CStr(request.form("Ruta"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset1.Fields.Item("ruta").Value)%></option>
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
      <td align="right">CLIENTES ASIGNADOS A ESTA RUTA:</td>
      <td><select name="CTES" >
        <option value="TODOS">TODOS</option>
        <%
While (NOT Recordset2.EOF)
%>
        <option value="<%=(Recordset2.Fields.Item("idCliente").Value)%>"><%=(Recordset2.Fields.Item("nombreCliente").Value)%></option>
        <%
  Recordset2.MoveNext()
Wend
If (Recordset2.CursorType > 0) Then
  Recordset2.MoveFirst
Else
  Recordset2.Requery
End If
%>
      </select></td>
    </tr>
    <tr>
      <td align="right"> FOLIO:</td>
      <td><input name="foliotext" type="text" id="textfield" value="<%=request.form("foliotext")%>" size="10" /></td>
    </tr>
    <tr>
      <td align="right">MES:</td>
      <td><select name="mes" id="mes">
        <option value="0">Todos</option>
        <%for i = 1 to 12%>
        <option value="<%=i%>" <%If (Not isNull(i)) Then If (m = (i)) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=MonthName(i)%></option>
        <%next%>
      </select></td>
    </tr>
    <tr>
      <td align="right">AÑO:</td>
      <td><select name="ano" id="ano">
        <%While (NOT Recordset4.EOF)%>
        <option value="<%=(Recordset4.Fields.Item("fecha").Value)%>"><%=(Recordset4.Fields.Item("fecha").Value)%></option>
        <%  Recordset4.MoveNext()
          Wend%>
      </select></td>
    </tr>
    <tr>
      <td align="right">Tipo de documento:</td>
      <td><select name="documento" id="documento">
        <%for i = 1 to 12%>
        <%next%>
        <%
While (NOT Recordset3.EOF)
%>
        <option value="<%=(Recordset3.Fields.Item("iddocumento").Value)%>"><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
        <%
  Recordset3.MoveNext()
Wend
If (Recordset3.CursorType > 0) Then
  Recordset3.MoveFirst
Else
  Recordset3.Requery
End If
%>
      </select></td>
    </tr>
    <tr>
      <td align="right">ESTATUS DE DOCUMENTO</td>
      <td><select name="ST" id="select">
        <option value="Facturada" <%If (Not isNull(request.form("ST"))) Then If ("Facturada" = CStr(request.form("ST"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Facturada</option>      
        <option value="Todas" <%If (Not isNull(request.form("ST"))) Then If ("Todas" = CStr(request.form("ST"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todas</option>
        <option value="Cancelada" <%If (Not isNull(request.form("ST"))) Then If ("Cancelada" = CStr(request.form("ST"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Cancelada</option>
      </select></td>
    </tr>
    <tr>
      <td align="right">ESTATUS DE COBRANZA</td>
      <td><select name="pago" id="select2">
        <option value="Todas" <%If (Not isNull(request.form("pago"))) Then If ("Todas" = CStr(request.form("pago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Todas</option>
        <option value="No pagada" <%If (Not isNull(request.form("pago"))) Then If ("No pagada" = CStr(request.form("pago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>No pagada</option>
        <option value="Pendiente" <%If (Not isNull(request.form("pago"))) Then If ("Pendiente" = CStr(request.form("pago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pendiente</option>
        <option value="Pagada" <%If (Not isNull(request.form("pago"))) Then If ("Pagada" = CStr(request.form("pago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pagada</option>
        <option value="Pagada parcial" <%If (Not isNull(request.form("pago"))) Then If ("Pagada parcial" = CStr(request.form("pago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Pagada parcial</option>
      </select></td>
    </tr>
    <tr>
      <td align="right">&nbsp;</td>
      <td><input type="submit" name="button" id="button" value="Enviar" /></td>
    </tr>
    <tr>
      <td align="right">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <p>&nbsp; </p>
</form>
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
Recordset3.Close()
Set Recordset3 = Nothing
%>
<SCRIPT language="javascript">
function filtro()
		{
			document.form1.action="filtroRutas.asp";
			document.form1.submit();
	
			}
</script>
