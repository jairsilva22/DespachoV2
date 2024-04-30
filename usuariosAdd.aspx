<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usuariosAdd.aspx.cs" Inherits="despacho.usuariosAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    <style type="text/css">
        .auto-style2 {
            height: 22px;
        }
        .auto-style3 {
            height: 37px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Usuario</h3>
                    <p>/ <a href="usuarios.aspx">Usuarios</a></p>
                </div>
            </div>
            <%--<asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataSourceID="dsTipoProducto" DataTextField="tipo" DataValueField="id" ></asp:DropDownList>
                                                                                            <asp:SqlDataSource ID="dsTipoProducto" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [tiposProductos]"></asp:SqlDataSource>--%>
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
                                                                    Usuario:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Nombre:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtUsuario" runat="server" class="form-control" name="clave"></asp:TextBox>
                                                            <%--<ajaxToolkit:AutoCompleteExtender ID="txtUsuario_AutoCompleteExtender" runat="server" BehaviorID="txtUsuario_AutoCompleteExtender" CompletionSetCount="1" DelimiterCharacters="" MinimumPrefixLength="1" ServiceMethod="getDataUsuario" ServicePath="" TargetControlID="txtUsuario" UseContextKey="True">
                                                            </ajaxToolkit:AutoCompleteExtender>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombre" runat="server" class="form-control" OnTextChanged="txtNombre_TextChanged"></asp:TextBox>
                                                            <%--<ajaxToolkit:AutoCompleteExtender ID="txtNombre_AutoCompleteExtender" runat="server" BehaviorID="txtNombre_AutoCompleteExtender" CompletionSetCount="1" DelimiterCharacters="" MinimumPrefixLength="1" ServicePath="" TargetControlID="txtNombre" UseContextKey="True">
                                                            </ajaxToolkit:AutoCompleteExtender>--%>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style3"></td>
                                                        <td class="auto-style3">
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Contraseña:
                                                                    <asp:Button ID="btnVer1" runat="server" Text="Ver" class="btn btn-info" OnClick="btnVer1_Click" Visible="False" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td class="auto-style3">
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Confirmar contraseña:
                                                                    <asp:Button ID="btnVer2" runat="server" Text="Ver" class="btn btn-info" OnClick="btnVer2_Click" Visible="False"/>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td class="auto-style3"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPasswd1" runat="server" class="form-control" type="password"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPasswd2" runat="server" class="form-control" type="password"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Teléfono:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Email:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:TextBox ID="txtTelefono" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    Perfil:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPerfil" runat="server" class="form-control" name="perfil">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-4">
                                                                    &nbsp;</div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar" class="btn btn-info" />
                                                            &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-info" Text="Volver" OnClick="btnCancelar_Click" />
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" class="auto-style2">
                                                            <asp:HiddenField ID="hfIdTurno" runat="server" />
                                                            <asp:HiddenField ID="hfIdPerfil" runat="server" />
                                                            <asp:HiddenField ID="hdPwd" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnlDetalleTitulo" runat="server" Visible="false">
                                                                <div class="row gutter">
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-4">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
