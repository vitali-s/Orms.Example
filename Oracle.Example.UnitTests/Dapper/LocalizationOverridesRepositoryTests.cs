using System.Collections.Generic;
using System.Linq;

using Dapper.Oracle.Example.Infrastructure;
using Dapper.Oracle.Example.Interface;
using Dapper.Oracle.Example.Models;
using Dapper.Oracle.Example.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ploeh.AutoFixture;

namespace Oracle.Example.UnitTests.Dapper
{
    [TestClass]
    public class LocalizationOverridesRepositoryTests : UnitTest
    {
        private readonly ILocalizationOverridesRepository _localizationOverridesRepository;
        private readonly ILocalizationSettingsRepository _localizationSettingsRepository;

        public LocalizationOverridesRepositoryTests()
        {
            _localizationOverridesRepository = new LocalizationOverridesRepository(new DatabaseConfiguration());
            _localizationSettingsRepository = new LocalizationSettingsRepository(new DatabaseConfiguration());
        }

        [TestMethod]
        public void Insert_Should_Create_Multiple_New_Overrides()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            var overrides = Fixture.Build<LocalizationData>()
                .With(p => p.LocalizationSettingsId, localizationSettings.Id)
                .CreateMany().ToList();

            _localizationOverridesRepository.Insert(overrides);

            ICollection<LocalizationData> createdOverrides = _localizationOverridesRepository.GetBySettingsId(localizationSettings.Id);

            _localizationOverridesRepository.Delete(localizationSettings.Id);

            Assert.AreEqual(overrides.Count, createdOverrides.Count);
        }
    }
}
