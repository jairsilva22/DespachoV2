<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ordenesAdd.aspx.cs" Inherits="despacho.ordenesAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">

    <script>

        
        function cerrarModal() {
            var gridDiv = document.querySelector('#myGrid');
            new agGrid.Grid(gridDiv, gridOptions);
            $("#myModalMensaje").modal("hide");
        }
        

        function abrirpopup() {
            mostrarGrid();
            //document.getElementById("frame").style.height = "100%";
            // document.getElementById("frame").style.width = "100%";
            // document.getElementById("frame").src = "clientesAG-grid.aspx";
            $("#modalGRID").modal("show");
        }
        //function abrirpopupProyectos() {
        //    //mostrarGridProyectos();
        //    //document.getElementById("frame").style.height = "100%";
        //    // document.getElementById("frame").style.width = "100%";
        //    // document.getElementById("frame").src = "clientesAG-grid.aspx";
        //    $("#modalProyectos").modal("show");
        //}

        function abrirpopup1() {
            window.open("clientesAG-grid.aspx", "Clientes", "width=1000,height=500, top: 30");
        }





        function cerrarmodal() {
            //cerrar modal 
            // document.getElementById("myGrid").innerHTML = "";
            document.getElementById("cuerpo1").innerHTML = "";
            // document.getElementById("frame").src = "";
            $("#modalGRID").modal("hide");
        }

        function cerrarmodalFrame() {
            $("#modalFrame").modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            document.getElementById("ctl00_ContentPlaceHolder_txtClaveCliente").value = "";
            document.getElementById("ctl00_ContentPlaceHolder_txtNombreCliente").value = "";
            document.getElementById("ctl00_ContentPlaceHolder_ddlFP").selectedIndex = "0";
            document.getElementById("ctl00_ContentPlaceHolder_ddlVendedores").selectedIndex = "0";
        }

        function cerrarmodalProyectos() {

            $("#modalFrame").modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function abrirpopupProyectos() {

            $("#modalFrame").modal("show");
            document.getElementById("h5Cliente").innerHTML = "<strong>Cliente: </strong>" + document.getElementById("ctl00_ContentPlaceHolder_txtNombreCliente").value;
        }

        function AlertaProyectos() {

            "use strict";
            alert("EL cliente no cuenta con un proyecto, favor de asignarle uno");
        }

        function mostrar(id, nombre, clave) {
            //console.log("ID: " + id + " Nombre: " + nombre + ". Clave: "+clave);

            //Limpiar selector
            limpiarddlP();

            //agregar la modificación con un ajax.        
            document.getElementById("ctl00_ContentPlaceHolder_txtClaveCliente").value = clave;
            document.getElementById("ctl00_ContentPlaceHolder_txtNombreCliente").value = nombre;

            // alert(id)
            document.getElementById("ctl00_ContentPlaceHolder_btnBuscar").click();
            $("#modalGRID").modal("hide");
            
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;<!-- Top Bar Starts -->
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Nueva Orden</h3>
                    <p>/ <a href="ordenes.aspx">Ordenes</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <ul class="right-stats" id="mini-nav-right">
                    <%# Eval("elemento") %>
                    <li>
                        <a href="home.aspx" class="btn btn-info">
                            <i class="icon-bulb"></i>Ayuda
                        </a>
                        <%# Eval("codigo") %>
                    </li>
                </ul>
            </div>
            <%# Eval("descripcion") %>
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
                                                <asp:Panel ID="pnlSolicitud" runat="server">
                                                    <div class="panel-heading">
                                                        <h4>Datos de la Solicitud</h4>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblFoli" runat="server" class="control-label" Text="Folio:" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblFolio" runat="server" class="form-control" name="folio" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
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
                                                            <asp:Label ID="Label3" runat="server" class="control-label" Text="Estado:"></asp:Label>
                                                            <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" class="form-control" DataSourceID="dsEstadoSolicitud" DataTextField="estado" DataValueField="id">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="dsEstadoSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT * FROM [estadosSolicitud]"></asp:SqlDataSource>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Label ID="Label28" runat="server" class="control-label" Text="Requiere Factura:"></asp:Label>
                                                            <asp:DropDownList ID="ddlReqFac" runat="server" class="form-control">
                                                                <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                                <asp:ListItem Value="SI">Si</asp:ListItem>
                                                                <asp:ListItem Value="NO">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel ID="Panel3" runat="server">
                                            <div class="panel-heading">
                                                <div class="col-md-12">
                                                    <div class="col-md-2" >
                                                        <a href="javascript:abrirpopup();" style="color: blue; visibility:visible;"><i class="icon-search"></i>Visualizar Clientes</a>
                                                    </div>
                                                    <%--<div class="col-md-2">
                                                        <a href="javascript:abrirpopup1();">Visualizar Clientes ventana emergente</a>
                                                    </div>--%>
                                                    <div class="col-md-10 text-center">
                                                        <h4>Datos del Cliente</h4>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label5" runat="server" class="control-label" Text="Clave:"></asp:Label>
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
                                                    <asp:Label ID="Label9" runat="server" class="control-label" Text="Forma de Pago:"></asp:Label>
                                                    <asp:DropDownList ID="ddlFP" runat="server" class="form-control" AutoPostBack="True" DataTextField="nombre" DataValueField="id" OnSelectedIndexChanged="ddlFP_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label10" runat="server" class="control-label" Text="Vendedor:"></asp:Label>
                                                    <asp:DropDownList ID="ddlVendedores" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlVendedores_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <%--  <asp:Panel ID="PanelGrid" runat="server">
                                            <div>
                                                <label id="cuerpo1" class="label label-danger"></label>
                                                <div id="myGrid" style="height: 200px; width: 100%;" class="ag-theme-alpine"></div>
                                            </div>
                                        </asp:Panel>--%>
                                        <br />
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="panel-heading">
                                                <div class="col-md-12">
                                                    <div class="col-md-2" style="visibility: hidden;">
                                                            <a href="javascript:abrirpopupProyectos();" style="color: blue"><i class="icon-search"></i>Visualizar Clientes</a>
                                                    </div>
                                                    <div class="col-md-8 text-center">
                                                        <h4>Datos del Proyecto</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label4" runat="server" class="control-label" Text="Proyecto:"></asp:Label>
                                                    <asp:DropDownList ID="ddlProyectos" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" OnSelectedIndexChanged="ddlProyectos_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label6" runat="server" class="control-label" Text="Código Postal:"></asp:Label>
                                                    <asp:TextBox ID="txtCP" runat="server" class="form-control" MaxLength="5" onkeypress="return onlyNumbers(event)" AutoPostBack="True" OnTextChanged="txtCP_TextChanged" AutoComplete="off"></asp:TextBox>
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
                                                    <asp:Label ID="Label11" runat="server" class="control-label" Text="Calle:"></asp:Label>
                                                    <asp:TextBox ID="txtCalle" runat="server" class="form-control" name="calle" AutoComplete="off"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label12" runat="server" class="control-label" Text="Número:"></asp:Label>
                                                    <asp:TextBox ID="txtNumero" runat="server" class="form-control" name="numero" onkeypress="return onlyNumbers(event)" AutoComplete="off"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label13" runat="server" class="control-label" Text="Interior:"></asp:Label>
                                                    <asp:TextBox ID="txtInterior" runat="server" class="form-control" name="interior" MaxLength="5" AutoComplete="off"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                        <a href="javascript:abrirpopupProyectos();" style="color: blue; visibility:hidden" ><i class="icon-search"></i>Modal Proyectos</a>
                                                    </div>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel ID="Panel4" runat="server">
                                            <div class="row gutter">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row gutter">
                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlDetalleProductos" runat="server">
                                    <div class="panel-heading">
                                        <h4></h4>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label19" runat="server" class="control-label" Text="Tipo:"></asp:Label>
                                            <asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control" DataTextField="tipo" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblElementos" runat="server" class="control-label" Text="Elemento:" Visible="False"></asp:Label>
                                            <asp:DropDownList ID="ddlElemento" runat="server" class="form-control" DataTextField="elemento" DataValueField="id" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlElemento_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label17" runat="server" class="control-label" Text="Código:"></asp:Label>
                                            <asp:TextBox ID="txtCodigoProducto" runat="server" class="form-control" name="codigoP" AutoPostBack="True" OnTextChanged="txtCodigoProducto_TextChanged" AutoComplete="off"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="txtCodigoProducto_AutoCompleteExtender" runat="server" BehaviorID="txtCodigoProducto_AutoCompleteExtender" DelimiterCharacters="" ContextKey="" ServiceMethod="getCodigoProductos" TargetControlID="txtCodigoProducto" MinimumPrefixLength="1" CompletionSetCount="1" UseContextKey="true">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label18" runat="server" class="control-label" Text="Descripción:"></asp:Label>
                                            <asp:TextBox ID="txtDescProducto" runat="server" class="form-control" name="descP"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label25" runat="server" class="control-label" Text="Unidad:"></asp:Label><br />
                                            <asp:Label ID="lblUDM" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="lblRevenimiento" runat="server" class="control-label" Text="Revenimiento:" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtRevenimiento" runat="server" class="form-control" name="Revenimiento" onkeypress="return onlyNumbers(event)" Visible="False"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label22" runat="server" class="control-label" Text="Precio Unitario:"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label15" runat="server" class="control-label" Text="Factor:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label16" runat="server" class="control-label" Text="Precio Neto con Descuento:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label29" runat="server" class="control-label" Text="Precio Neto con Descuento + IVA:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label14" runat="server" class="control-label" Text="Cantidad:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label23" runat="server" class="control-label" Text="Sub-Total:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label24" runat="server" class="control-label" Text="IVA:"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <br />
                                        </div>
                                        <div class="col-md-1">
                                            <asp:HiddenField runat="server" ID="hdCliente"></asp:HiddenField>
                                            <asp:Button runat="server" Text="Button" ID="btnBuscar" OnClick="btnBuscar_Click" Style="display: none"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                         <div class="col-md-1">
                                        </div>
                                         <div class="col-md-1">
                                             <asp:Label ID="lblPrecioU" runat="server" class="form-control" Text=""></asp:Label>
                                            <asp:Label ID="lblPrecioU2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                        </div>
                                         <div class="col-md-3">
                                            <asp:DropDownList ID="ddlFactor" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFactor_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                         <div class="col-md-1">
                                            <asp:TextBox ID="txtPrecioFactor" runat="server" class="form-control" name="precioF" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(event)" OnTextChanged="txtPrecioFactor_TextChanged"></asp:TextBox>
                                            <asp:Label ID="txtPrecioFactor2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                        </div>
                                         <div class="col-md-1">
                                            <asp:TextBox ID="txtprecioFIVA" runat="server" class="form-control" name="precioFIVA" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(event)" OnTextChanged="txtprecioFIVA_TextChanged"></asp:TextBox>
                                        </div>
                                         <div class="col-md-1">
                                            <asp:TextBox ID="txtCantOrdenada" runat="server" class="form-control" name="cantPrd" onkeypress="return onlyDotsAndNumbers(event)" AutoPostBack="True" OnTextChanged="txtCantOrdenada_TextChanged"></asp:TextBox>
                                        </div>
                                         <div class="col-md-1">
                                            <asp:Label ID="lblSubTotal" runat="server" class="form-control" Text=""></asp:Label>
                                            <asp:Label ID="lblSubTotal2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                        </div>
                                         <div class="col-md-1">
                                            <asp:TextBox ID="txtIVA" runat="server" class="form-control" name="iva" AutoPostBack="True" OnTextChanged="txtIVA_TextChanged"></asp:TextBox>
                                        </div>
                                         <div class="col-md-1">
                                             <asp:Button ID="btnInsertarProducto" runat="server" Text="Insertar" class="btn btn-info" OnClick="btnInsertarProducto_Click" />
                                            <asp:Button ID="btnModProducto" runat="server" class="btn btn-info" OnClick="btnModProducto_Click" Text="Modificar" Visible="False" />
                                        </div>
                                    </div>
                                    <div class="row gutter" style="display:block">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-2">
                                            <asp:Label ID="Label20" runat="server" class="control-label" Text="Total:"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" class="form-control" Text=""></asp:Label>
                                            <asp:Label ID="lblTotal2" runat="server" class="form-control" Text="" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row gutter">
                                        <div class="col-md-12">
                                            <%--<asp:UpdatePanel ID="upGrid" runat="server">
                                                <ContentTemplate>--%>
                                            <div id="ContentPlaceHolder_upGrid">
                                                <div class="row gutter">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <div class="panel panel-blue">
                                                            <div class="panel-heading">
                                                                <h4>Detalle</h4>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="table-responsive">
                                                                    <asp:ListView ID="lvDetalles" runat="server" OnItemCommand="lvDetalles_ItemCommand" OnItemDeleting="lvDetalles_ItemDeleting">
                                                                        <LayoutTemplate>
                                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                                                        <th style="text-align: center" id="elemento">Elemento</th>
                                                                                        <th style="text-align: center" id="codigo">Código</th>
                                                                                        <th id="descripción">Descripción</th>
                                                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                                                        <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                                                        <th style="text-align: center" id="precioU">Precio Unitario</th>
                                                                                        <th style="text-align: center" id="factor">Factor</th>
                                                                                        <th style="text-align: center" id="precioF">Precio Neto con Descuento</th>
                                                                                        <th style="text-align: center" id="precioFIVA">Precio Neto con Descuento + IVA</th>
                                                                                        <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                                        <th style="text-align: center" id="subTotal">Sub-Total</th>
                                                                                        <th style="text-align: center" id="iva">IVA</th>
                                                                                        <th style="text-align: center" id="total">Total</th>
                                                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                                                <tfoot>
                                                                                    <tr>
                                                                                        <th style="text-align: center">Tipo</th>
                                                                                        <th style="text-align: center">Elemento</th>
                                                                                        <th style="text-align: center">Código</th>
                                                                                        <th>Descripción</th>
                                                                                        <th style="text-align: center">Unidad</th>
                                                                                        <th style="text-align: center">Revenimiento</th>
                                                                                        <th style="text-align: center">Precio Unitario</th>
                                                                                        <th style="text-align: center">Factor</th>
                                                                                        <th style="text-align: center">Precio Neto con Descuento</th>
                                                                                        <th style="text-align: center">Precio Neto con Descuento + IVA</th>
                                                                                        <th style="text-align: center">Cantidad</th>
                                                                                        <th style="text-align: center">Sub-Total</th>
                                                                                        <th style="text-align: center">IVA</th>
                                                                                        <th style="text-align: center">Total</th>
                                                                                        <th style="text-align: center">Modificar</th>
                                                                                        <th style="text-align: center">Eliminar</th>
                                                                                    </tr>
                                                                                </tfoot>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center"><%# Eval("tipo") %></td>
                                                                                <td style="text-align: center"><%# Eval("elemento") %></td>
                                                                                <td style="text-align: center"><%# Eval("codigo") %></td>
                                                                                <td><%# Eval("descripcion") %></td>
                                                                                <td style="text-align: center"><%# Eval("unidad").ToString().Trim() %></td>
                                                                                <td style="text-align: center"><%# Eval("revenimiento") %></td>
                                                                                <td style="text-align: center"><%# Eval("precioU") %></td>
                                                                                <td style="text-align: center"><%# Eval("factor") + " - " + Eval("porcentaje") + "%" %></td>
                                                                                <td style="text-align: center"><%# Eval("precioF") %></td>
                                                                                <td style="text-align: center"><%# Eval("precioFIVA") %></td>
                                                                                <td style="text-align: center"><%# Eval("cantidad") %></td>
                                                                                <td style="text-align: center"><%# Eval("subtotal") %></td>
                                                                                <td style="text-align: center"><%# Eval("iva") %></td>
                                                                                <td style="text-align: center"><%# Eval("total") %></td>
                                                                                <td style="text-align: center">
                                                                                    <asp:LinkButton ID="lbtnMod" runat="server" CommandArgument='<%# Eval("id") + "ˇ" + Eval("idTipoProducto") + "ˇ" + Eval("idElemento") + "ˇ" + Eval("codigo") + "ˇ" + Eval("descripcion") + "ˇ" + Eval("unidad") + "ˇ" + Eval("revenimiento") + "ˇ" + Eval("precioU") + "ˇ" + Eval("idFactor") + "ˇ" + Eval("precioF") + "ˇ" + Eval("cantidad") + "ˇ" + Eval("subtotal") + "ˇ" + Eval("iva") + "ˇ" + Eval("total") %>' CommandName="modificar"><i class="icon-edit"></i></asp:LinkButton>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="eliminar"><i class="icon-delete"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <table id="responsiveTable" class="table table-striped table-bordered no-margin">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="text-align: center" id="tipo">Tipo</th>
                                                                                        <th style="text-align: center" id="elemento">Elemento</th>
                                                                                        <th style="text-align: center" id="codigo">Código</th>
                                                                                        <th id="descripción">Descripción</th>
                                                                                        <th style="text-align: center" id="unidad">Unidad</th>
                                                                                        <th style="text-align: center" id="Revenimiento">Revenimiento</th>
                                                                                        <th style="text-align: center" id="precioU">Precio Unitario</th>
                                                                                        <th style="text-align: center" id="factor">Factor</th>
                                                                                        <th style="text-align: center" id="precioF">Precio Factor</th>
                                                                                        <th style="text-align: center" id="cantidad">Cantidad</th>
                                                                                        <th style="text-align: center" id="subTotal">Sub-Total</th>
                                                                                        <th style="text-align: center" id="iva">IVA</th>
                                                                                        <th style="text-align: center" id="total">Total</th>
                                                                                        <th style="text-align: center" id="modificar">Modificar</th>
                                                                                        <th style="text-align: center" id="eliminar">Eliminar</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr>
                                                                                    <td colspan="15">
                                                                                        <label class="label label-danger">¡No hay Productos Registrados!</label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row gutter">

                                        <asp:Panel ID="Panel5" runat="server">
                                            <div class="form-group">
                                                <asp:Panel ID="Panel6" runat="server">
                                                    <div class="panel-heading">
                                                        <h4>Datos de la Orden</h4>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label21" runat="server" class="control-label" Text="Comentarios en la remisión:"></asp:Label>
                                                            <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label27" runat="server" class="control-label" Text="Comentarios sobre la Ubicación:"></asp:Label>
                                                            <asp:TextBox ID="txtComentariosUbicacion" runat="server" class="form-control" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row gutter">
                                                        <div class="col-md-4">
                                                            <asp:Label ID="Label26" runat="server" class="control-label" Text="Aprobado Por:"></asp:Label>
                                                            <asp:DropDownList ID="ddlV2" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </asp:Panel>
                                        <div class="row gutter">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnFinalizar" runat="server" class="btn btn-info" OnClick="btnFinalizar_Click" Text="Terminar" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
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
                                        <asp:Button ID="mbtnAceptar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="mbtnAceptar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal -->
                        <div class="form-group">
                            <div class="row gutter">
                                <div class="col-md-8">
                                    &nbsp;<asp:HiddenField ID="hfIdCliente" runat="server" />
                                    <asp:HiddenField ID="hfIdVendedor" runat="server" />
                                    <asp:HiddenField ID="hfIdProyecto" runat="server" />
                                    <asp:HiddenField ID="hfIdSolicitud" runat="server" />
                                    <asp:HiddenField ID="hfIdDS" runat="server" />
                                    <asp:HiddenField ID="hfIdFP" runat="server" />
                                    <asp:HiddenField ID="hfIdProducto" runat="server" />
                                    <asp:HiddenField ID="hfIdElemento" runat="server" />
                                    <asp:HiddenField ID="hfIdEstado" runat="server" />
                                    <asp:HiddenField ID="hfIdCiudad" runat="server" />
                                    <asp:HiddenField ID="hfIdEstadoCP" runat="server" />
                                    <asp:HiddenField ID="hfIdCiudadCP" runat="server" />
                                    <asp:HiddenField ID="hfSearchBy" runat="server" />
                                    <asp:HiddenField ID="hfIdOrden" runat="server" />
                                    <asp:HiddenField ID="hfIdUDM" runat="server" />
                                </div>
                            </div>
                            <!-- Modal iFrame Proyectos -->
                            <div class="modal " id="modalFrame" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog">
                                    <%--style="width: 80%"--%>
                                    <div class="modal-content">
                                        <%--style="width: 100%"--%>
                                        <div class="modal-header">
                                            <h4 class="modal-title">Proyectos </h4>
                                        </div>
                                        <div class="modal-body">
                                            <div>
                                                <label id="cuerpo3" class="label label-danger"></label>
                                                <h5 id="h5Cliente"></h5>
                                                <br />

                                                <iframe src="frameProyectos.aspx?id=<%=hfIdCliente.Value.ToString()%>"  style="width: 100%; height: 400px; border:none;" " ></iframe>
                                                <%--<iframe src="frameProyectos.aspx?id=<%= Request.QueryString["id"] %>"  style="width: 100%; height: 400px; border:none;"" ></iframe>--%>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="cerrarModalProyectos" runat="server" Text="Cerrar" class="btn btn-danger" OnClick="mbtnCloseModalP_Click" />
                                            <%--<a href="javascript: cerrarmodalFrame();" class="btn btn-danger">Cerrar</a>--%>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal -->
                        <div class="modal fade" id="myModalMensaje">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title"></h4>
                                    </div>
                                    <div class="modal-body">
                                        <label id="cuerpo"></label>
                                    </div>
                                    <div class="modal-footer">
                                        <%--  <asp:Button runat="server" ID="btnmodalAceptar" CssClass="btn btn-default" Text="Aceptar" OnClick="btnmodalAceptar_Click" AutoPostBack="true"  />--%>
                                        <button type="button" id="btnClosePopup1" class="btn btn-default" data-dismiss="modal">Close</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Row Ends -->


                        <br />
                        <br />
                        </div>
                        </div>
                        </form>
                        </div>
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
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoProducto" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlElemento" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCodigoProducto" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlFactor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlV2" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtPrecioFactor" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtCantOrdenada" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnInsertarProducto" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModProducto" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lvDetalles" EventName="ItemCommand" />
                        <asp:AsyncPostBackTrigger ControlID="txtIVA" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button runat="server" style="display: none" ID="btnMostrarCte" OnClick="btnMostrarCte_Click" />
                <asp:HiddenField runat="server" ID="hdClienteAG" />
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalGRID">
        <div class="modal-dialog"> <%--style="width: 80%"--%>
            <div class="modal-content"> <%--style="width: 100%"--%>
                <div class="modal-header">
                    <h4 class="modal-title">Clientes </h4>
                </div>
                <div class="modal-body">
                    <div>
                        <label id="cuerpo1" class="label label-danger"></label>
                        <input
                            type="text"
                            class="form-control"
                            id="filter-text-box"
                            placeholder="Buscar..."
                            autocomplete="off"
                            oninput="onFilterTextBoxChanged()" />
                        <br />

                        <div id="myGrid" style="height: 500px; width: 100%;" class="ag-theme-alpine"></div>

                        <%--<iframe src="clientesAG-grid.aspx" style="width: 100%; height: 400px;"></iframe>--%>

                    </div>
                </div>
                <div class="modal-footer">
                    <a href="javascript: cerrarmodal();" class="btn btn-default">Cerrar</a>

                </div>
            </div>
        </div>
    </div>

    <!-- Modal para proyectos -->
    <div class="modal fade" id="modalProyectos">
        <div class="modal-dialog"> <%--style="width: 80%"--%>
            <div class="modal-content"> <%--style="width: 100%"--%>
                <div class="modal-header">
                    <h4 class="modal-title">Clientes </h4>
                </div>
                <div class="modal-body">
                    <div>
                        <label id="cuerpo11" class="label label-danger"></label>
                        <input
                            type="text"
                            class="form-control"
                            id="filter-text-box-proyectos"
                            placeholder="Buscar..."
                            autocomplete="off"
                            oninput="onFilterTextBoxProyectosChanged()" />
                        <br />

                        <div id="myGridProyectos" style="height: 500px; width: 100%;" class="ag-theme-alpine"></div>

                        <%--<iframe src="clientesAG-grid.aspx" style="width: 100%; height: 400px;"></iframe>--%>

                    </div>
                </div>
                <div class="modal-footer">
                    <a href="javascript: cerrarmodal();" class="btn btn-default">Cerrar</a>

                </div>
            </div>
        </div>
    </div>
    <script>

        function verqueonda() {
            console.log("Hola");
        }
        
        const eGridDiv = document.getElementById("myGrid");
        //eGridDiv.innerHTML = "";
        const columnDefs = [
            { headerName: "", field: "select", width: 10, cellRenderer: seleccionarRenderer, filter: false },
            { headerName: "Clave", field: "clave", width: 40 },
            { headerName: "Nombre", field: "nombre", editable: true, width: 200, },
            { headerName: "Forma Pago", field: "fp", width: 40, editable: true, cellEditorSelector: cellEditorSelector },
            { field: "mod", minWidth: 55, cellRenderer: TotalValueRenderer, filter: false },
        ];

        // specify the data
        const rowData = [
            { id: "Toyota", clave: "Celica", nombre: "Luis", códigopostal: 25050 },
            { id: "Ford", clave: "Mondeo", nombre: "Enrique", códigopostal: 30800 },
            { id: "Porsche", clave: "Boxster", nombre: "Sandoval", códigopostal: 15750 }
        ];


        // let the grid know which columns and what data to use
        const gridOptions = {
            columnDefs: columnDefs,
            rowData: rowData,
            rowSelection: 'single',
            animateRows: true,
            defaultColDef: { sortable: true, minWidth: 100, editable: true },
            maintainColumnOrder: true,
            cacheQuickFilter: true
        };


        //Para proyectos
        //const pGridDiv = document.getElementById("myGridProyectos");
        //const columnDefsP = [
        //    { headerName: "", field: "select", width: 10, cellRenderer: seleccionarProyectoRenderer, filter: false },
        //    { headerName: "Nombre", field: "nombre", editable: true, width: 200, },
        //    { headerName: "Forma Pago", field: "fp", width: 40, editable: false },
        //    { field: "mod", minWidth: 55, cellRenderer: TotalValueRenderer, filter: false },
        //];

        //Para el selector de forma de pago
        function cellEditorSelector(params) {
            return {
                component: 'agRichSelectCellEditor',
                params: {
                    values: ['Crédito', 'Efectivo', 'Bloqueado', 'Sin Crédito'],
                },
                popup: true,
            };
        }

        function limpiarddlP() {
            const select = document.getElementById("ctl00_ContentPlaceHolder_ddlProyectos");
            for (let i = select.options.length; i >= 0; i--) {
                select.remove(i);
            }
            //console.log("Limpiado");
        }

        function documentReady() {
            
            const gridDiv = document.querySelector('#myGrid');
            new agGrid.Grid(gridDiv, gridOptions);

        }
        $(document).ready(documentReady());


        function onFilterTextBoxChanged() {
            gridOptions.api.setQuickFilter(
                document.getElementById('filter-text-box').value
            );
        }

        function onFilterTextBoxProyectosChanged() {
            gridOptions.api.setQuickFilter(
                document.getElementById('filter-text-box-proyectos').value
            );
        }

        function onPrintQuickFilterTexts() {
            gridOptions.api.forEachNode(function (rowNode, index) {
                console.log(
                    'Row ' +
                    index +
                    ' quick filter text is ' +
                    rowNode.quickFilterAggregateText
                );
            });
        }

        

        function buscarClientes() {
            var datos = "[{fp:\"\", nombre: \"\", clave: \"\", mod: 0, select: 0}]";
            $.ajax({
                type: "POST",
                url: "ordenesAdd.aspx/clientesAGGrid(<%= Request.Cookies["ksroc"]["idSucursal"]%>)",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //console.log((data));
                     datos = JSON.Parse(data);
                  //  datos = data;

                },
                error: function (XMLHttpRequest) {
                    var error = eval(XMLHttpRequest.responseText);
                    console.log("Error: " + error);
                    // datos = error.Message;
                    //$("#myModalMensaje").modal("show");
                }
            });

            return datos;
        }

        function mostrarGrid() {

            $.ajax({
                type: "POST",                                                                   // tipo de request que estamos generando
                url: 'ordenesAdd.aspx/llenarAgGrid',                                            //URL al que vamos a hacer el pedido
                data: '{idSucursal: ' + <%= Request.Cookies["ksroc"]["idSucursal"]%> + '}',     // data es un arreglo JSON que contiene los parámetros que 
                // van a ser recibidos por la función del servidor
                contentType: "application/json; charset=utf-8",                                 // tipo de contenido
                dataType: "json",                                                               // formato de transmición de datos
                async: true,                                                                    // si es asincrónico o no
                success: function (resultado) {                                                 // función que va a ejecutar si el pedido fue exitoso
                    var num = resultado.d;
                    num = JSON.parse(num);
                    //console.log(num);
                    const rowDataB = num;
                    gridOptions.api.setRowData(rowDataB);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
                    var error = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(error.Message);
                }
            });

        }

        function mostrarGridProyectos() {

            $.ajax({
                type: "POST",                                                                   // tipo de request que estamos generando
                url: 'ordenesAdd.aspx/llenarAgGridPr',                                          //URL al que vamos a hacer el pedido
                data: '{idSucursal: ' + <%= Request.Cookies["ksroc"]["idSucursal"]%> + '}',     // data es un arreglo JSON que contiene los parámetros que 
                // van a ser recibidos por la función del servidor
                contentType: "application/json; charset=utf-8",                                 // tipo de contenido
                dataType: "json",                                                               // formato de transmición de datos
                async: true,                                                                    // si es asincrónico o no
                success: function (resultado) {                                                 // función que va a ejecutar si el pedido fue exitoso
                    var num = resultado.d;
                    num = JSON.parse(num);
                    console.log(num);
                    const rowDataB = num;
                    //gridOptions.api.setRowData(rowDataB);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
                    var error = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(error.Message);
                }
            });

        }

        function modificar(id, nombre, clave, fp) {
            // console.log("ID: " + id + " Nombre: " + nombre);
            //agregar la modificación con un ajax.
            var prd = document.getElementById("cuerpo1");
            var textFilter = document.getElementById("filter-text-box");
            $.ajax({
                type: "POST",
                url: "Ajax/modificarCliente.aspx",
                data: { "clave": clave, "idCliente": id, "nombre": nombre, "fp": fp },
                async: true,
                success: function (data) {
                    mostrarGrid();
                    // $("#repetido").val(data);
                    if (data != "") {
                        prd.innerHTML = data;
                    }
                    else {
                        prd.innerHTML = "Por el momento no se puede modificar al Cliente.";
                    }


                    //$("#myModalMensaje").modal("show");
                },
                error: function (XMLHttpRequest) {
                    var error = eval(XMLHttpRequest.responseText);
                    console(error.Message);
                    prd.innerHTML = error.Message;
                    //$("#myModalMensaje").modal("show");
                }
            });
        }

    </script>
</asp:Content>