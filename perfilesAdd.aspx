<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="perfilesAdd.aspx.cs" Inherits="despacho.perfilesAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Agregar Perfíl</h3>
                    <p> / <a href="perfiles.aspx">Perfiles</a></p>
                </div>
            </div>
            <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <li>
                        <a href="javascript:void(0)" class="btn btn-danger"><span>76</span>Sales</a>
                    </li>
                    <li>
                        <a href="tasks.html" class="btn btn-success">
                            <span>18</span>Tasks</a>
                    </li>
                    <li>
                        <a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i>Export</a>
                    </li>
                </ul>
            </div>--%>
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
                                        <asp:UpdatePanel ID="upAddMaterial" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="form-group">
                                                        <div class="row gutter">
                                                            <div class="col-md-8">
                                                                Descripción:
                                                        <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" name="descripcion"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Éste dato es requerido" ControlToValidate="txtDescripcion" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row gutter">
                                                            <div class="col-md-4 selectContainer">
                                                                <asp:CheckBox ID="chbxActivo" runat="server" class="radio-inline" Text="Activo" TextAlign="Left" Checked="True"/>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar" class="btn btn-default" OnClick="btnAgregar_Click" />
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
