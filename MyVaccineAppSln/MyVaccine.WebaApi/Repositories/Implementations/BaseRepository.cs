using Microsoft.EntityFrameworkCore;
using MyVaccine.WebaApi.Models;
using MyVaccine.WebaApi.Repositories.Contracts;
using System.Linq.Expressions;

namespace MyVaccine.WebaApi.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly MyVaccineAppDbContext _context;

    public BaseRepository(MyVaccineAppDbContext context)
    {
        _context = context;
    }
    public async Task Add(T entity)
    {
        var UpdatedAt = entity.GetType().GetProperty("UpdatedAt");
        if (UpdatedAt != null) entity.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.UtcNow);

        var CreatedAt = entity.GetType().GetProperty("CreatedAt");
        if (CreatedAt != null) entity.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);

        await _context.AddAsync(entity);
       // _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task AddRange(List<T> entity)
    {
        entity = entity.Select(x =>
        {
            if (x.GetType().GetProperty("UpdatedAt") != null) x.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.UtcNow);
            if (x.GetType().GetProperty("CreatedAt") != null) x.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);
            return x;
        }).ToList();

        _context.AddRange(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRange(List<T> entity)
    {
        _context.RemoveRange(entity);
        await _context.SaveChangesAsync();
    }
    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRange(List<T> entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll()
    {
        var entitySet = _context.Set<T>();
        return entitySet.AsQueryable();
    }

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }


    public Task PatchRange(List<T> entities)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate)
    {
        return GetAll().AsNoTracking().Where(predicate);
    }

    public Task Patch(T entity)
    {
        throw new NotImplementedException();
    }
}
