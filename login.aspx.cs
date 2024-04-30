using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace despacho
{
    public partial class login : System.Web.UI.Page
    {
        cUsuarios cUsr = new cUsuarios();
        cUtilidades cUtl = new cUtilidades();
        cEmpleadosPercepciones per = new cEmpleadosPercepciones();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlSucursal.SelectedValue = Request.Cookies["ksroc"]["idSucursal"];
                }
                catch (Exception)
                {
                    ddlSucursal.SelectedIndex = 0;
                }
            }
        }

        protected void Entrar_Click(object sender, EventArgs e)
        {
            try
            {
                //hacemos el login del usuario
                if (cUsr.login(txtUsuario.Text, txtPassword.Text, int.Parse(ddlSucursal.SelectedValue)) == true)
                {
                    //generamos las variables de sesion
                    Response.Cookies["login"]["usuario"] = cUsr.usuario;
                    Response.Cookies["login"]["idPerfil"] = cUsr.idPerfil.ToString();
                    Response.Cookies["login"]["idSucursal"] = cUsr.idSucursal.ToString();
                    Response.Cookies["login"]["id"] = cUsr.id.ToString();
                    //generamos las variables de sesion
                    Response.Cookies["ksroc"]["usuario"] = cUsr.usuario;
                    Response.Cookies["ksroc"]["idPerfil"] = cUsr.idPerfil.ToString();
                    Response.Cookies["ksroc"]["idSucursal"] = cUsr.idSucursal.ToString();
                    Response.Cookies["ksroc"]["id"] = cUsr.id.ToString();
                    //Cookies para compras
                    Response.Cookies["ksroc"]["us"] = txtUsuario.Text;
                    Response.Cookies["ksroc"]["Pa"] = txtPassword.Text;
                    Response.Cookies["ksroc"]["sucursal"] = ddlSucursal.SelectedItem.Text.Replace(" ", "¬");
                    Response.Cookies["ksroc"]["sys"] = "Despacho";
                    Response.Cookies["ksroc"].Expires = DateTime.Now.AddHours(14);
                    //enviamos a la pagina principal
                    Response.Redirect("~/home.aspx");
                }
                else
                {
                    //enviamos mensaje de error
                    lblError.Text = "Usuario no Encontrado !";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}