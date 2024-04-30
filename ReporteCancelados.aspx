<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ReporteCancelados.aspx.cs" Inherits="despacho.ReporteCancelados" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte Documentos Cancelados</title>
    <style type="text/css">
        <!--
        .titulos {
            font-size: 20px;
            font-weight: bold;
        }
        -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:95%; text-align:center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div runat="server" style="">
                            <div id="imagen" runat="server"> </div>
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
                    <td width="40%" valign="bottom">Fecha Impresion : <%= DateTime.Now.ToShortDateString() %></td>
                </tr>
                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>
            <asp:ListView ID="ListView1" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <td>Folio</td>
                            <td>R.F.C.</td>
                            <td>Cliente</td>
                            <td>Tipo documento</td>
                            <td>Total</td>
                            <td>Estatus</td>
                            <td>Fecha Peticion</td>
                            <td>Fecha Cancelacion</td>
                            <td>Observaciones</td>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <td>Folio</td>
                            <td>R.F.C.</td>
                            <td>Cliente</td>
                            <td>Tipo documento</td>
                            <td>Total</td>
                            <td>Estatus</td>
                            <td>Fecha Peticion</td>
                            <td>Fecha Cancelacion</td>
                            <td>Observaciones</td>
                        </tr>
                        <tr>
                            <td colspan="9" align="center">
                                Sin Resultados
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("folio") %></td>
                        <td><%# Eval("rfcCliente") %></td>
                        <td><%# Eval("nombreCliente") %></td>
                        <td><%# Eval("descripcion") %></td>
                        <td><%# String.Format("{0:c}", Eval("total")) %></td>
                        <td><%# estado(Eval("codigoCan").ToString()) %></td>
                        <td><%# Eval("fechaSolicitud") %></td>
                        <td><%# Eval("fechaCancelado") %></td>
                        <td><%# Eval("observaciones") %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
