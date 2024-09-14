using MobileMekaniko.Data;
using MobileMekaniko.Repository.IRepository;

namespace MobileMekaniko.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _data;

        public UnitOfWork(ApplicationDbContext data)
        {
            _data = data;
            Customer = new CustomerRepository(_data);
        }
        public ICustomerRepository Customer { get; private set; }

      
    }
}
