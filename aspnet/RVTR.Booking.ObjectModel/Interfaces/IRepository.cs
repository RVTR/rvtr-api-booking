using System.Collections.Generic;
using System.Threading.Tasks;

namespace RVTR.Booking.ObjectModel.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    Task DeleteAsync(int id);
    Task InsertAsync(TEntity entry);
    Task<IEnumerable<TEntity>> SelectAsync();
    Task<TEntity> SelectAsync(int id);
    void Update(TEntity entry);
  }
}
