using System;
using System.Collections.Generic;
using NHibernate.Oracle.Example.Infrastructure;
using NHibernate.Oracle.Example.Interfaces;
using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Repositories
{
    public class LocalizationOverridesRepository : Repository, ILocalizationOverridesRepository
    {
        public ICollection<LocalizationData> GetBySettingsId(Guid settingsId)
        {
            var localizationData = Execute(
                session => session.QueryOver<LocalizationData>()
                    .Where(s => s.LocalizationSettingsId == settingsId)
                    .List());

            return localizationData;
        }

        public void Insert(ICollection<LocalizationData> overrides)
        {
            ExecuteTransaction(session =>
            {
                foreach (var item in overrides)
                {
                    session.SaveOrUpdate(item);
                }
            });
        }

        public void Delete(Guid localizationSettingsId)
        {
            Execute(session =>
            {
                session.CreateQuery("DELETE FROM LocalizationData WHERE LocalizationSettingsId = :id")
                   .SetParameter("id", localizationSettingsId)
                   .ExecuteUpdate();
            });
        }
    }
}
