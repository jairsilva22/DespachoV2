using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class detallesComExt : System.Web.UI.Page
    {
        Factura fact;
        DetFactura detalles;
        double suma = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            detalles = new DetFactura();
            fact = new Factura();

            if (!IsPostBack)
            {
                detalles.id_factura = long.Parse(Request.QueryString["id"]);
                llenarGrid();

                //obtenemos el total de la factura
                fact.idfactura = long.Parse(Request.QueryString["id"]);
                fact.camposComercioExterior();

                hdfTotal.Value = fact.total.ToString();

                //obtenemos la suma del total de los detalles
                detalles.id_factura = fact.idfactura;


                totalMerc.Text = Math.Round(detalles.sumaValorUSD(), 2).ToString();

            }
        }

        //metodo para llenar el gridView
        internal void llenarGrid()
        {
            ListView1.DataSource = detalles.detallesFactura();
            ListView1.DataBind();

            //mostramos los datos
            TextBox text;
            DropDownList ddl;
            Label lbl;
            UnidadesAduana unidadA = new UnidadesAduana();

            //recorremos el listview
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                //obtenemos los datos de los textbox
                lbl = (Label)ListView1.Items[i].FindControl("lblId");
                detalles.id_detfactura = long.Parse(lbl.Text);

                //buscamos los datos de comercio exterior
                detalles.camposComercioExterior();

                text = (TextBox)ListView1.Items[i].FindControl("txtnoIdentificacion");
                text.Text = detalles.noIdentificacion;
                if (detalles.cantidadAduana != "")
                {
                    text = (TextBox)ListView1.Items[i].FindControl("txtCantidad");
                    text.Text = detalles.cantidadAduana.ToString();
                }
                ddl = (DropDownList)ListView1.Items[i].FindControl("ddlUnidad");
                ddl.DataSource = unidadA.unidadesComExt();
                ddl.DataValueField = "codigo";
                ddl.DataTextField = "descripcion";
                ddl.DataBind();
                ddl.SelectedValue = detalles.unidadAduana;
                if (detalles.valorUnitarioAduana != "")
                {
                    text = (TextBox)ListView1.Items[i].FindControl("txtValorUnitario");
                    text.Text = detalles.valorUnitarioAduana.ToString();
                }
                text = (TextBox)ListView1.Items[i].FindControl("txtDls");
                text.Text = detalles.valorDolares.ToString();
                text = (TextBox)ListView1.Items[i].FindControl("txtFracion");
                text.Text = detalles.fraccionArancelaria.ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            TextBox text;
            DropDownList ddl;
            Label lbl;
            int contador = 0;

            //recorremos el listview
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                //obtenemos los datos de los textbox
                lbl = (Label)ListView1.Items[i].FindControl("lblId");
                detalles.id_detfactura = long.Parse(lbl.Text);

                text = (TextBox)ListView1.Items[i].FindControl("txtnoIdentificacion");
                //validamos que no estén vacíos los campos
                if (text.Text != "")
                {
                    detalles.noIdentificacion = text.Text;

                    text = (TextBox)ListView1.Items[i].FindControl("txtCantidad");
                    if (text.Text != "")
                    {
                        detalles.cantidadAduana = (text.Text);
                        ddl = (DropDownList)ListView1.Items[i].FindControl("ddlUnidad");

                        if (ddl.SelectedValue != "")
                        {
                            detalles.unidadAduana = ddl.SelectedValue;
                            text = (TextBox)ListView1.Items[i].FindControl("txtValorUnitario");

                            if (text.Text != "")
                            {
                                detalles.valorUnitarioAduana = (text.Text);
                                text = (TextBox)ListView1.Items[i].FindControl("txtDls");

                                if (text.Text != "")
                                {
                                    detalles.valorDolares = (text.Text);
                                    suma += double.Parse(detalles.valorDolares);
                                    text = (TextBox)ListView1.Items[i].FindControl("txtFracion");

                                    if (text.Text != "")
                                    {
                                        detalles.fraccionArancelaria = text.Text;

                                        //realizamos el update de los datos de comercio exterior
                                        detalles.modificarComExt();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                                        contador++;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                                    contador++;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                                contador++;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                            contador++;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                        contador++;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "1", "validar();", true);
                    contador++;
                }

                if (i == ListView1.Items.Count - 1 && contador == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "2", "alert('Datos Guardados Correctamente');", true);

                    //obtenemos la suma del total de los detalles
                    detalles.id_factura = long.Parse(Request.QueryString["id"]);


                    totalMerc.Text = Math.Round(detalles.sumaValorUSD(), 2).ToString();

                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //validamos si la suma de los datos es igual a la de la factura
            if (hdfTotal.Value == totalMerc.Text)
            {
                //modificamos el estatus de la factura
                fact.idfactura = long.Parse(Request.QueryString["id"]);
                fact.estatus = "FormaPago";
                fact.modificarEstatus();

                //redirigimos a la siguiente pagina
                ScriptManager.RegisterStartupScript(Page, GetType(), "2", "salir();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "3", "alert('La suma del total del valor en USD debe ser igual al total de la factura');", true);
            }
            //Response.Redirect("folios.asp?idfactura=" + Request.QueryString["id"]);
        }

    }
    
}