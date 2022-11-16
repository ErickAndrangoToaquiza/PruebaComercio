using Curso.ComercioElectronico.Domain;
using Xunit;

namespace Curso.ComercioElectronico.Application;

public class CalcularIvaCarro {


    [Fact]
    public void calcular_iva()
    {
        
        var porcentajeIva = 12;  
        var carro = CarroMock.GetCarro(1); 
        Decimal resultadoEsperado = 10*0.12M;
        var servicioCalculoIvaCarro = new ServicioCalculoIvaCarro(porcentajeIva);

        decimal resultado = servicioCalculoIvaCarro.Calcular(carro);

      
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 
    }

    [Fact]
    public void calcular_iva_valores_especificos()
    {
        var carro = CarroMock.GetCarro(1); 
        var porcentajeIva = 12;  
        
       
        Decimal resultadoEsperado = 2.4M;
        var servicioCalculoIvaCarro = new ServicioCalculoIvaCarro(porcentajeIva);

     
        decimal resultado = servicioCalculoIvaCarro.Calcular(carro);

        
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 


        Decimal resultadoEsperadoProductoSinIva = 1.2M;
      
        resultado = servicioCalculoIvaCarro.Calcular(carro);

         
        Assert.Equal(resultadoEsperadoProductoSinIva,resultado); 
    }


    [Fact]
    public void calcular_iva_orden_nulo()
    {
        
        var porcentajeIva = 12;
        Carro carro = null;
        var servicioCalculoIvaCarro = new ServicioCalculoIvaCarro(porcentajeIva);

   
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => servicioCalculoIvaCarro.Calcular(carro));
    
    }

    [Fact]
    public void calcular_iva_orden_detalle_sin_productos_aplicar_iva()
    {
        //AAA.
        //1.Organizar (Arrange):  
        var porcentajeIva = 12;
        var carro = CarroMock.GetCarroAplicaIva(3,1); 
        var resultadoEsperado =10*0.12M;
        var servicioCalculoIvaCarro = new ServicioCalculoIvaCarro(porcentajeIva);

        //2.Actuar (Act):  
        decimal resultado = servicioCalculoIvaCarro.Calcular(carro);
 
       //3.Afirmar (Assert):  
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 

    }


    [Fact]
    public void calcular_iva_diferentes_porcentajes()
    {
        
        var porcentajeIva = 12;
        var procentajeIvaNuevo = 14;

        var carro = CarroMock.GetCarro(1); 
        var resultadoEsperado = 10*0.12M;
        var resultadoEsperadoNuevo = 10*0.14M;
        var servicioCalculoIvaCarro = new ServicioCalculoIvaCarro(porcentajeIva);
        var servicioCalculoIvaNuevo = new ServicioCalculoIvaCarro(procentajeIvaNuevo);

         //2.Actuar (Act):  
        decimal resultado = servicioCalculoIvaCarro.Calcular(carro);
        decimal resultadoIvaNuevo = servicioCalculoIvaNuevo.Calcular(carro);


       //3.Afirmar (Assert):  
       Assert.Equal(resultadoEsperado,resultado); 
       Assert.Equal(resultadoEsperadoNuevo,resultadoIvaNuevo);  

    }


    [Fact]
    public void calcular_iva_porcentaje_iva_mayor_igual_cero()
    {
        
        var porcentajeIva = -12;  
        var carro = CarroMock.GetCarro(1); 
      
        //2.Actuar (Act):  
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new ServicioCalculoIvaCarro(porcentajeIva));
    }
}

public static class CarroMock {

    public static Carro GetCarro(int numeroProductos=5){
        var carro = new Carro();
        carro.Items = new List<CarroItem>();

        for (int i = 0; i < numeroProductos; i++)
        {
            var item = new CarroItem();

            item.Cantidad = (i+1);
            item.Precio  = (i+1) * 10;
            item.AplicarIva = true;

            carro.Items.Add(item);
        }

        return carro;
    }

    public static Carro GetCarroAplicaIva(int numeroProductos=5,int numeroProductosAlicaIva = 2){
        var carro = new Carro();
        carro.Items = new List<CarroItem>();

        var contador = 0;
        for (int i = 0; i < numeroProductos; i++)
        {
            var item = new CarroItem();
            
            item.Cantidad = (i+1);
            item.Precio  = (i+1) * 10;
            if (contador < numeroProductosAlicaIva){
                item.AplicarIva = true;
            }else{
                 item.AplicarIva = false;
            }
            
            carro.Items.Add(item);
            contador++;
        }

        return carro;
    }

}

public class ServicioCalculoIvaCarro {

    private double porcentajeIva;

    public ServicioCalculoIvaCarro(double porcentajeIva){

        if (porcentajeIva<0){
            throw new ArgumentException($"El porcentaje del IVA no puede ser negativo. Valor actual {porcentajeIva}");
        }

        this.porcentajeIva = porcentajeIva;
    }

    public decimal Calcular(Carro carro){
        
        if (carro == null)
            throw new ArgumentNullException(nameof(carro),"El carro es nulo");
 
        if (!carro.Items.Any(p => p.AplicarIva))
            return 0;

        return carro.Items.Where(x => x.AplicarIva).Sum(x => x.Cantidad*x.Precio*
                    ((decimal)this.porcentajeIva/100));

       



    }
}