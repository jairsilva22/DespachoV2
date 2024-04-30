<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteProgramacionOrdenes.aspx.cs" Inherits="despacho.reporteProgramacionOrdenes" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Programación de Ordenes</title>
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
        <div style="width: 100%; text-align: center;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Programación de Ordenes</asp:Label>
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
            <asp:ListView ID="lvProgOrdenes" runat="server">
                <LayoutTemplate>
                    
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <%--<tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Codigo">Codigo de Orden</th>
                            <th style="text-align: center" id="Cliente" colspan="2">Cliente</th>
                            <th style="text-align: center" id="Direccion">Direccion de entrega</th>
                            <th style="text-align: center" id="Producto">Producto</th>
                            <th style="text-align: center" id="Descripcion">Descripcion producto</th>
                            <th style="text-align: center" id="Rate">Rate Type</th>
                            <th style="text-align: center" id="Primer">Primer Ticket</th>
                            <th style="text-align: center" id="Hora">Hora Inicio</th>
                            <th style="text-align: center" id="Travel">Travel</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="TipoUnidad">Tipo Unidad</th>
                            <th style="text-align: center" id="Truck">Truck Reg</th>
                            <th style="text-align: center" id="CantidadEntregada">Cantidad Entregada</th>
                        </tr>--%>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Codigo">Codigo de Orden</th>
                            <th style="text-align: center" id="Cliente" colspan="3">Cliente</th>
                            <th style="text-align: center" id="Direccion" colspan="2">Direccion de entrega</th>
                            <th style="text-align: center" id="Producto" colspan="2">Producto</th>
                            <th style="text-align: center" id="Descripcion" colspan="2">Descripcion producto</th>
                            <th style="text-align: center" id="Rate">Rate Type</th>
                            <th style="text-align: center" id="Primer">Primer Ticket</th>
                            <th style="text-align: center" id="Hora">Hora Inicio</th>
                            <th style="text-align: center" id="Travel">Travel</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="TipoUnidad">Tipo Unidad</th>
                            <th style="text-align: center" id="Truck">Truck Reg</th>
                            <th style="text-align: center" id="CantidadEntregada">Cantidad Entregada</th>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                    </tr>
                    
                    <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Codigo">Codigo de Orden</th>
                            <th style="text-align: center" id="Cliente" colspan="3">Cliente</th>
                            <th style="text-align: center" id="Direccion" colspan="2">Direccion de entrega</th>
                            <th style="text-align: center" id="Producto" colspan="2">Producto</th>
                            <th style="text-align: center" id="Descripcion" colspan="2">Descripcion producto</th>
                            <th style="text-align: center" id="Rate">Rate Type</th>
                            <th style="text-align: center" id="Primer">Primer Ticket</th>
                            <th style="text-align: center" id="Hora">Hora Inicio</th>
                            <th style="text-align: center" id="Travel">Travel</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="TipoUnidad">Tipo Unidad</th>
                            <th style="text-align: center" id="Truck">Truck Reg</th>
                            <th style="text-align: center" id="CantidadEntregada">Cantidad Entregada</th>
                        </tr>
                    <tr>
                        <td style="text-align: center""><%# Eval("idSucursal") %></td>
                        <td style="text-align: center"> <%# Eval("orden") %></td>
                        <td style="text-align: center" colspan="3"><%# codigoNombre(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center" colspan="2"><%# direccionCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center" colspan="2"><%# codigoProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center" colspan="2"><%# descProducto(int.Parse(Eval("idProducto").ToString())) %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"><%# Eval("fechaalta").ToString().Remove(0, 10) %></td>
                        <td style="text-align: center"><%# Eval("hora") %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"><%# Eval("Cantidad")%></td>
                        <td style="text-align: center"><%# tipoUnidad(int.Parse(Eval("idUnidadTransporte").ToString())) %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"><%# consultaCantidadEntrega(int.Parse(Eval("orden").ToString()), float.Parse(Eval("CantidadEntregada").ToString()), int.Parse(Eval("ordenD").ToString()), int.Parse(Eval("idDetalleSolicitud").ToString()))%></td>
                    </tr>
                    <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                        <td style="text-align: center" colspan="19">Cantidad Total: <%# Eval("Cantidad") %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lvTotal" runat="server">
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
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center" colspan="2">Promedio tiempo</td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:TextBox ID="txtTotal" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromImpresion" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromOnLoad" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromToJob" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromOnJob" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromPouring" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromWash" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPromLeave" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>