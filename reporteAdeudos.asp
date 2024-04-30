<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<!--#include file="stylo2.asp"-->
<%
Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows
Dim pagos, adeudo

    if Request.form("cliente") <> "" then
        idCte = Request.form("cliente")
    else
        idCte = request.QueryString("id")
    end if 

     

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT factura.idcliente, factura.fechaAlta, factura.idfactura, factura.folio, factura.idruta, factura.estatus, factura.total, clientes.idcliente, clientes.nombrecliente, clientes.rfcCliente, documento.descripcion as documento, estcobranza FROM dbo.factura, dbo.clientesFacturacion AS clientes, dbo.documento WHERE clientes.idcliente=factura.idcliente AND tipo_comprobante = iddocumento AND (factura.estcobranza='Pendiente' OR factura.estcobranza='Pagada parcial') AND factura.estatus = 'Facturada' AND tipo_comprobante = 1 AND factura.idcliente="& idCte &" ORDER BY fechacfd ASC"
Recordset1_cmd.Prepared = true
'Response.Write Recordset1_cmd.CommandText
Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0

Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

Set RecordsetC_cmd = Server.CreateObject ("ADODB.Command")
RecordsetC_cmd.ActiveConnection = MM_Conecta1_STRING
RecordsetC_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idcliente="&idCte
RecordsetC_cmd.Prepared = true
    
Set RecordsetC = RecordsetC_cmd.Execute

Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset3_cmd.CommandText = "SELECT * FROM dbo.sucursales WHERE id="&Request.Cookies("login")("idSucursal")
   ' response.Write(Recordset3_cmd.CommandText)
Recordset3_cmd.Prepared = true

Set Recordset3 = Recordset3_cmd.Execute
%>
<!Doctype html>
<html>
<head>
    <style type="text/css">
        .stilo1 {
            font-size: 12px;
            font-weight: bold;
        }

        .stylo1 {
            font-size: 12px;
            font-weight: bold;
        }

        .stylo2 {
            font-size: 12px;
        }

        .titulos {
            font-size: 32px;
            font-weight: bold;
        }

        .stilo2 {
            font-size: 12px;
        }

        .stilo1 {
            font-weight: bold;
        }

        .stilo2 {
            font-size: 12px;
        }
    </style>
</head>
<body>
    <table width="700" border="0" align="center">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr align="center" valign="bottom">
                        <td width="16%">
                            <img src="img/pepi_logo.png" width="91" height="69" /></td>
                        <td colspan="2" class="titulos"><%=Recordset3.fields.item("nombre").value %></td>
                    </tr>
                    <tr align="center">
                        <td height="25" colspan="2" valign="bottom">Adeudos de Clientes</td>
                        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>Cliente&nbsp;<%=recordsetC.fields.item("nombreCliente").value%></td>
        </tr>
        <tr>
            <td>Rfc: <%=recordsetC.fields.item("rfcCliente").value%></td>
        </tr>
        <tr>
            <td align="center">PAGOS SIN REALIZAR</td>
        </tr>
        <tr>
            <td>
                <table width="99%" border="0">
                    <tr align="center" class="stilo1" bgcolor="#e33045">
                        <td>FOLIO</td>
                        <td>COBRANZA</td>
                        <td>FECHA</td>
                        <td>MONTO</td>
                        <td>PAGOS</td>
                        <td>SALDO</td>
                    </tr>
                    <% While(NOT Recordset1.EOF)
			   
		 'Inicializar variables
				pagos = 0
				'////////////////////////////	  
					IF Recordset1.Fields.Item("folio").Value <>"" THEN
					   Set Recordset3_cmd = Server.CreateObject ("ADODB.Command") 
				       Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
				       Recordset3_cmd.CommandText = "SELECT SUM(total) AS pagos FROM dbo.logCXC WHERE  estatus<>'Cancelado' AND monto > 0 AND folio="&Recordset1.Fields.Item("folio").Value 'hacemos la suma del total de pagos por factura con el folio de la factura en turno segun el ciclo while
				       Recordset3_cmd.Prepared = true

				       Set Recordset3 = Recordset3_cmd.Execute
				       Recordset3_numRows = 0
				       ' if para asegurar que venga algun valor en el campo pagos
					  if (recordset3.fields.item("pagos").value<>"") then 'si viene vacio NO entra en la suma
				       pagosTotal=pagosTotal+(recordset3.fields.item("pagos").value) ' se suman los pagos que se han realizado para sacar los pagos totales
					  end if
					 END IF
					 
					   montototal=montototal+Recordset1.Fields.Item("total").Value
					   'if para valida que tiene pagos
					   if Recordset3.Fields.Item("pagos").Value <> "" then
					   pagos = Recordset3.Fields.Item("pagos").Value
					   pagos=pagos-cancelados
					   end if
					   
					   saldo = Round(Recordset1.fields.item("total").value - pagos,2)
					   		'if para validar el cambio del color
				if color = cgrid2 then'color
				color = cgrid1
				else'color
				color = cgrid2
				end if'color
                    %>
                    <tr align="center" class="stilo2" bgcolor="<%=color%>">
                        <td height="23" align="center"><%=(Recordset1.Fields.Item("folio").Value)%></td>
                        <td><%=(Recordset1.Fields.Item("estcobranza").Value)%>&nbsp;</td>
                        <td><%=(Recordset1.Fields.Item("fechaAlta").Value)%></td>
                        <td align="right"><%=formatnumber(Recordset1.Fields.Item("total").Value, 2)%>&nbsp;</td>
                        <td align="right"><%=formatnumber(pagos, 2) %>&nbsp;</td>
                        <td align="right"><%=formatnumber(saldo, 2)%>&nbsp;</td>
                    </tr>
                    <% adeudo = adeudo + saldo
		  Recordset1.MoveNext()
		Wend
                    %>
                    <tr>
                        <td colspan="5" align="right">Total:</td>
                        <td>$ <%=formatnumber(adeudo, 2)%></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>&nbsp;</p>
</body>
</html>
