<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="filtroCuentaCliente.aspx.cs" Inherits="despacho.filtroCuentaCliente" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content runat="server" ContentPlaceHolderID="scripts">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <script src="js/AG-Grid/filtroClientesContpaq.js"></script>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Filtro de Reporte de Cuenta del Cliente</h3>
                    <p>/ <a href="home.aspx">Inicio</a></p>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <%--<ul class="right-stats" id="mini-nav-right">
                   
                    <li>
                        <a href="solicitudesAdd.aspx" class="btn btn-info">
                            <i class="icon-add-to-list"></i>Agregar
                        </a>
                        
                    </li>
                </ul>--%>
            </div>
        </div>
    </div>
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-blue">
                <p>&nbsp;</p>
                <asp:UpdatePanel ID="upPanel" runat="server">
                    <ContentTemplate>
                        <p style="text-align: center; margin-bottom: 30px">Filtro de Reporte de Estado de cuenta del cliente</p>
                        <div class="form-group">
                            <div class="row gutter" style="margin-bottom: 15px">
                                <div class="col-md-2 selectContainer">
                                </div>
                                <div class="col-md-4 selectContainer text-center">
                                    <label style="display: inline" class="control-label">Fecha Inicio:</label>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" name="fecha" TextMode="DateTime" AutoComplete="off" OnTextChanged="txtFechaFin_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                                </div>
                                <div class="col-md-4 selectContainer text-center">
                                    <label style="display: inline" class="control-label">Fecha Fin:</label>
                                    <asp:TextBox ID="txtFechaFin" runat="server" name="fecha" TextMode="DateTime" AutoComplete="off" OnTextChanged="txtFechaFin_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin" FirstDayOfWeek="Monday" Format="yyyy/MM/dd" PopupPosition="BottomRight" />
                                </div>
                                <div class="col-md-2 selectContainer">
                                </div>
                            </div>
                            <div class="row gutter" style="margin-bottom: 15px">
                                <div class="col-md-4 selectContainer">
                                </div>
                                <div class="col-md-4 selectContainer text-center" style="display:none;">
                                    <label style="display: inline" class="control-label">Cliente:</label>
                                    <%-- <ajaxToolkit:ComboBox ID="ddlClienteInicio" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="Simple" Style="width: 75%" AutoPostBack="True"></ajaxToolkit:ComboBox>--%>
                                    <asp:DropDownList ID="ddlCliente" runat="server" Style="display: none;"></asp:DropDownList>
                                    <asp:Panel ID="panel3" runat="server">
                                        <!-- Text box para búsqueda de clientes. Agregado por Enrique 19/09/2022 -->
                                        <asp:TextBox ID="txtNombreCliente" runat="server" class="form-control" AutoPostBack="True"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtNombreCliente_AutoCompleteExtender" runat="server" BehaviorID="TtxtNombreCliente_AutoCompleteExtender" DelimiterCharacters="" ServicePath="" TargetControlID="txtNombreCliente" ServiceMethod="getDataNombreCliente" MinimumPrefixLength="1" CompletionSetCount="1">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-4 selectContainer">
                                </div>
                            </div>
                            <div class="row gutter" style="margin-bottom: 15px">
                                <div class="col-md-4 selectContainer">
                                </div>
                                <div class="col-md-4 selectContainer text-center">
                                    <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col-md-4 selectContainer">
                                </div>
                            </div>
                            <div class="row gutter" style="display: none;">
                                <div class="col-md-2 selectContainer">
                                </div>
                                <div class="col-md-4 selectContainer text-center">
                                    <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" />
                                </div>
                                <div class="col-md-4 selectContainer text-center">
                                    <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />--%>
                                </div>
                                <div class="col-md-2 selectContainer">
                                </div>
                            </div>
                            <br />
                            <div class="row gutter" style="margin-bottom: 15px">
                                <div class="col-md-2 selectContainer"></div>
                                <div class="col-md-7 selectContainer">
                                    <input
                                      type="text"
                                      id="filter-text-box"
                                      placeholder="Buscar..."
                                      oninput="onFilterTextBoxChanged()"
                                      class="form-control"
                                      autocomplete="off"
                                    />
                                    <br />
                                    <div id="myGrid" style="height: 500px;" class="ag-theme-alpine"></div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

    <script>
        function sendReport(sucursal, fi, ff, cid) {
            console.log("Sucursal: " + sucursal + ". Fecha inicio: " + fi + ". Fecha final: " + ff + ". Id Cliente: " + cid);

            window.open("ReporteCuentaCliente.aspx?idSucursal=" + sucursal + "&FechaInicio=" + fi + "&FechaFin=" + ff + "&CIDCLIENTEPROVEEDOR=" + cid, "_blank");
        }
    </script>
    <script>
        function quitarError() {
            document.getElementById("ctl00_ContentPlaceHolder_lblError").innerHTML = "";
        }
        function mostrar(cid, nombre, sucursal) {
            let fechainicial = document.getElementById("ctl00_ContentPlaceHolder_txtFechaInicio").value;
            let fechafinal = document.getElementById("ctl00_ContentPlaceHolder_txtFechaFin").value;
            if (fechafinal == "") {
                //alert("Falta seleccionar fechas");
                document.getElementById("ctl00_ContentPlaceHolder_lblError").innerHTML = "Falta seleccionar fecha final";
            }
            else if (fechainicial == "") {
                document.getElementById("ctl00_ContentPlaceHolder_lblError").innerHTML = "Falta seleccionar fecha inicial";
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder_lblError").innerHTML = "";
                var idSucursal;
                switch (sucursal) {
                    case "Concretos Saltillo Facturable":
                        idSucursal = 1;
                        break;
                    case "Concretos Saltillo Ventas General":
                        idSucursal = 2;
                        break;
                    case "Block Saltillo Facturable":
                        idSucursal = 3;
                        break;
                    case "Block Saltillo Ventas General":
                        idSucursal = 4;
                        break;
                    case "Block Irapuato":
                        idSucursal = 5;
                        break;
                    case "Irapuato Concretos":
                        idSucursal = 6;
                        break;
                    case "Block Irapuato Facturable":
                        idSucursal = 7;
                        break;
                    case "Block Irapuato Ventas General":
                        idSucursal = 8;
                        break;
                    case "Concretos Irapuato Facturable":
                        idSucursal = 9;
                        break;
                    case "Concretos Irapuato Ventas General":
                        idSucursal = 10;
                        break;
                    case "":
                        idSucursal = 1;
                        break;
                    default:
                        idSucursal = 1;
                        break;
                }

                console.log("Sucursal: " + idSucursal + ". Fecha inicio: " + fechainicial + ". Fecha final: " + fechafinal + ". Id Cliente: " + cid);

                window.open("ReporteCuentaCliente.aspx?idSucursal=" + idSucursal + "&FechaInicio=" + fechainicial + "&FechaFin=" + fechafinal + "&CIDCLIENTEPROVEEDOR=" + cid, "_blank");
            }
            
        }
    </script>
    <script>
        function llenarAgGrid(res) {
            const rowDataB = res;
            //console.log(rowDataB);
            $(document).ready(function () {
                gridOptions.api.setRowData(rowDataB);
                //console.log(JSON.stringify(res));
            });
            //gridOptions.api.setRowData(rowDataB);
        }

    </script>
</asp:Content>
