<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteVentasPorCliente.aspx.cs" Inherits="despacho.reporteVentasPorCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Ventas por Fecha</title>
    <style type="text/css">
        <!--
        .titulos {
            font-size: 20px;
            font-weight: bold;
        }

        .colorFondo {
            background-color: rgb(218,227,243);
        }

        table {
            table-layout: fixed;
            width: 100%;
        }
        -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 95%; text-align: center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Ventas por Fecha</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                                </div>
                              <%--  <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="LabelNumeroCte" runat="server"><strong>Cliente: </strong></asp:Label>
                                    <asp:Label ID="LabelNombreCte" runat="server"><strong>Nombre: </strong></asp:Label>
                                </div>--%>
                            </div>
                            <div></div>
                        </div>
                    </td>
                </tr>
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <hr color="#e33045" />
                    </td>
                </tr>

                <tr align="center">
                    <td height="25" valign="bottom">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>

            <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            
                            <%--<th style="text-align: center" id="Código Documento">Código Documento</th>--%>
                            <th style="text-align: center" id="Codigo Cliente">Codigo Cliente</th>
                            <th style="text-align: center" id="Nombre">Nombre</th>
                            <%--<th style="text-align: center" id="CIDproducto">CIDproducto</th>--%>
                            <th style="text-align: center" id="Codigo Producto">Codigo Producto</th>
                            <th style="text-align: center" id="Nombre(productos, servicios, Paquetes)">Nombre(productos, servicios, Paquetes)</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Neto">Neto</th>
                            <th style="text-align: center" id="Descuento">Descuento</th>
                            <th style="text-align: center" id="Neto-Desc">Neto-Desc</th>
                            <th style="text-align: center" id="Impuesto">Impuesto</th>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                           
                          <%--<th style="text-align: center" id="Código Documento">Código Documento</th>--%>
                            <th style="text-align: center" id="Codigo Cliente">Codigo Cliente</th>
                            <th style="text-align: center" id="Nombre">Nombre</th>
                            <%--<th style="text-align: center" id="CIDproducto">CIDproducto</th>--%>
                            <th style="text-align: center" id="Codigo Producto">Codigo Producto</th>
                            <th style="text-align: center" id="Nombre(productos, servicios, Paquetes)">Nombre(productos, servicios, Paquetes)</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Neto">Neto</th>
                            <th style="text-align: center" id="Descuento">Descuento</th>
                            <th style="text-align: center" id="Neto-Desc">Neto-Desc</th>
                            <th style="text-align: center" id="Impuesto">Impuesto</th>
                            <th style="text-align: center" id="Total">Total</th>
                        </tr>
                        <tr>
                            <td colspan="10" align="center">Sin Resultados
                            </td>

                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <%--<td style="text-align: center"><%# Eval("CIDDOCUMENTO").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("CCODIGOCLIENTE").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CRAZONSOCIAL").ToString() %></td>
                        <%--<td style="text-align: center"><%# Eval("CIDPRODUCTO").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("CCODIGOPRODUCTO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CNOMBREPRODUCTO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CUnidades").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CNETO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CDESCUENTO1").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Neto-Desc").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CIMPUESTO1").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CTOTAL").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>







            </div>
    </form>
</body>
</html>
