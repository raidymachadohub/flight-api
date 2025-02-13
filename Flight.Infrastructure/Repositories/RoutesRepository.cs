using System.Linq.Expressions;
using Flight.Domain.Entities;
using Flight.Infrastructure.Context;
using Flight.Infrastructure.Repositories.Interfaces;
using Flight.Shared.FlowControl.Enum;
using Flight.Shared.FlowControl.Model;
using Microsoft.EntityFrameworkCore;

namespace Flight.Infrastructure.Repositories;

public class RoutesRepository(FlightContext context) : IRoutesRepository
{
    public async Task<Result<IEnumerable<RoutesEntity?>>> FindAllAsync()
    {
        var entities = await context.Routes.ToListAsync();
        return Result.Ok<IEnumerable<RoutesEntity?>>(entities);
    }

    public async Task<Result<RoutesEntity>> AddAsync(RoutesEntity entity)
    {
        await context.Routes.AddAsync(entity);
        return Result.Ok(entity);
    }

    public  Result<RoutesEntity> Update(RoutesEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.Routes.Update(entity);
        return Result.Ok(entity);
    }

    public async Task<Result<RoutesEntity?>> FindAsync(Guid id)
    {
        var entity = await context.Routes.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return Result.Ok(entity);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var entity = await context.Routes.FindAsync(id);
        if (entity == null)
            return Result.Fail(new Error(ErrorType.NotFound, "Rota não encontrada"));

        context.Routes.Remove(entity);
        return Result.Ok();
    }

    public async Task<Result<IEnumerable<RoutesEntity?>>> FindFilterAsync(Expression<Func<RoutesEntity, bool>> expression)
    {
        var entities = await context.Routes.Where(expression).ToListAsync();
        return Result.Ok<IEnumerable<RoutesEntity?>>(entities);
    }

    public async Task SaveOrChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}