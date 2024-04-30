<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="choferes.aspx.cs" Inherits="despacho.choferes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Choferes</h3>
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
                        <%--<a href="usuariosAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>--%>
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
                                <h4>Choferes</h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="usuario">Usuario</th>
                                                        <th id="nombre">Nombre</th>
                                                        <th style="text-align: center" id="idTurno">Turno</th>
                                                        <th style="text-align: center" id="idPerfil">Perfil</th>
                                                        <th style="text-align: center" id="activo">Activo en Unidad</th>
                                                        <th style="text-align: center" id="unidad">Asignado a Unidad</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Usuario</th>
                                                        <th>Nombre</th>
                                                        <th style="text-align: center">Turno</th>
                                                        <th style="text-align: center">Perfil</th>
                                                        <th style="text-align: center">Activo en Unidad</th>
                                                        <th style="text-align: center">Asignado a Unidad</th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("usuario") %></td>
                                                <td><%# Eval("nombre") %></td>
                                                <td style="text-align: center"><%# Eval("turno") %></td>
                                                <td style="text-align: center"><%# Eval("perfil") %></td>
                                                <td style="text-align: center">
                                                    <%# ((bool)Eval("activo") == true) ? "Sí" : "No" %></td>
                                                <td style="text-align: center"><%# Eval("unidad") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="usuario">Usuario</th>
                                                        <th id="nombre">Nombre</th>
                                                        <th style="text-align: center" id="idTurno">Turno</th>
                                                        <th style="text-align: center" id="idPerfil">Perfil</th>
                                                        <th style="text-align: center" id="activo">Activo en Unidad</th>
                                                        <th style="text-align: center" id="unidad">Asignado a Unidad</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="6">
                                                        <label class="label label-danger">¡No hay Choferes Registrados!</label></td>
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
