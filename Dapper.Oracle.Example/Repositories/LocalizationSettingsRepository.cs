using System;
using System.Linq;

using Dapper.Oracle.Example.Infrastructure;
using Dapper.Oracle.Example.Interface;
using Dapper.Oracle.Example.Models;

namespace Dapper.Oracle.Example.Repositories
{
    public class LocalizationSettingsRepository : Repository, ILocalizationSettingsRepository
    {
        public LocalizationSettingsRepository(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        public LocalizationSettingsAggregateData GetSettings(Guid entityId, string language)
        {
            return ExecuteTransaction<LocalizationSettingsAggregateData>((connection, transaction) =>
            {
                var settingsAggregateData = connection.Query<LocalizationSettingsAggregateData>(
                    @"SELECT ID, ENTITY_ID, PARENT_ID, LANGUAGE, FORMATTING
                    FROM LOCALIZATION_SETTINGS
                    WHERE ENTITY_ID = :entity_id",
                    new { entity_id = entityId.ToByteArray() },
                    transaction).FirstOrDefault();

                settingsAggregateData.Localizations = connection.Query<LocalizationData>(
                    @"SELECT ID, LOCALIZATION_SETTINGS_ID, KEY, VALUE
                    FROM LOCALE_OVERRIDES
                    WHERE LOCALIZATION_SETTINGS_ID = :id",
                    new { id = settingsAggregateData.Id },
                    transaction).ToList();

                settingsAggregateData.Formattings = connection.Query<FormattingData>(
                    @"SELECT ID, LOCALIZATION_SETTINGS_ID, KEY, VALUE
                    FROM FORMATTING_OVERRIDES
                    WHERE LOCALIZATION_SETTINGS_ID = :id",
                    new { id = settingsAggregateData.Id },
                    transaction).ToList();

                return settingsAggregateData;
            });
        }

        public LocalizationSettingsData GetByEntityId(Guid entityId)
        {
            var localizationSettings = Execute<LocalizationSettingsData>(
                @"SELECT ID, ENTITY_ID, PARENT_ID, LANGUAGE, FORMATTING
                FROM LOCALIZATION_SETTINGS
                WHERE ENTITY_ID = :entity_id",
                new { entity_id = entityId.ToByteArray() }).FirstOrDefault();

            return localizationSettings;
        }

        public void Insert(LocalizationSettingsData localizationSettings)
        {
            Execute(
                @"INSERT INTO LOCALIZATION_SETTINGS (ID, ENTITY_ID, PARENT_ID, LANGUAGE, FORMATTING)
                VALUES (:id, :entity_id, :parent_id, :language, :formatting)",
                new
                {
                    id = localizationSettings.Id.ToByteArray(),
                    entity_id = localizationSettings.EntityId.ToByteArray(),
                    parent_id = localizationSettings.ParentId.ToByteArray(),
                    language = localizationSettings.Language,
                    formatting = localizationSettings.Formatting
                });
        }

        public void Update(LocalizationSettingsData localizationSettings)
        {
            Execute(
                @"UPDATE LOCALIZATION_SETTINGS
                SET
                    ENTITY_ID = :entity_id,
                    PARENT_ID = :parent_id,
                    LANGUAGE = :language,
                    FORMATTING = :formatting
                WHERE ID = :id",
                new
                {
                    id = localizationSettings.Id.ToByteArray(),
                    entity_id = localizationSettings.EntityId.ToByteArray(),
                    parent_id = localizationSettings.ParentId.ToByteArray(),
                    language = localizationSettings.Language,
                    formatting = localizationSettings.Formatting
                });
        }

        public void Delete(Guid entityId)
        {
            Execute(
                @"DELETE FROM LOCALIZATION_SETTINGS WHERE ENTITY_ID = :entity_id",
                new { entity_id = entityId.ToByteArray() });
        }
    }
}
