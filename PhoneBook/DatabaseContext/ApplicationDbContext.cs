using Microsoft.EntityFrameworkCore;
using PhoneBook.src.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } 
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories{ get; set; }

    public DbSet<User> Users{ get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=postgres;Port=5432;Database=PhoneBook;Username=pbuser;Password=password");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
        .HasMany(x => x.Subcategories)
        .WithOne(y => y.Category);

        modelBuilder.Entity<Subcategory>()
        .HasMany(x => x.Contacts)
        .WithOne(y => y.Subcategory);

        modelBuilder.Entity<Contact>()
        .HasIndex(e => e.Email)
        .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}