<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteEstatusTicket.aspx.cs" Inherits="despacho.reporteEstatusTicket" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Estatus Ticket</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Estatus Ticket</asp:Label>
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
            <asp:ListView ID="lvTicket" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <%--<tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">--%>
                            <%--<th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Impresion">Impresion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Conductor">Conductor</th>
                            <th style="text-align: center" id="Mezcla">Mezcla</th>--%>
                        <%--</tr>--%>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <%--<th style="text-align: center" id="Ticket">Ticket</th>
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Orden">Orden</th>
                            <th style="text-align: center" id="Camion">Camion</th>
                            <th style="text-align: center" id="Planta">Planta</th>
                            <th style="text-align: center" id="Cantidad">Cantidad</th>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Impresion">Impresion</th>
                            <th style="text-align: center" id="Cliente">Cliente</th>
                            <th style="text-align: center" id="Conductor">Conductor</th>
                            <th style="text-align: center" id="Mezcla">Mezcla</th>--%>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center" colspan="2">Fecha: <%# Eval("fecha").ToString().Substring(0, 10) %></td>
                        <td style="text-align: center" colspan="2">Orden#: <%# Eval("id") %></td>
                        <td style="text-align: center" colspan="2">Cantidad programada: <%# Eval("Cantidad")%></td>
                        <td style="text-align: center" colspan="2">Cantidad entregada</td>
                        <br />
                        <td style="text-align: center" colspan="2">Cliente: <%# nombreCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                        <td style="text-align: center"colspan="2">Direccion: <%# direccionCliente(int.Parse(Eval("idCliente").ToString())) %></td>
                    </tr>
                    <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                        <th style="text-align: center" id="Ticket">Ticket</th>
                        <th style="text-align: center" id="Camion">Camion</th>
                        <th style="text-align: center" id="Impresion">Impresion</th>
                        <th style="text-align: center" id="Aprobado">Aprobado</th>
                        <th style="text-align: center" id="Altrabajo">Al trabajo</th>
                        <th style="text-align: center" id="Entrabajo">En trabajo</th>
                        <th style="text-align: center" id="Dosificado">Dosificado</th>
                        <th style="text-align: center" id="Camino">Camino</th>
                        <th style="text-align: center" id="Cliente">Cliente</th>
                        <th style="text-align: center" id="Regreso">Regreso</th>
                    </tr>
                    <tr>
                        <td style="text-align: center"><%# ordenesDosificacion(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# nombreUnidad(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# impresionOrden(int.Parse(Eval("id").ToString())) %> </td>
                        <td style="text-align: center"><%# onLoadEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# toJobEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# onJobEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# pouringEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# washEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# leaveJobEstatus(int.Parse(Eval("id").ToString())) %></td>
                        <td style="text-align: center"><%# arrivePlantEstatus(int.Parse(Eval("id").ToString())) %></td>
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
                        <td style="text-align: center"><%# promedioImpresion() %></td>
                        <td style="text-align: center"><%# promedioOnLoad() %></td>
                        <td style="text-align: center"><%# promedioToJob() %></td>
                        <td style="text-align: center"><%# promedioOnJob() %></td>
                        <td style="text-align: center"><%# promedioPouring() %></td>
                        <td style="text-align: center"><%# promedioWash() %></td>
                        <td style="text-align: center"><%# promedioLeave() %></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
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
