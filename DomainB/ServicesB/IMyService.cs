using DomainB.Aspects;

namespace DomainB.ServicesB {
    /*
     * Validation aspect [NoExplicitInstantiaionPolicy] checks,
     * that classes implementing that interface won't be
     * instantiated explicitly
     */
    [NoExplicitInstantiaionPolicy]
    public interface IMyService
    {
        void Boo();
    }

    public class MyService : IMyService
    {
        public void Boo() { }
    }

    public class MyConsumingService
    {
        public void Foo(MyService myService){
            /*
             * Uncomment folling code to see [NoExplicitInstantiaionPolicy]
             * in action
             */
//            var service = new MyService(); 
            myService.Boo();
        }

        public void Foo2(MyService myService){
            /*
             * Uncomment folling code to see [NoExplicitInstantiaionPolicy]
             * in action
             */
//            var service = new MyService(); 
            myService.Boo();
        }
    }
}