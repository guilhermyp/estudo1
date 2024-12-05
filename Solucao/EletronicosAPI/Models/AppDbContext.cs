using Microsoft.EntityFrameworkCore;

namespace EletronicosAPI.models;

public class AppDataContext : DbContext

{
    public DbSet<Eletronicos>? Eletronicos {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = BancoEletronicos.db");
    }

}