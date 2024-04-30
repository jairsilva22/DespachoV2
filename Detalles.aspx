<%@ Page Language="C#"%>
<%@ Import Namespace="System.Data" %>
<%

    DetallesPagos detalle = new DetallesPagos();
    double saldoAnt = 0, saldoPagado = 0, salgoInsoluto = 0;
    
    try
    {
        detalle.idpago = int.Parse(Request.QueryString["idPago"]);
        detalle.idfactura = int.Parse(Request.QueryString["idfactura"]);
        detalle.buscarFactura();
    }
    catch (Exception ex)
    {
        throw (ex);
    }

%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title><style>
        .table1 {
            border-collapse: collapse;
            width: 100%;
        }

        .th1, .td1 {
            text-align: left;
            padding: 8px;
        }

        .color1 {
            background-color: dodgerblue;
            color: white;
        }

        .tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .AText {
            text-align: right;
        }

        .AText8 {
            text-align: center;
        }
    </style>
</head>
<body>
        <div align="center">
        <h2>
            <center>Pagos</center>
        </h2>
        <div align="left">
            <a href="pagosFactura.asp?idfactura=<%=Request.QueryString["idfactura"] %>">
                <img src="imagenes/Arrow-left.png" width="16" height="16" border="0" />
                Regresar</a>
        </div>

        <div align="center">
            <table width="80%">
                <tr>
                    <td>Fecha CFDI:
                    </td>
                    <td>
                        <%=detalle.fechaAlta %>
                    </td>
                </tr>
                <tr>
                    <td>Cliente:
                    </td>
                    <td>
                        <%=detalle.nombreCliente %>
                    </td>
                </tr>
                <tr>
                    <td>RFC:
                    </td>
                    <td>
                        <%=detalle.rfcCliente %>
                    </td>
                </tr>
                <tr>
                    <td>MonedaP:</td>
                    <td><%=detalle.moneda %></td>
                </tr>
                <tr>
                    <td>TipoCambioP:</td>
                    <td><%=detalle.cambioP %></td>
                </tr>
                <tr>
                    <td>Forma de Pago:
                    </td>
                    <td><%=detalle.formaPago %></td>
                </tr>
                <tr>
                    <td>Tipo Pago:
                    </td>
                    <td>
                        <%=detalle.metodoPago %>
                    </td>
                </tr>
                <tr>
                    <td>No. operacion:
                    </td>
                    <td><%=detalle.noOperacion %></td>
                </tr>
                <tr>
                    <td>RfcEmisorCtaOrd:
                    </td>
                    <td><%=detalle.rfcEmisorCtaOrd %></td>
                </tr>
                <tr>
                    <td>CtaOrdenante:
                    </td>
                    <td>
                        <%=detalle.ctaOrdenante %>
                    </td>
                </tr>
                <tr>
                    <td>NomBancoOrdExt:
                    </td>
                    <td>
                        <%=detalle.NomBancoOrdExt %>
                    </td>
                </tr>                
                <tr>
                    <td>RfcEmisorCtaBen:
                    </td>
                    <td>
                        <%=detalle.rfcEmisorCteBen %>
                    </td>
                </tr>
                <tr>
                    <td>CtaBeneficiario:
                    </td>
                    <td>
                        <%=detalle.ctaBeneficiario %>
                    </td>
                </tr>
            </table>
        </di>

        <div align="right">
            
        </div>
        <br>
        <table class="Table1">
            <tr class="color1">
                <td class="td1">Parc.
                </td>
                <td class="td1">Folio
                </td> 
                <td class="td1">Moneda
                </td> 
                <td class="td1">Tipo
                </td> 
                <td class="td1">Serie
                </td>
                <td class="td1">Saldo Anterior
                </td>
                <td class="td1">Saldo
                </td>
                <td class="td1">Importe Pagado
                </td>
             
            </tr>
            <%
                DataTable tableDetalles = detalle.buscarDetFactura();

                for (int i = 0; i < tableDetalles.Rows.Count; i++)
                {
            %>
            <tr class="tr">
                <td>
                    <%=tableDetalles.Rows[i]["noParcialidad"].ToString()%>
                </td>
                <td>
                    <%=tableDetalles.Rows[i]["folio"].ToString()%>
                </td> 
                <td>
                    <%=tableDetalles.Rows[i]["moneda"].ToString()%>
                </td>        
                <td>
                    <%=tableDetalles.Rows[i]["descripcion"].ToString()%>
                </td> 
                <td>
                    <%=tableDetalles.Rows[i]["serie"].ToString()%>
                </td>
                <td>
                    <%=tableDetalles.Rows[i]["ImpSaldoAnt"].ToString()%>
                </td>
                <td>
                    <%=tableDetalles.Rows[i]["impSaldoInsoluto"].ToString()%>
                </td>
                <td>
                    <%=tableDetalles.Rows[i]["impPagado"].ToString()%> 
                </td>
                
            </tr>
            <%      //saldoAnt = saldoAnt + double.Parse(tableDetalles.Rows[i]["ImpSaldoAnt"].ToString());
                    //saldoPagado = saldoPagado + double.Parse(tableDetalles.Rows[i]["impPagado"].ToString());
                    //salgoInsoluto = salgoInsoluto + double.Parse(tableDetalles.Rows[i]["impSaldoInsoluto"].ToString());
                    if(tableDetalles.Rows[i]["tComprobante"].ToString() == "2")
                    {
                        //saldoAnt = saldoAnt - double.Parse(tableDetalles.Rows[i]["ImpSaldoAnt"].ToString());
                        saldoPagado = saldoPagado - double.Parse(tableDetalles.Rows[i]["impPagado"].ToString());
                        //salgoInsoluto = salgoInsoluto - double.Parse(tableDetalles.Rows[i]["impSaldoInsoluto"].ToString());
                    }else
                    {
                         saldoAnt = saldoAnt + double.Parse(tableDetalles.Rows[i]["ImpSaldoAnt"].ToString());
                        saldoPagado = saldoPagado + double.Parse(tableDetalles.Rows[i]["impPagado"].ToString());
                        salgoInsoluto = salgoInsoluto + double.Parse(tableDetalles.Rows[i]["impSaldoInsoluto"].ToString());
                    }
                    //update de los salfos
                    detalle.ImpSaldoAnt = Math.Round(saldoAnt, 2);
                    detalle.impPagado = Math.Round(saldoPagado, 2);
                    detalle.impSaldoInsoluto = Math.Round(salgoInsoluto, 2);
                    detalle.actualizarPago();
                }
            %>
            <tr>
                <td colspan="6" class="AText">Total saldo anterior
                </td>
                <td class="AText">
                    <%=saldoAnt %>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="AText">Importe pagado 
                </td>
                <td class="AText">
                    <%=saldoPagado %>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="AText">Total saldo insoluto
                </td>
                <td class="AText">
                    <%=salgoInsoluto %>
                </td>
            </tr>
        </table>
        <br />
        
        <p>&nbsp;</p>
    </div>
</body>
      <script type="text/javascript">
         function submitform() {
             document.myform1.submit();alert("gfd");
             if (confirm("¿Desea eliminar el pago?")) {
                 
                 return true;
             } else {
                 return false;
             }
           
       }
     </script>
</html>
