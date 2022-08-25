using Microsoft.EntityFrameworkCore;
using Serilog;
using HotelListing.API.Data;
using HotelListing.API.Data.Models;
using HotelListing.API.Core.Configurations;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelListing.API.Core.Middleware;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("HotelListingConnStr");
builder.Services.AddDbContext<HotelListingDbContext>(options => {
    options.UseSqlServer(connStr);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Custom swagger options, including JWT authentication 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Listing API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Ecample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "0auth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Add CORS support
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
});


//builder.Services.AddApiVersioning(options =>
//{
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.ReportApiVersions = true;
//    options.ApiVersionReader = ApiVersionReader.Combine(
//        new QueryStringApiVersionReader("api-version"), // use version as a parameter of the request
//        new HeaderApiVersionReader("X-Version"), // use version as a parameter in the HEADER of the request
//        new MediaTypeApiVersionReader("ver")
//        );
//});

//builder.Services.AddVersionedApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV";
//    options.SubstituteApiVersionInUrl = true;
//});

// Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy();
});

// Identity Core
builder.Services.AddIdentityCore<APIUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<APIUser>>("HotelListingApi")
    .AddEntityFrameworkStores<HotelListingDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
