<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCuentasPorPagarAProveedores.aspx.cs" Inherits="despacho.ReporteCuentasPorPagarAProvedores" %>

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
                                    <asp:Label ID="lblReporte" runat="server">Reporte de cuentas por pagar a proveedores</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                     <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
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
                            <th style="text-align: center" id="Codigo">FECHA</th>
                            <th style="text-align: center" id="Nombre">FACTURA</th>
                            <th style="text-align: center" colspan="2" id="TotalVencido">PROVEEDOR</th>
                            <th style="text-align: center" id="Vendedor">CONCEPTO</th>
                            <th style="text-align: center" id="Vencimiento">SUBTOTAL</th>
                            <th style="text-align: center" id="">IVA</th>
                            <th style="text-align: center" id="">ISR RE</th>
                            <th style="text-align: center" id="">TOTAL</th>
                            <th style="text-align: center" id="">MONEDA</th>
                            <th style="text-align: center" id="">FECHA DE PAGO</th>
                            <th style="text-align: center" id="">BANCO</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Codigo">FECHA</th>
                            <th style="text-align: center" id="Nombre">FACTURA</th>
                            <th style="text-align: center" id="TotalVencido">PROVEEDOR</th>
                            <th style="text-align: center" id="Vendedor">CONCEPTO</th>
                            <th style="text-align: center" id="Vencimiento">SUBTOTAL</th>
                            <th style="text-align: center" id="">IVA</th>
                            <th style="text-align: center" id="">ISR RE</th>
                            <th style="text-align: center" id="">TOTAL</th>
                            <th style="text-align: center" id="">MONEDA</th>
                            <th style="text-align: center" id="">FECHA DE PAGO</th>
                            <th style="text-align: center" id="">BANCO</th>
                        </tr>
                        <tr>
                            <td colspan="11" align="center">Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("CFECHA").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CFOLIO").ToString() %></td>
                        <td style="text-align: center" colspan="2"><%# Eval("CRAZONSOCIAL").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CDENCOMERCIAL").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CNETO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CIMPUESTO1").ToString() %></td>
                        <td style="text-align: center"><%--<%# Eval("isr").ToString() %>--%></td>
                        <td style="text-align: center"><%# Eval("CTOTAL").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CCLAVESAT").ToString() %></td>
                        <td style="text-align: center"><%--<%# Eval("CFECHAULTIMOINTERES").ToString() %>--%></td>
                        <td style="text-align: center"><%# Eval("CNOMBRECUENTA").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>


        </div>
        <div>

            <asp:Label ID="labelError" runat="server" />
        </div>
    </form>
</body>
</html>
