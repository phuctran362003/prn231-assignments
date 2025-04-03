using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Models;

public class VaccinaCareDbContext : DbContext
{
    public VaccinaCareDbContext()
    {
    }

    public VaccinaCareDbContext(DbContextOptions<VaccinaCareDbContext> options)
        : base(options)
    {
    }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    #region DbSet

    public virtual DbSet<VaccineType> VaccineTypes { get; set; }
    public virtual DbSet<Vaccine> Vaccines { get; set; }
    public virtual DbSet<User> Users { get; set; }

    #endregion
}