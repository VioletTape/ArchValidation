namespace ArchValidation.NHibernate.Entities {
/*
 * Consider the following example, when you create class
 * to represent SQL data with NHibernate.
 * In this case properties should be virtual and it's
 * unlikely that someone override those propeties.
 *
 * Validation aspect applied on assembly level, so any
 * type in specific project/namespace will be checked.
 */
    public class Customer : Aspect.NHibernate{
        public string Id;
        public virtual int Age { get; set; }
        public virtual int MarkA { get; }

        public virtual int MarkB {
            set {
                var a = value;
            }
        }

        public string Name { get; set; }
    }

    public class VipCustomer : Customer {
        /*
         * Uncomment following code to see [NoExplicitOverride] validation
         * in action. Attribute applied on assembly level.
         */
//        public override int Age { get; set; }

        public int MarkC { get; set; }
    }

    public class SuperVipCustomer : VipCustomer {
        /*
         * Uncomment following code to see [NoExplicitOverride] validation
         * in action. Attribute applied on assembly level.
         */
//        public override int Age { get; set; }

        public int MarkC { get; set; }
    }


}