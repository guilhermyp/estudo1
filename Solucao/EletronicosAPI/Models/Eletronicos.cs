namespace EletronicosAPI.models;
using System.ComponentModel.DataAnnotations;

public class Eletronicos
{

    [Key]
     public int Id {get; set;}
    
    [Required]
    public string? Nome {get ; set;}
    
    [Required]
    public double? Valor {get; set;}

    public int? CategoriaId { get; set; } // Chave estrangeira

    public Categoria? Categoria { get; set; } // Relacionamento

    public DateTime CriadoEm { get; set; } = DateTime.Now;


}