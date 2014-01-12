using System;

using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Interfaces
{
    public interface ILocalizationSettingsRepository
    {
        LocalizationSettingsData GetByEntityId(Guid empty);

        void Insert(LocalizationSettingsData localizationSettings);

        void Delete(LocalizationSettingsData localizationSettings);
        
        void Update(LocalizationSettingsData localizationSettings);

        LocalizationSettingsData GetSettings(Guid entityId, string language);
    }
}
