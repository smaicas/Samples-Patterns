
// Prepare strategies
StrategyOne strategyOne = new();
StrategyTwo strategyTwo = new();

ClassThatUsesStrategy firstClass = new(strategyOne);

firstClass.DoAProcess();

firstClass.Strategy = strategyTwo;
firstClass.DoAProcess();

class ClassThatUsesStrategy
{
    public IMyStrategy Strategy { get; set; }

    public ClassThatUsesStrategy(IMyStrategy strategy) => this.Strategy = strategy;

    public void DoAProcess()
    {
        Console.WriteLine($"Doing a process following current strategy: {Strategy.GetType().FullName}");
        Strategy.ProcedureWithStrategy();
        Console.WriteLine(Strategy.FunctionWithStrategy());
    }
}

interface IMyStrategy
{
    void ProcedureWithStrategy(string[]? args = null);
    string FunctionWithStrategy();
}

class StrategyOne : IMyStrategy
{
    public void ProcedureWithStrategy(string[]? args = null) =>
        Console.WriteLine(
            $"Executed procedure with {nameof(GetType)}. Args: {string.Join(", ", args ?? Array.Empty<string>())}");

    public string FunctionWithStrategy() => $"Executed function with {nameof(GetType)}";
}

class StrategyTwo : IMyStrategy
{
    public void ProcedureWithStrategy(string[]? args = null) =>
        Console.WriteLine(
            $"Executed procedure with {nameof(GetType)}. Args: {string.Join(", ", args ?? Array.Empty<string>())}");

    public string FunctionWithStrategy() => $"Executed function with {nameof(GetType)}";
}