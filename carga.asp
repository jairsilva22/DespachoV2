<%@LANGUAGE="VBSCRIPT"%>
<!--#include file="Connections/Conecta1.asp" -->
<%
Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset2_cmd.CommandText = "SELECT estatus, folio FROM dbo.factura WHERE idfactura = "&Request.QueryString("id")
Recordset2_cmd.Prepared = true

Set Recordset2 = Recordset2_cmd.Execute
Recordset2_numRows = 0

If Lcase(Recordset2.Fields.Item("estatus").Value) <> "facturada"Then%>
<div align="center"><img src="imagenes/ajax-loader.gif" width="20" height="20" border="0" /><br>Procesando...</div>
<meta http-equiv="refresh" content="6">
<%Else%>
<div align="center">Facturada<br /><a href="factura.asp" target="_top">Actualizar Vista</a></div>
<%
	Set Recordset31_cmd = Server.CreateObject ("ADODB.Command")
    Recordset31_cmd.ActiveConnection = MM_conecta1_STRING
    Recordset31_cmd.CommandText = "SELECT * FROM cancelarProductos WHERE idfactura = "&Request.QueryString("id")
    Recordset31_cmd.Prepared = true
    Set Recordset31 = Recordset31_cmd.Execute

    If Not Recordset31.EOF Then
    	'actualizamos el folio de la factura
    	Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
        Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
        Recordset3_cmd.CommandText = "UPDATE cancelarProductos SET folio = "&Recordset2.Fields.Item("folio").Value&" WHERE idfactura = "&Request.QueryString("id")
        Recordset3_cmd.Prepared = true
        Set Recordset3 = Recordset3_cmd.Execute
    End If
End IF%>
