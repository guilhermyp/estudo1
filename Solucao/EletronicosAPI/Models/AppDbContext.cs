using Microsoft.EntityFrameworkCore;

namespace EletronicosAPI.models;

public class AppDataContext : DbContext

{
    public DbSet<Eletronicos> Eletronicos {get; set;} = null!;

    public DbSet<Categoria> Categoria {get; set;} = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = BancoEletronicos.db");
    }

}