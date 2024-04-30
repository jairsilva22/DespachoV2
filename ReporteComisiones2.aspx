<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteComisiones2.aspx.cs" Inherits="despacho.ReporteComisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Reporte de Comisiones</h3>
                    <p>/ <a href="ReporteComisiones.aspx">Reporte de Comisiones </a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <%--<li>
									<a href="javascript:void(0)" class="btn btn-danger"><span>76</span>Sales</a>
								</li>
								<li>
									<a href="tasks.html" class="btn btn-success">
										<span>18</span>Tasks</a>
								</li>--%>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <div class="panel-heading">
                    <h4>Reporte de Comisiones</h4>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFolio" runat="server"></asp:Label>
                        </div>
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblVend" runat="server"><strong>Vendedor: </strong></asp:Label>
                        </div>
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFechaInicio" runat="server"><strong>Fecha Inicio: </strong></asp:Label>
                            <asp:Label ID="lblFechaFin" runat="server"><strong>Fecha Fin: </strong></asp:Label>
                        </div>
                        <asp:ListView ID="lvComisiones" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="Orden">Orden</th>
                                            <th style="text-align: center" id="FechaVenta">Fecha Venta</th>
                                            <th style="text-align: center" id="Cliente">Cliente</th>
                                            <th style="text-align: center" id="Tipo">Tipo</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <th style="text-align: center" id="FechaPago">Fecha Pago</th>
                                            <th style="text-align: center" id="FormaPago">Forma Pago</th>
                                            <th style="text-align: center" id="Factura">Factura</th>
                                            <th style="text-align: center" id="Comisión">Comisión</th>
                                            <th style="text-align: center" id="FueradeFecha">Fuera de Fecha</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center" id="OrdenF">Orden</th>
                                            <th style="text-align: center" id="FechaVentaF">Fecha Venta</th>
                                            <th style="text-align: center" id="ClienteF">Cliente</th>
                                            <th style="text-align: center" id="TipoF">Tipo</th>
                                            <th style="text-align: center" id="MontoF">Monto</th>
                                            <th style="text-align: center" id="FechaPagoF">Fecha Pago</th>
                                            <th style="text-align: center" id="FormaPagoF">Forma Pago</th>
                                            <th style="text-align: center" id="FacturaF">Factura</th>
                                            <th style="text-align: center" id="ComisiónF">Comisión</th>
                                            <th style="text-align: center" id="FueradeFechaF">Fuera de Fecha</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("id") %></td>
                                    <td style="text-align: center"><%# Eval("fecha").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# Eval("nombre") %></td>
                                    <td style="text-align: center"><%# tipo(int.Parse(Eval("idCliente").ToString())) %></td>
                                    <td style="text-align: center">$<%# monto(int.Parse(Eval("idSolicitud").ToString())) %></td>
                                    <td style="text-align: center"><%# fechaPago(int.Parse(Eval("idSolicitud").ToString())) %></td>
                                    <td style="text-align: center"><%# formaPago(int.Parse(Eval("idSolicitud").ToString())) %></td>
                                    <td style="text-align: center"><%# factura(int.Parse(Eval("idSolicitud").ToString())) %></td>
                                    <td style="text-align: center">$<%# comision(int.Parse(Eval("idCliente").ToString()), int.Parse(Eval("idSolicitud").ToString())) %></td>
                                    <td style="text-align: center"><%# fueraDeFecha(int.Parse(Eval("idSolicitud").ToString()), int.Parse(Eval("id").ToString())) %> días</td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="OrdenE">Orden</th>
                                            <th style="text-align: center" id="FechaVentaE">Fecha Venta</th>
                                            <th style="text-align: center" id="ClienteE">Cliente</th>
                                            <th style="text-align: center" id="TipoE">Tipo</th>
                                            <th style="text-align: center" id="MontoE">Monto</th>
                                            <th style="text-align: center" id="FormaPagoE">Forma Pago</th>
                                            <th style="text-align: center" id="FacturaE">Factura</th>
                                            <th style="text-align: center" id="ComisiónE">Comisión</th>
                                            <th style="text-align: center" id="FueradeFechaE">Fuera de Fecha</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Pagos Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
