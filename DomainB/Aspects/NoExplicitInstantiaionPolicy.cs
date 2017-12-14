using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace DomainB.Aspects {
/*
 * Validation aspect prevent direct instantiation of any classes
 * that was instrumented. Was developed with IoC/DI frameworks in
 * mind, to force proper usage of IoC/DI.
 */
    [MulticastAttributeUsage(MulticastTargets.Interface
        , Inheritance = MulticastInheritance.Multicast)]
    public class NoExplicitInstantiaionPolicy : ReferentialConstraint
    {
        public override void ValidateCode(object target, Assembly assembly)
        {
            var targetType = (Type)target;
            var usages = ReflectionSearch.GetDerivedTypes(targetType);

            foreach (var usage in usages)
            {
                var constructorInfos = usage.DerivedType
                                            .GetConstructors(BindingFlags.Instance 
                                                             | BindingFlags.Public);

                foreach (var ctr in constructorInfos)
                {
                    var illeagalUsage = ReflectionSearch.GetMethodsUsingDeclaration(ctr);

                    foreach (var usageRefs in illeagalUsage)
                        Message.Write(usageRefs.UsingMethod
                                      , SeverityType.Error
                                      , "re001"
                                      , "There should be no explicit instantiations for {0}"
                                      , usageRefs.UsingMethod.DeclaringType);
                }
            }

            base.ValidateCode(target, assembly);
        }
    }

    public class MyConstraint : ReferentialConstraint {
        public override void ValidateCode(object target, Assembly assembly) {
            base.ValidateCode(target, assembly);
        }

        public override bool ValidateConstraint(object target) {
            return base.ValidateConstraint(target);
        }
    }

    public class TypeLevelConstraint : TypeLevelAspect {
        public override bool CompileTimeValidate(Type type) {
            return base.CompileTimeValidate(type);
        }
    }
}