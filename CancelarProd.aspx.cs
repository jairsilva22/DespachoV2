using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class CancelarProd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    frame.Src = "CancelarProd.asp?folio=" + Request.QueryString["folio"] + "&tipo=" + Request.QueryString["tipo"];
                }
            }
            catch (Exception)
            {

            }
        }
    }
}