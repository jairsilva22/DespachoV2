<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<%
Dim cliente 

'if para validar que el cliente tiene consecutivo
if Request.QueryString("ccliente") <> "" then'ccliente
    consectuvivo = " AND consecutivo = " & Request.QueryString("ccliente") 
end if'ccliente

   ' response.Write(request.QueryString("idf")&"vfgfhgf")
   '
'buscamos el rfc del cliente de control admin
    Dim Recordset1
    Dim Recordset1_cmd
    Dim Recordset1_numRows

    Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
    Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
    Recordset1_cmd.CommandText = "SELECT rfcCliente, c.obsCliente FROM dbo.clientesFacturacion c INNER JOIN factura f ON f.idcliente = c.idCliente WHERE idfactura = "&request.QueryString("id")
    Recordset1_cmd.Prepared = true

    Set Recordset1 = Recordset1_cmd.Execute

    Dim Recordset3__MMColParam
    Recordset3__MMColParam = "0"
    If (Not Recordset1.EOF) Then
      Recordset3__MMColParam = Recordset1.Fields.Item("rfcCliente").Value
      obsClientes = (Recordset1.Fields.Item("obsCliente").Value)
    End If

    Dim Recordset3
    Dim Recordset3_cmd
    Dim Recordset3_numRows

    Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
    Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
    Recordset3_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE rfcCliente = '"&Recordset3__MMColParam&"'"
    Recordset3_cmd.Prepared = true

    Set Recordset3 = Recordset3_cmd.Execute
    Recordset3_numRows = 0

    'if para validar que hay datos
    if NOT Recordset3.EOF then'Recordset3
      metodoPago = Recordset3.Fields.Item("metodoPago").value
      numeroCuenta = Recordset3.Fields.Item("numeroCuenta").value
      cliente = Recordset3.Fields.Item("idCliente").Value
    Else
      cliente = 0
    end if'Recordset3

Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.factura WHERE ASN = '"&request.querystring("numVenta")&"'"
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0

dim bandera'Variable para validar que ya esta ese numero de venta
dim metodoPago'Variable para guardar el metodo de pago
dim numeroCuenta'Variable para guardar el numero de cuenta
dim obsClientes'Variable para guardar las observaciones

'Inicializar variables
bandera = "no"


Set RSnventa_cmd = Server.CreateObject ("ADODB.Command")
RSnventa_cmd.ActiveConnection = MM_Conecta1_STRING
RSnventa_cmd.CommandText = "SELECT * FROM ventaslog WHERE nventa = '"&request.querystring("numVenta")&"'" 
RSnventa_cmd.Prepared = true

Set RSnventa = RSnventa_cmd.Execute

if not RSnventa.eof then
    tipo = RSnventa.fields.item("tipo").value
    doc = "remisión"
else
    doc = "factura"
end if

Set RSFacturas_cmd = Server.CreateObject ("ADODB.Command")
RSFacturas_cmd.ActiveConnection = MM_Conecta1_STRING
RSFacturas_cmd.CommandText = "Select f.* from factura f left join anticipo a on f.idfactura=a.idFacturaAplicada where a.id is null AND idcliente =" & cliente &" AND estatus ='Facturada' AND timbre ='NO'"
RSFacturas_cmd.Prepared = true
   ' response.Write(RSFacturas_cmd.CommandText)
Set RSFacturas = RSFacturas_cmd.Execute
RSFacturas_numRows = 0
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Documento sin título</title>
    <style type="text/css">
        .Nventa {
            font-size: 36px;
        }
    </style>
</head>
<body>
    <% If bandera = "no" Then %>
    <form id="form1" name="form1" method="get" action="AplicarAnt.asp" onsubmit="return val(this)">
        <table width="548" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center">Datos adicionales a la Factura</td>
            </tr>
            <tr>
                <td width="169">&nbsp;</td>
                <td width="379">&nbsp;</td>
            </tr>

            <tr>
                <td align="right">Factura:</td>
                <td>
                    <label>
                        <select name="foliofactura" id="foliofactura">
                            <option value="">Seleccionar</option>
                            <%
                            While (NOT RSFacturas.EOF)
                            %>
                            <option value="<%=(RSFacturas.Fields.Item("idfactura").Value)%>"><%=(RSFacturas.Fields.Item("folio").Value)%></option>
                            <%
                              RSFacturas.MoveNext()
                            Wend
                            %>
                        </select>
                    </label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;<input name="idFactA" type="hidden" id="idFactA" value="<%=request.QueryString("id")%>" /></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>
                    <label>
                        <input name="vendedor" type="hidden" id="vendedor" value="<%=request.querystring("vendedor")%>" />
                        <input name="numVenta" type="hidden" id="numVenta" value="<%=request.querystring("numVenta")%>" />
                        <input name="estacion" type="hidden" id="estacion" value="<%=request.querystring("estacion")%>" />
                        <input name="formaPago" type="hidden" id="formaPago" value="<%=request.querystring("formaPago")%>" />
                        <input name="ccliente" type="hidden" id="ccliente" value="<%=request.querystring("ccliente")%>" />
                        <input name="metodoPago" type="hidden" id="metodoPago" value="<%=metodoPago%>" />
                        <input name="numeroCuenta" type="hidden" id="numeroCuenta" value="<%=numeroCuenta%>" />
                        <input name="cliente" type="hidden" id="cliente" value="<%=cliente%>"/>
                        <input name="clientePTV" type="hidden" id="clientePTV" value="<%=Request.QueryString("cliente")%>"/>
                    </label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <label>
                        <input type="submit" name="button" id="button" value="Enviar" />
                    </label>
                </td>
            </tr>
        </table>
    </form>
    <% Else %>
    <table width="90%" border="0" align="center">
        <tr class="Nventa">
            <td align="center">El sistema detecto un intento de duplicacion de venta</td>
        </tr>
    </table>
    <% End If %>
    <p>&nbsp;</p>
    <script>
        function val(f)
        {
	        if(f.foliofactura.value == "")
	        {
		        alert("No se ha proporcionado el folio de la factura")
		        return false;
	        }
	        else
	        {
		        return true;
	        }
        }
    </script>
</body>
</html>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
