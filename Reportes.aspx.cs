using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class Reportes : System.Web.UI.Page
    {
        cMenu cMnu = new cMenu();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                buscarReportes();
            }
        }

        private void buscarReportes()
        {
            cMnu.idPerfil = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cMnu.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dtTipo = cMnu.obtenerTiposReportes(-1);
            lvMenu.DataSource = dtTipo;
            lvMenu.DataBind();

        }

        public string buscarMenusTipo(string tipo)
        {
            string param = "";
            cMnu.idPerfil = int.Parse(Request.Cookies["ksroc"]["idPerfil"]);
            cMnu.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            DataTable dtM = cMnu.obtenerMenuReportesByTipo(-1, tipo);

            if(dtM.Rows.Count > 0)
            {
                for(int i = 0; i < dtM.Rows.Count; i++)
                {
                    param += "<div class='col-md-6'>";
                    param += "<a href='"+ dtM.Rows[i]["url"].ToString() + "'><img src='imagenes/flecha-correcta.png' />"+dtM.Rows[i]["etiqueta"].ToString()+"</a>";
                    param += "</div>";
                }
            }
            return param;
        }
    }
}