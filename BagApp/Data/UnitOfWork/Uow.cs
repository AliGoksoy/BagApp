using BagApp.Data.Entities;
using BagApp.Data.Interfaces;
using BagApp.Data.Repositories;
using System.Threading.Tasks;

namespace BagApp.Data.UnitOfWork
{
    public class Uow : IUow
    {

        private readonly BagContext _context;

        public Uow(BagContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
