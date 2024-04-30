<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%

Response.Buffer = true
Server.ScriptTimeout=50000

dim folio'Variable para guardar el folio
dim ven'Vairable para guardar el vendedor
dim vendedor'Variable para guardar el vendedor a filtrar
dim dia'Variable para guardar el mes que se va a filtrar
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

dim otrosSub 
dim otrosIva 
dim otrosTotal 
dim Sub01 
dim Iva01 
dim Total01 
dim Sub04 
dim Iva04 
dim Total04 
dim Sub28
dim Iva28
dim Total28
dim Sub03 
dim Iva03 
dim Total03 
dim Sub02 
dim Iva02 
dim Total02 

otrosSub = 0
otrosIva = 0
otrosTotal = 0
Sub01 = 0
Iva01 = 0
Total01 = 0
Sub04 = 0
Iva04 = 0
Total04 = 0
Sub28 = 0
Iva28 = 0
Total28 = 0
Sub03 = 0
Iva03 = 0
Total03 = 0
Sub02 = 0
Iva02 = 0
Total02 = 0
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
if Request.Form("vendedor") <> "" then'vendedor
vendedor = "AND vendedor = '" & Request.Form("vendedor") & "'"
ven = "Vendedor: "& Request.Form("vendedor")
end if'vendedor

'if para validar que se filtra el documento
if Request.Form("cliente") <> "" then'documento
  cliente = " AND idcliente = " & Request.Form("cliente")
end if'documento

'if para validar que se filtra el documento
if Request.Form("fpago") <> "" then'documento
  fpago = " AND factura.forma_pago = " & Request.Form("fpago")
end if'documento

'if para validar que se filtra el mes
if Request.Form("dia") <> "" then'mes
  dia = " AND CONVERT(varchar, fechasellado, 103) = CONVERT(varchar, '" & Request.Form("dia")&"', 103)"
end if'mes
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idcliente, folio, documento.descripcion as documento, subtotal, iva, total , formPago.descripcion as fpago, estatus, fechasellado, retencion, idfactura, metodopago FROM dbo.factura, dbo.formPago, dbo.documento WHERE idfactura > 0 AND abreviatura = 'FA' AND estatus = 'Facturada' AND idpago = factura.forma_pago AND subtotal > 0 and folio is not NULL AND iddocumento = tipo_comprobante AND UUID IS NOT NULL AND timbre = 'NO' " & dia & fpago & cliente & vendedor &" ORDER BY " & ordenar
Recordset1_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<% 'busca nombre de la empresa
Dim Recordset1E
Dim Recordset1E_cmd
Dim Recordset1E_numRows

Set Recordset1E_cmd = Server.CreateObject ("ADODB.Command")
Recordset1E_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1E_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = " & Request.Cookies("login")("idSucursal")
Recordset1E_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1E = Recordset1E_cmd.Execute
Recordset1E_numRows = 0
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows
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
<table width="700" border="0">
  <tr>
    <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" valign="bottom">
        <td width="16%"><img src="img/pepi_logo.png" width="91" height="69" /></td>
        <td colspan="2" class="titulos"><%= Recordset1E.Fields.Item("nombre").Value %></td>
      </tr>
      <tr align="center">
        <td height="25" colspan="2" valign="bottom">
		
		<% 
		'if para validar si hay un mes filtrado
		if request.form("mes") <> "" then'mes
		 Response.Write(UCase("Detalles De Las Facturas Del Mes De "&Monthname(Request.Form("mes"))&" Año " & Request.Form("yearemiso"))) 
		 else'mes		 
		 Response.Write("Detalles de Facturas ")
		 end if'mes
		 %></td>
        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
      </tr>
      <tr class="stylo1">
        <td colspan="3"><%= ven %></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td><table width="100%" border="1" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="#e33045" class="stylo1">
        <td width="4%">Folio</td>
        <td width="5%">R.F.C.</td>
        <td width="30%">Cliente</td>
        <td width="17%" bgcolor="#e33045">Tipo documento</td>
        <td width="7%">Subtotal</td>
        <td width="5%">I.V.A.</td>
        <td width="5%">Total</td>
        <td width="15%">Forma de pago</td>
        <td width="16%">Estatus</td>
        <td width="17%">Fecha</td>
        <td width="17%">Observaciones</td>
      </tr>
      <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) %>
      <%
rfc = "&nbsp;"
nombre = "&nbsp;"

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

'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    total1 = 0
    subtotal1 = 0
    retencion = 0
    totalImpuesto = 0
                  '-------Recordset para redondear los precios-------------------------------
    Set RSPrecios_cmd = Server.CreateObject ("ADODB.Command")
    RSPrecios_cmd.ActiveConnection = MM_Conecta1_STRING
    RSPrecios_cmd.CommandText = "select * FROM dbo.detFactura WHERE id_factura = " & (Recordset1.Fields.Item("idfactura").Value)
    RSPrecios_cmd.Prepared = true

    Set RSPrecios = RSPrecios_cmd.Execute
    
    while ((NOT RSPrecios.EOF))
        'mostrar el total y el descuento
        if RSPrecios.fields.item("descuento").value > 0 Then
            precioUnitario = truncarAseis(RSPrecios.Fields.Item("precio_unitario").Value * ((100 - RSPrecios.Fields.Item("descuento").Value) / 100))
        Else
            precioUnitario = truncarAseis(RSPrecios.Fields.Item("precio_unitario").Value)
	    End If
   
	    'validamos si se trunca o redondea
	    If RSCleinte.Fields.Item("truncar").Value = "si" Then
		    totalConcepto = truncarAdos(precioUnitario * RSPrecios.Fields.Item("cantidad").Value)
		    subtotal1 = subtotal1 + truncarAdos(totalConcepto)
		    If RSPrecios.Fields.Item("iva").Value <> "0" Then
			    impuesto = truncarAdos(totalConcepto * 0.16)
			    totalImpuesto = totalImpuesto + truncarAdos(impuesto)
		    Else
			    impuesto = 0
			    totalImpuesto = 0
		    End If
	    Else
		    totalConcepto = redondear(precioUnitario * RSPrecios.Fields.Item("cantidad").Value)
		    subtotal1 = redondear(CDbl(subtotal1) + CDbl(totalConcepto))
		    If RSPrecios.Fields.Item("iva").Value <> "0" Then
			    impuesto = redondear(totalConcepto * 0.16)
			    totalImpuesto = totalImpuesto + redondear(impuesto)
		    Else
			    impuesto = 0
			    totalImpuesto = 0
		    End If		
	    End If
      RSPrecios.MoveNext()
    Wend

    If RSCleinte.Fields.Item("truncar").Value = "si" Then
	    total1 = truncarAdos(subtotal1 + totalImpuesto)

	    If RSCleinte.Fields.Item("isr").Value = "si" Then
		    total1 = truncarAdos(subtotal1 - (subtotal1 * 0.15))
	    End If
	    'response.Write "si"
    Else
	    total1 = redondear(subtotal1 + totalImpuesto)

	    If RSCleinte.Fields.Item("isr").Value = "si" Then
		    total1 = redondear(subtotal1 - (subtotal1 * 0.15))
	    End If
	    'response.Write "no"&total
    End If


    'validar si la factura incluye rentencion 
    retCte = Recordset1.fields.item("retencion").value

    'if para valdiar que el usuario tenga retencion
    if retCte > 0  then'site_retencion
        retencion = totalImpuesto
        total1 = subtotal1
    else
        retencion = 0
        total1 = total1
    end if'site_retencion



  '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

'subtot = digi(Recordset1.Fields.Item("subtotal").Value)
'ivarep2 = digi(Recordset1.Fields.Item("iva").Value)
'tot = digi(Recordset1.Fields.Item("total").Value)
    If Request.Form("fpago") = "2" Then
      If Recordset1.Fields.Item("metodopago").Value = "99" Then
        otrosSub = otrosSub + subtot
        otrosIva = otrosIva + ivarep2
        otrosTotal = otrosTotal + tot
      End IF
      subcontado = subcontado + subtot
      ivacontado = ivacontado + ivarep2
      totcontado = totcontado + tot
    ElseIf Request.Form("fpago") = "1" Then
      If Recordset1.Fields.Item("metodopago").Value = "01" Then
        Sub01 = Sub01 + subtot
        Iva01 = Iva01 + ivarep2
        Total01 = Total01 + tot
      ElseIf Recordset1.Fields.Item("metodopago").Value = "04" Then
        Sub04 = Sub04 + subtot
        Iva04 = Iva04 + ivarep2
        Total04 = Total04 + tot
      ElseIf Recordset1.Fields.Item("metodopago").Value = "28" Then
        Sub28 = Sub28 + subtot
        Iva28 = Iva28 + ivarep2
        Total28 = Total28 + tot      
      ElseIf Recordset1.Fields.Item("metodopago").Value = "03" Then
        Sub03 = Sub03 + subtot
        Iva03 = Iva03 + ivarep2
        Total03 = Total03 + tot
      ElseIf Recordset1.Fields.Item("metodopago").Value = "02" Then
        Sub02 = Sub02 + subtot
        Iva02 = Iva02 + ivarep2
        Total02 = Total02 + tot
      End If
      subcontado = subcontado + subtot
      ivacontado = ivacontado + ivarep2
      totcontado = totcontado + tot
    End If
subtot = FormatNumber(subtotal1)
ivarep2 = FormatNumber(totalImpuesto)
tot = FormatNumber(total1)  
%>
      <tr bgcolor="<%=color%>" class="stylo2">
        <td><% If Recordset1.Fields.Item("folio").Value <> "" Then 
		Response.Write(Recordset1.Fields.Item("folio").Value)
		Else Response.Write("&nbsp;")
		End If %></td>
        <td><%=rfc%></td>
        <td><%=nombre%></td>
        <td align="center"><%=(Recordset1.Fields.Item("documento").Value)%></td>
        <td><%=(subtot)%></td>
        <td><%=(ivarep2)%></td>
        <td><%=(tot)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("fpago").Value)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("estatus").Value)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("fechasellado").Value)%></td>
        <td align="center">&nbsp;</td>
      </tr>
      <% 
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
  response.Flush()
Wend
%>
<%
coniva = 1 + ((iva/100))
submostrador=(submostrador/coniva)
if ivamostrador="" OR ivamostrador=0 THEN
ivamostrador = submostrador * (iva/100)
END IF
totmostrador = submostrador + ivamostrador
%>
    </table></td>
  </tr>
  <%If Request.Form("fpago") = "2" Then 'Contado = 1 Credito = 2%>
  <tr>
    <td><table width="700" border="1" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="#e33045" class="stylo1">
        <td>M&#233;todo Pago</td>
        <td>Subtotal</td>
        <td>I.V.A</td>
        <td>Total</td>
      </tr>
      <tr class="stylo2">
        <td>Otros</td>
        <td><%= FormatNumber(otrosSub)%></td>
        <td><%= FormatNumber(otrosIva)%></td>
        <td><%= FormatNumber(otrosTotal)%></td>
      </tr>
      <%
      subtotal = CDbl(submostrador) + CDbl(subcontado) + CDbl(subcredito)
      ivarep = CDbl(ivamostrador) + CDbl(ivacontado) + CDbl(ivacredito)
      total = CDbl(totmostrador) + CDbl(totcontado) + CDbl(totcredito)
      %>      
      <tr class="stylo2">
        <td>Total</td>
        <td><%= FormatNumber(subtotal) %></td>
        <td><%= FormatNumber(ivarep) %></td>
        <td><%= FormatNumber(total) %></td>
      </tr>
    </table></td>
  </tr>
  <%Else%>
  <tr>
    <td><table width="700" border="1" align="center" cellpadding="0" cellspacing="0">
      <tr align="center" bgcolor="#e33045" class="stylo1">
        <td>Tipo Cliente</td>
        <td>Subtotal</td>
        <td>I.V.A</td>
        <td>Total</td>
      </tr>
      <tr class="stylo2">
        <td>Efectivo</td>
        <td><%= FormatNumber(Sub01)%></td>
        <td><%= FormatNumber(Iva01)%></td>
        <td><%= FormatNumber(Total01)%></td>
      </tr>
      <tr class="stylo2">
        <td>Tarjeta Crédito</td>
        <td><%= FormatNumber(Sub04)%></td>
        <td><%= FormatNumber(Iva04)%></td>
        <td><%= FormatNumber(Total04)%></td>
      </tr>
      <tr class="stylo2">
        <td>Tarjeta Dédito</td>
        <td><%= FormatNumber(Sub28)%></td>
        <td><%= FormatNumber(Iva28)%></td>
        <td><%= FormatNumber(Total28)%></td>
      </tr>
      <tr class="stylo2">
        <td>Transferencias</td>
        <td><%= FormatNumber(Sub03)%></td>
        <td><%= FormatNumber(Iva03)%></td>
        <td><%= FormatNumber(Total03)%></td>
      </tr>
      <tr class="stylo2">
        <td>Cheques</td>
        <td><%= FormatNumber(Sub02)%></td>
        <td><%= FormatNumber(Iva02)%></td>
        <td><%= FormatNumber(Total02)%></td>
      </tr>
      <%
      subtotal = CDbl(submostrador) + CDbl(subcontado) + CDbl(subcredito)
      ivarep = CDbl(ivamostrador) + CDbl(ivacontado) + CDbl(ivacredito)
      total = CDbl(totmostrador) + CDbl(totcontado) + CDbl(totcredito)
      %>      
      <tr class="stylo2">
        <td>Total</td>
        <td><%= FormatNumber(subtotal) %></td>
        <td><%= FormatNumber(ivarep) %></td>
        <td><%= FormatNumber(total) %></td>
      </tr>
    </table></td>
  </tr>
  <%End If%>
</table>
<p>&nbsp;</p>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
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

    %>