


using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
using Microsoft.EntityFrameworkCore;


//interface data
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    public DbSet<BeerModel> Beers { get; set; }
    public DbSet<SaleModel> Sales { get; set; }
    public DbSet<ConceptModel> Models { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BeerModel>().ToTable("Beer");
        modelBuilder.Entity<SaleModel>().ToTable("Sale");
        modelBuilder.Entity<ConceptModel>().ToTable("Concept");

        modelBuilder.Entity<SaleModel>()
            .HasMany(c => c.Concepts)
            .WithOne()
            .HasForeignKey(c=>c.IdSale)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

