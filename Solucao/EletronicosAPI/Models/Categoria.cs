namespace EletronicosAPI.models;
using System.ComponentModel.DataAnnotations;

public class Categoria
{

    [Key]
    public int CategoriaId {get; set;}
    
    [Required]
    public string? NomeCategoria {get ; set;}

    public DateTime CriadoEm { get; set; } = DateTime.Now;
    
}