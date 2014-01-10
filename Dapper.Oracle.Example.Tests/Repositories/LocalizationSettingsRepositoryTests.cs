using System;

using Dapper.Oracle.Example.Infrastructure;
using Dapper.Oracle.Example.Interface;
using Dapper.Oracle.Example.Models;
using Dapper.Oracle.Example.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ploeh.AutoFixture;

namespace Dapper.Oracle.Example.Tests.Repositories
{
    [TestClass]
    public class LocalizationSettingsRepositoryTests : UnitTest
    {
        private readonly ILocalizationSettingsRepository _localizationSettingsRepository;

        public LocalizationSettingsRepositoryTests()
        {
            _localizationSettingsRepository = new LocalizationSettingsRepository(new DatabaseConfiguration());
        }

        [TestMethod]
        public void GetSettings_Should_Retrun_Settings_Aggregate()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsAggregateData settings = _localizationSettingsRepository.GetSettings(localizationSettings.EntityId, localizationSettings.Language);

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
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsData createdLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(createdLocalizationSettings.EntityId);

            Assert.IsTrue(Comparer.Compare(localizationSettings, createdLocalizationSettings));
        }

        [TestMethod]
        public void Insert_Should_Create_New_LocalizationSettings()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            LocalizationSettingsData createdLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(createdLocalizationSettings.EntityId);

            Assert.IsTrue(Comparer.Compare(localizationSettings, createdLocalizationSettings));
        }

        [TestMethod]
        public void Update_Should_Update_LocalizationSettings_Language_Formatting()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            localizationSettings.Language = Fixture.Create<string>();
            localizationSettings.Formatting = Fixture.Create<string>();

            _localizationSettingsRepository.Update(localizationSettings);

            LocalizationSettingsData updatedLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            _localizationSettingsRepository.Delete(updatedLocalizationSettings.EntityId);

            Assert.IsTrue(Comparer.Compare(localizationSettings, updatedLocalizationSettings));
        }

        [TestMethod]
        public void Delete_Should_Remove_Created_LocalizationSettings()
        {
            var localizationSettings = Fixture.Create<LocalizationSettingsData>();

            _localizationSettingsRepository.Insert(localizationSettings);

            _localizationSettingsRepository.Delete(localizationSettings.EntityId);

            LocalizationSettingsData deletedLocalizationSettings = _localizationSettingsRepository.GetByEntityId(localizationSettings.EntityId);

            Assert.IsNull(deletedLocalizationSettings);
        }
    }
}
