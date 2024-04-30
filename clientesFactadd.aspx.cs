using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class clientesFactadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                frame.Src = "clientesadd.asp?idEmpresa="+Request.Cookies["login"]["idSucursal"];
            }
        }
    }
}