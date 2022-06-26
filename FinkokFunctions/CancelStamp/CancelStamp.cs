using Microsoft.VisualBasic.FileIO;
using ServiceCancelacion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinkokFunctions.CancelStamp
{
    public  class CancelStamp
    {
        public string cfdiCancelation(string UUIDtoCancel, string cerFilesRoot, string compiledFilesRoot)
        {
            string directCer = cerFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer";
            string directKey = cerFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key";
            string passwordFinkok = "The_Beatles1960";
            string passwordCer = "12345678a";
            string username = "uriel.rr@ticas.com.mx";
            string statusUuid = "";
            string certifiedPem, keyEnc;
            try
            {
                PEMFactory(directCer, directKey, passwordFinkok, passwordCer, compiledFilesRoot,out certifiedPem, out keyEnc);
                string cer = "";
                string key = "";

                using (TextFieldParser fileReader = new TextFieldParser(certifiedPem))
                    cer = fileReader.ReadToEnd();

                using (TextFieldParser fileReader = new TextFieldParser(keyEnc))
                    key = fileReader.ReadToEnd();
                ServiceCancelacion.ApplicationClient cancela = new ServiceCancelacion.ApplicationClient();

                cancel can = new cancel();

                List<UUID> listUuid = new List<UUID>();
                listUuid.Add(new UUID { UUID1 = UUIDtoCancel, FolioSustitucion = "", Motivo = "02" });

                can.username = username;
                can.password = passwordFinkok;
                can.taxpayer_id = "EKU9003173C9";
                can.UUIDS = listUuid.ToArray();
                can.cer = stringToBase64ByteArray(cer);
                can.key = stringToBase64ByteArray(key);
                cancelResponse1 cancelResponse = new cancelResponse1();

                cancelResponse = cancela.cancelAsync(can).Result;

                if (cancelResponse.cancelResponse.cancelResult.CodEstatus == null)
                {
                    string emisor = cancelResponse.cancelResponse.cancelResult.RfcEmisor;
                    string acuse = cancelResponse.cancelResponse.cancelResult.Acuse;
                    string date = cancelResponse.cancelResponse.cancelResult.Fecha;

                    Array folioFiscal = cancelResponse.cancelResponse.cancelResult.Folios;

                    for (int pos = 0; pos < folioFiscal.Length; pos++)
                    {
                        Console.WriteLine("UUID: " + cancelResponse.cancelResponse.cancelResult.Folios[pos].UUID +
                                 "\nEstatus cancelación: " + cancelResponse.cancelResponse.cancelResult.Folios[pos].EstatusCancelacion +
                                 "\nEstatus UUID: " + cancelResponse.cancelResponse.cancelResult.Folios[pos].EstatusUUID);
                    }
                    statusUuid = cancelResponse.cancelResponse.cancelResult.Folios.FirstOrDefault().UUID;
                    return statusUuid;
                }
                else
                {
                    statusUuid = cancelResponse.cancelResponse.cancelResult.CodEstatus;
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
        public void PEMFactory(string cer, string key, string pass, string passCSDoFIEL, string outputPath, out string finalPathCertifiedPem, out string finalPathKeyEnc)
        {
            Dictionary<String, String> DicArchivos = new Dictionary<String, String>();

            string convertCerToPem;
            string convertKeyToPem;
            string encryptaKey;
            string fileCer = cer;
            string fileKey = key;
            string nameFileCertified = Path.GetFileName(fileCer);
            string nameFileKey = Path.GetFileName(fileKey);

            

            string url;
            url = outputPath;
            string path;
            path = "C:\\Program Files\\OpenSSL-Win64\\bin\\";

             finalPathCertifiedPem = url + nameFileCertified + ".pem";
             finalPathKeyEnc = url + nameFileKey + ".enc";


            convertCerToPem = $"openssl x509 -inform DER -outform PEM -in \"{fileCer}\" -pubkey -out \"{finalPathCertifiedPem}\"";
            convertKeyToPem = $"openssl pkcs8 -inform DER -in \"{fileKey}\" -passin pass:{passCSDoFIEL} -out \"{url + nameFileKey + ".pem"}\"";
            encryptaKey = $"openssl rsa -in \"{url + nameFileKey + ".pem"}\" -des3 -out \"{finalPathKeyEnc}\" -passout pass:{pass}";


            System.IO.StreamWriter oSW = new System.IO.StreamWriter(url + "CERyKEY.bat");
            oSW.WriteLine(convertCerToPem);
            oSW.WriteLine(convertKeyToPem);
            oSW.WriteLine(encryptaKey);
            oSW.Flush();
            oSW.Close();

            //Process.Start(url + "CERyKEY.bat").WaitForExit();
            
        }
    }
}
