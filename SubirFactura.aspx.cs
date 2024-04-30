using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace despacho
{
    public partial class SubirFactura : System.Web.UI.Page
    {
        Cliente clnt;
        Configuracion conf;
        Empresa emp;
        Factura fact;
        DetFactura det;
        Log_IdCo co;

        protected void Page_Load(object sender, EventArgs e)
        {
            clnt = new Cliente();
            conf = new Configuracion();
            emp = new Empresa();
            fact = new Factura();
            det = new DetFactura();
            co = new Log_IdCo();
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string nodo = string.Empty;
            string uid = null;

            //validamos si hay archivo en el file
            if (fuArchivo.HasFile)
            {
                //validamos si el archivo es xml
                FileInfo dato = new FileInfo(fuArchivo.FileName);
                if (dato.Extension.ToLower() == ".xml")
                {
                    //buscamos la carpeta en donde se va a guardar el archivo
                    conf.idempresa = int.Parse(Request.QueryString["idempresa"]);
                    conf.configProcesos();

                    //buscamos los datos de la empresa
                    emp.idEmpresa = conf.idempresa;
                    emp.empresaProcesos();

                    //subimos el archivo
                    fuArchivo.SaveAs(conf.path + emp.carpetaTimbre + @"\" + fuArchivo.FileName);

                    //leemos el archivo xml para obtener los datos de la factura
                    using (XmlTextReader reader = new XmlTextReader(conf.path + emp.carpetaTimbre + @"\" + fuArchivo.FileName))
                    {
                        while (reader.Read())
                        {
                            reader.MoveToContent();

                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                nodo = reader.Name;

                                //validamos si está en el nodo de comprobante
                                if (nodo == "cfdi:Comprobante")
                                {
                                    fact.total = float.Parse(reader.GetAttribute("Total"));
                                    fact.fechaAlta = DateTime.Parse(reader.GetAttribute("Fecha").Replace('T', ' '));
                                    fact.subtotal = float.Parse(reader.GetAttribute("SubTotal"));
                                    fact.numero_certificado = reader.GetAttribute("NoCertificado");
                                    fact.sello = reader.GetAttribute("Sello");
                                    fact.certificado = reader.GetAttribute("Certificado");
                                    fact.numero_certificado = reader.GetAttribute("NoCertificado");
                                    fact.tipo_comprobante = 1;
                                    fact.estadoComprobante = 1;
                                    fact.vendedor = "";
                                    fact.abreviatura = "FA";
                                    fact.metodoPago = reader.GetAttribute("FormaPago");
                                    fact.metodo_pago = "";
                                    fact.ordenCompra = "";
                                    fact.va = "0";
                                    fact.retencion = "0";
                                    fact.idusuario = 3;
                                    fact.idempresa = emp.idEmpresa;
                                    fact.moneda = fact.obtenerMoneda(reader.GetAttribute("Moneda"));
                                    fact.cambio = float.Parse(reader.GetAttribute("TipoCambio"));
                                    fact.cadenaOrig = cadenaOriginal(conf.path + emp.carpetaTimbre + @"\" + fuArchivo.FileName);

                                    if (reader.GetAttribute("Folio") == "" || reader.GetAttribute("Folio") == string.Empty || reader.GetAttribute("Folio") == null)
                                    {
                                        fact.folio = 0;
                                        fact.asn = "";
                                        fact.nfactura = "";
                                    }
                                    else
                                    {
                                        fact.folio = int.Parse(reader.GetAttribute("Folio"));
                                        fact.asn = reader.GetAttribute("Folio");
                                        fact.nfactura = reader.GetAttribute("Folio");
                                    }
                                    if (reader.GetAttribute("Serie") == "" || reader.GetAttribute("Serie") == string.Empty || reader.GetAttribute("Serie") == null)
                                    {
                                        Folios foil = new Folios();
                                        foil.idEmpresa = int.Parse(Request.QueryString["idempresa"]);
                                        foil.foliosProcesos("Factura");
                                        fact.serie = foil.serie;
                                    }
                                    else
                                    {
                                        fact.serie = reader.GetAttribute("Serie");
                                    }
                                    if (reader.GetAttribute("MetodoPago") == "PPD")
                                    {
                                        fact.forma_pago = "2";
                                        fact.condicionesDePago = "CREDITO";
                                        fact.terminos = "CREDITO";
                                    }
                                    else
                                    {
                                        fact.forma_pago = "1";
                                        fact.condicionesDePago = "CONTADO";
                                        fact.terminos = "CONTADO";
                                    }
                                }

                                //validamos si está en el nodo de receptor
                                if (nodo == "cfdi:Emisor")
                                {
                                    emp.rfc = reader.GetAttribute("Rfc");
                                }

                                //validamos si está en el nodo de receptor
                                if (nodo == "cfdi:Receptor")
                                {
                                    clnt.rfcCliente = reader.GetAttribute("Rfc");
                                    //clnt.idCliente = 198;
                                    clnt.obtenerID();

                                    fact.idcliente = int.Parse(clnt.idCliente.ToString());
                                    fact.obsCliente = clnt.obsCliente;
                                    fact.NumCtaPago = clnt.numeroCuenta;
                                    fact.embarque = clnt.entrega;
                                }

                                //validamos si está en el nodo de receptor
                                if (nodo == "tfd:TimbreFiscalDigital")
                                {
                                    fact.uuid = "?re=" + emp.rfc + "&rr=" + clnt.rfcCliente + "&tt=19757&id=" + reader.GetAttribute("UUID");
                                    fact.selloCFD = reader.GetAttribute("SelloCFD");
                                }

                                //validamos si está en ele nodo de impuestos
                                if (nodo == "cfdi:Impuestos")
                                {
                                    if (reader.GetAttribute("TotalImpuestosTrasladados") != "" && reader.GetAttribute("TotalImpuestosTrasladados") != string.Empty && reader.GetAttribute("TotalImpuestosTrasladados") != null)
                                    {
                                        fact.iva = float.Parse(reader.GetAttribute("TotalImpuestosTrasladados"));
                                        fact.tasa = "16";
                                    }

                                    if (reader.GetAttribute("TotalImpuestosRetenidos") != "" && reader.GetAttribute("TotalImpuestosRetenidos") != string.Empty && reader.GetAttribute("TotalImpuestosRetenidos") != null)
                                    {
                                        fact.retencion = "16";
                                        fact.tretencion = 0;
                                    }

                                }
                            }
                        }
                    }

                    //verificamos si la factura es para la empresa
                    if (emp.rfcEmpresa())
                    {
                        //luego validamos si la el rfc del cliente pertenece a volkswagen(solo KSR)
                        if (clnt.existeRFC())
                        {
                            //una vez terminado la lectura de los datos de la factura buscamos si ya existe en la tabla
                            uid = fact.uuid.Substring(fact.uuid.IndexOf("id=") + 3);
                            if (!fact.existeFactura(uid))
                            {
                                //se inserta la factura en la tabla y se obtiene el id generado
                                fact.insertarFacturaXML();
                                det.id_factura = fact.idfactura;
                                //Response.Write("Datos Insertados <br/>");

                                //luego leemos de nuevo el xml para obtener los detalles
                                using (XmlTextReader reader = new XmlTextReader(conf.path + emp.carpetaTimbre + @"\" + fuArchivo.FileName))
                                {
                                    while (reader.Read())
                                    {
                                        reader.MoveToContent();
                                        if (reader.NodeType == XmlNodeType.Element)
                                        {
                                            nodo = reader.Name;

                                            if (nodo == "cfdi:Concepto")
                                            {
                                                det.cantidad = float.Parse(reader.GetAttribute("Cantidad"));
                                                det.descripcion = reader.GetAttribute("Descripcion");
                                                det.precio_unitario = float.Parse(reader.GetAttribute("ValorUnitario"));
                                                det.total = float.Parse(reader.GetAttribute("Importe"));
                                                det.descuento = 0;
                                                det.unidad = reader.GetAttribute("Unidad");
                                                det.claveUnidad = reader.GetAttribute("ClaveUnidad");
                                                det.claveProdServ = reader.GetAttribute("ClaveProdServ");
                                                det.retencion = 0;
                                                det.tretencion = 0;
                                                det.nparte = "";
                                                det.iva = float.Parse(fact.tasa);
                                            }
                                            if (nodo == "cfdi:Traslado")
                                            {
                                                if (reader.GetAttribute("Base") != "" && reader.GetAttribute("Base") != null && reader.GetAttribute("Base") != string.Empty)
                                                {
                                                    det.impuesto = float.Parse(reader.GetAttribute("Importe"));

                                                    //insertamos en la tabla
                                                    det.insertarDetFactura();
                                                }
                                            }
                                        }
                                    }
                                }

                                //actualizamos a pdf la factura
                                fact.actualizarStatusTimbre();

                                //insertamos en log_idCO
                                co.nombre_archivo = fuArchivo.FileName;
                                co.archivo_pdf = fuArchivo.FileName.Replace(".xml", ".pdf").Replace(".XML", ".pdf");
                                co.carpeta = emp.Ncarpeta;
                                co.id_empresa = emp.idEmpresa;
                                co.id_factura = fact.idfactura;
                                co.insert();

                                //mostramos mensaje, cerramos pagina y actualizamos la pagina padre
                                ScriptManager.RegisterStartupScript(Page, GetType(), "", "Terminar()", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "", "error('Factura ya existente')", true);
                            }
                        }
                        else
                        {
                            //mostramos error de que no existe
                            ScriptManager.RegisterStartupScript(Page, GetType(), "", "error('La factura no pertenece a un cliente del sistema')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "", "error('La factura no es de la empresa')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "", "error('Solo se pueden subir archivos xml')", true);
                }
            }
        }

        //metodo para generar la cadena Original
        internal string cadenaOriginal(string archivo)
        {
            //cargamos el xml
            StreamReader reader = new StreamReader(archivo);
            XPathDocument pathDoc = new XPathDocument(reader);

            //cargamos el xslt
            XslCompiledTransform myTrans = new XslCompiledTransform();
            myTrans.Load(conf.path + @"Proceso_facturacion\cadenaoriginal_3_3.xslt");

            StringWriter str = new StringWriter();
            XmlTextWriter myWriter = new XmlTextWriter(str);

            //aplicando transformacion
            myTrans.Transform(pathDoc, null, myWriter);

            return str.ToString();
        }
    }
}