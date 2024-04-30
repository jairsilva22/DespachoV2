<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MetodoPagoAdd.aspx.cs" Inherits="despacho.MetodoPagoAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     &nbsp;<!-- Top Bar Starts -->
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nuevo Método de Pago</h3>
                    <p>/ <a href="MetodoPago.aspx">Métodos de Pago</a></p>
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
                        <div class="panel-body">
                            <form id="defaultForm" method="post">
                                <div class="form-group">
                                    <div class="row gutter">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="form-group">
                                                <div class="row gutter">
                                                    <div class="col-md-12">
                                                        Clave Método de Pago:
                                                        <asp:TextBox ID="txtClave" runat="server" class="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator CssClass="label label-danger" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave" ErrorMessage="Ingrese la Clave Método de Pago"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-12 selectContainer">
                                                        <label class="control-label">Descripción:</label>
                                                        <asp:TextBox ID="txtDescripcion" runat="server" class="form-control"></asp:TextBox>
                                                         <asp:RequiredFieldValidator CssClass="label label-danger" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Ingrese la Descripción"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-4 selectContainer">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-info" data-dismiss="modal"  data-target=".bd-example-modal-sm" OnClick="btnAgregar_Click"/> 
                                            
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
