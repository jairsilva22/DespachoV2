using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class pAsignarUnidad : System.Web.UI.Page
    {
        cUTransporte cUT = new cUTransporte();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        cUtilidades cUtl = new cUtilidades();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                lblOD.Text = Request.QueryString["idOD"];

                txtIdOD.Text = Request.QueryString["idOD"];
                llenarLVUT();
            }
            catch (Exception)
            {

            }
        }
        protected void llenarLVUT()
        {
            lvUT.DataSource = cUT.obtenerUDByIDOD(int.Parse(Request.QueryString["idOD"]));
            lvUT.DataBind();
        }

        protected void lBtnAsignar_Command(object sender, CommandEventArgs e)
        {
            asignarU(int.Parse(txtIdOD.Text), int.Parse(e.CommandArgument.ToString()));
        }

        private bool asignarU(int idOD, int idU)
        {
            int idUA = int.Parse(Request.Cookies["ksroc"]["id"]);
            try
            {
                cUT.idEstadoUnidad = 2;
                cUT.actualizarEstado(idU, idUA);
                cOD.idUnidadTransporte = idU;
                cOD.asignarUnidadTransporte(idOD, idUA);
                cOD.idEstadoDosificacion = 4;
                cOD.actualizarEstadoDosificacion(idOD, idUA);
                llenarLVUT();
                lblMensaje.Text = "Se asignó la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return true;
            }
            catch (Exception)
            {
                lblMensaje.Text = "No se pudo asignar la unidad Correctamente, favor de cerrar ésta ventana y actualizar Programación";
                return false;
                throw;
            }
        }
    }
}