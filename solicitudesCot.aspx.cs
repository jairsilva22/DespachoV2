using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class solicitudesCot : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cCotizacionPDF cCot = new cCotizacionPDF();
        cSolicitudes cSol = new cSolicitudes();

        string Documento;
        string Ruta ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);

            }
            catch (Exception)
            {

            }
        }

        private void verPDF(int id)
        {
            cCot.nombreDoc = "Cotizacion_";
            cCot.extencion = ".pdf";
            cCot.path = Server.MapPath(".");
            cCot.idSolicitud = id;
            cCot.sObservaciones = txtObservaciones.Text;
            lblMensaje.Text = cCot.generarPdf();
            Documento = cCot.nombreDoc + cCot.folio.ToString() + cCot.extencion;

            hfFolio.Value = cCot.folio.ToString();
            hfDocumento.Value = Documento;

            Ruta = Server.MapPath(@"Cotizacion");
            hfRuta.Value = Ruta + "\\";
            //lblMensaje.Text += "Cotizacion/" + Documento;
            string htm = "<iframe src ='Cotizacion/" + Documento + "' width='100%' height='600px' ></iframe>";
            lPDF.Text = htm.ToString();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = cSol.obtenerDTByID(int.Parse(Request.QueryString["id"]));
                string filename = hfRuta.Value + hfDocumento.Value;
                Attachment data = new Attachment(filename, MediaTypeNames.Application.Octet);

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                // utilizamos el servidor SMTP de gmail
                client.Host = "mail.concretospepi.com";
                client.Port = 26;
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                // nos autenticamos con nuestra cuenta de gmail
                client.Credentials = new NetworkCredential("avisos@concretospepi.com", "Admin*2021");

                MailMessage mail = new MailMessage("avisos@concretospepi.com", dt.Rows[0]["email"].ToString(), "Cotización", "Estimado cliente, se envía la cotización de su solicitud. Saludos");
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.Attachments.Add(data);
                client.Send(mail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            verPDF(int.Parse(Request.QueryString["id"]));
            btnEnviar.Visible = true;
        }
    }
}