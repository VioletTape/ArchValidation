using PostSharp.Constraints;
using PostSharp.Extensibility;

namespace DAL {
    [InternalImplement]
    public interface IUserRepository {
        void GetX();
    }

/*
    public class Customer : NHibernate {
        [NoExplicitOverride]
        public virtual int Age { get; set; }
    }
*/

//    public class UserRepository : IUserRepository {
//        [Internal(Severity = SeverityType.Error)]
//        public void GetX() { }
//    }
}