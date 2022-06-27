using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactureyaFunctions.Utils
{
    public  class CertifiedTools
    {
        string cer = "";
        string key = "";
        string privateKeyCode = "";
        public string PFXFile = "";
        string KPEMFile = "";
        public string CPEMFile = "";
        public string ENCFile = "";
        string passwordFinkok;
        public string error = "";
        public string succesMessage = "";


        public CertifiedTools(string cer_, string key_, string privateKeyCode_, string PfxFile_, string pathTemp, string EncFile_, string passwordFinkok_ = null)
        {
            cer = cer_;
            key = key_;
            privateKeyCode = privateKeyCode_;
            KPEMFile = pathTemp + "K.pem";
            CPEMFile = pathTemp + "c.pem";
            PFXFile = PfxFile_;
            ENCFile = EncFile_;
            passwordFinkok = passwordFinkok_;
        }

        public bool createPFX()
        {
            bool success = false;
            if (!File.Exists(cer))
            {
                error = "No existe algun certificado en el sistema";
                return false;
            }

            if (!File.Exists(key))
            {
                error = "No existe alguna KEY en el sistema";
                return false;
            }

            if (privateKeyCode.Trim().Equals(""))
            {
                error = "No exite una clave privada aun en el sistema";
                return false;
            }

            Process proc = new Process();
            Process proc2 = new Process();
            Process proc3 = new Process();
            Process proc4 = new Process();

            proc.EnableRaisingEvents = false;
            proc2.EnableRaisingEvents = false;
            proc3.EnableRaisingEvents = false;
            proc4.EnableRaisingEvents = false;


            proc.StartInfo.FileName = "openssl";
            proc.StartInfo.Arguments = "x509 -inform DER -in \"" + cer + "\" -out \"" + CPEMFile + "\"";
            proc.StartInfo.WorkingDirectory = @"C:\Program Files\OpenSSL-Win64\bin";
            proc.Start();
            proc.WaitForExit();



            proc2.StartInfo.FileName = "openssl";
            proc2.StartInfo.Arguments = "pkcs8 -inform DER -in \"" + key + "\" -passin pass:" + privateKeyCode + " -out \"" + KPEMFile + "\"";
            proc2.StartInfo.WorkingDirectory = @"C:\Program Files\OpenSSL-Win64\bin";
            proc2.Start();
            proc2.WaitForExit();


            proc3.StartInfo.FileName = "openssl";
            proc3.StartInfo.Arguments = "pkcs12 -export -out \"" + PFXFile + "\" -inkey \"" + KPEMFile + "\" -in \"" + CPEMFile + "\" -passout pass:" + privateKeyCode;
            proc3.StartInfo.WorkingDirectory = @"C:\Program Files\OpenSSL-Win64\bin";
            proc3.Start();
            proc3.WaitForExit();

            if (passwordFinkok != null)
            {
                proc4.StartInfo.FileName = "openssl";
                proc4.StartInfo.Arguments = "rsa -in \"" + KPEMFile + "\" -des3 -out \"" + ENCFile + "\" -passout pass:" + passwordFinkok;
                proc4.StartInfo.WorkingDirectory = @"C:\Program Files\OpenSSL-Win64\bin";
                proc4.Start();
                proc4.WaitForExit();
            }

      



            proc.Dispose();
            proc2.Dispose();
            proc3.Dispose();
            proc4.Dispose();
            //enviamos mensaje exitoso
            if (System.IO.File.Exists(PFXFile))
                succesMessage = "Se ha creado el archivo PFX ";
            else
            {
                error = "Error al crear el archivo PFX, puede ser que el cer o el key no sean archivos con formato correcto";
                return false;
            }

            if (File.Exists(ENCFile)) succesMessage = "Ya existe el archivo ENC";
            else
            {
                error = "Error al crear el archivo ENC, puede ser que el cer o el key no sean archivos con formato correcto";
                return false;
            }
            //eliminamos los archivos pem
           // if (File.Exists(CPEMFile)) File.Delete(CPEMFile);
           // if (File.Exists(KPEMFile)) File.Delete(KPEMFile);

            success = true;

            return success;
        }
    }
}
