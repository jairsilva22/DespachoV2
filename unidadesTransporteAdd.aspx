<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="unidadesTransporteAdd.aspx.cs" Inherits="despacho.unidadesTransporteAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        function Color_Changed(sender) {

            sender.get_element().value = "#" + sender.get_selectedColor();

        }

        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nueva Unidad</h3>
                    <p>/ <a href="unidadesTransporte.aspx">Unidades de Transporte</a></p>
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
                                                    <div class="col-md-6">
                                                        <label class="control-label">Nombre:</label>
                                                        <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="control-label">Matrícula:</label>
                                                        <asp:TextBox ID="txtMatricula" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Tipo de Unidad:</label>
                                                        <asp:DropDownList ID="ddlTU" runat="server" class="form-control" DataTextField="tipo" DataValueField="id"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Unidad de Medida:</label>
                                                        <asp:DropDownList ID="ddlUDM" runat="server" class="form-control" DataTextField="unidad" DataValueField="id"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Capacidad:</label>
                                                        <asp:TextBox ID="txtCapacidad" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Capacidad Máxima:</label>
                                                        <asp:TextBox ID="txtCapacidadMax" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <label class="control-label">Color:</label>
                                                        <asp:TextBox ID="txtColor" runat="server" class="form-control" AutoPostBack="True"></asp:TextBox>
                                                        <ajaxToolkit:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" BehaviorID="txtColor_ColorPickerExtender" TargetControlID="txtColor" PopupButtonID="txtColor" SampleControlID="PreviewColor" />
                                                    </div>
                                                    <div class="col-md-2 selectContainer">
                                                        <br />
                                                        <div id="PreviewColor" style="width: 50%; height: 45px; border: 1px solid Gray">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Combustible:</label>
                                                        <asp:DropDownList ID="ddlCombustible" runat="server" class="form-control">
                                                            <asp:ListItem>Diesel</asp:ListItem>
                                                            <asp:ListItem>Gasolina</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12 selectContainer">
                                                        <asp:HiddenField ID="hfCapacidad" runat="server" />
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
