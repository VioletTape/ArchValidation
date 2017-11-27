using System;
using System.Collections.Generic;
using System.Linq;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace ArchValidation_Game.Interfaces.Aspects {
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    [MulticastAttributeUsage(MulticastTargets.Class,
        Inheritance = MulticastInheritance.Multicast)]
    public class BattleStuffAttribute : TypeLevelAspect {
        private readonly List<Type> types;

        public BattleStuffAttribute() {
            types = new List<Type> {
                                       typeof(IWeapon),
                                       typeof(IShield),
                                       typeof(IItem)
                                   };
        }

        public override bool CompileTimeValidate(Type type) {
            var notAllowed = type.GetInterfaces()
                                 .Except(types)
                                 .Select(i => i.Name)
                                 .ToList();

            if (notAllowed.Any()) {
                var messageLocation = MessageLocation.Of(type.GetConstructors().First());
                var messageText =
                    $"There should be only IWeapon/IShield implementation in the {type.Name} class (was found {string.Join(",", notAllowed)})";
                var message = new Message(messageLocation, SeverityType.Error, "f001", messageText, "", "file", null);
                Message.Write(message);
                return false;
            }

            return base.CompileTimeValidate(type);
        }
    }
}