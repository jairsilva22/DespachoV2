using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using despacho.DemoCancelacion;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace despacho
{
    public partial class HacerCancelacion2 : System.Web.UI.Page
    {
        //variables para la pagina
        string mensaje;
        Factura fac;

        protected void Page_Load(object sender, EventArgs e)
        {
            fac = new Factura();
            try
            {
                if (!IsPostBack)
                {
                    //buscamos los datos de la factura
                    fac.idfactura = long.Parse(Request.QueryString["id"]);
                    fac.datosFactura();

                    //validamos en que estatus está la factura
                    if (fac.estatus == "PCancelada")
                    {
                        lblMensaje.Text = mensajeEstatusCancelacionSAT(fac.uuid, fac.idempresa);
                    }
                    else
                    {
                        //validamos si no se ha cancelado la factura anteriormente
                        if (fac.estatus != "Cancelada")
                        {
                            CancelarXML(int.Parse(Request.QueryString["id"]));
                        }
                        else
                        {
                            //si es así buscamos el mensaje del SAT
                            lblMensaje.Text = mensajeEstatusCancelacionSAT(fac.uuid, fac.idempresa);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        //metodo para realizar la cancelacion del xml
        internal void CancelarXML(int idF)
        {
            //clases para la factura
            Factura fac = new Factura();
            Configuracion config = new Configuracion();
            Cliente cln = new Cliente();
            Empresa emp = new Empresa();

            //buscamos los datos de la factura
            fac.idfactura = long.Parse(idF.ToString());
            fac.buscarFactura();

            //buscamos los datos del cliente
            cln.idCliente = fac.idcliente;
            cln.buscarCliente();

            //buscamos la configuracion del sistema
            config.idempresa = fac.idempresa;
            config.configProcesos();

            //buscamos los datos de la empresa
            emp.idEmpresa = fac.idempresa;
            emp.empresaProcesos();
            emp.archivoCer();

            String DireccionCer = config.path + emp.Ncarpeta + @"\" + emp.nombreCer;
            String DireccionKey = config.path + emp.Ncarpeta + @"\" + emp.nombreKey; ;
            String PasswordFinkok = "Atrejo*1321";
            String username = "soporte@catedralsoftware.com";
            String PasswordCer = emp.contraseña;
            String estatusUuid = "";
            try
            {
                FabricaPEM(DireccionCer, DireccionKey, PasswordFinkok, PasswordCer, config.path, emp.Ncarpeta);
                String cer;
                String key;

                //Para importar clase TextFieldParser, ingresas al menú Proyecto-- > Agregar Referencia-- > Ensamblados-- > Seleccionar Microsotf.VisualBasic-- > Aceptar
                using (TextFieldParser fileReader = new TextFieldParser(config.path + emp.Ncarpeta + @"\" + emp.nombreCer + ".pem"))
                    cer = fileReader.ReadToEnd();


                using (TextFieldParser fileReader = new TextFieldParser(config.path + emp.Ncarpeta + @"\" + emp.nombreKey + ".enc"))
                    key = fileReader.ReadToEnd();

                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                CancelSOAP cancela = new CancelSOAP();
                cancel can = new cancel();
                UUIDS nim = new UUIDS();
                string[] uid = fac.uuid.Split('&');
                string[] uuid = new string[] { uid[uid.Length - 1].Replace("id=", "") };
                String rfc;
                String status = "";
                rfc = emp.rfc;
                nim.uuids = uuid.ToArray();
                can.UUIDS = nim;
                can.username = username;
                can.password = PasswordFinkok;
                can.taxpayer_id = rfc;
                can.cer = stringToBase64ByteArray(cer);
                can.key = stringToBase64ByteArray(key);

                //crea el SOAP_Request para errores de FINKOK
                /*String usuario;
                 usuario = Environment.UserName;
                 String url = "C:\\Users\\" + usuario + "\\Documents\\";
                 StreamWriter XML = new StreamWriter(url + "SOAP_Request.xml");     //Direccion donde guardaremos el SOAP Envelope
                 XmlSerializer soap = new XmlSerializer(can.GetType());
                 soap.Serialize(XML, can);
                 XML.Close();*/
                cancelResponse cancelresponse = new cancelResponse();

                try
                {
                    cancelresponse = cancela.cancel(can);
                    status = "Folio: " + fac.folio + "<br/>";
                    status += "Cliente: " + cln.nombreCliente + "<br/>";
                    status += "UUID: " + cancelresponse.cancelResult.Folios[0].UUID + "<br/>";
                    status += "EstatusUUID: " + cancelresponse.cancelResult.Folios[0].EstatusUUID + "<br/>";
                    status += "EstatusCancelacion: " + cancelresponse.cancelResult.Folios[0].EstatusCancelacion;

                    mensaje = status;

                    //cambiamos el estatus de la factura
                    fac.idfactura = long.Parse(idF.ToString());
                    fac.codigoCan = cancelresponse.cancelResult.Folios[0].EstatusUUID;
                    fac.fechaSolicitud = DateTime.Now;
                    fac.procesoCancelar();

                    //agregamos los datos a la tabla de cancelados
                    FacturaCancelada fcan = new FacturaCancelada();
                    fcan.idfactura = long.Parse(idF.ToString());
                    fcan.folio = fac.folio;
                    fcan.total = fac.total;
                    fcan.uuid = cancelresponse.cancelResult.Folios[0].UUID;
                    fcan.codigoCan = fac.codigoCan;
                    fcan.fechaSolicitud = DateTime.Now;
                    fcan.observaciones = status;
                    fcan.insertar();

                    //Mostramos mensaje de la cancelacion
                    lblMensaje.Text = mensaje;
                }
                catch (Exception)
                {
                    estatusUuid = cancelresponse.cancelResult.CodEstatus;
                    mensaje = estatusUuid;

                    lblMensaje.Text = mensaje;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //metodo para obtener el estatus de la cancelacion del cfdi
        internal string[] ObtenerStatusSat(string rfc, string rfcCliente, string uuid, int idempresa)
        {
            CancelSOAP selloSoap = new CancelSOAP();
            get_sat_status consulta = new get_sat_status();
            get_sat_statusResponse getResponse = new get_sat_statusResponse();

            consulta.username = "soporte@catedralsoftware.com";
            consulta.password = "Atrejo*1321";
            consulta.taxpayer_id = rfc;
            consulta.rtaxpayer_id = rfcCliente;
            consulta.uuid = uuid;
            //buscamos el nombre del archivo
            Factura fac = new Factura();
            fac.idfactura = long.Parse(Request.QueryString["id"]);
            fac.nombreXml();
            //leemos el xml
            consulta.total = leerXML(fac.abreviatura, idempresa);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            getResponse = selloSoap.get_sat_status(consulta);
            String Escancelable = "";
            String CodigoEstatus = "";
            String Estado = "";
            String estatusUuid = "";
            //declaramos el arreglo para guardar los datos
            string[] resultado = new string[4];

            try
            {
                Escancelable = getResponse.get_sat_statusResult.sat.EsCancelable;
                CodigoEstatus = getResponse.get_sat_statusResult.sat.CodigoEstatus;
                Estado = getResponse.get_sat_statusResult.sat.Estado;
                estatusUuid = getResponse.get_sat_statusResult.error;

                //agregamos los datos al arreglo
                resultado[0] = Escancelable;
                resultado[1] = CodigoEstatus;
                resultado[2] = Estado;
                resultado[3] = estatusUuid;
            }
            catch (Exception)
            {
                resultado[0] = estatusUuid;
            }

            return resultado;
        }

        //metodo para obtener el mensaje del estado de cancelacion
        internal string mensajeEstatusCancelacionSAT(string uuid, int idempresa)
        {
            string[] uid1 = uuid.Split('&');
            string[] uuid1 = new string[] { uid1[uid1.Length - 1].Replace("id=", "") };

            //buscamos los datos de la empresa
            Empresa emp = new Empresa();

            emp.idEmpresa = idempresa;
            emp.empresaProcesos();

            //buscamos el estatus de cancelacion
            string[] resultado = ObtenerStatusSat(emp.rfc, fac.ordenCompra, uuid1[uuid1.Length - 1], idempresa);
            string mensaje = "";

            mensaje += "Folio: " + fac.folio + "<br/>";
            mensaje += "Cliente: " + fac.vendedor + "<br/>";
            mensaje += "UUID: " + uuid1[uuid1.Length - 1] + "<br/>";
            mensaje += "Estado Cancelacion: " + resultado[2] + "<br/>";

            //cancelamos la factura por completo
            if (resultado[2] == "Cancelado")
            {
                //actualizamos a cancelada la factura y regeneramos el pdf
                fac.cancelacionFactura();
            }

            //actualizamos los datos de cancelacion
            FacturaCancelada fcan = new FacturaCancelada();
            fcan.folio = fac.folio;
            fcan.idfactura = fac.idfactura;
            fcan.observaciones = resultado[2];
            fcan.codigoCan = resultado[1];
            fcan.modificarCodigo();

            return mensaje;
        }

        internal void FabricaPEM(String cer, String key, String pass, String passCSDoFIEL, string path, string carpetaTimbre)
        {
            Dictionary<String, String> DicArchivos = new Dictionary<String, String>();
            String ConvierteCerAPem;
            String ConvierteKeyAPem;
            String EncriptaKey;
            String ArchivoCer = cer;
            String ArchivoKey = key;
            String NombreArchivoCertificado = Path.GetFileName(ArchivoCer);
            String NombreArchivoLlave = Path.GetFileName(ArchivoKey);
            String url;
            url = path + carpetaTimbre + @"\";
            String ruta;
            ruta = path + carpetaTimbre + @"\";//Esta ruta es donde tiene ubicado el .exe del OpenSSL
            ConvierteCerAPem = ruta + "openssl.exe x509 -inform DER -outform PEM -in " + ArchivoCer + " -pubkey -out " + url + NombreArchivoCertificado + ".pem";
            ConvierteKeyAPem = ruta + "openssl.exe pkcs8 -inform DER -in " + ArchivoKey + " -passin pass:" + passCSDoFIEL + " -out " + url + NombreArchivoLlave + ".pem";
            EncriptaKey = ruta + "openssl.exe rsa -in " + url + NombreArchivoLlave + ".pem" + " -des3 -out " + url + NombreArchivoLlave + ".enc -passout pass:" + pass;

            //Crea el archivo Certificado.BAT
            System.IO.StreamWriter oSW = new System.IO.StreamWriter(url + "CERyKEY.bat");
            oSW.WriteLine(ConvierteCerAPem);
            oSW.WriteLine(ConvierteKeyAPem);
            oSW.WriteLine(EncriptaKey);
            oSW.Flush();
            oSW.Close();

            Process.Start(url + "CERyKEY.bat").WaitForExit();
        }

        internal byte[] stringToBase64ByteArray(String input)
        {
            Byte[] ret = Encoding.UTF8.GetBytes(input);
            String s = Convert.ToBase64String(ret);
            ret = Convert.FromBase64String(s);
            return ret;
        }

        internal string leerXML(string xml, int idemp)
        {
            string total = "";
            Empresa emp = new Empresa();
            Configuracion config = new Configuracion();

            //buscamos la configuracion del sistema
            config.idempresa = idemp;
            config.configProcesos();

            //buscamos los datos de la empresa
            emp.idEmpresa = idemp;
            emp.empresaProcesos();

            using (XmlTextReader reader = new XmlTextReader(config.path + emp.carpetaTimbre + @"\" + xml))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "Total")
                                {
                                    total = reader.Value;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return total;
        }
    }
}