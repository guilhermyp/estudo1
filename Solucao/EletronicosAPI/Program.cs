using EletronicosAPI.models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();


app.MapPost("Api/Eletronicos/Cadastrar", ([FromBody] Eletronicos eletronicos,
[FromServices] AppDataContext ctx) =>
{
    ctx.Eletronicos.Add(eletronicos);
    ctx.SaveChanges();
    return Results.Created($"/eletronico/{eletronicos.Id}", eletronicos);

});

app.MapGet("/Api/eletronicos/listar", ([FromServices] AppDataContext ctx) =>
{
return Results.Ok(ctx.Eletronicos.ToList());

});

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
    eletronico.Categoria = updatedEletronico.Categoria ?? eletronico.Categoria;

    ctx.SaveChanges();

    return Results.Ok($"Eletrônico com Id {id} atualizado com sucesso.");
});


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


app.MapGet("/Api/eletronicos/buscarPorCategoria/{categoria}", ([FromServices] AppDataContext ctx, string categoria) =>
{
    var eletronicos = ctx.Eletronicos
        .Where(e => e.Categoria!.ToLower() == categoria.ToLower())
        .ToList();

    if (eletronicos.Count == 0)
    {
        return Results.NotFound($"Nenhum eletrônico encontrado na categoria: {categoria}");
    }

    return Results.Ok(eletronicos);
});



app.Run();
