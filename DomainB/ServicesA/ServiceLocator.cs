using DomainB.Aspects;
using DomainB.ServicesA;

namespace DomainB.ServicesA {
    /*
     * Aspect [ServiceLocatorUsagePolicy] prevent usage of IoC/DI
     * methods directly in code
     */
    [ServiceLocatorUsagePolicy]
    public class ServiceLocator {
        /*
         * Aspect [ServiceLocatorPolicy] prevent usage of Container directly,
         * because this is dangerous, and considered as anti-pattern
         */
        [ServiceLocatorPolicy]
        public static ServiceLocator Container = new ServiceLocator();

        public void Add(string typeName) { }
        public void Get(string typeName) { }
    }

    public class DependencyManager {
        [ServiceLocatorPolicy.Allow]
        public DependencyManager() {
            var serviceLocator = ServiceLocator.Container;
            serviceLocator.Add("ServiceA");
            serviceLocator.Add("ServiceB");
            serviceLocator.Add("ServiceC");
        }
    }


    public class MySuperService {
        public MySuperService() {
            /*
             * Uncomment to see [ServiceLocatorPolicy] in action
             */
//            var serviceLocator = ServiceLocator.Container;
        }

        [ServiceLocatorPolicy.Allow]
        public void PrintHello(ServiceLocator serviceLocator) {
            /*
             * Uncomment to see [ServiceLocatorPolicy] in action
             */
//            serviceLocator.Get("HelloService");
        }
    }
}

namespace SomeOtherNamespace {
    public class MyOtherSuperService {
        public MyOtherSuperService() {
            /*
             * Uncomment to see [ServiceLocatorPolicy] in action
             * with validation by namespace.
             */
//            var serviceLocator = ServiceLocator.Container;
        }
    }
}