using System.ComponentModel.DataAnnotations;
namespace Curso.ComercioElectronico.Domain;
public class CarroItem {
    public CarroItem()
    {
    }

    public CarroItem(Guid id){
        this.Id = id;
    }

    [Required]
    public Guid Id {get;set; }

    [Required]
    public int ProductId {get; set;}

    public virtual Producto Product { get; set; }

    [Required]
    public int CarroId {get; set;}
    public bool AplicarIva{get;set;}

    public virtual Carro Carro { get; set; }

    [Required]
    public long Cantidad {get;set;}

    public decimal Precio {get;set;}

    public string? Observaciones { get;set;}
}

public enum CarroEstado{

    Anulada = 0,

    Registrada=1,

    Procesada=2,

    Entregada=3
}
