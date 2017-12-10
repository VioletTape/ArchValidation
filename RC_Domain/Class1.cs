using System;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using RC_Domain;

namespace RC_Domain {
//    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
//    public class ServiceLocatorPolicy : ScalarConstraint {
//        public override void ValidateCode(object target) {
//            var targetType = (Type) target;
//            var usages = ReflectionSearch.GetMethodsUsingDeclaration(typeof(ServiceLocator));
//            Message.Write(targetType, SeverityType.Warning, "re001", targetType.FullName);
//
//            foreach (var usage in usages) {
//                Message.Write(usage.UsingMethod, SeverityType.Error, "re001", "No ServiceLocator anti-pattern in my code!");
//            }
//            base.ValidateCode(target);
//        }
//
//        public override bool ValidateConstraint(object target) {
//            var type = (Type) target;
//            return type.Namespace.Contains("RC_Domain");
//        }
//    }

    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    public class ServiceLocatorPolicy2 : ReferentialConstraint
    {
        public override void ValidateCode(object target, Assembly assembly)
        {
            var targetType = (Type)target;
            var usages = ReflectionSearch.GetMethodsUsingDeclaration(typeof(ServiceLocator));

            Message.Write(targetType, SeverityType.Warning, "re001", ">>>>" + targetType.FullName);

            foreach (var usage in usages)
            {
                Message.Write(usage.UsedDeclaration, SeverityType.Error, "re001", "No ServiceLocator2 anti-pattern in my code!");
            }
            base.ValidateCode(target, assembly);
        }

        public override bool ValidateConstraint(object target)
        {
            return false;

            // namespace restrictions
            // return ((Type)target).Namespace.Contains("RC_Domain"); 
        }
    }

//    [ServiceLocatorPolicy2]
    public class ServiceLocator {
        public static ServiceLocator Container;
    }


    [MulticastAttributeUsage(MulticastTargets.Interface, Inheritance = MulticastInheritance.Multicast)]
    public class IMyServicePolicy : ReferentialConstraint {
        public override void ValidateCode(object target, Assembly assembly) {
            var targetType = (Type) target;
            var usages = ReflectionSearch.GetDerivedTypes(targetType);

            foreach (var usage in usages) {
                var constructorInfos = usage.DerivedType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                foreach (var ctr in constructorInfos) {
                    var illeagalUsage = ReflectionSearch.GetMethodsUsingDeclaration(ctr);

                    foreach (var usageRefs in illeagalUsage)
                        Message.Write(usageRefs.UsingMethod, SeverityType.Error, "re001", "There should be no explicit instanciations for {0}", usage.BaseType);
                }
            }

            base.ValidateCode(target, assembly);
        }

        public override bool ValidateConstraint(object target) {
            return true;
        }
    }

    public class MyService : IMyService {
        public void Boo() { }
    }

    [IMyServicePolicy]
    public interface IMyService {
        void Boo();
    }

    public class MySuperService {
        public MySuperService() {
            var serviceLocator = ServiceLocator.Container;
        }

        public void Foo(MyService myService) {
//            var service = new MyService(); // uncomment for error
            myService.Boo();
        }

        public void Foo2(MyService myService) {
//            var service = new MyService(); // uncomment for error
            myService.Boo();
        }
    }
}

namespace SomeOtherNamespace {
    [ServiceLocatorPolicy2]
    public class MyOtherSuperService {
        public MyOtherSuperService() {
            var serviceLocator = ServiceLocator.Container;
        }
    }
}