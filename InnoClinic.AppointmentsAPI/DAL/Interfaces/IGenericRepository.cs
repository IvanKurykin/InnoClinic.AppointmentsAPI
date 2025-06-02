namespace DAL.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity,  CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
}