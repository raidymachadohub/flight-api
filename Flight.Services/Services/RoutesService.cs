using AutoMapper;
using Flight.Domain.Dto;
using Flight.Domain.Entities;
using Flight.Infrastructure.Repositories.Interfaces;
using Flight.Services.Services.Interfaces;
using Flight.Shared.FlowControl.Enum;
using Flight.Shared.FlowControl.Model;

namespace Flight.Services.Services;

public class RoutesService(IRoutesRepository routesRepository,
                           IMapper mapper) : IRoutesService
{
    
    private readonly BestRouteDto _bestRoute = new();
    public async Task<Result<IEnumerable<RoutesDto?>>> FindAllAsync()
    {
        var result = await routesRepository.FindAllAsync();
        if (!result.Success)
        {
            return Result.Fail<IEnumerable<RoutesDto?>>(result.Error);
        }

        var routeDto = mapper.Map<IEnumerable<RoutesDto>>(result.Value);

        return Result.Ok(routeDto)!;
    }

    public async Task<Result<RoutesDto>> AddAsync(RoutesDto dto)
    {
        try
        {
            var entity = mapper.Map<RoutesEntity>(dto);
            await routesRepository.AddAsync(entity);


            await routesRepository.SaveOrChangesAsync();
            return Result.Ok(dto);
        }
        catch (Exception e)
        {
            return Result.Fail<RoutesDto>(new Error(ErrorType.Internal, e.Message));
        }
    }

    public async Task<Result<RoutesDto>> Update(RoutesDto dto)
    {
        try
        {
            var entity = mapper.Map<RoutesEntity>(dto);

            routesRepository.Update(entity);
            await routesRepository.SaveOrChangesAsync();


            return Result.Ok(dto);
        }
        catch (Exception e)
        {
            return Result.Fail<RoutesDto>(new Error(ErrorType.Internal, e.Message));
        }
    }

    public async Task<Result<RoutesDto?>> FindAsync(Guid id)
    {
        var result = await routesRepository.FindAsync(id);
        if (!result.Success)
        {
            return Result.Fail<RoutesDto?>(result.Error);
        }

        var routeDto = mapper.Map<RoutesDto>(result.Value);

        return Result.Ok(routeDto)!;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            await routesRepository.DeleteAsync(id);
            await routesRepository.SaveOrChangesAsync();
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new Error(ErrorType.Internal, e.Message));
        }
    }

    public async Task<Result<BestRouteDto>> GoodRoutes(string from, string to)
    {
        var routes = await routesRepository.FindFilterAsync(x => x.From.Equals(from));
        if (routes.Success)
        {
            await FindRoutes(routes.Value.ToList()!, to, [], [], 0);
        }

        return Result.Ok(_bestRoute);
    }

    #region Private
    private async Task FindRoutes(ICollection<RoutesEntity> routes, string destination, List<string> visitedAirports, List<RoutesEntity> currentRoute, decimal currentPrice)
    {
        if (routes.Count == 0) return;

        visitedAirports.Add(routes.First().From);

        foreach (var route in routes)
        {
            currentRoute.Add(route);
            var totalPrice = currentPrice + route.Price;

            if (_bestRoute.TotalPrice <= totalPrice) continue;
            if (route.To == destination)
            {
                _bestRoute.TotalPrice = totalPrice;
                    
                var currentRouteDto = mapper.Map<IEnumerable<RoutesDto>>(currentRoute);
                _bestRoute.Routes = new List<RoutesDto>(currentRouteDto);
                continue;
            }

            if (visitedAirports.Contains(route.To)) continue;
            var newRoutes = await routesRepository.FindFilterAsync(x => x.From == route.To);
            if (newRoutes.Success)
            {
                await FindRoutes(newRoutes.Value.ToList()!, destination, visitedAirports, currentRoute, totalPrice);
            }
        }

        currentRoute.RemoveAt(currentRoute.Count - 1);
        visitedAirports.RemoveAt(visitedAirports.Count - 1);
    }

    #endregion
}