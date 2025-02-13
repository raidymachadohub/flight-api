using System.Linq.Expressions;
using Flight.Domain.Entities;
using Flight.Shared.FlowControl.Model;

namespace Flight.Infrastructure.Repositories.Interfaces;

public interface IRoutesRepository
{
    Task<Result<IEnumerable<RoutesEntity?>>> FindAllAsync();
    Task<Result<RoutesEntity>> AddAsync(RoutesEntity entity);
    Result<RoutesEntity> Update(RoutesEntity entity);
    Task<Result<RoutesEntity?>> FindAsync(Guid id);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<IEnumerable<RoutesEntity?>>> FindFilterAsync(Expression<Func<RoutesEntity, bool>> expression);
    Task SaveOrChangesAsync();
}