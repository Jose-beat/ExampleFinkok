using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinkokFunctions.Stamp;
namespace ExampleFinkok.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            MethodStamp stampMethods = new MethodStamp();
            XMLGenerator createXml = new XMLGenerator();
            //string createInvoice = stampMethods.invoice();
            //ViewData["ResposeInvoice"] = createInvoice ;
            ///string createResponse = createXml.generateXML4();
            //ViewData["ResposeInvoice"] = createResponse;
        }
        public void OnPost()
        {
            
            
        }
    }
}