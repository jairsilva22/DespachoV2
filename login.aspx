<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="despacho.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Arise Admin Panel" />
    <meta name="keywords" content="Admin, Dashboard, Bootstrap3, Sass, transform, CSS3, HTML5, Web design, UI Design, Responsive Dashboard, Responsive Admin, Admin Theme, Best Admin UI, Bootstrap Theme, Themeforest, Bootstrap" />
    <meta name="author" content="Ramji" />
    <link rel="shortcut icon" href="img/fav.png">
    <title>Sistema de Despacho</title>
    <link href="css/assets/styles.css" rel="stylesheet" />


    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />

    <!-- Main CSS -->
    <link href="css/main.css" rel="stylesheet" media="screen" />

    <!-- Ion Icons -->
    <link href="fonts/font-awesome.css" rel="stylesheet" />

    <!-- Ion Icons -->
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet" />

    <!-- Error CSS -->
    <link href="css/login.css" rel="stylesheet" media="screen">

    <!-- Animate CSS -->
    <link href="css/animate.css" rel="stylesheet" media="screen">

    <!-- Ion Icons -->
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet" />


</head>
<body>
    <form id="form2" runat="server">
        <div id="box" class="animated bounceIn">
            <div id="top_header">
                <img src="img/pepi_logo.png" class="logo" alt="Sistema de despacho" width="100" /><br />
                <br />
                <br />
                <br />
                <h5>Bienvenidos al
                    <br />
                    Sistema de despacho...
                </h5>
            </div>
            <div id="inputs">
                <div class="form-block">
                    <asp:TextBox ID="txtUsuario" name="usuario" CssClass="form-control" runat="server" placeholder="Usuario" required="required"></asp:TextBox>
                    <i class="icon-email"></i>
                </div>
                <div class="form-block">
                    <asp:TextBox ID="txtPassword" name="password" CssClass="form-control" runat="server" placeholder="Contraseña" required="required" TextMode="Password"></asp:TextBox>
                    <i class="icon-eye4"></i>
                </div>
                <div class="form-block">
                    <asp:DropDownList ID="ddlSucursal" name="sucursal" runat="server" CssClass="form-control" placeholder="Sucursal" DataSourceID="ds" DataTextField="nombre" DataValueField="id"></asp:DropDownList>
                    <asp:SqlDataSource ID="ds" runat="server" ConnectionString="<%$ ConnectionStrings:cnx %>" SelectCommand="SELECT [id], [nombre] FROM [sucursales] WHERE ([elimino] IS NULL)"></asp:SqlDataSource>
                </div>
                <div class="form-block">
                    <asp:Button ID="Entrar" runat="server" CssClass="boton_login" Style="font-size: 16px; font-family: 'Myriad Pro'; width: 140px" Text="Entrar" OnClick="Entrar_Click" />
                </div>
                <div class="form-block" style="align-content: center">
                    <asp:Label ID="lblError" runat="server" CssClass="control-label" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div id="bottom" class="clearfix">
                <div class="pull-right">
                    <label class="switch pull-right">
                        <input type="checkbox" class="switch-input" checked>
                        <span class="switch-label" data-on="Yes" data-off="No"></span>
                        <span class="switch-handle"></span>
                    </label>
                </div>
                <div class="pull-right">
                    <span class="cb-label">Recordarme</span>
                </div>
            </div>
        </div>



    </form>
</body>
</html>

