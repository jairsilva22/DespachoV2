<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Folios.aspx.cs" Inherits="despacho.Folios1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Folios</h3>
                    <p><a href="home.aspx">Home</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <li>
                        <a href="FoliosAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>

                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->


    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:ListView ID="lvFolios" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th style="text-align: center">Folio Inicial</th>
                                            <th style="text-align: center">Folio Final</th>
                                            <th style="text-align: center">Folio Activo</th>
                                            <th style="text-align: center">Serie</th>
                                            <th style="text-align: center">Solicitudes</th>
                                            <th style="text-align: center">Ordenes</th>
                                            <th style="text-align: center">Remisiones</th>
                                            <th style="text-align: center">Factura</th>
                                            <th style="text-align: center">Nota de Crédito</th>
                                            <th style="text-align: center">Nota de Cargo</th>
                                            <th style="text-align: center">Complemento de Pago</th>
                                            <th style="text-align: center">Pagos Finanzas</th>
                                            <th style="text-align: center">Pagos Vendedores</th>
                                            <th style="text-align: center">Modificar</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                             <th style="text-align: center">#</th>
                                            <th style="text-align: center">Folio Inicial</th>
                                            <th style="text-align: center">Folio Final</th>
                                            <th style="text-align: center">Folio Activo</th>
                                            <th style="text-align: center">Serie</th>
                                            <th style="text-align: center">Solicitudes</th>
                                            <th style="text-align: center">Ordenes</th>
                                            <th style="text-align: center">Remisiones</th>
                                            <th style="text-align: center">Factura</th>
                                            <th style="text-align: center">Nota de Crédito</th>
                                            <th style="text-align: center">Nota de Cargo</th>
                                            <th style="text-align: center">Complemento de Pago</th>
                                            <th style="text-align: center">Pagos Finanzas</th>
                                            <th style="text-align: center">Pagos Vendedores</th>
                                            <th style="text-align: center">Modificar</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                    <td style="text-align: center"><%# Eval("folioInicio").ToString() %></td>
                                    <td style="text-align: center"><%# Eval("folioFinal") %></td>
                                    <td style="text-align: center"><%# Eval("folioActivo") %></td>
                                    <td style="text-align: center"><%# Eval("serie") %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("solicitudes").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("ordenes").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("remisiones").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("factura").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("nCredito").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("nCargo").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("cPago").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("pagosFinanzas").ToString())) %></td>
                                    <td style="text-align: center"><%# mostrar(bool.Parse(Eval("pagosVendedor").ToString())) %></td>
                                    <td style="text-align: center">
                                        <asp:LinkButton ID="btnMod" runat="server" CommandArgument='<%# Eval("idLogs") %>' CommandName="modificar" OnCommand="btnMod_ItemCommand"><i class="icon-new-message"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th style="text-align: center">Folio Inicial</th>
                                            <th style="text-align: center">Folio Final</th>
                                            <th style="text-align: center">Folio Activo</th>
                                            <th style="text-align: center">Serie</th>
                                            <th style="text-align: center">Solicitudes</th>
                                            <th style="text-align: center">Ordenes</th>
                                            <th style="text-align: center">Remisiones</th>
                                            <th style="text-align: center">Factura</th>
                                            <th style="text-align: center">Nota de Crédito</th>
                                            <th style="text-align: center">Nota de Cargo</th>
                                            <th style="text-align: center">Complemento de Pago</th>
                                            <th style="text-align: center">Pagos Finanzas</th>
                                            <th style="text-align: center">Pagos Vendedores</th>
                                            <th style="text-align: center">Modificar</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="15" class="text-center">
                                            <label class="label label-danger">¡No hay Folios Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfId" runat="server" Value="0" />
    </div>
</asp:Content>
