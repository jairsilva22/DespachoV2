using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class pBitacoraInsidenciasOD : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUTransporte cUT = new cUTransporte();
        cBitacora cBit = new cBitacora();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOD.Text = Request.QueryString["idOD"];
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                llenarLV();
            }
            catch (Exception)
            {

            }
        }
        protected void llenarLV()
        {
            lvUT.DataSource = cBit.obtenerBitacoraDT("ordenDosificacionIncidencias", int.Parse(Request.QueryString["idOD"]));
            lvUT.DataBind();
        }

        protected void lBtnAsignar_Command(object sender, CommandEventArgs e)
        {
            cBit.obtenerBitacora("ordenDosificacionIncidenciasBitacora", int.Parse(Request.QueryString["idOD"]));
            hfIdBitacora.Value = cBit.id.ToString();
            txtAccion.Text = cBit.accion;
            txtMotivo.Text = cBit.motivo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            cBit.idMaster = int.Parse(Request.QueryString["idOD"]);
            cBit.insertar(cBit.idMaster, txtAccion.Text, int.Parse(Request.Cookies["ksroc"]["id"]), "ordenDosificacionIncidencias", txtMotivo.Text);

            llenarLV();
        }
    }
}