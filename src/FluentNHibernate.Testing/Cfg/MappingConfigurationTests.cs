using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.Fixtures;
using NHibernate.Cfg;
using NUnit.Framework;

namespace FluentNHibernate.Testing.Cfg
{
    [TestFixture]
    public class MappingConfigurationTests
    {
        private Configuration cfg;
        private MappingConfiguration mapping;

        [SetUp]
        public void CreateMappingConfiguration()
        {
            cfg = new Configuration();

            SQLiteConfiguration.Standard
                .InMemory()
                .ConfigureProperties(cfg);

            mapping = new MappingConfiguration();
        }

        [Test]
        public void AddFromAssemblyOfAddsAnyClassMapMappingsToCfg()
        {
            mapping.FluentMappings.AddFromAssemblyOf<Record>();
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldBeGreaterThan(0);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(Record));
        }

        [Test]
        public void AddFromAssemblyAddsAnyClassMapMappingsToCfg()
        {
            mapping.FluentMappings.AddFromAssembly(typeof(Record).Assembly);
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldBeGreaterThan(0);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(Record));
        }

        [Test]
        public void AddAutoMappingAddsAnyAutoMappedMappingsToCfg()
        {
            mapping.AutoMappings.Add(
                AutoPersistenceModel.MapEntitiesFromAssemblyOf<Record>()
                    .Where(type => type == typeof(Record)));
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldBeGreaterThan(0);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(Record));
        }

        [Test]
        public void AddHbmMappingsAddsClasses()
        {
            mapping.HbmMappings.AddClasses(typeof(HbmOne), typeof(HbmTwo));
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldEqual(2);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmOne));
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmTwo));
        }

        [Test]
        public void AddHbmMappingsFromAssemblyOfAddsClasses()
        {
            mapping.HbmMappings.AddFromAssemblyOf<HbmOne>();
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldEqual(2);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmOne));
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmTwo));
        }

        [Test]
        public void AddHbmMappingsFromAssemblyAddsClasses()
        {
            mapping.HbmMappings.AddFromAssembly(typeof(HbmOne).Assembly);
            mapping.Apply(cfg);

            cfg.ClassMappings.Count.ShouldEqual(2);
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmOne));
            cfg.ClassMappings.ShouldContain(c => c.MappedClass == typeof(HbmTwo));
        }

        [Test]
        public void WasUsedIsFalseWhenNothingCalled()
        {
            mapping.WasUsed.ShouldBeFalse();
        }

        [Test]
        public void WasUsedIsTrueWhenAddFromAssemblyOfCalled()
        {
            mapping.FluentMappings.AddFromAssemblyOf<Record>();
            mapping.WasUsed.ShouldBeTrue();
        }

        [Test]
        public void WasUsedIsTrueWhenAddFromAssemblyCalled()
        {
            mapping.FluentMappings.AddFromAssembly(typeof(Record).Assembly);
            mapping.WasUsed.ShouldBeTrue();
        }

        [Test]
        public void WasUsedIsTrueWhenAddAutoMappingsCalled()
        {
            mapping.AutoMappings.Add(
                AutoPersistenceModel.MapEntitiesFromAssemblyOf<Record>()
                    .Where(type => type == typeof(Record)));
            mapping.WasUsed.ShouldBeTrue();
        }

        [Test]
        public void WasUsedIsTrueWhenAddHbmMappingsCalled()
        {
            mapping.HbmMappings.AddClasses(typeof(HbmOne));
            mapping.WasUsed.ShouldBeTrue();
        }

        [Test]
        public void WasUsedIsTrueWhenAddHbmMappingsFromAssemblyCalled()
        {
            mapping.HbmMappings.AddFromAssembly(typeof(HbmOne).Assembly);
            mapping.WasUsed.ShouldBeTrue();
        }

        [Test]
        public void WasUsedIsTrueWhenAddHbmMappingsFromAssemblyOfCalled()
        {
            mapping.HbmMappings.AddFromAssemblyOf<HbmOne>();
            mapping.WasUsed.ShouldBeTrue();
        }
    }
}