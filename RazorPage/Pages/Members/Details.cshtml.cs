using ClassLibrary;
using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly IDBConnection dBConnection;

        public Employee? Employee { get; set; }

        [TempData]
        public string Message { get; set; }

        [TempData]
        public string Alert { get; set; }

        public DetailsModel(IDBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }
        public IActionResult OnGet(int Id)
        {
            Employee = dBConnection.GetEmployeeById(Id);

            if (Employee == null)
                return RedirectToPage("/NotFound");

            return Page();
        }


    }
}
