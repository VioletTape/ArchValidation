using FluentAssertions;
using NUnit.Framework;

namespace ArchValidation.ClassInheritance {
    [TestFixture]
    public class TestsForHero {
        [Test]
        public void Test01() {
            var hero = new Hero {Weapon = new Excalibur(SwordType.OneHanded)};

            hero.GetStats()
                .Should()
                .Be("Stats for hero: \n\tWeapon:9000\n\tShield:\n\tWear:");
        }

        [Test]
        public void Test02() {
            var hero = new Hero
                           {Shield = new CaptAmericasShield()};

            hero.GetStats()
                .Should()
                .Be("Stats for hero: \n\tWeapon:\n\tShield:500\n\tWear:");
        }
    }
}