using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FactureyaService;
namespace FactureyaFunctions.Stamp
{
    public class StampTask
    {

        public async Task<string> stamp(string stampedRoot, string cfdiRoot)

        {

            string user = "RORU000901UZ2";
            string password = "Facturas+00";

            string message = "";

            WSCFDI33Client stampService = new WSCFDI33Client();

            RespuestaTFD33 stampResponse = new RespuestaTFD33();
            XmlDocument cfdi = new XmlDocument();
            cfdi.Load(cfdiRoot + "miSegundoXML.xml");
            string stringXML = cfdi.OuterXml;

            stampResponse = await stampService.TimbrarCFDIAsync(user,password, stringXML, "00001");

            if(stampResponse.OperacionExitosa == true)
            {
                cfdi.LoadXml(stampResponse.XMLResultado);
                cfdi.Save(stampedRoot + "XML_Timbrado.xml");
                message = stampResponse.Timbre.UUID;
            }
            else
            {
                message = stampResponse.MensajeErrorDetallado;
            }


            return message;

        }
       

            


    }
}
