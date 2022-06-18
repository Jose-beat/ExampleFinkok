using FinkokFunctions.Stamp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.Finkok
{
    
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IndexModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet(string message = null)
        {
            ViewData["ResposeInvoice"] = message;
        }
        public IActionResult OnPost()
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "/cfdiFiles/";
            MethodStamp stampMethods = new MethodStamp();
            
            string createInvoice = stampMethods.invoice(certifiedFilesRoot);

            ViewData["ResposeInvoice"] = createInvoice ;

            return RedirectToPage("./Index", new { message = createInvoice});
        }
    }
}
