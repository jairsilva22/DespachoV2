<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FoliosAdd.aspx.cs" Inherits="despacho.FoliosAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Folio</h3>
                    <p>/ <a href="Folios.aspx">Folios</a></p>
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
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                       Empresa:
                                                        <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Folio Inicio:
                                                        <asp:TextBox ID="txtFolioI" runat="server" class="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator CssClass="label label-danger" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFolioI" ErrorMessage="Ingrese el Folio Inicio"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Folio Final:
                                                        <asp:TextBox ID="txtFolioF" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Folio Activo:
                                                        <asp:TextBox ID="txtFolioA" runat="server" class="form-control"></asp:TextBox>
                                                         <asp:RequiredFieldValidator CssClass="label label-danger" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFolioA" ErrorMessage="Ingrese el Folio Activo"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Serie:
                                                        <asp:TextBox ID="txtSerie" runat="server" class="form-control"></asp:TextBox>
                                                         <asp:RequiredFieldValidator CssClass="label label-danger" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSerie" ErrorMessage="Ingrese la Serie"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        N° de Aprobación:
                                                        <asp:TextBox ID="txtNoAprobacion" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Año de Aprobación:
                                                        <asp:TextBox ID="txtAnoAprobacion" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" CssClass="checkbox-inline" ID="cbSolicitudes" Text="Solicitudes" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" CssClass="checkbox-inline" ID="cbOrdenes" Text="Ordenes" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" CssClass="checkbox-inline" ID="cbRemisiones" Text="Remisiones" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" CssClass="checkbox-inline" ID="cbFactura" Text="Factura" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" CssClass="checkbox-inline" ID="cbNotaCred" Text="Nota de Crédito" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" ID="cbNotaCargo" CssClass="checkbox-inline" Text="Nota de Cargo" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" ID="cbComPago" Text="Complemento de Pago" CssClass="checkbox-inline" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" ID="cbPagosF" Text="Pagos Finanzas" CssClass="checkbox-inline" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox runat="server" ID="cbPagosV" Text="Pagos Vendedores" CssClass="checkbox-inline" />
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" OnClick="btnAgregar_Click"/> 
                                            
                                            <asp:Button ID="btnCancelar" runat="server" Text="Volver" class="btn btn-info" OnClick="btnCancelar_Click"/>
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
