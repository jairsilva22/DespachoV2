using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class Folios1 : System.Web.UI.Page
    {
        Folio folio;

        protected void Page_Load(object sender, EventArgs e)
        {
            folio = new Folio();
            try
            {
                if (!IsPostBack)
                {
                    mosrarFlios();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void mosrarFlios()
        {
            folio.idEmpresa = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            lvFolios.DataSource = folio.mostrarFolios();
            lvFolios.DataBind();
        }

        protected void btnMod_ItemCommand(object sender, CommandEventArgs e)
        {
            Response.Redirect("FoliosMod.aspx?id=" + e.CommandArgument);
        }

        public string mostrar(bool activo)
        {
            string pram = "";
            if (activo)
            {
                pram = "<i class='icon-checkmark' style='color: #32AB52'></i>";
            }
            else
            {
                pram = "<i class='icon-cancel' style='color: gray'></i>";
            }
            return pram;
        }
    }
}