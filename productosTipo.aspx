﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productosTipo.aspx.cs" Inherits="despacho.productosTipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Tipo de Productos</h3>
                    <p><a href="home.aspx">Home</a></p>
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
                    <li>
                        <a href="productosTipoAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div id="ContentPlaceHolder_upGrid">
                <div class="row gutter">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-blue">
                            <div class="panel-heading">
                                <h4>Tipos de Productos</h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                        <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                        <th style="text-align: center" id="cat">Categorías</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Tipo</th>
                                                        <th style="text-align: center">Revenimiento</th>
                                                        <th style="text-align: center">Categorías</th>
                                                        <th style="text-align: center">Modificar</th>
                                                        <th style="text-align: center">Eliminar</th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("tipo") %></td>
                                                <td style="text-align: center">
                                                    <%# ((bool)Eval("revenimiento") == true) ? "Sí" : "No" %>
                                                </td>
                                                <td style="text-align: center">
                                                    <a href="categoriasProducto.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-chart-alt"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <a href="productosTipoMod.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-new-message"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("tipo") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                        <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                        <th style="text-align: center" id="cat">Categorías</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="5">
                                                        <label class="label label-danger">¡No hay Tipos de Productos Registrados!</label></td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                                <!-- Button trigger modal -->
                                <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                                    data-toggle="modal" data-target="#myModal">
                                    Launch demo modal
                                </button>

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
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Row Ends -->

</asp:Content>