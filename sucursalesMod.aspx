<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sucursalesMod.aspx.cs" Inherits="despacho.sucursalesMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Sucursal</h3>
                    <p>/ <a href="sucursales.aspx">Sucursales</a></p>
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
                <%--<asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>--%>
                <div class="panel-body">
                    <%-- <form id="defaultForm" method="post">--%>
                    <div class="form-group">
                        <div class="row gutter">
                            <asp:Panel ID="pnlSolicitud" runat="server">
                                <div class="panel-heading">
                                    <h4>Datos Generales</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Nombre:
                                        <asp:TextBox ID="txtNombre" runat="server" class="form-control" name="nombre"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Código:
                                        <asp:Label ID="lblCodigo" runat="server" class="form-control" name="codigo" MaxLength="5"></asp:Label>
                                    </div>
                                    <div class="col-md-5">
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Razón Social:
                                        <asp:TextBox ID="txtRazon" runat="server" class="form-control" name="razon"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        RFC:
                                        <asp:TextBox ID="txtRFC" runat="server" class="form-control" name="razon"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        Régimen Fiscal:
                                        <asp:DropDownList ID="ddlRegimen" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-3">
                                        Código Postal:
                                        <asp:TextBox ID="txtCP" runat="server" class="form-control" name="cp" AutoPostBack="True" MaxLength="5" onkeypress="return onlyNumbers(event)" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Estado:
                                        <asp:DropDownList ID="ddlEstado" runat="server" class="form-control" name="estado" AutoPostBack="True" DataTextField="estado" DataValueField="id" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        Ciudad:
                                        <asp:DropDownList ID="ddlCiudades" runat="server" class="form-control" DataTextField="ciudad" DataValueField="id" name="colonia">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:SqlDataSource ID="dsEstados" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT [id], [estado] FROM [estados]"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-3">
                                        Colonia:<br />
                                        <ajaxToolkit:ComboBox ID="cbxColonia" runat="server" name="colonia" AutoPostBack="True" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" Width="100%" DataTextField="asenta" DataValueField="asenta"></ajaxToolkit:ComboBox>
                                    </div>
                                    <div class="col-md-3">
                                        Calle:
                                        <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Numero:
                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" MaxLength="5" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Interior:
                                        <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="panel-heading">
                                    <h4>Datos de Comisión</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-12">
                                        (0.01 = 1% | 0.1 = 10% | 1 = 100%)
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-3">
                                        Cliente Directo:
                                        <asp:TextBox ID="txtComisionD" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)" MaxLength="5"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Cliente Indirecto:
                                        <asp:TextBox ID="txtComisionI" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)" MaxLength="5"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <%--<asp:Panel ID="Panel2" runat="server">
                                <div class="panel-heading">
                                    <h4>Datos de Facturación</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Archivo .cer:
                                        <asp:TextBox ID="txtCer" runat="server" class="form-control" name="cer" ReadOnly="true"></asp:TextBox>
                                        <asp:FileUpload runat="server" ID="fuCer" enctype="multipart/form-data" />
                                        <asp:RegularExpressionValidator id="FileUpLoadValidator" runat="server" ErrorMessage="Solamente se permiten archivos .cer" CssClass="label label-danger fa-2x"
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.cer|.CER)$" ControlToValidate="fuCer"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-4">
                                        Archivo .key:
                                        <asp:TextBox ID="txtKey" runat="server" class="form-control" name="key" ReadOnly="true"></asp:TextBox>
                                        <asp:FileUpload runat="server" ID="fuKey" />
                                        <asp:RegularExpressionValidator id="FileUpLoadValidator2" runat="server" ErrorMessage="Solamente se permiten archivos .key" CssClass="label label-danger fa-2x"
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.key|.KEY)$" ControlToValidate="fuKey"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-4">
                                        Contraseña:
                                        <asp:TextBox ID="txtContraArch" runat="server" class="form-control" name="contrseñaarchivos" type="password"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />--%>
                            <asp:Panel ID="Panel3" runat="server">
                                <div class="panel-heading">
                                    <h4>Logotipo</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Logo:<br />
                                        <asp:Image ID="imgLogo" runat="server" Width="200px" Height="100px" />
                                        <asp:FileUpload runat="server" ID="fuLogo" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Solamente se permiten archivos .png y .jpg" CssClass="label label-danger fa-2x"
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.png|.PNG|.jpg|.JPG|.jpeg|.JPEG)$" ControlToValidate="fuLogo"> </asp:RegularExpressionValidator>
                                         <asp:HiddenField ID="hdfLogo" runat="server" /> 
                                    </div>
                                    <div class="col-md-4">
                                        Ancho:
                                        <asp:TextBox ID="txtAncho" runat="server" class="form-control" name="Ancho"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        Alto:
                                        <asp:TextBox ID="txtAlto" runat="server" class="form-control" name="Alto"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                           <%-- <br />
                            <asp:Panel ID="Panel4" runat="server">
                                <div class="panel-heading">
                                    <h4>Carpetas</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Nombre de la carpeta:
                                        <asp:TextBox ID="txtNCarpeta" runat="server" class="form-control" name="NCarpeta"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        XML Timbrado:
                                        <asp:TextBox ID="txtCarpetaTimbre" runat="server" class="form-control" name="CarpetaTimbre"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </asp:Panel>--%>
                            <br />
                            <asp:Panel ID="Panel8" runat="server">
                                <div class="panel-heading">
                                    <h4>Cotización</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-12">
                                        Observaciones:
                                        <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" name="CotObservaciones" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel9" runat="server">
                                <div class="panel-heading">
                                    <h4>Remisión</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-12">
                                        Descargo de responsabilidad:
                                        <asp:TextBox ID="txtDisclaiment" runat="server" class="form-control" name="disclaiment" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                     <%--<div class="col-md-12">
                                        Descargo de responsabilidad Block:
                                        <asp:TextBox ID="txtDisclaimentB" runat="server" class="form-control" name="disclaimentB" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </div>--%>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel5" runat="server">
                                <div class="panel-heading">
                                    <h4>Configuración</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Simulación en dosificación:<br />
                                        <asp:CheckBox ID="chbxSimulacion" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        IVA:
                                        <asp:TextBox ID="txtIva" runat="server" class="form-control" name="iva"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        Path:
                                        <asp:TextBox ID="txtPath" runat="server" class="form-control" name="path" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-4">
                                        Path Utilerias:
                                        <asp:TextBox ID="txtPathUtilerias" runat="server" class="form-control" name="pathutilerias" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Cadena de Conexión:
                                        <asp:TextBox ID="txtCadena" runat="server" class="form-control" name="cadena" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel7" runat="server">
                                <div class="panel-heading">
                                    <h4>Información del Correo</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-3">
                                        Correo:
                                        <asp:TextBox ID="txtCorreo" runat="server" class="form-control" name="correo"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Smtp:
                                        <asp:TextBox ID="txtSmtp" runat="server" class="form-control" name="smtp" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        Contraseña Correo:
                                        <asp:TextBox ID="txtContraCorreo" runat="server" class="form-control" name="password" type="password"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="panel-heading">
                                    <h4>Información del Correo</h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-3">
                                        Sucursal en compras:
                                        <asp:DropDownList ID="ddlSucursalCompras" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-9">
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel6" runat="server">
                                <div class="panel-heading">
                                    <h4></h4>
                                </div>
                                <div class="row gutter">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Modificar" class="btn btn-info" />
                                        &nbsp;<asp:Button ID="btnRegresar" runat="server" class="btn btn-info" OnClick="btnRegresar_Click" Text="Volver" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <%--  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnRegresar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtCP" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
