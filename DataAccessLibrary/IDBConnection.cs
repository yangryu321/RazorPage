using ClassLibrary;

namespace DataAccessLibrary
{
    public interface IDBConnection
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int Id);
        Employee UpdateEmployee(Employee employee);
        Employee CreateEmployee(Employee newemployee);
        Employee DeleteEmployee(int Id);
        IEnumerable<DptHeadCount> EmployeeCountByDPT(Dept? dept);
        List<Employee> SearchByItem(string searchItem);
    }
}