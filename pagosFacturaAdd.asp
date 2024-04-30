<%@  language="VBSCRIPT" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows
Dim parcialidad, saldoAnterior, saldoInsoluto, cantidad, saldo, total, precioUnitario,totalConcepto

parcialidad = 0
saldo = 0
total = 0

If Request.Form("cantidad") <> "" And Request.Form("cantidad") <> "0" Then
    if(request.Form("moneda") <> request.Form("monedaF")) then
    '  if request.Form("cambioP") <> "" then
           ' conver1 = redondear(request.Form("totalFactura") * request.Form("cambioP"))
            conver1 = redondear(request.Form("totalFactura"))
            cambioDR = 1 / request.Form("cambioP")
            cambio1 = request.Form("cambioP")
       ' end if
    else
      response.write("segundo ciclo")
   
          conver1 = redondear(Request.Form("totalFactura"))
          cambio1 = Request.Form("cambioP")
    end if
    'response.write(conver1)
	'response.Write(conver1 & " " & cambioDR)
	cantidad = Request.Form("cantidad")
	'calculamos los saldos correspondientes
	
	'------------SaldoInsoluto-------------
	'buscamos si hay más pagos anteriores a este
	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "SELECT TOP(1) impSaldoInsoluto FROM dbo.pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado' ORDER BY idcc DESC"
	Recordset2_cmd.Prepared = true
	'response.Write Recordset2_cmd.CommandText
	Set Recordset2 = Recordset2_cmd.Execute

	If Not Recordset2.EOF Then
		If Recordset2.Fields.Item("impSaldoInsoluto").Value = "" Or IsNull(Recordset2.Fields.Item("impSaldoInsoluto").Value) Then
			saldoInsoluto = (conver1 - cantidad)
		Else
			saldoInsoluto = (Recordset2.Fields.Item("impSaldoInsoluto").Value - cantidad)
		End If
	Else
		saldoInsoluto = (conver1 - cantidad)
	End If
    saldoInsoluto = redondear(saldoInsoluto)
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
			saldoAnterior = conver1
		End If
	Else
		saldoAnterior = conver1
	End If

	'insertamos en la tabla de pagos
	Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
	Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset1_cmd.CommandText = "INSERT INTO pagosFactura(idfactura, idCliente, cantidad_recibida, serie, formaPago, metodoPago, estatus, rfcProvCertif, moneda, tipoCambioDR, impSaldoAnt, impSaldoInsoluto, impPagado, noParcialidad, fechaAlta, RfcEmisorCtaOrd, CtaOrdenante, NomBancoOrdExt, RfcEmisorCtaBen, CtaBeneficiario, noOperacion) VALUES("&Request.QueryString("idfactura")&", "&Request.Form("idCliente")&", "&cantidad&", '"&Request.Form("serie")&"', '"&Request.Form("formaPago")&"', '"&Request.Form("metodoPago")&"', 'Pendiente', '"&Request.Form("rfcCliente")&"', '"&Request.Form("moneda")&"', '"&cambio1&"', "&saldoAnterior&", "&saldoInsoluto&", "&Request.Form("cantidad")&", "&Request.Form("parcialidad")&", CONVERT(datetime, '"&Request.Form("fechaAlta")&"', 103), '"&trim(Request.Form("RfcEmisorCtaOrd"))&"', '"&trim(Request.Form("CtaOrdenante"))&"', '"&trim(Request.Form("NomBancoOrdExt"))&"', '"&trim(Request.Form("RfcEmisorCtaBen"))&"', '"&trim(Request.Form("CtaBeneficiario"))&"', '" & trim(request.form("NoOperacion")) & "')"
	Recordset1_cmd.Prepared = true
	'response.Write(Recordset1_cmd.CommandText)
	Set Recordset1 = Recordset1_cmd.Execute
	Recordset1_numRows = 0

	'buscamos el ultimo registro generado
	Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
	Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset1_cmd.CommandText = "SELECT MAX(idcc) AS ultimo FROM pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")
	Recordset1_cmd.Prepared = true
	'response.Write(Recordset1_cmd.CommandText)
	Set Recordset1 = Recordset1_cmd.Execute
	Recordset1_numRows = 0

    'Consultar el id de couemnto
	Set RecordsetUUID_cmd = Server.CreateObject ("ADODB.Command")
	RecordsetUUID_cmd.ActiveConnection = MM_Conecta1_STRING
	RecordsetUUID_cmd.CommandText = "SELECT UUID FROM Factura WHERE idfactura = "&Request.QueryString("idfactura")
	RecordsetUUID_cmd.Prepared = true
	'response.Write(Recordset1_cmd.CommandText)
	Set RecordsetUUID = RecordsetUUID_cmd.Execute
	RecordsetUUID_numRows = 0

    'if request.Form("TipoCambioDR") = 0 then
     '   tipoCDR = request.Form("cambioA")
    'else
     '   tipoCDR = request.Form("TipoCambioDR")
  '  end if
   
    idDoc = split(RecordsetUUID.fields.item("UUID").value, "&id=" )
   ' response.Write(idDoc(1))
  '  .Substring(pago.idDocumento.IndexOf("&id=") + 4);
  	'insertamos en la tabla de los detalles de pagos
	Set RecordsetDet_cmd = Server.CreateObject ("ADODB.Command")
	RecordsetDet_cmd.ActiveConnection = MM_Conecta1_STRING
	RecordsetDet_cmd.CommandText = "INSERT INTO detallespagoFacturas(idcc, idfactura, folio, serie,  impSaldoInsoluto, impPagado, ImpSaldoAnt, noParcialidad, metodoPago, moneda, idDocumento, cambio, formaPago, tipoCambioDR, tComprobante) VALUES("&recordset1.fields.item("ultimo").value &", "&Request.QueryString("idfactura")&", "&Request.Form("folio")&", '"&Request.Form("serie")&"', "&saldoInsoluto&","&Request.Form("cantidad")&","&saldoAnterior&",  "&Request.Form("parcialidad")&",'"&Request.Form("metodoPago")&"', '"&Request.Form("monedaF")&"', '"&idDoc(1)&"','"&Request.Form("cambioP")&"','"&request.Form("formaPago")&"','"&request.Form("TipoCambioDR")&"', '"&request.Form("tipoC")&"')"
	RecordsetDet_cmd.Prepared = true
	response.Write(RecordsetDet_cmd.CommandText)
	Set RecordsetDet = RecordsetDet_cmd.Execute
	RecordsetDet_numRows = 0
	
	'vamos a la pagina donde se verifica/modifica el pago
	Response.Redirect("pagosFacturaAgregar.aspx?idfactura="&Request.QueryString("idfactura")&"&idPago="&Recordset1.Fields.Item("ultimo").Value)
End If
'-------------------------------------------------------------------------------------------------------'

'obtenemos los datos de la factura
Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT folio, serie, factura.fechaAlta, nombreCliente, factura.idCliente, descripcion, cmoneda, factura.metodoPago, rfcCliente, total, moneda.tcambio, iva, truncar, isr, moneda, factura.cambio AS facCambio, factura.tipo_comprobante, cadena_original, factura.retencion FROM factura JOIN clientesFacturacion AS clientes ON clientes.idcliente = factura.idcliente JOIN moneda ON moneda = idmd WHERE idfactura = "&Request.QueryString("idfactura")
Recordset1_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

'buscamos el maximo numero de parcialidad
Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT MAX(noParcialidad) AS maximo FROM dbo.pagosFactura WHERE idfactura = "&Request.QueryString("idfactura")&" AND estatus <> 'Cancelado' AND timbre = 'NO'"
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT SUM(d.impPagado) AS suma FROM dbo.detallespagoFacturas d INNER JOIN pagosFactura p on d.idcc = p.idcc WHERE d.idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado'"
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute

'obtenemos el total de la factura
Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT precio_unitario, cantidad, descuento FROM dbo.detFactura WHERE id_factura = "&Request.QueryString("idfactura")
'response.Write Recordset4_cmd.CommandText
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
	'response.Write totalImpuesto
	Recordset4.MoveNext()
Wend

'obtenemos el total
If Recordset1.Fields.Item("truncar").Value = "si" Then
	total = truncarAdos(subtotal + totalImpuesto)

	If Recordset1.Fields.Item("isr").Value = "si" Then
		total = truncarAdos(subtotal - (subtotal * 0.15))
	End If
	'response.Write "si"
Else
	total = redondear(subtotal + totalImpuesto)

	If Recordset1.Fields.Item("isr").Value = "si" Then
		total = redondear(subtotal - (subtotal * 0.15))
	End If
	'response.Write "no"&total
End If
	
'sacar el saldo anterior 
Set RecordsetSaldoAnt_cmd = Server.CreateObject ("ADODB.Command")
RecordsetSaldoAnt_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetSaldoAnt_cmd.CommandText = "SELECT MAX(d.impPagado) AS suma FROM dbo.detallespagoFacturas d INNER JOIN pagosFactura p on d.idcc = p.idcc WHERE d.idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado'"
RecordsetSaldoAnt_cmd.Prepared = true

Set RecordsetSaldoAnt = RecordsetSaldoAnt_cmd.Execute


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
RecordsetMoneda_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE descripcion <> '' ORDER BY descripcion ASC" 
RecordsetMoneda_cmd.Prepared = true

Set RecordsetMoneda = RecordsetMoneda_cmd.Execute
RecordsetMoneda_numRows = 0

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
    <title>Agregar Pago</title>
    <script>
		function pagar()
		{	
            var form = document.form1
        			
           if (parseFloat(form.cantidad.value) > parseFloat(form.saldo.value))
			{
				alert("La cantidad no puede ser mayor al saldo disponible de la factura");
                return false
			}

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
            <center>Agregar Pago</center>
        </h2>
        <div align="left">
            <a href="pagosFactura.asp?idfactura=<%=Request.QueryString("idfactura")%>">
                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" />
                Regresar</a>
        </div>
        <form method="post" name="form1" action="pagosFacturaAdd.asp?idfactura=<%=Request.QueryString("idfactura")%>">
            <table width="80%">
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
                        <select name="moneda" id="moneda" onchange="tipoCambio()">
                            <option value="">Seleccionar</option>
                            <%
                            While (NOT RecordsetMoneda.EOF)
                            %>
                            <option value="<%=(RecordsetMoneda.Fields.Item("cMoneda").Value)%>" <%If (Not isNull(Recordset1.Fields.Item("descripcion"))) Then If (CStr(RecordsetMoneda.Fields.Item("descripcion").Value) = CStr(Recordset1.Fields.Item("descripcion"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(RecordsetMoneda.Fields.Item("descripcion").Value)%> </option>
                            <%
                              RecordsetMoneda.MoveNext()
                            Wend
                            %>
                        </select>
                    </td>
                </tr>
              <!--  <tr bgcolor="<%=cgrid1%>">
                    <td align="right">TipoCambioP:</td>
                    <td>
                        <input type="text" name="cambio" id="cambio" value="" onkeypress="return SoloNumeros1(event);">
                    </td>
                </tr>-->
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Tipo Cambio Pago:</td>
                    <td><% if Recordset1.Fields.Item("cMoneda") = "MXN" then
                          monedaAT = "1"
                        else
                          monedaAT = "" 
                        end if %>
                        <input type="text" name="cambioP" id="cambioP" value="<%=monedaAT %>" onchange="conversion()" onkeypress="return SoloNumerosDec(event);">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Forma de Pago:</td>
                    <td>
                        <select name="formaPago" id="formaPago">
                            <option value="">Seleccionar</option>
                            <%
                            While (NOT RecordsetF.EOF)
                            %>
                            <option value="<%=(RecordsetF.Fields.Item("codigo").Value)%>" <%If (Not isNull(Request.Form("formaPago"))) Then If (CStr(RecordsetF.Fields.Item("codigo").Value) = CStr(Request.Form("formaPago"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(RecordsetF.Fields.Item("codigo").Value)%> - <%=(RecordsetF.Fields.Item("descripcion").Value)%></option>
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
                        'response.Write(totales(11))
                    %>
                    <td>$ <%=total%> 
                        <input type="hidden" name="totalFactura" id="totalFactura" value="<%=total%>">
                        <input type="hidden" name="totalFacturaA" id="totalFacturaA" value="<%=total%>">
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
                        <input type="hidden" id="saldo" name="saldo" value="<%=saldo%>">
                        <input type="hidden" id="saldoA" name="saldoA" value="<%=saldo%>">
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Tipo Pago:</td>
                    <td>
                        <select name="metodoPago" id="metodoPago" onchange="parcialidades()">
                            <option value="PPD" <%If Request.Form("metodoPago") = "PPD" Then Response.Write("selected") %>>Pago en parcialidades o diferido</option>
                        </select>
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">Cantidad:</td>
                    <td>
                        <input type="text" name="cantidad" id="cantidad" value="<%=saldo%>" onkeypress="return SoloNumerosDec(event);"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">Fecha Pago:</td>
                    <td>
                        <input type="text" name="fechaAlta" id="fechaAlta" value="<%=Date()%>" class="tcal" readonly></td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">No. operacion:</td>
                    <td>
                        <input type="text" name="NoOperacion" id="NoOperacion" ></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">RfcEmisorCtaOrd:</td>
                    <td>
                        <input type="text" name="RfcEmisorCtaOrd" id="RfcEmisorCtaOrd" size="30" onchange="bancoOrdExt(this.value)"></td>
                </tr>
                <tr bgcolor="<%=cgri2%>">
                    <td align="right">CtaOrdenante:</td>
                    <td>
                        <input type="text" name="CtaOrdenante" id="CtaOrdenante" size="30" onkeypress="return SoloNumeros(event);"></td>
                </tr>
                <tr bgcolor="<%=cgri1%>">
                    <td align="right">NomBancoOrdExt:</td>
                    <td>
                        <input type="text" name="NomBancoOrdExt" id="NomBancoOrdExt" size="30" disabled></td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">RfcEmisorCtaBen:</td>
                    <td>
                        <input type="text" name="RfcEmisorCtaBen" id="RfcEmisorCtaBen" size="30"></td>
                </tr>
                <tr bgcolor="<%=cgrid1%>">
                    <td align="right">CtaBeneficiario:</td>
                    <td>
                        <input type="text" name="CtaBeneficiario" id="CtaBeneficiario" size="30" onkeypress="return SoloNumeros(event);"></td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="center" colspan="2">Moneda de la Factura seleccionada:</td>
                    
                </tr><tr bgcolor="<%=cgrid1%>">
                    <td align="right">MonedaDR:</td>
                    <td>
                        <%=Recordset1.Fields.Item("descripcion") %>
                    </td>
                </tr>
                <tr bgcolor="<%=cgrid2%>">
                    <td align="right">TipoCambioDR:</td>
                    <td>
                        <input type="text" name="TipoCambioDR" id="TipoCambioDR" size="30" value ="<%=monedaAT %>" onkeypress="return SoloNumerosDec(event);"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <input type="submit" value="Guardar" onclick="return pagar()">
                        <input type="hidden" name="parcialidad" value="<%=parcialidad + 1%>">
                        <input type="hidden" name="monedaF" id="monedaF" value="<%=Recordset1.Fields.Item("cMoneda") %>">
                        <input type="hidden" name="cambioA" id="cambioA" value="<%=Recordset1.Fields.Item("facCambio") %>"/>
                        <input type="hidden" name="tipoC" value="<%=Recordset1.Fields.Item("tipo_comprobante") %>" />
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

    function tipoCambio() {
        var x = document.getElementById("moneda").value;
        var y = document.getElementById("monedaF").value;
       // if (x == "MXN" ) {
          
         //   document.getElementById("cambioP").value = "1";
       // }
       
        if (x != y) { 
            document.getElementById("TipoCambioDR").disabled = false;
        } else {
            document.getElementById("TipoCambioDR").disabled = true;
        }

       // document.getElementById("demo").innerHTML = "You selected: " + x;
    }

    function cambiarCantidades() {
      //  alert(document.getElementById("cambioP").value);
        if (document.getElementById("cambioP").value != "") {
            document.getElementById("totalFacturaA").value = "";
            document.getElementById("saldoA").value = "";
            totalT = (document.getElementById("totalFactura").value * document.getElementById("cambioP").value);
            totalS = document.getElementById("saldo").value * document.getElementById("cambioP").value;
            document.getElementById("totalFacturaA").value = totalT;
            document.getElementById("saldoA").value = totalS;
            alert(totalS + "rrrr");
        } else {
            document.getElementById("totalFacturaA").value = "";
            document.getElementById("saldoA").value = "";

            document.getElementById("totalFacturaA").value = document.getElementById("totalFactura").value;
            document.getElementById("saldoA").value = document.getElementById("saldo").value;
            alert(totalS + "ddd");
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