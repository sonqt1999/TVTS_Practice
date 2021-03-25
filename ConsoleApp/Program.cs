using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace ConsoleApp
{
    public enum ScopeType
    {
        Singleton,
        Transient,
    }

    //Interface Service
    public interface IClassService
    {
        Guid GetInstanceId();
    }

    public class ClassServiceA : IClassService
    {
        private static Guid _instanceId;

        public ClassServiceA()
        {
            _instanceId = Guid.NewGuid();
        }
        public Guid GetInstanceId()
        {
            Console.WriteLine(_instanceId);
            return _instanceId;
        }
    }
    public class ClassServiceB : IClassService
    {
        private Guid _instanceId;
        public ClassServiceB()
        {
            _instanceId = Guid.NewGuid();
        }
        public Guid GetInstanceId()
        {
            Console.WriteLine(_instanceId);
            return _instanceId;
        }
    }
    public class ClassServiceC : IClassService
    {
        private Guid _instanceId;
        public ClassServiceC()
        {
            _instanceId = Guid.NewGuid();
        }
        public Guid GetInstanceId()
        {
            Console.WriteLine(_instanceId);
            return _instanceId;
        }
    }


    public class ManageClassInstances
    {
        IClassService IG;

        public IClassService Register(ScopeType scopeType,int selectClassService)
        {
            var services = new ServiceCollection();
            IServiceCollection userService;
            switch (selectClassService)
            {
                case 1:
                    if (scopeType == ScopeType.Singleton)
                    {
                        userService = services.AddSingleton<IClassService, ClassServiceA>();
                    }else
                    {
                        userService = services.AddTransient<IClassService, ClassServiceA>();
                    }
                    break;
                case 2:
                    if (scopeType == ScopeType.Singleton)
                    {
                        userService = services.AddSingleton<IClassService, ClassServiceB>();
                    }
                    else
                    {
                        userService = services.AddTransient<IClassService, ClassServiceB>();
                    }
                    break;
                case 3:
                    if (scopeType == ScopeType.Singleton)
                    {
                        userService = services.AddSingleton<IClassService, ClassServiceC>();
                    }
                    else
                    {
                        userService = services.AddTransient<IClassService, ClassServiceC>();
                    }
                    break;
                default:
                    Console.WriteLine("End.");
                    return null;
            }
            var _serviceProvider = services.BuildServiceProvider(true);
            IG = GetService<ClassServiceA>(_serviceProvider);

            return IG;
        }

        public T GetService<T>(IServiceProvider provider)
        {
            return (T)provider.GetService(typeof(T));
        }
    }

    //// STEP 1: Define an interface.
    ///// <summary>
    ///// Defines how a user is notified. 
    ///// </summary>
    ///// public interface IGuidGenerator
    //public interface IGuidGenerator
    //{
    //    Guid GetGuid();
    //}

    //public class GuidGenerator : IGuidGenerator
    //{
    //    private readonly Guid _guid;

    //    public GuidGenerator()
    //    {
    //        _guid = Guid.NewGuid();
    //        Debug.WriteLine($"Calling getGuid: {_guid}");
    //    }

    //    public Guid GetGuid()
    //    {
    //        return _guid;
    //    }
    //}
    //public interface IClassServiceA
    //{
    //    string GetGuidClassService();
    //}
    //public interface IClassServiceB
    //{
    //    string GetGuidClassService();
    //}
    //public interface IClassServiceC
    //{
    //    string GetGuidClassService();
    //}
    //// STEP 2: Implement the interface
    ///// <summary>
    ///// Implementation of INotifier that notifies users by email.
    ///// </summary>
    ///// 
    //public class ClassServiceA : IClassServiceA
    //{
    //    private readonly IGuidGenerator guidGenerator;

    //    public ClassServiceA(IGuidGenerator guidGenerator)
    //    {
    //        this.guidGenerator = guidGenerator;
    //    }
    //    public string GetGuidClassService() => $"{guidGenerator.GetGuid()} - ClassServiceA";
    //}
    //public class ClassServiceB : IClassServiceB
    //{
    //    private readonly IGuidGenerator guidGenerator;

    //    public ClassServiceB(IGuidGenerator guidGenerator)
    //    {
    //        this.guidGenerator = guidGenerator;
    //    }
    //    public string GetGuidClassService() => $"{guidGenerator.GetGuid()} - ClassServiceB";
    //}
    //public class ClassServiceC : IClassServiceC
    //{
    //    private readonly IGuidGenerator guidGenerator;

    //    public ClassServiceC(IGuidGenerator guidGenerator)
    //    {
    //        this.guidGenerator = guidGenerator;
    //    }
    //    public string GetGuidClassService() => $"{guidGenerator.GetGuid()} - ClassServiceC";
    //}
    //// STEP 3: Create a class that requires an implementation of the interface.
    //public class ManageClassInstances
    //{
    //    static IServiceProvider serviceProvider;
    //    public ClassServiceA classServiceA;
    //    public ClassServiceB classServiceB;
    //    public ClassServiceC classServiceC;
    //    public ManageClassInstances(ClassServiceA classServiceA, ClassServiceB classServiceB, ClassServiceC classServiceC)
    //    {
    //        this.classServiceA = classServiceA;
    //        this.classServiceB = classServiceB;
    //        this.classServiceC = classServiceC;
    //    }
    //    public ManageClassInstances()
    //    {


    //    }

    //    public void Register()
    //    {

    //        //classServiceA.GetGuidClassService();
    //        //classServiceB.GetGuidClassService(); 
    //        //classServiceC.GetGuidClassService();
    //    }


    public class Program
    {
        // STEP 4: Create console app to setup DI
        public static void Main(string[] args)
        {
            int n;
            ScopeType idx;
            int selectScopeType,selectClassService;
            Console.WriteLine("Input the times Instance what you want to create: ");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Input the ScopeType what you want to register: ");
            Console.WriteLine("1.--Singleton--");
            Console.WriteLine("2.--Transient--");
            Console.WriteLine("0.--Exit--");
            selectScopeType = int.Parse(Console.ReadLine());
            switch (selectScopeType)
            {
                case 1:
                    idx = ScopeType.Singleton;
                    break;
                case 2:
                    idx = ScopeType.Transient;
                    break;
                default:
                    Console.WriteLine("End.");
                    return;
            }
            Console.WriteLine("Input the Service what you want to get: ");
            Console.WriteLine("1.--ClassServiceA --");
            Console.WriteLine("2.--ClassServiceB --");
            Console.WriteLine("3.--ClassServiceC --");
            Console.WriteLine("0.--Exit--");
            selectClassService = int.Parse(Console.ReadLine());
            ManageClassInstances dm = new ManageClassInstances();
            var a = dm.Register(idx, selectClassService);
            for (int i = 0; i < n; i++)
            {
                a.GetInstanceId();
            }
            Console.ReadLine();
        }
    }
}