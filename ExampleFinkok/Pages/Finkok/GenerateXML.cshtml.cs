using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinkokFunctions.Stamp;

namespace ExampleFinkok.Pages.Finkok
{
    public class GenerateXMLModel : PageModel
    {
        public void OnGet(string message = null)
        {
            ViewData["ResposeXML"] = message;
        }

        public IActionResult OnPost()
        {
            XMLGenerator generatorXML = new XMLGenerator();
            string responseXml = generatorXML.generateXML4();
            ViewData["ResposeXML"] = responseXml;

            return RedirectToPage("./Index", new { message = responseXml });
        }
    }
}
