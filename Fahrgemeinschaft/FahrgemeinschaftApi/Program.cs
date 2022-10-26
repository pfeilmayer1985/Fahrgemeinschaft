using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Providers;
using TecAlliance.Carpool.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IPassengerBusinessService, PassengerBusinessService>();
builder.Services.AddSingleton<IDriverBusinessService, DriverBusinessService>();
builder.Services.AddSingleton<ICarpoolBusinessService, CarpoolBusinessService>();
builder.Services.AddSingleton<IPassengerDataService, PassengerDataService>();
builder.Services.AddSingleton<IDriverDataService, DriverDataService>();
builder.Services.AddSingleton<ICarpoolDataService, CarpoolDataService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});
builder.Services.AddSingleton<DriverDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<DriverDtoProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
