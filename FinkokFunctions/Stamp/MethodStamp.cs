using System;
using System.Text;
using System.Xml;
using ServiceCancelacion;
using Microsoft.VisualBasic.FileIO;
using ServiceFinkok;
namespace FinkokFunctions.Stamp
{
    public class MethodStamp
    {

        public string invoice(string cfdiFilesRoot)
        {
            string user = "uriel.rr@ticas.com.mx";
            string password = "The_Beatles1960";
            string stampXMLName = "cfdiFelixManuel.xml";
            ServiceFinkok.ApplicationClient tim = new ServiceFinkok.ApplicationClient();

            stamp param = new stamp();

            stampResponse1 response = new stampResponse1();

            XmlDocument xmlChargeDocument = new XmlDocument();

            xmlChargeDocument.Load(cfdiFilesRoot + stampXMLName);


            byte[] byteXmlDocument = Encoding.UTF8.GetBytes(xmlChargeDocument.OuterXml);

            string stringByteXmlDocument = Convert.ToBase64String(byteXmlDocument);

            byteXmlDocument = Convert.FromBase64String(stringByteXmlDocument);

            param.username = user;
            param.password = password;
            param.xml = byteXmlDocument;

            response = tim.stampAsync(param).Result;
            try
            {

                Console.WriteLine("No se timbro el XML, se presento un error: " + response.stampResponse.stampResult.Incidencias[0].CodigoError.ToString());
                Console.WriteLine(response.stampResponse.stampResult.Incidencias[0].MensajeIncidencia.ToString());
                return "No se timbro el XML, se presento un error: " + response.stampResponse.stampResult.Incidencias[0].CodigoError.ToString() + ' ' +  response.stampResponse.stampResult.Incidencias[0].MensajeIncidencia.ToString();
            }
            catch (Exception)
            {
                //Imprime el valor de la varible CodEstatus en este caso seria "Comprobante timbrado satisfactoriamente"
                Console.WriteLine(response.stampResponse.stampResult.CodEstatus.ToString());
                //Imprime la fecha de timbrado
                Console.WriteLine(response.stampResponse.stampResult.Fecha.ToString());
                //Imprime el UUID asignado al XML
                Console.WriteLine(response.stampResponse.stampResult.UUID.ToString());
                //Imprime el XML timbrado
                Console.WriteLine(response.stampResponse.stampResult.xml.ToString());

                return response.stampResponse.stampResult.UUID.ToString();
            }



        }
    }
}

