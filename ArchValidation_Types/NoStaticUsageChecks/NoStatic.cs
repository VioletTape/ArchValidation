using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace ArchValidation.NoStaticUsageChecks {
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly)]
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Multicast)]
    public class NoStatic : TypeLevelAspect {
        public override bool CompileTimeValidate(Type type) {
            var isStatic = type.IsAbstract &&
                           type.IsSealed;

            var hasStaticProperties = type.GetProperties(BindingFlags.Static | BindingFlags.Public).Any();

            var methodInfos = type.GetMethods(BindingFlags.Static | BindingFlags.Public);

            var extensionMethods = methodInfos.Where(m => m.GetCustomAttribute<ExtensionAttribute>() != null)
                                              .Select(m => m)
                                              .ToList();

            var hasStaticMethods = methodInfos.Except(extensionMethods).Any();

            if (isStatic && extensionMethods.Any() && !hasStaticMethods && !hasStaticProperties) return false;

            if (isStatic ||hasStaticProperties ||hasStaticMethods) {
                var messageLocation = MessageLocation.Of(type);
                var messageText = $"Looks like you are increasing chaos on a project using static modificator";
                var message = new Message(messageLocation, SeverityType.Error, "f001", messageText, "", "file", null);
                Message.Write(message);
                return false;
            }

            return base.CompileTimeValidate(type);
        }
    }
}