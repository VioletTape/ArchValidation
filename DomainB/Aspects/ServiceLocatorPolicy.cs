using System;
using System.Linq;
using System.Reflection;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace DomainB.Aspects {
/*
 * Set of validation aspects that prevent usage of IoC/DI as
 * ServiceLocator anti-pattern.
 * ServiceLocator can't be passed or called directly outside 
 * allowed places. 
 */
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    public class ServiceLocatorUsagePolicy : ReferentialConstraint {
        public override void ValidateCode(object target, Assembly assembly) {
            var targetType = (Type) target;
            var methodInfos = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var method in methodInfos) {
                var usages = ReflectionSearch.GetMethodsUsingDeclaration(method);

                foreach (var usage in usages) {
                    var parameterInfos = usage.UsingMethod.GetParameters();
                    var any = parameterInfos.Any(info => info.ParameterType == targetType);
                    if (any) {
                        Message.Write(usage.UsingMethod, SeverityType.Error, "re001", "Passing ServiceLocator as paramter is prohibited");
                    }
                }

            }
        }
    }

    [MulticastAttributeUsage(MulticastTargets.Field)]
    public class ServiceLocatorPolicy : ReferentialConstraint {
        public string TargetNamespace;

        public override void ValidateCode(object target, Assembly assembly) {
            var targetType = (FieldInfo) target;
            var usages = ReflectionSearch.GetMethodsUsingDeclaration(targetType);

            foreach (var usage in usages) {
                if (!string.IsNullOrWhiteSpace(TargetNamespace) &&
                    !usage.UsingMethod.DeclaringType.Namespace.Contains(TargetNamespace))
                    continue;

                if(usage.UsingMethod.DeclaringType == targetType.DeclaringType)
                    continue;

                if(usage.UsingMethod.GetCustomAttribute<Allow>() != null)
                    continue;

                Message.Write(usage.UsingMethod, SeverityType.Error, "re001", "Ref: No ServiceLocator anti-pattern in my code!");
            }

            base.ValidateCode(target, assembly);
        }

        public class Allow : Attribute { };
    }
}