using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class filtroBitacoraDosificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.open('reporteBitacoraDosificacion.aspx?idSucursal=" + Request.Cookies["ksroc"]["idSucursal"] + "&fechaIni=" + txtFechaInicio.Text + "&fechaFin=" + txtFechaFin.Text + "' ,'_blank');</script>");
        }

        protected void btnEnviarExcel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.open('reporteBitacoraDosificacion.aspx?idSucursal=" + Request.Cookies["ksroc"]["idSucursal"] + "&fechaIni=" + txtFechaInicio.Text + "&fechaFin=" + txtFechaFin.Text + "&Excel=1' ,'_blank');</script>");
        }
    }
}