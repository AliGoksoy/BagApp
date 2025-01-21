using BagApp.Data.Entities;
using BagApp.Data.Interfaces;
using System.Threading.Tasks;

namespace BagApp.Data.UnitOfWork
{
    public interface IUow
    {

        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChangesAsync();
    }
}
