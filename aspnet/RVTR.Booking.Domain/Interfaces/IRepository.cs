using System.Collections.Generic;
using System.Threading.Tasks;

namespace RVTR.Booking.Domain.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
  //  Task DeleteAsync(int id);
    Task InsertAsync(TEntity entry);
    Task<IEnumerable<TEntity>> SelectAsync(string input);

    void Update(TEntity entry);
  }
}
