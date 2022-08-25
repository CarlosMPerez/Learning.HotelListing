using HotelListing.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
                new Hotel { Id = 1, Name = "Komodo Bay", Address = "James St", CountryId = 19, Rating = 3.5 },
                new Hotel { Id = 2, Name = "Sun star", Address = "Main St", CountryId = 49, Rating = 3.5 },
                new Hotel { Id = 3, Name = "Lovitz Island", Address = "Lowell St", CountryId = 135, Rating = 4 },
                new Hotel { Id = 4, Name = "Lovell", Address = "Maradona St", CountryId = 135, Rating = 5 },
                new Hotel { Id = 5, Name = "Ritz Madrid", Address = "Gran Via, 15", CountryId = 209, Rating = 1.5 },
                new Hotel { Id = 6, Name = "Carlton Barcelona", Address = "Rambla, 12", CountryId = 209, Rating = 4 },
                new Hotel { Id = 76, Name = "Carlton Bilbao", Address = "Carballeda, 32", CountryId = 209, Rating = 2.5 },
                new Hotel { Id = 8, Name = "Trump Mar-a-Lago", Address = "Motherfucker St.", CountryId = 236, Rating = 4.5 },
                new Hotel { Id = 9, Name = "Maduro's Wonderland", Address = "Tontolaba St.", CountryId = 240, Rating = 4 },
                new Hotel { Id = 10, Name = "Virgin Madonna", Address = "Tarantino St.", CountryId = 242, Rating = 3.5 }
            );
    }
}
