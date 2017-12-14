using FluentAssertions;
using NUnit.Framework;
using PostSharp.Constraints;
using PostSharp.Extensibility;
using StructureMap;

namespace DomainA {
    public class MyServiceForIoCUsage {
        public string Name { get; }

        /*
         * Applying [Internal] to the constructor, makes it internal within
         * assembly. Technically it's available for 3rd party elements, that
         * requires public ctor/method/property, but we don't want to expose it
         */ 
        [Internal(Severity = SeverityType.Error)]
        public MyServiceForIoCUsage(string name) {
            Name = name;
        }
    }

    public class Registry {
        public Container container;

        public void Setup() {
            container = new Container(x => x.For<MyServiceForIoCUsage>()
                                            .Use<MyServiceForIoCUsage>()
                                            .Ctor<string>()
                                            .Is("hey!"));
        }
    }

    [TestFixture]
    public class Test {
        [Test]
        public void CheckInstantiation() {
            // arrange 
            var registry = new Registry();

            // act
            registry.Setup();

            // assert
            registry.container.GetInstance<MyServiceForIoCUsage>()
                    .Should()
                    .NotBeNull();
        }
    }
}