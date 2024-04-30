<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="clientesProyectos.aspx.cs" Inherits="despacho.clientesProyectos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Cliente</h3>
                    <p>/ <a href="clientes.aspx">Clientes</a></p>
                </div>
            </div>
            <%--<asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataSourceID="dsTipoProducto" DataTextField="tipo" DataValueField="id" ></asp:DropDownList>
                                                                                            <asp:SqlDataSource ID="dsTipoProducto" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [tiposProductos]"></asp:SqlDataSource>--%>
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
           <button type="button" class="btn btn-secondary" datadismiss="modal">Cerrar</button>
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
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="panel-heading">
                                                <h4></h4>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label4" runat="server" class="control-label" Text="Proyecto:"></asp:Label>
                                                    <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label6" runat="server" class="control-label" Text="Código Postal:"></asp:Label>
                                                    <asp:TextBox ID="txtCP" runat="server" class="form-control" MaxLength="5" onkeypress="return onlyNumbers(event)" AutoPostBack="True" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" class="control-label" Text="Colonia:"></asp:Label><br />
                                                    <ajaxToolkit:ComboBox ID="cbxColoniaProyecto" runat="server" name="colonia" AutoPostBack="True" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" Width="100%" OnSelectedIndexChanged="cbxColoniaProyecto_SelectedIndexChanged" DataTextField="asenta" DataValueField="asenta"></ajaxToolkit:ComboBox>
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label11" runat="server" class="control-label" Text="Calle:"></asp:Label>
                                                    <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label12" runat="server" class="control-label" Text="Número:"></asp:Label>
                                                    <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label13" runat="server" class="control-label" Text="Interior:"></asp:Label>
                                                    <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row gutter">
                                                <div class="col-md-8">
                                                    <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar" class="btn btn-info" data-dismiss="modal"  data-target=".bd-example-modal-sm"/>
                                                    &nbsp;<asp:Button ID="btnCancelar" runat="server" class="btn btn-info" OnClick="btnCancelar_Click" Text="Volver" />
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
