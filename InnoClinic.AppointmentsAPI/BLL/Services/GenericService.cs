using AutoMapper;
using BLL.Exceptions;
using BLL.Helpers.Constants;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services;

public abstract class GenericService<TEntity, TEntityDto, TCreateDto, TUpdateDto> : IGenericService<TEntityDto, TCreateDto, TUpdateDto> where TEntity : class where TEntityDto : class where TCreateDto : class where TUpdateDto : class
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    protected GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TEntityDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
    {
        if (dto is null) throw new BadRequestException<TEntity>(ErrorMessages.RequestDTOCannotBeNull);

        var entity = _mapper.Map<TEntity>(dto);
        var result = await _repository.CreateAsync(entity, cancellationToken);

        if (result is null) throw new BadRequestException<TEntity>(ErrorMessages.FailedToCreateEntity);

        return _mapper.Map<TEntityDto>(result);
    }

    public virtual async Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (dto is null) throw new BadRequestException<TEntity>(ErrorMessages.RequestDTOCannotBeNull);

        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null) throw new NotFoundException<TEntity>(id);

        _mapper.Map(dto, entity);

        var result = await _repository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<TEntityDto>(result);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null) throw new NotFoundException<TEntity>(id);

        await _repository.DeleteAsync(entity, cancellationToken);
    }

    public virtual async Task<TEntityDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);

        if (result == null) throw new NotFoundException<TEntity>(id);

        return _mapper.Map<TEntityDto>(result);
    }

    public virtual async Task<IReadOnlyCollection<TEntityDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyCollection<TEntityDto>>(result);
    }
}