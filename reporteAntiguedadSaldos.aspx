<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteAntiguedadSaldos.aspx.cs" Inherits="despacho.reporteAntiguedadSaldos2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Antiguedad de Saldos</title>
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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de Antiguedad de Saldos y Pronostico de Cobranza de Cliente</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha de Corte: </strong></asp:Label>
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
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>

            <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Codigo">Codigo</th>
                            <th style="text-align: center" id="Nombre">Nombre(Cliente)</th>
                            <th style="text-align: center" id="TotalVencido">Total Vencido</th>
                            <th style="text-align: center" id="Vendedor">VENDEDOR</th>
                            <th style="text-align: center" id="Vencimiento">VENCIMIENTO</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Codigo">Codigo</th>
                            <th style="text-align: center" id="Nombre">Nombre(Cliente)</th>
                            <th style="text-align: center" id="TotalVencido">Total Vencido</th>
                            <th style="text-align: center" id="Vendedor">VENDEDOR</th>
                            <th style="text-align: center" id="Vencimiento">VENCIMIENTO</th>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("Codigo").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Nombre").ToString() %></td>
                        <td style="text-align: center"><%# Eval("TotalVencido").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Vendedor").ToString() %></td>
                        <td style="text-align: center"><%# Eval("Vencimiento").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>


        </div>
        <div>
        </div>
    </form>
</body>
</html>
