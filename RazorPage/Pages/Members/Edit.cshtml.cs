using ClassLibrary;
using DataAccessLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Pages.Members
{
    
    public class EditModel : PageModel
    {
        private readonly IDBConnection dBConnection;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Employee Employee { get;  set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public bool Notification { get; set; }
        public string Alert { get; set; }
        public string Message { get; set; }
        public EditModel(IDBConnection dBConnection, IWebHostEnvironment webHostEnvironment)
        {
            this.dBConnection = dBConnection;
            this.webHostEnvironment = webHostEnvironment;
           
        }
        
        public IActionResult OnGet(int Id)
        {
            if(Id > 0)
            {
                Employee = dBConnection.GetEmployeeById(Id);
                if (Employee == null)
                    return RedirectToPage("/NotFound");
            }
            else
            {
                Employee = new Employee();
            }
           
            return Page();

        }

        
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if(Employee.PhotoPath!=null)
                    { 
                        //if the photo path is not null
                        //get the photo path and delete the old pic
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);

                    }
                    Employee.PhotoPath = ProcessUploadedFile();

                }

                

                if (Employee == null)
                {
                    return RedirectToPage("/NotFound");
                }

                if(Employee.Id > 0)
                {
                    Employee = dBConnection.UpdateEmployee(Employee);
                }
                else
                {
                    Employee = dBConnection.CreateEmployee(Employee);
                    return RedirectToPage("Index");
                }
                

                return RedirectToPage("Details", new { id = Employee.Id });

            }
            return Page();




        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if(Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" +Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(filestream);
                }

            }

            return uniqueFileName;
        }

        //If the Onpost() doesnt redirect to other pages, by default it redirects to the template of this page model
        //So it rerenders the page?
        public IActionResult OnPostHandlerForNT(int Id)
        {
            

            if(Notification == true)
            {
                Message = "You have turned on email notification";
                Alert = "success";
            }
            else
            {
                Message = "You have turned off email notification";
                Alert = "warning";
            }

            TempData["message"] = Message;
            TempData["alert"] = Alert;
 
            return RedirectToPage("Details", new { id = Id});
        }

        
    }
}
