<%@LANGUAGE="VBSCRIPT"%>
<% Session.CodePage = 65001%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<%
dim archivo'Variable para guardar el archvio que se guardo
dim serie'Varaible para guardar la serie del xml
dim folio'Varaible para guardar el folio del xml
dim fecha'Variable para guardar la fecha del xml
dim sello'Variable para guardar el sello del xml
dim noAprobacion'Variable para guardar el noAprobacion del xml
dim anoAprobacion'Variable para guardar el anoAprobacion del xml
dim tipoDeComprobante'Variable para guardar el tipoDeComprobante del xml
dim formaDePago'Variable para guardar la formaDePago del xml
dim noCertificado'Variable para guardar el noCertificado del xml
dim certificado'Variable para guardar el noCertificado del xml
dim subTotal'Variable para guardar el noCertificado del xml
dim total'Variable para guardar el total del xml
dim rfcEmisor'Variable para guardar el rfc del emisor
dim nombreEmisor'Variable para guardar el nombre del emisor
dim domicilioEmisor'Variable para guardar el DomicilioFiscal del emisor
dim noExteriorEmisor'Variable para guardar el noExterior del emisor
dim noInteriorEmisor'Variable para guardar el noInterior del emisor
dim coloniaEmisor'Variable para guardar la colonia del emisor
dim municipioEmisor'Variable para guardar el municipio del emisor
dim estadoEmisor'Variable para guardar el municipio del emisor
dim paisEmisor'Variable para guardar el pais del emisor
dim codigoPostalEmisor'Variable para guardar el codigoPostal de la Expedido
dim domicilioExpedido'Variable para guardar el DomicilioFiscal de la Expedido
dim noExteriorExpedido'Variable para guardar el noExterior de la Expedido
dim noInteriorExpedido'Variable para guardar el noInterior de la Expedido
dim coloniaExpedido'Variable para guardar la colonia de la Expedido
dim municipioExpedido'Variable para guardar el municipio de la Expedido
dim estadoExpedido'Variable para guardar el municipio de la Expedido
dim paisExpedido'Variable para guardar el pais de la Expedido
dim codigoPostalExpedido'Variable para guardar el codigoPostal de la Expedido
dim rfcreceptor'Variable para guardar el rfc del receptor
dim nombrereceptor'Variable para guardar el nombre del receptor
dim domicilioreceptor'Variable para guardar el DomicilioFiscal del receptor
dim noExteriorreceptor'Variable para guardar el noExterior del receptor
dim noInteriorreceptor'Variable para guardar el noInterior del receptor
dim coloniareceptor'Variable para guardar la colonia del receptor
dim municipioreceptor'Variable para guardar el municipio del receptor
dim estadoreceptor'Variable para guardar el municipio del receptor
dim paisreceptor'Variable para guardar el pais del receptor
dim codigoPostalreceptor'Variable para guardar el codigoPostal del receptor
dim conceptos'variable para guardar los conceptos del xml
dim totalImpuestosTrasladados'Variable para guardar el totalImpuestosTrasladados del xml
dim impuesto'Variable para guardar el impuesto del xml
dim tasa'Variable para guardar la tasa del xml
dim importe'Variable para guardar el importe del xml
dim carpeta'Variable para guardar el nombre de la carpeta
'conectamos con el FSO 
set fso = createObject("scripting.filesystemobject") 
%>
<%
carpeta = "DATG600412JT3"'Request.QueryString("carpeta")
archivo = Request.QueryString("nombre")


'volvemos a abrir el fichero para lectura 
'fso.copyfile Server.MapPath(".")&"\Proveedor_Aprobado\"&archivo , Server.MapPath(".")&"\leerxml\"&Mid(archivo,1,Instrrev(archivo,".")-1)&".txt"
    Response.Write(Server.MapPath(".")&"\"&carpeta&"\"&archivo)
set fich = fso.OpenTextFile(Server.MapPath(".")&"\"&carpeta&"\"&archivo) 
'leemos el contenido del fichero 
texto_fichero = fich.readAll()
'Response.Write(texto_fichero)
'-----------serie del xml----------------------------- 
serie = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"serie")+1)
serie = Mid(serie,Instr(serie,chr(34))+1)
serie = Mid(serie,1,Instr(serie,chr(34))-1)
'-----------------------------------------------------
'-----------folio del xml--------------------------------
folio = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"folio")+1)
folio = Mid(folio,Instr(folio,chr(34))+1)
folio = Mid(folio,1,Instr(folio,chr(34))-1)
'-----------------------------------------------------
'-----------fecha del xml--------------------------------
fecha = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"fecha")+1)
fecha = Mid(fecha,Instr(fecha,chr(34))+1)
fecha = Mid(fecha,1,Instr(fecha,chr(34))-1)
'-----------------------------------------------------
'-----------sello del xml--------------------------------
sello = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"sello")+1)
sello = Mid(sello,Instr(sello,chr(34))+1)
sello = Mid(sello,1,Instr(sello,chr(34))-1)
'-----------------------------------------------------
'-----------noAprobacion del xml--------------------------------
noAprobacion = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"noAprobacion")+1)
noAprobacion = Mid(noAprobacion,Instr(noAprobacion,chr(34))+1)
noAprobacion = Mid(noAprobacion,1,Instr(noAprobacion,chr(34))-1)
'-----------------------------------------------------
'-----------anoAprobacion del xml--------------------------------
anoAprobacion = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"anoAprobacion")+1)
anoAprobacion = Mid(anoAprobacion,Instr(anoAprobacion,chr(34))+1)
anoAprobacion = Mid(anoAprobacion,1,Instr(anoAprobacion,chr(34))-1)
'-----------------------------------------------------
'-----------tipoDeComprobante del xml--------------------------------
tipoDeComprobante = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"tipoDeComprobante")+1)
tipoDeComprobante = Mid(tipoDeComprobante,Instr(tipoDeComprobante,chr(34))+1)
tipoDeComprobante = Mid(tipoDeComprobante,1,Instr(tipoDeComprobante,chr(34))-1)
'-----------------------------------------------------
'-----------formaDePago del xml--------------------------------
formaDePago = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"formaDePago")+1)
formaDePago = Mid(formaDePago,Instr(formaDePago,chr(34))+1)
formaDePago = Mid(formaDePago,1,Instr(formaDePago,chr(34))-1)
'-----------------------------------------------------
'-----------noCertificado del xml--------------------------------
noCertificado = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"noCertificado")+1)
noCertificado = Mid(noCertificado,Instr(noCertificado,chr(34))+1)
noCertificado = Mid(noCertificado,1,Instr(noCertificado,chr(34))-1)
'-----------------------------------------------------
'-----------certificado del xml--------------------------------
certificado = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"certificado")+1)
certificado = Mid(certificado,Instr(certificado,chr(34))+1)
certificado = Mid(certificado,1,Instr(certificado,chr(34))-1)
'-----------------------------------------------------
'-----------subTotal del xml--------------------------------
subTotal = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"subTotal")+1)
subTotal = Mid(subTotal,Instr(subTotal,chr(34))+1)
subTotal = Mid(subTotal,1,Instr(subTotal,chr(34))-1)
'-----------------------------------------------------
'-----------total del xml--------------------------------
total = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"total")+1)
total = Mid(total,Instr(total,chr(34))+1)
total = Mid(total,1,Instr(total,chr(34))-1)
'-----------------------------------------------------
'-----------rfcEmisor del xml--------------------------------
rfcEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
rfcEmisor = Mid(rfcEmisor,Instr(rfcEmisor,"rfc")+1)
rfcEmisor = Mid(rfcEmisor,Instr(rfcEmisor,chr(34))+1)
rfcEmisor = Mid(rfcEmisor,1,Instr(rfcEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------nombreEmisor del xml--------------------------------
nombreEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
nombreEmisor = Mid(nombreEmisor,Instr(nombreEmisor,"nombre")+1)
nombreEmisor = Mid(nombreEmisor,Instr(nombreEmisor,chr(34))+1)
nombreEmisor = Mid(nombreEmisor,1,Instr(nombreEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------domicilioEmisor del xml--------------------------------
domicilioEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
domicilioEmisor = Mid(domicilioEmisor,Instr(domicilioEmisor,"calle")+1)
domicilioEmisor = Mid(domicilioEmisor,Instr(domicilioEmisor,chr(34))+1)
domicilioEmisor = Mid(domicilioEmisor,1,Instr(domicilioEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------noExteriorEmisor del xml--------------------------------
noExteriorEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
noExteriorEmisor = Mid(noExteriorEmisor,Instr(noExteriorEmisor,"noExterior")+1)
noExteriorEmisor = Mid(noExteriorEmisor,Instr(noExteriorEmisor,chr(34))+1)
noExteriorEmisor = Mid(noExteriorEmisor,1,Instr(noExteriorEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------noInterior del xml--------------------------------
noInteriorEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
noInteriorEmisor = Mid(noInteriorEmisor,Instr(noInteriorEmisor,"noInterior")+1)
noInteriorEmisor = Mid(noInteriorEmisor,Instr(noInteriorEmisor,chr(34))+1)
noInteriorEmisor = Mid(noInteriorEmisor,1,Instr(noInteriorEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------coloniaEmisor del xml--------------------------------
coloniaEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
coloniaEmisor = Mid(coloniaEmisor,Instr(coloniaEmisor,"colonia")+1)
coloniaEmisor = Mid(coloniaEmisor,Instr(coloniaEmisor,chr(34))+1)
coloniaEmisor = Mid(coloniaEmisor,1,Instr(coloniaEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------municipioEmisor del xml--------------------------------
municipioEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
municipioEmisor = Mid(municipioEmisor,Instr(municipioEmisor,"municipio")+1)
municipioEmisor = Mid(municipioEmisor,Instr(municipioEmisor,chr(34))+1)
municipioEmisor = Mid(municipioEmisor,1,Instr(municipioEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------estadoEmisor del xml--------------------------------
estadoEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
estadoEmisor = Mid(estadoEmisor,Instr(estadoEmisor,"estado")+1)
estadoEmisor = Mid(estadoEmisor,Instr(estadoEmisor,chr(34))+1)
estadoEmisor = Mid(estadoEmisor,1,Instr(estadoEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------paisEmisor del xml--------------------------------
paisEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
paisEmisor = Mid(paisEmisor,Instr(paisEmisor,"pais")+1)
paisEmisor = Mid(paisEmisor,Instr(paisEmisor,chr(34))+1)
paisEmisor = Mid(paisEmisor,1,Instr(paisEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------codigoPostalEmisor del xml--------------------------------
codigoPostalEmisor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Emisor")+1)
codigoPostalEmisor = Mid(codigoPostalEmisor,Instr(codigoPostalEmisor,"codigoPostal")+1)
codigoPostalEmisor = Mid(codigoPostalEmisor,Instr(codigoPostalEmisor,chr(34))+1)
codigoPostalEmisor = Mid(codigoPostalEmisor,1,Instr(codigoPostalEmisor,chr(34))-1)
'-----------------------------------------------------
'-----------domicilioExpedido del xml--------------------------------
domicilioExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
domicilioExpedido = Mid(domicilioExpedido,Instr(domicilioExpedido,"calle")+1)
domicilioExpedido = Mid(domicilioExpedido,Instr(domicilioExpedido,chr(34))+1)
domicilioExpedido = Mid(domicilioExpedido,1,Instr(domicilioExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------noExteriorEmisor del xml--------------------------------
noExteriorExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
noExteriorExpedido = Mid(noExteriorExpedido,Instr(noExteriorExpedido,"noExterior")+1)
noExteriorExpedido = Mid(noExteriorExpedido,Instr(noExteriorExpedido,chr(34))+1)
noExteriorExpedido = Mid(noExteriorExpedido,1,Instr(noExteriorExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------noInteriorEmisor del xml--------------------------------
noInteriorExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
noInteriorExpedido = Mid(noInteriorExpedido,Instr(noInteriorExpedido,"noInterior")+1)
noInteriorExpedido = Mid(noInteriorExpedido,Instr(noInteriorExpedido,chr(34))+1)
noInteriorExpedido = Mid(noInteriorExpedido,1,Instr(noInteriorExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------coloniaEmisor del xml--------------------------------
coloniaExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
coloniaExpedido = Mid(coloniaExpedido,Instr(coloniaExpedido,"colonia")+1)
coloniaExpedido = Mid(coloniaExpedido,Instr(coloniaExpedido,chr(34))+1)
coloniaExpedido = Mid(coloniaExpedido,1,Instr(coloniaExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------municipioEmisor del xml--------------------------------
municipioExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
municipioExpedido = Mid(municipioExpedido,Instr(municipioExpedido,"municipio")+1)
municipioExpedido = Mid(municipioExpedido,Instr(municipioExpedido,chr(34))+1)
municipioExpedido = Mid(municipioExpedido,1,Instr(municipioExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------estadoEmisor del xml--------------------------------
estadoExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
estadoExpedido = Mid(estadoExpedido,Instr(estadoExpedido,"estado")+1)
estadoExpedido = Mid(estadoExpedido,Instr(estadoExpedido,chr(34))+1)
estadoExpedido = Mid(estadoExpedido,1,Instr(estadoExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------paisEmisor del xml--------------------------------
paisExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
paisExpedido = Mid(paisExpedido,Instr(paisExpedido,"pais")+1)
paisExpedido = Mid(paisExpedido,Instr(paisExpedido,chr(34))+1)
paisExpedido = Mid(paisExpedido,1,Instr(paisExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------codigoPostalEmisor del xml--------------------------------
codigoPostalExpedido = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"ExpedidoEn")+1)
codigoPostalExpedido = Mid(codigoPostalExpedido,Instr(codigoPostalExpedido,"codigoPostal")+1)
codigoPostalExpedido = Mid(codigoPostalExpedido,Instr(codigoPostalExpedido,chr(34))+1)
codigoPostalExpedido = Mid(codigoPostalExpedido,1,Instr(codigoPostalExpedido,chr(34))-1)
'-----------------------------------------------------
'-----------rfcreceptor del xml--------------------------------
rfcreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
rfcreceptor = Mid(rfcreceptor,Instr(rfcreceptor,"rfc")+1)
rfcreceptor = Mid(rfcreceptor,Instr(rfcreceptor,chr(34))+1)
rfcreceptor = Mid(rfcreceptor,1,Instr(rfcreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------nombrereceptor del xml--------------------------------
nombrereceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
nombrereceptor = Mid(nombrereceptor,Instr(nombrereceptor,"nombre")+1)
nombrereceptor = Mid(nombrereceptor,Instr(nombrereceptor,chr(34))+1)
nombrereceptor = Mid(nombrereceptor,1,Instr(nombrereceptor,chr(34))-1)
'-----------------------------------------------------
'-----------domicilioreceptor del xml--------------------------------
domicilioreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
domicilioreceptor = Mid(domicilioreceptor,Instr(domicilioreceptor,"calle")+1)
domicilioreceptor = Mid(domicilioreceptor,Instr(domicilioreceptor,chr(34))+1)
domicilioreceptor = Mid(domicilioreceptor,1,Instr(domicilioreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------noExteriorreceptor del xml--------------------------------
noExteriorreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
noExteriorreceptor = Mid(noExteriorreceptor,Instr(noExteriorreceptor,"noExterior")+1)
noExteriorreceptor = Mid(noExteriorreceptor,Instr(noExteriorreceptor,chr(34))+1)
noExteriorreceptor = Mid(noExteriorreceptor,1,Instr(noExteriorreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------noInteriorreceptor del xml--------------------------------
noInteriorreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
'if para validar que el xml tenga numero interior del receptor
if Instr(noInteriorreceptor,"noInterior") <> 0 then'noInterior
noInteriorreceptor = Mid(noInteriorreceptor,Instr(noInteriorreceptor,"noInterior")+1)
noInteriorreceptor = Mid(noInteriorreceptor,Instr(noInteriorreceptor,chr(34))+1)
noInteriorreceptor = Mid(noInteriorreceptor,1,Instr(noInteriorreceptor,chr(34))-1)
end if'noInterior
'-----------------------------------------------------
'-----------coloniareceptor del xml--------------------------------
coloniareceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
coloniareceptor = Mid(coloniareceptor,Instr(coloniareceptor,"localidad")+1)
coloniareceptor = Mid(coloniareceptor,Instr(coloniareceptor,chr(34))+1)
coloniareceptor = Mid(coloniareceptor,1,Instr(coloniareceptor,chr(34))-1)

'-----------------------------------------------------
'-----------municipioreceptor del xml--------------------------------
municipioreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
municipioreceptor = Mid(municipioreceptor,Instr(municipioreceptor,"municipio")+1)
municipioreceptor = Mid(municipioreceptor,Instr(municipioreceptor,chr(34))+1)
municipioreceptor = Mid(municipioreceptor,1,Instr(municipioreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------estadoreceptor del xml--------------------------------
estadoreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
estadoreceptor = Mid(estadoreceptor,Instr(estadoreceptor,"estado")+1)
estadoreceptor = Mid(estadoreceptor,Instr(estadoreceptor,chr(34))+1)
estadoreceptor = Mid(estadoreceptor,1,Instr(estadoreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------paisreceptor del xml--------------------------------
paisreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
paisreceptor = Mid(paisreceptor,Instr(paisreceptor,"pais")+1)
paisreceptor = Mid(paisreceptor,Instr(paisreceptor,chr(34))+1)
paisreceptor = Mid(paisreceptor,1,Instr(paisreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------codigoPostalreceptor del xml--------------------------------
codigoPostalreceptor = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Receptor")+1)
codigoPostalreceptor = Mid(codigoPostalreceptor,Instr(codigoPostalreceptor,"codigoPostal")+1)
codigoPostalreceptor = Mid(codigoPostalreceptor,Instr(codigoPostalreceptor,chr(34))+1)
codigoPostalreceptor = Mid(codigoPostalreceptor,1,Instr(codigoPostalreceptor,chr(34))-1)
'-----------------------------------------------------
'-----------conceptos del xml--------------------------------
conceptos = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Conceptos")+1)
conceptos = Mid(conceptos,Instr(conceptos,">")+1)
IF Instr(conceptos,"/>/Conceptos") <> 0 THEN
conceptos = Mid(conceptos,1,Instr(conceptos,"/>/Conceptos")-1)
END IF
'-----------------------------------------------------
'-----------totalImpuestosTrasladados del xml--------------------------------
totalImpuestosTrasladados = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"totalImpuestosTrasladados")+1)
totalImpuestosTrasladados = Mid(totalImpuestosTrasladados,Instr(totalImpuestosTrasladados,chr(34))+1)
totalImpuestosTrasladados = Mid(totalImpuestosTrasladados,1,Instr(totalImpuestosTrasladados,chr(34))-1)
'-----------------------------------------------------
'-----------impuesto del xml--------------------------------
impuesto = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"impuesto")+1)
impuesto = Mid(impuesto,Instr(impuesto,chr(34))+1)
impuesto = Mid(impuesto,1,Instr(impuesto,chr(34))-1)
'-----------------------------------------------------
'-----------tasa del xml--------------------------------
tasa = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"tasa")+1)
tasa = Mid(tasa,Instr(tasa,chr(34))+1)
tasa = Mid(tasa,1,Instr(tasa,chr(34))-1)
'-----------------------------------------------------
'-----------importe del xml--------------------------------
importe = Mid(Replace(texto_fichero,"<",""),Instr(Replace(texto_fichero,"<",""),"Traslado")+1)
importe = Mid(importe,Instr(importe,"importe")+1)
importe = Mid(importe,Instr(importe,chr(34))+1)
importe = Mid(importe,1,Instr(importe,chr(34))-1)
'-----------------------------------------------------
'Response.Write(subTotal)
'Response.Write("<br />")
'Response.Write(totalImpuestosTrasladados)
'Response.Write("<br />")
'Response.Write(total)
'imprimimos en la página el contenido del fichero 
'response.write("texto="&importe) 

'cerramos el fichero 
fich.close() 
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Documento sin título</title>
</head>

<body>
<p align="center"><a href="cargaFactura2.asp?rfc=<%=rfcreceptor%>&folio=<%=folio%>&fechacfd=<%=fecha%>&iva=<%=totalImpuestosTrasladados%>&subtotal=<%=subtotal%>&total=<%=total%>&nombre=<%=request.QueryString("nombre")%>">SIGUIENTE</a></p>
</body>
</html>