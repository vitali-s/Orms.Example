using System;
using System.Collections.Generic;

using Dapper.Oracle.Example.Models;

namespace Dapper.Oracle.Example.Interface
{
    public interface ILocalizationOverridesRepository
    {
        void Insert(ICollection<LocalizationData> overrides);

        ICollection<LocalizationData> GetBySettingsId(Guid settingsId);

        void Delete(Guid settingsId);
    }
}