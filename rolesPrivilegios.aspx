<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rolesPrivilegios.aspx.cs" Inherits="despacho.rolesPrivilegios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <%--<th>URL</th>
                                                                                <th style="text-align: center">Orden</th>--%>
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4>
                        <asp:Label ID="lblPerfil" runat="server" Font-Bold="True" Font-Size="XX-Large" Width="100%"></asp:Label>
                    </h4>
                </div>
                <asp:UpdatePanel ID="upForm" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-5">
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-5">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-8">
                                                        <div class="table-responsive">
                                                            <asp:ListView ID="listView" runat="server">
                                                                <LayoutTemplate>
                                                                    <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                                        <thead>
                                                                            <tr>
                                                                                <%--<th style="text-align: center" id="id">ID</th>--%>
                                                                                <%--<th style="text-align: center" id="idMenuPadre">Id Padre</th>--%>
                                                                                <th id="etiqueta">Etiqueta</th>
                                                                                <%--<th id="url">URL</th>--%>
                                                                                <%--<th style="text-align: center" id="orden">Orden</th>--%>
                                                                                <th style="text-align: center" id="activo">Activo<br />
                                                                                    <asp:CheckBox ID="chbxTodos" runat="server" Checked="false" Visible="false" OnCheckedChanged="chbxTodos_CheckedChanged" />
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <%--<th style="text-align: center">ID</th>--%>
                                                                                <%--<th style="text-align: center">Id Padre</th>--%>
                                                                                <th>Etiqueta</th>
                                                                                <%--<th>URL</th>
                                                                                <th style="text-align: center">Orden</th>--%>
                                                                                <th style="text-align: center">Activo</th>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <%--<td style="text-align: center">
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("idPr") %>' Visible="true"></asp:Label></td>--%>
                                                                        <%--<td style="text-align: center"><%# Eval("idMenuPadre") %></td>--%>
                                                                        <td><%# Eval("etiqueta") %></td>
                                                                        <%--<td><%# Eval("url") %></td>
                                                                        <td style="text-align: center"><%# Eval("orden") %></td>--%>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="chbxPrivilegio" runat="server" Checked='<%# (bool)Eval("activo") %>' OnCheckedChanged="chbxPrivilegio_CheckedChanged" CommandName='<%#Eval("idPr") + "ˇ" + Eval("idM")%>' AutoPostBack="True" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                        <thead>
                                                                            <tr>
                                                                                <%--<th style="text-align: center" id="id">ID</th>--%>
                                                                                <%--<th style="text-align: center" id="idMenuPadre">Id Padre</th>--%>
                                                                                <th id="etiqueta">Etiqueta</th>
                                                                                <%--<th id="url">URL</th>
                                                                                <th style="text-align: center" id="orden">Orden</th>--%>
                                                                                <th style="text-align: center" id="activo">Activo</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <label class="label label-danger">¡No hay Privilegios Registrados!</label></td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
