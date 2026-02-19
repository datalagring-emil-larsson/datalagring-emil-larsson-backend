using CourseManager.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Infrastructure.Persistance.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly CourseManagerDbContext _context;
    protected readonly DbSet<TEntity> _set;

    public BaseRepository(CourseManagerDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct)
        => _set.FindAsync([id], ct).AsTask();

    public virtual Task<List<TEntity>> ListAsync(CancellationToken ct)
        => _set.ToListAsync(ct);

    public virtual Task AddAsync(TEntity entity, CancellationToken ct)
        => _set.AddAsync(entity, ct).AsTask();

    public virtual void Update(TEntity entity)
        => _set.Update(entity);

    public virtual void Remove(TEntity entity)
        => _set.Remove(entity);

    public virtual Task SaveChangesAsync(CancellationToken ct) =>
        _context.SaveChangesAsync(ct);
}
