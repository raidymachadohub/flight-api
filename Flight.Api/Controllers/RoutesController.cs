using Flight.Domain.Dto;
using Flight.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutesController(IRoutesService routesService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoutes()
    {
        var routes = await routesService.FindAllAsync();
        return Ok(routes);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoute(Guid id)
    {
        var route = await routesService.FindAsync(id);
        return Ok(route);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddRoute(RoutesDto routeDto)
    {
        var result = await routesService.AddAsync(routeDto);
        return Ok(result);
    }
    
    [HttpPut]
    public IActionResult UpdateRoute(RoutesDto routeDto)
    {
        var result = routesService.Update(routeDto);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(Guid id)
    {
        var result = await routesService.DeleteAsync(id);
        return Ok(result);
    }
    
    [HttpGet("/good-routes/{from}/{to}")]
    public async Task<IActionResult> GoodRoutes(string from, string to)
    {
        return Ok(await routesService.GoodRoutes(from, to));
    }
}