using AutoMapper;
using Flight.Domain.Dto;
using Flight.Domain.Entities;
using Flight.Infrastructure.Repositories.Interfaces;
using Flight.Services.Services;
using Flight.Shared.FlowControl.Model;
using Moq;

namespace Flight.Tests.Services;

public class RouteServiceTests
{
    private readonly Mock<IRoutesRepository> _routesRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly RoutesService _routesService;

    public RouteServiceTests()
    {
        _routesService = new RoutesService(_routesRepositoryMock.Object, _mapperMock.Object);
    }


    [Fact]
    public async Task Update_UpdatesRouteSuccessfully()
    {
        // Arrange
        var dto = new RoutesDto { From = "A", To = "B", Price = 150 };
        var entity = new RoutesEntity { Id = Guid.NewGuid(), From = "A", To = "B", Price = 100 };
        _mapperMock.Setup(m => m.Map<RoutesEntity>(dto)).Returns(entity);
        _routesRepositoryMock.Setup(r => r.Update(entity));
        _routesRepositoryMock.Setup(r => r.SaveOrChangesAsync()).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<RoutesDto>(entity)).Returns(dto);

        // Act
        var result = await _routesService.Update(dto);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(dto.Price, result.Value.Price);
    }

    [Fact]
    public async Task FindAsync_ReturnsRouteById()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new RoutesEntity { Id = id, From = "A", To = "B", Price = 100 };
        _routesRepositoryMock.Setup(r => r.FindAsync(id))!.ReturnsAsync(Result.Ok(entity));
        _mapperMock.Setup(m => m.Map<RoutesDto>(entity)).Returns(new RoutesDto { From = "A", To = "B", Price = 100 });

        // Act
        var result = await _routesService.FindAsync(id);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal("A", result.Value.From);
    }
}
