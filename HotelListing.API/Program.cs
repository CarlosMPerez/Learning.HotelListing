using Microsoft.EntityFrameworkCore;
using Serilog;
using HotelListing.API.Data;
using HotelListing.API.Configurations;
using HotelListing.API.Contracts;
using HotelListing.API.Repository;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("HotelListingConnStr");
builder.Services.AddDbContext<HotelListingDbContext>(options => {
    options.UseSqlServer(connStr);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS support
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
});

// Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
