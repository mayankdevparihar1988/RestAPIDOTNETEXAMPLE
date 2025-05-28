using cleancodeprojectwebapi.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



//Adding Serilog for logging 

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
// Configuring the database context
builder.Services.AddInfrastructureServices(builder.Configuration);




// Scan all currentUs project assemblies	AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
// Scan specific assemblies	AddAutoMapper(typeof(SomeProfile).Assembly, ...)
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Automapper is registed in Application Domain

var app = builder.Build();

// Seeding 

//var scope = app.Services.CreateScope();
//var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
//await seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adding Serilogs

app.UseSerilogRequestLogging();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Custom Middleware for error handling
app.UseExceptionHandlingMiddleware();

app.UseRequestTimeLoggingMiddleware();

app.Run();
