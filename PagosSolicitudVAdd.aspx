<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagosSolicitudVAdd.aspx.cs" Inherits="despacho.PagosSolicitudVAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Pago</h3>
                    <p>/ <a href="cvendedores.aspx">Cobranza Vendedor </a>/ Pagos de Solicitud</p>
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
               <%-- <asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>--%>
                        <div class="panel-body">
                            <%--<form id="defaultForm" method="post">--%>
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Folio:</label>
                                                        <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Monto:</label>
                                                        <asp:TextBox ID="txtMonto" runat="server" class="form-control text-center" readonly="true" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Saldo:</label>
                                                        <asp:TextBox ID="txtSaldo" runat="server" class="form-control text-center" required="required" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Pago:</label>
                                                        <asp:TextBox ID="txtPago" runat="server" class="form-control text-center" required="required" ValidationGroup="agregar"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Forma de pago:</label>
                                                        <asp:DropDownList ID="ddlFP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                    <div class="col-md-4 selectContainer text-center">
                                                        <label class="control-label">Método de pago:</label>
                                                        <asp:DropDownList ID="ddlMP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id" >
                                                            </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 selectContainer">
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click" ValidationGroup="agregar"/>
                                            <a href="pagosSolicitudV.aspx?idSolicitud=<%= Request.QueryString["idSolicitud"] %>&Vendedor=<%= Request.QueryString["Vendedor"] %>" class="btn btn-info text-center">
                                                Volver
                                            </a>
                                           <%-- <asp:Button ID="btnCancelar" runat="server" Text="Volver" class="btn btn-info" OnClick="btnCancelar_Click" ValidationGroup="volver"/>--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                            <%--</form>--%>
                        </div>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
