using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Booking.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    public readonly DbSet<TEntity> Db;

    public Repository(BookingContext context)
    {
      Db = context.Set<TEntity>();
    }

    public virtual async Task DeleteAsync(int id) => Db.Remove(await SelectAsync(id));

    public virtual async Task InsertAsync(TEntity entry) => await Db.AddAsync(entry).ConfigureAwait(true);

    public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await Db.ToListAsync();

    public virtual async Task<TEntity> SelectAsync(int id) => await Db.FindAsync(id).ConfigureAwait(true);

    public virtual void Update(TEntity entry) => Db.Update(entry);
  }
}
