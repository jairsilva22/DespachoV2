<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cnfAlertaPMod.aspx.cs" Inherits="despacho.cnfAlertaPMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript">

        function Color_Changed(sender) {

            sender.get_element().value = "#" + sender.get_selectedColor();

        }

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if ((charCode < 48 || charCode > 57))
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
                    <h3>Modificar Alerta</h3>
                    <p>/ <a href="cnfAlertaP.aspx">Alerta de Remisión en Programación</a></p>
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
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Color:</label>
                                                        <asp:DropDownList ID="ddlColor" runat="server" class="form-control" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged" >
                                                            <asp:ListItem Value="0">Selecciona un color</asp:ListItem>
                                                            <asp:ListItem Value="#00FF00">Verde</asp:ListItem>
                                                            <asp:ListItem Value="#FFCC00">Amarillo</asp:ListItem>
                                                            <asp:ListItem Value="#FF0000">Rojo</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Sucursal:</label>
                                                        <asp:DropDownList ID="ddlSucursal" runat="server" class="form-control" DataTextField="nombre" DataValueField="id"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-6 selectContainer">
                                                        <label class="control-label">Tiempo (Minutos):</label>
                                                        <asp:TextBox ID="txtTiempo" runat="server" class="form-control" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Modificar" class="btn btn-info" OnClick="btnAgregar_Click"/> 
                                            
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
