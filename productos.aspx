<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="despacho.productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Productos</h3>
                    <p><a href="home.aspx">Home</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <%--<li>
									<a href="javascript:void(0)" class="btn btn-danger"><span>76</span>Sales</a>
								</li>
								<li>
									<a href="tasks.html" class="btn btn-success">
										<span>18</span>Tasks</a>
								</li>--%>
                    <li>
                        <a href="productosAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                    <li>
                        <asp:Button ID="btnHomologar" runat="server" CssClass="btn btn-success" Text="Homologar" OnClick="btnHomologar_Click"/>
                        <%--<a href="javascript:void(0)" class="btn btn-info"><i class="icon-download6"></i> Export</a>--%>
                    </li>
                </ul>
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
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div id="ContentPlaceHolder_upGrid">
                <div class="row gutter">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="panel panel-blue">
                            <div class="panel-heading">
                                <h4></h4>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:ListView ID="listView" runat="server" OnItemCommand="listView_ItemCommand" OnItemDeleting="listView_ItemDeleting" data-dismiss="modal" DataKeyNames="id">
                                        <LayoutTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="codigo">Código</th>
                                                        <th id="descripcion">Descripción</th>
                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                        <th style="text-align: center"  id="tipo">Tipo de Producto</th>
                                                        <th style="text-align: center" id="categoria">Categoría</th>
                                                        <th style="text-align: center" id="precio">Precio</th>
                                                        <th style="text-align: center" id="iva">IVA</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="homologar">Homologar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                <tfoot>
                                                    <tr>
                                                        <th style="text-align: center">Código</th>
                                                        <th>Descripción</th>
                                                        <th style="text-align: center">Unidad</th>
                                                        <th style="text-align: center" >Tipo de Producto</th>
                                                        <th style="text-align: center" id="categoria">Categoría</th>
                                                        <th style="text-align: center">Precio</th>
                                                        <th style="text-align: center">IVA</th>
                                                        <th style="text-align: center">Modificar</th>
                                                        <th style="text-align: center">Homologar</th>
                                                        <th style="text-align: center">Eliminar</th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center"><%# Eval("codigo") %></td>
                                                <td><%# Eval("descripcion") %></td>
                                                <td style="text-align: center"><%# Eval("unidad").ToString().Trim() %></td>
                                                <td style="text-align: center"><%# Eval("tipo") %></td>
                                                <td style="text-align: center"><%# Eval("categoria") %></td>
                                                <td style="text-align: center"><%# Eval("precio") %></td>
                                                <td style="text-align: center"><%# Eval("iva") %></td>
                                                <td style="text-align: center">
                                                    <a href="productosMod.aspx?id=<%# Eval("id") %>">
                                                        <i class="icon-new-message"></i>
                                                    </a>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chkHomologar" runat="server" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("descripcion") %>' CommandName="delete"><i class="icon-delete"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center" id="codigo">Código</th>
                                                        <th id="descripcion">Descripción</th>
                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                        <th style="text-align: center" id="tipoProducto">Tipo de Producto</th>
                                                        <th style="text-align: center" id="categoria">Categoría</th>
                                                        <th style="text-align: center" id="precio">Precio</th>
                                                        <th style="text-align: center" id="iva">IVA</th>
                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                        <th style="text-align: center" id="homologar">Homologar</th>
                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td colspan="8">
                                                        <label class="label label-danger">¡No hay Productos Registrados!</label></td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <br /><asp:Label ID="lblResultados" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
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
                                                <h4 class="modal-title">
                                                    <asp:Label ID="mlblTitle" runat="server" />
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <asp:Label ID="mlblMessage" runat="server" />
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="mbtnClose" runat="server" Text="Cerrar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                                <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                                    Close</button>
                                                <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" data-dismiss="modal"  data-target=".bd-example-modal-sm" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.modal -->
                        </div>
                        <div>
                            <!-- Modal -->
                                <div class="modal fade" id="modalInfo">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span></button>
                                                <h4 class="modal-title">
                                                    Eliminar Producto
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <span>Se ha eliminado correctamente el producto en Contpaq y Despacho</span>
                                            </div>
                                            <div class="modal-footer">
                                                <a href="productos.aspx" class="btn btn-default">Cerrar</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.modal -->
                        </div>
                    </div>
                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Row Ends -->
    <script type="text/javascript">
        if (window.location.search.indexOf('error=0') !== -1) {
            // Llama a la función OpenModal
            OpenModal();
        }
        function OpenModal() {
            $('#modalInfo').modal('show'); // Abre la modal usando jQuery
            return false; // Evita que el botón realice su acción de postback
        }
    </script>

</asp:Content>
