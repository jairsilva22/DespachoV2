using System;
using System.Data;

namespace despacho
{
    public partial class Site : System.Web.UI.MasterPage
    {
        cUsuarios cUser = new cUsuarios();
        cMenu cMnu = new cMenu();
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
                    lblMPUserName1.InnerHtml = dt.Rows[0]["nombre"].ToString();
                    lblMP.Text = dt.Rows[0]["descripcion"].ToString();
                    Request.Cookies["ksroc"]["tipo"] = dt.Rows[0]["descripcion"].ToString();
                    if (lblSucursalMaster.Text.Equals(""))
                        Response.Redirect("login.aspx");

                    cargarMenu();

                }
                catch (Exception)
                {
                    Response.Redirect("login.aspx");
                }
            }

        }

        private void cargarMenu()
        {
            //DataTable dtM = cUtl.getMenu(int.Parse(Request.Cookies["ksroc"]["idPerfil"]));
            cMnu.idPerfil = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cMnu.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dtM = cMnu.obtenerByIdPerfil();
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["activo"].ToString().Equals("True"))
                {
                    lMenu.Text += "<li>";
                    //lMenu.Text += "<li id='mnu_" + dr["etiqueta"].ToString().Substring(0, 5) + "'>";
                    if (dr["etiqueta"].ToString().ToLower().Equals("compras"))
                        lMenu.Text += "<a href ='" + dr["url"] + "' target='_blank'>";
                    else
                        lMenu.Text += "<a href ='" + dr["url"] + "'>";
                    lMenu.Text += "<i class='" + dr["icono"] + "'>";
                    lMenu.Text += "</i>";
                    lMenu.Text += "<span class='menu-item'>" + dr["etiqueta"] + "</span>";
                    lMenu.Text += "</a>";
                    cargarSubMenus(int.Parse(dr["id"].ToString()));
                    //if (!cargarSubMenus(int.Parse(dr["id"].ToString())))
                    //    cargarMenuComoSubMenu(dr["etiqueta"].ToString(), dr["url"].ToString());
                    lMenu.Text += "</li>";
                }
            }
        }

        private bool cargarSubMenus(int id)
        {
            DataTable dtChilds = new DataTable();
            dtChilds.Clear();
            dtChilds = cMnu.obtenerByIdPerfil(id);
            if (dtChilds.Rows.Count.Equals(0))
                return false;
            lMenu.Text += "<ul>";
            foreach (DataRow drSM in dtChilds.Rows)
            {
                if (tieneSubMenus(int.Parse(drSM["id"].ToString())))
                {
                    if (drSM["activo"].ToString().Equals("True"))
                    {
                        lMenu.Text += "<li>";
                        lMenu.Text += "<a href ='" + drSM["url"] + "'>";
                        lMenu.Text += "<span class='menu-item'>" + drSM["etiqueta"] + "</span>";
                        lMenu.Text += "</a>";
                        cargarSubMenus(int.Parse(drSM["id"].ToString()));
                        lMenu.Text += "</li>";
                    }
                }
                else
                {
                    if (drSM["activo"].ToString().Equals("True"))
                    {
                        lMenu.Text += "<li>";
                        lMenu.Text += "<a href ='" + drSM["url"] + "'>";
                        lMenu.Text += "<span class='menu-item'>" + drSM["etiqueta"] + "</span>";
                        lMenu.Text += "</a>";
                        lMenu.Text += "</li>";
                    }
                }
            }
            lMenu.Text += "</ul>";
            return true;
        }
        private void cargarMenuComoSubMenu(string etiqueta, string url)
        {
            lMenu.Text += "<ul>";
            lMenu.Text += "<li>";
            lMenu.Text += "<a href ='" + url + "'>";
            lMenu.Text += "<span class='menu-item'>" + etiqueta + "</span>";
            lMenu.Text += "</a>";
            lMenu.Text += "</li>";
            lMenu.Text += "</ul>";
        }

        private bool tieneSubMenus(int id)
        {
            DataTable dtM = cMnu.obtenerByIdPerfil(id);
            if (dtM.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}