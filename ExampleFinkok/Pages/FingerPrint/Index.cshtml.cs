using FingerPrintReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.FingerPrint
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            GetStateFingerPrint fingerprint = new GetStateFingerPrint();
            string result = fingerprint.stateFingerPrint();
            ViewData["ResposeInvoice"] = result;
        }
    }
}
