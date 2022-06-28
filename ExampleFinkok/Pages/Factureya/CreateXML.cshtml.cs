using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XMLFunctions;

namespace ExampleFinkok.Pages.Factureya
{
    public class CreateXMLModel : PageModel
    {
        public CreateXMLModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private readonly IWebHostEnvironment _hostingEnvironment;
        public void OnGet(string message = null)
        {
        }

        public IActionResult OnPost()
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "\\certifiedDocs\\";
            string cfdiFilesRoot = _hostingEnvironment.WebRootPath + "\\cfdiFiles\\";
            string pathCer = certifiedFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer";
            string pathKey = certifiedFilesRoot + "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key";
            string pathXsl = certifiedFilesRoot + "cadenaoriginal_4_0.xslt";
            //  XMLGenerator generatorXML = new XMLGenerator();
            // string responseXml = generatorXML.generateXML4(certifiedFilesRoot);
            // ViewData["ResposeXML"] = responseXml;

            XMLMethods generateXML = new XMLMethods("01", "ESCUELA KEMPER URGATE", "EKU9003173C9", "Pablo Neruda Perez", "TES030201001", "72000", "FactureyaCFDI","12345678a");
            

            //string responseString = generateXML.generateOriginalString(certifiedFilesRoot);
            string responseString = generateXML.structureXML(pathCer, pathKey, pathXsl, cfdiFilesRoot);


            return RedirectToPage("./CreateXML", new { message = responseString });
        }
    }
}
