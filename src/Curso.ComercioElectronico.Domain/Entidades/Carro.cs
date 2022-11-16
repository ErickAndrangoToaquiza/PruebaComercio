using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;

public class Carro {
    public Carro()
    {
    }



    public Carro(Guid id){
        this.Id = id;
    }


    [Required]
    public Guid Id {get;set; }
 
    [Required]
    public Guid ClienteId {get;set;}
   
    public virtual Cliente Cliente {get;set;}

    public virtual ICollection<CarroItem> Items {get;set;} = new List<CarroItem>();

    [Required]
    public DateTime Fecha {get;set;}

    public DateTime? FechaAnulacion {get;set;}
    

    [Required]
    public decimal Total {get;set;}

    public string? Observaciones { get;set;}

    [Required]
    public CarroEstado Estado {get;set;}

    public void AgregarItem(CarroItem item){
       
        item.Carro = this;
        Items.Add(item); 
    }
}



