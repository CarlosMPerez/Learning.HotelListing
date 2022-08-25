using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelListing.API.Data;

/// <summary>
/// This class is added because NET Core had a problem when scaffolding new controllers
/// after refactoring the solution into separated projects
/// </summary>
public class HotelListingDbContextFactory : IDesignTimeDbContextFactory<HotelListingDbContext>
{
    public HotelListingDbContext CreateDbContext(string[] args)
    {
        IConfiguration cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HotelListingDbContext>();
        var conn = cfg.GetConnectionString("HotelListingConnStr");
        optionsBuilder.UseSqlServer(conn);
        return new HotelListingDbContext(optionsBuilder.Options);
    }
}
