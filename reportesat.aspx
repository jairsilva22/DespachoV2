<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="reportesat.aspx.cs" Inherits="despacho.reportesat" %>
<!DOCTYPE html>
 <%# Response.Buffer = true %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="efectos/css/demos.css" media="screen" type="text/css"/>
    <script type="text/javascript" src="efectos/js/menu-for-applications.js"></script>
    <script src="js/jquery.min.js"></script>
    <script>
        function parametros() {
            var parametro = "";
            //verificamos si los campos están vacios
            if ($("[id*='ddlYear']").val() != "") {
                parametro += " AND YEAR(detFactura.fecha_alta) = " + $("[id*='ddlYear']").val()
            }

            //asignamos el valor de la variable a un input tipo hidden
            $("[id*='hdfParametros']").val(parametro)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="scriptM" runat="server"></asp:ScriptManager>        
        <div align="center">
            <h1 style="text-align:center">Reporte de Claves SAT</h1>
            <asp:Label runat="server" ID="lblYear" style="font-size:16px;"></asp:Label>
           <%-- <table align="center" class="table" style="width:80%">
                <tr>
                    <td style="font-size:12px;">Buscar Año: <asp:DropDownList ID="ddlYear" runat="server" Width="250"></asp:DropDownList>   
                           <asp:Button runat="server" ID="btnBuscar" Text="Enviar" OnClick="btnBuscar_Click" OnClientClick="parametros()" /> </td>
                </tr>
            </table>--%>
        </div>
        <div style="align-content: center" id="content">
            <asp:ListView ID="lvCveSAT" runat="server">
                <LayoutTemplate>
                    <table align="center" class="table" id="iseqchart" style="color: black; width:80%; " border="1" >
                        <tr style="background-color:#e33045">
                            <th width="15%">Clave</th>
                            <th>Clave Unidad</th>
                            <th>Descripción</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                       <td align="center"><%# Eval("claveProdServ").ToString() %></td>
                       <td align="center"><%# cveUnidad(Eval("claveProdServ").ToString()) %></td>
                       <td><%# cvesSat(Eval("claveProdServ").ToString()) %></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="table" style="align-content:center">
                        <tr style="background-color: #ed1414; color: white; font-size:20px">
                            <td colspan="20" style="text-align:center">
                                <label class="label label-danger fa-2x"><strong>¡No hay Claves!</strong></label></td>
                        </tr>
                        
                    </table>
                </EmptyDataTemplate>
                
            </asp:ListView>
        </div>
        
        <br />
        <br />
        <asp:HiddenField ID="hdfParametros" runat="server" />

    </form>
</body>
</html>
