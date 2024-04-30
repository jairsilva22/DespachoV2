<%@ Page Title="" Language="C#" MasterPageFile="~/PopUP.Master" AutoEventWireup="true" CodeBehind="pAsignarUnidad.aspx.cs" Inherits="despacho.pAsignarUnidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                                    <table>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="2">
                                                <div class="row gutter">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div class="form-group">
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <asp:Label ID="Label1" runat="server" Text="Orden de Dosificación: "></asp:Label>
                                                                        <asp:Label ID="lblOD" runat="server"></asp:Label>
                                                                        <asp:TextBox ID="txtIdOD" runat="server" Visible="false"></asp:TextBox>
                                                                        <br />
                                                                        <div class="panel-body">

                                                                            <div class="table-responsive" style="width:100%">
                                                                                <asp:Label ID="lblIDRemision" runat="server" Text=""></asp:Label>
                                                                                <asp:ListView ID="lvUT" runat="server" ItemPlaceholderID="itemPlaceHolderTU">
                                                                                    <LayoutTemplate>
                                                                                        <table id="OrderTable" class="table no-margin" style="width:100%">
                                                                                            <thead>
                                                                                                <tr style="width:100%">
                                                                                                    <th style="text-align: center">ID</th>
                                                                                                    <th style="text-align: center">Unidad</th>
                                                                                                    <th style="text-align: center">Tipo de Unidad</th>
                                                                                                    <th style="text-align: center">Asignar</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:PlaceHolder ID="itemPlaceHolderTU" runat="server"></asp:PlaceHolder>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr style="width:100%">
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblIDUnidad" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center"><i class="icon-truck2"></i>
                                                                                                <asp:LinkButton ID="lbtnUnidadT" runat="server" Text='<%# Eval("nombre") %>'></asp:LinkButton>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblTipoUnidad" runat="server" Text='<%# Eval("tipo") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:LinkButton ID="lBtnAsignar" runat="server" CommandName="xAssignUnit" CommandArgument='<%# Eval("id") %>' OnCommand="lBtnAsignar_Command">Asignar</asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <EmptyDataTemplate>
                                                                                        <table id="responsiveTable0" class="table table-striped table-bordered no-margin">
                                                                                            <thead>
                                                                                                <tr style="width:100%">
                                                                                                    <th style="text-align: center">ID</th>
                                                                                                    <th style="text-align: center">Unidad</th>
                                                                                                    <th style="text-align: center">Tipo de Unidad</th>
                                                                                                    <th style="text-align: center">Asignar</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tr>
                                                                                                <td colspan="4">
                                                                                                    <label class="label label-danger">
                                                                                                        ¡No hay Unidades disponibles!</label></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </EmptyDataTemplate>
                                                                                </asp:ListView>
                                                                                <br />
                                                                                <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
