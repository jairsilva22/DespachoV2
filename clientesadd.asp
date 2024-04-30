<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="stylo2.asp"-->
<!--#include file="config.asp"-->
<!--#include file="checklogin.asp"-->
<%
dim pais'Variable para guardar el pasi para filtrar los estados
dim estado'Variable para guardar el esta para filtrar las ciudades
estado = 0
pais = 0
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
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then
    ' execute the insert
    Dim MM_editCmd

    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.clientesFacturacion (nombreCliente, rfcCliente, estadoCliente, telefonoCliente, ciudadCliente, correo, colonia, calleCliente, codigopostalCliente, noExterior, referencia, noInterior, terminoPago, entrega, obsCliente, idusuario, fechaAlta, idempresa, nombreEmpresa, clave, destino, valorAgregado, metodoPago, numeroCuenta, retencion, tazaIva) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '"&Request.Form("retencion")&"', ?)" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 202, 1, 225, Request.Form("nombreCliente")) ' adVarWChar
    'MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 202, 1, 225, Request.Form("paisCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 202, 1, 225, Request.Form("rfcCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 202, 1, 225, Request.Form("estadoCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 202, 1, 225, Request.Form("telefonoCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 202, 1, 225, Request.Form("ciudadCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 202, 1, 225, Request.Form("correo")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 201, 1, 255, Request.Form("colonia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 202, 1, 225, Request.Form("calleCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 202, 1, 225, Request.Form("codigopostalCliente")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 201, 1, 255, Request.Form("noExterior")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 201, 1, 255, Request.Form("referencia")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("noInterior")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param13", 202, 1, 225, Request.Form("terminoPago")) ' adVarWChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 201, 1, -1, Request.Form("entrega")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 201, 1, 8000, Request.Form("obsCliente")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 5, 1, -1, MM_IIF(Request.Form("idusuario"), Request.Form("idusuario"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 135, 1, -1, MM_IIF(Request.Form("fechaAlta"), Request.Form("fechaAlta"), null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param18", 5, 1, -1, MM_IIF(Request.Form("idempresa"), Request.Form("idempresa"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param19", 201, 1, 8000, Request.Form("nombreFiscal")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param20", 201, 1, 8000, Request.Form("clave")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param21", 201, 1, 8000, Request.Form("destino")) ' adLongVarChar			
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param22", 5, 1, -1, MM_IIF(Request.Form("valorAgregado"), 1, 0)) ' adDouble	
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param23", 201, 1, 225, Request.Form("metodoPago")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param25", 201, 1, 225, Request.Form("numeroCuenta")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param25", 5, 1, -1, MM_IIF(Request.Form("tazaIva"), Request.Form("tazaIva"), null)) ' adLongVarChar			
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
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset1__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idempresa = ? ORDER BY nombreCliente ASC" 
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
'Dim Recordset2
'Dim Recordset2_cmd
'Dim Recordset2_numRows

'Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
'Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset2_cmd.CommandText = "SELECT * FROM dbo.pais ORDER BY pais ASC" 
'Recordset2_cmd.Prepared = true

'Set Recordset2 = Recordset2_cmd.Execute
'Recordset2_numRows = 0
%>
<%
'if NOT Recordset2.EOF then
'pais = Recordset2.Fields.Item("idpais").Value
'end if
%>
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = pais
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
'Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
if NOT Recordset3.EOF then
estado = (Recordset3.Fields.Item("id").Value)
end if
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = estado
If (Request.Form("estadoCliente") <> "") Then 
  Recordset4__MMColParam = Request.Form("estadoCliente")
End If
%>
<%
Dim Recordset4__MMColParam2
Recordset4__MMColParam2 = pais
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

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="JavaScript" src="jsF/overlib_mini.js"></script>

<style type="text/css">
    <!--
    yyyyy {
        color: #FFF;
    }

    yy {
        color: #FFF;
    }
    -->
</style>
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
  var nombreFiscal = document.getElementById('nombreFiscal').value;
  if(nombreFiscal == null || nombreFiscal.length == 0 || /^\s+$/.test(nombreFiscal)){
      alert('ERROR: El campo nombre cliente no debe ir vacío ');
      return false;
    }

    var nombreCliente = document.getElementById('nombreCliente').value;
    if(nombreCliente == null || nombreCliente.length == 0 || /^\s+$/.test(nombreCliente)){
      alert('ERROR: El campo nombre fiscal no debe ir vacío ');
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
                    <table width="902" align="center">
                        <tr valign="baseline">
                            <td colspan="4" align="center" nowrap="nowrap" bgcolor="#66CCFF">Alta de Clientes (Información General)</td>
                        </tr>
                         <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                        <tr valign="baseline">
                            <td width="137" align="right" nowrap="nowrap">Nombre del cliente:</td>
                            <td width="271">
                                <input name="nombreFiscal" type="text" id="nombreFiscal" value="<%=Request.Form("nombreFiscal")%>" size="32" /></td>
                           
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Nombre fiscal:</td>
                            <td>
                                <input type="text" name="nombreCliente" id="nombreCliente" value="<%=Request.Form("nombreCliente")%>" size="32" /></td>
                            <td align="right">Estado:</td>
                            <td>
                                <select name="estadoCliente" onchange="cambiar()">
                                    <option value="" <%If (Not isNull(Request.Form("estadoCliente"))) Then If ("" = CStr(Request.Form("estadoCliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                    <%
While (NOT Recordset3.EOF)
                                    %>
                                    <option value="<%=(Recordset3.Fields.Item("id").Value)%>" <%If (Not isNull(Request.Form("estadoCliente"))) Then If (CStr(Recordset3.Fields.Item("id").Value) = CStr(Request.Form("estadoCliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset3.Fields.Item("estado").Value)%></option>
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
                            <td nowrap="nowrap" align="right">RFC:</td>
                            <td>
                                <input type="text" name="rfcCliente" id="rfcCliente" value="<%=Request.Form("rfcCliente")%>" size="32" /></td>
                            <td align="right">Ciudad:</td>
                            <td>
                                <select name="ciudadCliente" onchange="cambiar()">
                                    <option value="" <%If (Not isNull(Request.Form("ciudadCliente"))) Then If ("" = CStr(Request.Form("ciudadCliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                                    <%
While (NOT Recordset4.EOF)
                                    %>
                                    <option value="<%=(Recordset4.Fields.Item("id").Value)%>" <%If (Not isNull(Request.Form("ciudadCliente"))) Then If (CStr(Recordset4.Fields.Item("id").Value) = CStr(Request.Form("ciudadCliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>><%=(Recordset4.Fields.Item("ciudad").Value)%></option>
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
                            <td nowrap="nowrap" align="right">Calle:</td>
                            <td>
                                <input type="text" name="calleCliente" value="<%=Request.Form("calleCliente")%>" size="32" /></td>
                            <td align="right">Telefono:</td>
                            <td>
                                <input type="text" name="telefonoCliente" value="<%=Request.Form("telefonoCliente")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Colonia:</td>
                            <td>
                                <input type="text" name="colonia" value="<%=Request.Form("colonia")%>" size="32" /></td>
                            <td align="right">Correo</td>
                            <td>
                                <input name="correo" type="text" id="correo" value="<%=Request.Form("correo")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Código postal:</td>
                            <td>
                                <input type="text" name="codigopostalCliente" value="<%=Request.Form("codigopostalCliente")%>" size="32" /></td>
                            <td colspan="2" align="center" bgcolor="#66CCFF">Datos Opcionales</td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">No Exterior:</td>
                            <td>
                                <input type="text" name="noExterior" value="<%=Request.Form("noExterior")%>" size="32" /></td>
                            <td align="right">Clave:</td>
                            <td>
                                <input name="clave" type="text" id="clave" value="<%=Request.Form("clave")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">No Interior:</td>
                            <td>
                                <input type="text" name="noInterior" value="<%=Request.Form("noInterior")%>" size="32" /></td>
                            <td align="right">Referencia:</td>
                            <td>
                                <input type="text" name="referencia" value="<%=Request.Form("referencia")%>" size="32" /></td>
                        </tr>


                    </table>
                    <p>&nbsp;</p>
                    <table width="900" border="0" align="center">
                        <tr valign="top">
                            <td colspan="4" align="center" nowrap="nowrap" bgcolor="#66CCFF">Requisitos para la Generación de Facturas Automáticas</td>
                        </tr>
                        <tr valign="top">
                            <td align="right" nowrap="nowrap">Destino:</td>
                            <td>
                                <input name="destino" type="text" id="destino" value="<%=Request.Form("destino")%>" size="32" /></td>
                            <td align="right">Términos:</td>
                            <td>
                                <input name="terminoPago" type="text" id="terminoPago" value="<%=Request.Form("terminoPago")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Retención:</td>
                            <td>
                                <label for="retencion"></label>
                                <select name="retencion">
                                    <option value="True" <%If retencion = True then response.write("Selected") end if %>>Si</option>
                                    <option value="False" <%If retencion = False then response.write("Selected") end if %>>No</option>
                                </select>
                            </td>
                            <td align="right">I.V.A.:</td>
                            <td>
                                <label for="tazaIva"></label>
                                <input name="tazaIva" type="text" id="tazaIva" value="<%=Request.Form("tazaIva")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">Método de pago:</td>
                            <td>
                                <label for="metodoPago"></label>
                                <input name="metodoPago" type="text" id="metodoPago" value="<%=Request.Form("metodoPago")%>" size="32" /></td>
                            <td align="right">Número de cuenta:</td>
                            <td>
                                <label for="numeroCuenta"></label>
                                <input name="numeroCuenta" type="text" id="numeroCuenta" value="<%=Request.Form("numeroCuenta")%>" size="32" /></td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right">Valor agregado:</td>
                            <td>
                                <label>
                                    <input <%If (Request.Form("valorAgregado") = True) Then Response.Write("checked=""checked""") : Response.Write("")%> name="valorAgregado" type="checkbox" id="valorAgregado" value="1" />
                                </label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td nowrap="nowrap" align="right">Entrega:</td>
                            <td>
                                <textarea name="entrega" cols="45" rows="5"><%=Request.Form("entrega")%></textarea></td>
                            <td align="right">Observaciones:</td>
                            <td>
                                <textarea name="obsCliente" id="obsCliente" cols="45" rows="5"><%=Request.Form("obsCliente")%></textarea></td>
                        </tr>
                        <tr valign="top">
                            <td align="right" nowrap="nowrap">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr valign="baseline">
                            <td nowrap="nowrap" align="right">&nbsp;</td>
                            <td>
                                <input type="submit" value="Guardar" /></td>
                            <td align="right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <input type="hidden" name="idusuario" value="<%=Request.Cookies("login")("id")%>" />
                    <input type="hidden" name="fechaAlta" value="<%=datef2(Date())%>" />
                    <input type="hidden" name="idempresa" value="<%=Request.Cookies("login")("idSucursal")%>" />
                    <input type="hidden" name="MM_insert" value="form1" />
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
document.form1.MM_insert.value="0000"
document.form1.submit()
}
</script>
