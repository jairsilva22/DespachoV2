<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"--> 
<!--#include file="checklogin.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset2
Dim Recordset2_cmd
Dim Recordset2_numRows

If Request.Form("formaPago") <> "" Then
	dim tipo
	'buscamos el tipo de documento de la factura'
	Set RSCEmpresa_cmd = Server.CreateObject ("ADODB.Command")
    RSCEmpresa_cmd.ActiveConnection = MM_Conecta1_STRING
    RSCEmpresa_cmd.CommandText = "SELECT descripcion FROM dbo.factura JOIN documento ON iddocumento = tipo_comprobante WHERE idfactura = " & request.QueryString("id")
    RSCEmpresa_cmd.Prepared = true
    Set RSCEmpresa = RSCEmpresa_cmd.Execute

    tipo = RSCEmpresa.Fields.Item("descripcion").Value

    'buscamos la serie de acuerdo al tipo de documento
    Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
	Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
	If tipo = "Factura" Then
		Recordset3_cmd.CommandText = "SELECT serie FROM dbo.folios WHERE idempresa = "&Request.Cookies("login")("idSucursal")&" AND factura = 'True'"
	ElseIf tipo = "Nota de Credito" Then
		Recordset3_cmd.CommandText = "SELECT serie FROM dbo.folios WHERE idempresa = "&Request.Cookies("login")("idSucursal")&" AND nCredito = 'True'"
	ElseIf tipo = "Nota de Cargo" Then
		Recordset3_cmd.CommandText = "SELECT serie FROM dbo.folios WHERE idempresa = "&Request.Cookies("login")("idSucursal")&" AND nCargo = 'True'"
	ElseIf tipo = "Traslado" Then
		Recordset3_cmd.CommandText = "SELECT serie FROM dbo.folios WHERE idempresa = "&Request.Cookies("login")("idSucursal")&" AND trapaso = 'True'"
	End If
	Recordset3_cmd.Prepared = true

	Set Recordset3 = Recordset3_cmd.Execute


	Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
	Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset2_cmd.CommandText = "UPDATE factura SET metodoPago = '"&Request.Form("formaPago")&"', NumCtaPago = '"&Request.Form("tarjeta")&"', estatus = 'ProcesoCO', forma_pago = '"& Request.Form("metodoPago") &"', idempresa = "&Request.Cookies("login")("idSucursal")&", serie = '"&Recordset3.Fields.Item("serie").Value&"' WHERE idfactura = "&Request.QueryString("id")
	Recordset2_cmd.Prepared = true

	Set Recordset2 = Recordset2_cmd.Execute
	Recordset2_numRows = 0
	
	Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
	Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
	Recordset1_cmd.CommandText = "UPDATE clientesFacturacion SET usoCFDI = '"&Request.Form("usoCFDI") &"' WHERE idcliente = "& Request.QueryString("idcliente")
	'Response.Write Recordset1_cmd.CommandText
	Recordset1_cmd.Prepared = true
	'response.write(Recordset1_cmd.CommandText)
	Set Recordset1 = Recordset1_cmd.Execute
	Recordset1_numRows = 0
	
	%>
	<script type="text/javascript">
		opener.location.reload();
		window.close()
	</script>
    
<%End If

Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT * FROM dbo.formaPagoSat ORDER BY codigo"
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0
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
Recordset1_cmd.CommandText = "SELECT usoCFDI FROM dbo.clientesFacturacion WHERE idCliente = " & Request.QueryString("idcliente")
Recordset1_cmd.Prepared = true
Recordset1_cmd.Parameters.Append Recordset1_cmd.CreateParameter("param1", 5, 1, -1, Recordset1__MMColParam) ' adDouble

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>


<%

dim usoCFDI

'if para vavlidar si cambio el usocfdi
if Request.Form("usoCFDI") <> "" then'usoCFDI
usoCFDI = Request.Form("usoCFDI")
else'usoCFDI
usoCFDI = (Recordset1.Fields.Item("usoCFDI").Value)
end if'usocfdi
%>
<!Doctype html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>Forma de Pago</title>
		<script type="text/javascript">
		function myFunction(){
		 var cte = document.getElementById('cliente').value;
		 
			var form = document.form1
		  	if(cte == "13")
		  	{
				form.formaPago.value = "01"
		  		form.tarjeta.value = "NO APLICA"
				form.metodoPago.value = "1"
		  	}
		  	else
		  	{
		  		form.tarjeta.value = ""	
				form.metodoPago.value = "2"
		  	}
			}
			
		  function valida ()
		  {
		    var form = document.form1

		    if(form.formaPago.value == '')
		    {
		      alert("Falta agregar la forma de pago")
		      return false
		    }
		     if(form.usoCFDI.value == '')
		    {
		      alert("Falta seleccionar el usoCFDI")
		      return false
		    }
		    if(form.tarjeta.value == '')
		    {
		      alert("Falta agregar los datos de la tarjeta")
		      return false
		    }
		    else
		    {
		    	if (form.tarjeta.value.length < 4)
			  	{
			  		alert("Deben ser 4 caracteres minimo")
			  		return false
			  	}
		    }
		  }

		  function tipo(valor)
		  {
		  	var form = document.form1
		  	if(valor == "01")
		  	{
		  		form.tarjeta.value = "NO APLICA"
				form.metodoPago.value = "1"
		  	}
		  	else
		  	{
		  		form.tarjeta.value = ""	
				form.metodoPago.value = "2"
		  	}
		  	if(valor == "99")
		  	{
		  		form.tarjeta.value = "POR DEFINIR"
				form.metodoPago.value = "2"
		  	}		  	

		  }
		</script>
	</head>
	<body onload="myFunction()">
		<p>&nbsp;</p>
		<h2 align="center">Factura con el ID: <%=Request.QueryString("id")%></h2>
		<p>&nbsp;</p>
		<form action="formaPago.asp?id=<%=Request.QueryString("id")%>&idCliente=<%=Request.QueryString("idCliente")%>" name="form1" method="post">
			<table align="center">
				<tr>
					<td align="right">Forma de Pago:</td>
				    <td><select name="formaPago" id="formaPago" onChange="tipo(this.value)">
				    		<option value="">Seleccionar</option>
						    <%While (NOT Recordset2.EOF)%>
						    <option value="<%=(Recordset2.Fields.Item("codigo").Value)%>"><%=(Recordset2.Fields.Item("codigo").Value)%>-> <%=(Recordset2.Fields.Item("descripcion").Value)%></option>
						    <%  Recordset2.MoveNext()
							Wend%>
				      	</select>
				    </td>
				</tr>
				<tr>
				  <td align="right">Datos de Tarjeta:</td>
				  <td><input type="text" name="tarjeta" id="tarjeta" value="" /></td>
			  </tr>
				<tr>
				  <td align="right">Metodo de pago:</td>
				  <td><select name="metodoPago" id="metodoPago">
                  <option>Seleccionar</option>
				    <option value="1">PUE Pago en una sola exhibicion</option>
				    <option value="2">PPD Pago en parcialidades o diferido</option>
			      </select></td>
			  </tr>
				<tr>
					<td align="right">UsoCFDI:</td>
					<td><select name="usoCFDI" id="usoCFDI" style="width: 90%">
    		   <option value="" <%If (Not isNull(usoCFDI)) Then If ("" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>Seleccionar</option>         

              <option value="G01"<%If (Not isNull(usoCFDI)) Then If ("G01" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>G01 Adquisicion de mercancias </option>
              
              <option value="G02" <%If (Not isNull(usoCFDI)) Then If ("G02" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>G02 Devoluciones, descuentos o bonificaciones</option>
              
              <option value="G03" <%If (Not isNull(usoCFDI)) Then If ("G03" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>G03 Gastos en general</option>
              
              <option value="I01" <%If (Not isNull(usoCFDI)) Then If ("I01" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I01 Construcciones </option>
              
              <option value="I02" <%If (Not isNull(usoCFDI)) Then If ("I02" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I02 Moviliario y equipo de oficina por inversiones</option>
              
              <option value="I03" <%If (Not isNull(usoCFDI)) Then If ("I03" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I03 Equipo de transporte </option>
              
              <option value="I04" <%If (Not isNull(usoCFDI)) Then If ("I04" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I04 Equipo de computo y accesorios</option>
              
              <option value="I05" <%If (Not isNull(usoCFDI)) Then If ("I05" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I05 Dados, tronqueles, moldes, matrices y heramental</option>
              
              <option value="I06" <%If (Not isNull(usoCFDI)) Then If ("I06" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I06 Comunicaciones telefonicas</option>
              
              <option value="I07" <%If (Not isNull(usoCFDI)) Then If ("I07" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I07 Comunicaciones satelitales</option>
              
              <option value="I08" <%If (Not isNull(usoCFDI)) Then If ("I08" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>I08 Otra maquinaria y equipo</option>
              
              <option value="D01" <%If (Not isNull(usoCFDI)) Then If ("D01" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D01 Honorarios medicos, dentales y gastos hospitalarios</option>
              
              <option value="D02" <%If (Not isNull(usoCFDI)) Then If ("D02" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D02 Gastos medicos por incapacidad o discapacidad </option>
              
              <option value="D03" <%If (Not isNull(usoCFDI)) Then If ("D03" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D03 Gastos funrales </option>
              
              <option value="D04" <%If (Not isNull(usoCFDI)) Then If ("D04" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D04 Donativos</option>
              
              <option value="D05" <%If (Not isNull(usoCFDI)) Then If ("D05" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D05 Intereses reales efectivamente pagados por credito hipotecarios (casa habitacion)</option>
              
              <option value="D06" <%If (Not isNull(usoCFDI)) Then If ("D06" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D06 Aportaciones voluntarias al SAR</option>
              
              <option value="D07" <%If (Not isNull(usoCFDI)) Then If ("D07" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D07 Primas por seguros de gastos medicos</option>
              
              <option value="D08" <%If (Not isNull(usoCFDI)) Then If ("D08" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D08 Gastos de transporte escolar obligatoria</option>
              
              <option value="D09" <%If (Not isNull(usoCFDI)) Then If ("D09" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D09 Depositos en cuentas para el ahorro, primas que tengan como base planes de pensiones </option>
              <option value="D10" <%If (Not isNull(usoCFDI)) Then If ("D10" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>D10 Pagos por servicios educativos (colegiaturas)</option>
              
              <option value="P01" <%If (Not isNull(usoCFDI)) Then If ("P01" = usoCFDI) Then Response.Write("selected=""selected""") : Response.Write("")%>>P01 Por definir</option>
            </select></td>
				</tr>
				<tr>
					<td align="center" colspan="2">
                    <input type="hidden" name="cliente" id="cliente" value="<%=(Request.QueryString("idCliente"))%>" />
                    <input type="submit" name="boton" value="Terminar" onclick="return valida()" /></td>
				</tr>
			</table>
		</form>
	</body>
</html>
