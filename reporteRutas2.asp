<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
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
mes = " AND Month(factura.fechacfd)="&Month(Date())

'if para validar que se filtro tipo de comprobante
if Request.Form("documento") <> "" then'documento
documento = " AND tipo_comprobante = " & Request.Form("documento") 
end if'documento

'if para validar que se filtra el mes
if Request.Form("mes") <> "0" AND Request.Form("mes") <> "" then'mes
mes = " AND Month(factura.fechacfd)="&request.Form("mes")
m = Cint(Request.Form("mes"))
elseif Request.Form("mes") = "0" then'mes
mes = ""
m = ""
end if'mes

F=" AND factura.folio="&request.Form("foliotext")
IF  request.Form("foliotext")="" THEN
F=""
END IF
Dim estatus 'variable para filtrar el estatus de la factura
estatus=" AND factura.estatus='"&request.form("ST")&"'"

IF  request.Form("ST")="" OR request.Form("ST")="Todas" THEN
estatus=""
END IF

Dim P  'variable para checar el estado de cobro de la factura
P=" AND factura.estcobranza='"&request.form("pago")&"'"
IF  request.Form("pago")="" OR request.Form("pago")="Todas" THEN
P=""
END IF

'if para validar que el fitro de pendientes y pagadas parcial
IF  request.Form("pago")="No pagada" THEN
P=" AND factura.estcobranza='Pendiente' OR factura.estcobranza='Pagada parcial'"
END IF

Dim vctes ' variable que nos servira para hacer el select hacia un cliente especifico
vctes="AND factura.idcliente="&request.form("CTES")
IF request.form("CTES")="TODOS" THEN
vctes=""
END IF

Dim vruta ' variable que se utilizara para hacer el select hacia una ruta especifica
'vruta=" AND factura.idruta="&request.form("Ruta")
IF request.form("Ruta")="" THEN
vruta=""
END IF

If Request.Form("ano") <> "" Then
  ano = " AND YEAR(factura.fechaAlta) = "& Request.Form("ano")
End If
Dim saldo
saldo=0


Dim montototal
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
Recordset1_cmd.CommandText = "SELECT factura.idcliente, factura.fechacfd, factura.idfactura, factura.folio, factura.idruta, factura.estatus, factura.total, clientes.idcliente, clientes.nombrecliente, clientes.rfcCliente, factura.retencion, factura.iva, documento.descripcion as documento FROM dbo.factura, dbo.clientesFacturacion as clientes, dbo.documento WHERE clientes.idcliente=factura.idcliente AND tipo_comprobante = iddocumento "&vruta &vctes &estatus &F &P &mes& documento& ano &" ORDER BY clientes.nombrecliente, fechacfd ASC"
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
%>
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
</style>
<p align="center">REPORTE DE RUTAS</p>

<table width="95%" border="0" align="center">
  <tr>
    <td colspan="2"><table width="98%" border="0" align="center">
      <tr class="stilo1">
        <td colspan="9" align="right"><a href="javascript:popup()">COBRO</a></td>
      </tr>
      <tr class="stilo1" bgcolor="#6DB6B6">
        <td>FOLIO</td>
        <td>RFC</td>
        <td>NOMBRE</td>
        <td>Documento</td>
        <td>FECHA</td>
        <td>MONTO</td>
        <td>PAGOS</td>
        <td>SALDO</td>
        <td>VER PAGOS</td>
      </tr>
      <% While ((Repeat1__numRows <> 0) AND (NOT Recordset1.EOF)) 

      '----------------------------------------------------------------------------------------calcular total de la factura'
        subtotal = 0
        totalImpuestosTrasladados = 0 
        total = 0
        totalImpuestosretenidos =0

        
        Set RecordsetDet_cmd = Server.CreateObject ("ADODB.Command")
        RecordsetDet_cmd.ActiveConnection = MM_Conecta1_STRING
        RecordsetDet_cmd.CommandText = "SELECT cantidad, precio_unitario, detFactura.descuento, ISNULL(detFactura.iva, 0) AS iva FROM dbo.detFactura WHERE id_factura = " & recordset1.fields.item("idfactura").value 
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
    
          retCte = Recordset1.Fields.item("retencion").Value
            'validar si la factura contiene iva'

          if Recordset1.fields.item("iva").value <> 0 then
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
'--------------------------------------------------------------------------------------------------------------------------------'




'Inicializar variables
pagos = 0
'////////////////////////////	  
	IF Recordset1.Fields.Item("folio").Value <>"" THEN   
	  
	  
	  
	   Set Recordset3_cmd = Server.CreateObject ("ADODB.Command") 
       Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
       Recordset3_cmd.CommandText = "SELECT SUM(total) AS pagos FROM dbo.logCXC WHERE  estatus<>'Cancelado' and folio="&Recordset1.Fields.Item("folio").Value 'hacemos la suma del total de pagos por factura con el folio de la factura en turno segun el ciclo while
       Recordset3_cmd.Prepared = true

       Set Recordset3 = Recordset3_cmd.Execute
       Recordset3_numRows = 0
       ' if para asegurar que venga algun valor en el campo pagos
	  if (recordset3.fields.item("pagos").value<>"") then 'si viene vacio NO entra en la suma
       pagosTotal=pagosTotal+(recordset3.fields.item("pagos").value) ' se suman los pagos que se han realizado para sacar los pagos totales
	  end if
	 END IF
	 
	   montototal=montototal+total
	   'if para valida que tiene pagos
	   if Recordset3.Fields.Item("pagos").Value <> "" then
	   pagos = Recordset3.Fields.Item("pagos").Value
	   pagos=pagos-cancelados
	   end if
	   
	   saldo = Round(total - pagos,2)
	   
    if saldo < 1 then
    
      saldo = 0
    end if
     		'if para validar el cambio del color
if color = cgrid2 then'color
color = cgrid1
else'color
color = cgrid2
end if'color
	   %>
   
      <tr class="stilo2" bgcolor="<%=color%>">
        <td height="23" align="center"><%=(Recordset1.Fields.Item("folio").Value)%></td>
        <td><%=(Recordset1.Fields.Item("rfcCliente").Value)%>&nbsp;</td>
        <td><%=(Recordset1.Fields.Item("nombreCliente").Value)%>&nbsp;</td>
        <td><%=(Recordset1.Fields.Item("documento").Value)%></td>
        <td align="center"><%=(Recordset1.Fields.Item("fechacfd").Value)%></td>
        <td align="right"><%=formatnumber(total, 2)%>&nbsp;</td>
        <td align="right"><%= pagos %>&nbsp;</td>
        <td align="right"><%=(saldo)%>&nbsp;</td>
        <td align="center"><a href="LogPagos.asp?folio=<%=recordset1.fields.item("folio").value%>&cliente=<%=recordset1.fields.item("nombreCliente").value%>&RFC=<%=recordset1.fields.item("rfcCliente").value%>"><img src="imagenes/money.png" width="16" height="16" border="0" /></a></td>
      </tr>
      <% 
	  
  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset1.MoveNext()
Response.Flush
Wend
%>
    </table></td>
  </tr>
  <tr>
    <td width="91%" align="right">MONTO TOTAL</td>
    <td width="9%" align="right"><%=formatnumber(montototal,2)%>&nbsp;</td>
  </tr>
  <tr>
    <td align="right">PAGOS TOTALES</td>
    <td align="right"><%=formatnumber(pagosTotal, 2)%>&nbsp;</td>
  </tr>
  <tr>
  <%saldoTotal=montototal-pagosTotal%>
    <td align="right">SALDO TOTAL</td>
    <td align="right"><%=formatnumber(saldoTotal, 2)%>&nbsp;</td>
  </tr>
</table>
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
<!--script que nos ayuda a abrir en otra ventana la caja para hacer un cobro-->
<SCRIPT language="javascript">
function popup()
{
 open('CajaPopUp.asp','NewWindow','top=0,left=0,width=450,height=500,status=yes,resizable=yes,scrollbars=yes'); <!--instruccion para abrir la pagina cajapopup por como una ventana emergente-->
	}
</SCRIPT>