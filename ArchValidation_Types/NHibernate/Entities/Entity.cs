using ArchValidation.NHibernate.Aspect;

namespace ArchValidation.NHibernate.Entities {
    public class Customer {
        public string Id;
        public virtual int Age {  get; set; }
        public virtual int MarkA {  get; }
        public virtual int MarkB {
            set {
                var a = value;
            }
        }
        public string Name { get; set; }
    }

    public class VipCustomer : Customer {
//        public override int Age { get; set; }

        public int MarkC { get; set; }
    }

    public class SuperVipCustomer : VipCustomer
    {
//        public override int Age { get; set; }
      
        public int MarkC { get; set; }
    }
}