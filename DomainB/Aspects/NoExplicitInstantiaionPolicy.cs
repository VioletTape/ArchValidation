using System;
using System.Reflection;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace DomainB.Aspects {
    [MulticastAttributeUsage(MulticastTargets.Interface, Inheritance = MulticastInheritance.Multicast)]
    public class NoExplicitInstantiaionPolicy : ReferentialConstraint
    {
        public override void ValidateCode(object target, Assembly assembly)
        {
            var targetType = (Type)target;
            var usages = ReflectionSearch.GetDerivedTypes(targetType);

            foreach (var usage in usages)
            {
                var constructorInfos = usage.DerivedType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                foreach (var ctr in constructorInfos)
                {
                    var illeagalUsage = ReflectionSearch.GetMethodsUsingDeclaration(ctr);

                    foreach (var usageRefs in illeagalUsage)
                        Message.Write(usageRefs.UsingMethod, SeverityType.Error, "re001", "There should be no explicit instantiations for {0}", usageRefs.UsingMethod.DeclaringType);
                }
            }

            base.ValidateCode(target, assembly);
        }
    }
}