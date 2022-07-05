using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XMLFunctions;

namespace ExampleFinkok.Pages.Finkok
{
    public class CreateXMLTModel : PageModel
    {
        public CreateXMLTModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private readonly IWebHostEnvironment _hostingEnvironment;
        public void OnGet(string message)
        {
            ViewData["ResposeXML"] = message;
        }

        public IActionResult OnPost()
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "\\certifiedDocs\\";
            string cfdiFilesRoot = _hostingEnvironment.WebRootPath + "\\cfdiFiles\\";
            string pathCer = certifiedFilesRoot +  "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer";
            string pathKey = certifiedFilesRoot +  "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key";
            string pathXsl = certifiedFilesRoot + "cadenaoriginal_4_0.xslt";
            //  XMLGenerator generatorXML = new XMLGenerator();
            // string responseXml = generatorXML.generateXML4(certifiedFilesRoot);
            // ViewData["ResposeXML"] = responseXml;


            XMLMethods generateXML = new XMLMethods("067", "ESCUELA KEMPER URGATE", "EKU9003173C9", "FELIX MANUEL ANDRADE BALLADO", "AABF800614HI0", "86400", "ordenTrasladoFinkok", "12345678a");

            //string responseString = generateXML.generateOriginalString(certifiedFilesRoot);
            string responseString = generateXML.structureXMLTransfer(pathCer, pathKey, pathXsl, cfdiFilesRoot);


            return RedirectToPage("./CreateXML", new { message = responseString });
        }
    }
}
