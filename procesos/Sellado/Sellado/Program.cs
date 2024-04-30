using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Sellado.Clases;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Sellado
{
    class Program
    {
        //generamos los objetos a utilizar en el proceso
        static Factura fact;
        static Configuraciones config;
        static Folios folio;
        static LogCO co;
        static Empresa emp;
        static Cliente clnt;

        //datatables para facturas y detalles
        static DataTable facturas;
        static XmlDocument doc;
        static Timer t;

        static void Main(string[] args)
        {
            //generamos el timer con tiempo de ejecucion cada 10 segundos
            using (t = new Timer())
            {
                t.Interval = 30000;
                t.Elapsed += foliarFactura;
                t.AutoReset = true;
                t.Start();
                Console.WriteLine("Sistema de Facturacion Foleado CFDI Arcanet V3.3 " + System.Configuration.ConfigurationManager.AppSettings["Empresa"]);
                Console.WriteLine("Para salir, presiona una tecla");
                Console.ReadLine();

            }
        }

        //metodo para dar folio y demás datos a la factura
        internal static void foliarFactura(Object sender, EventArgs e)
        {
            try
            {
                //inicializamos los objetos 
                fact = new Factura();
                config = new Configuraciones();
                folio = new Folios();
                co = new LogCO();
                facturas = new DataTable();
                emp = new Empresa();
                clnt = new Cliente(); //se agrega clase cliente para nombre del cliente al crear carpeta 02/11/2020

                //procedemos a buscar las facturas
                facturas = co.buscarPendientes();
                string nombreXML = string.Empty;

                //bloqueamos para que se ejecute una vez cada que le toque
                lock (facturas)
                {
                    //si hay facturas por procesar
                    if (facturas.Rows.Count != 0)
                    {
                        //paramos el timer
                        t.Stop();

                        foreach (DataRow row in facturas.Rows)
                        {
                            //obtenemos los datos necesarios
                            co.nombre_archivo = row["nombre_archivo"].ToString();
                            co.nombre_pdf = row["archivo_pdf"].ToString();
                            co.carpeta = row["carpeta"].ToString();
                            co.status = "Terminado";
                            co.idFolios = long.Parse(row["id_folios"].ToString());
                            co.observaciones = "";
                            co.idFactura = long.Parse(row["id_factura"].ToString());

                            //buscamos los datos de la factura
                            fact.idFactura = co.idFactura;
                            fact.buscarFacturas();

                            //obtenemos las configuraciones
                            config.idEmpresa = fact.idEmpresa;
                            config.buscarConfig();

                            //mostramos mensaje de la factura que se está generando
                            Console.WriteLine("Arcanet CFDI 3.3 " + DateTime.Now);
                            Console.WriteLine("Procesando el ID: " + co.idFactura.ToString());

                            //buscamos los datos de los folios
                            folio.idLogs = co.idFolios;
                            folio.buscarFolios();

                            //validamos si la factura ya tiene folio
                            if (fact.folio == "" && fact.folio == string.Empty)
                            {
                                //validamos que el folio obtenido sea diferente a 0
                                if (folio.folioActivo != 0)
                                {
                                    //si no tiene folio validamos que el folio activo no sobrepase al folio final
                                    if (folio.folioActivo > folio.folioFinal)
                                    {
                                        //si lo sobrepasa enviarmos mensaje de error
                                        throw new Exception("El folio asignado a la factura es MAYOR al folio Final guardado");
                                    }
                                    else
                                    {
                                        fact.folio = folio.folioActivo.ToString();
                                        fact.serie = folio.serie;

                                        //actualizamos el folio en la bd
                                        folio.folioActivo += 1;
                                        folio.actualizarFolio(folio.folioActivo);
                                    }

                                    //mostramos mensja de folio asignado
                                    Console.WriteLine("Folio asignado: " + fact.folio);
                                }
                                else
                                {
                                    //si lo sobrepasa enviarmos mensaje de error
                                    throw new Exception("Folio no encontrado | Factura sin foliar");
                                }
                            }
                            else
                            {
                                //si no está vacío validamos si no excede el folio final
                                if (long.Parse(fact.folio) > folio.folioFinal)
                                {
                                    throw new Exception("El folio asignado a la factura es MAYOR al folio Final guardado | Factura sin foliar");
                                }
                                else
                                {
                                    fact.serie = folio.serie;
                                }
                            }

                            //buscamos el nombre del archivo del certificado
                            emp.idempresa = fact.idEmpresa;
                            emp.archivoCer();

                            //buscamos datos del cliente 02/11/2020
                            clnt.idCliente = fact.idCliente;
                            clnt.datosCliente();
                            //mostramos mensaje de generacion de xml
                            Console.WriteLine("Foliando el XML");

                            //buscamos el xml en la carpeta del sistema
                            doc = new XmlDocument();
                            doc.Load(config.path + co.carpeta + @"\" + co.nombre_archivo);
                            //agregamos el atributo del folio
                            XmlAttribute fol = doc.CreateAttribute("Folio");
                            fol.Value = fact.folio.ToString();
                            doc.DocumentElement.SetAttributeNode(fol);
                            //agregamos el noCertificado
                            XmlAttribute noCer = doc.CreateAttribute("NoCertificado");
                            noCer.Value = certificadoDigital(config.path + co.carpeta + @"\" + emp.nombreCer, emp.contraseña);
                            fact.noCertificado = noCer.Value;
                            doc.DocumentElement.SetAttributeNode(noCer);
                            //agregamos la serie
                            //XmlAttribute ser = doc.CreateAttribute("Serie");
                            //ser.Value = fact.serie;
                            //doc.DocumentElement.SetAttributeNode(ser);
                            //agregando los nuevos nombres del archivo
                            nombreXML = config.path + fact.carpeta + DateTime.Today.Year.ToString() + "_" + fact.folio + fact.serie + "_" + fact.abreviatura + "_" + co.idFactura + "CO.xml";
                            //co.nombre_archivo = DateTime.Today.Year.ToString() + "_" + fact.folio + fact.serie + "_" + fact.abreviatura + "_" + co.idFactura + "CO.xml";
                            co.nombre_archivo = DateTime.Today.Year.ToString() + @"\" + clnt.nombreCliente.Replace(".", "") + @"\" + DateTime.Today.Year.ToString() + "_" + fact.folio + fact.serie + "_" + fact.abreviatura + "_" + co.idFactura + "CO.xml";
                            //co.nombre_pdf = DateTime.Today.Year.ToString() + "_" + fact.folio + fact.serie + "_" + fact.abreviatura + "_" + co.idFactura + "CO.pdf";
                            co.nombre_pdf = DateTime.Today.Year.ToString() + @"\" + clnt.nombreCliente.Replace(".", "") + @"\" + DateTime.Today.Year.ToString() + "_" + fact.folio + fact.serie + "_" + fact.abreviatura + "_" + co.idFactura + "CO.pdf";
                            //guardamos con el nuevo nombre el xml
                            XmlWriter writer = new XmlTextWriter(nombreXML, Encoding.UTF8);
                            doc.WriteTo(writer);
                            writer.Close();

                            //verificamos si existe la carpeta del cliente dentro del año en curso agregado 02/11/2020
                            if(!Directory.Exists(config.path + fact.carpetaTimbre + @"\" + DateTime.Today.Year.ToString() + @"\" + clnt.nombreCliente.Replace(".", "")))
                            {
                                //creamos la carpeta
                                Directory.CreateDirectory(config.path + fact.carpetaTimbre + @"\" + DateTime.Today.Year.ToString() + @"\" + clnt.nombreCliente.Replace(".", ""));
                            }

                            //generamos la cadena original
                            fact.CadenaOrig = cadenaOriginal(nombreXML);

                            //actualizamos tanto el idCO como la factura
                            fact.idFactura = co.idFactura;
                            fact.estatus = "Facturada";
                            fact.actualizarFactura();

                            co.observaciones = "Terminado con exito";
                            co.modificarLog();

                            //mostramos mensaje de terminado
                            Console.WriteLine("Proceso Terminado");
                            Console.WriteLine("");

                            //limpiamos variables
                            fact.limpiar();
                            co.limpiar();
                            folio.limpiar();
                            config.limpiar();
                        }

                        //activamos el timer
                        t.Start();
                    }                    
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
                co.observaciones = ex.Message;
                co.modificarError();
                t.Start();
            }
        }

        //metodo para generar la cadena Original
        internal static string cadenaOriginal(string archivo)
        {
            //cargamos el xml
            StreamReader reader = new StreamReader(archivo);
            XPathDocument pathDoc = new XPathDocument(reader);

            //cargamos el xslt
            XslCompiledTransform myTrans = new XslCompiledTransform();
            myTrans.Load(config.path + @"Proceso_facturacion\cadenaoriginal_3_3.xslt");

            StringWriter str = new StringWriter();
            XmlTextWriter myWriter = new XmlTextWriter(str);

            //aplicando transformacion
            myTrans.Transform(pathDoc, null, myWriter);

            return str.ToString();
        }

        //metodp para obtener el certificado digital de la empresa
        internal static string certificadoDigital(string archivo, string contraseña)
        {
            try
            {
                string cert = "";

                //abrimos el archivo .cer con la contraseña
                X509Certificate2 m_cer = new X509Certificate2(archivo, contraseña);

                //validamos si se pudo leer el archivo
                if (m_cer != null)
                {
                    //obtenemos el numero de serie convirtiendolo a string
                    cert = Encoding.Default.GetString(m_cer.GetSerialNumber());

                    //se obtiene el noCertificado pero de forma inversa
                    //hay que invertirlo para que sea correcto
                    char[] arrayes = cert.ToCharArray();
                    Array.Reverse(arrayes);
                    cert = new string(arrayes);
                }
                else
                {
                    throw new Exception("No se encontró el archivo del Certificado");
                }

                return cert;
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

    }
}
