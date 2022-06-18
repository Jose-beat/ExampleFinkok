using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleFinkok.Pages.Finkok
{
    public class ClientModel : PageModel
    {
        public class Client
        {
            public string Name { get; set; }    

        }
        public void OnPost()
        {

        }
        public void OnGet()
        {
        }
    }
}
