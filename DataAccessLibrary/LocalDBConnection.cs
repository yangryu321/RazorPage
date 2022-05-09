using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class LocalDBConnection : IDBConnection
    {
        private readonly List<Employee> employees = new List<Employee>();

        public LocalDBConnection()
        {
            employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Yang", Email = "yang@gmail.com", Department = Dept.IT,
                    PhotoPath = "Yang.jpg"},

                new Employee() { Id = 2, Name = "Tomo", Email = "tomo@gmail.com", Department = Dept.HR,
                    PhotoPath = "Tomo.jpg"},

                new Employee() { Id = 4, Name = "Misheru", Email = "misheru@gmail.com", Department = Dept.Insurance,
                    PhotoPath = "Misheru.jpg"},

                new Employee() { Id = 3, Name = "Charlie", Email = "charlie@gmail.com", Department = Dept.Payroll,
                    PhotoPath = "Charlie.jpg"},

                new Employee() { Id = 5, Name = "CHOWCHOW", Email = "CHOWCHOW@gmail.com", Department = Dept.Payroll
                   }

            };
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public Employee GetEmployeeById(int Id)
        {
            var result = employees.Where(x => x.Id == Id).FirstOrDefault();

            return result;
        }

        public Employee UpdateEmployee(Employee updatedemployee)
        {

            Employee employee = employees.Where(x => x.Id == updatedemployee.Id).FirstOrDefault();
            employee.Name = updatedemployee.Name;
            employee.Email = updatedemployee.Email;
            employee.Department = updatedemployee.Department;
            employee.PhotoPath = updatedemployee.PhotoPath;


            return employee;

        }

        public Employee CreateEmployee(Employee newemployee)
        {

            if (newemployee != null)
            {
                Employee employee = new Employee()
                {
                    Name = newemployee.Name,
                    Email = newemployee.Email,
                    Department = newemployee.Department,
                    PhotoPath = newemployee.PhotoPath

                };
                employee.Id = employees.Max(x => x.Id) + 1;
                employees.Add(employee);

                return employee;
            }

            return newemployee;

        }

        public Employee DeleteEmployee(int Id)
        {
            Employee employee = employees.Where(x => x.Id == Id).FirstOrDefault();

            if (employee != null)
            {
                employees.Remove(employee);
            }

            return employee;
        }

        public IEnumerable<DptHeadCount> EmployeeCountByDPT(Dept? dept)
        {
            var query = employees;
            if (dept.HasValue)
            {
                query = employees.Where(x => x.Department == dept.Value).ToList();

            }


            return query.GroupBy(x => x.Department)
                .Select(g => new DptHeadCount()
                {
                    Department = g.Key.Value,
                    Count = g.Count()
                }).ToList();

        }

        public List<Employee> SearchByItem(string searchItem)
        {
            //if the search item is null then return the whole list of employees
            if (string.IsNullOrEmpty(searchItem))
            {
                return employees;
            }

            //if not then do the query
            return employees.Where(x => x.Name.ToLower().Contains(searchItem.ToLower())).ToList();
            
        }
    }



}
