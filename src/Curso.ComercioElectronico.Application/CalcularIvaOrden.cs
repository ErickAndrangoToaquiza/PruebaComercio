using Curso.ComercioElectronico.Domain;
using Xunit;

namespace Curso.ComercioElectronico.Application;

public class CalcularIvaOrden {


    [Fact]
    public void calcular_iva()
    {
        
        var porcentajeIva = 12;  
        var orden = OrdenMock.GetOrden(1); 
        Decimal resultadoEsperado = 10*0.12M;
        var servicioCalculoIva = new ServicioCalculoIva(porcentajeIva);

        decimal resultado = servicioCalculoIva.Calcular(orden);

      
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 
    }

    [Fact]
    public void calcular_iva_valores_especificos()
    {
        var orden = OrdenMock.GetOrden(1); 
        var porcentajeIva = 12;  
        
       
        Decimal resultadoEsperado = 2.4M;
        var servicioCalculoIva = new ServicioCalculoIva(porcentajeIva);

     
        decimal resultado = servicioCalculoIva.Calcular(orden);

        
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 


        Decimal resultadoEsperadoProductoSinIva = 1.2M;
        //2.Actuar (Act):  
        resultado = servicioCalculoIva.Calcular(orden);

        //3.Afirmar (Assert):   
        Assert.Equal(resultadoEsperadoProductoSinIva,resultado); 
    }


    [Fact]
    public void calcular_iva_orden_nulo()
    {
        
        var porcentajeIva = 12;
        Orden orden = null;
        var servicioCalculoIva = new ServicioCalculoIva(porcentajeIva);

   
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => servicioCalculoIva.Calcular(orden));
    
    }

    [Fact]
    public void calcular_iva_orden_detalle_sin_productos_aplicar_iva()
    {
        //AAA.
        //1.Organizar (Arrange):  
        var porcentajeIva = 12;
        var orden = OrdenMock.GetOrdenAplicaIva(3,1); 
        var resultadoEsperado =10*0.12M;
        var servicioCalculoIva = new ServicioCalculoIva(porcentajeIva);

        //2.Actuar (Act):  
        decimal resultado = servicioCalculoIva.Calcular(orden);
 
       //3.Afirmar (Assert):  
        Assert.NotNull(resultado);
        Assert.Equal(resultadoEsperado,resultado); 

    }


    [Fact]
    public void calcular_iva_diferentes_porcentajes()
    {
        //AAA.
        //1.Organizar (Arrange):  
        var porcentajeIva = 12;
        var procentajeIvaNuevo = 14;

        var orden = OrdenMock.GetOrden(1); 
        var resultadoEsperado = 10*0.12M;
        var resultadoEsperadoNuevo = 10*0.14M;
        var servicioCalculoIva = new ServicioCalculoIva(porcentajeIva);
        var servicioCalculoIvaNuevo = new ServicioCalculoIva(procentajeIvaNuevo);

         //2.Actuar (Act):  
        decimal resultado = servicioCalculoIva.Calcular(orden);
        decimal resultadoIvaNuevo = servicioCalculoIvaNuevo.Calcular(orden);


       //3.Afirmar (Assert):  
       Assert.Equal(resultadoEsperado,resultado); 
       Assert.Equal(resultadoEsperadoNuevo,resultadoIvaNuevo);  

    }


    [Fact]
    public void calcular_iva_porcentaje_iva_mayor_igual_cero()
    {
        //AAA.
        //1.Organizar (Arrange):
        var porcentajeIva = -12;  
        var orden = OrdenMock.GetOrden(1); 
      
        //2.Actuar (Act):  
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new ServicioCalculoIva(porcentajeIva));
    }
}

public static class OrdenMock {

    public static Orden GetOrden(int numeroProductos=5){
        var orden = new Orden();
        orden.Items = new List<OrdenItem>();

        for (int i = 0; i < numeroProductos; i++)
        {
            var item = new OrdenItem();

            item.Cantidad = (i+1);
            item.Precio  = (i+1) * 10;
            item.AplicarIva = true;

            orden.Items.Add(item);
        }

        return orden;
    }

    public static Orden GetOrdenAplicaIva(int numeroProductos=5,int numeroProductosAlicaIva = 2){
        var orden = new Orden();
        orden.Items = new List<OrdenItem>();

        var contador = 0;
        for (int i = 0; i < numeroProductos; i++)
        {
            var item = new OrdenItem();
            
            item.Cantidad = (i+1);
            item.Precio  = (i+1) * 10;
            if (contador < numeroProductosAlicaIva){
                item.AplicarIva = true;
            }else{
                 item.AplicarIva = false;
            }
            
            orden.Items.Add(item);
            contador++;
        }

        return orden;
    }

}







public class ServicioCalculoIva {

    private double porcentajeIva;

    public ServicioCalculoIva(double porcentajeIva){

        if (porcentajeIva<0){
            throw new ArgumentException($"El porcentaje del IVA no puede ser negativo. Valor actual {porcentajeIva}");
        }

        this.porcentajeIva = porcentajeIva;
    }

    public decimal Calcular(Orden orden){
        //1. Validaciones
        if (orden == null)
            throw new ArgumentNullException(nameof(orden),"Orden es nulo");
 
        if (!orden.Items.Any(p => p.AplicarIva))
            return 0;

        return orden.Items.Where(x => x.AplicarIva).Sum(x => x.Cantidad*x.Precio*
                    ((decimal)this.porcentajeIva/100));

       



    }
}