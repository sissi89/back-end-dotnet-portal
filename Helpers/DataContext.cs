namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connessione che va prendere la stringa di connessione dal web api database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
         
    }

    public DbSet<Incarichi> Incarichi { get; set; }
}