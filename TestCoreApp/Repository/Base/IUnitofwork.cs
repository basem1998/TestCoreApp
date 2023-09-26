using TestCoreApp.Models;

namespace TestCoreApp.Repository.Base
{
    public interface IUnitofwork :IDisposable
    {
        Irepository<Category> categories { get; }
        Irepository<Item> items { get; }
        IEmpRepo employees { get; }
        int CommitChanges();
    }
}
