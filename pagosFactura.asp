<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows
Dim numero, xml

Set file = Server.CreateObject("Scripting.FileSystemObject")
numero = 0
totalAux = 0
xml = ""
saldoPAux = 0
Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT p.idcc, p.fechaAlta, p.estatus, p.nombreArchivo, p.nombrePDF, d.impPagado, p.idfactura, d.noParcialidad, p.timbre, p.estatusCorreo, p.moneda, p.tipoCambioDR, d.moneda as monedaP FROM pagosFactura p INNER JOIN detallesPagoFacturas d ON p.idcc = d.idcc WHERE d.idfactura ="&Request.QueryString("idfactura")&" AND (p.estatus <> 'Cancelado' OR timbre ='NO') ORDER BY idcc DESC"
Recordset1_cmd.Prepared = true
'response.Write(Recordset1_cmd.CommandText)
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

if not recordset1.eof then
    
        Set RecordsetSuma_cmd = Server.CreateObject ("ADODB.Command")
	    RecordsetSuma_cmd.ActiveConnection = MM_Conecta1_STRING
	    RecordsetSuma_cmd.CommandText = "SELECT isnull(sum(d. impPagado),0) AS suma FROM detallesPagoFacturas d INNER JOIN pagosFactura p ON d.idcc = p.idcc WHERE d.idfactura = "&Request.QueryString("idfactura")&" AND p.timbre = 'NO'"
	    RecordsetSuma_cmd.Prepared = true
	  '  response.Write(RecordsetSuma_cmd.CommandText)
	    Set RecordsetSuma = RecordsetSuma_cmd.Execute
	    RecordsetSuma_numRows = 0

        If RecordsetSuma.Fields.Item("suma").Value <> "" And Not IsNull(RecordsetSuma.Fields.Item("suma").Value) Then
	      saldo1 = RecordsetSuma.Fields.Item("suma").Value
	    Else
	      saldo1 = 0
	    End If    
    end if    

    Set RecordsetTotal_cmd = Server.CreateObject ("ADODB.Command")
	    RecordsetTotal_cmd.ActiveConnection = MM_Conecta1_STRING
	    RecordsetTotal_cmd.CommandText = "SELECT cadena_original FROM factura WHERE idfactura = "&Request.QueryString("idfactura")
	    RecordsetTotal_cmd.Prepared = true
	   ' response.Write(RecordsetTotal.CommandText)
	    Set RecordsetTotal = RecordsetTotal_cmd.Execute
	    RecordsetTotal_numRows = 0
   
        if not RecordsetTotal.eof then
            totalFact = split(RecordsetTotal.fields.item("cadena_original").value, "|")        
             if Instr(RecordsetTotal.fields.item("cadena_original").value, "||3.3|M") then
                tt1 = totalFact(12)    
            else
                tt1 = totalFact(11)
            end if              
        else
            tt1 = saldo1
        end if
   'response.write(tt1 +" - "+saldo1)
     '   totalRestante = (tt1 - saldo1)   
   ' RESPONSE.Write(totalRestante)

    'CONSULTAR LA CARPETA de pagos 
    Set RecordsetCarpeta_cmd = Server.CreateObject ("ADODB.Command")
RecordsetCarpeta_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetCarpeta_cmd.CommandText = "SELECT e.carpetaPagos FROM sucursales e INNER JOIN factura f ON e.id = f.idempresa WHERE f.idfactura = "&Request.QueryString("idfactura")
RecordsetCarpeta_cmd.Prepared = true

'response.Write(RecordsetCarpeta_cmd.CommandText)
Set RecordsetCarpeta = RecordsetCarpeta_cmd.Execute
RecordsetCarpeta_numRows = 0

Set RecordsetCarpetaA_cmd = Server.CreateObject ("ADODB.Command")
RecordsetCarpetaA_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetCarpetaA_cmd.CommandText = "SELECT e.path FROM configmenor e INNER JOIN factura f ON e.idEmpresa = f.idempresa WHERE f.idfactura = "&Request.QueryString("idfactura")
RecordsetCarpetaA_cmd.Prepared = true

'response.Write(RecordsetCarpetaA_cmd.CommandText)
Set RecordsetCarpetaA = RecordsetCarpetaA_cmd.Execute
RecordsetCarpetaA_numRows = 0

         'buscamos la suma de los pagos de la factura
                Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
                Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
                    'SELECT d.* FROM dbo.detallespagoFacturas d INNER JOIN pagosFactura p on d.idcc = p.idcc WHERE d.idfactura = 30296 AND estatus = 'Pagado' and timbre ='NO'
                Recordset3_cmd.CommandText = "SELECT d.*, p.moneda as monedaP, p.tipoCambioDR as cambioP FROM dbo.detallespagoFacturas d INNER JOIN pagosFactura p on d.idcc = p.idcc WHERE d.idfactura = "&Request.QueryString("idfactura")&" AND estatus = 'Pagado' and timbre ='NO'"
                'RESPONSE.Write(Recordset3_cmd.CommandText)
                Recordset3_cmd.Prepared = true

                Set Recordset3 = Recordset3_cmd.Execute
                'obtener el total pagado de la factra
                    While (NOT Recordset3.EOF)
                        'if(recordset3.fields.item("moneda").value <> recordset3.fields.item("monedaP").value ) then
                        '    if(recordset3.fields.item("moneda").value = "MXN" AND recordset3.fields.item("monedaP").value = "USD") then
                        '        saldoI = redondear(recordset3.fields.item("impSaldoInsoluto").value * recordset3.fields.item("cambioP").value)
                        '        saldoP = redondear(recordset3.fields.item("impPagado").value * recordset3.fields.item("cambioP").value)
                         '       saldoA = redondear(recordset3.fields.item("ImpSaldoAnt").value * recordset3.fields.item("cambioP").value)   
                        '    elseif(recordset3.fields.item("moneda").value = "USD" AND recordset3.fields.item("monedaP").value = "MXN") then 
                        '        saldoI = redondear(recordset3.fields.item("impSaldoInsoluto").value / recordset3.fields.item("cambioP").value)
                        '        saldoP = redondear(recordset3.fields.item("impPagado").value / recordset3.fields.item("cambioP").value)
                        '        saldoA = redondear(recordset3.fields.item("ImpSaldoAnt").value / recordset3.fields.item("cambioP").value)
                        '    end if
                       ' else
                            saldoI = recordset3.fields.item("impSaldoInsoluto").value
                            saldoP = recordset3.fields.item("impPagado").value
                            saldoA = recordset3.fields.item("ImpSaldoAnt").value
                      '  end if 
        
                        saldoIAux = saldoIAux + saldoI
                        saldoPAux = saldoPAux + saldoP
                        saldoAAux = saldoAAux + saldoA

                    Recordset3.MoveNext()
                    Wend
    
                   ' totalPagado = tt1 - saldoPAux
                  '  totalFacturaSP = Split(recordset1.fields.item("cadena_original").value, "|")
' response.Write(totalPagado) 
                  ' saldoPagoPte = redondear(CDbl(totalFacturaSP(11)) - saldoPAux)

'Set objXMLDoc = Server.CreateObject("MSXML2.DOMDocument.3.0") 
'objXMLDoc.async = False 
'objXMLDoc.load(Server.MapPath("/nova/xmlTimbrado/2017_93977M_FA_1CO.xml")) 
 '   response.Write(Server.MapPath("/nova/xmlTimbrado/2017_93977M_FA_1CO.xml"))
'Dim xmlProduct       
'For Each xmlProduct In objXMLDoc.documentElement.selectNodes("cfdi:Comprobante")
 '    Dim productCode : productCode = xmlProduct.selectSingleNode("Total").text    
  '   Response.Write Server.HTMLEncode(productCode) & "<br>"   
'Next   
    'find xml file path
  '  dim strFilename,strXMLFile
   ' strFilename = "config.xml"
   ' strXMLFile=server.mapPath(strFilename)
    ' standard "create xml object" code
    'Set oXML = server.createObject("MSXML2.DOMDocument.3.0")
    'oXML.async=False
    ' check file exists
    'response.write(strFilename)
    'bitFileExists = oXML.load(Server.MapPath("/nova/xmlTimbrado/2017_93977M_FA_1CO.xml"))
    'if not bitFileExists then
    'response.Write("<h1>Error: " & strFilename & " does not exist</h1>")
    'response.End
    'end if
    'if oXML.parseError.errorCode <> 0 then
    'response.Write("There is a parsing error on your file (" & strFilename & ") " & oXML.parseError.reason & "<p>")
    'response.Write("Line: " & oXML.parseError.line & " Position: " & oXML.parseError.linepos)
    'response.end
    'end if
    'Set lstElements = oXML.getElementsByTagName("cfdi:Comprobante")
     '   for each tmpElement in lstElements
      '  response.Write("<p>Name: " & getElementsByTagName("Total").text & "<br>")
        'response.Write(" Email:" & tmpElement.childnodes(1).text)

    'next

%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link rel="stylesheet" href="css.css" type="text/css" media="screen"  />
</head>
<body>
	<div align="center">
      <h2>
      	<center>Pagos Realizados</center>
         <%if Not Recordset1.EOF then 
              %> <input type="hidden" name="pagoMoneda" value="<%=Recordset1.Fields.Item("monedaP").Value %>"/><input type="hidden" value="<%=Recordset1.Fields.Item("tipoCambioDR").Value %>"/>
     <%end if %>
           </h2>
      <table width="70%">
      	<tr align="right">
      		<td>
      		    <a href="pagosFacturaAdd.asp?idfactura=<%=Request.QueryString("idfactura")%>"><img src="imagenes/application_(add)_16x16.gif" width="16" height="16" border="0" />Agregar</a>		
              </td>
      	</tr>
      </table>
		<br>
	  <table width="85%">
	  	<tr align="center" bgcolor="<%=ctabla%>">
	  		<td># Parcialidad</td>
	  		<td>Fecha</td>
	  		<td>Monto Pagado</td>
	  		<td>Estatus</td>
	  		<td>XML</td>
            <td>PDF</td>
	  		<td>Correo</td>
            <td>Estatus correo</td>
            <td>Cancelar</td>
              <td>Detalles</td>
            <td>Mod</td>            
	  	</tr>
	  	<%While(Not Recordset1.EOF)
	  		numero = numero + 1
              'response.Write(RecordsetCarpetaA.fields.item("path").value)
	  		xml = RecordsetCarpetaA.fields.item("path").value & RecordsetCarpeta.Fields.Item("carpetaPagos").Value&"\"&Year(Now)&"_"&Recordset1.Fields.Item("idfactura").Value&"_P_"&Recordset1.Fields.Item("idcc").Value&".xml"
	  		pdf = RecordsetCarpetaA.fields.item("path").value & RecordsetCarpeta.Fields.Item("carpetaPagos").Value&"\"&Year(Now)&"_"&Recordset1.Fields.Item("idfactura").Value&"_P_"&Recordset1.Fields.Item("idcc").Value&".pdf"
	  		'if para validar el cambio del color
			if color = cgrid2 then'color
				color = cgrid1
			else'color
				color = cgrid2
			end if'color
	  		%>
	  	<tr bgcolor="<%=color%>">
	  		<td align="center"><%=Recordset1.Fields.Item("noParcialidad").Value%></td>
	  		<td align="center"><%=Recordset1.Fields.Item("fechaAlta").Value%></td>
	  		<td>$ <%=Recordset1.Fields("impPagado").Value%></td>
	  		<td><%=Recordset1.Fields.Item("Estatus").Value%></td>
	  		<td align="center">
	  			<%If file.fileExists(RecordsetCarpetaA.fields.item("path").value&RecordsetCarpeta.Fields.Item("carpetaPagos").Value&"\"&Year(Now)&"_"&Recordset1.Fields.Item("idfactura").Value&"_P_"&Recordset1.Fields.Item("idcc").Value&".xml") Then%>
	  			<a href='download.asp?file=<%=xml%>'><img src="imagenes/xml.jpg" width="20" height="20"></a>
	  			<%End If%>
	  		</td>
            <td align="center">
	  			<%If file.fileExists(RecordsetCarpetaA.fields.item("path").value&RecordsetCarpeta.Fields.Item("carpetaPagos").Value&"\"&Year(Now)&"_"&Recordset1.Fields.Item("idfactura").Value&"_P_"&Recordset1.Fields.Item("idcc").Value&".pdf") Then%>
	  			<a href='download.asp?file=<%=pdf%>'><img src="imagenes/pdf.jpg" width="20" height="20"></a>
	  			<%End If%>
	  		</td>
              <td align="center">
                <%If Recordset1.Fields.Item("estatus").Value = "Pagado" Then%>
	  			<a href="enviarCorreoPagos.asp?idfactura=<%=Request.QueryString("idfactura")%>&idPago=<%=Recordset1.Fields.Item("idcc").Value%>"><img src="imagenes/correo2.png" width="25" height="25" border="0" /></a>
	  			<%End If%>
              </td>
                   <% If Recordset1.Fields.Item("estatusCorreo").Value <> "" Then %>
		                <% If Recordset1.Fields.Item("estatusCorreo").Value <> "Enviado" Then %>
                        <td style="background-color:#FF6868;"><%=Recordset1.Fields.Item("estatusCorreo").Value%></td>
                        <% Else %>
                        <td style="background-color:#82FF9E;" align="center"><%=Recordset1.Fields.Item("estatusCorreo").Value%></td>
                        <% End If %>
                    <% Else %>
                        <td>&nbsp;</td>
                    <% End If %>
            <td align="center">
               <% if recordset1.fields.item("timbre").value = "NO" AND recordset1.fields.item("estatus").value <> "Cancelado" then
            %><a href="CancelarFacturas.aspx?idfactura=<%=Request.QueryString("idfactura")%>&idPago=<%=Recordset1.Fields.Item("idcc").Value%>"><img src="imagenes/tacha.gif" width="20" height="20" border="0" /></a>
	  			<%
                  
                   end if 
                 %>
            </td>
              <td align="center">
                  <a href="Detalles.aspx?idfactura=<%=Request.QueryString("idfactura")%>&idPago=<%=Recordset1.Fields.Item("idcc").Value%>"><img src="imagenes/produccion.png" width="20" height="20" border="0" /></a>
              </td>
	  		<td align="center">
                 <%If Recordset1.Fields.Item("estatus").Value = "Pendiente" OR Recordset1.Fields.Item("timbre").Value = "Error"  Then%>
	  			<a href="pagosFacturaMod.asp?idfactura=<%=Request.QueryString("idfactura")%>&idPago=<%=Recordset1.Fields.Item("idcc").Value%>"><img src="imagenes/database_table_(edit)_16x16.gif" width="20" height="20" border="0" /></a>
	  			
                  <% end if %>
	  		</td>
	  	</tr>
	  	<%	
              'variable auxiliar para sumar las cantidades y realizar la conversion de las facturas
             'if(Recordset1.Fields("moneda").Value <> Recordset1.Fields("monedaP").Value) then
             '   monedaAux = redondear(Recordset1.Fields("impPagado").Value / Recordset1.Fields("tipoCambioDR").Value)
             'else
             '   monedaAux = Recordset1.Fields("impPagado").Value
            ' end if 

              ' totalAux =  totalAux + monedaAux

              Recordset1.MoveNext
	  	Wend%>
	  	<tr>
	  		<%
	  		   
	  		%>
	  		<td colspan="4" align="right">Total Pagado:</td>
	  		<td colspan="2" align="left">$ 
				<%=saldoPAux %>
	  		</td>
	  	</tr>
	  </table>
      <p>&nbsp;</p>
    </div>
</body>
<!-- InstanceEnd -->
</html>
<%
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