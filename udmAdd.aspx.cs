﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class udmAdd : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cUDM cUdm = new cUDM();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                }
            }
            catch (Exception)
            {

            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> getDataUDM(string prefixText, int count, string contextKey)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT unidad FROM unidadesDeMedida WHERE LOWER(unidad) LIKE '%" + prefixText + "%' OR UPPER(unidad) LIKE '%" + prefixText + "%'", conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            List<String> list = new List<String>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr["unidad"].ToString());// + "-" + dr["nombre"].ToString()) ;
                            }
                            return list;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void limpiar()
        {
            txtDescripcion.Text = "";
            txtUnidad.Text = "";
            txtUnidadSAT.Text = "";
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {


                if (txtUnidad.Text.Equals(""))
                {
                    lblError.Text = "Favor de ingresar el nombre de la unidad de medida";
                    return;
                }
                cUdm.unidad = txtUnidad.Text;
                cUdm.descripcion = txtDescripcion.Text;
                cUdm.unidadSAT = txtUnidadSAT.Text;

                if (cUdm.existe(cUdm.unidad, cUdm.descripcion, cUdm.unidadSAT))
                {
                    lblError.Text = "Ya existe la Unidad, favor de revisar";
                }
                else
                {
                    cUdm.insertar(int.Parse(Request.Cookies["ksroc"]["id"]));
                    Response.Redirect("udm.aspx");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "ok")
                {

                    //script que te abre el modal 
                    string errorMessage = "Unidad agregada correctamente ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                    limpiar();

                }
                else
                {
                    string errorMessage = "Verifica que la informacón ingresada sea correcta ejemplo Unidad: Kg , Descripcion : Kilogramos , UnidadSat : KGM  ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showErrorModal", $"$('#errorMessage').text('{errorMessage}'); $('#errorModal').modal('show');", true);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("udm.aspx");
        }

        protected void txtUnidad_TextChanged(object sender, EventArgs e)
        {
            cUdm.unidad = txtUnidad.Text;
            DataTable dt = cUdm.obtenerUnidadByUnidad(cUdm.unidad);
            if (dt.Rows.Count > 0)
            {
                lblError.Text = "Ya existe la Unidad, se llenarán los campos con la información de la unidad... Favor de revisar si es otra la que se desea agregar";
                txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();
                txtUnidadSAT.Text = dt.Rows[0]["unidadSAT"].ToString();
            }
        }
    }
}
