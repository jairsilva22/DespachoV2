<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="asignarChofer.aspx.cs" Inherits="despacho.asignarChofer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    <style type="text/css">
        .auto-style2 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    &nbsp;<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Asignar Chofer</h3>
                    <p>/ <a href="unidadesTransporte.aspx">Unidades de Transporte</a></p>
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
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <table class="nav-justified">
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Unidad:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblNombre" runat="server" class="form-control"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Chofer:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlChofer" runat="server" AutoPostBack="True" class="form-control" name="chofer">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chbxActivo" runat="server" Text="Activo" Checked="True" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click" />
                                                            &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-secondary" Text="Volver" OnClick="btnCancelar_Click" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" class="auto-style2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <!-- Row Starts -->
                                                            <asp:UpdatePanel ID="upGrid" runat="server">
                                                                <ContentTemplate>
                                                                    <div id="ContentPlaceHolder_upGrid">
                                                                        <div class="row gutter">
                                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                                <div class="panel panel-blue">
                                                                                    <div class="panel-heading">
                                                                                        <h4>Choferes Asignados</h4>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="table-responsive">
                                                                                            <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand" OnItemDeleting="lvDetalles_ItemDeleting">
                                                                                                <LayoutTemplate>
                                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                                        <thead>
                                                                                                            <tr>
                                                                                                                <th style="text-align: center" id="unidad">unidad</th>
                                                                                                                <th style="text-align: center" id="chAsignados">Choferes Asignados</th>
                                                                                                                <th style="text-align: center" id="activo">Activo</th>
                                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                                                        <tfoot>
                                                                                                            <tr>
                                                                                                                <th style="text-align: center">unidad</th>
                                                                                                                <th style="text-align: center">Choferes Asignados</th>
                                                                                                                <th style="text-align: center">Activo</th>
                                                                                                                <th style="text-align: center">Eliminar</th>
                                                                                                            </tr>
                                                                                                        </tfoot>
                                                                                                    </table>
                                                                                                </LayoutTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td style="text-align: center"><%# Eval("unidad") %></td>
                                                                                                        <td style="text-align: center"><%# Eval("chofer") %></td>
                                                                                                        <td style="text-align: center"><%# mostrar(bool.Parse(Eval("activo").ToString())) %></td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("chofer") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                                <EmptyDataTemplate>
                                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                                        <thead>
                                                                                                            <tr>
                                                                                                                <th style="text-align: center" id="unidad">unidad</th>
                                                                                                                <th style="text-align: center" id="chAsignados">Choferes Asignados</th>
                                                                                                                <th style="text-align: center" id="activo">Activo</th>
                                                                                                                <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tr>
                                                                                                            <td colspan="4">
                                                                                                                <label class="label label-danger">¡No hay Choferes asignados a la Unidad!</label></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </EmptyDataTemplate>
                                                                                            </asp:ListView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <!-- Row Ends -->
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
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
                                                <asp:Label ID="lblReasignar" runat="server" Text="0" Visible="false"></asp:Label>
                                                <asp:Label ID="lblDelete" runat="server" Text="0" Visible="false"></asp:Label>
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
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnAceptar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="mbtnClose" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:HiddenField ID="hfIdChofer" runat="server" />
            <asp:HiddenField ID="hfIdUnidad" runat="server" />
            <asp:HiddenField ID="hfIdAux" runat="server" />
            <asp:HiddenField ID="hfIdUC" runat="server" />
            <asp:HiddenField ID="hfDelete" runat="server" />
            <asp:HiddenField ID="hfReasignar" runat="server" />
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
