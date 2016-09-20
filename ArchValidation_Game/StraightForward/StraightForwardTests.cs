﻿using FluentAssertions;
using NUnit.Framework;

namespace ArchValidation_Game.StraightForward {
    [TestFixture]
    public class StraightForwardTests {
        [ Test ]
        public void Test01() {
            var hero = new Hero {
                           Weapon = new Excalibur(SwordType.OneHanded)
                       };

            hero.GetStats()
                 .Should()
                 .Be("Stats for hero: \n\tWeapon:9000\n\tShield:\n\tWear:");
        }
    }
}