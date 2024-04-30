﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteProduccionTotal.aspx.cs" Inherits="despacho.ReporteProduccionTotal" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Comisiones</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Producción Total</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                                </div>
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
                    <%--<td width="40%" valign="bottom">Fecha Impresion : <%= DateTime.Now.ToShortDateString() %></td>--%>
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>
            <asp:ListView ID="lvProduccion" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Sucursal">Planta</th>
                            <th style="text-align: center" id="TipoProducto">Tipo de producto</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Monto">Monto</th>
                            <th style="text-align: center" id="IVA">IVA</th>
                            <th style="text-align: center" id="MontoTotal">Monto Total</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="FechaE">Fecha</th>
                            <th style="text-align: center" id="SucursalE">Planta</th>
                            <th style="text-align: center" id="TipoProductoE">Tipo de producto</th>
                            <th style="text-align: center" id="CantidadE">Cantidad</th>
                            <th style="text-align: center" id="UnidadE">Unidad</th>
                            <th style="text-align: center" id="MontoE">Monto</th>
                            <th style="text-align: center" id="IVAE">IVA</th>
                            <th style="text-align: center" id="MontoTotalE">Monto Total</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                        <td style="text-align: center"><%# Eval("planta") %></td>
                        <td style="text-align: center"><%# Eval("tipo") %></td>
                        <td style="text-align: center"><%# Eval("cantidad") %></td>
                        <td style="text-align: center"><%# medidaProducto(int.Parse(Eval("idUDM").ToString())) %></td>
                        <td style="text-align: right">$<%# Eval("subtotal") %></td>
                        <td style="text-align: right">$<%# Eval("iva") %></td>
                        <td style="text-align: right">$<%# Eval("total") %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvTotales" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            
                        </tr>
                        <%--<tr>
                            <th style="text-align: center" id="Subtotal1E">Subtotal para 1</th>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>--%>
                         <tr>
                            <th style="text-align: center" id="TotalPlantasE">Total para todas la plantas</th>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                         <%--<tr>
                            <th style="text-align: center" id="ImporteTotalE">Importe Total General</th>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>--%>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <%--<tr>
                        <td style="text-align: center;">Subtotal para 1</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;" ><%# Eval("tipo") %></td>
                        <td style="text-align: center;"><%# Eval("cantidad") %></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;">$<%# Eval("subtotal") %></td>
                        <td style="text-align: center;">$<%# Eval("iva") %></td>
                        <td style="text-align: center;">$<%# Eval("total") %></td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: center;">Total General</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;" ><%# Eval("tipo") %></td>
                        <td style="text-align: center;"><%# sumaCantidad() %></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;">$<%# sumaSubtotal() %></td>
                        <td style="text-align: center;">$<%# sumaIva() %></td>
                        <td style="text-align: center;">$<%# sumaTotal() %></td>
                    </tr>
                    <%--<tr>
                         <td style="text-align: center;">Importe Total General</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;" ></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;">$<%# sumaGranTotal() %></td>
                    </tr>--%>
                </ItemTemplate>
            </asp:ListView>
        </div>

        <asp:TextBox ID="txtPlantasCantidad" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPlantasSubtotal" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPlantasIva" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPlantasTotal" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPlanta1Total" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtGranTotal" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
