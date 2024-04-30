<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="stylo2.asp"-->
<%
function number2word(byval numero)'convierte un número a palabras
Dim decimales

number2word=oneNumberToWord(Cdbl(Int(numero)))'lo pasamos a palabras

decimales = (numero - Cdbl(Int(numero))) * 100'obtenemos si tenia decimales
'If decimales <> 0 Then number2word = number2word & " con " & CStr(oneNumberToWord(Round(decimales))) 'si tiene decimales convertimos los decimales a palabras

end function

Function oneNumberToWord(pnumero)'un número simple sin decimales a palabras

Dim xcen(9) 'centenas
Dim xdec(9) 'decenas
Dim xuni(9) 'unidades
Dim xexc(6) 'except
Dim ceros(9)

Dim letras,i,c,j,xnumero, xnum,num,digito,numero_ent,entero,temp

xcen(2) = "dosc"
xcen(3) = "tresc"
xcen(4) = "cuatrosc"
xcen(5) = "quin"
xcen(6) = "seisc"
xcen(7) = "setec"
xcen(8) = "ochoc"
xcen(9) = "novec"
xdec(2) = "veinti"
xdec(3) = "trei"
xdec(4) = "cuare"
xdec(5) = "cincue"
xdec(6) = "sese"
xdec(7) = "sete"
xdec(8) = "oche"
xdec(9) = "nove"
xuni(1) = "uno"
xuni(2) = "dos"
xuni(3) = "tres"
xuni(4) = "cuatro"
xuni(5) = "cinco"
xuni(6) = "seis"
xuni(7) = "siete"
xuni(8) = "ocho"
xuni(9) = "nueve"
xexc(1) = "diez"
xexc(2) = "once"
xexc(3) = "doce"
xexc(4) = "trece"
xexc(5) = "catorce"
xexc(6) = "quince"
ceros(1) = "0"
ceros(2) = "00"
ceros(3) = "000"
ceros(4) = "0000"
ceros(5) = "00000"
ceros(6) = "000000"
ceros(7) = "0000000"
ceros(8) = "00000000"

c = 1
i = 1
j = 0
    
xnumero = cStr(pnumero)
   ' if pnumero = "" OR pnumero <> 0 then 
   ' pnumero = 0
   ' else
   'pnumero = pnumero
   ' end if
    'Response.Write(xnumero)
If Cdbl(LTrim(RTrim(pnumero))) < 999999999.99 Then
numero_ent = Cdbl(Int(pnumero))
If Len(numero_ent) < 9 Then
numero_ent = ceros(9 - Len(numero_ent)) & numero_ent
End If
    
entero = Cdbl(Int(numero_ent))
   ' Response.Write(entero)
Do While i < 8
temp = 0
num = Cdbl(Mid(numero_ent, i, 3))
xnum = Mid(numero_ent, i, 3)
digito = Cdbl(Mid(xnum, 1, 1))

'/* analizo el numero entero de a 3 */
If xnum = "000" Then
j = 0
Else
j = 1
If digito > 1 Then
letras = letras & xcen(digito) & "ientos "
End If
If Mid(xnum, 1, 1) = "1" And Mid(xnum, 2, 2) <> "00" Then
letras = letras & "ciento "
ElseIf Mid(xnum, 1, 1) = "1" Then
letras = letras & "cien "
End If

'/* analisis de las decenas */
digito = Cdbl(Mid(xnum, 2, 1))
If digito > 2 And Mid(xnum, 3, 1) = "0" Then
letras = letras & xdec(digito) & "nta "
temp = 1
End If

If digito > 2 And Mid(xnum, 3, 1) <> "0" Then
letras = letras & xdec(digito) & "nta y "

End If

If digito = 2 And Mid(xnum, 3, 1) = "0" Then
letras = letras & "veinte "
temp = 1
ElseIf digito = 2 And Mid(xnum, 3, 1) <> "0" Then
letras = letras & "veinti"

End If

If digito = 1 And Mid(xnum, 3, 1) >= "6" Then
letras = letras & "dieci"
ElseIf digito = 1 And Mid(xnum, 3, 1) < "6" Then
letras = letras & xexc(Cdbl(Mid(xnum, 3, 1) + 1))
temp = 1
End If
End If

if temp = 0 then
'/* analisis del ultimo digito */
digito = Cdbl(Mid(xnum, 3, 1))
If ((c = 1) Or (c = 2)) And xnum = "001" Then
'letras = letras & "un"
Else
If ((c = 1) Or (c = 2)) And xnum >= "020" And Mid(xnum, 3, 1) = "1" Then
letras = letras & "un"
Else
If digito <> 0 Then
letras = letras & xuni(digito)
End If
End If
End If
end if

If j = 1 And i = 1 And xnum = "001" And c = 1 Then
letras = letras & " millon "
ElseIf j = 1 And i = 1 And xnum <> "001" And c = 1 Then
letras = letras & " millones "
ElseIf j = 1 And i = 4 And c = 2 Then
letras = letras & " mil "
End If
i = i + 3
c = c + 1
Loop
If letras = "" Then
letras = "NINGUNA "
End If

End If

oneNumberToWord = letras
End Function
%>
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
'Recordset1_cmd.CommandText = "SELECT idfactura, iva, UUID, subtotal, total, retencion, numctapago, tipo_comprobante, condiciones_pago, serie, forma_pago, folio, numero_certificado, no_aprobacion, ano_aprobacion, fechacfd, ASN, nfactura, embarque, vendedor, ordenCompra, CONVERT (CHAR (12), fEmbarque, 107) as fEmbarque, VA, FOB, terminos, obsCliente, cadena_original, sello, metodoPago, moneda, fechaAlta, estatus FROM dbo.factura WHERE idfactura = ?" 
Recordset1_cmd.CommandText = "SELECT * FROM dbo.solicitudes WHERE id = ?" 
'response.Write(Recordset1_cmd.CommandText)
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Request.QueryString("idfactura") <> "") Then 
  Recordset2__MMColParam = Request.QueryString("idfactura")
End If
%>
<%
ingPed = ""
'Set RecordsetPed_cmd = Server.CreateObject ("ADODB.Command")
'RecordsetPed_cmd.ActiveConnection = MM_Conecta1_STRING
'RecordsetPed_cmd.CommandText = "SELECT * FROM dbo.detallesPed WHERE nventa = '"&Recordset1.fields.item("ASN").value&"'"   
'RecordsetPed_cmd.Prepared = true
'response.write(RecordsetPed_cmd.CommandText)
'Set RecordsetPed = RecordsetPed_cmd.Execute
'RecordsetPed_numRows = 0

'if not RecordsetPed.eof then
   ' ingPed = "1"
   ' qry = "SELECT det.cantidad, d.nparte, d.claveProdServ, d.claveUnidad,  d.unidad,  d.id_detFactura, d.precio_unitario,   d.total, d.descuento,  d.impuesto,  d.id_factura,  d.iva, d.no_identificacion, d.aduana_numero, d.aduana_fecha, d.aduana, d.predial_no, d.retencion, d.tretencion, d.valorAgregado,  det.idpedimento, d.descripcion, d.impuestoP FROM dbo.detFactura d INNER JOIN claves c ON d.nparte = c.clave INNER JOIN detallesPed det ON c.idarticulo = det.idarticulo WHERE d.id_factura = " & Recordset2__MMColParam & " And det.nventa = '" & Recordset1.fields.item("ASN").value & "'"
'else
    'qry = "SELECT cantidad, d.nparte, d.claveProdServ,  d.claveUnidad,   d.id_detFactura,  d.unidad,  d.precio_unitario, d.descripcion, d.descuento,  d.impuestoP,  iva  FROM dbo.detFactura d WHERE id_factura = " & Recordset2__MMColParam
    qry = "SELECT d.id, cantidad, subtotal, d.iva, total, precioF AS precio_unitario, p.codigoSAT AS claveProdServ, p.descripcion, P.codigo AS nparte, U.unidad, U.unidadSAT AS claveUnidad FROM detallesSolicitud AS d JOIN productos AS p ON p.id = d.idProducto JOIN unidadesDeMedida AS U ON U.id = P.idUDM WHERE idSolicitud = " & Recordset2__MMColParam
'end if
  'Response.Write(qry)
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows
   'response.Write(qry)
Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = qry 
Recordset2_cmd.Prepared = true
   
'Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
         
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Request.Cookies("login")("idSucursal") <> "") Then 
  Recordset3__MMColParam = Request.Cookies("login")("idSucursal")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id = ?" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = (Recordset3.Fields.Item("idCiudad").Value)
If (Request("MM_EmptyValue") <> "") Then 
  Recordset4__MMColParam = Request("MM_EmptyValue")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.ciudades WHERE id = ?" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<%
Dim Recordset5__MMColParam
Recordset5__MMColParam = (Recordset3.Fields.Item("idEstado").Value)
If (Request("MM_EmptyValue") <> "") Then 
  Recordset5__MMColParam = Request("MM_EmptyValue")
End If
%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT * FROM dbo.estados WHERE id = ?" 
Recordset5_cmd.Prepared = true
Recordset5_cmd.Parameters.Append Recordset5_cmd.CreateParameter("param1", 5, 1, -1, Recordset5__MMColParam) ' adDouble

Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%
'Dim Recordset6__MMColParam
'Recordset6__MMColParam = (Recordset3.Fields.Item("pais").Value)
'If (Request("MM_EmptyValue") <> "") Then 
'  Recordset6__MMColParam = Request("MM_EmptyValue")
'End If
%>
<%
'Dim Recordset6
'Dim Recordset6_cmd
'Dim Recordset6_numRows

'Set Recordset6_cmd = Server.CreateObject ("ADODB.Command")
'Recordset6_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset6_cmd.CommandText = "SELECT * FROM dbo.pais WHERE idpais = ?" 
'Recordset6_cmd.Prepared = true
'Recordset6_cmd.Parameters.Append Recordset6_cmd.CreateParameter("param1", 5, 1, -1, Recordset6__MMColParam) ' adDouble

'Set Recordset6 = Recordset6_cmd.Execute
'Recordset6_numRows = 0
%>
<%
'Dim Recordset7__MMColParam
'Recordset7__MMColParam = (Recordset1.Fields.Item("tipo_comprobante").Value)
'If (Request("MM_EmptyValue") <> "") Then 
'  Recordset7__MMColParam = Request("MM_EmptyValue")
'End If
%>
<%
'Dim Recordset7
'Dim Recordset7_cmd
'Dim Recordset7_numRows

'Set Recordset7_cmd = Server.CreateObject ("ADODB.Command")
'Recordset7_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset7_cmd.CommandText = "SELECT * FROM dbo.documento WHERE iddocumento = ?" 
'Recordset7_cmd.Prepared = true
'Recordset7_cmd.Parameters.Append Recordset7_cmd.CreateParameter("param1", 5, 1, -1, Recordset7__MMColParam) ' adDouble

'Set Recordset7 = Recordset7_cmd.Execute
'Recordset7_numRows = 0
%>
<%
Dim Recordset8__MMColParam
Recordset8__MMColParam = "1"
If (Request.QueryString("idCliente") <> "") Then 
  Recordset8__MMColParam = Request.QueryString("idCliente")
End If
%>
<%
Dim Recordset8
Dim Recordset8_cmd
Dim Recordset8_numRows

Set Recordset8_cmd = Server.CreateObject ("ADODB.Command")
Recordset8_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset8_cmd.CommandText = "SELECT * FROM dbo.clientes WHERE idCliente = ?" 
'Recordset8_cmd.CommandText = "SELECT * FROM clientesFacturacion c JOIN estados e ON e.id = c.estadoCliente JOIN ciudades cd ON cd.id = c.ciudadCliente WHERE idCliente = ?" 
Recordset8_cmd.CommandText = "SELECT * FROM clientes c JOIN estados e ON e.id = c.idEstado JOIN ciudades cd ON cd.id = c.idCiudad WHERE c.id = ?" 
Recordset8_cmd.Prepared = true
Recordset8_cmd.Parameters.Append Recordset8_cmd.CreateParameter("param1", 5, 1, -1, Recordset8__MMColParam) ' adDouble

Set Recordset8 = Recordset8_cmd.Execute
Recordset8_numRows = 0

'Dim Recordset8A
'Dim Recordset8A_cmd
'Dim Recordset8A_numRows

'Set Recordset8A_cmd = Server.CreateObject ("ADODB.Command")
'Recordset8A_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset8A_cmd.CommandText = "SELECT CONCAT(claveSat, ' - ', descripcion) As claveSat FROM usoCfdi WHERE claveSat = '"&Recordset8.fields.item("usoCfdi").value&"'" 
'Recordset8A_cmd.Prepared = true

'Set Recordset8A = Recordset8A_cmd.Execute
'Recordset8A_numRows = 0

Set RecordsetF_cmd = Server.CreateObject ("ADODB.Command")
RecordsetF_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetF_cmd.CommandText = "SELECT * FROM dbo.formasPago WHERE id = " &  Recordset1.fields.item("idFormaPago").value
RecordsetF_cmd.Prepared = true

Set RecordsetF = RecordsetF_cmd.Execute
RecordsetF_numRows = 0

    if not recordsetF.eof then
        descF = RecordsetF.fields.item("descripcion").value
        formP = RecordsetF.fields.item("nombre").value
    end if

'Set RecordsetMet_cmd = Server.CreateObject ("ADODB.Command")
'RecordsetMet_cmd.ActiveConnection = MM_Conecta1_STRING
'RecordsetMet_cmd.CommandText = "SELECT * FROM dbo.formaPagoSat WHERE codigo = '"&Recordset1.fields.item("metodoPago").value&"'"   
'RecordsetMet_cmd.Prepared = true

'Set RecordsetMet = RecordsetMet_cmd.Execute
'RecordsetMet_numRows = 0

'if NOT RecordsetMet.eof then
'    codigoM = RecordsetMet.fields.item("codigo").value & " - " & RecordsetMet.fields.item("descripcion").value 
'end if 
codigoM = ""

'Set RecordsetMoneda_cmd = Server.CreateObject ("ADODB.Command")
'RecordsetMoneda_cmd.ActiveConnection = MM_Conecta1_STRING
'RecordsetMoneda_cmd.CommandText = "SELECT * FROM dbo.moneda WHERE idmd = '"&Recordset1.fields.item("moneda").value&"'"   
'RecordsetMoneda_cmd.Prepared = true

'Set RecordsetMoneda = RecordsetMoneda_cmd.Execute
'RecordsetMoneda_numRows = 0

'if NOT RecordsetMoneda.eof then
'    moneda = RecordsetMoneda.fields.item("cmoneda").value 
'end if 

moneda = "MXN"
%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset2_numRows = Recordset2_numRows + Repeat1__numRows

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<style type="text/css">
    <!--
    .stylea {
        font-size: 12px;
    }
    -->
</style>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=titlePage%></title>
</head>

<body>
    <table width="680" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td colspan="2">
                <table width="100%" border="0">
                    <tr align="center" valign="top">
                        <td width="10%" align="center">
                            <br />
                            <img src="<%=(Recordset3.Fields.Item("Ncarpeta").Value)%>/<%=(Recordset3.Fields.Item("logo").Value)%>" width="120" height="65" /></td>
                        <td width="90%">
                            <table width="100%" border="0" align="center">
                                <tr>
                                    <td align="center" style="font-size:18px"><strong><%=(Recordset3.Fields.Item("razon").Value)%></strong></td>
                                </tr>
                                <%
                                    if Recordset3.Fields.Item("c_RegimenFiscal").Value = "601" then 
                                    regimen = "General de Ley Personas Morales"
                                    end if
                                    direccion = Recordset3.Fields.Item("calle").Value  & " #"&Recordset3.Fields.Item("numero").Value& " " & Recordset3.Fields.Item("colonia").Value
                                    direccion2 = Recordset4.Fields.Item("ciudad").Value &" "&Recordset5.Fields.Item("estado").Value& " C.P. "&Recordset3.Fields.Item("codigoPostal").Value & " México"
                                    direccion3 = "Régimen Fiscal: "&regimen
                                    direccion4 = "R.F.C.: " & Recordset3.Fields.Item("rfc").Value & "      Registro Patronal: " &Recordset3.Fields.Item("registroPatronal").Value
                                %>
                                <tr>
                                    <td align="center"><%=(direccion)%></td>                                    
                                </tr>
                                <tr>
                                    <td align="center"><%=(direccion2)%></td>
                                </tr>
                                <tr>
                                    <td align="center"><strong><%=(direccion3)%></strong></td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;<strong><%=(direccion4)%></strong></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="background-color:#e33045; color:white; font-size:20px; border:0px; border-color: #e33045;">
            <td width="340" style="border:0px">FACTURA</td>
            <% folio = Recordset1.Fields.Item("folio").Value
                if folio > 0 AND folio < 10 then
                folio = "00"&folio
                elseif folio > 9 AND folio < 100 then 
                folio = "0"&folio
                else
                folio = folio
                end if
            %>
            <td width="340" style="text-align:right; border:0px">FOLIO:&nbsp; </td>
        </tr>
        </table>
    <table width="680" border="1" cellpadding="0" cellspacing="0" style="border-color:#e33045">
        <tr>
            <td width="340">
                <table width="100%" border="0" style="font-size:12px">
                    <tr>
                        <td colspan="3"><strong>VENDIDO A:</strong></td>
                    </tr>
                    <tr>
                        <td colspan="3"><strong><%=(Recordset8.Fields.Item("nombre").Value)%></strong></td>
                    </tr>
                    <tr>
                        <td colspan="3"><%=(Recordset8.Fields.Item("calle").Value)%> #<%=(Recordset8.Fields.Item("numero").Value)%></td>
                    </tr>
                    <tr>
                        <td colspan="2"><%=(Recordset8.Fields.Item("colonia").Value)%></td>
                        
                    </tr>
                    <tr>
                        <td colspan="2"><%=(Recordset8.Fields.Item("ciudad").Value) %></td>
                        <td ><strong>Estado: </strong> <%=(Recordset8.Fields.Item("estado").Value) %></td>
                    </tr>
                    <tr>
                       <td><strong>R.F.C.: </strong></td>
                        <td><strong>C.P.: </strong><%=(Recordset8.Fields.Item("cp").Value) %></td>
                        <td><strong>País: </strong>México</td>
                    </tr>
                    <tr>
                        <td colspan="3"><strong>USOCFDI: </strong></td>
                    </tr>
                  
                   
                    
                </table>
            </td>
            <td width="340">
                <table width="100%" border="0" style="font-size:12px">
                    <tr>
                        <td><strong>FECHA</strong></td>
                    </tr>
                    <tr>
                        <td>
                            <%=(Recordset1.Fields.Item("fecha").Value)%>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>FECHA DE VENTA</strong></td>
                    </tr>
                    <tr>
                        <td> <%=(Recordset1.Fields.Item("fechaAlta").Value)%></td>
                    </tr>
                     <tr>
                        <td><strong>CONDICIONES DE VENTAS</strong></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>METODO DE PAGO</strong></td>
                    </tr>
                    <tr>
                        <td><%=formP %></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="680" colspan="2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-size:13px">
                    <tr align="center">
                        <td><strong>FORMA PAGO:</strong> <%=codigoM %></td>
                        <td><strong>No. DE CUENTA DE PAGO:</strong> </td>
                        <td><strong>CONDICIONES DE PAGO:</strong> <%=descF %></td>
                        <td><strong>ORDEN DE COMPRA:</strong></td>
                    </tr>
                </table>
            </td>
        </tr>
<!--        </table>
    <table width="680" border="0" cellpadding="0" cellspacing="0"">-->
        <tr>
            <td colspan="2">
                <table width="100%" border="1" align="center" class="stylea" cellpadding="0" cellspacing="0">
                    <tr align="center" style="background-color:rgb(34,46,112); color:white">
                        <td><strong>CANT</strong></td>
                        <td><strong>CODIGO/SAT</strong></td>
                        <td><strong>DESCRIPCION</strong></td>
                        <td><strong>UNIDAD/SAT</strong></td>
                        <td><strong>PEDIMENTO</strong></td>
                        <td><strong>P.UNITARIO / DESC. / P. DESC. / IMP </strong></td>
                        <td><strong>P. TOTAL</strong></td>
                    </tr>
                    <% iddetalles = ""
                         colorB = "rgb(236,236,236)"
While ((Repeat1__numRows <> 0) AND (NOT Recordset2.EOF)) 
        'REVISAR EL PEDIMENTO
        iddetalles = iddetalles & " " & recordset2.Fields.item("id").Value
       

        'calcular el importe 
         descripcion = Recordset2.Fields.Item("descripcion").Value
     
          Dim cantidad 
            Dim nparte 
            Dim unidad 
            Dim des 
            Dim precio_unitario

            'valdiar si todos los productos estan 
            
            'if para validar que tiene cantidad
            If recordset2.Fields.item("cantidad").Value <> 0 Then 'cantidad
                cantidad = recordset2.Fields.item("cantidad").Value
            End If 'cantidad

            'if para validar que tiene numero de partes
		    nparte=Recordset2.Fields.Item("nparte").Value
	        
            'if para validar que tiene unidad
                unidad = Recordset2.Fields.item("unidad").Value
            
            'if para validar que tiene precio_unitario
            If Recordset2.Fields.item("precio_Unitario").Value <> 0 Then 'precio_unitario
                precioUnitario = (Recordset2.Fields.item("precio_Unitario").Value)
            End If 'precio_unitario

            'if para validar si tiene descuento
            'If Recordset2.Fields("descuento").Value <> 0 Then
            '    des = (Recordset2.Fields("descuento").Value)
            'Else
                des = 0
            'End If 
          
          'if para validar si tiene descuento
           ' If Recordset2.Fields("impuestoP").Value <> 0 AND Recordset2.Fields("impuestoP").Value <> "" Then
           '     impt = (Recordset2.Fields("impuestoP").Value)
           ' Else
                impt = 0
            'End If

          precio_Unitario = precioUnitario
    'para el precio Unitario
            'If (Recordset8.Fields.Item("truncar").Value) = "si" Then

            '    If impt > 0 Then
             '       precio_Unitario = truncarAseis(precio_Unitario * ((100 + (impt)) / 100))
             '   Else
            '        precio_Unitario = truncarAseis(precio_Unitario)
            '    End If
          
            '    If des > 0 Then
            '        precio_Unitario = truncarAseis(precio_Unitario * ((100 - (des)) / 100))
            '    Else
            '        precio_Unitario = truncarAseis(precio_Unitario)
           '     End If

            '    totalConcepto = truncarAdos(precio_Unitario * cantidad)
            'Else
                If impt > 0 Then
                    precio_Unitario = truncarAseis(precio_Unitario * ((100 + (impt)) / 100))
                Else
                    precio_Unitario = truncarAseis(precio_Unitario)
                End If
          
                If des > 0 Then
                    precio_unitario = truncarAseis(precio_Unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = truncarAseis(precio_Unitario)
                End If

                totalConcepto = redondear(precio_unitario * cantidad)
            'End If
                       

                        if colorB = "rgb(255,255,255)" Then
                            colorB = "rgb(236,236,236)"
                        else
                            colorB = "rgb(255,255,255)"
                        end if

                    %>
                    <tr align="center" style="background-color: <%= colorB%>">
                        <td><%=(cantidad)%></td>
                        <td><%=nparte %>/<%=Recordset2.Fields("claveprodserv").Value %></td>
                        <td><%=(descripcion)%>&nbsp;<%=(nparte)%></td>
                        <td><%=(unidad)%>/<%=Recordset2.Fields("claveUnidad").Value %></td>
                        <td><%=ped %></td>
                        <td>$<%=FormatNumber(Recordset2.Fields.item("precio_Unitario").Value) %>&nbsp;<%=des %> $<%=FormatNumber(precioUnitario)%>&nbsp; <%=impt %></td>
                        <td>$<%=FormatNumber(totalConcepto) %></td>
                    </tr>
                    <% 

  'total '
'si el cliente trae truncar, hacer las primero las operaciones del subtotal e iva
        Dim precioUnitario
        Dim totalConcepto
        Dim subTotal
        Dim impuesto
        Dim retencion
        
        Dim retCte 
	      retCte = 0
        iva = Recordset2.Fields.Item("iva").Value
        

        Dim totalImpuestosTrasladados
        Dim totalImpuestosretenidos
        'If Recordset8.Fields.item("truncar").Value = "si" Then
         '       If impt > 0 Then
         '           precioUnitario = truncarAseis(precioUnitario * ((100 + (impt)) / 100))
         '       Else
         '           precioUnitario = truncarAseis(precioUnitario)
         '       End If
          

         '   If des > 0 Then
         '       precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
         '   Else
         '       precioUnitario = truncarAseis(precioUnitario)
         '   End If
          '  totalConcepto = truncarAdos(precioUnitario * cantidad)
         '   subTotal = subtotal + truncarAdos(totalConcepto)
                'validamos si la factura tiene iva
         '   If iva <> 0 Then
         '       impuesto = truncarAdos(totalConcepto * (iva / 100))
          '      totalImpuestosTrasladados = totalImpuestosTrasladados + truncarAdos(impuesto)
         '   End If

          '  subTotal = truncarAdos(subTotal)
        'Else
            If impt > 0 Then
                precioUnitario = truncarAseis(precioUnitario * ((100 + (impt)) / 100))
            Else
                precioUnitario = truncarAseis(precioUnitario)
            End If
          
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
                    impuesto = redondear(totalConcepto * (iva))

                    totalImpuestosTrasladados = totalImpuestosTrasladados +  redondear(impuesto)
                End If
                If retCte > 0 Then

                    'Console.Write(totalConcepto)
                    retencion = redondear(totalConcepto * (0.16))

                    totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
                Else
                    totalImpuestosretenidos = 0
                End If
                  subTotal = redondear(subTotal)
                 
     ' end if

'limpiamos las variables de los conceptos
        impuesto = 0
        totalConcepto = 0
        precioUnitario = 0

        Dim total
                        total =0
        
        Dim isr
        'obtenemos el total
        If truncar = "si" Then
            total = truncarAdos(totalImpuestosTrasladados + subTotal)
            'totalImpuestosTrasladados = 0
        Else
            total = redondear(totalImpuestosTrasladados + subTotal)
            '  totalImpuestosTrasladados = 0
        End If

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

  Repeat1__index=Repeat1__index+1
  Repeat1__numRows=Repeat1__numRows-1
  Recordset2.MoveNext()

Wend
                      'Response.Write("f "& Recordset2_cmd.CommandText)
                       ' detalles = replace(trim(iddetalles)," ",", ")
                       if iddetalles = "" then 
                        detalles= "0"
                        else
                        detalles = replace(trim(iddetalles)," ",", ")
                        end if
                        
'buscar kos productos que no estan
'response.Write(detalles)
 Set RecordsetDetF_cmd = Server.CreateObject ("ADODB.Command")
 RecordsetDetF_cmd.ActiveConnection = MM_Conecta1_STRING
 RecordsetDetF_cmd.CommandText = "SELECT cantidad, d.nparte, d.claveProdServ, d.claveUnidad, d.id_detFactura,  d.unidad,  d.precio_unitario, d.descripcion, d.descuento,  d.impuestoP,  iva FROM detFactura d WHERE id_factura ="&Recordset2__MMColParam&" AND id_detFactura NOT IN ("&detalles&")"
RecordsetDetF_cmd.Prepared = true
  'Response.Write(detalles)
 Set RecordsetDetF = RecordsetDetF_cmd.Execute
 RecordsetDetF_numRows = 0

While ((NOT RecordsetDetF.EOF)) 

                       'response.Write(RecordsetDetF_cmd)
                        if colorB = "rgb(255,255,255)" Then
                            colorB = "rgb(236,236,236)"
                        else
                            colorB = "rgb(255,255,255)"
                        end if


 'if para validar que tiene cantidad
            If RecordsetDetF.Fields.item("cantidad").Value <> 0 Then 'cantidad
                cantidad = RecordsetDetF.Fields.item("cantidad").Value
            End If 'cantidad

            'if para validar que tiene numero de partes
		    nparte=RecordsetDetF.Fields.Item("nparte").Value
	        descripcion = RecordsetDetF.Fields.Item("descripcion").Value
            
            'if para validar que tiene unidad
                unidad = RecordsetDetF.Fields.item("unidad").Value
            
            'if para validar que tiene precio_unitario
            If RecordsetDetF.Fields.item("precio_Unitario").Value <> 0 Then 'precio_unitario
                precioUnitario = (RecordsetDetF.Fields.item("precio_Unitario").Value)
            End If 'precio_unitario

            'if para validar si tiene descuento
            If RecordsetDetF.Fields("descuento").Value <> 0 Then
                des = (RecordsetDetF.Fields("descuento").Value)
            Else
                des = 0
            End If 
          
          'if para validar si tiene descuento
            If RecordsetDetF.Fields("impuestoP").Value <> 0 AND RecordsetDetF.Fields("impuestoP").Value <> "" Then
                impt = (RecordsetDetF.Fields("impuestoP").Value)
            Else
                impt = 0
            End If

          precio_Unitario = precioUnitario
    'para el precio Unitario
            If (Recordset8.Fields.Item("truncar").Value) = "si" Then

                If impt > 0 Then
                    precio_Unitario = truncarAseis(precio_Unitario * ((100 + (impt)) / 100))
                Else
                    precio_Unitario = truncarAseis(precio_Unitario)
                End If
          
                If des > 0 Then
                    precio_Unitario = truncarAseis(precio_Unitario * ((100 - (des)) / 100))
                Else
                    precio_Unitario = truncarAseis(precio_Unitario)
                End If

                totalConcepto = truncarAdos(precio_Unitario * cantidad)
            Else
                If impt > 0 Then
                    precio_Unitario = truncarAseis(precio_Unitario * ((100 + (impt)) / 100))
                Else
                    precio_Unitario = truncarAseis(precio_Unitario)
                End If
          
                If des > 0 Then
                    precio_unitario = truncarAseis(precio_Unitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = truncarAseis(precio_Unitario)
                End If

                totalConcepto = redondear(precio_unitario * cantidad)
            End If

                    %>
                    <tr align="center" style="background-color: <%= colorB%>">
                        <td><%=(cantidad)%></td>
                        <td><%=nparte %>/<%=RecordsetDetF.Fields("claveprodserv").Value %></td>
                        <td><%=(descripcion)%>&nbsp;<%=(nparte)%></td>
                        <td><%=(unidad)%>/<%=RecordsetDetF.Fields("claveUnidad").Value %></td>
                        <td>&nbsp;</td>
                        <td>$<%=FormatNumber(RecordsetDetF.Fields.item("precio_Unitario").Value) %>&nbsp;<%=des %> $<%=FormatNumber(precioUnitario)%>&nbsp; <%=impt %></td>
                        <td>$<%=FormatNumber(totalConcepto) %></td>
                    </tr>
                    <%



        If Recordset8.Fields.item("truncar").Value = "si" Then
                If impt > 0 Then
                    precioUnitario = truncarAseis(precioUnitario * ((100 + (impt)) / 100))
                Else
                    precioUnitario = truncarAseis(precioUnitario)
                End If
          

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
            If impt > 0 Then
                precioUnitario = truncarAseis(precioUnitario * ((100 + (impt)) / 100))
            Else
                precioUnitario = truncarAseis(precioUnitario)
            End If
          
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
                End If
                If retCte > 0 Then

                    'Console.Write(totalConcepto)
                    retencion = redondear(totalConcepto * (0.16))

                    totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
                Else
                    totalImpuestosretenidos = 0
                End If
                  subTotal = redondear(subTotal)
                 
      end if

'limpiamos las variables de los conceptos
        impuesto = 0
        totalConcepto = 0
        precioUnitario = 0


        'obtenemos el total
        If truncar = "si" Then
            total = truncarAdos(totalImpuestosTrasladados + subTotal)
            'totalImpuestosTrasladados = 0
        Else
            total = redondear(totalImpuestosTrasladados + subTotal)
            '  totalImpuestosTrasladados = 0
        End If

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




RecordsetDetF.MoveNext()

Wend
    '  Console.Write(hayretencion)
        If retCte > 0 Then

            total = subTotal
        Else

            total = (total)
        End If

 'validar si se tiene anticipos 
 Set RecordsetAnt_cmd = Server.CreateObject ("ADODB.Command")
 RecordsetAnt_cmd.ActiveConnection = MM_Conecta1_STRING
 RecordsetAnt_cmd.CommandText = "SELECT * FROM anticipo WHERE idFacturaRelacionada ="&Recordset2__MMColParam
 RecordsetAnt_cmd.Prepared = true
    
 Set RecordsetAnt = RecordsetAnt_cmd.Execute
 RecordsetAnt_numRows = 0

anticipo = 0
if not RecordsetAnt.EOF then
    anticipo = RecordsetAnt.fields.item("montoAnt").value
    total = total - anticipo
end if

                        
                    %>
                
                </table>
            </td>
        </tr>

        </table>
    <br />
    <table width="680" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="2">
                <table width="100%" border="1" cellpadding="0" cellspacing="0" style="border-color:#e33045">
                    <tr align="center" style="background-color:#e33045; color:white">
                        <td><strong>OBSERVACIONES</strong></td>
                    </tr>
                    <tr>
                        <% 'if Recordset1.Fields.Item("obsCliente").Value = "" then
                            param = "<br/>"
                            'else
                           ' param = Recordset1.Fields.Item("obsCliente").Value 
                            'end if%>
                        <td><%=(param)%></td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <table width="100%" border="0" align="center">
                    <tr valign="top">
                        <td> <!--ESTE DOCUMENTO NO ES IMPRESIÓN DE UN COMPROBANTE FISCAL-->
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" style="border-color:#e33045">
                                <tr align="center" style="background-color:#e33045; color:white;">
                                    <td><strong>IMPORTE CON LETRA</strong></td>
                                </tr>
                                <tr>
                                    <td style="font-size:15px"><strong><%=UCase(number2word((Total)))%>&nbsp;<%=moneda %>&nbsp;<%=Mid(Total,InStr(Total,".")+1,2)&"/100"%></strong><br /></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" style="border-color:#e33045">
                                <tr>
                                    <td style="border-bottom: 1px solid #ccc; background-color:#e33045; color:white; text-align:center; border:0px">SUB-TOTAL: &nbsp;&nbsp; $</td>
                                    <td style="border-bottom: 1px solid #ccc; border-color:#e33045" align="right"><%
                                        if subtotal > 0 then
                                        op = FormatNumber(subtotal)
                                        else
                                        op = "0.00"
                                        end if
                                    %><%=op %></td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ccc; background-color:#e33045; color:white; text-align:center; border:0px">IVA: &nbsp;&nbsp; $</td>
                                    <td style="border-bottom: 1px solid #ccc; border-color:#e33045" align="right">
                                        <% if totalImpuestosTrasladados > 0 then
                                            op1 = FormatNumber(totalImpuestosTrasladados)
                                        else
                                            op1 = "0.00"
                                        end if %>

                                        <%=(op1)%></td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #ccc; background-color:#e33045; color:white; text-align:center; border:0px">Retencion IVA: &nbsp;&nbsp; $ </td>
                                    <td style="border-bottom: 1px solid #ccc; border-color:#e33045" align="right">
                                        <% if totalImpuestosretenidos > 0 then
                                            op2 = FormatNumber(totalImpuestosretenidos)
                                        else
                                            op2 = "0.00"
                                        end if %>
                                        <%=(op2)%></td>
                                </tr>
                                <% if anticipo > 0 then %>
                                <tr>
                                    <td style="border-bottom: 1px solid #ccc; background-color:#e33045; color:white; text-align:center; border:0px">Anticipo: &nbsp;&nbsp; $</td>
                                    <td style="border-bottom: 1px solid #ccc; border-color:#e33045" align="right">
                                        <% if anticipo > 0 then
                                            op4 = FormatNumber(anticipo)
                                        else
                                            op4 = "0.00"
                                        end if %>
                                        <%=(op4)%></td>
                                </tr>
                                <%end if %>
                                <tr>
                                    <td style="border-bottom: 1px solid #ccc; background-color:#e33045; color:white; text-align:center; border:0px">TOTAL: &nbsp;&nbsp; $</td>
                                    <td style="border-bottom: 1px solid #ccc; border-color:#e33045" align="right">
                                        <% if Total > 0 then
                                            op3 = FormatNumber(Total)
                                        else
                                            op3 = "0.00"
                                        end if %>
                                        <%=(op3)%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">ESTE DOCUMENTO NO ES IMPRESIÓN DE UN COMPROBANTE FISCAL</td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
        <tr>
            <td colspan="2">UIDD: <span style="border-bottom: 1px solid #ccc;"></span></td>
        </tr>
    </table>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
<%
Recordset2.Close()
Set Recordset2 = Nothing
%>
<%
'Recordset7.Close()
'Set Recordset7 = Nothing
%>
<%
Recordset8.Close()
Set Recordset8 = Nothing
%>
<%
'Recordset8A.Close()
'Set Recordset8A = Nothing
%>

<%
Recordset5.Close()
Set Recordset5 = Nothing
%>
<%
Recordset4.Close()
Set Recordset4 = Nothing
%>
<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
