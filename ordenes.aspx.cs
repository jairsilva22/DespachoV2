using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class ordenes : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cOrdenes cOrd = new cOrdenes();
        cSolicitudes cSol = new cSolicitudes();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    txtFechaI.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtFechaF.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                    llenarGrid(false);
                }
            }
            catch (Exception)
            {

            }
        }

        protected void llenarGrid(bool bProg)
        {
            cOrd.idSucursal = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);
            cOrd.programada = bProg;
            listView.DataSource = cOrd.obtenerOrdenes(txtFechaI.Text, txtFechaF.Text);
            listView.DataBind();
        }

        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                lblID.Text = arr[0];
                lblFolio.Text = arr[1];
                lblIDS.Text = arr[2];
                lblAux.Text = "";
                //if (cOrd.esProgramada(int.Parse(lblID.Text)))
                if (cOrd.esOrdenSinViajesDosificados(int.Parse(lblID.Text)))
                {
                    lblAux.Text = "";
                    this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                    this.mlblMessage.Text = "La orden con folio: " + arr[1] + " ya ha generado ordenes de entrega en proceso, favor de realizar la cancelación primero de las entregas para poder realizar la acción";
                }
                else
                {
                    if (e.CommandName.Equals("solicitud"))
                    {
                        lblAux.Text = "1";
                        this.mlblMessage.Text = "¿Estás seguro que deseas pasar la orden con folio: " + arr[1] + " a solicitud?";
                    }
                    if (e.CommandName.Equals("delete"))
                    {
                        lblAux.Text = "2";
                        this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la orden con folio: " + arr[1] + " del sistema?";
                    }
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void listView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        protected void chbxProgramadas_CheckedChanged(object sender, EventArgs e)
        {
            fiiGrid();
        }

        private void fiiGrid()
        {
            if (chbxProgramadas.Checked)
                llenarGrid(true);
            else
                llenarGrid(false);
        }
        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            if (lblAux.Text.Equals("1"))
            {
                cSol.id = int.Parse(lblIDS.Text);
                cSol.cambiarASolicitud(int.Parse(Request.Cookies["ksroc"]["id"]));
                cOrd.eliminar(int.Parse(lblID.Text), int.Parse(Request.Cookies["ksroc"]["id"]));
            }
            else if (lblAux.Text.Equals("2"))
            {
                cOrd.eliminar(int.Parse(lblID.Text), int.Parse(Request.Cookies["ksroc"]["id"]));
                cOrd.eliminarOdsByIdOrden(int.Parse(lblID.Text), int.Parse(Request.Cookies["ksroc"]["id"]));

                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cUtl.idSucursalActiva = int.Parse(Request.Cookies["ksroc"]["idSucursal"]);

                //Generar alerta random
                cUtl.motivo = "Se ha eliminado una Orden desde Despacho ¿Deseas actualizar?";
                cUtl.generarAlerta();
            }
            lblAux.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
            fiiGrid();
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            lblAux.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            fiiGrid();
        }
    }
}