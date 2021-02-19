using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Domain.Interfaces;

namespace RVTR.Booking.Context.Repositories
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

   // public virtual async Task DeleteAsync(int id) => Db.Remove(await SelectAsync(id.ToString());

    public virtual async Task InsertAsync(TEntity entry) => await Db.AddAsync(entry).ConfigureAwait(true);


    public virtual async Task<IEnumerable<TEntity>> SelectAsync(string input) => await Db.ToListAsync();

    public virtual void Update(TEntity entry) => Db.Update(entry);
  }
}
