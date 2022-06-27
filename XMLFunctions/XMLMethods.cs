﻿using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using Org.BouncyCastle.X509;
using System.Xml.Serialization;
using System.Text;
using XSDToXML.Utils;

namespace XMLFunctions
{
    public class XMLMethods
    {
        string folio;
        string eNombre;
        string eRFC;
        string rNombre;
        string rRFC;
        string rDomicilio;
        string nameXML;


        public XMLMethods(string folio_, string eNombre_, string eRFC_, string rNombre_, string rRFC_, string rDomicilio_, string nameXML)
        {
            folio = folio_;
            eNombre = eNombre_;
            eRFC = eRFC_;
            rNombre = rNombre_;
            rRFC = rRFC_;
            rDomicilio = rDomicilio_;
            this.nameXML = nameXML;
        }

        public string structureXML(string pathCer, string pathKey, string pathXsl, string cfdiFilesRoot)
        {
            
            string clavePrivada = "12345678a";
            string path = cfdiFilesRoot + nameXML + ".xml";

            string numeroCertificado, aa, b, c;

            SelloDigital.leerCER(pathCer, out aa, out b, out c, out numeroCertificado);



            Comprobante comprobante = new Comprobante();
            comprobante.Version = "4.0";
            comprobante.Serie = "d";
            comprobante.Folio = folio;
            //comprobante.Sello = "Faltante";
            comprobante.NoCertificado = numeroCertificado;
           // comprobante.Certificado = "";
            comprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            comprobante.SubTotal = 10m;
            comprobante.Moneda = c_Moneda.MXN;
            comprobante.Total = 10m;
            comprobante.TipoDeComprobante = "E";
            comprobante.MetodoPago = c_MetodoPago.PUE;
            comprobante.FormaPago = c_FormaPago.Item04;
            comprobante.Exportacion = c_Exportacion.Item04;
            comprobante.LugarExpedicion = "75660";


            ComprobanteEmisor oEmisor = new ComprobanteEmisor();
            oEmisor.Rfc = eRFC;
            oEmisor.Nombre = eNombre;
            oEmisor.RegimenFiscal = c_RegimenFiscal.Item601;

            ComprobanteReceptor oReceptor = new ComprobanteReceptor();
            oReceptor.Nombre = rNombre;
            oReceptor.Rfc = rRFC;
            oReceptor.DomicilioFiscalReceptor = rDomicilio;
            oReceptor.RegimenFiscalReceptor = c_RegimenFiscal.Item616;
            oReceptor.UsoCFDI = c_UsoCFDI.S01;

            comprobante.Emisor = oEmisor;
            comprobante.Receptor = oReceptor;

            List<ComprobanteConcepto> listConcept = new List<ComprobanteConcepto>();
            ComprobanteConcepto oConcepto = new ComprobanteConcepto();
            oConcepto.Importe = 10m;
            oConcepto.ClaveProdServ = "50193201";
            oConcepto.Cantidad = 1;
            oConcepto.ClaveUnidad = "AS";
            oConcepto.Descripcion = "Ensalada fresca preparada";
            oConcepto.ValorUnitario = 10m;
            oConcepto.Importe = 10m;
            listConcept.Add(oConcepto);
            comprobante.Conceptos = listConcept.ToArray();

            createXML(comprobante, path);
            string cadenaOriginal = "";
            
            XslCompiledTransform transformador = new XslCompiledTransform(true);
           
            XsltSettings sets = new XsltSettings(true, true);
            var resolver = new XmlUrlResolver();
            transformador.Load(pathXsl, sets, resolver);


            using (StringWriter sw = new StringWriter())
            {
                using(XmlWriter xwo = XmlWriter.Create(sw, transformador.OutputSettings))
                {
                    transformador.Transform(path,xwo);
                    cadenaOriginal = sw.ToString();


                }
            }
            SelloDigital oSelloDigital = new SelloDigital();
            comprobante.Certificado = oSelloDigital.Certificado(pathCer);
            comprobante.Sello = oSelloDigital.Sellar(cadenaOriginal, pathKey, clavePrivada);

            createXML(comprobante, path);
              

            return "todo bien creo";
            
        }
        public void createXML(Comprobante comprobante, string path)
        {
            
            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/timbrefiscaldigital");
            //xmlNameSpace.Add("xs", "http://www.w3.org/2001/XMLSchema");
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Comprobante));
            string sXML = "";

            using (var sww = new StringWritterWithEnconding(Encoding.UTF8))
            {
                using (XmlWriter writter = XmlWriter.Create(sww))
                {
                    oXmlSerializer.Serialize(writter, comprobante, xmlNameSpace);
                    sXML = sww.ToString();
                }

            }

            System.IO.File.WriteAllText(path, sXML);
        }
        
    }
}