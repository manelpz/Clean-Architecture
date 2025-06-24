


using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
using Microsoft.EntityFrameworkCore;


//interface data
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    public DbSet<BeerModel> Beers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BeerModel>().ToTable("Beer");
    }

}

