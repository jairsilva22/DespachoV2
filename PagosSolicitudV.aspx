<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagosSolicitudV.aspx.cs" Inherits="despacho.PagosSolicitudV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Pagos de Solicitud</h3>
                    <p>/ <a href="cvendedores.aspx">Cobranza Vendedor</a> / Pagos de Solicitud</p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <li>
                        <a href="PagosSolicitudVAdd.aspx?idSolicitud=<%= Request.QueryString["idSolicitud"] %>&Vendedor=<%= Request.QueryString["Vendedor"] %>" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <div class="panel-heading">
                    <h4>Pagos de Solicitud</h4>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFolio" runat="server"></asp:Label>
                        </div>
                        <div class="text-center" style="margin-bottom: 10px; margin-top: 10px;">
                            <asp:Label ID="lblVendedor" runat="server"></asp:Label>
                        </div>
                        <asp:ListView ID="LvPagosVendedor" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folioV">Folio</th>
                                            <th style="text-align: center" id="FechaV">Fecha</th>
                                            <th style="text-align: center" id="MontoV">Monto</th>
                                            <th style="text-align: center" id="SaldoV">Saldo</th>
                                            <th style="text-align: center" id="PagoV">Pago</th>
                                            <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center">Cancelar</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center" id="folioVF">Folio</th>
                                            <th style="text-align: center" id="FechaVF">Fecha</th>
                                            <th style="text-align: center" id="MontoVF">Monto</th>
                                            <th style="text-align: center" id="SaldoVF">Saldo</th>
                                            <th style="text-align: center" id="PagoVF">Pago</th>
                                             <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center" id="Cancelar">Cancelar</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("folio") %></td>
                                    <td style="text-align: center"><%# Eval("fechaPago").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("monto").ToString()) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("saldo").ToString()) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("pago").ToString()) %></td>
                                    <td style="text-align: center"><%# Eval("estatus") %></td>
                                     <td style="text-align: center">
                                         <div <%# mostrarCancelar(Eval("estatus").ToString()) %>>
                                             <asp:LinkButton runat="server" ID="lkCancelar" CommandArgument='<%# Eval("id") %>' OnCommand="lkCancelar_ItemCommand">
                                            <span class="icon-cancel2" style="font-size: 20px"></span>
                                        </asp:LinkButton>
                                        </div>
                                     </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <th style="text-align: center" id="Saldo">Saldo</th>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                             <th style="text-align: center">Estatus</th>
                                            <th style="text-align: center" id="Cancelar">Cancelar</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Pagos Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>

                        
                        <div class="text-center" style="margin-bottom: 10px">
                            <asp:Label ID="lblFinanzas" runat="server"><strong>Finanzas</strong></asp:Label>
                        </div>
                        <asp:ListView ID="lvPagos" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <th style="text-align: center" id="Saldo">Saldo</th>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                            <th style="text-align: center" id="Estatus">Estatus</th>
                                            
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center" id="folioF">Folio</th>
                                            <th style="text-align: center" id="FechaF">Fecha</th>
                                            <th style="text-align: center" id="MontoF">Monto</th>
                                            <th style="text-align: center" id="SaldoF">Saldo</th>
                                            <th style="text-align: center" id="PagoF">Pago</th>
                                            <th style="text-align: center" id="EstatusF">Estatus</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Eval("folio") %></td>
                                    <td style="text-align: center"><%# Eval("fechaPago").ToString().Substring(0, 10) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("monto").ToString()) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("saldo").ToString()) %></td>
                                    <td style="text-align: center"><%# convertir(Eval("pago").ToString()) %></td>
                                    <td style="text-align: center"><%# Eval("estatus") %></td>
                                   
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center" id="folio">Folio</th>
                                            <th style="text-align: center" id="Fecha">Fecha</th>
                                            <th style="text-align: center" id="Monto">Monto</th>
                                            <th style="text-align: center" id="Saldo">Saldo</th>
                                            <th style="text-align: center" id="Pago">Pago</th>
                                            <th style="text-align: center" id="Pago">Estatus</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Pagos Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>

                        <asp:HiddenField runat="server" ID="hdIdPago" />

                        <!-- Modal -->
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">
                                            Cancelar Pago
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <p>¿Seguro(a) que desea cancelar el Pago?</p>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" />
                                        <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                            Close</button>
                                        <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
