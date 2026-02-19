namespace CourseManager.Application.Abstractions.Persistence;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<TEntity>> ListAsync(CancellationToken ct);

    Task AddAsync(TEntity entity, CancellationToken ct);
    void Update(TEntity entity);
    void Remove(TEntity entity);

    Task SaveChangesAsync(CancellationToken ct);

}
