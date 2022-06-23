using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using Org.BouncyCastle.X509;

namespace XMLFunctions
{
    public class XMLMethods
    {
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

            return "";
        }
    }
}