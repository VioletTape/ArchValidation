using ArchValidation_Game.Interfaces.Aspects;

namespace ArchValidation_Game.Interfaces {
    public interface IItem {}

    public interface IPotion : IItem {}

    public interface IWeapon : IItem {}

    public interface IShield : IItem {}
        
    public interface IWear : IItem {}

    public class Bow : IWeapon {}

    public class Dagger : IWeapon {}

    public class Wand : IWeapon {}

    public class Sword : IWeapon {
        public SwordType Type { get; }

        public Sword(SwordType swordType) {
            Type = swordType;
        }
    }

    public enum SwordType {
        OneHanded,
        Bastard,
        TwoHanded
    }

    public class Excalibur : Sword {
        public Excalibur(SwordType swordType) : base(swordType) {}
    }

    /*
     * What to do next, if I want to use Shield as Weapon? 
     * - Add interface
    */

    [ BattleStuff ]
    public class CaptAmericasShield : IShield, IWeapon, IPotion {
        public CaptAmericasShield() {}
    }


    public class Hero {
        public IWeapon Weapon { get; set; }
        public IShield Shield { get; set; }

        public IWear Protection { get; set; }

        public string GetStats() {
            return $"Stats for hero: \n\tWeapon:{Weapon}\n\tShield:{Shield}\n\tWear:{Protection}";
        }
    }
}