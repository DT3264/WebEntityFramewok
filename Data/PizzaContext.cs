using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;

namespace ContosoPizza.Data;

public class PizzaContext : DbContext
{
    public PizzaContext (DbContextOptions<PizzaContext> options)
        : base(options)
    {
    }

    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Sauce> Sauces => Set<Sauce>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    //   optionsBuilder.UseMySQL("server=localhost;database=PizzaDB;user=root;password=0000");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Sauce>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name);
        entity.Property(e => e.IsVegan);
      });

       modelBuilder.Entity<Topping>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name);
        entity.Property(e => e.Calories);
      });
      
      modelBuilder.Entity<Pizza>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name);
        entity.HasOne(e => e.Sauce);
        entity.HasMany(e => e.Toppings).WithMany(e => e.Pizzas);
      });
    }
}