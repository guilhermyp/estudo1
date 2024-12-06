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
    
    [Required]
    public string? Categoria {get; set;} 
}