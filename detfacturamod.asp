<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("iddetFactura") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("iddetFactura")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT id_detFactura, total, tretencion, impuesto, cantidad, descripcion, precio_unitario, unidad, descuento, iva, nparte FROM dbo.detFactura WHERE id_detFactura = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble
   ' response.Write(Recordset1_cmd.CommandText & Recordset1__MMColParam )
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
dim impuesto'Variable para guardar el impuesto
dim subtotal'Variable para guardar el subtotal
dim total'Variabel para guardar el total
dim retencion'Variablepar aguardar la rentencion
retencion = 0
'if para validar si se elimina el articulo
If (CStr(Request("MM_delete")) = "form2") Then'MM_delete
  If (Not MM_abortEdit) Then'MM_abortEdit

'if para validar que la suma del iva a la factura
if Request.Form("tretencion") <> "" then
retencion = Request.Form("retencion") - (Request.Form("tretencion"))
end if
   response.Write(Request.Form("iva") & " " & Request.Form("impuesto2"))
if Request.Form("iva") <> "" then
impuesto = Request.Form("iva") - CDbl(Request.Form("impuesto2"))
else
impuesto = 0
end if

if Request.Form("subtotal") <> "" then
subtotal = Request.Form("subtotal") - CDbl(Request.Form("total"))
else
subtotal = 0
end if

if Request.Form("total") <> "" then
total = (subtotal + impuesto)
else
total = 0
end if

if retencion <> 0 then
total = total - retencion
end if

'-----------Recordset para modificar el iva, subtotal y total de la fatcura--------------------------------
  Set RSFactura_cmd = Server.CreateObject ("ADODB.Command")
RSFactura_cmd.ActiveConnection = MM_Conecta1_STRING
RSFactura_cmd.CommandText = "UPDATE dbo.factura SET iva = "& impuesto &", subtotal = " & subtotal & ", total = "& total &", retencion = "&retencion&" WHERE idfactura = " & Request.QueryString("idfactura")
RSFactura_cmd.Prepared = true

Set RSFactura = RSFactura_cmd.Execute
  
  End If'MM_abortEdit
End If'MM_delete
%>
<%
' *** Delete Record: construct a sql delete statement and execute it

If (CStr(Request("MM_delete")) = "form2" And CStr(Request("MM_recordId")) <> "") Then

  If (Not MM_abortEdit) Then
    ' execute the delete
    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "DELETE FROM dbo.detFactura WHERE id_detFactura = ?"
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, Request.Form("MM_recordId")) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    'Dim MM_editRedirectUrl
	if Request.QueryString("eliminar") <> "1" then  '
    
    MM_editRedirectUrl = "detfacturaadd.asp?cantidad="&(Recordset1.Fields.Item("cantidad").Value)&"&descripcion="&(Recordset1.Fields.Item("descripcion").Value)&"&precio="&Replace(Recordset1.Fields.Item("precio_unitario").Value,".","*")&"&unidad="&(Recordset1.Fields.Item("unidad").Value)&"&descuento="&(Recordset1.Fields.Item("descuento").Value)&"&importe="&(Recordset1.Fields.Item("total").Value)&"&impuesto="&(Recordset1.Fields.Item("iva").Value)&"&impuesto2="&(Recordset1.Fields.Item("impuesto").Value)&"&total2="&(Recordset1.Fields.Item("total").Value+Recordset1.Fields.Item("impuesto").Value)&"&nparte="&Recordset1.Fields.Item("nparte").Value
	else
	 MM_editRedirectUrl = "detfacturaadd.asp"
	end if
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
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Request.QueryString("idfactura") <> "") Then 
  Recordset2__MMColParam = Request.QueryString("idfactura")
End If
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.factura WHERE idfactura = ?" 
Recordset2_cmd.Prepared = true
Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset3__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.unidadesDeMedida ORDER BY descripcion ASC" 
Recordset3_cmd.Prepared = true
'Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"><!-- InstanceBegin template="Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
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
	background-color: white;
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
      <p>&nbsp;</p>
<form id="form2" name="form2" method="POST" action="<%=MM_editAction%>">
        <label>        </label>
        <label><br />
        </label>
        <input type="hidden" name="MM_delete" value="form2" />
        <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("id_detFactura").Value %>" />
        <input name="total" type="text" id="total" value="<%=(Recordset1.Fields.Item("total").Value)%>" />
        <input name="iva" type="text" id="iva" value="<%=(Recordset2.Fields.Item("iva").Value)%>" />        
        <input name="tretencion" type="text" id="tretencion" value="<%=(Recordset1.Fields.Item("tretencion").Value)%>" />
        
        <input name="subtotal" type="text" id="subtotal" value="<%=(Recordset2.Fields.Item("subtotal").Value)%>" />        
        <input name="impuesto2" type="text" id="impuesto2" value="<%=(Recordset1.Fields.Item("impuesto").Value)%>" />
        <input name="retencion" type="text" id="retencion" value="<%=(Recordset2.Fields.Item("retencion").Value)%>" />
        <input type="submit" name="button" id="button" value="Eliminar" />
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
<script language="javascript1.2">
if ('<%=Request.QueryString("idcliente")%>' != "")
{
document.form2.submit()
}
</script>
