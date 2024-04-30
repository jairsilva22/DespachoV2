<%@ Page Title="" Language="C#" MasterPageFile="~/PopUP.Master" AutoEventWireup="true" CodeBehind="solicitudesCot.aspx.cs" Inherits="despacho.solicitudesCot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="panel-body">
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label1" runat="server" Text="Observaciones" class="control-label"></asp:Label>
                                                            <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar Cotización" class="form-control" OnClick="btnGenerarPDF_Click"/>
                                                            <asp:Button ID="btnEnviar" runat="server" Text="Enviar por Correo" class="form-control" OnClick="btnEnviar_Click" Visible="False"/>
                                                            <asp:Literal ID="lPDF" runat="server"></asp:Literal>
                                                            <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Large" Font-Bold="True"></asp:Label>
                                                            <asp:HiddenField ID="hfDocumento" runat="server" />
                                                            <asp:HiddenField ID="hfRuta" runat="server" />
                                                            <asp:HiddenField ID="hfFolio" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
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
