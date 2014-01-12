using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NHibernate.Oracle.Example.Interfaces;
using NHibernate.Oracle.Example.Models;
using NHibernate.Oracle.Example.Repositories;
using Ploeh.AutoFixture;

namespace Oracle.Example.UnitTests.NHibernate
{
    [TestClass]
    public class LocalizationSettingsRepositoryTests : UnitTest
    {
        private readonly ILocalizationSettingsRepository _localizationSettingsRepository;

        public LocalizationSettingsRepositoryTests()
        {
            _localizationSettingsRepository = new LocalizationSettingsRepository();
        }

        [TestMethod]
        public void GetSettings_Should_Retrun_Settings_Aggregate()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsData settings = _localizationSettingsRepository.GetSettings(localizationSettings.EntityId, localizationSettings.Language);

            Assert.IsNotNull(settings);
        }

        [TestMethod]
        public void GetByEntityId_Should_Return_Null_If_Setting_Does_Not_Created()
        {
            LocalizationSettingsData localizationSettings = _localizationSettingsRepository.GetByEntityId(Guid.Empty);

            Assert.IsNull(localizationSettings);
        }

        [TestMethod]
        public void GetByEntityId_Should_Return_Created_Settings()
        {
            var localizationSettings = Fixture.Build<LocalizationSettingsData>()
                .With(p => p.Formattings, null)
                .With(p => p.Localizations, null)
                .Create();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsData createdLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(createdLocalizationSettings);

            Assert.IsTrue(Comparer.Compare(localizationSettings, createdLocalizationSettings));
        }

        [TestMethod]
        public void Insert_Should_Create_New_LocalizationSettings()
        {
            var localizationSettings = Fixture.Build<LocalizationSettingsData>()
                .With(p => p.Formattings, null)
                .With(p => p.Localizations, null)
                .Create();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsData createdLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(createdLocalizationSettings);

            Assert.IsTrue(Comparer.Compare(localizationSettings, createdLocalizationSettings));
        }

        [TestMethod]
        public void Update_Should_Update_LocalizationSettings_Language_Formatting()
        {
            var localizationSettings = Fixture.Build<LocalizationSettingsData>()
                .With(p => p.Formattings, null)
                .With(p => p.Localizations, null)
                .Create();

            _localizationSettingsRepository.Insert(localizationSettings);

            localizationSettings.Language = Fixture.Create<string>();
            localizationSettings.Formatting = Fixture.Create<string>();

            _localizationSettingsRepository.Update(localizationSettings);

            LocalizationSettingsData updatedLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(updatedLocalizationSettings);

            Assert.IsTrue(Comparer.Compare(localizationSettings, updatedLocalizationSettings));
        }

        [TestMethod]
        public void Delete_Should_Remove_Created_LocalizationSettings()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            _localizationSettingsRepository.Delete(localizationSettings);

            LocalizationSettingsData deletedLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            Assert.IsNull(deletedLocalizationSettings);
        }
    }
}
