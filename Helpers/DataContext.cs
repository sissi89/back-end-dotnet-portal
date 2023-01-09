namespace WebApi;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;


    using System.Linq;

public class DataContext : DbContext
//gestisce le operazioni sulle entità, traducendole in istruzioni SQL.
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