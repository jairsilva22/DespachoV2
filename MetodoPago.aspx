<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MetodoPago.aspx.cs" Inherits="despacho.MetodoPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Métodos de Pago</h3>
                    <p><a href="home.aspx">Home</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <li>
                        <a href="MetodoPagoAdd.aspx" class="btn btn-info">
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
                        <asp:ListView ID="lvMetodoPago" runat="server">
                            <LayoutTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th style="text-align: center">Clave Método de Pago</th>
                                            <th style="text-align: center">Descripción</th>
                                            <th style="text-align: center">Modificar</th>
                                            <th style="text-align: center">Eliminar</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                    <tfoot>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th style="text-align: center">Clave Método de Pago</th>
                                            <th style="text-align: center">Descripción</th>
                                            <th style="text-align: center">Modificar</th>
                                            <th style="text-align: center">Eliminar</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                    <td style="text-align: center"><%# Eval("forma_pago").ToString() %></td>
                                    <td style="text-align: center"><%# Eval("descripcion") %></td>
                                    <td style="text-align: center">
                                        <asp:LinkButton ID="btnMod" runat="server" CommandArgument='<%# Eval("idPago") + ";" + Eval("forma_pago") %>' CommandName="modificar" OnCommand="btnMod_ItemCommand"><i class="icon-new-message"></i></asp:LinkButton>

                                    </td>
                                    <td style="text-align: center">
                                        <asp:LinkButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("idPago") + ";" + Eval("forma_pago") %>' CommandName="eliminar" OnCommand="btnMod_ItemCommand"><i class="icon-delete"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th style="text-align: center">Clave Método de Pago</th>
                                            <th style="text-align: center">Descripción</th>
                                            <th style="text-align: center">Modificar</th>
                                            <th style="text-align: center">Eliminar</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="14" class="text-center">
                                            <label class="label label-danger">¡No hay Métodos de Pago Registrados!</label></td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>

                <!-- Modal -->
                <div class="modal fade" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">
                                    <asp:Label ID="mlblTitle" runat="server" />
                                </h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="mlblMessage" runat="server" />
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
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
        <asp:HiddenField ID="hfId" runat="server" Value="0" />
    </div>
</asp:Content>
