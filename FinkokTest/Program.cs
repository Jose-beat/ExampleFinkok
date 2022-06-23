// See https://aka.ms/new-console-template for more information
using Org.BouncyCastle.X509;

Console.WriteLine("Hello, World!");
createStringPEM();
void createStringPEM()
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


}