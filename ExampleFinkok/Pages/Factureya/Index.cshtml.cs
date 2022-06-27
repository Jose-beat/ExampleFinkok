using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactureyaFunctions.Stamp;

namespace ExampleFinkok.Pages.Factureya
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

        public async Task<IActionResult> OnPost()
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "\\certifiedDocs\\";
            string cfdiFilesRoot = _hostingEnvironment.WebRootPath + "\\cfdiFiles\\";
            string stampedRoot = _hostingEnvironment.WebRootPath + "\\stamped\\";
            
            StampTask fatureya = new StampTask();

            string message = await fatureya.stamp(stampedRoot, cfdiFilesRoot);

             return RedirectToPage("./Index", new { message = message });

        }
    }
}
