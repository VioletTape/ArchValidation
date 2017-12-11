using DomainB.Aspects;

namespace DomainB.ServicesB {
    public class MyService : IMyService
    {
        public void Boo() { }
    }

    [NoExplicitInstantiaionPolicy]
    public interface IMyService
    {
        void Boo();
    }

    public class MySuperService
    {
        public void Foo(MyService myService){
//            var service = new MyService(); // uncomment for error
            myService.Boo();
        }

        public void Foo2(MyService myService){
//            var service = new MyService(); // uncomment for error
            myService.Boo();
        }
    }
}