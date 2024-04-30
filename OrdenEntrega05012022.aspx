<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdenEntrega05012022.aspx.cs" Inherits="despacho.OrdenEntrega05012022" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Arise Admin Panel" />
    <meta name="keywords" content="Admin, Dashboard, Bootstrap3, Sass, transform, CSS3, HTML5, Web design, UI Design, Responsive Dashboard, Responsive Admin, Admin Theme, Best Admin UI, Bootstrap Theme, Themeforest, Bootstrap, C3 Graphs, D3 Graphs, NVD3 Graphs, Admin Skin, Black Admin Dashboard, Grey Admin Dashboard, Dark Admin Dashboard, Simple Admin Dashboard, Simple Admin Theme, Simple Bootstrap Dashboard, " />
    <meta name="author" content="Ramji" />
    <link rel="shortcut icon" href="img/fav.png">
    <title>Orden de Entrega</title>

    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />

    <!-- Main CSS -->
    <link href="css/main.css" rel="stylesheet" media="screen" />

    <!-- Ion Icons -->
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet" />

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery.js"></script>

    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>

    <!-- jquery ScrollUp JS -->
    <script src="js/scrollup/jquery.scrollUp.js"></script>

    <!-- Sparkline Graphs -->
    <script src="js/sparkline/sparkline.js"></script>
    <script src="js/sparkline/custom-sparkline.js"></script>

    <!-- DataBars JS -->
    <script src="js/databars/jquery.databar.js"></script>
    <script src="js/databars/custom-databars.js"></script>

    <!-- Data Tables -->
    <script src="js/datatables/dataTables.min.js"></script>
    <script src="js/datatables/dataTables.bootstrap.min.js"></script>
    <script src="js/datatables/autoFill.min.js"></script>
    <script src="js/datatables/autoFill.bootstrap.min.js"></script>
    <script src="js/datatables/fixedHeader.min.js"></script>
    <script src="js/datatables/custom-datatables.js"></script>

    <!-- Custom JS -->
    <%--<script src="js/custom.js"></script>--%>

    <script type="text/javascript">

        function ShowPopup() {
            $("#btnShowPopup").click();
        }

        function ClosePopup() {
            $("#btnClosePopup").click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
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

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function cerrar() {
            
            window.close();
           // window.opener.location.reload();
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManagerPopUp" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="upWUC" runat="server">
                <ContentTemplate>
                    <!-- Dashboard Wrapper Start -->
                    <div class="dashboard-wrappe">

                        <!-- Container fluid Starts -->
                        <div class="container-fluid">
                            <div class="row gutter">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="panel panel-blue">
                                        <div class="row gutter">
                                            <asp:Panel ID="pnlMdlOrden" runat="server">
                                                <asp:HiddenField ID="hfIdOrden" runat="server" />
                                                <asp:HiddenField ID="hfidOD" runat="server" />
                                                <asp:HiddenField ID="hfIdSolicitud" runat="server" />
                                                <asp:HiddenField ID="hfIdCliente" runat="server" />
                                                <asp:HiddenField ID="hfIdProyecto" runat="server" />
                                                <asp:HiddenField ID="hfIdEstado" runat="server" />
                                                <asp:HiddenField ID="hfIdCiudad" runat="server" />
                                                <asp:HiddenField ID="hfSearchBy" runat="server" />
                                                <asp:HiddenField ID="hfIdFP" runat="server" />
                                                <asp:HiddenField ID="hfIdVendedor" runat="server" />
                                                <asp:HiddenField ID="hfOIdVendedor" runat="server" />
                                                <asp:HiddenField ID="hfIdElemento" runat="server" />
                                                <asp:HiddenField ID="hfIdTipoProducto" runat="server" />
                                                <asp:HiddenField ID="hfIdFactor" runat="server" />
                                                <asp:HiddenField ID="hfIdDS" runat="server" />
                                                <asp:HiddenField ID="hfIdProducto" runat="server" />
                                                <asp:HiddenField ID="hfPrecioU" runat="server" />
                                                <asp:HiddenField ID="hfIVA" runat="server" />
                                                <asp:HiddenField ID="hfCargaEnBD" runat="server" />
                                                <asp:HiddenField ID="hfTP" runat="server" />
                                                <asp:HiddenField ID="hfQtyOrdenada" runat="server" />
                                                <asp:HiddenField ID="hfQtyOrdenadaEnBD" runat="server" />
                                                <asp:HiddenField ID="hfCant" runat="server" />
                                                <div class="panel-heading">
                                                    <h4>Datos de la Solicitud</h4>
                                                </div>
                                                <div class="row gutter">
                                                    <%--<div class="col-md-4">
                                                        <asp:Label ID="lblFoli" runat="server" class="control-label" Text="Folio:"></asp:Label>
                                                        <asp:Label ID="lblFolio" runat="server" class="form-control" name="folio"></asp:Label>
                                                    </div>--%>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label33" runat="server" class="control-label" Text="Estado:"></asp:Label>
                                                        <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" class="form-control" DataSourceID="dsEstadoSolicitud" DataTextField="estado" DataValueField="id">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="dsEstadoSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [estadosSolicitud]"></asp:SqlDataSource>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Fecha de Entrega:"></asp:Label>
                                                        <asp:TextBox ID="txtFecha" runat="server" class="form-control" name="fecha" TextMode="DateTime" AutoComplete="off"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Hora:"></asp:Label><br />
                                                        <div class="row gutter">
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="cbxHora" runat="server" class="form-control">
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem Selected="True">07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="cbxMinutos" runat="server" class="form-control">
                                                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>
                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>51</asp:ListItem>
                                                                    <asp:ListItem>52</asp:ListItem>
                                                                    <asp:ListItem>53</asp:ListItem>
                                                                    <asp:ListItem>54</asp:ListItem>
                                                                    <asp:ListItem>55</asp:ListItem>
                                                                    <asp:ListItem>56</asp:ListItem>
                                                                    <asp:ListItem>57</asp:ListItem>
                                                                    <asp:ListItem>58</asp:ListItem>
                                                                    <asp:ListItem>59</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                      <div class="col-md-4">
                                                            <asp:Label ID="Label28" runat="server" class="control-label" Text="Requiere Factura:"></asp:Label>
                                                            <asp:DropDownList ID="ddlReqFac" runat="server" class="form-control">
                                                                <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="SI">Si</asp:ListItem>
                                                                <asp:ListItem Value="NO">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                </div>
                                                <div class="panel-heading">
                                                    <h4>Datos del Cliente</h4>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label12" runat="server" class="control-label" Text="Clave:"></asp:Label>
                                                        <asp:TextBox ID="txtClaveCliente" runat="server" class="form-control" name="clave" OnTextChanged="txtClaveCliente_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ID="txtClaveCliente_AutoCompleteExtender" runat="server" BehaviorID="txtClaveCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtClaveCliente" ServiceMethod="getDataClaveCliente" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="True">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label8" runat="server" class="control-label" Text="Nombre:"></asp:Label>
                                                        <asp:TextBox ID="txtNombreCliente" runat="server" class="form-control" OnTextChanged="txtNombreCliente_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ID="txtNombreCliente_AutoCompleteExtender" runat="server" BehaviorID="TtxtNombreCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtNombreCliente" ServiceMethod="getDataNombreCliente" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="True">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label13" runat="server" class="control-label" Text="Forma de Pago:"></asp:Label>
                                                        <asp:DropDownList ID="ddlFP" runat="server" class="form-control" DataTextField="nombre" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddlFP_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label14" runat="server" class="control-label" Text="Vendedor:"></asp:Label>
                                                        <asp:DropDownList ID="ddlVendedores" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlVendedores_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="panel-heading">
                                                    <h4>Datos del Proyecto</h4>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label15" runat="server" class="control-label" Text="Proyecto:"></asp:Label>
                                                        <asp:DropDownList ID="ddlProyectos" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" OnSelectedIndexChanged="ddlProyectos_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label16" runat="server" class="control-label" Text="Código Postal:"></asp:Label>
                                                        <asp:TextBox ID="txtCP" runat="server" class="form-control" MaxLength="5" onkeypress="return onlyNumbers(event)" AutoPostBack="True" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        Estado:
                                                        <asp:DropDownList ID="ddlEstados" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        Ciudad:
                                                        <asp:DropDownList ID="ddlCiudades" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCiudades_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label7" runat="server" class="control-label" Text="Colonia:"></asp:Label><br />
                                                        <ajaxToolkit:ComboBox ID="cbxColonias" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="True" DropDownStyle="Simple" name="colonia" DataTextField="asenta" DataValueField="asenta" OnSelectedIndexChanged="cbxColonias_SelectedIndexChanged">
                                                        </ajaxToolkit:ComboBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label17" runat="server" class="control-label" Text="Calle:"></asp:Label>
                                                        <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label18" runat="server" class="control-label" Text="Número:"></asp:Label>
                                                        <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" onkeypress="return onlyNumbers(event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label19" runat="server" class="control-label" Text="Interior:"></asp:Label>
                                                        <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="panel-heading">
                                                    <h4>Detalle</h4>
                                                </div>
                                                <div class="row gutter">
                                                    <asp:Panel ID="pnlDetalleProductos" runat="server">
                                                        <div class="row gutter">
                                                            <%--<div class="col-md-3">
                                                                    <asp:Label ID="Label20" runat="server" class="control-label" Text="Tipo:"></asp:Label>
                                                                    <asp:Label ID="lblTipoProducto" runat="server" class="form-control"></asp:Label>--%>
                                                            <%--<asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged"></asp:DropDownList>--%>
                                                            <%--</div>--%>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="Label21" runat="server" class="control-label" Text="Código:"></asp:Label>
                                                                <%--<asp:TextBox ID="txtCodigoProducto" runat="server" class="form-control" name="codigoP" AutoPostBack="True" OnTextChanged="txtCodigoProducto_TextChanged"></asp:TextBox>
                                                                                            <ajaxToolkit:AutoCompleteExtender ID="txtCodigoProducto_AutoCompleteExtender" runat="server" BehaviorID="txtCodigoProducto_AutoCompleteExtender" DelimiterCharacters="" ContextKey="" ServiceMethod="getCodigoProductos" TargetControlID="txtCodigoProducto" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="true">
                                                                                            </ajaxToolkit:AutoCompleteExtender>--%>
                                                                <asp:Label ID="lblCodigoProducto" runat="server" class="form-control"></asp:Label>
                                                                <asp:Label ID="lblTipoProducto" runat="server" class="form-control" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="Label4" runat="server" class="control-label" Text="Precio de Venta:"></asp:Label>
                                                                <asp:TextBox ID="txtPrecioF" runat="server" class="form-control" name="precioF" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="lblRevenimiento" runat="server" class="control-label" Text="Rev:" Visible="False"></asp:Label>
                                                                <asp:TextBox ID="txtRevenimiento" runat="server" class="form-control" name="Revenimiento" onkeypress="return onlyNumbers(event)" Visible="False"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="Label27" runat="server" class="control-label" Text="Cantidad:"></asp:Label>
                                                                <asp:TextBox ID="txtCantOrdenada" runat="server" class="form-control" name="cantPrd" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="Label25" runat="server" class="control-label" Text="Unidad:"></asp:Label><br />
                                                                <asp:Label ID="lblUDM" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="lblElementos" runat="server" class="control-label" Text="Elemento:" Visible="False"></asp:Label>
                                                                <asp:DropDownList ID="ddlElemento" runat="server" class="form-control" DataTextField="elemento" DataValueField="id" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlElemento_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <br />
                                                <div class="panel-heading">
                                                    <h4>Orden</h4>
                                                </div>
                                                <div class="row gutter">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div class="row gutter">
                                                            <div class="col-md-12">
                                                                <asp:Label ID="Label6" runat="server" class="control-label" Text="Observaciones: (Éstas observaciones saldrán en la Remisión)"></asp:Label><br />
                                                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row gutter">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label3" runat="server" class="control-label" Text="Vendedor que aprobó:"></asp:Label>
                                                                <asp:DropDownList ID="ddlVendedorOrden" runat="server" class="form-control" DataTextField="vendedor" DataValueField="id"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-8">
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="row gutter">
                                                    <div class="col-md-5">
                                                        <br />
                                                        <asp:Button ID="bntCancelar" runat="server" Text="Cancelar" class="btn btn-default" OnClick="bntCancelar_Click" />
                                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="btnAceptar_Click" />
                                                        <br />
                                                        <br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <asp:Label ID="lblError" runat="server" Font-Size="X-Large" ForeColor="Red" />
                                    </div>
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
                                            <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button>--%>
                                            <h4 class="modal-title">
                                                <asp:Label ID="lblAux" runat="server"></asp:Label>
                                                <asp:Label ID="mlblTitle" runat="server" />
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="mlblMessage" runat="server" />
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="mbtnClose" runat="server" Text="Cancelar" class="btn btn-default" OnClick="mbtnClose_Click" />
                                            <button type="button" style="display: none;" id="btnClosePopup" class="btn btn-default" data-dismiss="modal">
                                                Close</button>
                                            <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.modal -->

                        </div>
                    </div>
                    <!-- Container fluid ends -->

                    </div>
                    <!-- Container fluid ends -->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtClaveCliente" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtNombreCliente" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlFP" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlVendedores" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlProyectos" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtCP" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlEstados" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCiudades" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cbxColonias" EventName="SelectedIndexChanged" />
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlTipoProducto" EventName="SelectedIndexChanged" />--%>
                    <asp:AsyncPostBackTrigger ControlID="ddlElemento" EventName="SelectedIndexChanged" />
                    <%--<asp:AsyncPostBackTrigger ControlID="txtCodigoProducto" EventName="TextChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
