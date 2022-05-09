using ClassLibrary;
using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace RazorPage.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly IDBConnection dBConnection;

        [BindProperty]
        public Employee Employee { get; set; }
        public DeleteModel(IDBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }
        public IActionResult OnGet(int Id)
        {
            Employee = dBConnection.GetEmployeeById(Id);
            if(Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            //delete the user by Id
            Employee employee = dBConnection.DeleteEmployee(Employee.Id);
            return RedirectToPage("Index");
        }
    }
}
