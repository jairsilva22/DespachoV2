<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
dim pais2'Variable para guardar el pasi para filtrar los estados
dim estado2'Variable para guardar el esta para filtrar las ciudades
estado2 = 0
pais2 = 0
%>
<%
Dim MM_editAction
MM_editAction = CStr(Request.ServerVariables("SCRIPT_NAME"))
If (Request.QueryString <> "") Then
  MM_editAction = MM_editAction & "?" & Server.HTMLEncode(Request.QueryString)
End If

' boolean to abort record edit
Dim MM_abortEdit
MM_abortEdit = false
%>
<%
' IIf implementation
Function MM_IIf(condition, ifTrue, ifFalse)
  If condition = "" Then
    MM_IIf = ifFalse
  Else
    MM_IIf = ifTrue
  End If
End Function
%>
<%
If (CStr(Request("MM_update")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the update
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "UPDATE dbo.clientesFacturacion SET nombreCliente = ?, rfcCliente = ?, telefonoCliente = ?, correo = ?, calleCliente = ?, paisCliente = ?, estadoCliente = ?, ciudadCliente = ?, codigopostalCliente = ?, noExterior = ?, noInterior = ?, colonia = ?, referencia = ?, terminoPago = ?, entrega = ?, obsCliente = ?, nombreEmpresa = ?, clave = ?, destino = ?, valorAgregado = ?, metodoPago = ?, numeroCuenta = ?, retencion = '"&Trim(Request.Form("retencion"))&"', tazaIva = ? WHERE idCliente = ?" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 202, 1, 225, Request.Form("nombreCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 202, 1, 225, Request.Form("rfcCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 202, 1, 225, Request.Form("telefonoCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 202, 1, 225, Request.Form("correo")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 202, 1, 225, Request.Form("calleCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 202, 1, 225, 1) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 202, 1, 225, Request.Form("estadoCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 202, 1, 225, Request.Form("ciudadCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 202, 1, 225, Request.Form("codigopostalCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 201, 1, 255, Request.Form("noExterior")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 201, 1, 255, Request.Form("noInterior")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("colonia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 201, 1, 255, Request.Form("referencia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 202, 1, 225, Request.Form("terminoPago")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 201, 1, 255, Request.Form("entrega")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 201, 1, 8000, Request.Form("obsCliente")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 201, 1, 8000, Request.Form("nombreEmpresa")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param18", 201, 1, 8000, Request.Form("clave")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param19", 201, 1, 8000, Request.Form("destino")) ' adLongVarChar		
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param20", 5, 1, -1, MM_IIF(Request.Form("valorAgregado"), 1, 0)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param21", 201, 1, 225, Request.Form("metodoPago")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param22", 201, 1, 225, Request.Form("numeroCuenta")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param23", 5, 1, -1, MM_IIF(Request.Form("tazaIva"), Request.Form("tazaIva"), null)) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param24", 5, 1, -1, MM_IIF(Request.Form("MM_recordId"), Request.Form("MM_recordId"), null)) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    Dim MM_editRedirectUrl
    MM_editRedirectUrl = "clientes.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If
End If
%>
<%
' *** Delete Record: construct a sql delete statement and execute it

If (CStr(Request("MM_delete")) = "form2" And CStr(Request("MM_recordId")) <> "") Then

  If (Not MM_abortEdit) Then
    ' execute the delete
    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "DELETE FROM dbo.clientesFacturacion WHERE idCliente = ?"
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 5, 1, -1, Request.Form("MM_recordId")) ' adDouble
    MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close

    ' append the query string to the redirect URL
    'Dim MM_editRedirectUrl
    MM_editRedirectUrl = "clientes.asp"
    If (Request.QueryString <> "") Then
      If (InStr(1, MM_editRedirectUrl, "?", vbTextCompare) = 0) Then
        MM_editRedirectUrl = MM_editRedirectUrl & "?" & Request.QueryString
      Else
        MM_editRedirectUrl = MM_editRedirectUrl & "&" & Request.QueryString
      End If
    End If
    Response.Redirect(MM_editRedirectUrl)
  End If

End If
%>
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idcliente") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idcliente")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT idCliente, nombreCliente, rfcCliente, telefonoCliente, calleCliente, paisCliente, estadoCliente, ciudadCliente, codigopostalCliente, idusuario, fechaAlta, idempresa, noExterior, noInterior, colonia, referencia, municipio, nombreEmpresa, correo, terminoPago, clave, destino, valorAgregado, retencion, tazaIva, metodoPago, numeroCuenta, entrega, obsCliente FROM dbo.clientesFacturacion WHERE idCliente = ?" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>


<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = pais2
If (Request.Form("paisCliente") <> "") Then 
  Recordset3__MMColParam = Request.Form("paisCliente")
End If
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.estados ORDER BY estado ASC" 
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
if NOT Recordset3.EOF then
estado2 = (Recordset3.Fields.Item("id").Value)
'if para validar que el estado no venga solo
if Recordset1.Fields.Item("estadoCliente").Value <> "" then

'--------Recordset para buscar el estado que se selcciono----------------------------------
Set RSEstados_cmd = Server.CreateObject ("ADODB.Command")
RSEstados_cmd.ActiveConnection = MM_Conecta1_STRING
RSEstados_cmd.CommandText = "SELECT * FROM dbo.estados WHERE id = "&(Recordset1.Fields.Item("estadoCliente").Value)&" ORDER BY estado ASC" 
RSEstados_cmd.Prepared = true

Set RSEstados = RSEstados_cmd.Execute

if NOT RSEstados.EOF then
estado2 = (RSEstados.Fields.Item("id").Value)
end if
end if

end if
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = estado2
If (Request.Form("estadoCliente") <> "") Then 
  Recordset4__MMColParam = Request.Form("estadoCliente")
End If
%>
<%
Dim Recordset4__MMColParam2
Recordset4__MMColParam2 = pais2
If (Request.Form("paisCliente") <> "") Then 
  Recordset4__MMColParam2 = Request.Form("paisCliente")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.ciudades WHERE idEstado = ? ORDER BY ciudad ASC" 
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble
'Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param2", 5, 1, -1, Recordset4__MMColParam2) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<%
dim retencion'Variable para guardar la retencion
dim tazaIva'Variable para guardar el iva
dim metodoPago'Variable para guardar el metodo de pago
dim numeroCuenta'Variable para guardar el numero de cuenta
dim valorAgregado'Variable para guardar el valor agregado del cliente
dim nombre'Variable para guardar el nombre del cliente
dim empresa'Variable para guardar el nombre fiscal
dim rfc'Variable para guardar el RFC
dim telefono'Variable para guardar el telefono
dim calle'Variable para guardar la calle
dim pais'Variable para guardar el pais
dim estado'Variable para guardar el estado
dim ciudad'Variable para guardar la ciudad
dim postal'Variable para guardar el codigo postal
dim extrior'Variable para guardar el numero exterior
dim interiro'Variable para guardar el numero interior
dim colonia'Variable para guardar la colonia
dim referencia'Variable para guardar la referencia
dim entrega'Variable ara guardar los daots de entrea
dim correo'Variable para guardar el correo
dim terminos'Variable para guardar los terminos
dim obsCliente'Varaibela para guardar las observaciones
dim clave'Variable para guardar la calve
dim destino'Variable para guardar el destino

'if para validar si cambio de retencion
if Request.Form("retencion") <> "" then'retencion
retencion = Request.Form("retencion")
else'retencion
retencion = (Recordset1.Fields.Item("retencion").Value)
end if'retencion

'if para validar si cambio de tazaIva
if Request.Form("tazaIva") <> "" then'tazaIva
tazaIva = Request.Form("tazaIva")
else'tazaIva
tazaIva = (Recordset1.Fields.Item("tazaIva").Value)
end if'tazaIva

'if para validar si cambio de metodoPago
if Request.Form("metodoPago") <> "" then'metodoPago
metodoPago = Request.Form("metodoPago")
else'metodoPago
metodoPago = (Recordset1.Fields.Item("metodoPago").Value)
end if'metodoPago

'if para validar si cambio de numeroCuenta
if Request.Form("numeroCuenta") <> "" then'numeroCuenta
numeroCuenta = Request.Form("numeroCuenta")
else'numeroCuenta
numeroCuenta = (Recordset1.Fields.Item("numeroCuenta").Value)
end if'numeroCuenta

'if para validar si cambio de valorAgregado
if Request.Form("valorAgregado") <> "" then'valorAgregado
valorAgregado = Request.Form("valorAgregado")
else'valorAgregado
valorAgregado = (Recordset1.Fields.Item("valorAgregado").Value)
end if'valorAgregado

'if para validar si cambio de clave
if Request.Form("clave") <> "" then'clave
clave = Request.Form("clave")
else'clave
clave = (Recordset1.Fields.Item("clave").Value)
end if'clave

'if para vavlidar si cambio el destino
if Request.Form("destino") <> "" then'destino
destino = Request.Form("destino")
else'destino
destino = (Recordset1.Fields.Item("destino").Value)
end if'destino

'if para vavlidar si cambio el termino de pago
if Request.Form("terminoPago") <> "" then'terminoPago
terminos = Request.Form("terminoPago")
else'terminoPago
terminos = (Recordset1.Fields.Item("terminoPago").Value)
end if'terminoPago

'if para vavlidar si cambio el nombre del cliente
if Request.Form("nombreCliente") <> "" then'nombreCliente
nombre = Request.Form("nombreCliente")
else'nombreCliente
nombre = (Recordset1.Fields.Item("nombreCliente").Value)
end if'nombreCliente

'if para vavlidar si cambio el nombre de la empresa
if Request.Form("nombreEmpresa") <> "" then'nombreEmpresa
empresa = Request.Form("nombreEmpresa")
else'nombreEmpresa
empresa = (Recordset1.Fields.Item("nombreEmpresa").Value)
end if'nombreEmpresa

'if para vavlidar si cambio el rfc del cliente
if Request.Form("rfcCliente") <> "" then'rfcCliente
rfc = Request.Form("rfcCliente")
else'rfcCliente
rfc = (Recordset1.Fields.Item("rfcCliente").Value)
end if'rfcCliente

'if para vavlidar si cambio el telefono del cliente
if Request.Form("telefonoCliente") <> "" then'telefonoCliente
telefono = Request.Form("telefonoCliente")
else'telefonoCliente
telefono = (Recordset1.Fields.Item("telefonoCliente").Value)
end if'telefonoCliente

'if para vavlidar si cambio el docimilio
if Request.Form("calleCliente") <> "" then'calleCliente
calle = Request.Form("calleCliente")
else'calleCliente
calle = (Recordset1.Fields.Item("calleCliente").Value)
end if'calleCliente

'if para vavlidar si cambio el pais
if Request.Form("paisCliente") <> "" then'paisCliente
pais = Request.Form("paisCliente")
else'paisCliente
pais = (Recordset1.Fields.Item("paisCliente").Value)
end if'paisCliente

'if para vavlidar si cambio el estado
if Request.Form("estadoCliente") <> "" then'estadoCliente
estado = Request.Form("estadoCliente")
else'estadoCliente
estado = (Recordset1.Fields.Item("estadoCliente").Value)
end if'estadoCliente

'if para vavlidar si cambio la ciudad
if Request.Form("ciudadCliente") <> "" then'ciudadCliente
ciudad = Request.Form("ciudadCliente")
else'ciudadCliente
ciudad = (Recordset1.Fields.Item("ciudadCliente").Value)
end if'ciudadCliente

'if para vavlidar si cambio el codigo
if Request.Form("codigopostalCliente") <> "" then'codigopostalCliente
postal = Request.Form("codigopostalCliente")
else'codigopostalCliente
postal = (Recordset1.Fields.Item("codigopostalCliente").Value)
end if'codigopostalCliente

'if para vavlidar si cambio el noExterior
if Request.Form("noExterior") <> "" then'noExterior
extrior = Request.Form("noExterior")
else'noExterior
extrior = (Recordset1.Fields.Item("noExterior").Value)
end if'noExterior

'if para vavlidar si cambio el noInterior
if Request.Form("noInterior") <> "" then'noInterior
interiro = Request.Form("noInterior")
else'noInterior
interiro = (Recordset1.Fields.Item("noInterior").Value)
end if'noInterior

'if para vavlidar si cambio el colonia
if Request.Form("colonia") <> "" then'colonia
colonia = Request.Form("colonia")
else'colonia
colonia = (Recordset1.Fields.Item("colonia").Value)
end if'colonia

'if para vavlidar si cambio la referencia
if Request.Form("referencia") <> "" then'referencia
referencia = Request.Form("referencia")
else'referencia
referencia = (Recordset1.Fields.Item("referencia").Value)
end if'referencia

'if para vavlidar si cambio la entrega
if Request.Form("entrega") <> "" then'entrega
entrega = Request.Form("entrega")
else'entrega
entrega = (Recordset1.Fields.Item("entrega").Value)
end if'entrega

'if para vavlidar si cambio la nombreCliente
if Request.Form("nombreCliente") <> "" then'nombreCliente
nombre = Request.Form("nombreCliente")
else'nombreCliente
nombre = (Recordset1.Fields.Item("nombreCliente").Value)
end if'nombreCliente

'if para vavlidar si cambio la correo
if Request.Form("correo") <> "" then'correo
correo = Request.Form("correo")
else'correo
correo = (Recordset1.Fields.Item("correo").Value)
end if'correo

'if para vavlidar si cambio la obsCliente
if Request.Form("obsCliente") <> "" then'obsCliente
obsCliente = Request.Form("obsCliente")
else'obsCliente
obsCliente = (Recordset1.Fields.Item("obsCliente").Value)
end if'obsCliente
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <!-- InstanceBeginEditable name="doctitle" -->
    <title><%=titlePage%></title>
     <link rel="stylesheet" href="jsF/css.css" type="text/css" media="screen"  />
    <link rel="stylesheet" href="jsF/bootstrap.css" />
    <script src="jsF/jquery.min.js" type="text/javascript"></script>
    <script src="jsF/bootstrap.min.js"></script>
    <!-- InstanceEndEditable -->
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
    <script>
  function validarFormulario(){
  var nombreFiscal = document.getElementById('nombreEmpresa').value;
  if(nombreFiscal == null || nombreFiscal.length == 0 || /^\s+$/.test(nombreFiscal)){
      alert('ERROR: El campo nombre cliente no debe ir vacío ');
      return false;
    }

    var nombreCliente = document.getElementById('nombreCliente').value;
    if(nombreCliente == null || nombreCliente.length == 0 || /^\s+$/.test(nombreCliente)){
      alert('ERROR: El campo nombre fiscal no debe ir vacío ');
      return false;
    }
    var paisCliente = document.getElementById('paisCliente').selectedIndex;

    if(paisCliente == null || paisCliente == 0){
      alert('ERROR: Debe seleccionar una opcion del combo box pais');
      return false;
    }
    var estadoCliente = document.getElementById('estadoCliente').selectedIndex;

    if(estadoCliente == null || estadoCliente == 0){
      alert('ERROR: Debe seleccionar una opcion del combo box estado');
      return false;
    }
    var ciudadCliente = document.getElementById('ciudadCliente').selectedIndex;
    if(ciudadCliente == null || ciudadCliente == 0){
      alert('ERROR: Debe seleccionar una opcion del combo box ciudad');
      return false;
    }
    var rfcCliente = document.getElementById('rfcCliente').value;
    if( rfcCliente == null || rfcCliente.length == 0 || /^\s+$/.test(rfcCliente)){
      alert('ERROR: El campo RFC no debe ir vacío ');
      return false;
    }
  return true
  }
    </script>
    <!-- InstanceBeginEditable name="head" -->
    <!-- InstanceEndEditable -->
</head>

<body style="background-color: white">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
       
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion1" -->
                <p>&nbsp;</p>
                <form action="<%=MM_editAction%>" method="POST" name="form1" id="form1" onsubmit="return validarFormulario()">
                    <table align="center" width="80%">
                        <tr valign="baseline">
                            <td colspan="4" align="center" nowrap="nowrap" bgcolor="#66CCFF">Modificar Cliente (Información General)</td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Nombre del cliente:</td>
                            <td>
                                <input type="text" name="nombreCliente" id="nombreCliente" value="<%=(nombre)%>" size="32" />
                            </td>
                           
                            
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Nombre fiscal:</td>
                            <td>
                                <input name="nombreEmpresa" type="text" id="nombreEmpresa" value="<%=empresa%>" size="32" />
                            </td>
                            <td align="right">Estado:</td>
                            <td>
                                <select name="estadoCliente" id="estadoCliente" onchange="cambiar()">
                                    <option value="" <%If (Not isNull((estado))) Then If ("" = CStr((estado))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                    <%
                                    While (NOT Recordset3.EOF)
                                    %>
                                    <option value="<%=(Recordset3.Fields.Item("id").Value)%>" <%If (Not isNull((estado))) Then If (CStr(Recordset3.Fields.Item("id").Value) = CStr((estado))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset3.Fields.Item("estado").Value)%></option>
                                    <%
                                      Recordset3.MoveNext()
                                    Wend
                                    If (Recordset3.CursorType > 0) Then
                                      Recordset3.MoveFirst
                                    Else
                                      Recordset3.Requery
                                    End If
                                    %>
                                </select></td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right" nowrap="nowrap">RFC:</td>
                            <td>
                                <input type="text" name="rfcCliente" id="rfcCliente" value="<%=(rfc)%>" size="32" /></td>
                            <td align="right">Ciudad:</td>
                            <td>
                                <select name="ciudadCliente" id="ciudadCliente" onchange="cambiar()">
                                    <option value="" <%If (Not isNull((ciudad))) Then If ("" = CStr((ciudad))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                    <%
                                    While (NOT Recordset4.EOF)
                                    %>
                                    <option value="<%=(Recordset4.Fields.Item("id").Value)%>" <%If (Not isNull((ciudad))) Then If (CStr(Recordset4.Fields.Item("id").Value) = CStr((ciudad))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset4.Fields.Item("ciudad").Value)%></option>
                                    <%
                                      Recordset4.MoveNext()
                                    Wend
                                    If (Recordset4.CursorType > 0) Then
                                      Recordset4.MoveFirst
                                    Else
                                      Recordset4.Requery
                                    End If
                                    %>
                                </select></td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right" nowrap="nowrap">Calle:</td>
                            <td>
                                <input type="text" name="calleCliente" value="<%=(calle)%>" size="32" /></td>
                            <td align="right" nowrap="nowrap">Teléfono:</td>
                            <td>
                                <input type="text" name="telefonoCliente" value="<%=(telefono)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right">Colonia:</td>
                            <td>
                                <input type="text" name="colonia" value="<%=(colonia)%>" size="32" /></td>
                            <td align="right" nowrap="nowrap">Correo:</td>
                            <td>
                                <input name="correo" type="text" id="correo" value="<%=(correo)%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right">Código postal:</td>
                            <td>
                                <input type="text" name="codigopostalCliente" value="<%=(postal)%>" size="32" /></td>
                            <td colspan="2" align="center" bgcolor="#66CCFF">Datos Opcionales</td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right" nowrap="nowrap">No Exterior:</td>
                            <td>
                                <input type="text" name="noExterior" value="<%=(extrior)%>" size="32" /></td>
                            <td align="right">Clave:</td>
                            <td>
                                <input name="clave" type="text" id="clave" value="<%=clave%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td align="right" nowrap="nowrap">No Interior:</td>
                            <td>
                                <input type="text" name="noInterior" value="<%=(interiro)%>" size="32" /></td>


                            <td align="right">Referencia:</td>
                            <td>
                                <input type="text" name="referencia" value="<%=(referencia)%>" size="32" /></td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <table border="0" align="center">
                        <tr bgcolor="#66CCFF">
                            <td colspan="4" align="center">Requisitos para la Generación de Facturas Automáticas</td>
                        </tr>
                        <tr>
                            <td align="right" valign="baseline" nowrap="nowrap">Destino:</td>
                            <td valign="baseline">
                                <input name="destino" type="text" id="destino" value="<%=destino%>" size="32" /></td>
                            <td align="right" valign="baseline">Terminos:</td>
                            <td valign="baseline">
                                <input name="terminoPago" type="text" id="terminoPago" value="<%=terminos%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Retención:</td>
                            <td>
                                <label for="retencion"></label>
                                <select name="retencion">
                                    <option value="" <%If retencion = "" then response.write("Selected") end if %>>Seleccionar</option>
                                    <option value="True" <%If retencion = True then response.write("Selected") end if %>>Si</option>
                                    <option value="False" <%If retencion = False then response.write("Selected") end if %>>No</option>
                                </select></td>
                            <td align="right">I.V.A.:</td>
                            <td>
                                <label for="tazaIva"></label>
                                <input name="tazaIva" type="text" id="tazaIva" value="<%=tazaIva%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Método de pago:</td>
                            <td>
                                <label for="metodoPago"></label>
                                <input name="metodoPago" type="text" id="metodoPago" value="<%=metodoPago%>" size="32" /></td>
                            <td align="right">Número de cuenta:</td>
                            <td>
                                <label for="numeroCuenta"></label>
                                <input name="numeroCuenta" type="text" id="numeroCuenta" value="<%=numeroCuenta%>" size="32" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right" valign="baseline">Valor agregado:</td>
                            <td valign="baseline">
                                <label>
                                    <input <%If (valorAgregado = True) Then Response.Write("checked=""checked""") : Response.Write("")%> <%If (Request.Form("valorAgregado") = True) Then Response.Write("checked=""checked""") : Response.Write("")%> name="valorAgregado" type="checkbox" id="valorAgregado" value="1" />
                                </label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td align="right" nowrap="nowrap">Entrega:</td>
                            <td>
                                <textarea name="entrega" cols="45" rows="5" id="entrega"><%=(entrega)%></textarea></td>
                            <td align="right">Observaciones:</td>
                            <td>
                                <textarea name="obsCliente" id="obsCliente" cols="45" rows="5"><%=obsCliente%></textarea></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <input type="submit" value="Modificar" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <p>
                        <input type="hidden" name="MM_update" value="form1" />
                        <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("idCliente").Value %>" />
                    </p>
                </form>
                <form id="form2" name="form2" method="POST" action="<%=MM_editAction%>">
                    <label>
                        <!--<input type="submit" name="button" id="button" value="Eliminar" />-->
                    </label>
                    <input type="hidden" name="MM_delete" value="form2" />
                    <input type="hidden" name="MM_recordId" value="<%= Recordset1.Fields.Item("idCliente").Value %>" />
                </form>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
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

<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
<%
Recordset4.Close()
Set Recordset4 = Nothing
%>
<script language="javascript1.2">
function cambiar()
{
document.form1.MM_update.value="0000"
document.form1.submit()
}
</script>
