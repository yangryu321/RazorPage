using ClassLibrary;
using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;

namespace RazorPage.ViewComponents
{
    public class HeadCountViewComponent :ViewComponent
    {
        private readonly IDBConnection dBConnection;

        public HeadCountViewComponent(IDBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }


        public IViewComponentResult Invoke(Dept? dept = null)
        {
            //get the head count of all the departments
            var result = dBConnection.EmployeeCountByDPT(dept);
            return View(result);
        }
    }
}
