using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace despacho
{
    public partial class testPDF : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        //cRemisionPDF cRem = new cRemisionPDF();
        string Documento;
        string Ruta = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void verPDF(int id)
        {
            //cRem.nombreDoc = "Remision";
            //cRem.extencion = ".pdf";
            //cRem.path = Server.MapPath(".");
            //cRem.idOD = id;
            //cRem.sObservaciones = txtObservaciones.Text;
            //lblMensaje.Text = cRem.generarPdf();
            //Documento = cRem.nombreDoc + cRem.folio.ToString() + cRem.extencion;

            //hfFolio.Value = cRem.folio.ToString();
            //hfDocumento.Value = Documento;

            //Ruta = Server.MapPath(@"Remisiones");
            //hfRuta.Value = Ruta + "\\";
            ////lblMensaje.Text += "Cotizacion/" + Documento;
            //string htm = "<iframe src ='Remisiones/" + Documento + "' width='100%' height='600px' ></iframe>";
            //lPDF.Text = htm;
        }
            protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            verPDF(3051);
        }
    }
}