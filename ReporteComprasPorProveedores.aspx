<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteComprasPorProveedores.aspx.cs" Inherits="despacho.ReporteComprasPorProveedores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reporte Compras Por Proveedores</title>
     <style type="text/css">
        
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
        
    </style>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.js"></script>
    
    <!-- AG-Grid -->
    <script src="js/AG-Grid/ag-grid-enterprise.js"></script>
    <link href="css/reportesAgGrid.css" rel="stylesheet" />
    <script src="js/AG-Grid/reporteComprasProveedores.js"></script>
</head>
<body>
     <form id="form1" runat="server">
         <div class="loader-wrapper">
             <span class="loader"></span>
         </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 95%; text-align: center;">
            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr align="center" valign="bottom">
                    <td colspan="2" valign="middle" class="titulos">
                        <div style="display: flex; justify-content: space-between" runat="server">
                            <div id="imagen" runat="server"></div>
                            <div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblReporte" runat="server">Reporte Compras Por Proveedores</asp:Label>
                                </div>
                                <div class="text-center" style="margin-bottom: 10px">
                                    <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                                    <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                                </div>
                                <%--<div class="text-center" style="margin-bottom: 10px">
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
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr class="stylo1">
                    <td colspan="2"></td>
                </tr>
            </table>
            <div class="col-md-12">
                <input type="text" id="filter-text-box" class="form-control" placeholder="Buscar..." oninput="onFilterTextBoxChanged()" autocomplete="off" />
                <br />
                <div id="myGrid" style="height:600px;" class="ag-theme-alpine col-md-12"></div>
            </div>
            <div class="col-md-12">
                <table class="table" style="width: 99%;">
                    <tr>
                        <td colspan="4"></td>
                        <td><span class="titulos">Total General</span></td>
                        <td><asp:Label ID="lblTotal" runat="server" Text="1" CssClass="size20"></asp:Label></td>
                        <td></td>
                    </tr>
                </table>
            </div>
            <div style="display:none;">
                <asp:ListView ID="lvCliente" runat="server">
                <LayoutTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Factura">Factura</th>
                            <th style="text-align: center" id="Proveedor">Proveedor</th>
                            <th style="text-align: center" id="Concepto">Concepto</th>
                            <%--<th style="text-align: center" id="Subtotal">Subtotal</th>--%>
                            <%--<th style="text-align: center" id="Iva">Iva</th>--%>
                            <%--<th style="text-align: center" id="ISR">Isr Ret</th>--%>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Total">Total</th>
                            <th style="text-align: center" id="Moneda">Moneda</th>
                            <%--<th style="text-align: center" id="FehaPago">Fecha de Pago</th>--%>
                            <%--<th style="text-align: center" id="Banco">Banco</th>--%>
                            <th style="text-align: center" id="Sucursal">Sucursal</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="" id="Vacio">
                        <tr align="center" bgcolor="#e33045" style="color: white" class="stylo1">
                            <th style="text-align: center" id="Fecha">Fecha</th>
                            <th style="text-align: center" id="Factura">Factura</th>
                            <th style="text-align: center" id="Proveedor">Proveedor</th>
                            <th style="text-align: center" id="Concepto">Concepto</th>
                            <%--<th style="text-align: center" id="Subtotal">Subtotal</th>--%>
                            <%--<th style="text-align: center" id="Iva">Iva</th>--%>
                            <%--<th style="text-align: center" id="ISR">Isr Ret</th>--%>
                            <th style="text-align: center" id="Unidad">Unidad</th>
                            <th style="text-align: center" id="Total">Total</th>
                            <th style="text-align: center" id="Moneda">Moneda</th>
                            <%--<th style="text-align: center" id="FehaPago">Fecha de Pago</th>--%>
                            <%--<th style="text-align: center" id="Banco">Banco</th>--%>
                            <th style="text-align: center" id="Sucursal">Sucursal</th>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">Sin Resultados
                            </td>

                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center"><%# Eval("FECHA").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CFOLIO").ToString() %></td>
                        <td style="text-align: center"><%# Eval("PROVEEDOR").ToString() %></td>
                        <td style="text-align: center"><%# Eval("CONCEPTO").ToString() %></td>
                        <%--<td style="text-align: center"><%# Eval("SUBTOTAL").ToString() %></td>--%>
                        <%--<td style="text-align: center"><%# Eval("IVA").ToString() %></td>--%>
                        <%--<td style="text-align: center"><%# Eval("ISR").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("UNIDAD").ToString() %></td>
                        <td style="text-align: center"><%# Eval("TOTAL").ToString() %></td>
                        <td style="text-align: center"><%# Eval("MONEDA").ToString() %></td>
                         <%--<td style="text-align: center"><%# Eval("FECHAPAGO").ToString() %></td>--%>
                        <%--<td style="text-align: center"><%# Eval("BANCO").ToString() %></td>--%>
                        <td style="text-align: center"><%# Eval("SUCURSAL").ToString() %></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            </div>
        </div>
        <asp:Label ID="labelError1" runat="server" />
         <script>
             function onFilterTextBoxChanged() {
                 gridOptions.api.setQuickFilter(
                     document.getElementById('filter-text-box').value
                 );
             }

             function llenarAgGrid(res) {
                 const rowDataB = res;

                 $(document).ready(function () {
                     gridOptions.api.setRowData(rowDataB);
                     var alto = 42 * rowDataB.length + 70;
                     document.getElementById("myGrid").style.height = alto + "px";
                     document.getElementById("myGrid").style.maxHeight = "600px";
                     document.getElementById("myGrid").style.minHeight = "100px";

                 });

             }
         </script>
         <script src="js/loader/loader.js"></script>
    </form>
</body>
</html>