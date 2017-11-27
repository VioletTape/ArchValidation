using FluentAssertions;
using NUnit.Framework;

namespace ArchValidation_Game.Interfaces {
    [TestFixture]
    public class StraightForwardTests {
        [Test]
        public void Test01() {
            var     hero = new Hero {Weapon = new CaptAmericasShield()};

            hero.GetStats()
                .Should()
                .Be("Stats for hero: \n\tWeapon:5000\n\tShield:\n\tWear:");
        }

        [Test]
        public void Test02() {
            var     hero = new Hero {Shield = new CaptAmericasShield()};

            hero.GetStats()
                .Should()
                .Be("Stats for hero: \n\tWeapon:\n\tShield:9000\n\tWear:");
        }
    }
}