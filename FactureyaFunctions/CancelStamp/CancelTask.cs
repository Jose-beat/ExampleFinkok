using FactureyaFunctions.Utils;
using FactureyaService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace FactureyaFunctions.CancelStamp
{
    public class CancelTask
    {
        public async Task<string> cancelCfdi(string inputUUID, string cerFilesRoot, string compiledFilesRoot)
        {
            string directCer = cerFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer";
            string directKey = cerFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key";
            string voucherCancelRoot = cerFilesRoot + "\\CancelVaucher\\";
            string pfxRoot = compiledFilesRoot + "myfile.pfx";
            string encRoot = compiledFilesRoot + "myfile.enc";
            string username = "uriel.rr@ticas.com.mx";
            string issuingRFC = "RORU000901UZ2";
            string PFXPassword = "12345678a";
            string PFXBase64  = "";
            string finalResponse = "";
            DetalleCancelacion responseWS = new DetalleCancelacion();

            WSCFDI33Client client = new WSCFDI33Client();
            RespuestaCancelacion response = new RespuestaCancelacion();
            List<DetalleCancelacion> cancelDetails = new List<DetalleCancelacion>();
            List<DetalleCFDICancelacion> cfdiDetails = new List<DetalleCFDICancelacion>();

            DetalleCFDICancelacion testCfdi = new DetalleCFDICancelacion() {UUID = inputUUID };
            cfdiDetails.Add(testCfdi);
            CertifiedTools tools = new CertifiedTools(directCer, directKey, PFXPassword, pfxRoot, compiledFilesRoot, encRoot);

            if (!tools.createPFX())
            {
                finalResponse = "Ocurrio un error de certificados";
                return finalResponse;
            }

            PFXBase64 = stringToBase64(tools.PFXFile);
            response = await client.CancelarCFDIAsync(username, PFXPassword, issuingRFC, cfdiDetails.ToArray(), PFXBase64, PFXPassword);

            if (response.OperacionExitosa == true)
            {
                cancelDetails = response.DetallesCancelacion.ToList();

                responseWS = cancelDetails.FirstOrDefault();
                finalResponse = responseWS.CodigoResultado + " " + responseWS.MensajeResultado;


                XmlDocument AcuseXML = new XmlDocument();
                AcuseXML.LoadXml(response.XMLAcuse);
                AcuseXML.Save(voucherCancelRoot);
            }
            else
            {

                finalResponse = response.MensajeErrorDetallado;

            }
            return finalResponse;
        }

        public string stringToBase64(string input)
        {
            Byte[] ret = Encoding.UTF8.GetBytes(input);
            string s = Convert.ToBase64String(ret);
            //ret = Convert.FromBase64String(s);
            return s;
        }

    }
}
