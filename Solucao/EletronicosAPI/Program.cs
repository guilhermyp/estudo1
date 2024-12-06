using EletronicosAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();


app.MapPost("/Api/Eletronicos/Cadastrar", ([FromBody] Eletronicos eletronicos, [FromServices] AppDataContext ctx) =>
{
    // Valida se CategoriaId foi fornecido e verifica sua existência
    if (eletronicos.CategoriaId.HasValue)
    {
        var categoria = ctx.Categoria.FirstOrDefault(c => c.CategoriaId == eletronicos.CategoriaId.Value);
        if (categoria == null)
        {
            eletronicos.CategoriaId = null; // Define como null se a categoria não existir
        }
    }

    ctx.Eletronicos.Add(eletronicos);
    ctx.SaveChanges();
    return Results.Created($"/eletronico/{eletronicos.Id}", eletronicos);
});


/*
//CADASTRAR ELETRONICO
app.MapPost("Api/Eletronicos/Cadastrar", ([FromBody] Eletronicos eletronicos,
[FromServices] AppDataContext ctx) =>
{
    ctx.Eletronicos.Add(eletronicos);
    ctx.SaveChanges();
    return Results.Created($"/eletronico/{eletronicos.Id}", eletronicos);

});
*/


//CADASTRAR CATEGORIA
app.MapPost("Api/Eletronicos/CadastrarCategoria", ([FromBody] Categoria categoria,
[FromServices] AppDataContext ctx) =>
{
    ctx.Categoria.Add(categoria);
    ctx.SaveChanges();
    return Results.Created($"/eletronico/{categoria.CategoriaId}", categoria);
});



app.MapGet("/Api/eletronicos/listar", ([FromServices] AppDataContext ctx) =>
{

var eletronicos = ctx.Eletronicos
        .Include(e => e.Categoria) // Inclui a propriedade de navegação Categoria
        .ToList();// ESSAS 3 LINHAS SÓ SÂO NECESSARIAS PORQUE QUERO TRAZER O ID CATEGORIA JUNTO COM SEU NOME

return Results.Ok(ctx.Eletronicos.ToList());

});


app.MapPut("/Api/Eletronicos/Editar/{id}", ([FromServices] AppDataContext ctx, int id, [FromBody] Eletronicos updatedEletronico) =>
{
    // Busca o eletrônico pelo Id
    var eletronico = ctx.Eletronicos.FirstOrDefault(e => e.Id == id);
    if (eletronico == null)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado com o Id: {id}");
    }

    // Atualiza os campos do eletrônico
    eletronico.Nome = updatedEletronico.Nome ?? eletronico.Nome;
    eletronico.Valor = updatedEletronico.Valor ?? eletronico.Valor;

    // Atualiza o CategoriaId, se fornecido
    if (updatedEletronico.CategoriaId.HasValue)
    {
        var categoria = ctx.Categoria.FirstOrDefault(c => c.CategoriaId == updatedEletronico.CategoriaId.Value);
        eletronico.CategoriaId = categoria != null ? updatedEletronico.CategoriaId : null;
    }

    ctx.SaveChanges();
    return Results.Ok($"Eletrônico com Id {id} atualizado com sucesso.");
});

/*
// EDITAR POR ID
app.MapPut("/Api/eletronicos/editar/{id}", ([FromServices] AppDataContext ctx, int id, [FromBody] Eletronicos updatedEletronico) =>
{
    var eletronico = ctx.Eletronicos.FirstOrDefault(e => e.Id == id);
    if (eletronico == null)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado com o Id: {id}");
    }

    // Atualiza os campos do eletrônico
    eletronico.Nome = updatedEletronico.Nome ?? eletronico.Nome;
    eletronico.Valor = updatedEletronico.Valor ?? eletronico.Valor;

    ctx.SaveChanges();

    return Results.Ok($"Eletrônico com Id {id} atualizado com sucesso.");
});
*/

//EXCLUIR POR ID
app.MapDelete("/Api/eletronicos/excluir/{id}", ([FromServices] AppDataContext ctx, int id) =>
{
    var eletronico = ctx.Eletronicos.FirstOrDefault(e => e.Id == id);
    if (eletronico == null)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado com o Id: {id}");
    }

    ctx.Eletronicos.Remove(eletronico);
    ctx.SaveChanges();
    return Results.Ok($"Eletrônico com Id {id} excluído com sucesso.");
});


//FILTRAR PELA CATEGORIA ID
app.MapGet("/Api/eletronicos/buscarPorCategoria/{CategoriaId}", ([FromServices] AppDataContext ctx, int CategoriaId) =>
{
    var eletronicos = ctx.Eletronicos
        .Include(e => e.Categoria) // Carrega os dados da Categoria
        .Where(e => e.CategoriaId == CategoriaId) // Filtra pela categoria
        .ToList();        

    if (eletronicos.Count == 0)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado na categoria: {CategoriaId}");
    }

    return Results.Ok(eletronicos);
});

//FILTRAR POR NOMECATEGORIA

app.MapGet("/Api/eletronicos/buscarPorNome/{nome}", ([FromServices] AppDataContext ctx, string nome) =>
{
    var eletronicos = ctx.Eletronicos
        .Include(e => e.Categoria) // Carrega o relacionamento com a categoria
        .Where(e => e.Nome != null && e.Nome.Contains(nome)) // Filtra pelo nome do eletrônico
        .ToList();

    if (eletronicos.Count == 0)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado com o nome: {nome}");
    }

    return Results.Ok(eletronicos);
});



app.Run();
