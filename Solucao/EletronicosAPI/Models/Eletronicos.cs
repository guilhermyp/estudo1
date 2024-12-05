namespace EletronicosAPI.models;

public class Eletronicos
{

    public string Id {get; set;} = Guid.NewGuid().ToString();
    public string? Nome {get ; set;}
    public double? Valor {get; set;}
    public string? Categoria {get; set;} 
}