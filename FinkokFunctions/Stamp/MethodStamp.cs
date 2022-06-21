using System;
using System.Text;
using System.Xml;
using DemoFinkok.cancelacion;
using Microsoft.VisualBasic.FileIO;
using ServiceFinkok;
namespace FinkokFunctions.Stamp
{
    public class MethodStamp
    {

        public string cancelInvoice(string cerFilesRoot)
        {
            string directCer = cerFilesRoot;
            string directKey = cerFilesRoot;
            string passwordFinkok = "";
            string passwordCer = "";
            string username = "";
            string statusUuid = "";

            try
            {
                fabricaPEM(directCer, directKey, passwordFinkok, passwordCer);
                string cer = "";
                string key = "";

                using (TextFieldParser fileReader = new TextFieldParser(""))
                    key = fileReader.ReadToEnd();

                using (TextFieldParser fileReader = new TextFieldParser(""))
                    key = fileReader.ReadToEnd();
                ApplicationClient cancela = new ApplicationClient();
                cancel can = new cancel();

                List<UUID> listUuid = new List<UUID>();
                listUuid.Add(new UUID { UUID1 = "", FolioSustitucion = "", Motivo = "" });

                can.username = username;
                can.password = passwordFinkok;
                can.taxpayer_id = "";
                can.UUIDS = listUuid.ToArray();
                can.cer = stringToBase64ByteArray(cer);
                cancelResponse cancelResponse = new cancelResponse();

              // cancelResponse = cancelAsync(can).Result; 

                if (cancelResponse.cancelResult.CodEstatus == null)
                {
                    string emisor = cancelResponse.cancelResult.RfcEmisor;
                    string acuse = cancelResponse.cancelResult.Acuse;
                    string date = cancelResponse.cancelResult.Fecha;

                    Array folioFiscal = cancelResponse.cancelResult.Folios;

                    for (int pos = 0; pos < folioFiscal.Length; pos++)
                    {
                     Console.WriteLine("UUID: " + cancelResponse.cancelResult.Folios[pos].UUID +
                              "\nEstatus cancelación: " + cancelResponse.cancelResult.Folios[pos].EstatusCancelacion +
                              "\nEstatus UUID: " + cancelResponse.cancelResult.Folios[pos].EstatusUUID);
                    }
                    statusUuid = cancelResponse.cancelResult.CodEstatus;
                    return statusUuid;
                }
                else
                {
                    statusUuid = cancelResponse.cancelResult.CodEstatus;
                    return statusUuid;
                }

            }
            catch (Exception e)
            {
                return statusUuid;
            }

        }

        public byte[] stringToBase64ByteArray(string input)
        {
            Byte[] ret = Encoding.UTF8.GetBytes(input);
            string s = Convert.ToBase64String(ret);
            ret = Convert.FromBase64String(s);
            return ret;
        }

        public void fabricaPEM(string cer, string key, string pass, string passCSDoFIEL)
        {
            Dictionary<String, String> DicArchivos = new Dictionary<String, String>();

            string convertCerToPem;
            string convertKeyToPem;
            string encryptaKey;
            string fileCer = cer;
            string fileKey = key;
            string nameFileCertified = Path.GetFileName(fileCer) ;
            string nameFileKey = Path.GetFileName(fileKey);

            string url;
            url = "";
            string path;
            path = "";

            convertCerToPem = path + "" + fileCer + "" + passCSDoFIEL + "" + nameFileCertified + ".pem";
            convertKeyToPem = path + "" + url + nameFileKey + "" + "" + url + nameFileKey + "" + pass;

            encryptaKey = path + "" + url + nameFileKey + "" + "" + url + nameFileKey + "" + pass;


            System.IO.StreamWriter oSW = new System.IO.StreamWriter(url + "CERyKEY.bat");
            oSW.WriteLine(convertCerToPem);
            oSW.WriteLine(convertKeyToPem);
            oSW.WriteLine(encryptaKey);
            oSW.Flush();
            oSW.Close();


            








        }
        public string invoice(string cfdiFilesRoot)
        {
            string user = "uriel.rr@ticas.com.mx";
            string password = "The_Beatles1960";
            string stampXMLName = "cfdi_1.xml";
            ApplicationClient tim = new ApplicationClient();

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

