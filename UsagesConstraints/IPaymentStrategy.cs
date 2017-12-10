using PostSharp.Constraints;
using PostSharp.Extensibility;

namespace DomainA {
/*
 * Following example is about [InternalImplement]
 *
 * No one can implement interface in other projects
 */



    //
    // Payment strategise should be implemented within
    // the same project, but other project should be able 
    // use interface, without ability to reimplement it.

    [InternalImplement(Severity = SeverityType.Error)]
    public interface IPaymentStrategy {
        decimal Calculate();
    }

    internal class PaymentStrategyA : IPaymentStrategy {
        public decimal Calculate() {
            return 0;
        }
    }

    internal class PaymentStrategyB : IPaymentStrategy {
        public decimal Calculate() {
            return 0;
        }
    }
}