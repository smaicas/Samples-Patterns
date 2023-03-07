namespace Singleton;

class Program
{
    static void Main(string[] args)
    {
        //ONE Thread program
        NotThreadSafeSingleton notThreadSafeSingletonOne = NotThreadSafeSingleton.GetInstance;

        notThreadSafeSingletonOne.Add(10);

        NotThreadSafeSingleton notThreadSafeSingletonTwo = NotThreadSafeSingleton.GetInstance;

        notThreadSafeSingletonTwo.Add(15);

        notThreadSafeSingletonOne.PrintResult();
        notThreadSafeSingletonTwo.PrintResult();

        // MULTI THREAD program
        // It's supposed NotThreadSafeSingleton used in multi-thread programs
        // can cause multiple instances initialized.

        // Not Thread Safe
        Parallel.Invoke(
            DoSomethingUsingSingleton1,
            DoSomethingUsingSingleton2
        );

        // Thread Safe (manual)
        Parallel.Invoke(
            DoSomethingUsingSingleton3,
            DoSomethingUsingSingleton4
        );

        // Eager Thread Safe (by CLR)
        Parallel.Invoke(
            DoSomethingUsingSingleton5,
            DoSomethingUsingSingleton6
        );

        // Lazy Thread Safe (by Lazy wrap)
        Parallel.Invoke(
            DoSomethingUsingSingleton7,
            DoSomethingUsingSingleton8
        );

        Console.ReadLine();

    }

    private static void DoSomethingUsingSingleton1()
    {
        NotThreadSafeSingleton notThreadSafeSingleton = NotThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton2()
    {
        NotThreadSafeSingleton notThreadSafeSingleton = NotThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton3()
    {
        ThreadSafeSingleton notThreadSafeSingleton = ThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton4()
    {
        ThreadSafeSingleton notThreadSafeSingleton = ThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton5()
    {
        EagerThreadSafeSingleton notThreadSafeSingleton = EagerThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton6()
    {
        EagerThreadSafeSingleton notThreadSafeSingleton = EagerThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton7()
    {
        LazyThreadSafeSingleton notThreadSafeSingleton = LazyThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    private static void DoSomethingUsingSingleton8()
    {
        LazyThreadSafeSingleton notThreadSafeSingleton = LazyThreadSafeSingleton.GetInstance;
        notThreadSafeSingleton.Add(10);
        notThreadSafeSingleton.PrintResult();
    }

    public sealed class NotThreadSafeSingleton
    {
        private static int _result = 0;
        private static int _nInstances = 0;

        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private NotThreadSafeSingleton()
        {
            _nInstances++;
            Console.WriteLine($"Instances {_nInstances}");
        }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static NotThreadSafeSingleton? _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static NotThreadSafeSingleton GetInstance => _instance ??= new NotThreadSafeSingleton();

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public void Add(int value) => _result += value;

        public void Subtract(int value) => _result -= value;

        public void PrintResult() => Console.WriteLine(_result);
    }

    public sealed class ThreadSafeSingleton
    {
        private static int _result = 0;
        private static int _nInstances = 0;

        private ThreadSafeSingleton()
        {
            _nInstances++;
            Console.WriteLine($"Instances {_nInstances}");
        }

        private static ThreadSafeSingleton? _instance;

        private static readonly object InstanceLock = new();

        public static ThreadSafeSingleton GetInstance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (InstanceLock)
                {
                    return _instance ??= new ThreadSafeSingleton();
                }
            }
        }

        public void Add(int value) => _result += value;

        public void Subtract(int value) => _result -= value;
        public void PrintResult() => Console.WriteLine(_result);
    }

    // The Eager loading in the singleton design pattern is nothing but a process in which we need to initialize 
    // the singleton object at the time of application start-up rather than on-demand and keep it ready in 
    // memory to be used in the future. The advantage of using Eager Loading in the Singleton design pattern is 
    // that the CLR (Common Language Runtime) will take care of object initialization and thread safety. 
    // That means we will not require to write any code explicitly for handling the thread safety for a multithreaded environment
    public sealed class EagerThreadSafeSingleton
    {
        private static int _result = 0;
        private static int _nInstances = 0;
        private EagerThreadSafeSingleton()
        {
            _nInstances++;
            Console.WriteLine($"Instances {_nInstances}");
        }

        public static EagerThreadSafeSingleton GetInstance { get; } = new();

        public void Add(int value) => _result += value;
        public void Subtract(int value) => _result -= value;
        public void PrintResult() => Console.WriteLine(_result);
    }

    // The lazy keyword which was introduced as part of .NET Framework 4.0 provides the built-in support for lazy 
    // initialization i.e. on-demand object initialization. If you want to make an object (such as Singleton) 
    // as lazily initialized then you just need to pass the type (Singleton) ) of the object to the lazy keyword as shown below.
    // The most important point that you need to remember is the Lazy<T> objects are by default thread-safe. In a multi-threaded 
    // environment, when multiple threads are trying to access the same Get Instance property at the same time, then the 
    // lazy object will take care of thread safety.
    public sealed class LazyThreadSafeSingleton
    {
        private static int _result = 0;
        private static int _nInstances = 0;
        private LazyThreadSafeSingleton()
        {
            _nInstances++;
            Console.WriteLine($"Instances {_nInstances}");
        }

        private static readonly Lazy<LazyThreadSafeSingleton> InstanceLock =
            new(() => new LazyThreadSafeSingleton());

        public static LazyThreadSafeSingleton GetInstance => InstanceLock.Value;

        public void Add(int value) => _result += value;
        public void Subtract(int value) => _result -= value;
        public void PrintResult() => Console.WriteLine(_result);
    }
}