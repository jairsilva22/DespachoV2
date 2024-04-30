<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteProduccionChofer.aspx.cs" Inherits="despacho.ReporteProduccionChofer2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Producción por Chofer</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Produccion por Chofer</asp:Label>
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
            <asp:ListView ID="lvChofer" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="chofer">Chofer</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Tipo">Tipo de Producto</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Viajes">Viajes</th>
                            <th style="text-align: center" id="Promedio">Promedio</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="chofer">Chofer</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Tipo">Tipo de Producto</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Viajes">Viajes</th>
                            <th style="text-align: center" id="Promedio">Promedio</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                       <td style="text-align: center"><%# nombreChofer(int.Parse(Eval("idChofer").ToString())) %></td>
                        <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                        <td style="text-align: center"><%# Eval("planta") %></td>
                        <td style="text-align: center"><%# tipoProducto(int.Parse(Eval("idTipoProducto").ToString())) %></td>
                        <td style="text-align: center"><%# Eval("Cantidad") %></td>
                        <td style="text-align: center"><%# medidaProducto(int.Parse(Eval("idUDM").ToString()))%></td>
                        <td style="text-align: center"><%# cantidadViajes(int.Parse(Eval("idtrans").ToString()), DateTime.Parse(Eval("fecha").ToString())) %></td>
                        <td style="text-align: center"><%# obtenerPromedio(int.Parse(Eval("idtrans").ToString()), DateTime.Parse(Eval("fecha").ToString()), float.Parse(Eval("Cantidad").ToString())) %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvTotales" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center;" colspan="8">Suma por chofer</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center;" colspan="8">Suma por chofer</th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center;"><%# nombreChofer(int.Parse(Eval("idChofer").ToString())) %></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;"><%# Eval("cantidad")%></td>
                        <td style="text-align: center;">&nbsp;</td>                        
                        <td style="text-align: center;"><%# Eval("viajes")%></td>
                        <td style="text-align: center;"><%# formatoPromedio(float.Parse(Eval("cantidad").ToString()),float.Parse(Eval("viajes").ToString())) %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvPromedioDia" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center;" colspan="8">Promedio por dia</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            
                        </tr>
                        <tr>
                            <th style="text-align: center" id="Subtotal1E">Promedio por dia</th>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center;"><%# nombreChofer(int.Parse(Eval("idChofer").ToString())) %></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;"><%# promedioCantidadDia(float.Parse(Eval("cantidad").ToString()),int.Parse(Eval("idtrans").ToString())) %></td>
                        <td style="text-align: center;">&nbsp;</td>                        
                        <td style="text-align: center;"><%# promedioViajesDia(float.Parse(Eval("viajes").ToString()),int.Parse(Eval("idtrans").ToString()))%></td>
                        <td style="text-align: center;"><%# promedioViajesCantidadDia(float.Parse(Eval("cantidad").ToString()),float.Parse(Eval("viajes").ToString()),int.Parse(Eval("idtrans").ToString())) %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvTotalTotales" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center;" colspan="8">Total</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            
                        </tr>
                        <tr>
                            <th style="text-align: center" id="Subtotal1E">Total</th>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;"></td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;">&nbsp;</td>
                        <td style="text-align: center;"><%# totalCantidades() %></td>
                        <td style="text-align: center;">&nbsp;</td>                        
                        <td style="text-align: center;"><%# totalViajes()%></td>
                        <td style="text-align: center;"><%# totalPromedio() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:TextBox ID="txtViajes" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromedio" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtTotalCantidad" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtTotalViajes" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
