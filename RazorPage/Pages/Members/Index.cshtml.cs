using ClassLibrary;
using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly IDBConnection dBConnection;
        public List<Employee> Employees { get; set; } = new List<Employee>();

        [BindProperty(SupportsGet =true)]
        public string SearchItem { get; set; }

        public IndexModel(IDBConnection localDb)
        {
            this.dBConnection = localDb;
            
        }
        public void OnGet()
        {
            Employees = dBConnection.SearchByItem(SearchItem);
        }

    
    }
}
