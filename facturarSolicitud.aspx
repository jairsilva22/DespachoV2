<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="facturarSolicitud.aspx.cs" Inherits="despacho.facturarSolicitud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
var posicion_x;
        var posicion_y;
        posicion_x = (screen.width / 2) - (500 / 2);
        posicion_y = (screen.height / 2) - (500 / 2);
function clientes() {
            open("clienteFactura1.asp", '', 'top=' + posicion_y + ', left=' + posicion_x + ', width=800, height=650')
        }
        function borrar() {
            document.getElementById("<%= clientee.ClientID %>").innerHTML = ""
            document.getElementById("<%= clientes1.ClientID %>").value = "";
            location.reload();
        }
        function buscarDatos() {
            document.getElementById('<%= btnGuardarN.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Datos para facturar</h3>
                    <p>/ <a href="cfinanzas.aspx">Finanzas</a></p>
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
                                                <%--<ul class="nav nav-tabs">
										<li class="active">
											<a href="#tabOne" data-toggle="tab">Nuevo Cliente</a>
										</li>
										<li>
											<a href="#tabTwo" data-toggle="tab">Buscar Cliente</a>
										</li>
										
									</ul>--%>
                                                <div>
                                                <div id="tabOne">
                                                <table class="nav-justified">
                                                    <tr>
                                                        <td colspan="6">
                                                            <div class="row gutter">
                                                                <div class="col-md-8 h3">
                                                                    Favor de ingresar los datos del cliente a facturar
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <a href="javascript:clientes()"><i class="icon-search5"></i>Buscar Cliente</a>
                                                        </td>
                                                        <td colspan="3">
                                                            <div id="cliente" style="display: inline;" onclick="buscarDatos();"></div>
                                                            <div runat="server" id="clientee" style="display: inline;"></div>
                                                                <asp:HiddenField runat="server" ID="clientes1" />
                                                        </td>
                                                    </tr>
                                     
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Nombre del Cliente:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Nombre empresa:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    RFC:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Forma de Pago:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <%--<td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Tipo Comisión:
                                                                </div>
                                                            </div>
                                                        </td>--%>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreCliente" runat="server" class="form-control" OnTextChanged="txtNombreCliente_TextChanged"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="txtNombreCliente_AutoCompleteExtender" runat="server" BehaviorID="TtxtNombreCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtNombreCliente" ServiceMethod="getDataNombreCliente" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="True">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNombreEmpresa" runat="server" class="form-control" OnTextChanged="txtNombreCliente_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRFC" runat="server" class="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id" OnSelectedIndexChanged="ddlFP_SelectedIndexChanged">
                                                                <asp:ListItem>Selecciona una Forma de Pago</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <%--<td>
                                                            <asp:CheckBox ID="chbxDirecto" runat="server" Text="Directo" />
                                                        </td>--%>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    IVA:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Código Postal:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Estado:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Ciudad:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:TextBox ID="txtIva" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCP" runat="server" AutoPostBack="True" class="form-control" MaxLength="5" OnTextChanged="txtCP_TextChanged" TextMode="Number"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEstados" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCiudades" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCiudades_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Colonia:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Calle:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Número:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Interior:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <ajaxToolkit:ComboBox ID="cbxColonias" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DropDownStyle="Simple" name="colonia" Width="100%" DataTextField="asenta" DataValueField="asenta">
                                                            </ajaxToolkit:ComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" TextMode="Number"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Email:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Teléfono:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Celular:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Uso CFDI:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTelefono" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCelular" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUsoCFDI" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Método de Pago:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                           <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Clave cliente:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Moneda:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="row gutter">
                                                                <div class="col-md-8">
                                                                    Documento:
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlMetodoPago" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtClave" runat="server" class="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlMoneda" runat="server" class="form-control" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDocumento" runat="server" class="form-control" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" class="btn btn-info" />
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:Button ID="btnCancelar" runat="server" class="btn btn-info" OnClick="btnCancelar_Click" Text="Cancelar" />
                                                        </td>
                                                        <td></td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <asp:HiddenField runat="server" ID="hdClave" />
                                                </table>
                                                    </div>
                                                <%--<div class="tab-pane" id="tabTwo">
                                                    
                                                    <div id="datos">
                                                    <table>
                                                        <tr>
                                                            <td>I.V.A:</td>
                                                            <td>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtIva1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Forma de Pago: </td>
                                                            <td>
                                                                   <asp:DropDownList CssClass="form-control" ValidationGroup="nuevo" runat="server" ID="ddlFP1"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Moneda: </td>
                                                            <td>
                                                                <asp:DropDownList CssClass="form-control" ValidationGroup="nuevo" runat="server" ID="ddlMoneda1"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Documento:</td>
                                                            <td>
                                                                <asp:DropDownList CssClass="form-control" ValidationGroup="nuevo" runat="server" ID="ddlDocumento1"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="lblError1" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Enviar" ValidationGroup="nuevo" OnClick="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </div>
                                                </div>--%>
                                                    <button id="btnGuarda" onclick="buscarDatos();" style="display: none"></button>
                                                    <asp:Button runat="server" ID="btnGuardarN" style="display:none" OnClick="btnGuardarN_Click" />
                                                    </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-8">
                                                        &nbsp;<asp:HiddenField ID="hfIdCliente" runat="server" />
                                                        <asp:HiddenField ID="hfIdVendedor" runat="server" />
                                                        <asp:HiddenField ID="hfIdEstado" runat="server" />
                                                        <asp:HiddenField ID="hfIdCiudad" runat="server" />
                                                        <asp:HiddenField ID="hfIdEstadoCP" runat="server" />
                                                        <asp:HiddenField ID="hfIdCiudadCP" runat="server" />
                                                        <asp:HiddenField ID="hfIdFP" runat="server" />
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
                                        <%--<asp:Panel ID="pnlClientes" runat="server">
                                            <!-- Row Starts -->
                                            <asp:UpdatePanel ID="upGrid" runat="server">
                                                <ContentTemplate>
                                                    <div id="ContentPlaceHolder_upGrid">
                                                        <div class="row gutter">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <div class="panel panel-blue">
                                                                    <div class="panel-heading">
                                                                        <table class="nav-justified">
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td><h4>Clientes</h4></td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lbtnCerrarPnlClientes" runat="server"><i class="icon-cross2"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <table class="nav-justified">
                                                                            <tr>
                                                                                <td>&nbsp;     </td>
                                                                                <td style="width:80%">
                                                                        <div class="table-responsive">
                                                                            <asp:ListView ID="lvClientes" runat="server" OnItemCommand="lvClientes_ItemCommand">
                                                                                <LayoutTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="id">ID</th>
                                                                                                <th id="nombre">Nombre</th>
                                                                                                <th id="seleccionar">Seleccionar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                                                        <tfoot>
                                                                                            <tr>
                                                                                                <th style="text-align: center">ID</th>
                                                                                                <th>Nombre</th>
                                                                                                <th id="seleccionar">Seleccionar</th>
                                                                                            </tr>
                                                                                        </tfoot>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center"><%# Eval("id") %></td>
                                                                                        <td><%# Eval("nombre") %></td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lbtnSeleccionarCliente" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("nombre") %>' CommandName="seleccionar"><i class="icon-user-check"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <EmptyDataTemplate>
                                                                                    <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="text-align: center" id="id">ID</th>
                                                                                                <th id="nombre">Nombre</th>
                                                                                                <th style="text-align: center" id="seleccionar">Seleccionar</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <label class="label label-danger">¡No hay Clientes Registrados!</label></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                                </td>
                                                                                <td>&nbsp;     </td>
                                                                            </tr>
                                                                            </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <!-- Row Ends -->
                                        </asp:Panel>
                                        <ajaxToolkit:ModalPopupExtender ID="pnlClientes_ModalPopupExtender" runat="server" BehaviorID="pnlClientes_ModalPopupExtender" DynamicServicePath="" PopupControlID="pnlClientes" TargetControlID="btnClientes" CancelControlID="lbtnCerrarPnlClientes" DropShadow="True">
                                        </ajaxToolkit:ModalPopupExtender>--%>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtNombreCliente" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlFP" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstados" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCiudades" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCP" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Row Ends -->
</asp:Content>
