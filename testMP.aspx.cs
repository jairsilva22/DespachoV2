using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class testMP : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.mlblTitle.Text = "Your Title";
            this.mlblMessage.Text = "Your Registration is done successfully. Our team will contact you shotly";
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "$('#myModal').on('shown.bs.modal', function () { $('#myInput').focus() })", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnAlerta_Click(object sender, EventArgs e)
        {
            cUtl.idUsuarioActivo = 9451213;
            cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

            //Generar alerta random
            cUtl.motivo = DateTime.Now.ToString();
            cUtl.generarAlerta();
        }
    }
} 