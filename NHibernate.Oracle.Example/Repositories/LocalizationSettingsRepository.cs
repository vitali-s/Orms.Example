using System;
using NHibernate.Oracle.Example.Infrastructure;
using NHibernate.Oracle.Example.Interfaces;
using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Repositories
{
    public class LocalizationSettingsRepository : Repository, ILocalizationSettingsRepository
    {
        public LocalizationSettingsData GetSettings(Guid entityId, string language)
        {
            LocalizationSettingsData settingsData = Execute(session => session.QueryOver<LocalizationSettingsData>()
                        .Where(s => s.EntityId == entityId && s.Language == language)
                        .Fetch(s => s.Localizations).Lazy
                        .SingleOrDefault());

            return settingsData;
        }

        public LocalizationSettingsData GetByEntityId(Guid entityId)
        {
            LocalizationSettingsData settingsData = Execute(
                session => session.QueryOver<LocalizationSettingsData>()
                    .Where(s => s.EntityId == entityId)
                    .SingleOrDefault());

            return settingsData;
        }

        public void Insert(LocalizationSettingsData localizationSettings)
        {
            ExecuteTransaction(session => session.Save(localizationSettings));
        }

        public void Delete(LocalizationSettingsData localizationSettings)
        {
            ExecuteTransaction(session => session.Delete(localizationSettings));
        }

        public void Update(LocalizationSettingsData localizationSettings)
        {
            ExecuteTransaction(session => session.Update(localizationSettings));
        }
    }
}
