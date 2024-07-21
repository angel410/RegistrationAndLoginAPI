using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistrationAPI.Data;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Logging.ClearProviders();
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"), o => o.CommandTimeout(180)));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
