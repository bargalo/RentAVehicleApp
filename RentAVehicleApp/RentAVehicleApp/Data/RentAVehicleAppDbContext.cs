using Microsoft.EntityFrameworkCore;
using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Data;

public class RentAVehicleAppDbContext : DbContext
{
    public DbSet<Rentier> Rentiers => Set<Rentier>();

    public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }
}
