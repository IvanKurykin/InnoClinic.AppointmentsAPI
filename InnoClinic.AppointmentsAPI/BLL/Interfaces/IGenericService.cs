namespace BLL.Interfaces;

public interface IGenericService<TEntityDto, TCreateDto, TUpdateDto> where TEntityDto : class where TCreateDto : class where TUpdateDto : class
{
    Task<TEntityDto> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default);
    Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntityDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TEntityDto>> GetAllAsync(CancellationToken cancellationToken = default);
}