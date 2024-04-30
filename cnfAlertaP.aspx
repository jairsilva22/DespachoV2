<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cnfAlertaP.aspx.cs" Inherits="despacho.cnfAlertaP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Alertas de Programación</h3>
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
                        <a href="cnfAlertaPAdd.aspx" class="btn btn-info">
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
                                <h4>Unidades de Transporte</h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting" OnSelectedIndexChanged="listView_SelectedIndexChanged">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="tiempo">Tiempo (Minutos)</th>
                                                        <th style="text-align: center" id="color">Color</th>
                                                        <th style="text-align: center" id="sucursal">Sucursal</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Tiempo (Minutos)</th>
                                                        <th style="text-align: center">Color</th>
                                                        <th style="text-align: center">Sucursal</th>
                                                        <th style="text-align: center">Modificar</th>
                                                        <th style="text-align: center">Eliminar</th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("tiempo") %></td>
                                                <td style="text-align: center; background-color:<%# Eval("color") %>"><%# Eval("colorNombre") %></td>
                                                <td style="text-align: center"><%# Eval("nombre") %></td>
                                                <td style="text-align: center">
                                                    <a href="cnfAlertaPMod.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-new-message"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="delete" ><i class="icon-delete"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="tiempo">Tiempo (Minutos)</th>
                                                        <th style="text-align: center" id="color">Color</th>
                                                        <th style="text-align: center" id="sucursal">Sucursal</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="6">
                                                        <label class="label label-danger">¡No hay Alertas Registradas!</label></td>
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

</asp:Content>
