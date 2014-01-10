using System;
using System.Collections.Generic;
using System.Linq;

using Dapper.Oracle.Example.Infrastructure;
using Dapper.Oracle.Example.Interface;
using Dapper.Oracle.Example.Models;

namespace Dapper.Oracle.Example.Repositories
{
    public class LocalizationOverridesRepository : Repository, ILocalizationOverridesRepository
    {
        public LocalizationOverridesRepository(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        public ICollection<LocalizationData> GetBySettingsId(Guid settingsId)
        {
            var localizationData = Execute<LocalizationData>(
                @"SELECT ID, LOCALIZATION_SETTINGS_ID, KEY, VALUE
                FROM LOCALE_OVERRIDES
                WHERE LOCALIZATION_SETTINGS_ID = :id",
                new { id = settingsId.ToByteArray() }).ToList();

            return localizationData;
        }

        public void Insert(ICollection<LocalizationData> overrides)
        {
            ExecuteTransaction((connection, transaction) =>
            {
                foreach (var item in overrides)
                {
                    connection.Execute(
                        @"INSERT INTO LOCALE_OVERRIDES (ID, LOCALIZATION_SETTINGS_ID, KEY, VALUE)
                        VALUES (:id, :localization_settings_id, :key, :value)",
                        new
                        {
                            id = item.Id.ToByteArray(),
                            localization_settings_id = item.LocalizationSettingsId.ToByteArray(),
                            key = item.Key,
                            value = item.Value
                        },
                        transaction);
                }
            });
        }

        public void Delete(Guid settingsId)
        {
            Execute(
                @"DELETE FROM LOCALE_OVERRIDES WHERE LOCALIZATION_SETTINGS_ID = :id",
                new { id = settingsId.ToByteArray() });
        }
    }
}
