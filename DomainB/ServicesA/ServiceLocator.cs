using DomainB.Aspects;
using DomainB.ServicesA;

namespace DomainB.ServicesA {
    [ServiceLocatorUsagePolicy]
    public class ServiceLocator {
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
//            var serviceLocator = ServiceLocator.Container;
        }

        [ServiceLocatorPolicy.Allow]
        public void PrintHello(ServiceLocator serviceLocator) {
//            serviceLocator.Get("HelloService");
        }
    }
}

namespace SomeOtherNamespace {
    public class MyOtherSuperService {
        public MyOtherSuperService() {
//            var serviceLocator = ServiceLocator.Container;
        }
    }
}