using FactureyaFunctions.CancelStamp;
using FactureyaFunctions.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.Factureya
{
    public class CancelModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CancelModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void OnGet(string message = null)
        {
            

            ViewData["ResposeInvoice"] = message;
        }

        public async  Task<IActionResult> OnPost(string uuidToCancel)
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "\\certifiedDocs\\";
            string compiledFilesRoot = _hostingEnvironment.WebRootPath + "\\compiledFiles\\";
            CancelTask cancel = new CancelTask();
            string messageCancel = await cancel.cancelCfdi(uuidToCancel, certifiedFilesRoot, compiledFilesRoot);
            return RedirectToPage("./Index", new { message = messageCancel });
        }
    }
}
