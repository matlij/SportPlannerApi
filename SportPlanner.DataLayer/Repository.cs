using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportPlanner.DataLayer.Data;
using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications;
using SportPlanner.DataLayer.Specifications.Abstract;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.DataLayer;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly SportPlannerContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<T> _entities;

    public Repository(SportPlannerContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entities = _context.Set<T>();
    }

    public async Task<IEnumerable<Tdto>> GetAll<Tdto>(int limit = 100)
    {
        var entities = await _entities.Take(limit).ToListAsync();

        return _mapper.Map<IEnumerable<Tdto>>(entities);
    }

    public async Task<Tdto?> GetById<Tdto>(GetByIdSpecification<T> spec)
    {
        return (await Get<Tdto>(spec)).SingleOrDefault();
    }

    public async Task<IEnumerable<Tdto>> Get<Tdto>(ISpecification<T> spec, int limit = 100)
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(_entities.AsQueryable(),
                (current, include) => current.Include(include));

        var entities = await queryableResultWithIncludes
            .Where(spec.Filter)
            .Take(limit)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Tdto>>(entities);
    }

    public async Task<(CrudResult, Tdto)> Add<Tdto>(Tdto entityDto, ISpecification<T>? customDuplicateSpec = null)
    {
        var entity = _mapper.Map<T>(entityDto);
        
        var duplicateSpec = customDuplicateSpec ?? new GetByIdSpecification<T>(entity.Id);
        var storedEntity = (await Get<Tdto>(duplicateSpec)).SingleOrDefault();
        if (storedEntity != null)
        {
            return (CrudResult.AlreadyExists, _mapper.Map<Tdto>(storedEntity));
        }
        
        await _entities.AddAsync(entity);

        _context.SaveChanges();

        return (CrudResult.Ok, _mapper.Map<Tdto>(entity));
    }

    public async Task<CrudResult> Update<Tdto>(GetByIdSpecification<T> spec, Tdto entityDto)
    {
        var trackedEntity = await GetById<T>(spec);
        if (trackedEntity is null)
        {
            return CrudResult.NotFound;
        }

        _mapper.Map(entityDto, trackedEntity);
        _context.SaveChanges();
        return CrudResult.Ok;
    }

    public async Task<CrudResult> Delete(Guid id)
    {
        var entity = await GetById<T>(new GetByIdSpecification<T>(id)); //TODO TEST
        if (entity is null)
        {
            return CrudResult.NotFound;
        }

        _entities.Remove(entity);
        _context.SaveChanges();

        return CrudResult.Ok;
    }
}

