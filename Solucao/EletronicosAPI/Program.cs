using EletronicosAPI.models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapPost("Api/Eletronicos/Cadastrar", ([FromBody] Eletronicos eletronicos,
[FromServices] AppDataContext ctx) =>
{
    ctx.Eletronicos.Add(eletronicos);
    ctx.SaveChanges();
    return Results.Created($"/eletronico/{eletronicos.Id}", eletronicos);


});



app.Run();
