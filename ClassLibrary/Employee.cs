using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name should contain at least 3 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office email")]
        public string Email { get; set; } = string.Empty;

        public string? PhotoPath { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public Dept? Department { get; set; }




    }
}
