using FinkokFunctions.CancelStamp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.Finkok
{
    public class CancelationModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CancelationModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void OnGet(string message = null)
        {
            ViewData["ResposeInvoice"] = message;
        }

        public IActionResult OnPost(string uuidToCancel)
        {
            string certifiedFilesRoot = _hostingEnvironment.WebRootPath + "\\certifiedDocs\\";
            string compiledFilesRoot = _hostingEnvironment.WebRootPath + "\\compiledFiles\\";
            CancelStamp cancel = new CancelStamp();

            string uuidCancelled = cancel.cfdiCancelation(uuidToCancel,certifiedFilesRoot,compiledFilesRoot);
            return RedirectToPage("./Cancelation", new { message = uuidCancelled });
        }
    }
}
