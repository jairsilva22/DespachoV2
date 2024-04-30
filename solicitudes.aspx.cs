using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class solicitudes : System.Web.UI.Page
    {
        cUtilidades cUtl = new cUtilidades();
        cSolicitudes cSol = new cSolicitudes();
        cDetallesSolicitud cDS = new cDetallesSolicitud();
        cCotizacionPDF cCot = new cCotizacionPDF();
        string Documento;
        string Ruta = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cUtl.idUsuarioActivo = int.Parse(Request.Cookies["ksroc"]["id"]);
                if (!IsPostBack)
                {
                    llenarGrid();
                }
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
        protected void llenarGrid()
        {
            listView.DataSource = cSol.obtenerSolicitudes(int.Parse(Request.Cookies["ksroc"]["idSucursal"]));
            listView.DataBind();
        }

        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string[] arr;
                arr = e.CommandArgument.ToString().Split('ˇ');
                hfId.Value = arr[0];
                if (e.CommandName.Equals("Cot"))
                {
                    cDS.idSolicitud = int.Parse(arr[0].ToString());
                    if (!cDS.existenDetalles())
                    {
                        this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                        this.mlblMessage.Text = "No es posible generar la cotización si no se han dado de alta productos";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                    }
                    else
                    {
                        btnEnviarCot.Visible = false;
                        mlblPara.Visible = false;
                        mtxtPara.Visible = false;
                        mlblCopia.Visible = false;
                        mtxtCopia.Visible = false;
                        mtxtPara.Text = arr[2];
                        mtxtCopia.Text = "";
                        lPDF.Text = "";
                        lblMensaje.Text = "";
                        mbtnCloseCot.Visible = false;

                        verPDF(int.Parse(hfId.Value));
                        btnEnviarCot.Visible = true;
                        mlblPara.Visible = true;
                        mtxtPara.Visible = true;
                        mlblCopia.Visible = true;
                        mtxtCopia.Visible = true;

                        this.mlblTitleCot.Text = "COTIZACIÓN PARA SOLICITUD: " + arr[1];
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCot();", true);
                    }
                }
                if (e.CommandName.Equals("ValSol"))
                {
                    cDS.idSolicitud = int.Parse(e.CommandArgument.ToString());
                    if (cDS.existenDetalles())
                        Response.Redirect("validarSolicitud.aspx?id=" + e.CommandArgument.ToString());
                    else
                    {
                        this.mlblTitle.Text = "¡¡¡ATENCIÓN!!!";
                        this.mlblMessage.Text = "La solicitud no cuenta con productos, no es posible aprobarla hasta que se den de alta productos";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                    }
                }
                if (e.CommandName.Equals("delete"))
                {
                    hfDelete.Value = "1";
                    this.mlblTitle.Text = "¡¡¡CONFIRMACIÓN!!!";
                    this.mlblMessage.Text = "¿Estás seguro que deseas eliminar la solicitud con folio: " + arr[1] + " del sistema?";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void listView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
        }

        protected void lbtnCotizacion_Command(object sender, CommandEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PopUP", "openCot(" + e.CommandArgument.ToString() + ")", true);
        }

        protected void mbtnAceptar_Click(object sender, EventArgs e)
        {
            if (!hfDelete.Value.Equals(""))
            { 
                cSol.eliminar(int.Parse(hfId.Value), int.Parse(Request.Cookies["ksroc"]["id"]));
                llenarGrid();
            }
            hfDelete.Value = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "redirectScript", "setTimeout(function() { window.location.href = 'solicitudes.aspx'; }, 1000);", true);
        }

        protected void mbtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopup();", true);
        }

        protected void mbtnAceptarCot_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCot();", true);
        }

        protected void mbtnCloseCot_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCot();", true);
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            //verPDF(int.Parse(hfId.Value));
            //btnEnviarCot.Visible = true;
            //mlblPara.Visible = true;
            //mtxtPara.Visible = true;
            //mlblCopia.Visible = true;
            //mtxtCopia.Visible = true;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCot();", true);
        }

        protected void btnEnviarCot_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = cSol.obtenerDTByID(int.Parse(lblIdSol.Text));
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
                mail.CC.Add(mtxtCopia.Text);
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.Attachments.Add(data);
                client.Send(mail);

                lblMensaje.Text = "Se envió la cotización satisfactoriamente al correo del cliente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCot();", true);
            }
            catch (Exception)
            {
                lblMensaje.Text = "Error durante el envió la cotización al correo del cliente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopupCot();", true);
            }
        }

        protected void lbtnCloseModalCot_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ClosePopupCot();", true);
        }
    }
}