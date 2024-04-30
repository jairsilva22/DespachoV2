<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%Server.ScriptTimeout=50000000%> 
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idfactura") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idfactura")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT cantidad, d.nparte, d.claveProdServ, d.claveUnidad, d.id_detFactura, d.unidad, d.precio_unitario, d.descripcion, d.impuestoP, iva, total, d.descuento, impuesto FROM dbo.detFactura d WHERE id_factura = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

   ' response.Write(Recordset1_cmd.CommandText)
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Request.QueryString("idCliente") <> "") Then 
  Recordset3__MMColParam = Request.QueryString("idCliente")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion AS clientes, dbo.ciudades AS ciudad, dbo.estados AS estado WHERE idCliente = ? AND ciudad.id = ciudadCliente AND estado.id = estadoCliente" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
%>
<%
dim tretencion'Vairable para guardar el iva rteneido
dim retencion'Variable para guardar la retencion
dim subtotal'Variable para guardar el subtotal de la factura
dim ivafac'Variable para guardar el iva de la factura
dim total'Variable para guardar el total de la factura

tretencion = 0
retencion = 0
subtotal = 0
ivafac = 0
total = 0
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
<title>Documento sin título</title>
     <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
</head>

<body>
<table border="1">
  <tr>
    <td>Cantidad</td>
    <td>descripcion</td>
    <td>precio_unitario</td>
    <td>total</td>
    <td>descuento</td>
    <td>impuesto</td>
    <td>iva</td>
    <td>unidad</td>
  </tr>
  <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
<%
precio = Round(Recordset1.Fields.Item("precio_unitario").Value, 2)
subtotal = subtotal + (Recordset1.Fields.Item("cantidad").Value*precio)
ivafac = ivafac + ((Recordset1.Fields.Item("cantidad").Value*precio) * (Recordset1.Fields.Item("iva").Value / 100))

'-------Recordset para redondear los precios-------------------------------
Set RSPrecios_cmd = Server.CreateObject ("ADODB.Command")
RSPrecios_cmd.ActiveConnection = MM_Conecta1_STRING
RSPrecios_cmd.CommandText = "UPDATE dbo.detFactura SET precio_unitario = "&precio&", impuesto = "&((Recordset1.Fields.Item("cantidad").Value*precio) * (Recordset1.Fields.Item("iva").Value / 100))&",total = "&(Recordset1.Fields.Item("cantidad").Value*precio)&" WHERE id_detfactura = " & (Recordset1.Fields.Item("id_detFactura").Value)
RSPrecios_cmd.Prepared = true

Set RSPrecios = RSPrecios_cmd.Execute

    'mostrar el total y el descuento
      if recordset1.fields.item("descuento").value > 0 Then
        precioUnitario = truncarAseis(Recordset1.Fields.Item("precio_unitario").Value * ((100 - Recordset1.Fields.Item("descuento").Value) / 100))
    Else
        precioUnitario = truncarAseis(Recordset1.Fields.Item("precio_unitario").Value)
	End If
   
	'validamos si se trunca o redondea
	If Recordset3.Fields.Item("truncar").Value = "si" Then
		totalConcepto = truncarAdos(precioUnitario * Recordset1.Fields.Item("cantidad").Value)
		subtotal1 = subtotal1 + truncarAdos(totalConcepto)
		If Recordset1.Fields.Item("iva").Value <> "0" Then
			impuesto = truncarAdos(totalConcepto * 0.16)
			totalImpuesto = totalImpuesto + truncarAdos(impuesto)
		Else
			impuesto = 0
			totalImpuesto = 0
		End If
	Else
		totalConcepto = redondear(precioUnitario * Recordset1.Fields.Item("cantidad").Value)
		subtotal1 = subtotal1 + redondear(totalConcepto)
		If Recordset1.Fields.Item("iva").Value <> "0" Then
			impuesto = redondear(totalConcepto * 0.16)
			totalImpuesto = totalImpuesto + redondear(impuesto)
		Else
			impuesto = 0
			totalImpuesto = 0
		End If		
	End If
%>  



    <tr>
      <td><%=(Recordset1.Fields.Item("cantidad").Value)%></td>
      <td><%=(Recordset1.Fields.Item("descripcion").Value)%></td>
      <td><%=(Recordset1.Fields.Item("precio_unitario").Value)%></td>
      <td><%=(Recordset1.Fields.Item("total").Value)%></td>
      <td><%=(Recordset1.Fields.Item("descuento").Value)%></td>
      <td><%=(Recordset1.Fields.Item("impuesto").Value)%></td>
      <td><%=(Recordset1.Fields.Item("iva").Value)%></td>
      <td><%=(Recordset1.Fields.Item("unidad").Value)%></td>
    </tr>
    <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Wend

        If Recordset3.Fields.Item("truncar").Value = "si" Then
	total1 = truncarAdos(subtotal1 + totalImpuesto)

	If Recordset3.Fields.Item("isr").Value = "si" Then
		total1 = truncarAdos(subtotal1 - (subtotal1 * 0.15))
	End If
	'response.Write "si"
Else
	total1 = redondear(subtotal1 + totalImpuesto)

	If Recordset3.Fields.Item("isr").Value = "si" Then
		total1 = redondear(subtotal1 - (subtotal1 * 0.15))
	End If
	'response.Write "no"&total
End If
%>
<%





'if para valdiar que el usuario tenga retencion
if Recordset3.Fields.Item("retencion").Value = true then'site_retencion
retencion = ivafac
end if'site_retencion

total = subtotal + ivafac

'if para validar que tiene retencion
if retencion <> 0 then'retencion
total = total - CDbl(retencion)
end if'retencion

Response.Write("Subtotal:"&subtotal1)
Response.Write("<br />")
Response.Write("IVA:"&totalImpuesto)
Response.Write("<br />")
Response.Write("Total:"&total1)

'-------Recordset para redondear los precios-------------------------------
Set RSPrecios_cmd = Server.CreateObject ("ADODB.Command")
RSPrecios_cmd.ActiveConnection = MM_Conecta1_STRING
RSPrecios_cmd.CommandText = "UPDATE dbo.factura SET subtotal = "&subtotal1&", iva = "&totalImpuesto&", total = "&total1&", retencion = "&retencion&" WHERE idfactura = " & Request.QueryString("idfactura")
RSPrecios_cmd.Prepared = true

Set RSPrecios = RSPrecios_cmd.Execute

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "detfacturaadd.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
%>
</table>
</body>
</html>
<%

  function truncarAdos(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		digitos = Mid(valor,InsTr(valor,".")+1)
		'if para validar si tiene un solo digito
		if Len(digitos) <= 1 then'digitos
			valor = valor & "0"
		else'digitos
			valor =  Mid(valor,1,InsTr(valor,".")-1)
			valor = valor & "." & Mid(digitos,1,2)
		end if'digitos
	else'valor
		valor = valor&".00"
	end if'valor
	truncarAdos=valor
End function

function truncarAseis(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		digitos = Mid(valor,InsTr(valor,".")+1)
		'if para validar si tiene un solo digito
		if Len(digitos) <= 1 then'digitos
			valor = valor & "0"
		else'digitos
			valor =  Mid(valor,1,InsTr(valor,".")-1)
			valor = valor & "." & Mid(digitos,1,6)
		end if'digitos
	else'valor
		valor = valor&".00"
	end if'valor
	truncarAseis=valor
End function

function redondear(valor)
	'if para validar si el subtotal tiene digitos
	if InsTr(valor,".") <> 0 then'valor
		valor = Round(valor, 2)
	else'valor
		valor = valor&".00"
	end if'valor
	redondear=valor
End function

Recordset1.Close()
Set Recordset1 = Nothing

  
%>
