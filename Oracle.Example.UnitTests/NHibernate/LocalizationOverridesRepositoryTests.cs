using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Oracle.Example.Interfaces;
using NHibernate.Oracle.Example.Models;
using NHibernate.Oracle.Example.Repositories;
using Ploeh.AutoFixture;

namespace Oracle.Example.UnitTests.NHibernate
{
    [TestClass]
    public class LocalizationOverridesRepositoryTests : UnitTest
    {
        private readonly ILocalizationOverridesRepository _localizationOverridesRepository;
        private readonly ILocalizationSettingsRepository _localizationSettingsRepository;

        public LocalizationOverridesRepositoryTests()
        {
            _localizationOverridesRepository = new LocalizationOverridesRepository();
            _localizationSettingsRepository = new LocalizationSettingsRepository();
        }

        [TestMethod]
        public void Insert_Should_Create_Multiple_New_Overrides()
        {
            var localizationSettings = Fixture.Build<LocalizationSettingsData>()
                .With(p => p.Formattings, null)
                .With(p => p.Localizations, null)
                .Create();

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
