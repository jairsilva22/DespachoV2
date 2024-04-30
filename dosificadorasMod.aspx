<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dosificadorasMod.aspx.cs" Inherits="despacho.dosificadorasMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Dosificadora</h3>
                    <p>/ <a href="dosificadoras.aspx">Dosificadoras</a></p>
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
                                                <div class="row gutter">
                                                    <div class="col-md-4">
                                                        Nombre:
                                                        <asp:TextBox ID="txtNombre" runat="server" class="form-control" name="nombre"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        Unidad de Medida:
                                                        <asp:DropDownList ID="ddlUDM" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4">
                                                        Capacidad:
                                                        <asp:TextBox ID="txtCapacidad" runat="server" class="form-control" name="capacidad" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        Límite:
                                                        <asp:TextBox ID="txtLimite" runat="server" class="form-control" name="limite" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Modificar" class="btn btn-info" />
                                                        &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-info" OnClick="btnCancelar_Click" Text="Volver" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
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
