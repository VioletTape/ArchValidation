using PostSharp.Constraints;
using PostSharp.Extensibility;

namespace ArchValidation.NHibernate {
    [MulticastAttributeUsage(MulticastTargets.Assembly)]
    public class NoExplicitOverride : ScalarConstraint {
        public override void ValidateCode(object target) {
            var o = target;
        }
    }


    public class Customer {
       
        public virtual int Age { get; set; }
    }
}