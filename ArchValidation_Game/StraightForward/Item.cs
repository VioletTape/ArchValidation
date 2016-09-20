﻿namespace ArchValidation_Game.StraightForward {
    public class Item {}

    public class Potion : Item {}

    public class Weapon : Item {}

    public class Shield : Item {}

    public class Wear : Item {}

    public class Bow : Weapon {}

    public class Dagger : Weapon {}

    public class Wand : Weapon {}

    public class Sword : Weapon {
        public SwordType Type { get; }
        public int Damage { get; protected set; }

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
        public Excalibur(SwordType swordType) : base(swordType) {
            Damage = 9000;
        }
    }

    /*
     * What to do next, if I want to use Shield as Weapon? 
    */
    public class CaptAmericasShield : Shield {
        public int Damage { get; } = 300;
        public int Armor { get; } = 500;        

        public CaptAmericasShield() {}
    }
        
    public class Game {
        
    }

    public class Hero {
        public Weapon Weapon { get; set; }   
        public Shield Shield { get; set; }

        public Wear Protection { get; set; }   
    }
    
}