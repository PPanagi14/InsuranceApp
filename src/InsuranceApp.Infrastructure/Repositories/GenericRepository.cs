using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Domain.Entities;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext db) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await db.Set<T>().FindAsync([id], ct);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default) =>
        await db.Set<T>().AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct = default) =>
        await db.Set<T>().AddAsync(entity, ct);

    public Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        db.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        db.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }
    public Task RestoreAsync(T entity, CancellationToken ct = default)
    {
        entity.DeletedAtUtc = null;
        entity.UpdatedAtUtc = DateTime.UtcNow;
        db.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

}

