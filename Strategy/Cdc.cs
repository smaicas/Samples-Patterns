namespace CdeCImpl;

// <summary>
// Author: @Cdecompilador
// Implementación del patrón strategy con structs y tratando de evitar dynamic dispatch.
// el dynamic dispatch apenas reduce el rendimiento en mis benchmarks
//para resultados pequeños está apenas unos picosegundos por encima normalemente
// </summary>

public interface IStrategy
{
    void Procedure();
}

/// Ejecutor de las estrategias que NO usa dynamic dispatch haciendo uso de
/// generics (los cuales se resuelven en tiempo de compilación), con la
/// desventaja de perder una estrategia por defecto o global
public struct StratExecutor
{
    public static StrategyOne StrategyOne = new();
    public static StrategyTwo StrategyTwo = new();

    public void RunProcedure<TStrategy>(TStrategy strategy)
        where
        TStrategy : IStrategy => strategy.Procedure();
}

public struct StratExecutor2
{
    public static StrategyOne StrategyOne = new();
    public static StrategyTwo StrategyTwo = new();

    public void RunProcedure(ref IStrategy strategy) => strategy.Procedure();
}

public class ClassThatUsesStrategy
{
    public StratExecutor Executor { get; set; }

    public ClassThatUsesStrategy() => Executor = new StratExecutor();

    /// CASO BUENO: Para el caso de que la estrategia dependa unicamente de
    /// condiciones es mejor
    public void DoSomething()
    {
        if (true)
        {
            Executor.RunProcedure<StrategyOne>(StratExecutor.StrategyOne);
        }
        else
        {
            Executor.RunProcedure(StratExecutor.StrategyTwo);
        }
    }

    /// CASO MALO: La estrategia simplemente se necesita la global 
    /// independientemente del contexto, aquí el rendimiento acaba siendo el 
    /// mismo ya que el dynamic dispatch se hace con la variable local al
    /// inicio
    public void DoSomething2()
    {
        IStrategy selecctedStrat = StratExecutor.StrategyOne;

        if (true)
        {
            selecctedStrat = StratExecutor.StrategyTwo;
        }

        Executor.RunProcedure(selecctedStrat);
    }
}

public struct StrategyOne : IStrategy
{
    public void Procedure() => Console.WriteLine("Hello from One");
}

public struct StrategyTwo : IStrategy
{
    public void Procedure() => Console.WriteLine("Hello from Two");
}
