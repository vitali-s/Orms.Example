using System;

using Dapper.Oracle.Example.Models;

namespace Dapper.Oracle.Example.Interface
{
    public interface ILocalizationSettingsRepository
    {
        LocalizationSettingsAggregateData GetSettings(Guid entityId, string language);

        LocalizationSettingsData GetByEntityId(Guid entityId);

        void Insert(LocalizationSettingsData localizationSettings);

        void Delete(Guid entityId);

        void Update(LocalizationSettingsData localizationSettings);
    }
}