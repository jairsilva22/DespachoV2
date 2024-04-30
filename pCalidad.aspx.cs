using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class pCalidad : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUTransporte cUT = new cUTransporte();
        cCalidad cCal = new cCalidad();
        cOrdenesDosificacion cOD = new cOrdenesDosificacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                //lblOD.Text = Request.QueryString["idOD"];
                if (!IsPostBack)
                {
                    cleanData();
                    llenarLV();
                    llenarFormulario();
                }

            }
            catch (Exception)
            {

            }
        }

        protected void txtEdadEnsaye_TextChanged(object sender, EventArgs e)
        {
            // Verifica si txtFechaColado tiene una fecha válida
            DateTime fechaColado;
            if (DateTime.TryParse(txtFechaColado.Text, out fechaColado))
            {
                // Verifica si txtEdadEnsaye tiene un número válido
                int edadEnsaye;
                if (int.TryParse(txtEdadEnsaye.Text, out edadEnsaye))
                {
                    // Calcula la fecha de ensaye sumando la edad de ensaye a la fecha de colado
                    DateTime fechaEnsaye = fechaColado.AddDays(edadEnsaye);
                    // Actualiza el valor de txtFechaEnsaye con la nueva fecha calculada
                    txtFechaEnsaye.Text = fechaEnsaye.ToString("yyyy/MM/dd");
                }
            }
        }
        //llenar fórmulas
        protected void llenarFormulario()
        {
            int numCilindro = cCal.obtenerNumCilindroMasGrandeByIdOD(int.Parse(Request.QueryString["idOD"]));
            txtCilindro.Text = (numCilindro + 1).ToString();
            txtAreaCM.Text = (3.1415926535 * (7.5 * 7.5)).ToString("0.00");
            string[] codigo = cCal.obtenerCodigoByIdOD(int.Parse(Request.QueryString["idOD"])).Split('-');
            txtTipoConcreto.Text = codigo[codigo.Length - 1];
            string elemento = cCal.obtenerElementoByIdOD(int.Parse(Request.QueryString["idOD"]));
            txtElemento.Text = elemento;
        }
        protected void llenarLV()
        {
            listView.DataSource = cCal.obtenerViewByIdOD(int.Parse(Request.QueryString["idOD"]));
            string a = "hola";
            listView.DataBind();
        }
        protected void getDataFromOD()
        {
            listView.DataSource = cCal.obtenerViewByIdOD(int.Parse(Request.QueryString["idOD"]));
            listView.DataBind();
        }

        private void addMod()
        {
            try
            {



                cCal.idOD = int.Parse(Request.QueryString["idOD"]);
                cCal.numCilindro = int.Parse(txtCilindro.Text);
                cCal.fechaColado = DateTime.Parse(txtFechaColado.Text);
                cCal.edadEnsaye = int.Parse(txtEdadEnsaye.Text);
                cCal.fechaEnsaye = DateTime.Parse(txtFechaEnsaye.Text);
                cCal.resistenciaKG = int.Parse(txtResistencia.Text);
                cCal.cargaKG = int.Parse(txtCargaKG.Text);
                cCal.areaCM = txtAreaCM.Text;
                cCal.esfuerzoKG = txtEsfuerzoKg.Text;
                cCal.resistenciaP = txtResistenciaP.Text;
                cCal.tamMax = int.Parse(txtTamMax.Text);
                cCal.revCM = int.Parse(txtRevCM.Text);
                cCal.tipoConcreto = txtTipoConcreto.Text;
                cCal.tempAmb = int.Parse(txtTempAmb.Text);
                cCal.tempConc = int.Parse(txtTempConc.Text);
                cCal.aditivo = txtAditivo.Text;
                cCal.elemento = txtElemento.Text;


                if (cCal.existe())
                {
                    cCal.id = int.Parse(hfIdCalidad.Value);
                    cCal.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }
                else
                {
                    cCal.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                }


                llenarLV();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                hfIdCalidad.Value = e.CommandArgument.ToString();

                if (e.CommandName.Equals("update"))
                {
                    cCal.id = int.Parse(hfIdCalidad.Value);
                    cCal.obtenerByID();
                    txtCilindro.Text = cCal.numCilindro.ToString();
                    txtFechaColado.Text = cCal.fechaColado.ToString().Substring(0, 10);
                    txtEdadEnsaye.Text = cCal.edadEnsaye.ToString();
                    txtFechaEnsaye.Text = cCal.fechaEnsaye.ToString().Substring(0, 10);
                    txtResistencia.Text = cCal.resistenciaKG.ToString();
                    txtCargaKG.Text = cCal.cargaKG.ToString();
                    txtAreaCM.Text = cCal.areaCM.ToString();
                    txtEsfuerzoKg.Text = cCal.esfuerzoKG.ToString();
                    txtResistenciaP.Text = cCal.resistenciaP.ToString();
                    txtTamMax.Text = cCal.tamMax.ToString();
                    txtRevCM.Text = cCal.revCM.ToString();
                    txtTipoConcreto.Text = cCal.tipoConcreto.ToString();
                    txtTempAmb.Text = cCal.tempAmb.ToString();
                    txtTempConc.Text = cCal.tempConc.ToString();
                    txtAditivo.Text = cCal.aditivo.ToString();
                    txtElemento.Text = cCal.elemento.ToString();
                   // hfIdCalidad.Value = cCal.id.ToString();
                    btnAgregar.Visible = false;
                    btnModificar.Visible = true;
                }
                if (e.CommandName.Equals("delete"))
                {
                    cCal.eliminar(int.Parse(hfIdCalidad.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                    llenarLV();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            addMod();
            cleanData();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cCal.id = int.Parse(hfIdCalidad.Value);
            //cCal.idOD = int.Parse(Request.QueryString["idOD"]);
            //cCal.numCilindro = int.Parse(txtCilindro.Text);
            //cCal.fechaColado = DateTime.Parse(txtFechaColado.Text);
            //cCal.edadEnsaye = int.Parse(txtEdadEnsaye.Text);
            //cCal.fechaEnsaye = DateTime.Parse(txtFechaEnsaye.Text);
            //cCal.resistenciaKG = int.Parse(txtResistencia.Text);
            //cCal.cargaKG = int.Parse(txtCargaKG.Text);
            //cCal.areaCM = txtAreaCM.Text;
            //cCal.esfuerzoKG = txtEsfuerzoKg.Text;
            //cCal.resistenciaP = txtResistenciaP.Text;
            //cCal.tamMax = int.Parse(txtTamMax.Text);
            //cCal.revCM = int.Parse(txtRevCM.Text);
            //cCal.tipoConcreto = txtTipoConcreto.Text;
            //cCal.tempAmb = int.Parse(txtTempAmb.Text);
            //cCal.tempConc = int.Parse(txtTempConc.Text);
            //cCal.aditivo = txtAditivo.Text;
            //cCal.elemento = txtElemento.Text;

            //cCal.actualizar(int.Parse(Request.Cookies["ksroc"]["id"]));
            //llenarLV();

            //cleanData();.
            addMod();
        }
        private void cleanData()
        {
            txtCilindro.Text = "0";
            txtFechaColado.Text = "";
            txtEdadEnsaye.Text = "0";
            txtFechaEnsaye.Text = "";
            txtResistencia.Text = "0";
            txtCargaKG.Text = "0";
            txtAreaCM.Text = "0";
            txtEsfuerzoKg.Text = "0";
            txtResistenciaP.Text = "0";
            txtTamMax.Text = "0";
            txtRevCM.Text = "0";
            txtTipoConcreto.Text = "";
            txtTempAmb.Text = "0";
            txtTempConc.Text = "0";
            txtAditivo.Text = "";
            txtElemento.Text = "";

            btnAgregar.Visible = true;
            btnModificar.Visible = false;
        }

        protected void listView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void listView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }
        protected void txtCargaKG_TextChanged(object sender, EventArgs e)
        {
            decimal carga = decimal.Parse(txtCargaKG.Text);
            decimal area = decimal.Parse(txtAreaCM.Text);
            decimal resistencia = decimal.Parse(txtResistencia.Text);
            if (area != 0)
            {
                decimal esfuerzo = carga / area;
                txtEsfuerzoKg.Text = esfuerzo.ToString("0.00");
                txtResistenciaP.Text = ((esfuerzo / resistencia) * 100).ToString("0.00");
            }

        }
        protected void txtResistencia_TextChanged(object sender, EventArgs e)
        {
            decimal resistencia;
            decimal esfuerzo;
            // Intenta convertir txtResistencia y txtEsfuerzoKg a valores decimales
            if (decimal.TryParse(txtResistencia.Text, out resistencia) && decimal.TryParse(txtEsfuerzoKg.Text, out esfuerzo))
            {
                if (esfuerzo != 0)
                {
                    // Calcula la resistencia y establece el valor en txtResistenciaP
                    txtResistenciaP.Text = ((esfuerzo / resistencia) * 100).ToString("0.00");
                }
                else
                {
                    // Si esfuerzo es igual a 0, no coloques nada en txtResistenciaP
                    txtResistenciaP.Text = string.Empty;
                }
            }
        }
        protected void txtAreaCM_TextChanged(object sender, EventArgs e)
        {
            decimal resistencia;
            decimal carga;
            decimal area;
            // Intenta convertir txtResistencia y txtEsfuerzoKg a valores decimales
            if (decimal.TryParse(txtResistencia.Text, out resistencia) && decimal.TryParse(txtCargaKG.Text, out carga) && decimal.TryParse(txtAreaCM.Text, out area))
            {
                if (area != 0)
                {
                    //Calcula el esfuerzo
                    decimal esfuerzo = (carga / area);
                    txtEsfuerzoKg.Text = esfuerzo.ToString("0.00");
                    // Calcula la resistencia y establece el valor en txtResistenciaP
                    txtResistenciaP.Text = ((esfuerzo / resistencia) * 100).ToString("0.00");
                }
                else
                {
                    // Si esfuerzo es igual a 0, no coloques nada en txtResistenciaP
                    txtResistenciaP.Text = string.Empty;
                    txtEsfuerzoKg.Text = string.Empty;
                }
            }
        }
    }
}