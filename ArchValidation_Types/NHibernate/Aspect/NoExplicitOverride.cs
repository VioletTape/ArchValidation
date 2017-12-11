using System;
using System.Linq;
using System.Reflection;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace ArchValidation.NHibernate.Aspect {
    [MulticastAttributeUsage(MulticastTargets.Class | MulticastTargets.Assembly, Inheritance = MulticastInheritance.Multicast)]
    public class NoExplicitOverride : ReferentialConstraint {

        public override void ValidateCode(object target, Assembly assembly) {
            var type = target as Type;
            if(type == null)
                return;

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var virtuals = properties.Where(IsVirtual).Select(p => p.Name).ToList();

            var derrivedTypes = ReflectionSearch.GetDerivedTypes(type, ReflectionSearchOptions.IncludeDerivedTypes);

            foreach (var derrivedType in derrivedTypes) {
                var propertyInfos = derrivedType.DerivedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                var names = propertyInfos.Select(p => p.Name).Intersect(virtuals).Select(n => n).ToList();

                if (names.Any()) {
                    var violations = propertyInfos.Where(p => names.Contains(p.Name)).Select(p => p).ToList();
                    foreach (var vp in violations) {
                        Message.Write(vp, SeverityType.Error, "vir001", $"Property {vp.Name} declared in {vp.DeclaringType} should override base property.");
                    }
                }
            }
        }

        private bool IsVirtual(PropertyInfo property) {
            return property.GetMethod != null && property.GetMethod.IsVirtual
                   || property.SetMethod != null && property.SetMethod.IsVirtual;
        }
    }
}