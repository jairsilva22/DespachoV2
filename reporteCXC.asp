<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="stylo2.asp"-->
<%
Response.Buffer = true
Server.ScriptTimeout=50000

Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

%>
<%
dim documento
dim pagos'Variable para guardar el pago
dim m'Variable guardar el mes
dim mes'Variable para filtrar el mes
Dim F 'variable para comparar folio de factura
Dim ano 'variable para comparar año de factura

m = Month(Date())
mes = " AND Month(f.fechacfd)="&Month(Date())

'if para validar que se filtra el mes
if Request.Form("mes") <> "0" AND Request.Form("mes") <> "" then'mes
mes = " AND Month(f.fechacfd)<="&request.Form("mes")
m = Cint(Request.Form("mes"))
elseif Request.Form("mes") = "0" then'mes
mes = ""
m = ""
end if'mes

If Request.Form("yearemiso") <> "" Then
  ano = " AND YEAR(f.fechaAlta) = "& Request.Form("yearemiso")
End If
Dim saldo
saldo=0

if request.Form("moneda") <> "" then
    mnda = " AND f.moneda = "&request.Form("moneda")
else
    mnda = ""
    saldoUSD = 0
    saldoMXN = 0
end if

Dim vctes ' variable que nos servira para hacer el select hacia un cliente especifico
vctes="AND f.idcliente="&request.form("CTES")
IF request.form("CTES")="TODOS" THEN
vctes=""
END IF


Dim montototalMXN
Dim montototalUSD
Dim saldoTotal
saldoTotal=0
Dim pagosTotal
pagosTotal=0

'para truncar a 6 decimales
    Function truncarAseis(valor)
        Dim digitos 'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 6)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAseis = valor
    End Function

 'truncar a 2 digitos
    Function truncarAdos( valor)
        Dim digitos'Variable para guardar los digitos

        'if para validar si el subtotal tiene digitos
        If InStr(valor, ".") <> 0 Then 'valor
            digitos = Mid(valor, InStr(valor, ".") + 1)
            'if para validar si tiene un solo digito
            If Len(digitos) <= 1 Then 'digitos
                valor = valor & "0"
            Else 'digitos
                valor = Mid(valor, 1, InStr(valor, ".") - 1)
                valor = valor & "." & Mid(digitos, 1, 2)
            End If 'digitos
        Else 'valor
            valor = valor & ".00"
        End If 'valor
        truncarAdos = valor
    End Function

    'funcion redondear a 2 digitos
    Function redondear(val)

        'if para validar si tiene digitos
        If InStr(val, ".") <> 0 Then
            'redondear la funcion
            val = Round(val, 2)
        Else
            val = val & ".00"
        End If
        redondear = val
    End Function

%>

<%

Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText ="SELECT DISTINCT f.idcliente, nombreCliente, terminoPago FROM dbo.factura f, dbo.clientesFacturacion, dbo.documento, dbo.formPago fP WHERE clientesFacturacion.idcliente=f.idcliente AND estcobranza <> 'Pagada' AND timbre = 'NO'  AND documento.iddocumento = f.tipo_comprobante AND documento.tipo = 'ingreso' AND f.forma_pago = fp.idpago AND fp.forma_Pago = 'PPD' "&vctes&" ORDER BY clientesFacturacion.nombrecliente ASC"
Recordset1_cmd.Prepared = true
'Response.Write Recordset1_cmd.CommandText
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

%>

<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat1__numRows

if request.Form("moneda") <> "" then
    Set RecordsetMnda_cmd = Server.CreateObject ("ADODB.Command")
    RecordsetMnda_cmd.ActiveConnection = MM_Conecta1_STRING
    RecordsetMnda_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE idmd="&request.Form("moneda")&" ORDER BY cMoneda"
    RecordsetMnda_cmd.Prepared = true

    Set RecordsetMnda = RecordsetMnda_cmd.Execute
   
    if NOT RecordsetMnda.EOF then
        moneda = RecordsetMnda.fields.item("cmoneda").value
    end if

end if
    Set RecordsetMnda_cmd = Server.CreateObject ("ADODB.Command")
    RecordsetMnda_cmd.ActiveConnection = MM_Conecta1_STRING
    RecordsetMnda_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE cmoneda='MXN' ORDER BY cMoneda"
    RecordsetMnda_cmd.Prepared = true

    Set RecordsetMnda = RecordsetMnda_cmd.Execute
   
    if NOT RecordsetMnda.EOF then
        monedaMXN = RecordsetMnda.fields.item("idmd").value
    end if

     Set RecordsetMndaUSD_cmd = Server.CreateObject ("ADODB.Command")
    RecordsetMndaUSD_cmd.ActiveConnection = MM_Conecta1_STRING
    RecordsetMndaUSD_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE cmoneda='USD' ORDER BY cMoneda"
    RecordsetMndaUSD_cmd.Prepared = true

    Set RecordsetMndaUSD = RecordsetMndaUSD_cmd.Execute
   
    if NOT RecordsetMndaUSD.EOF then
        monedaUSD = RecordsetMndaUSD.fields.item("idmd").value
    end if


function Separar(ByVal Cadena, ByRef sNumeros, ByRef sCadena)
    if Cadena <> null Then
        For i=1 To Len(Cadena)
          c = MID(Cadena,i,1)
          if IsNumeric(c) Then 
               sNumeros = sNumeros & c
            else 
               sCadena = sCadena & c
            End If  
        Next
    end if
End function
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- InstanceBeginEditable name="doctitle" -->
    <title><%=titlePage%></title>
    <!-- InstanceEndEditable -->
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />
    <style type="text/css">
        <!--
        body {
            background-color: #CCC;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }

        .Estilo1 {
            color: #000000
        }

        .Estilo7 {
            font-size: 9px
        }

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
            width: 800px;
        }
        -->
    </style>
    <!-- InstanceBeginEditable name="head" -->

    <!-- InstanceEndEditable -->
</head>

<body style="background-color:white;">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->

                <style type="text/css">
                    <!--
                    .stilo1 {
                        font-size: 12px;
                        font-weight: bold;
                    }

                    .stilo1 {
                        text-align: center;
                    }

                    .stilo2 {
                        font-size: 12px;
                    }
                    -->
                    p {
                        padding-left: 50px;
                    }
                </style>
                <br />
                <table width="100%">
                    <tr>
                        <td width="20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="FiltroCxc.asp">
                            <img src="imagenes/Arrow-right.png" width="16" height="16" />
                            Regresar</a></td>
                        <td width="60%">
                            <h2 align="center">Cargo de los clientes</h2>
                            <%
                                fechaEm = "1/"&Request.Form("mes")&"/"&Request.Form("yearemiso")
                            %>
                            <h4 align="center">&nbsp; </h4>
                        </td>
                        <td width="20%" align="center"><a href="ReporteExcelCXC.asp?mes=<%=Request.Form("mes") %>&ano=<%= Request.Form("yearemiso") %>&moneda=<%= Request.Form("moneda") %>&CTES=<%=request.form("CTES") %>">
                            <img src="img/arrow-down.png" width="16" height="16" />
                            Generar Excel</a></td>

                    </tr>
                </table>

                <strong>
                    <p align="left">
                        <%=Day(date()) & " de " & MonthName(Month(date())) & " del " & Year(date())  %><br />
                        <%=moneda %>
                    </p>
                </strong>
                <table width="92%" border="0"  >
                    <tr>
                        <td colspan="5" width="50%">Cliente
                        </td>
                        <td colspan="6" width="50%" align="left">Condiciones
                        </td>
                    </tr>
                    <tr class="stilo1">
                        <td colspan="11" align="right">
                            <hr width="100%" />
                        </td>
                    </tr>
                    <% 
                      
                        cantCte = 0
                        While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) 
                   
                        cadena =""
                        sNumeros=""
                        sCadena=""
                        saldoP = 0
                        'valdiar si tiene facturas 
                         Set RecordsetValFact_cmd = Server.CreateObject ("ADODB.Command")
                         RecordsetValFact_cmd.ActiveConnection = MM_Conecta1_STRING
                         'RecordsetValFact_cmd.CommandText = "SELECT f.idcliente, f.fechacfd, f.idfactura, f.folio, f.idruta, f.estatus, f.total, c.idcliente, c.nombrecliente, c.rfcCliente, f.retencion, f.iva, d.descripcion as x, f.moneda FROM dbo.factura f LEFT JOIN detallesPagoFacturas p ON f.idfactura = p.idfactura INNER JOIN clientes c ON c.idcliente=f.idcliente INNER JOIN documento d ON tipo_comprobante = d.iddocumento INNER JOIN formPago fp ON f.forma_pago = fp.idpago AND fp.formaPago = 'PPD' WHERE p.idfactura IS NULL and f.idcliente = "&Recordset1.fields.item("idcliente").value&" AND f.estcobranza <> 'Pagada' AND f.timbre = 'NO' AND d.tipo = 'ingreso' AND f.estatusP IS NULL ORDER BY c.nombrecliente, fechacfd ASC"
                         RecordsetValFact_cmd.CommandText = "SELECT f.idcliente, f.fechacfd, f.idfactura, f.folio, f.idruta, f.estatus, f.total, c.idcliente, c.nombrecliente, c.rfcCliente, f.retencion, f.iva, d.descripcion as x, f.moneda FROM dbo.factura f LEFT JOIN detallesPagoFacturas p ON f.idfactura = p.idfactura INNER JOIN clientesFacturacion c ON c.idcliente=f.idcliente INNER JOIN documento d ON tipo_comprobante = d.iddocumento INNER JOIN formPago fp ON f.forma_pago = fp.idpago AND fp.forma_pago = 'PPD' WHERE p.idfactura IS NULL and f.idcliente = "&Recordset1.fields.item("idcliente").value&" AND f.estcobranza <> 'Pagada' AND f.timbre = 'NO' AND d.tipo = 'ingreso' ORDER BY c.nombrecliente, fechacfd ASC"
                            'response.Write( RecordsetValFact_cmd.CommandText &"<br>")
                        RecordsetValFact_cmd.Prepared = true
                                   
                         Set RecordsetValFact = RecordsetValFact_cmd.Execute
                       
                        if NOT RecordsetValFact.EOF then
                            saldoP = 0
                            impP = 0
                            ' SELECT ISNULL(SUM(d.impPagado), 0) as impPagado from detallesPagoFacturas d INNER JOIN pagosFactura p ON p.idcc = d.idcc where d.folio = 12352 and p.timbre = 'NO' AND estatus = 'Pagado'
                             Set RecordsetValCP_cmd = Server.CreateObject ("ADODB.Command")
                             RecordsetValCP_cmd.ActiveConnection = MM_Conecta1_STRING
                             RecordsetValCP_cmd.CommandText = "SELECT ISNULL(SUM(d.impPagado), 0) as impPagado from detallesPagoFacturas d INNER JOIN pagosFactura p ON p.idcc = d.idcc where d.folio = "&RecordsetValFact.fields.item("folio").value&" and p.timbre = 'NO' AND estatus = 'Pagado'"           
                                'response.Write( RecordsetValCP_cmd.CommandText &"<br>")
                             RecordsetValCP_cmd.Prepared = true
                                   
                             Set RecordsetValCP = RecordsetValCP_cmd.Execute

                            if NOT RecordsetValCP.EOF then
                                saldoP = CDbl(RecordsetValFact.fields.item("total").value) - CDbl(RecordsetValCP.fields.item("impPagado").value)
                                impP = CDbl(RecordsetValCP.fields.item("impPagado").value)
                            else
                                saldoP = CDbl(RecordsetValFact.fields.item("total").value)
                            end if
                        end if

                        if saldoP > 0 then
                    %>
                    <tr>
                        <td colspan="5">
                            <%=Recordset1.Fields.item("nombreCliente").value %>
                        </td>
                        <td colspan="5">
                            <% 

cadena = Recordset1.Fields.Item("terminoPago").value 
call Separar(cadena, sNumeros, sCadena)
                    
                            %>
                            <%=sNumeros %>
                        </td>
                    </tr>
                    <tr class="stilo1">
                        <th>&nbsp;&nbsp;&nbsp;</th>
                        <th align="left">Concepto</th>
                        <th align="left">Folio</th>
                        <th align="left">Fecha</th>
                        <th align="left">Vence</th>
                        <th>Atraso</th>
                        <th align="left">Descripci&oacute;n</th>
                        <th align="left">Importe</th>
                        <th colspan="2" align="right">Saldo MXN</th>
                        <th colspan="2" align="right">Saldo USD</th>
                    </tr>
                    <%
     totalEMXN = 0
                        totalEUSD = 0
       cantCte = cantCte + 1  
       contUSD = 0
       contMXN = 0
       Set RecordsetFact_cmd = Server.CreateObject ("ADODB.Command")
       RecordsetFact_cmd.ActiveConnection = MM_Conecta1_STRING
       'RecordsetFact_cmd.CommandText = "SELECT f.idcliente, f.fechacfd, f.idfactura, f.folio, f.idruta, f.estatus, f.total, c.idcliente, c.nombrecliente, c.rfcCliente, f.retencion, f.iva, d.descripcion as x, f.moneda FROM dbo.factura f LEFT JOIN detallesPagoFacturas p ON f.idfactura = p.idfactura INNER JOIN clientes c ON c.idcliente=f.idcliente INNER JOIN documento d ON tipo_comprobante = d.iddocumento INNER JOIN formPago fp ON f.forma_pago = fp.idpago AND fp.formaPago = 'PPD' WHERE p.idfactura IS NULL and f.idcliente = "&Recordset1.fields.item("idcliente").value&" AND f.estcobranza <> 'Pagada' AND f.timbre = 'NO' AND d.tipo = 'ingreso' AND f.estatusP IS NULL ORDER BY c.nombrecliente, fechacfd ASC"
       RecordsetFact_cmd.CommandText = "SELECT f.idcliente, f.fechacfd, f.idfactura, f.folio, f.idruta, f.estatus, f.total, c.idcliente, c.nombrecliente, c.rfcCliente, f.retencion, f.iva, d.descripcion as x, f.moneda FROM dbo.factura f LEFT JOIN detallesPagoFacturas p ON f.idfactura = p.idfactura INNER JOIN clientesFacturacion c ON c.idcliente=f.idcliente INNER JOIN documento d ON tipo_comprobante = d.iddocumento INNER JOIN formPago fp ON f.forma_pago = fp.idpago AND fp.forma_pago = 'PPD' WHERE p.idfactura IS NULL and f.idcliente = "&Recordset1.fields.item("idcliente").value&" AND f.estcobranza <> 'Pagada' AND f.timbre = 'NO' AND d.tipo = 'ingreso' ORDER BY c.nombrecliente, fechacfd ASC"
                        '"SELECT factura.idcliente, factura.fechacfd, factura.idfactura, factura.folio, factura.idruta, factura.estatus, factura.total, clientes.idcliente, clientes.nombrecliente, clientes.rfcCliente, factura.retencion, factura.iva, documento.descripcion as x FROM dbo.factura, dbo.clientes, dbo.documento WHERE clientes.idcliente=factura.idcliente AND tipo_comprobante = iddocumento " & mes & ano & mnda & " and factura.idcliente = "&Recordset1.fields.item("idcliente").value&" ORDER BY clientes.nombrecliente, fechacfd ASC "
                                
       RecordsetFact_cmd.Prepared = true
       ' Response.Write RecordsetFact_cmd.CommandText & "<br>"
       Set RecordsetFact = RecordsetFact_cmd.Execute
                                          
       while (NOT RecordsetFact.EOF)
            
            'calcular el total de las facturas
            '----------------------------------------------------------------------------------------calcular total de la factura'
            subtotal = 0
            totalImpuestosTrasladados = 0 
            total = 0
            totalImpuestosretenidos =0

            Set RecordsetDet_cmd = Server.CreateObject ("ADODB.Command")
            RecordsetDet_cmd.ActiveConnection = MM_Conecta1_STRING
            RecordsetDet_cmd.CommandText = "SELECT cantidad, precio_unitario, detFactura.descuento, ISNULL(detFactura.iva, 0) AS iva FROM dbo.detFactura WHERE id_factura = " & RecordsetFact.fields.item("idfactura").value 
            RecordsetDet_cmd.Prepared = true
         
            Set RecordsetDet = RecordsetDet_cmd.Execute
            RecordsetDet_numRows = 0
        
            While (NOT RecordsetDet.EOF)

              If RecordsetDet.Fields.item("cantidad").Value <> 0 Then 'cantidad
                cantidad = RecordsetDet.Fields.item("cantidad").Value
              End If 'cantidad

              'if para validar que tiene precio_unitario
              If RecordsetDet.Fields.item("precio_Unitario").Value <> 0 Then 'precio_unitario
                precioUnitario = (RecordsetDet.Fields.item("precio_Unitario").Value)
              End If 'precio_unitario
                'response.write(precioUnitario)

              'if para validar si tiene descuento
              If RecordsetDet.Fields("descuento").Value <> 0 Then
                des = (RecordsetDet.Fields("descuento").Value)
              Else
                des = 0
              End If

              '------------------para el precio Unitario
              'response.write(Recordset3.Fields.Item("truncar").Value)
              If (Otrunc) = "si" Then
                If des > 0 Then
                  precio_Unitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                  precio_Unitario = truncarAseis(precioUnitario)
                End If
                totalConcepto = truncarAdos(precioUnitario * cantidad)
              Else
          
                If des > 0 Then
                  precio_unitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                  precio_unitario = truncarAseis(precioUnitario)
                End If
            
                totalConcepto = redondear(precio_unitario * cantidad)
              End If

              'response.write(totalConcepto &" :totalConcepto -cantidad:" & cantidad)
                'impuestos-----------------'
    
              retCte = RecordsetFact.Fields.item("retencion").Value
                'validar si la factura contiene iva'

              if RecordsetFact.fields.item("iva").value <> 0 then
                iva = 16
              else
                iva = 0
              end if 

               '' iva = RecordsetDet.Fields.Item("iva").Value
                'response.write(iva & " iva " & retCte)
              If Otrunc = "si" Then
                  If des > 0 Then
                    precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                  Else
                    precioUnitario = truncarAseis(precioUnitario)
                  End If
                totalConcepto = truncarAdos(precioUnitario * cantidad)
                subTotal = subtotal + truncarAdos(totalConcepto)
            
                  'validamos si la factura tiene iva
                If iva <> 0 Then
                  impuesto = truncarAdos(totalConcepto * (iva / 100))
                  totalImpuestosTrasladados = totalImpuestosTrasladados + truncarAdos(impuesto)
                End If
                subTotal = truncarAdos(subTotal)
              Else
                If des > 0 Then
                  precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                  precioUnitario = truncarAseis(precioUnitario)
                End If
                
                totalConcepto = redondear(precioUnitario * cantidad)
                subTotal = CDbl(subtotal) + redondear(totalConcepto)
                     'validamos si la factura tiene iva

                If iva <> 0 Then
                       'Console.Write(totalConcepto)
                  impuesto = redondear(totalConcepto * ((iva) / 100))
                  totalImpuestosTrasladados = totalImpuestosTrasladados +  redondear(impuesto)
     ''                     response.write("Imp trasaladodos...."&totalImpuestosTrasladados&"....")
                End If
                If retCte > 0 Then
                        'Console.Write(totalConcepto)
                  retencion = redondear(totalConcepto * (0.16))
                  totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
                Else
                  totalImpuestosretenidos = 0
                End If
                subTotal = redondear(subTotal)   
                     '' response.write("subTotal:"&subTotal)
              end if

              Dim isr
            'obtenemos el total
              If truncar = "si" Then
                total = truncarAdos(totalImpuestosTrasladados + subTotal)
              ''  totalImpuestosTrasladados = 0
              Else
                total = redondear(totalImpuestosTrasladados + subTotal)
                ''  totalImpuestosTrasladados = 0
              End If
            'response.write("total ="&total&"#"& hayisr)
            'validamos si hay isr

              If hayisr <> "" And hayisr = "si" Then
                isr = (subTotal * 0.15)
                total = CDbl(subTotal) - isr

                If truncar = "si" Then
                    total = truncarAdos(total)
                Else
                    total = redondear(total)
                End If
              End If

        RecordsetDet.MoveNext()
        Wend

        If retCte > 0 Then

            total = subTotal
        Else

            total = (total)
        End If

        saldoMXN   = 0 
        saldoUSD = 0       
        if monedaUSD = RecordsetFact.Fields.item("moneda").Value then
              contUSD = contUSD + 1
              if impP > 0 then
                    saldoUSD = total - impP
              else
                    saldoUSD = total
              end if                    
        elseif monedaMXN = RecordsetFact.Fields.item("moneda").Value then
              contMXN= contMXN + 1
              if impP > 0 then
                    saldoMXN = total - impP
              else
                    saldoMXN = total
              end if 

        end if
                            
                    %>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td>Venta</td>
                        <td><%=RecordsetFact.fields.item("folio").value %></td>
                        <td><%=FormatDateTime(RecordsetFact.fields.item("fechacfd").value, 2) %></td>
                        <td>
                            <% if sNumeros <> "" then
                                    fechaVig = DateAdd("d", sNumeros, FormatDateTime(RecordsetFact.fields.item("fechacfd").value, 2))
                                else
                                    fechaVig = ""
                               end if
                                
                            %>
                            <%=fechaVig %>
                        </td>
                        <td align="center">
                            <%
                               if fechaVig <> "" then
                                    if fechaVig < Date() then
                                        cantVig = Date() - fechaVig
                                    else
                                        cantVig = "0"
                                    end if

                                else
                                    cantVig = "0"
                               end if
                               
                               
                            %>
                            <%=cantVig %>
                        </td>
                        <td></td>
                        <td><%=FormatNumber(total) %></td>
                        <%
                        'buscamos si hay datos de pagos pendientes en la tabla de logCXC
                        Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
                        Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
                        Recordset5_cmd.CommandText = "SELECT IsNULL(SUM(cantidad_recibida), 0) AS parcial FROM dbo.logCXC WHERE folio = "&RecordsetFact.fields.item("folio").value
                        Recordset5_cmd.Prepared = true
                        'response.write Recordset5_cmd.CommandText&"<br>"

                        Set Recordset5 = Recordset5_cmd.Execute

                        If Recordset5.Fields.Item("parcial").Value <> "0" And Recordset5.Fields.Item("parcial").Value <> "" Then
                            If monedaUSD = RecordsetFact.Fields.item("moneda").Value Then
                                saldoUSD = saldoUSD - Recordset5.Fields.Item("parcial").Value
                            ElseIf monedaMXN = RecordsetFact.Fields.item("moneda").Value Then
                                saldoMXN = saldoMXN - Recordset5.Fields.Item("parcial").Value
                            End If
                        End If

                        %>
                        <td colspan="2" align="right"><%=FormatNumber(saldoMXN) %></td>
                        <td colspan="2" align="right"><%=FormatNumber(saldoUSD) %></td>
                    </tr>
                    <% 
                           
                       totalEMXN = CDbl(saldoMXN) + CDbl(totalEMXN)
                       totalEUSD = CDbl(saldoUSD) + CDbl(totalEUSD)
                          
                        'response.Write(contMXN & " folio "& RecordsetFact.fields.item("folio").value &"total usd " & totalEUSD & " mxn " &totalEMXN&"<br>")
                        RecordsetFact.MoveNext()
                        wend
                        'response.Write(contMXN & "   "&contUSD & "<br>")
                            if(contMXN > 1) OR (contUSD > 1) then
                                if(contMXN > 1) then
                                    opcionMXN = FormatNumber(totalEMXN)
                                else
                                    opcionMXN = "&nbsp;0.00"
                                end if

                                if(contUSD > 1) then
                                    opcionUSD = FormatNumber(totalEUSD)
                                else
                                    opcionUSD = "&nbsp;0.00"
                                end if   

                                mTotal = "<tr><td colspan='8'>&nbsp;</td><td colspan='2' align='right'> <hr width='100%' />"&opcionMXN&"</td></td><td align='right'> <hr width='100%' />"&opcionUSD&"</td></tr>"
                            else
                                mTotal = ""
                            end if


                      '  totalE =  FormatNumber(totalE)
                        montototalMXN = montototalMXN + (totalEMXN)
                        montototalUSD = montototalUSD + (totalEUSD)
                    %>
                    <%=mTotal %>
                    <% end if %>


                    <% 
	  
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Response.Flush
Wend
                    %>

                    <tr>
                        <td colspan="5" align="left"><strong>Total <%=cantCte %> clientes</strong></td>
                        <td colspan="5" align="right"><%=formatnumber(montototalMXN,2)%></td>
                        <td align="right"><%=formatnumber(montototalUSD,2)%></td>
                    </tr>
                </table>
                <!-- InstanceEndEditable -->
                <p>&nbsp;</p>
                <%=footerPage%>
            </td>
        </tr>

    </table>

</body>
<!-- InstanceEnd -->
</html>


<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
<!--script que nos ayuda a abrir en otra ventana la caja para hacer un cobro-->
<script language="javascript">
    function popup() {
        open('CajaPopUp.asp', 'NewWindow', 'top=0,left=0,width=450,height=500,status=yes,resizable=yes,scrollbars=yes'); < !--instruccion para abrir la pagina cajapopup por como una ventana emergente-- >
	}
</script>
