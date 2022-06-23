using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinkokFunctions.Clients;
namespace ExampleFinkok.Pages.Finkok
{
    public class ClientModel : PageModel
    {
        public class Client
        {
              

        }
        public IActionResult OnPost()
        {
            ClientMethods getClients = new ClientMethods();

            string clientDataReuslt = getClients.GetRegistrationClient();
            ViewData["ResposeInvoice"] = clientDataReuslt;

            return RedirectToPage("./Client", new { message = clientDataReuslt });

        }
        public void OnGet(string message = null)
        {
            ViewData["ResposeInvoice"] = message;
        }
    }
}
