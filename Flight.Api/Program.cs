using Flight.Infrastructure.Ioc;
using Flight.Services.Ioc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options=>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Host.ConfigureServices((hostContext, services) =>
{
    var config = hostContext.Configuration;
    services
        .AddSwagger()
        .AddServices()
        .AddRepositories()
        .AddAutoMapper()
        .AddDbContext(config)
        .AddHealthChecks();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunMigration();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.UseHealthChecks("/health");

app.Run();