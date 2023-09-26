using TestCoreApp.Data;
using TestCoreApp.Models;
using TestCoreApp.Repository.Base;

namespace TestCoreApp.Repository
{
    public class Unitofwork : IUnitofwork

       
    {
        public Unitofwork(AppDbContext context)
        {
           _context = context;
            categories=new MainRepository<Category>(context);
            items=new MainRepository<Item>(context);
            employees=new EmpRepo(context);
        }

         private AppDbContext _context { get; set; }
        public Irepository<Category> categories { get; set; }

        public Irepository<Item> items { get; set; }

        public IEmpRepo employees { get; set; }

        public int CommitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
           _context.Dispose();  
        }
    }
}
