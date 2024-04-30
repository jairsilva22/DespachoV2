<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="categoriasProducto.aspx.cs" Inherits="despacho.categoriasProducto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    &nbsp;<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Categorías</h3>
                    <p>/ <a href="productosTipo.aspx">Tipos de Productos</a></p>
                </div>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4></h4>
                </div>
                <asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Tipo de Producto:
                                            <asp:Label ID="lblTipoProducto" runat="server" class="form-control" name="TipoProducto"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-4">
                                            Nombre de la categoría:
                                            <asp:TextBox ID="txtNombre" runat="server" class="form-control" name="categoria"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <br />
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <br />
                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" class="btn btn-info"/>
                                            <asp:Button ID="btnVolver" runat="server" class="btn btn-info" PostBackUrl="~/productosTipo.aspx" Text="Volver" />
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="upGrid" runat="server">
                                                <ContentTemplate>
                                                    <div id="ContentPlaceHolder_upGrid">
                                                        <div class="row gutter">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="panel panel-blue">
                                                                    <div class="panel-heading">
                                                                        <h4></h4>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="table-responsive">
                                                                            <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand" OnItemDeleting="lvDetalles_ItemDeleting" OnItemUpdating="lvDetalles_ItemUpdating">
                                                                                <LayoutTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="nombre">Categoría</th>
                                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                                        <tfoot>
                                                                                            <tr>
                                                                                                <th style="text-align: center">Categoría</th>
                                                                                                <th style="text-align: center">Modificar</th>
                                                                                                <th style="text-align: center">Eliminar</th>
                                                                                            </tr>
                                                                                        </tfoot>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center"><%# Eval("nombre") %></td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lbtnModificar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("nombre") %>' CommandName="update"><i class="icon-new-message"></i></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("nombre") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <EmptyDataTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="nombre">Categoría</th>
                                                                                                <th style="text-align: center" id="modificar">Modificar</th>
                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <label class="label label-danger">¡El Tipo de producto no tiene Categorías!</label></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                    <asp:HiddenField ID="hfIdCat" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </form>
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
                        </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnAceptar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnClose" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemUpdating" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
