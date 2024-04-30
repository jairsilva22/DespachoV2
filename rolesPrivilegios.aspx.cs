using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class rolesPrivilegios : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cPerfiles cP = new cPerfiles();
        cMenu cMnu = new cMenu();
        cPrivilegios cPr = new cPrivilegios();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    cP.id = int.Parse(Request.QueryString["id"]);
                    cP.obtenerPerfilByID();
                    lblPerfil.Text = cP.descripcion;
                    llenarGrid();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void llenarGrid()
        {
            cPr.idPerfil = int.Parse(Request.QueryString["id"]);
            cPr.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dt = cPr.obtenerMenuWithPrivilegios();
            if (dt.Rows.Count < cMnu.countMenu())
            { 
                cPr.crearPrivilegios(int.Parse(Request.Cookies["ksroc"]["id"]));
                dt = cPr.obtenerMenuWithPrivilegios();
            }
            listView.DataSource = dt;
            listView.DataBind();
        }
        protected void chbxPrivilegio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = (CheckBox)sender;
                string[] arr;
                arr = chk.Attributes["CommandName"].ToString().Split('ˇ');
                cPr.id = int.Parse(arr[0].ToString());
                cPr.setActivo(chk.Checked, int.Parse(Request.Cookies["ksroc"]["id"]));
                upForm.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void chbxTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = sender as CheckBox;
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    CheckBox chkS = listView.Items[i].FindControl("chbxPrivilegio") as CheckBox;
                    Label idPr = listView.Items[i].FindControl("lblID") as Label;
                    chkS.Checked = chkHeader.Checked;
                    cPr.id = int.Parse(idPr.Text);
                    cPr.setActivo(chkS.Checked, int.Parse(Request.Cookies["ksroc"]["id"]));
                }
                upForm.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}