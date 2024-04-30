using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class SiteOld : System.Web.UI.MasterPage
    {
        cUsuarios cUser = new cUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Cookies["ksroc"] == null)
                {
                    Response.Redirect("login.aspx");
                }

                try
                {
                    DataTable dt = cUser.obtenerUsuarioByID(int.Parse(Request.Cookies["ksroc"]["id"]));
                    lblSucursalMaster.Text = dt.Rows[0]["sucursal"].ToString();
                    lblMPUserName.Text = dt.Rows[0]["nombre"].ToString();
                    lblMPUserName1.Text = dt.Rows[0]["nombre"].ToString();
                    lblMP.Text = dt.Rows[0]["descripcion"].ToString();
                    Request.Cookies["ksroc"]["tipo"] = dt.Rows[0]["descripcion"].ToString();
                    if (lblSucursalMaster.Text.Equals(""))
                        Response.Redirect("login.aspx");


                }
                catch (Exception)
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
    }
}