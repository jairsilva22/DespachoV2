using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class reportesat : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cClaves cve;
        string año, año2;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                cve = new cClaves();
                año = Request.QueryString["year"];
                if (!IsPostBack)
                {
                    if (año != "")
                    {
                        año2 = " Del año " + año;
                    }
                    lblYear.Text = año2;
                    mostrarClavesSAT();
                }
            }
            catch (Exception)
            {

            }
        }

        private void mostrarClavesSAT()
        {
            string param = "";
            if (año != "")
            {
                param = " AND YEAR(detFactura.fecha_alta) = " + año;
            }
            lvCveSAT.DataSource = cve.claves(param);
            lvCveSAT.DataBind();
        }

        public string cvesSat(string cves)
        {
            string param = "";
            string param2 = "";
            cve.clave = cves;
            int cont = 0;
            if (año != "")
            {
                param2 = " AND YEAR(detFactura.fecha_alta) = " + año;
            }
            DataTable tabCve = cve.descripcionCves(param2);
            for (int i = 0; i < tabCve.Rows.Count; i++)
            {
                cont = cont + 1;
                param += tabCve.Rows[i]["descripcion"].ToString() + "<br>";
            }
            return param;
        }

        public string cveUnidad(string cves)
        {
            string param = "";
            string param2 = "";
            if (año != "")
            {
                param2 = " AND YEAR(detFactura.fecha_alta) = " + año;
            }
            cve.clave = cves;
            int cont = 0;
            DataTable tabCve = cve.descripcionCvesU(param2);
            for (int i = 0; i < tabCve.Rows.Count; i++)
            {
                cont = cont + 1;
                param += tabCve.Rows[i]["claveUnidad"].ToString() + "<br>";
            }
            return param;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            mostrarClavesSAT();
        }
    }
}