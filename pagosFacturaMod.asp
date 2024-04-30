<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows
Dim parcialidad, saldoAnterior, saldoInsoluto, cantidad, saldo

parcialidad = 0
saldo = 0

If Request.QueryString("elim") <> "" Then
	'actualizamos a cancelado el pago
	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "UPDATE dbo.pagosFactura SET estatus = 'Cancelado' WHERE idcc = "&Request.QueryString("idPago")
	Recordset2_cmd.Prepared = true
	'response.Write Recordset2_cmd.CommandText
	Set Recordset2 = Recordset2_cmd.Execute
    '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

     '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
	Response.Redirect("pagosFactura.asp?idfactura="&Request.QueryString("idfactura"))
End If

'parte para modificar el pago
If Request.Form("cantidad") <> "" And Request.Form("cantidad") <> "0" Then
	cantidad = Request.Form("cantidad")
	'------------SaldoInsoluto-------------
	'buscamos si hay más pagos anteriores a este
	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "SELECT TOP(1) impSaldoInsoluto FROM dbo.pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado' ORDER BY idcc DESC"
	Recordset2_cmd.Prepared = true
	'response.Write Recordset2_cmd.CommandText
	Set Recordset2 = Recordset2_cmd.Execute

    if(request.form("moneda") <> request.form("monedaF")) then
 '     cambio = request.Form("cambio")
           cambio = 1
    else
        cambio = 1
    end if

	If Not Recordset2.EOF Then
		If Recordset2.Fields.Item("impSaldoInsoluto").Value = "" Or IsNull(Recordset2.Fields.Item("impSaldoInsoluto").Value) Then
			saldoInsoluto = redondear((Request.Form("totalFactura") * cambio)- cantidad)
		Else
			saldoInsoluto = (Recordset2.Fields.Item("impSaldoInsoluto").Value - cantidad)
		End If
	Else
		saldoInsoluto = redondear((Request.Form("totalFactura") * cambio) - cantidad)
	End If

	'------------SaldoAnterior-------------
	'hacemos la suma de los pagos anteriores
	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "SELECT TOP(1) impSaldoInsoluto FROM dbo.pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado' ORDER BY idcc DESC"
	Recordset2_cmd.Prepared = true

	Set Recordset2 = Recordset2_cmd.Execute

	If Not Recordset2.EOF Then
		If Recordset2.Fields.Item("impSaldoInsoluto").Value <> "" And Not IsNull(Recordset2.Fields.Item("impSaldoInsoluto").Value) Then
			saldoAnterior = Recordset2.Fields.Item("impSaldoInsoluto").Value
		Else
			saldoAnterior = redondear(Request.Form("totalFactura") * cambio)
		End If
	Else
		saldoAnterior = redondear(Request.Form("totalFactura") * cambio)
	End If

	'insertamos en la tabla de pagos
	Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
	Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset1_cmd.CommandText = "UPDATE pagosFactura SET metodoPago = '"&Request.Form("metodoPago")&"', RfcEmisorCtaOrd = '"&trim(Request.form("RfcEmisorCtaOrd"))&"', CtaOrdenante = '"&trim(Request.form("CtaOrdenante"))&"', RfcEmisorCtaBen = '"&trim(Request.form("RfcEmisorCtaBen"))&"', CtaBeneficiario = '"&trim(Request.form("CtaBeneficiario"))&"', NomBancoOrdExt = '"&trim(Request.Form("NomBancoOrdExt"))&"', formaPago = '"&Request.Form("formaPago")&"', moneda = '"&Request.Form("moneda")&"', tipoCambioDR = '"&request.Form("cambioP")&"', noOperacion = '"&request.Form("NoOperacion")&"', fechaAlta = CONVERT(datetime, '"&Request.Form("fechaAlta")&"', 103) WHERE idcc = "&Request.QueryString("idPago")
	Recordset1_cmd.Prepared = true
	response.Write(Recordset1_cmd.CommandText)
	Set Recordset1 = Recordset1_cmd.Execute
	Recordset1_numRows = 0

    'insertamos en la tabla de pagos
	Set RecordsetDet_cmd = Server.CreateObject ("ADODB.Command")
	RecordsetDet_cmd.ActiveConnection = MM_Conecta1_STRING
	RecordsetDet_cmd.CommandText = "UPDATE detallesPagoFacturas SET ImpSaldoAnt = "&saldoAnterior&", impSaldoInsoluto = "&saldoInsoluto&", impPagado = " & cantidad & ", tipoCambioDR = '"&request.Form("TipoCambioDR")&"' WHERE idcc = "&Request.QueryString("idPago") &" AND idfactura = "&request.QueryString("idfactura")
	RecordsetDet_cmd.Prepared = true
	response.Write(RecordsetDet_cmd.CommandText)
	Set RecordsetDet = RecordsetDet_cmd.Execute
	RecordsetDet_numRows = 0

    response.Redirect("pagosFacturaAgregar.aspx?idfactura="&Request.QueryString("idfactura")&"&idpago="&Request.QueryString("idPago"))
%>
<script>
		alert("Pago Modificado");
</script>
<%
End If
'-------------------------------------------------------------------------------------------------------'

'obtenemos los datos de la factura
Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT folio, serie, factura.fechaAlta, nombreCliente, factura.idCliente, descripcion, factura.metodoPago, rfcCliente, total, factura.cambio, iva, truncar, isr, cadena_original, factura.retencion FROM factura JOIN clientesFacturacion AS clientes ON clientes.idcliente = factura.idcliente JOIN moneda ON moneda = idmd WHERE idfactura = "&Request.QueryString("idfactura")
Recordset1_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

'buscamos el maximo numero de parcialidad
Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT MAX(noParcialidad) AS maximo FROM dbo.pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")&" AND estatus <> 'Cancelado'"
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute

'buscamos la suma de los pagos de la factura'
Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT SUM(d.impPagado) AS suma FROM dbo.detallespagoFacturas d INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE d.idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado'"
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute

'obtenemos el total de la factura
Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT precio_unitario, cantidad, descuento FROM dbo.detFactura WHERE id_factura = "&Request.QueryString("idfactura")
Recordset4_cmd.Prepared = true
Set Recordset4 = Recordset4_cmd.Execute

while(Not Recordset4.EOF)

	if recordset4.fields.item("descuento").value > 0 Then
        precioUnitario = truncarAseis(Recordset4.Fields.Item("precio_unitario").Value * ((100 - Recordset4.Fields.Item("descuento").Value) / 100))
    Else
        precioUnitario = truncarAseis(Recordset4.Fields.Item("precio_unitario").Value)
	End If
	
        'validamos si se trunca o redondea
	If Recordset1.Fields.Item("truncar").Value = "si" Then
		totalConcepto = truncarAdos(precioUnitario * Recordset4.Fields.Item("cantidad").Value)
		subtotal = subtotal + truncarAdos(totalConcepto)
		If Recordset1.Fields.Item("iva").Value <> "0" Then
			impuesto = truncarAdos(totalConcepto * 0.16)
			totalImpuesto = totalImpuesto + truncarAdos(impuesto)
		Else
			impuesto = 0
			totalImpuesto = 0
		End If
	Else
		totalConcepto = redondear(precioUnitario * Recordset4.Fields.Item("cantidad").Value)
		subtotal = subtotal + redondear(totalConcepto)
		If Recordset1.Fields.Item("iva").Value <> "0" Then
			impuesto = redondear(totalConcepto * 0.16)
			totalImpuesto = totalImpuesto + redondear(impuesto)
		Else
			impuesto = 0
			totalImpuesto = 0
		End If		
	End If
	
	Recordset4.MoveNext()
Wend

'obtenemos el total
If Recordset1.Fields.Item("truncar").Value = "si" Then
	total = truncarAdos(subtotal + totalImpuesto)

	If Recordset1.Fields.Item("isr").Value = "si" Then
		total = truncarAdos(subtotal - (subtotal * 0.15))
	End If
Else
	total = redondear(subtotal + totalImpuesto)

	If Recordset1.Fields.Item("isr").Value = "si" Then
		total = redondear(subtotal - (subtotal * 0.15))
	End If
End If

'obtenemos los datos del pago
Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT cantidad_recibida, metodoPago, noParcialidad, RfcEmisorCtaOrd, CtaOrdenante, NomBancoOrdExt, RfcEmisorCtaBen, CtaBeneficiario, formaPago, moneda, tipoCambioDR, noOperacion, fechaAlta FROM dbo.pagosFactura WHERE idcc = "&Request.QueryString("idPago")
Recordset4_cmd.Prepared = true
Set Recordset4 = Recordset4_cmd.Execute

    'obtenemos los datos del pago
Set RecordsetMonto_cmd = Server.CreateObject ("ADODB.Command")
RecordsetMonto_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetMonto_cmd.CommandText = "SELECT impPagado, tipoCambioDR FROM dbo.detallespagoFacturas WHERE idfactura = " & request.QueryString("idfactura") & "AND idcc = " & Request.QueryString("idPago")
'response.write(RecordsetMonto_cmd.CommandText)
RecordsetMonto_cmd.Prepared = true
Set RecordsetMonto = RecordsetMonto_cmd.Execute


If Not Recordset2.EOF Then
	If Recordset2.Fields.Item("maximo").Value <> "" And Not IsNull(Recordset2.Fields.Item("maximo").Value) Then
		parcialidad = Recordset2.Fields.Item("maximo").Value
	End If
End If

Set RecordsetF_cmd = Server.CreateObject ("ADODB.Command")
RecordsetF_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetF_cmd.CommandText = "SELECT * FROM dbo.formaPagoSat ORDER BY codigo ASC" 
RecordsetF_cmd.Prepared = true

Set RecordsetF = RecordsetF_cmd.Execute
RecordsetF_numRows = 0

Set RecordsetMoneda_cmd = Server.CreateObject ("ADODB.Command")
RecordsetMoneda_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetMoneda_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE descripcion <> '' ORDER BY descripcion desc" 
RecordsetMoneda_cmd.Prepared = true

Set RecordsetMoneda = RecordsetMoneda_cmd.Execute
RecordsetMoneda_numRows = 0

    if Recordset1.Fields.Item("descripcion").value = Recordset4.Fields.Item("moneda").value then
        opcionHab = "disabled"
    else
        opcionHab = ""
    end if 
if Recordset1.fields.item("retencion") > 0 then
    total = subtotal
else
    total = total
end if 


'valdiar si la factura tiene antixipo
Set RecordsetAnticipo_cmd = Server.CreateObject ("ADODB.Command")
RecordsetAnticipo_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetAnticipo_cmd.CommandText = "SELECT * FROM dbo.anticipo WHERE idFacturaRelacionada = " & request.QueryString("idfactura")
RecordsetAnticipo_cmd.Prepared = true

Set RecordsetAnticipo = RecordsetAnticipo_cmd.Execute
RecordsetAnticipo_numRows = 0

if NOT RecordsetAnticipo.eof then
    total = total - RecordsetAnticipo.fields.item("montoAnt").value    
end if
%>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="calendar/tcal.css" /> 
    <script type="text/javascript" src="calendar/tcal.js"></script>     
    <title>Verificar Pago</title>
    <script>
		window.onload = function(){

		}

		function pagar()
		{
			
            var form = document.form1

            if(form.formaPago.value == "")
            { 
                alert("Debe seleccionar la forma de pago")
                return false
            }
            
            if (form.moneda.value == "")
            {
                alert("Debe seleccionar moneda")
                return false
            }
            
            if(form.cambioP.value == "")
            {
                alert("Debe ingresar cambio Pago")
                return false        
            }

            if(isNaN(form.cambioP.value))
            {
                alert("Debe ingresar solo numeros")
                return false        
            }
        
            if (form1.TipoCambioDR.value == "0") {
                alert("El tipo de CambioDR no puede ser 0")
                return false;
            }            
        
            if (form1.TipoCambioDR.value == "") {
                alert("Debe ingresar CambioDR")
                return false;
            }

            if(form.moneda.value != form.monedaF.value)
            {
       
                if(form.TipoCambioDR.value == "")
                { 
                    alert("Debe ingresar TipoCambioDR")
                    return false   
             
                }else{
                    if (confirm("Guardar Pago?")) 
			        {
				        return true;
			        }else{
                        return false;
                    }
                }
            }

            if(form.cantidad.value == "0" || form.cantidad.value == "")
            {
            	alert("Debe ingresar Cantidad")
            	return false
            }

            if(isNaN(form.cantidad.value))
            {
                alert("Debe ingresar solo numeros")
                return false        
            }

            if (form.RfcEmisorCtaOrd.value == "XEXX010101000") 
            {
                if (form.NomBancoOrdExt.value == "") {
                    alert("El campo de NomBancoOrdExt es obligatorio si el RfcEmisorCtaOrd es Genérico o Extranjero");
                    return false;
                }

                if (form.NomBancoOrdExt.value.length != 10 && form.NomBancoOrdExt.value.length != 18) {
                    alert("El campo de NomBancoOrdExt debe tener 10 o 18 caracteres");
                    return false;
                }
                else{
                    if (confirm("Guardar Pago?")) 
                    {
                        return true;
                    }else{
                        return false;
                    }
                }
            }

            else{
                if (confirm("Guardar Pago?")) 
			    {
				    return true;
			    }else{
                    return false;
                }
            }
		}

		function parcialidades()
		{
			var selector = document.form1.metodoPago;

			if (selector.value == "PPD")
			{
				document.form1.parcialidad.value = <%=parcialidad + 1%>;
			}
			else
			{
				document.form1.parcialidad.value = 0;
			}
		}

		function cantidades()
		{
			var form = document.form1

			if (parseFloat(form.cantidad.value) > parseFloat(form.saldo.value))
			{
				alert("La cantidad no puede ser mayor al saldo disponible de la factura");
                form.cantidad.value = form.saldo.value;
			}
		}
        
        function bancoOrdExt(valor)
        {
            var form = document.form1

            if (valor == "XEXX010101000") 
            {
                form.NomBancoOrdExt.disabled = false;
            }
            else
            {
                form.NomBancoOrdExt.disabled = true;
            }
        }
    </script>
</head>
<body>
    <div align="center">
        <h2>
            <center>Modificar Pago</center>
        </h2>
        <div align="left">
            <a href="pagosFactura.asp?idfactura=<%=Request.QueryString("idfactura")%>">
                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" />
                Regresar</a>
        </div>
        <form method="post" name="form1" id="form1" action="pagosFacturaMod.asp?idfactura=<%=Request.QueryString("idfactura")%>&idpago=<%=Request.QueryString("idPago")%>">
            <table width="80%">
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right"># Parcialidad:</td>
                    <td><%=Recordset4.Fields.Item("noParcialidad").Value%></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Folio:</td>
                    <td><%=Recordset1.Fields.Item("folio").Value%>
                        <input type="hidden" name="folio" value="<%=Recordset1.Fields.Item("folio").Value%>">
                        
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Serie:</td>
                    <td><%=Recordset1.Fields.Item("serie").Value%>
                        <input type="hidden" name="serie" value="<%=Recordset1.Fields.Item("serie").Value%>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Fecha CFDI:</td>
                    <td><%=Recordset1.Fields.Item("fechaAlta").Value%></td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Cliente:</td>
                    <td><%=Recordset1.Fields.Item("nombreCliente").Value%>
                        <input type="hidden" name="idCliente" value="<%=Recordset1.Fields.Item("idCliente").Value%>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">RFC:</td>
                    <td><%=Recordset1.Fields.Item("rfcCliente").Value%>
                        <input type="hidden" name="rfcCliente" value="<%=Recordset1.Fields.Item("rfcCliente").Value%>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">MonedaP:</td>
                    <td>
                        <select name="moneda" id="moneda">

                            <option value="">Seleccionar</option>
                            <%
                            While (NOT RecordsetMoneda.EOF)
                            %>

                            <option value="<%=(RecordsetMoneda.Fields.Item("cMoneda").Value)%>" <%If (Not isNull(Recordset4.Fields.Item("moneda").value)) Then If (CStr(RecordsetMoneda.Fields.Item("cMoneda").Value) = CStr(Recordset4.Fields.Item("moneda"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(RecordsetMoneda.Fields.Item("descripcion").Value)%> </option>
                            <%
                              RecordsetMoneda.MoveNext()
                            Wend
                            %>
                        </select>
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">TipoCambioP:</td>
                    <td>
                        <input type="text" name="cambioP" id="cambio" value="<%=recordset4.fields.item("tipoCambioDR").value %>" onchange="conversion()" onkeypress="return SoloNumerosDec(event);">
                       
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Forma de Pago:</td>
                    <td> <select name="formaPago" id="formaPago">
                            <option value="">Seleccionar</option>
                                              <%
                            While (NOT RecordsetF.EOF)
                            %>
                             <option value="<%=(RecordsetF.Fields.Item("codigo").Value)%>" <%If (Not isNull(Recordset4.Fields.Item("formaPago"))) Then If (CStr(Recordset4.Fields.Item("formaPago").Value) = RecordsetF.Fields.Item("codigo").Value) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(RecordsetF.Fields.Item("codigo").Value)%> - <%=(RecordsetF.Fields.Item("descripcion").Value)%></option>
                            <%
                              RecordsetF.MoveNext()
                            Wend
                            %>
                    </select>
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Total Factura:</td>
                    <%
                    'obtenemos el total de la factura con la cadena original
                    totales = split(Recordset1.Fields.Item("cadena_original").Value, "|")
                    %>
                    <td>$ <%=total%>
                        <input type="hidden" name="totalFactura" id="totalFactura" value="<%=total%>">
                    </td>

                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Saldo:</td>
                    <td>$ 
	      			<%If Recordset3.Fields.Item("suma").Value = "" Or IsNull(Recordset3.Fields.Item("suma").Value) Then
	      				saldo = total
	      			Else
		      			If Recordset1.Fields.Item("truncar").Value = "si" Then
							saldo = truncarAdos(total - Recordset3.Fields.Item("suma").Value)
		      			Else
							saldo = redondear(total - Recordset3.Fields.Item("suma").Value)
		      			End If
	      			End If

	      			Response.Write(saldo)%>
                        <input type="hidden" id="saldo" value="<%=saldo%>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Tipo Pago:</td>
                    <td>
                        <select name="metodoPago" id="metodoPago" onchange="parcialidades()">
                           <option value="PPD" <%If Recordset4.Fields.Item("metodoPago").Value = "PPD" Then Response.Write("selected='selected'") %>>Pago en parcialidades o diferido</option>
                        </select>
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Cantidad:</td>
                    <td>
                        <input type="text" name="cantidad" id="cantidad" value="<%=RecordsetMonto.Fields.Item("impPagado").Value%>" onkeypress="return SoloNumerosDec(event);"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Fecha Pago:</td>
                    <td>
                        <input type="text" name="fechaAlta" id="fechaAlta" value="<%=FormatDateTime(Recordset4.Fields.Item("fechaAlta").Value, 2)%>" class="tcal" readonly></td>
                </tr>                  
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">No. operacion:</td>
                    <td>
                        <input type="text" name="NoOperacion" id="NoOperacion" value="<%=Recordset4.Fields.Item("noOperacion").Value %>"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">RfcEmisorCtaOrd:</td>
                    <td>
                        <input type="text" name="RfcEmisorCtaOrd" id="RfcEmisorCtaOrd" value="<%=Recordset4.Fields.Item("RfcEmisorCtaOrd").Value%>" size="30" onchange="bancoOrdExt(this.value)"></td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">CtaOrdenante:</td>
                    <td>
                        <input type="text" name="CtaOrdenante" id="CtaOrdenante" value="<%=Recordset4.Fields.Item("CtaOrdenante").Value%>" size="30" onkeypress="return SoloNumeros(event);"></td>
                </tr>
                <tr bgcolor="<%=cgri1%>">
                    <td align="right">NomBancoOrdExt:</td>
                    <td>
                        <input type="text" name="NomBancoOrdExt" id="NomBancoOrdExt" size="30" value="<%=Recordset4.Fields.Item("NomBancoOrdExt").Value%>" <%If Recordset4.Fields.Item("NomBancoOrdExt").Value = "" Then Response.Write("disabled") End If%>></td>
                </tr>                
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">RfcEmisorCtaBen:</td>
                    <td>
                        <input type="text" name="RfcEmisorCtaBen" id="RfcEmisorCtaBen" value="<%=Recordset4.Fields.Item("RfcEmisorCtaBen").Value%>" size="30"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">CtaBeneficiario:</td>
                    <td>
                        <input type="text" name="CtaBeneficiario" id="CtaBeneficiario" value="<%=Recordset4.Fields.Item("CtaBeneficiario").Value%>" size="30" onkeypress="return SoloNumeros(event);"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="center" colspan="2" >Moneda de la Factura seleccionada:</td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">MonedaDR:</td>
                    <td>
                        <%=Recordset1.Fields.Item("descripcion") %><input type="hidden" name="monedaF" id="monedaF" value="<%=Recordset1.Fields.Item("descripcion") %>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">TipoCambioDR:</td>
                    <td>
                        <input type="text" name="TipoCambioDR" id="TipoCambioDR" size="30" onkeypress="return SoloNumerosDec(event);" value="<%=RecordsetMonto.Fields.Item("tipoCambioDR").Value %>"  /></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <input type="submit" value="Modificar" onclick="return pagar()">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td colspan="2" align="center">
                        <br>
                        <a href="pagosFactura.asp?idfactura=<%=Request.QueryString("idfactura")%>">
                            <img src="imagenes/Arrow-right.png" width="20" height="20">Dejar Pendiente</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	      			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	      			<a href="pagosFacturaMod.asp?idfactura=<%=Request.QueryString("idfactura")%>&idPago=<%=Request.QueryString("idPago")%>&elim=si">
                          <img src="imagenes/banned.png" width="20" height="20">Cancelar</a></td>
                    </td>	
                </tr>

            </table>
        </form>
        <p>&nbsp;</p>
    </div>
</body>
<script type="text/javascript">

    //Se utiliza para que el campo de texto solo acepte numeros
    function SoloNumeros(evt) {
        if (window.event) {//asignamos el valor de la tecla a keynum
            keynum = evt.keyCode; //IE
        }
        else {
            keynum = evt.which; //FF
        }
        //comprobamos si se encuentra en el rango numérico y que teclas no recibirá.
        if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 6) {
            return true;
        }
        else {
            return false;
        }
    }

    function SoloNumerosDec(evt) {

        if (window.event) {//asignamos el valor de la tecla a keynum
            keynum = evt.keyCode; //IE
        }
        else {
            keynum = evt.which; //FF
        }
        //comprobamos si se encuentra en el rango numérico y que teclas no recibirá.
        if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 6 || keynum == 46) {
            return true;
        }
        else {
            return false;
        }
    }

    //Se utiliza para que el campo de texto solo acepte numeros
    function SoloNumeros1(evt) {
        var form1 = document.form1;

        if (form1.TipoCambioDR.value == "0") {
            alert("El tipo de CambioDR no puede ser 0")
            return false;
        }
    }

    function tipoCambio() {
        var x = document.getElementById("moneda").value;
        var y = document.getElementById("monedaF").value;
        
        if (x != y) {
            document.getElementById("TipoCambioDR").disabled = false;
        } else {
            document.getElementById("TipoCambioDR").disabled = true;
        }
    }

    function conversion() {
        var form = document.form1;
        if (form1.monedaF.value == form1.moneda.value) {
            form1.TipoCambioDR.value = 1
        }
        else if (form1.monedaF.value == "MXN" && form1.moneda.value == "USD") {
            form1.TipoCambioDR.value = redondeo(form1.cambioP.value / 1, 6)
        } else {
            form1.TipoCambioDR.value = redondeo(1 / form1.cambioP.value, 6)
        }
    }

    function trunc(x, posiciones) {
        var s = x.toString();
        var l = s.toString();
        var decimalLength = s.indexOf('.') + 1;
        var numStr = s.substr(0, decimalLength + posiciones);
        return Number(numStr)
    }
    function redondeo(numero, decimales) {
        var flotante = parseFloat(numero);
        var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
        return resultado;
    }
</script>
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
%>