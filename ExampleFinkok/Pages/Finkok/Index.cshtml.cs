using FinkokFunctions.Stamp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.Finkok
{
    public class IndexModel : PageModel
    {
        public void OnGet(string message = null)
        {
            ViewData["ResposeInvoice"] = message;
        }
        public IActionResult OnPost()
        {
            MethodStamp stampMethods = new MethodStamp();
            
            string createInvoice = stampMethods.invoice();

            ViewData["ResposeInvoice"] = createInvoice ;

            return RedirectToPage("./Index", new { message = createInvoice});
        }
    }
}
