﻿using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using Org.BouncyCastle.X509;
using System.Xml.Serialization;

namespace XMLFunctions
{
    public class XMLMethods
    {

        public string structureXML()
        {
            Comprobante comprobante = new Comprobante();
            comprobante.Version = "4.0";
            comprobante.Serie = "d";
            comprobante.Folio = "1";
            comprobante.Sello = "Faltante";
            comprobante.NoCertificado = "123456789897867654321";
            comprobante.Certificado = "";
            comprobante.SubTotal = 10m;
            comprobante.Descuento = 1;
            comprobante.Moneda = c_Moneda.MXN;
            comprobante.Total = 9;
            comprobante.TipoDeComprobante = c_TipoDeComprobante.I;
            comprobante.MetodoPago = c_MetodoPago.PUE;
            comprobante.Exportacion = c_Exportacion.Item04;
            comprobante.LugarExpedicion = "75660";


            ComprobanteEmisor oEmisor = new ComprobanteEmisor();
            oEmisor.Rfc = "RORU00090UZ2";
            oEmisor.Nombre = "Una Razon";
            oEmisor.RegimenFiscal = c_RegimenFiscal.Item605;

            ComprobanteReceptor oReceptor = new ComprobanteReceptor();
            oReceptor.Nombre = "pepe";
            oReceptor.Rfc = "PEPE0009012U";
            oReceptor.DomicilioFiscalReceptor = "75660";
            oReceptor.RegimenFiscalReceptor = c_RegimenFiscal.Item607;
            oReceptor.UsoCFDI = c_UsoCFDI.P01;

            comprobante.Emisor = oEmisor;
            comprobante.Receptor = oReceptor;

            List<ComprobanteConcepto> listConcept = new List<ComprobanteConcepto>();
            ComprobanteConcepto oConcepto = new ComprobanteConcepto();
            oConcepto.Importe = 10m;
            oConcepto.ClaveProdServ = "2323534";
            oConcepto.Cantidad = 1;
            oConcepto.ClaveUnidad = "CR1";
            oConcepto.Descripcion = "Una bomba nuclear de las chiquitas";
            oConcepto.ValorUnitario = 10m;
            oConcepto.Importe = 10m;

            listConcept.Add(oConcepto);
            comprobante.Conceptos = listConcept.ToArray();



            string path = @"C:\XML\miSegundoXML.xml";
            
            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Comprobante));
            string sXML = "";

            using(var sww = new StringWriter())
            {
                using (XmlWriter writter = XmlWriter.Create(sww))
                {
                    oXmlSerializer.Serialize(writter, comprobante);
                    sXML = sww.ToString();
                }
                    
            }

            System.IO.File.WriteAllText(path, sXML);

            return "todo bien creo";
            
        }
        public string generateOriginalString(string cfdiFilesRoot)
        {
            string XSLTRoot = @"http://www.sat.gob.mx/sitio_internet/cfd/4/cadenaoriginal_4_0/cadenaoriginal_4_0.xslt";
            string rutaXML = cfdiFilesRoot +  "cfdi_1.xml";

            XPathDocument xml = new XPathDocument(rutaXML);

            XslCompiledTransform transform = new XslCompiledTransform();

            transform.Load(xml);

            StringWriter str = new StringWriter();
            XmlTextWriter cad = new XmlTextWriter(str);
            transform.Transform(rutaXML, cad);
            string result = str.ToString();

            return result;
        }

        public string createStringPEM()
        {

            string SFileFrom = "C:\\Users\\WAR-PLANE\\Desktop\\Proyectos\\Facturacion\\pruebaKey\\CSD_EKU9003173C9_20190617131829\\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer";
            string SFileTo = "C:\\Users\\WAR-PLANE\\Desktop\\Proyectos\\Facturacion\\pruebaKey\\CSD_EKU9003173C9_20190617131829\\newFilePem.pem";

            if (File.Exists(SFileTo))
            {
                File.Delete(SFileTo);

            }

            Stream sr = File.OpenRead(SFileFrom);
            X509CertificateParser cp = new X509CertificateParser();
            Object cert = cp.ReadCertificate(sr);
            TextWriter tw = new StreamWriter(SFileTo);
            Org.BouncyCastle.OpenSsl.PemWriter pw = new Org.BouncyCastle.OpenSsl.PemWriter(tw);

            pw.WriteObject(cert);
            tw.Close();

            return ""
                ;
        }
    }
}