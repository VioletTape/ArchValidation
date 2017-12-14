using DomainA;

namespace ServiceLayer {
    public class EstimationCalculator {
        public EstimationCalculator(IPaymentStrategy paymentStrategy) {
            paymentStrategy.Calculate();
        }
    }

    /*
     * Uncomment following code to see [InternalImplement] constraint
     * for IPaymentStrategy from DomainA in action.
     * You should see Error or Warning, depening of severity type provided
     * for [InternalImplement]
     */
//        public class SomeLocalReimplementation : IPaymentStrategy {
//            public decimal Calculate() {
//                return 100;
//            }
//        }
}