<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productosMod.aspx.cs" Inherits="despacho.productosMod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Modificar Producto</h3>
                    <p>/ <a href="productos.aspx">Productos</a></p>
                </div>
            </div>
        </div>
    </div>
    <!-- Top Bar Ends -->
        <!-- Modal de Error -->
<div class="modal fade" id="errorModal" tabindex="-1" role="dialog" aria-labelledby="errorModalLabel" aria-hidden="true">
     <div class="modal-dialog modal-dialog-centered" role="document">
       <div class="modal-content">
         <div class="modal-header bg-primary text-white">
           <h5 class="modal-title" id="errorModalLabel">Mensaje:</h5>
           
           </button>
         </div>
         <div class="modal-body display-3">
           <p id="errorMessage" ></p>
         </div>
         <div class="modal-footer">
           <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
         </div>
       </div>
     </div>
   </div>
    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4></h4>
                </div>
                <asp:UpdatePanel ID="upForm" runat="server">
                    <ContentTemplate>

                       <!--  Este literal se agrego para que te muestre la alerte al momento de agregar un nuevo producto  -->
                        
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <div class="form-group">
                                                            <div class="row gutter">
                                                                <div class="col-md-6">
                                                                    <asp:Label ID="lblCodigo" runat="server" Text="Código (En caso de ser concreto favor de incluir la nomenclatura TD para Tiro Directo y BBA para Bombeado):" class="control-label"></asp:Label>
                                                                    <asp:TextBox ID="txtCodigo" runat="server" class="form-control" name="clave" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <ajaxToolkit:AutoCompleteExtender ID="txtCodigo_AutoCompleteExtender" runat="server" BehaviorID="txtCodigo_AutoCompleteExtender" CompletionSetCount="1" DelimiterCharacters="" MinimumPrefixLength="1" ServiceMethod="getDataCcodigoProducto" TargetControlID="txtCodigo" UseContextKey="True">
                                                                    </ajaxToolkit:AutoCompleteExtender>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    Descripción:
                                                                    <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" AutoPostBack="True"></asp:TextBox>
                                                                    <ajaxToolkit:AutoCompleteExtender ID="txtDescripcion_AutoCompleteExtender" runat="server" BehaviorID="txtDescripcion_AutoCompleteExtender" CompletionSetCount="1" DelimiterCharacters="" MinimumPrefixLength="1" ServiceMethod="getDataDescripcionProducto" TargetControlID="txtDescripcion" UseContextKey="True">
                                                                    </ajaxToolkit:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-6">
                                                                    Unidad:
                                                                    <asp:DropDownList ID="ddlUnidad" runat="server" class="form-control" name="colonia" AutoPostBack="True" DataTextField="unidad" DataValueField="id">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    Código SAT:
                                                                    <asp:TextBox ID="txtCodigoSAT" runat="server" class="form-control" name="codigoSAT"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-6">
                                                                    Precio:
                                                                    <asp:TextBox ID="txtPrecio" runat="server" class="form-control" name="precio" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    IVA:
                                                                    <asp:TextBox ID="txtIVA" runat="server" class="form-control" onkeypress="return onlyDotsAndNumbers(event)" MaxLength="4">0.16</asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-6">
                                                                    Tipo de Producto:
                                                                    <asp:DropDownList ID="ddlTipoProducto" runat="server" AutoPostBack="True" class="form-control" DataTextField="tipo" DataValueField="id" name="tipoProducto" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    Categoría:
                                                                    <asp:DropDownList ID="ddlCategoria" runat="server" class="form-control" DataTextField="categoria" DataValueField="id" name="categoria">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-6">
                                                                    <asp:Label ID="lblCarga" runat="server" Text="Cantidad de Block en Tarima:" Visible="False"></asp:Label>
                                                                    <asp:TextBox ID="txtCarga" runat="server" class="form-control" name="carga" onkeypress="return onlyNumbers(event)" Visible="False" Text="0"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:Label ID="lblPeso" runat="server" Text="Peso en KG:" Visible="False"></asp:Label>
                                                                    <asp:TextBox ID="txtPeso" runat="server" class="form-control" name="carga" onkeypress="return onlyDotsAndNumbers(event)" Visible="False" Text="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row gutter">
                                                                <div class="col-md-12">
                                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Modificar" class="btn btn-info" data-dismiss="modal"  data-target=".bd-example-modal-sm"/>
                                                        &nbsp;<asp:Button ID="btnRegresar" runat="server" class="btn btn-info" PostBackUrl="~/productos.aspx" Text="Volver" />
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-8">
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
                        <div>
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
                                            <h4 class="modal-title">Homologación de Productos
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <span>¿Desea homolgar el producto en Contpaq?</span>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="mbtnClose" runat="server" Text="No" class="btn btn-default" OnClick="mbtnClose_Click" />
                                            <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                                Close</button>
                                            <asp:Button ID="mbtnAceptar" runat="server" Text="Si" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.modal -->
                        </div>

                        <!-- codigo agregado para mostrar alertas en pantalla -->
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtDescripcion" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                       
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Row Ends -->
</asp:Content>
