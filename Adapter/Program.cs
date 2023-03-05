// Reference: https://refactoring.guru/es/design-patterns/adapter/csharp/example

// El patrón Adapter actúa como envoltorio entre dos objetos. 
// Atrapa las llamadas a un objeto y las transforma a un formato y una interfaz 
// reconocible para el segundo objeto.

// Ejemplos de uso: El patrón Adapter es muy común en el código C#. Se utiliza muy a menudo en 
// sistemas basados en algún código heredado. En estos casos, los adaptadores crean código heredado 
// con clases modernas.

// Identificación: Adapter es reconocible por un constructor que toma una instancia de distinto 
// tipo de clase abstracta/interfaz. Cuando el adaptador recibe una llamada a uno de sus métodos, 
// convierte los parámetros al formato adecuado y después dirige la llamada a uno o varios métodos
// del objeto envuelto.

Adapter adapter = new(new ClassToAdapt());
adapter.DoAProcess();

public class ClassToAdapt
{
    public void DoASingularProcess() => Console.WriteLine("Doing a singular process");
}

public interface IAdaptTo
{
    void DoAProcess();
}

public class Adapter : IAdaptTo
{
    private readonly ClassToAdapt _classToAdapt;

    public Adapter(ClassToAdapt classToAdapt) => _classToAdapt = classToAdapt;
    public void DoAProcess() => _classToAdapt.DoASingularProcess();
}