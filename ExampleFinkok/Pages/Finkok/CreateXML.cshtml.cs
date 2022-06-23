using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinkokFunctions.Stamp;
using XMLFunctions;


namespace ExampleFinkok.Pages.Finkok
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
            ViewData["ResposeXML"] = message;
        }

        public IActionResult OnPost()
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "/cfdiFiles/"; 
          //  XMLGenerator generatorXML = new XMLGenerator();
           // string responseXml = generatorXML.generateXML4(certifiedFilesRoot);
           // ViewData["ResposeXML"] = responseXml;

            XMLMethods generateXML = new XMLMethods();

            string responseString = generateXML.generateOriginalString(certifiedFilesRoot);


            return RedirectToPage("./Index", new { message = responseString });
        }

        
    }
}
