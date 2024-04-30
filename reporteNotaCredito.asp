<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Connections/Conecta1.asp" -->
<!--#include file="config.asp"-->
<%
Server.ScriptTimeOut = 500000
Dim NC
Dim cte

if request.form("notaCredito")<>"" THEN
NC=" AND folio="&request.form("notaCredito")
ELSE
NC="" 
END IF

IF request.form("ok")<>"" THEN
cte=" AND idCliente="&request.form("ok")
ELSE
cte="" 
END IF

Dim Recordset1
Dim Recordset1_cmd
Dim Recordset1_numRows

Set Recordset1_cmd = Server.CreateObject ("ADODB.Command")
Recordset1_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset1_cmd.CommandText = "SELECT * FROM dbo.factura WHERE idfactura > 0 AND tipo_comprobante = 2" &NC &cte &" AND estatus='Facturada' order by folio asc"
'response.write(Recordset1_cmd.CommandText)
Recordset1_cmd.Prepared = true

Set Recordset1 = Recordset1_cmd.Execute
Recordset1_numRows = 0
%>
<%
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
<%
Dim Recordset3
Dim Recordset3_cmd
Dim Recordset3_numRows

if NOT Recordset1.EOF then
    Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
    Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
    Recordset3_cmd.CommandText = "SELECT * FROM dbo.clientesFacturacion WHERE idcliente="&recordset1.fields.item("idcliente").value 
    'response.write(Recordset3_cmd.CommandText)
    Recordset3_cmd.Prepared = true

    Set Recordset3 = Recordset3_cmd.Execute
    Recordset3_numRows = 0

    Otrunc=Recordset3.fields.item("truncar").value
end if 

%>
<%
Dim Repeat1__numRows
Dim Repeat1__index

Repeat1__numRows = -1
Repeat1__index = 0
Recordset2_numRows = Recordset2_numRows + Repeat1__numRows
%>
<%
Dim Repeat2__numRows
Dim Repeat2__index

Repeat2__numRows = -1
Repeat2__index = 0
Recordset1_numRows = Recordset1_numRows + Repeat2__numRows

Set RSInfo_cmd = Server.CreateObject ("ADODB.Command")
RSInfo_cmd.ActiveConnection = MM_Conecta1_STRING
RSInfo_cmd.CommandText = "SELECT * FROM dbo.confimenor WHERE idConf = 1 AND refacciones = 'True'"
RSInfo_cmd.Prepared = true

Set RSInfo = RSInfo_cmd.Execute
%>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
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
    <table width="700" border="0">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr align="center" valign="bottom">
                        <td width="16%">
                            <img src="img/pepi_logo.png" width="91" height="69" /></td>
                        <td colspan="2" class="titulos"><%=RSInfo.fields.item("razon_social").value %></td>
                    </tr>
                    <tr align="center">
                        <td height="25" colspan="2" valign="bottom">Detalles Nota de Credito&nbsp;<%if not recordset1.eof then %>
                            <%=recordset1.fields.item("folio").value%>
                            <% end if %>
                        </td>
                        <td width="31%" valign="bottom">Fecha Impresion : <%= Date() %></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>Cliente&nbsp;<%if not recordset1.eof then %>
                <%=recordset3.fields.item("nombreCliente").value%>
                <% end if %>
            </td>
        </tr>
        <tr>
            <td>
                <table width="340" border="0" align="center">
                    <tr align="center" class="stilo1" bgcolor="#e33045">

                        <td>FOLIO</td>
                        <td>MONTO</td>
                        <td>SALDO</td>
                    </tr>
                    <%
           
        While ((Repeat2__numRows <> 0) AND (NOT Recordset1.EOF)) 

        subtotal = 0
        totalImpuestosTrasladados = 0 
        total = 0
        totalImpuestosretenidos =0
           
	           if saldo < 0 then
               saldo = 0
             end if

         '--------------------sacar el total de la factura----------------------'   
        Dim RecordsetDet
        Dim RecordsetDet_cmd
        Dim RecordsetDet_numRows

        Set RecordsetDet_cmd = Server.CreateObject ("ADODB.Command")
        RecordsetDet_cmd.ActiveConnection = MM_Conecta1_STRING
        RecordsetDet_cmd.CommandText = "SELECT cantidad, precio_unitario, detFactura.descuento, ISNULL(detFactura.iva, 0) AS iva FROM dbo.detFactura WHERE id_factura = " & recordset1.fields.item("idfactura").value 
        RecordsetDet_cmd.Prepared = true
        
        Set RecordsetDet = RecordsetDet_cmd.Execute
        RecordsetDet_numRows = 0

        While (NOT RecordsetDet.EOF)
	
          If RecordsetDet.Fields.item("cantidad").Value <> 0 Then 'cantidad
                cantidad = RecordsetDet.Fields.item("cantidad").Value
          End If 'cantidad

             'if para validar que tiene precio_unitario
            If RecordsetDet.Fields.item("precio_Unitario").Value <> 0 Then 'precio_unitario
                precioUnitario = (RecordsetDet.Fields.item("precio_Unitario").Value)
            End If 'precio_unitario
            'response.write(precioUnitario)

            'if para validar si tiene descuento
            If RecordsetDet.Fields("descuento").Value <> 0 Then
                des = (RecordsetDet.Fields("descuento").Value)
            Else
                des = 0
            End If

              '------------------para el precio Unitario
'response.write(Recordset3.Fields.Item("truncar").Value)
            If (Otrunc) = "si" Then
                If des > 0 Then
                    precio_Unitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                    precio_Unitario = truncarAseis(precioUnitario)
                End If

                totalConcepto = truncarAdos(precioUnitario * cantidad)
            Else
                If des > 0 Then
                    precio_unitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                    precio_unitario = truncarAseis(precioUnitario)
                End If

                totalConcepto = redondear(precio_unitario * cantidad)
            End If
'response.write(totalConcepto &" :totalConcepto -cantidad:" & cantidad)
            'impuestos-----------------'
    
            retCte = Recordset1.Fields.item("retencion").Value
            'validar si la factura contiene iva'

            if Recordset1.fields.item("iva").value <> 0 then
              iva = 16
            else
              iva = 0
            end if 


           '' iva = RecordsetDet.Fields.Item("iva").Value
'response.write(iva & " iva " & retCte)
          If Otrunc = "si" Then
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
           If des > 0 Then
                    precioUnitario = truncarAseis(precioUnitario * ((100 - (des)) / 100))
                Else
                    precioUnitario = truncarAseis(precioUnitario)

                End If
                totalConcepto = redondear(precioUnitario * cantidad)
                subTotal = subtotal + redondear(totalConcepto)
                 'validamos si la factura tiene iva

                If iva <> 0 Then
                    'Console.Write(totalConcepto)
                    impuesto = redondear(totalConcepto * ((iva) / 100))

                    totalImpuestosTrasladados = totalImpuestosTrasladados +  redondear(impuesto)
 ''                     response.write("Imp trasaladodos...."&totalImpuestosTrasladados&"....")
                End If
                If retCte > 0 Then

                    'Console.Write(totalConcepto)
                    retencion = redondear(totalConcepto * (0.16))

                    totalImpuestosretenidos = totalImpuestosretenidos +redondear(retencion)
                Else
                    totalImpuestosretenidos = 0
                End If
                  subTotal = redondear(subTotal)   
                 '' response.write("subTotal:"&subTotal)
      end if

       'limpiamos las variables de los conceptos
       'impuesto = 0
       '' totalConcepto = 0
       '' precioUnitario = 0

	     'total = 0
	      
        Dim isr
        'obtenemos el total
        If truncar = "si" Then
            total = truncarAdos(totalImpuestosTrasladados + subTotal)
          ''  totalImpuestosTrasladados = 0
        Else
            total = redondear(totalImpuestosTrasladados + subTotal)
            ''  totalImpuestosTrasladados = 0
        End If
'response.write("total ="&total&"#"& hayisr)
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

        RecordsetDet.MoveNext()
'response.write(total)
        Wend
         If retCte > 0 Then
            total = subTotal
        Else
            total = (total)
        End If
                    %>
                    <form id="form2" name="form2" method="POST" action="actualizarNC.asp">
                    <tr align="center" class="stilo2" bgcolor="#CCCCCC">
                        <td><%=(Recordset1.Fields.Item("folio").Value)%><input type="hidden" name="folio" value="<%=(Recordset1.Fields.Item("folio").Value)%>"></td>
                        <td>$&nbsp;<%=(total)%><input type="hidden" name="total" value="<%=(total)%>"></td>
                        <%

            Set Recordset3_cmd = Server.CreateObject ("ADODB.Command")
            Recordset3_cmd.ActiveConnection = MM_Conecta1_STRING
            Recordset3_cmd.CommandText = "SELECT SUM(cantidad_recibida) AS saldo FROM dbo.logCXC WHERE refecencia = '"&recordset1.fields.item("folio").value&"' AND tipo_pago = 7"
            'response.write(Recordset3_cmd.CommandText)
            Recordset3_cmd.Prepared = true
            Set Recordset3 = Recordset3_cmd.Execute


            If Recordset3.Fields.Item("saldo").Value <> "" Then
              saldo= (total-(Recordset3.Fields.Item("saldo").Value))
'response.write(saldo & "--")		
              If saldo <> "" Then
                if ((total)-(Recordset3.Fields.Item("saldo").Value)) =< 0 THEN
                  saldo = (total)-(Recordset3.Fields.Item("saldo").Value)

                else
                  saldo = (saldo)
                End If
              else
                saldo = (total)-(Recordset3.Fields.Item("saldo").Value)

              End If
            else
              saldo = total
            End If
 

            if (Otrunc = "si") then
                saldo = truncarAdos(saldo)          
		if saldo - .99 < 0 then
                  saldo = 0
                end if
            Else
  		if saldo - .99 < 0 then
                  saldo = 0
                end if
                saldo = redondear(saldo)
            end if
                        %>
                        <td>$&nbsp;<%=(saldo)%><input type="hidden" name="saldo" value="<%=saldo%>"></td>
                    </tr>
                    <% 
  Repeat2__index=Repeat2__index+1
  Repeat2__numRows=Repeat2__numRows-1
  Recordset1.MoveNext()
Wend
                    %>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <input type="submit" value="Actualizar" name="Actualizar" id="enviar" />
                
            </td>
        </tr>
        </form>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td align="center">PAGOS REALIZADOS</td>
        </tr>
        <tr>
            <td>
                <table width="99%" border="0">
                    <tr align="center" class="stilo1" bgcolor="#e33045">
                        <td>FECHA</td>
                        <td>FACTURA</td>
                        <td>NOTA DE CREDITO</td>
                        <td>CANTIDAD RECIBIDA</td>
                        <td>MONTO</td>
                        <td>CAMBIO</td>
                        <td>OBSERVACIONES</td>
                    </tr>
                    <%

Dim Recordset11
Dim Recordset11_cmd
Dim Recordset11_numRows

Set Recordset11_cmd = Server.CreateObject ("ADODB.Command")
Recordset11_cmd.ActiveConnection = MM_Conecta1_STRING
Recordset11_cmd.CommandText = "SELECT * FROM dbo.factura WHERE idfactura > 0 AND tipo_comprobante = 2" &NC &cte &" AND estatus='Facturada' order by folio asc"
Recordset11_cmd.Prepared = true

Set Recordset11 = Recordset11_cmd.Execute
Recordset11_numRows = 0

While (not recordset11.eof )
      Dim Recordset2
      Dim Recordset2_cmd
      Dim Recordset2_numRows

      Set Recordset2_cmd = Server.CreateObject ("ADODB.Command")
      Recordset2_cmd.ActiveConnection = MM_Conecta1_STRING
      Recordset2_cmd.CommandText = "SELECT * FROM dbo.logCXC WHERE refecencia='"&recordset11.fields.item("folio").value&"' AND tipo_pago = 7"
      'response.Write Recordset2_cmd.CommandText
      Recordset2_cmd.Prepared = true

      Set Recordset2 = Recordset2_cmd.Execute
      Recordset2_numRows = 0

      While ((Repeat1__numRows <> 0) AND (NOT Recordset2.EOF)) 
     'calcular cantidad correspondiente

        subTotalFact = 0
        totalImpuestosTrasladadosFact = 0 
        totalFact = 0
        totalImpuestosretenidosFact =0

        Dim RecordsetDetFac
        Dim RecordsetDetFac_cmd
        Dim RecordsetDetFac_numRows

        Set RecordsetDetFac_cmd = Server.CreateObject ("ADODB.Command")
        RecordsetDetFac_cmd.ActiveConnection = MM_Conecta1_STRING
        RecordsetDetFac_cmd.CommandText = "SELECT cantidad, precio_unitario, detFactura.descuento, ISNULL(detFactura.iva, 0) AS iva FROM factura INNER JOIN detFactura ON factura.idfactura = detFactura.id_factura WHERE factura.folio = " & Recordset2.Fields.Item("folio").Value
'response.write(RecordsetDetFac_cmd.CommandText)
        RecordsetDetFac_cmd.Prepared = true
        
        Set RecordsetDetFac = RecordsetDetFac_cmd.Execute
        RecordsetDetFac_numRows = 0
        dim cantidadFact 

	     While (NOT RecordsetDetFac.EOF)


            If RecordsetDetFac.Fields.item("cantidad").Value <> 0 Then 'cantidad
                cantidadFact = RecordsetDetFac.Fields.item("cantidad").Value
            End If 'cantidad
'response.write(cantidadFact)
             'if para validar que tiene precio_unitario
            If RecordsetDetFac.Fields.item("precio_Unitario").Value <> 0 Then 'precio_unitario
               precioUnitarioFact = (RecordsetDetFac.Fields.item("precio_Unitario").Value)
           End If 'precio_unitario
           'response.write(precioUnitarioFact)

            'if para validar si tiene descuento
            If RecordsetDetFac.Fields("descuento").Value <> 0 Then
                desFact = (RecordsetDetFac.Fields("descuento").Value)
            Else
                desFact = 0
            End If
  '------------------para el precio Unitario
'response.write(Recordset3.Fields.Item("truncar").Value)
            If (Otrunc) = "si" Then
               If desFact > 0 Then
                  precio_UnitarioFact = truncarAseis(precioUnitarioFact * ((100 - (desFact)) / 100))
               Else
                   precio_UnitarioFact = truncarAseis(precioUnitarioFact)
               End If

              totalConceptoFact = truncarAdos(precioUnitarioFact * cantidadFact)
           Else
               If desFact > 0 Then
                   precio_unitarioFact = truncarAseis(precioUnitarioFact * ((100 - (desFact)) / 100))
              Else
                  precio_unitarioFact = truncarAseis(precioUnitarioFact)
              End If

                totalConceptoFact = redondear(precio_unitarioFact * cantidadFact)
            End If


        'response.write(totalConceptoFact)
                                    'impuestos-----------------'
    
          '' response.Write(iva)
          If Otrunc = "si" Then
           If desFact > 0 Then
                  precioUnitarioFact = truncarAseis(precioUnitarioFact * ((100 - (desFact)) / 100))
               Else
                   precioUnitarioFact = truncarAseis(precioUnitarioFact)
               End If
               totalConceptoFact = truncarAdos(precioUnitarioFact * cantidadFact)
               subTotalFact = subTotalFact + truncarAdos(totalConceptoFact)
              'validamos si la factura tiene iva
               If iva <> 0 Then
                   impuestoFact = truncarAdos(totalConceptoFact * (iva / 100))
                   totalImpuestosTrasladadosFact = totalImpuestosTrasladadosFact + truncarAdos(impuestoFact)
               End If
           subTotalFact = truncarAdos(subTotalFact)
         Else
               If desFact > 0 Then
                   precioUnitarioFact = truncarAseis(precioUnitarioFact * ((100 - (desFact)) / 100))
               Else
                   precioUnitarioFact = truncarAseis(precioUnitarioFact)

               End If
              totalConceptoFact = redondear(precioUnitarioFact * cantidadFact)
              subTotalFact = subTotalFact + redondear(totalConceptoFact)

                'validamos si la factura tiene iva

                If iva <> 0 Then
                   'Console.Write(totalConcepto)
                  impuestoFact = redondear(totalConceptoFact * ((iva) / 100))
''
                   totalImpuestosTrasladadosFact = totalImpuestosTrasladadosFact +  redondear(impuestoFact)
              End If
             If retCte > 0 Then

                    'Console.Write(totalConceptoFact)
                   retencionFact = redondear(totalConceptoFact * (0.16))

                    totalImpuestosretenidosFact = totalImpuestosretenidosFact +redondear(retencionFact)
                Else
                    totalImpuestosretenidosFact = 0
                End If
                  subTotalFact = redondear(subTotalFact)
                 
          end if

          'limpiamos las variables de los conceptos
         '' response.write(subTotalFact)
       
        Dim isrFact
        'obtenemos el total
        If truncar = "si" Then
            totalFact = truncarAdos(totalImpuestosTrasladadosFact + subTotalFact)
           'totalImpuestosTrasladadosFact = 0
        Else
            totalFact = redondear(totalImpuestosTrasladadosFact + subTotalFact)
''             '' totalImpuestosTrasladadosFact = 0
        End If

        'validamos si hay isr
        If hayisr <> "" And hayisr = "si" Then
          isrFact = (subTotalFact * 0.15)
          totalFact = CDbl(subTotalFact) - isrFact

          If truncar = "si" Then
                 totalFact = truncarAdos(totalFact)
          Else
             totalFact = redondear(totalFact)
          End If
        End If

       RecordsetDetFac.MoveNext()

      Wend

      If retCte > 0 Then
           totalFact = subTotalFact
      Else
           totalFact = (totalFact)
      End If

      'if para validar el cambio del color
      if color = cgrid2 then'color
      color = cgrid1
      else'color
      color = cgrid2
      end if'color%>
                    <tr align="center" class="stilo2" bgcolor="<%=color%>">
                        <td><%=(Recordset2.Fields.Item("fechaalta").Value)%>&nbsp;</td>
                        <td><%=(Recordset2.Fields.Item("folio").Value)%></td>
                        <td><%=(Recordset2.Fields.Item("refecencia").Value)%></td>
                        <td><%=(Recordset2.Fields.Item("cantidad_recibida").Value)%></td>
                        <td><%=totalFact%></td>
                        <td><%=(Recordset2.Fields.Item("cambio").Value)%></td>
                        <td>&nbsp;<%=(Recordset2.Fields.Item("observaciones").Value)%></td>
                    </tr>
                    <% 
        Repeat1__index=Repeat1__index+1
        Repeat1__numRows=Repeat1__numRows-1
        Recordset2.MoveNext()
      Wend
Recordset11.MoveNext()
Wend

       
                    %>
                </table>
            </td>
        </tr>
    </table>
    <p>&nbsp;</p>
</body>
</html>
<%if not recordset1.eof then
    Recordset2.Close()
    Set Recordset2 = Nothing
    end if
%>
<%
    if not recordset1.eof then
        Recordset3.Close()
        Set Recordset3 = Nothing
    end if
%>
<%
Recordset1.Close()
Set Recordset1 = Nothing
%>
