using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDBConnection : IDBConnection
    {
        private readonly AppDbContext appDbContext;

        public SqlDBConnection(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Employee CreateEmployee(Employee newemployee)
        {
            if (newemployee != null)
            {
                appDbContext.Employees.Add(newemployee);
                appDbContext.SaveChanges();
            }

            return newemployee;
        }

        public Employee DeleteEmployee(int Id)
        {
            var employee = appDbContext.Employees.FirstOrDefault(x => x.Id == Id);
            if (employee != null)
            {
                appDbContext.Employees.Remove(employee);
                appDbContext.SaveChanges(true);
            }
            return employee;
        }

        public IEnumerable<DptHeadCount> EmployeeCountByDPT(Dept? dept)
        {

            var query = appDbContext.Employees.ToList().GroupBy(x => x.Department)
                    .Select(x => new DptHeadCount()
                    {
                        Department = x.Key.Value,
                        Count = x.Count()
                    });

            if (dept != null)
            {
                return query.Where(x => x.Department == dept.Value);
            }


            return query;

        }

        public List<Employee> GetAllEmployees()
        {
            //return appDbContext.Employees.ToList();

            //using stored procedure 
            return appDbContext.Employees.FromSqlRaw<Employee>("spGetAllMembers").ToList();
        }

        public Employee GetEmployeeById(int Id)
        {
            //return appDbContext.Employees.Where(x => x.Id == Id).FirstOrDefault();
            //return appDbContext.Employees.Find(Id);
            return appDbContext.Employees.FromSqlRaw<Employee>("spGetMemberById {0}", Id).ToList().FirstOrDefault();
        }

        public List<Employee> SearchByItem(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
                return appDbContext.Employees.ToList();

            return appDbContext.Employees.Where(x => x.Name.Contains(searchItem)).ToList();

        }

        public Employee UpdateEmployee(Employee employee)
        {
            var updatedEmployee = appDbContext.Employees.Attach(employee);
            updatedEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();

            return employee;
        }
    }
}
