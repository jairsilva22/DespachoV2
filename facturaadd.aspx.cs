using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class facturaadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    frame.Src = "facturaadd.asp?idEmpresa=" + Request.Cookies["ksroc"]["idSucursal"];
                }
            }
            catch (Exception)
            {

            }
        }
    }
}