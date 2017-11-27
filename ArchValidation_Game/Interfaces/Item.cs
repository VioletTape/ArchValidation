using ArchValidation_Game.Interfaces.Aspects;
using PostSharp.Patterns.Model;

namespace ArchValidation_Game.Interfaces {
    public interface IItem { }


    public interface IPotion : IItem { }

    public interface IWeapon : IItem {
        decimal Damage { get; set; }
    }

    public interface IShield : IItem {
        decimal Defence { get; set; }
    }

    public interface IWear : IItem { }


    public class Bow : IWeapon {
        public decimal Damage { get; set; }
    }

    public class Dagger : IWeapon {
        public decimal Damage { get; set; }
    }

    public class Wand : IWeapon {
        public decimal Damage { get; set; }
    }


    public class Sword : IWeapon {
        public SwordType Type { get; }

        public Sword(SwordType swordType) {
            Type = swordType;
        }

        public decimal Damage { get; set; }
    }

    public class Excalibur : Sword {
        public Excalibur(SwordType swordType) : base(swordType) {
            Damage = 9000;
        }
    }

    public enum SwordType {
        OneHanded,
        Bastard,
        TwoHanded
    }

    /*
     * What to do next, if I want to use Shield as Weapon? 
     * - Add non suitable interface (IPotion)
    */
    [BattleStuff]
    public class CaptAmericasShield : IShield, IWeapon {
        public CaptAmericasShield() {
            Damage = 5000;
            Defence = 9000;
        }

        public decimal Defence { get; set; }
        public decimal Damage { get; set; }
    }


    [NotifyPropertyChanged]
    public class Hero {
        public IWeapon Weapon { get; set; }
        public IShield Shield { get; set; }

        public IWear Protection { get; set; }

        public string GetStats() {
            return $"Stats for hero: \n\tWeapon:{Weapon?.Damage}\n\tShield:{Shield?.Defence}\n\tWear:{Protection}";
        }
    }
}