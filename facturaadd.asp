<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%Server.ScriptTimeout=50000000%> 
<%
Dim Recordset3__MMColParam
Recordset3__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset3__MMColParam = Session("site_empresa")
End If
    iva= 16
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
dim color'Variable para guardar el color de los renglones'
dim doc'Variable para guardar el tipo de documento
dim entrega'Variable para guardar donde se va entregar
dim terminos'Variable para guardar los terminos de pago
dim obsCliente'Vairable para guardar las observaciones del cliente
dim fecha'Variable para guardar la fecha
dim fembarques'Variable para guardar la fehca de embarques
dim fembarques2'Variable para guardar la fehca de embarques de guardado
dim vendedor

color = cgrid2

'if para validar el tipo de documento
if Request.Form("abreviatura") = "FA" then
doc = " AND factura = 'True'"
elseif Request.Form("abreviatura") = "CRED" then
doc = " AND nCredito = 'True'"
elseif Request.Form("abreviatura") = "CAR" then
doc = " AND nCargo = 'True'"
end if
%>
<%
Dim Recordset1__MMColParam
Recordset1__MMColParam = "1"
If (Request.QueryString("idempresa") <> "") Then 
  Recordset1__MMColParam = Request.QueryString("idempresa")
End If
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
'if para validar que no marque error el datef
if Request.Form("fEmbarque") <> "" then
fEmbarque2 = datef2(Request.Form("fEmbarque"))
end if
%>
<%
If (CStr(Request("MM_insert")) = "form1") Then
  If (Not MM_abortEdit) Then

  Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
  Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
  Recordset1_cmd.CommandText = "SELECT obsFactura FROM dbo.confimenor WHERE idconf = 1 AND refacciones = 'True'" 
  Recordset1_cmd.Prepared = true
  Set Recordset1 = Recordset1_cmd.Execute

  If Request.Form("abreviatura") = "CRED" Then
    observaciones = Request.Form("obsCliente")
    vendedor = ""
  Else
    if NOT Recordset1.EOF Then 
    observaciones = Recordset1.Fields.Item("obsFactura").Value
    else
    observaciones = ""
    end if
    
    vendedor = Request.Form("vendedor")
  End If
      

    ' execute the insert
    Dim MM_editCmd
    estcobranza="Pendiente"
    Set MM_editCmd = Server.CreateObject ("ADODB.Command")
    MM_editCmd.ActiveConnection = MM_Conecta1_STRING
    MM_editCmd.CommandText = "INSERT INTO dbo.factura (tipo_comprobante, serie, forma_pago, moneda, idcliente, obsCliente, embarque, ASN, vendedor, ordenCompra, fEmbarque, VA, terminos, tretencion, idusuario, fechaalta, idempresa, tasa, abreviatura, cambio, iva, subtotal, total, estatus, retencion,NumCtaPago, metodoPago, condicionesDePago, estcobranza, estatusCobranza, envioSatCancelada) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? , ?, ?, 'Pendiente', '"&Request.Form("bajar")&"')" 
    MM_editCmd.Prepared = true
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param1", 201, 1, 255, Request.Form("tipo_comprobante")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param2", 201, 1, 255, Request.Form("serie")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param3", 201, 1, 255, Request.Form("forma_pago2")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param4", 5, 1, -1, MM_IIF(Request.Form("moneda"), Request.Form("moneda"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param5", 5, 1, -1, MM_IIF(Request.Form("idcliente"), Request.Form("idcliente"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param6", 201, 1, 8000, observaciones) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param7", 201, 1, 255, Request.Form("embarque")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param8", 201, 1, 255, Request.Form("ASN")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param9", 201, 1, 255, vendedor) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param10", 201, 1, 255, Request.Form("ordenCompra")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param11", 135, 1, -1, MM_IIF(fEmbarque2, fEmbarque2, null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param12", 201, 1, 255, Request.Form("VA")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param14", 201, 1, 255, Request.Form("terminos")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param15", 5, 1, -1, MM_IIF(Request.Form("retencion"), Request.Form("retencion"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param16", 5, 1, -1, MM_IIF(Request.Form("idusuario"), Request.Form("idusuario"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param17", 135, 1, -1, MM_IIF(datef2(Request.Form("fechaalta")), datef2(Request.Form("fechaalta")), null)) ' adDBTimeStamp
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param18", 5, 1, -1, MM_IIF(Request.Form("idempresa"), Request.Form("idempresa"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param19", 201, 1, 255, Request.Form("tasa")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param20", 201, 1, 255, Request.Form("abreviatura")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param21", 5, 1, -1, MM_IIF(Request.Form("cambio"), Request.Form("cambio"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param22", 5, 1, -1, MM_IIF(Request.Form("iva"), Request.Form("iva"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param23", 5, 1, -1, MM_IIF(Request.Form("subtotal"), Request.Form("subtotal"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param24", 5, 1, -1, MM_IIF(Request.Form("total"), Request.Form("total"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param25", 201, 1, 255, Request.Form("estatus")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param26", 5, 1, -1, MM_IIF(Request.Form("tretencion"), Request.Form("tretencion"), null)) ' adDouble
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param27", 201, 1, 255, Request.Form("NumCtaPago")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param28", 201, 1, 255, Request.Form("metodoPago")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param30", 201, 1, 255, Request.Form("condicionesDePago")) ' adLongVarChar
    MM_editCmd.Parameters.Append MM_editCmd.CreateParameter("param35", 201, 1, 255, estcobranza) ' adLongVarChar
	MM_editCmd.Execute
    MM_editCmd.ActiveConnection.Close
  End If
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
Dim Recordset2__MMColParam
Recordset2__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset2__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.sucursales AS empresas, dbo.ciudades AS ciudad, dbo.estados AS estado WHERE empresas.id = ? AND ciudad.id= empresas.idCiudad AND estado.id = empresas.idEstado " 
Recordset2_cmd.Prepared = true
Recordset2_cmd.Parameters.Append Recordset2_cmd.CreateParameter("param1", 5, 1, -1, Recordset2__MMColParam) ' adDouble
  '  response.Write Recordset2_cmd.CommandText & " " &Recordset2__MMColParam

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
%>
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.documento WHERE idempresa = ? AND eliminado is null  ORDER BY descripcion ASC" 
Recordset3_cmd.Prepared = true
Recordset3_cmd.Parameters.Append Recordset3_cmd.CreateParameter("param1", 5, 1, -1, Recordset3__MMColParam) ' adDouble

Set Recordset3 = Recordset3_cmd.Execute
Recordset3_numRows = 0
%>
<%
Dim Recordset4__MMColParam
Recordset4__MMColParam = "1"
If (Session("site_empresa") <> "") Then 
  Recordset4__MMColParam = Session("site_empresa")
End If
%>
<%
Dim Recordset4
Dim Recordset4_cmd
Dim Recordset4_numRows

Set Recordset4_cmd = Server.CreateObject ("ADODB.Command")
Recordset4_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset4_cmd.CommandText = "SELECT * FROM dbo.folios WHERE idEmpresa = ?"&doc
Recordset4_cmd.Prepared = true
Recordset4_cmd.Parameters.Append Recordset4_cmd.CreateParameter("param1", 5, 1, -1, Recordset4__MMColParam) ' adDouble

Set Recordset4 = Recordset4_cmd.Execute
Recordset4_numRows = 0
%>
<%
Dim Recordset5
Dim Recordset5_cmd
Dim Recordset5_numRows

Set Recordset5_cmd = Server.CreateObject ("ADODB.Command")
Recordset5_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset5_cmd.CommandText = "SELECT * FROM dbo.formPago ORDER BY descripcion ASC" 
Recordset5_cmd.Prepared = true

Set Recordset5 = Recordset5_cmd.Execute
Recordset5_numRows = 0
%>
<%
Dim Recordset6__MMColParam
Recordset6__MMColParam = "1"
If (Request.Form("idCliente") <> "") Then 
  Recordset6__MMColParam = Request.Form("idCliente")
End If
%>
<%
Dim Recordset6
Dim Recordset6_cmd
Dim Recordset6_numRows

Set Recordset6_cmd = Server.CreateObject ("ADODB.Command")
Recordset6_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset6_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idCliente = ?" 
Recordset6_cmd.Prepared = true
Recordset6_cmd.Parameters.Append Recordset6_cmd.CreateParameter("param1", 5, 1, -1, Recordset6__MMColParam) ' adDouble

Set Recordset6 = Recordset6_cmd.Execute
Recordset6_numRows = 0

Dim Recordset7
Dim Recordset7_cmd
Dim Recordset7_numRows

Set Recordset7_cmd = Server.CreateObject ("ADODB.Command")
Recordset7_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset7_cmd.CommandText = "SELECT * FROM dbo.moneda ORDER BY descripcion ASC" 
Recordset7_cmd.Prepared = true

Set Recordset7 = Recordset7_cmd.Execute
Recordset7_numRows = 0

Dim Recordset8
Dim Recordset8_cmd
Dim Recordset8_numRows

Set Recordset8_cmd = Server.CreateObject ("ADODB.Command")
Recordset8_cmd.ActiveConnection = MM_Conecta1_STRING
'Recordset8_cmd.CommandText = "SELECT DISTINCT(vendedor) FROM dbo.factura WHERE vendedor IS NOT NULL AND vendedor <> '' AND abreviatura = 'FA' AND ASN <> '' ORDER BY vendedor ASC" 
Recordset8_cmd.CommandText = "SELECT nombre AS vendedor FROM usuarios JOIN perfiles ON idPerfil = perfiles.id WHERE (perfiles.descripcion = 'vendedor' or perfiles.descripcion = 'VENDEDOR') AND idSucursal = "&Request.Cookies("login")("idSucursal")&" ORDER BY vendedor ASC" 
'Response.Write(Recordset8_cmd.CommandText)
Recordset8_cmd.Prepared = true

Set Recordset8 = Recordset8_cmd.Execute

'if para validar si esta seleccionado un cliente
if Request.Form("idcliente") <> "" then
entrega = Recordset6.Fields.Item("entrega").Value
terminos = ""'Recordset6.Fields.Item("terminoPago").Value
obsCliente = Recordset6.Fields.Item("obsCliente").Value
end if

'if para validar la insercion'
if Request.Form("insert") = "Form1" then'insert'S
'---------------Recordset para consultar la factura-------------------------------------'
Set RSFac_cmd = Server.CreateObject ("ADODB.Command")
RSFac_cmd.ActiveConnection = MM_Conecta1_STRING
RSFac_cmd.CommandText = "SELECT * FROM dbo.factura ORDER BY idfactura DESC" 
RSFac_cmd.Prepared = true

Set RSFac = RSFac_cmd.Execute

'if para validae que hay datos'
if NOT RSFac.EOF then
  If RSFac.Fields.Item("envioSatCancelada").Value = "SI" Then
    Response.Redirect("selectFactura.asp?idfactura="&RSFac.Fields.Item("idfactura").Value&"&idcliente="&Request.Form("idcliente"))
  Else
    Response.Redirect("detfacturaadd.asp?idfactura="&RSFac.Fields.Item("idfactura").Value&"&idcliente="&Request.Form("idcliente"))
  End If
end if
end if'insert'

Dim RecordsetFormSAT
Dim RecordsetFormSAT_cmd
Dim RecordsetFormSAT_numRows

Set RecordsetFormSAT_cmd = Server.CreateObject ("ADODB.Command")
RecordsetFormSAT_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetFormSAT_cmd.CommandText = "SELECT * FROM dbo.formaPagoSat ORDER BY descripcion ASC" 
RecordsetFormSAT_cmd.Prepared = true

Set RecordsetFormSAT = RecordsetFormSAT_cmd.Execute
RecordsetFormSAT_numRows = 0

%>
<script language="javascript1.2">
var abreviatura = new Array()//array para guardar las abrebiaturas
var iddocumento = new Array()//array para guardar los id de los odcumetnos
var n//variable contador

var tcambio = new Array()//array para guardar los cambios
var idmoneda = new Array()//array para guardar los id de las monedas
var a//variable contador

a = 0
n = 0
</script>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

    <!-- InstanceBegin template="/Templates/plantillacfd.dwt.asp" codeOutsideHTMLIsLocked="false" -->
<head>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css">
    
	<script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <script language="JavaScript" src="jsF/overlib_mini.js"></script>
    <style type="text/css">
<!--
body {
	background-color: #CCC;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	font-weight: bold;
	font-size: 12px;
}
.Estilo1 {color: #000000}
.Estilo7 {font-size: 9px}
.Estilo11 {
	font-weight: bold;
	color: #000000;
	font-size: 16px;
}
.template1 {
	font-size: 12px;
}
.inicio {
	background-position: center;
	height: auto;
	width:800px;
}
.div1 {
	background-color: #3CF;
	height: 100%;
	width: 100%;
	border-top-style: groove;
	border-right-style: groove;
	border-bottom-style: groove;
	border-left-style: groove;
}
.perfil {
	font-size: 10px;
}
.perfil {
	color: #FFF;
}
.encabezado {
	font-size: 12px;
}

/*Modal*/
.modalDialog {
	position: fixed;
	font-family: Arial, Helvetica, sans-serif;
	top: 0;
	right: 0;
	bottom: 0;
	left: 0;
	background: rgba(0,0,0,0.8);
	z-index: 99999;
	opacity:0;
	-webkit-transition: opacity 400ms ease-in;
	-moz-transition: opacity 400ms ease-in;
	transition: opacity 400ms ease-in;
	pointer-events: none;
}
.modalDialog:target {
	opacity:1;
	pointer-events: auto;
}
.modalDialog > div {
	width: 600px;
    height: 500px;
	position: relative;
	margin: 10% auto;
	padding: 5px 20px 13px 20px;
	border-radius: 10px;
	background: #fff;
	background: -moz-linear-gradient(#fff, #999);
	background: -webkit-linear-gradient(#fff, #999);
	background: -o-linear-gradient(#fff, #999);
    -webkit-transition: opacity 400ms ease-in;
    -moz-transition: opacity 400ms ease-in;
    transition: opacity 400ms ease-in;
}
.close {
	background: #606061;
	color: #FFFFFF;
	line-height: 25px;
	position: absolute;
	right: -12px;
	text-align: center;
	top: -10px;
	width: 24px;
	text-decoration: none;
	font-weight: bold;
	-webkit-border-radius: 12px;
	-moz-border-radius: 12px;
	border-radius: 100%;
	-moz-box-shadow: 1px 1px 3px #000;
	-webkit-box-shadow: 1px 1px 3px #000;
	box-shadow: 1px 1px 3px #000;
}
.close:hover { background: #00d9ff; }
    .auto-style1 {
        width: 72%;
        height: 23px;
    }
    .auto-style2 {
        height: 23px;
    }
-->
</style>	
<style type="text/css">
        <!--
        body {
            background-color: #CCC;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            font-weight: bold;
            font-size: 12px;
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
            font-size: 16px;
        }

        .template1 {
            font-size: 12px;
        }

        .inicio {
            background-position: center;
            height: auto;
            width: 800px;
        }

        .div1 {
            background-color: #3CF;
            height: 100%;
            width: 100%;
            border-top-style: groove;
            border-right-style: groove;
            border-bottom-style: groove;
            border-left-style: groove;
        }

        .perfil {
            font-size: 10px;
        }

        .perfil {
            color: #FFF;
        }

        .encabezado {
            font-size: 12px;
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 30%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-header {
            padding: 2px 16px;
            background-color: #337ab7;
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
        }

        .modal-footer {
            padding: 2px 16px;
            color: white;
        }

        #myBtn {
            background-color: #337ab7;
            border: none;
            color: white;
            padding: 10px 30px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 4px 2px;
            cursor: pointer;
        }

        #myBtn2 {
            background-color:  red;
            border: none;
            color: white;
            padding: 10px 30px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 4px 2px;
            cursor: pointer;
        }

        -->
    </style>
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
       
<script language="javascript1.2">
function perfil()
{
		window.open("perfil.asp","Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=508, height=300, top=85, left=140")
}

function ayuda()
{
	pagina='<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
	
	
	window.open("ayuda.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
function logistica(pagina)
{
		
		pagina='<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
		
		window.open("logistica.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
     var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (500 / 2);
        posicion_y = (screen.height / 2) - (500 / 2);

        function pagar(id, idCliente) {
            open("formaPago.asp?id=" + id + "&idCliente=" + idCliente, '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=600, height=450')
        }

        function clientes() {
            open("clienteFactura.asp", '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650')
        }

        function borrar() {
            document.getElementById("cliente").innerHTML = ""
            document.getElementById("clientes").value = ""
        }
</script>




<script language="javascript1.2">
function perfil()
{
		window.open("perfil.asp","Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=508, height=300, top=85, left=140")
}

function ayuda(pagina)
{
		window.open("ayuda.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
function logistica(pagina)
{
  '<%=mid(Request.ServerVariables("URL"),Instrrev(Request.ServerVariables("URL"),"/")+1)%>'
	
		window.open("logistica.asp?pagina="+pagina,"Perfil","toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, width=908, height=500, top=85, left=140")
}
</script>
<!-- InstanceBeginEditable name="head" -->
        <script>
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
    </script>
<!-- InstanceEndEditable -->

</head>

<body style="background-color: white">
   
<table width="99.9%"  height="89%"   border="0" align="center" cellpadding="1" cellspacing="1" valign="top" >
     
  <tr>
    <td>
      
     <div   style="overflow:auto; width: 100%; height: 100%; scrollbar-arrow-color : #B2B2B2; scrollbar-face-color : #3FA5DC;
 scrollbar-track-color:#B6D0F3;"  >
      <!-- InstanceBeginEditable name="EditRegion1" --><p>&nbsp;</p>
     <form id="form1" name="form1" method="POST" action="<%=MM_editAction%>" onSubmit="return val(this)">
        <table width="80%" border="0" align="center">
          <tr>
            <td><table width="43%" border="0" align="center">
              <tr align="center">
                <td><strong>Seleccione los datos necesario   para crear la factura</strong></td>
              </tr>
              <tr>
                <td align="right">&nbsp;</td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td><table width="87%" border="1" align="center" cellspacing="1">
              <tr>
                <th colspan="4" align="center"  bgcolor="<%=ctabla%>" scope="col">Emisor</th>
              </tr>
              <tr>
                <td>Nombre</td>
                <td colspan="3"><%=(Recordset2.Fields.Item("razon").Value)%></td>
                
              </tr>
              <tr>
                <td>R.F.C.</td>
                <td><%=(Recordset2.Fields.Item("rfc").Value)%></td>
                <td>Pais</td>
                <td>México</td>
              </tr>
              <tr>
                <td>Calle</td>
                <td><%=(Recordset2.Fields.Item("calle").Value)%></td>
                <td>Estado</td>
                <td><%=(Recordset2.Fields.Item("estado").Value)%></td>
              </tr>
              <tr>
                <td>Código Postal</td>
                <td><%=(Recordset2.Fields.Item("codigoPostal").Value)%></td>
                <td>Ciudad</td>
                <td><%=(Recordset2.Fields.Item("ciudad").Value)%></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><table width="100%" border="0" align="center">
              <tr>
                <td width="16%" align="right">Tipo de Documento: </td>
                <td width="36%" bgcolor="#CBDFF5"><select name="tipo_comprobante" id="tipo_comprobante" onChange="iniciales()">
                  <option value="" selected="selected" <%If (Not isNull(Request.Form("tipo_comprobante"))) Then If ("" = CStr(Request.Form("tipo_comprobante"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                  <%While (NOT Recordset3.EOF)%>
                  <script language="JavaScript1.2" type="text/javascript">
                    //Se llenan los array				
                    abreviatura[n] = '<%=(Recordset3.Fields.Item("abrebiatura").Value)%>'
                    iddocumento[n] = '<%=(Recordset3.Fields.Item("iddocumento").Value)%>'
                    n = n + 1
                  </script>
                  <option value="<%=(Recordset3.Fields.Item("iddocumento").Value)%>" <%If (Not isNull(Request.Form("tipo_comprobante"))) Then If (CStr(Recordset3.Fields.Item("iddocumento").Value) = CStr(Request.Form("tipo_comprobante"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset3.Fields.Item("descripcion").Value)%></option>
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
                <td width="12%">Metodo de Pago:</td>
                <td width="36%" bgcolor="#CBDFF5"><select name="forma_pago2" id="forma_pago2">
                  <option value="" <%If (Not isNull(Request.Form("forma_pago2"))) Then If ("" = CStr(Request.Form("forma_pago2"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                  <%
                While (NOT Recordset5.EOF)
                %>
                  <option value="<%=(Recordset5.Fields.Item("idpago").Value)%>" <%If (Not isNull(Request.Form("forma_pago2"))) Then If (CStr(Recordset5.Fields.Item("idpago").Value) = CStr(Request.Form("forma_pago2"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset5.Fields.Item("forma_pago").Value)%> - <%=(Recordset5.Fields.Item("descripcion").Value)%></option>
                  <%
                  Recordset5.MoveNext()
                Wend
                If (Recordset5.CursorType > 0) Then
                  Recordset5.MoveFirst
                Else
                  Recordset5.Requery
                End If
                %>
                </select></td>
              </tr>
              <tr>
                <td align="right">Series: </td>
                <td bgcolor="#CBDFF5"><select name="serie" id="serie">
                  <option value="">Seleccionar</option>
                  <%
                    While (NOT Recordset4.EOF)
                    %>
                  <option value="<%=(Recordset4.Fields.Item("Serie").Value)%>" <%If (Not isNull(Request.Form("serie"))) Then If (CStr(Recordset4.Fields.Item("Serie").Value) = CStr(Request.Form("serie"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset4.Fields.Item("Serie").Value)%></option>
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
                <td>Moneda: </td>
                <td bgcolor="#CBDFF5"><select name="moneda" id="moneda" onChange="fcambio()">
                  <option value="" selected="selected" <%If (Not isNull(Request.Form("moneda"))) Then If ("" = CStr(Request.Form("moneda"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                  <%While (NOT Recordset7.EOF)%>
                  <script language="JavaScript1.2" type="text/javascript">
                    //Se llenan los array				
                    tcambio[a] = '<%=(Recordset7.Fields.Item("tcambio").Value)%>'
                    idmoneda[a] = '<%=(Recordset7.Fields.Item("idmd").Value)%>'
                    a = a + 1
                  </script>
                  <option value="<%=(Recordset7.Fields.Item("idmd").Value)%>" <%If (Not isNull(Request.Form("moneda"))) Then If (CStr(Recordset7.Fields.Item("idmd").Value) = CStr(Request.Form("moneda"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset7.Fields.Item("descripcion").Value)%></option>
                  <%
                      Recordset7.MoveNext()
                    Wend
                    If (Recordset7.CursorType > 0) Then
                      Recordset7.MoveFirst
                    Else
                      Recordset7.Requery
                    End If
                    %>
                </select></td>
              </tr>
              <tr>
                <td align="right">Cliente:</td>
                <td colspan="3" align="left"><label>
                  <select name="idcliente" id="idcliente" onChange="iniciales()">
                    <option value="" <%If (Not isNull(Request.Form("idcliente"))) Then If ("" = CStr(Request.Form("idcliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                    <%
                    While (NOT Recordset1.EOF)
                    %>
                    <option value="<%=(Recordset1.Fields.Item("idCliente").Value)%>" <%If (Not isNull(Request.Form("idcliente"))) Then If (CStr(Recordset1.Fields.Item("idCliente").Value) = CStr(Request.Form("idcliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(Recordset1.Fields.Item("nombreCliente").Value)%></option>
                    <%
                      Recordset1.MoveNext()
                    Wend
                    If (Recordset1.CursorType > 0) Then
                      Recordset1.MoveFirst
                    Else
                      Recordset1.Requery
                    End If
                    %>
                    </select>
                </label></td>
                </tr>
              <tr valign="top">
                <td align="right">Observaciones del cliente:</td>
                <td><label>
                  <textarea name="obsCliente" id="obsCliente" cols="45" rows="5"><%=obsCliente%></textarea>
                </label></td>
                <td>Bill to:</td>
                <td><textarea name="embarque" id="embarque" cols="45" rows="5"><%=(entrega)%></textarea></td>
              </tr>
              <%If Request.Form("abreviatura") = "CRED" Then%>
              <tr>
                <td align="right">Afectar Inventario:</td>
                <td>
                  <input type="radio" name="bajar" id="bajar" value="SI">SI
                  <input type="radio" name="bajar" id="bajar" value="NO">NO</td>
                <td></td>
                <td></td>
              </tr>
              <%End If%>
              <tr>
                <td align="right">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                  <td colspan="5" align="center" bgcolor="#66CCFF">Datos de factura</td>
                </tr>
              <tr>
                <td align="right">Forma de pago:</td>
                <td bgcolor="#CBDFF5"><label for="metodoPago"></label>
                    <select name="metodoPago" id="metodoPago" >
                        <option value="" <%If (Not isNull(Request.Form("idcliente"))) Then If ("" = CStr(Request.Form("idcliente"))) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>
                        <%
                        While (NOT RecordsetFormSAT.EOF)
                        %>
                        <option value="<%=(RecordsetFormSAT.Fields.Item("codigo").Value)%>" <%If (Not isNull(Request.Form("metodoPago"))) Then If (CStr(RecordsetFormSAT.Fields.Item("codigo").Value) = CStr(Request.Form("metodoPago"))) Then Response.Write("selected=""selected""") : Response.Write("")%> ><%=(RecordsetFormSAT.Fields.Item("codigo").Value)%> - <%=(RecordsetFormSAT.Fields.Item("descripcion").Value)%></option>
                        <%
                          RecordsetFormSAT.MoveNext()
                        Wend
                        If (RecordsetFormSAT.CursorType > 0) Then
                          RecordsetFormSAT.MoveFirst
                        Else
                          RecordsetFormSAT.Requery
                        End If
                        %>
                    </select>
                </td>
                <td align="right">Numero de cuenta:</td>
                <td bgcolor="#CBDFF5"><label for="NumCtaPago"></label>
                  <input name="NumCtaPago" type="text" id="NumCtaPago" value="<%=NumCtaPago%>" size="32" /></td>
              </tr>
              <tr>
                <td align="right">Condiciones de pago:</td>
                <td><label for="condicionesDePago"></label>
                  <input name="condicionesDePago" type="text" id="condicionesDePago" value="<%=condicionesDePago%>" size="32" /></td>
                <td align="right">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td><table width="100%"  align="center">
              <tr bgcolor="#CBDFF5">
                <td align="right">ASN:</td>
                <td><input name="ASN" type="text" id="ASN" value="<%=Request.Form("ASN")%>" size="10" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr align="center" bgcolor="<%=ctabla%>">
                <td>Vendedor</td>
                <td>Orden de Compra</td>
                <td>Fecha de Embarque</td>
                <td>V/A</td>
                <td>Terminos</td>
                <td width="96">Retencion</td>
              </tr>
              <tr>
                <td align="center">
                  <select name="vendedor" id="vendedor" <%If Request.Form("abreviatura") = "CRED" Then Response.Write("disabled='true'") End If%>>
                    <option value="">Seleccionar</option>
                    <%While(Not Recordset8.EOF)%>
                    <option value="<%=Recordset8.Fields.Item("vendedor").Value%>"><%=Recordset8.Fields.Item("vendedor").Value%></option>
                    <%  Recordset8.MoveNext
                    Wend%>
                  </select>
                </td>
                <td align="center">
                 <label>
                  <input name="ordenCompra" type="text" id="ordenCompra" value="<%=Request.Form("ordenCompra")%>" size="12" />
                </label></td>
                <td bgcolor="#CBDFF5" align="center"><label>
                    <%
                        if  Request.Form("fEmbarque") <> "" then                        
                            embarque = Request.Form("fEmbarque")
                        else
                            embarque = DATE
                        end if 

                        %>
                  <input name="fEmbarque" type="text" id="fEmbarque" value="<%=embarque%>" />
                  <a href="javascript:show_calendar('form1.fEmbarque');" onMouseOver="window.status='Elige fecha'; overlib('Pulsa para elegir fecha del mes actual en el calendario emergente.'); return true;" onMouseOut="window.status=''; nd(); return true;"> <img src= "imagenes/cal.gif" width="24" height="22" border="0" /></a></label></td>
                <td align="center"><label>
                  <input name="VA2" type="hidden" id="VA2" value="<%=Request.Form("VA")%>" />
                  <input name="VA" type="text" id="VA" value="<%=Request.Form("VA")%>" />
                </label></td>
                <td align="center"><label>
                  <input name="terminos" type="text" id="terminos" value="<%=terminos%>" size="12" />
                </label></td>
                <td align="center"><input name="retencion" type="text" id="retencion" value="<%=Request.Form("retencion")%>" size="12"  onkeypress="return SoloNumerosDec(event);"/></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td align="center">dd/mm/yyyy</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><input type="submit" name="button" id="button" value="Guardar" /></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <div id="overDiv" style="position:absolute; visibility:hidden; z-index:10;"></div>
        <p>
          <input name="insert" type="hidden" id="insert" value="Form1" />
          <input name="idusuario" type="hidden" id="idusuario" value="<%=Request.Cookies("login")("id")%>" />
          <input name="fechaalta" type="hidden" id="fechaalta" value="<%=datef2(Date())&" "&FormatDateTime(now, 4)%>" />
          <input name="idempresa" type="hidden" id="idempresa" value="<%=Request.QueryString("idempresa")%>" />
          <input name="tasa" type="hidden" id="tasa" value="<%=iva%>" />
          <input name="abreviatura" type="hidden" id="abreviatura" value="<%=Request.Form("abreviatura")%>" />
          <input name="cambio" type="hidden" id="cambio" value="<%=Request.Form("cambio")%>" />
          <input type="hidden" name="MM_insert" value="form1" />
          <input name="iva" type="hidden" id="iva" value="0" />
          <input name="subtotal" type="hidden" id="subtotal" value="0" />
          <input name="total" type="hidden" id="total" value="0" />
          <input name="estatus" type="hidden" id="estatus" value="Pendiente" />
          <input name="tretencion" type="hidden" id="tretencion" value="0" />
          <input name="estcobranza" type="hidden" id="estcobranza" value="Pendiente" />
          <input name="prueba" type="hidden" id="prueba" value="">
        </p>
     </form>
         </div>
      <p>&nbsp;</p>
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
<%
Recordset2.Close()
Set Recordset2 = Nothing
%>
<%
Recordset3.Close()
Set Recordset3 = Nothing
%>
<%
Recordset4.Close()
Set Recordset4 = Nothing
%>
<%
Recordset5.Close()
Set Recordset5 = Nothing
%>
<%
Recordset6.Close()
Set Recordset6 = Nothing
%>
<%
Recordset7.Close()
Set Recordset7 = Nothing
%>
<script language="javascript1.2">
function val(f)
{
  var message = "Falta:.\n\n";
  if (f.idcliente.value == "")
  {
    message = message + "- Cliente.\n\n";
    alert (message)
    return false;
  }
  if (f.moneda.value == "Seleccionar")
  {
    message = message + "- Cliente.\n\n";
    alert (message)
    return false;
  }
  if (f.tipo_comprobante.value == "")
  {
    message = message + "- Tipo de Documento.\n\n";
    alert (message)
    return false;
  }
  if (f.serie.value == "")
  {
    message = message + "- Serie.\n\n";
    alert (message)
    return false;
  }
  if (f.forma_pago2.value == "")
  {
    message = message + "- Metodo de Pago.\n\n";
    alert (message)
    return false;
  }

  if (f.moneda.value == "")
  {
    message = message + "- Moneda.\n\n";
    alert (message)
    return false;
  }

  if (f.metodoPago.value == "")
  {
    message = message + "- forma de Pago.\n\n";
    alert (message)
    return false;
  }

  if (f.fEmbarque.value == "")
  {
    message = message + "- Fecha de Embarque.\n\n";
    alert (message)
    return false;
  }

  var combo = document.getElementById("tipo_comprobante");
  var selected = combo.options[combo.selectedIndex].text;
  var cuantos = 0;

  if(selected == "Nota de Credito")
  {
    var bajar = document.getElementsByName("bajar")

    for(var i = 0; i < bajar.length; i++)
    {
      if(bajar[i].checked == false)
      {
        cuantos++
      }
    }
    if(cuantos == 2)
    {
      message = message + "Afectar el Inventario\n\n"
      alert(message)
      return false;
    }
  }

  if(selected == "Factura")
  {
    if(f.vendedor.value == "")
    {
      message = message + "El Vendedor\n\n"
      alert(message)
      return false; 
    }
  }
  else
  {
    return true
  }
}

function iniciales()
{
  var indice = document.form1.tipo_comprobante.selectedIndex //variable para guardar el indice de la opcion seleccionada del menu tipo de cocumento
  var valor = document.form1.tipo_comprobante.options[indice].value //variable para guardar el valor de la opcion seleccionada del menu tipo de documento
  for (i=0;i<=n;i++)
  {
  	if (iddocumento[i] == valor)
  	{
  		document.form1.abreviatura.value = abreviatura[i]
    }
  }
  document.form1.MM_insert.value = "0000"
  document.form1.insert.value = "0000"
  document.form1.submit()
}

function fcambio()
{
  var indice = document.form1.moneda.selectedIndex //variable para guardar el indice de la opcion seleccionada del menu tipo de cocumento
  var valor = document.form1.moneda.options[indice].value //variable para guardar el valor de la opcion seleccionada del menu tipo de cocumento
  for (i=0;i<=n;i++)
  {
  	if (idmoneda[i] == valor)
  	{
  		document.form1.cambio.value =  tcambio[i]
  	}
  }
}

</script>
