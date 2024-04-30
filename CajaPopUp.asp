<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="stylo2.asp"-->
<%
dim pagos'Variable para guardar los que se ha pagado
dim estcobranza'Varaible para guardar el estatus de pago
dim folio'Variable para guardar el folio de la factura
dim monto'Variable para guardar el total de la factura
dim saldo'Variable para guardar el saldo de la factura
dim bandera'Variable para validar si no exoste la factura
dim pagado'Variable para guardar cuanto se ha pagado

monto = 0
pagado = 0
saldo = 0
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.bancos ORDER BY nombrebanco ASC" 
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
Dim Recordset3__MMColParam
Recordset3__MMColParam = "000000000"
If (Request.Form("folio") <> "") Then 
  Recordset3__MMColParam = Request.Form("folio")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.factura WHERE folio = ?" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim MM_editAction
MM_editAction = CStr(Request.ServerVariables("SCRIPT_NAME"))
If (Request.QueryString <> "") Then
  MM_editAction = MM_editAction & "?" & Server.HTMLEncode(Request.QueryString)
End If

' boolean to abort record edit
Dim MM_abortEdit
MM_abortEdit = false
%>
<%
' IIf implementation
Function MM_IIf(condition, ifTrue, ifFalse)
  If condition = "" Then
    MM_IIf = ifFalse
  Else
    MM_IIf = ifTrue
  End If
End Function
%>
<%
'if para valdiar que se mando insertar
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
  pagos = CDbl(Request.Form("pagos")) + Cdbl(Request.Form("total"))
  
  if pagos >= Request.Form("monto") then
  estcobranza = "Pagada"
  else
  estcobranza = "Pagada parcial"
  end if
  
  Set RSFactura_cmd = Server.CreateObject ("ADODB.Command")
RSFactura_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactura_cmd.CommandText = "UPDATE dbo.factura SET estcobranza = ? WHERE folio = ?" 
RSFactura_cmd.Prepared = true
RSFactura_cmd.Parameters.Append RSFactura_cmd.CreateParameter("param1", 201, 1, 255, estcobranza) ' adLongVarChar
RSFactura_cmd.Parameters.Append RSFactura_cmd.CreateParameter("param2", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble

Set RSFactura = RSFactura_cmd.Execute   
  End If
End If
%>
<%
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the insert
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.logCXC (folio, tipo_pago, banco, refecencia, cantidad_recibida, monto, cambio, saldo, observaciones, total) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, MM_IIF(Request.Form("folio"), Request.Form("folio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 5, 1, -1, MM_IIF(Request.Form("tipo_pago"), Request.Form("tipo_pago"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 5, 1, -1, MM_IIF(Request.Form("banco"), Request.Form("banco"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 201, 1, 255, Request.Form("refecencia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 5, 1, -1, MM_IIF(Request.Form("cantidad_recibida"), Request.Form("cantidad_recibida"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 5, 1, -1, MM_IIF(Request.Form("monto"), Request.Form("monto"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 5, 1, -1, MM_IIF(digi(Request.Form("cambio")), digi(Request.Form("cambio")), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 5, 1, -1, MM_IIF(Request.Form("saldo"), Request.Form("saldo"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 201, 1, 8000, Request.Form("observaciones")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 5, 1, -1, MM_IIF(Request.Form("total"), Request.Form("total"), null)) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "CajaPopUp.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If
End If
%>
<%
'if para validar que existe
if NOT Recordset3.EOF then'Recordset3
bandera = "si"
folio = (Recordset3.Fields.Item("folio").Value)
monto = (Recordset3.Fields.Item("total").Value)

'-----------------Recordset para consultar los archivos xml y pdf de la factura---------------------------------
Set RSMonto_cmd = Server.CreateObject ("ADODB.Command")
RSMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RSMonto_cmd.CommandText = "SELECT SUM(total) as pagado FROM dbo.logCXC WHERE folio = " & folio
RSMonto_cmd.Prepared = true

Set RSMonto = RSMonto_cmd.Execute

'if para validar que hay pagos
if RSMonto.Fields.Item("pagado").Value <> "" then'pagado
saldo =  RSMonto.Fields.Item("pagado").Value
pagado = RSMonto.Fields.Item("pagado").Value
end if'pagado
saldo = monto - saldo
elseif Request.Form("folio") <> "" then
bandera = "no"
end if'Recordset3
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
<!--
.styloencabezado {
	font-size: 18px;
	font-weight: bold;
}
-->
</style>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body>
<table width="200" border="0" align="center">
  <tr align="center">
    <td>Caja</td>
  </tr>
</table>
<% If bandera = "no" Then %>
<table width="256" border="0" align="center">
  <tr align="center" class="styloencabezado">
    <td>El folio <%=Request.Form("folio")%> no  existe</td>
  </tr>
</table>
<% End If %>
<p>&nbsp;</p>
<form action="<%=MM_editAction%>" method="POST" name="form1" id="form1" onSubmit="return val(this)">
  <table align="center">
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Folio:</td>
      <td><input name="folio" type="text" value="<%=folio%>" size="32" /></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Tipo pago:</td>
      <td><select name="tipo_pago">
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
      </select></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Banco:</td>
      <td><select name="banco">
        <option value="">Seleccionar</option>
        <%
While (NOT Recordset1.EOF)
%>
        <option value="<%=(Recordset1.Fields.Item("idbanco").Value)%>"><%=(Recordset1.Fields.Item("nombrebanco").Value)%></option>
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
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Referencia:</td>
      <td><input type="text" name="refecencia" value="" size="32" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#CCCCCC">
      <td align="right" nowrap="nowrap">Monto:</td>
      <td><input name="monto" type="text" value="<%=monto%>" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5">
      <td align="right" nowrap="nowrap">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5">
      <td align="right" nowrap="nowrap">Cantidad recibida:</td>
      <td><input type="text" name="cantidad_recibida" value="" size="32" onChange="valor()"/></td>
    </tr>
    <tr valign="baseline" bgcolor="#CBDFF5">
      <td align="right" nowrap="nowrap">&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Cambio:</td>
      <td><input name="cambio" type="text" value="" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">Saldo:</td>
      <td><input name="saldo" type="text" value="<%=saldo%>" size="32" readonly="readonly" /></td>
    </tr>
    <tr valign="top">
      <td align="right" nowrap="nowrap">Observaciones:</td>
      <td><label>
        <textarea name="observaciones" id="observaciones" cols="28" rows="5"></textarea>
      </label></td>
    </tr>
    <tr valign="baseline">
      <td nowrap="nowrap" align="right">&nbsp;</td>
      <td><input type="submit" value="Guardar" /></td>
    </tr>
  </table>
  <input type="hidden" name="MM_insert" value="form1" />
  <input type="hidden" name="total" id="total" />
  <label>
    <input name="pagos" type="hidden" id="pagos" value="<%=pagado%>" />
  </label>
  <label>
    <input type="button" name="button" id="button" value="Cerrar Ventana" onClick="cerrar()" />
  </label>
</form>
<p>&nbsp;</p>
</body>
</html><%
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
<script language="javascript1.2">
if ('<%=Request.QueryString("cargar")%>' == "si")
{
	parent.location.reload()
}

function val(f)
{
	var message = "Falta:.\n\n";
	if (f.folio.value == "")
	{
      message = message + "- Folio.\n\n";
      alert (message)		
		return false;
	}
	if (f.monto.value == "")
	{
      message = message + "- Monto.\n\n";
      alert (message)	
		return false;
	}
	else
	{
		return true;
	}
}
function valor()
{
	var saldo
	saldo = document.form1.cantidad_recibida.value - document.form1.saldo.value		
	if (saldo >=0)
	{
		document.form1.total.value = document.form1.cantidad_recibida.value - saldo
		document.form1.cambio.value = saldo
	}
	else
	{
		document.form1.total.value = document.form1.cantidad_recibida.value
		document.form1.cambio.value = 0
	}
	
}

</script>
<script language="JavaScript">
document.onkeydown = checkKeycode//llamar la fucnion checkKeycode(eliminar tecla suprimir)
//fucnion para eliminaro con el tecla suprimir
function checkKeycode(e) {
var keycode;//variable para guardar la tecla
//if para valdiar si se presiono una tecla
if (window.event) keycode = window.event.keyCode;
else if (e) keycode = e.which;
//if para valdiar el enter
if (keycode == 13)
{
document.form1.MM_insert.value = "00000000"
document.form1.submit();
}
}
</script>
<SCRIPT language="javascript">
function cerrar()
{
	window.close();
	}
</SCRIPT>