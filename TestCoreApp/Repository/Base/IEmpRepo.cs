using TestCoreApp.Models;

namespace TestCoreApp.Repository.Base
{
    public interface IEmpRepo :Irepository<Employee>
    {
        void setPayRoll(Employee employee);
        decimal getSalary(Employee employee);
    }
}
