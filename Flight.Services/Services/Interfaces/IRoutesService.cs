using Flight.Domain.Dto;
using Flight.Shared.FlowControl.Model;

namespace Flight.Services.Services.Interfaces;

public interface IRoutesService
{
    Task<Result<IEnumerable<RoutesDto?>>> FindAllAsync();
    Task<Result<RoutesDto>> AddAsync(RoutesDto entity);
    Task<Result<RoutesDto>>  Update(RoutesDto entity);
    Task<Result<RoutesDto?>> FindAsync(Guid id);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<BestRouteDto>> GoodRoutes(string from, string to);
}