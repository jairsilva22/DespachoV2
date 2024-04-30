<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
dim folio'Variable para guardar el folio
dim ven'Vairable para guardar el vendedor
dim vendedor'Variable para guardar el vendedor a filtrar
dim mes'Variable para guardar el mes que se va a filtrar
dim yearrep'Variable para guardar el año que se va a filtrar
dim estatus'Variable para guardar el estatus a filtrar
dim documento'Variable para guardar el tipo de documeto a filtrar
dim fpago'Variable para guardar la forma de pago
dim rfc'Variable para guardar el rfc del cliente
dim nombre'Varaible para guardar el nombre del cliente
dim ordenar'Variable para guardar el orden de la consutla
dim subtotal'Variable apra guardar el total del subtotal
dim ivarep'Variable apra guardar el total del iva
dim total'Variable apra guardar el total del total
dim cliente'Variable para guardar el cliente a filtrar
dim subtot'Variable apra guardar el subtotal
dim ivarep2'Variable apra guardar el iva
dim tot'Variable apra guardar el total
dim submostrador'Variable para guardar el subtotal de las ventas de mostrador
dim ivamostrador'Variable para guardar el iva de las ventas de mostrador
dim totmostrador'Variable para guardar el total de las ventas de mostrador
dim subcontado'Variable para guardar el subtotal de las ventas de contado
dim ivacontado'Variable para guardar el iva de las ventas de contado
dim totcontado'Variable para guardar el total de las ventas de contado
dim subcredito'Variable para guardar el subtotal de las ventas de credito
dim ivacredito'Variable para guardar el iva de las ventas de credito
dim totcredito'Variable para guardar el total de las ventas de credito
dim sucursal'Variable para guardar el total de las ventas de credito

ven = "&nbsp;"
subcontado = 0
ivacontado = 0
totcontado = 0
subcredito = 0
ivacredito = 0
totcredito = 0
submostrador = 0
ivamostrador = 0
totmostrador = 0
subtotal = 0 
ivarep = 0
total = 0
ordenar = " " & Request.Form("orden")

'if para validar que se filtra el vendedor
if Request.Form("folio") <> "" then'vendedor
folio = "AND folio = " & Request.Form("folio")
end if'vendedor

'if para validar que se filtra el vendedor
if Request.Form("vendedor") <> "" then'vendedor
vendedor = "AND vendedor = '" & Request.Form("vendedor") & "'"
ven = "Vendedor: "& Request.Form("vendedor")
end if'vendedor

'if para validar que se filtra el documento
if Request.Form("cliente") <> "" then'documento
cliente = "AND idcliente = " & Request.Form("cliente")
end if'documento

'if para validar que se filtra el documento
if Request.Form("fpago") <> "" AND Request.Form("fpago") <> "XAXX010101000" then'documento
fpago = "AND forma_pago = " & Request.Form("fpago")
elseif Request.Form("fpago") <> "" AND Request.Form("fpago") = "XAXX010101000" then'documento

'-----------Recordset para consultar informacion del cliente mostrador-------------------------------------
Set RSCleinteMos_cmd = Server.CreateObject ("ADODB.Command")
RSCleinteMos_cmd.ActiveConnection = MM_Conecta1_STRING
RSCleinteMos_cmd.CommandText = "SELECT * FROM dbo.clientes WHERE  rfcCliente = '" & Request.Form("fpago") & "'"
RSCleinteMos_cmd.Prepared = true

Set RSCleinteMos = RSCleinteMos_cmd.Execute

'if para validar que tiene datos
if NOT RSCleinteMos.EOF then'RSCleinteMos
fpago = " AND idcliente = " & RSCleinteMos.Fields.Item("idCliente").Value
end if'RSCleinteMos
end if'documento

'if para validar que se filtra el documento
if Request.Form("documento") <> "" then'documento
documento = " AND tipo_comprobante = " & Request.Form("documento")
end if'documento

'if para validar que se filtra el estatus
if Request.Form("estatus") <> "" then'estatus
    if Request.Form("estatus") = "Facturada" Then 
      estatus = " AND estatus = 'Facturada' AND timbre = 'no' AND UUID is not null"
    elseif Request.Form("estatus") = "Error" Then 
      estatus = " AND (estatus = 'error' or timbre = 'error')"
    elseif Request.Form("estatus") = "Procesando" Then 
      estatus = " AND (estatus = 'Procesando' or estatus = 'procesoco')"
    elseif Request.Form("estatus") = "Cancelada" Then 
      estatus = " AND (estatus = 'Cancelada' or estatus = 'PCancelada')"
    else
      estatus = " AND estatus = '" & Request.Form("estatus") & "'"
    end if
end if'estatus

'if para validar que se filtra el yearemiso
if Request.Form("yearemiso") <> "" then'yearemiso
yearrep = " AND year(fechasellado) = " & Request.Form("yearemiso")
end if'yearemiso

'if para validar que se filtra el mes
if Request.Form("mes") <> "" then'mes
mes = " AND Month(fechasellado) = " & Request.Form("mes")
end if'mes

'if para validar que se filtra empresa
if Request.Form("empresa") <> "" then'mes
sucursal = " AND factura.idempresa = " & Request.Form("empresa")
end if'mes
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idcliente, folio, documento.descripcion as documento, subtotal, iva, total , formPago.descripcion as fpago, estatus, CONVERT(date, fechasellado, 103) AS fechasellado, idfactura FROM dbo.factura, dbo.formPago, dbo.documento WHERE idfactura > 0 AND idpago = factura.forma_pago AND subtotal>=0 and folio is not NULL AND iddocumento = tipo_comprobante " & mes & yearrep & estatus & documento & fpago & cliente & vendedor & folio & sucursal &" ORDER BY " & ordenar
Recordset1_cmd.Prepared = true
    'Response.Write(Recordset1_cmd.CommandText)
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows

Set RSInfo_cmd = Server.CreateObject ("ADODB.Command")
RSInfo_cmd.ActiveConnection = MM_Conecta1_STRING
RSInfo_cmd.CommandText = "SELECT * FROM dbo.confimenor WHERE idConf = 1 "
RSInfo_cmd.Prepared = true

Set RSInfo = RSInfo_cmd.Execute
    ctabla = "red"
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
<!--
.titulos {
	font-size: 32px;
	font-weight: bold;
}
-->
</style>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body>
<table width="950" border="0">
  <tr>
    <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" valign="bottom">
        <td width="16%"><img src="img/pepi_logo.png" width="120" height="60" /></td>
        <td colspan="2" class="titulos"><%=RSInfo.fields.item("razon_social").value %></td>
      </tr>
      <tr align="center">
        <td height="25" colspan="2" valign="bottom">
		
		<% 
		'if para validar si hay un mes filtrado
		if request.form("mes") <> "" then'mes
		 Response.Write(UCase("Detalles De Las Facturas Del Mes De "&Monthname(Request.Form("mes"))&" A&#xD1;o " & Request.Form("yearemiso"))) 
		 else'mes		 
		 Response.Write("Detalles de Facturas ")
		 end if'mes
		 %></td>
        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
      </tr>
      <tr class="stylo1">
        <td colspan="3"><%= ven %> </td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="1" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="#e33045" class="stylo1" style="color: white; ">
          <td width="4%">Folio</td>
        <td width="5%">R.F.C.</td>
        <td width="40%">Cliente</td>
        <td width="10%" bgcolor="#e33045">Tipo documento</td>
        <td width="7%">Subtotal</td>
        <td width="5%">I.V.A.</td>
        <td width="5%">Total</td>
        <td width="5%">Forma de pago</td>
        <td width="10%">Estatus</td>
        <td width="44%">Fecha</td>
        <td width="10%">Observaciones</td>
      </tr>
      <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
      <%
rfc = "&nbsp;"
nombre = "&nbsp;"

cgrid2 = "#ececec"
cgrid1 = "#ffffff"

'if para validar el cambio del color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color


'if para validar que tnga cliente
if Recordset1.Fields.Item("idcliente").Value <> "" then'idcliente
'-----------Recordset para consultar informacion del cliente-------------------------------------
Set RSCleinte_cmd = Server.CreateObject ("ADODB.Command")
RSCleinte_cmd.ActiveConnection = MM_Conecta1_STRING
RSCleinte_cmd.CommandText = "SELECT * FROM dbo.clientes WHERE  idCliente = " & Recordset1.Fields.Item("idcliente").Value
RSCleinte_cmd.Prepared = true

Set RSCleinte = RSCleinte_cmd.Execute

'if para validar que tiene datos
if NOT RSCleinte.EOF then'RSCleinte
rfc = RSCleinte.Fields.Item("rfcCliente").Value
nombre = RSCleinte.Fields.Item("nombreCliente").Value
end if'RSCleinte
end if'idcliente

subtot = digi(Recordset1.Fields.Item("subtotal").Value)
ivarep2 = digi(Recordset1.Fields.Item("iva").Value)
tot = digi(Recordset1.Fields.Item("total").Value)

          'Response.Write("Idfactura: "&Recordset1.Fields.Item("idfactura").Value&" Subtotal: "&subtot& "<br>")
'if para validar que no sume las facturas canceladas
'if (Recordset1.Fields.Item("estatus").Value) = "Facturada" then'Facturada

    'if para validar que solo sume las facturas de contado
    if Recordset1.Fields.Item("fpago").Value = "Pago en Una sola Exhibición" and rfc <> "XAXX010101000" then'Contado
    subcontado = subcontado + subtot
    ivacontado = ivacontado + ivarep2
    totcontado = totcontado + tot
          'Response.Write("idFactura: "&Recordset1.Fields.Item("idfactura").Value&" Subtotal: "&Recordset1.Fields.Item("subtotal").Value&" fpago: "&Recordset1.Fields.Item("fpago").Value&"<br>")
    end if'Contado

    'if para validar que solo sume las facturas de credito
    if Recordset1.Fields.Item("fpago").Value = "Pago en Parcialidades o Diferido" and rfc <> "XAXX010101000" then'Credito
    subcredito = subcredito + subtot
    ivacredito = ivacredito + ivarep2
    totcredito = totcredito + tot
    end if'Credito

    'if para validar que solo sume las facturas de venta de mostrador
    if rfc = "XAXX010101000" AND Request.Form("fpago") = "" OR Request.Form("fpago") = "XAXX010101000" then'XAXX010101000
    submostrador = submostrador + subtot
    ivamostrador = ivamostrador + ivarep2
    totmostrador = totmostrador + tot
    end if'XAXX010101000

'end if'Facturada
%>
      <tr bgcolor="<%=color%>" class="stylo2">
        <td><% If Recordset1.Fields.Item("folio").Value <> "" Then 
		Response.Write(Recordset1.Fields.Item("folio").Value)
		Else Response.Write("&nbsp;")
		End If %></td>
        <td><%=rfc%></td>
        <td><%=nombre%></td>
           <% if Recordset1.Fields.Item("documento").Value = "Factura" then
              documento = "FA"
              elseif Recordset1.Fields.Item("documento").Value = "Nota de Credito" OR Recordset1.Fields.Item("documento").Value = "Nota de Crédito" Then
              documento = "NC"
              elseif Recordset1.Fields.Item("documento").Value = "Nota de Cargo" then
              documento = "NCA"
               elseif Recordset1.Fields.Item("documento").Value = "Traslado" Then
               documento = "T"
              end if
              %>
        <td align="center"><%=(documento)%></td>
        <td><%=(subtot)%></td>
        <td><%=(ivarep2)%></td>
        <td><%=(tot)%></td>
            <%
              if Recordset1.Fields.Item("fpago").Value = "Pago en Parcialidades o Diferido" Then
              fpago = "PPD"
              else
              fpago = "PUE"
              end if
              %>
        <td align="center"><%=(fpago)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("estatus").Value)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("fechasellado").Value)%></td>
        <td align="center">&nbsp;</td>
      </tr>
      <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Wend
%>
<%
'coniva = 1 + ((iva/100))
    'response.Write("IVA: "&coniva&" submostrador: "&submostrador&" ivamostrador: "&ivamostrador)
'submostrador=(submostrador/coniva)
    
'if ivamostrador="" OR ivamostrador=0 THEN
'ivamostrador = submostrador * (iva/100)
'END IF
'totmostrador = submostrador + ivamostrador
%>
    </table></td>
  </tr>
  <tr>
    <td><table width="700" border="1" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="#e33045" class="stylo1" style="color: white; ">
        <td>Tipo Cliente</td>
        <td>Subtotal</td>
        <td>I.V.A</td>
        <td>Total</td>
      </tr>
      <tr class="stylo2">
        <td>Mostrador</td>
        <td><%=digi(submostrador)%></td>
        <td><%= digi(ivamostrador)%></td>
        <td><%= digi(totmostrador)%></td>
      </tr>
      <tr class="stylo2">
        <td>Contado</td>
        <td><%= digi(subcontado) %></td>
        <td><%= digi(ivacontado) %></td>
        <td><%= digi(totcontado) %></td>
      </tr>
      <tr class="stylo2">
        <td>Credito</td>
        <td><%= digi(subcredito) %></td>
        <td><%= digi(ivacredito) %></td>
        <td><%= digi(totcredito) %></td>
      </tr>
<%
subtotal = CDbl(submostrador) + CDbl(subcontado) + CDbl(subcredito)
ivarep = CDbl(ivamostrador) + CDbl(ivacontado) + CDbl(ivacredito)
total = CDbl(totmostrador) + CDbl(totcontado) + CDbl(totcredito)
%>      
      <tr class="stylo2">
        <td>Total</td>
        <td><%= digi(subtotal) %></td>
        <td><%= digi(ivarep) %></td>
        <td><%=digi(total) %></td>
      </tr>
    </table></td>
  </tr>
</table>
<p>&nbsp;</p>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
