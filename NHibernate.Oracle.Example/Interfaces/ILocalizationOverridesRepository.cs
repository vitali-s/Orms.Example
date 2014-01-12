using System;
using System.Collections.Generic;
using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Interfaces
{
    public interface ILocalizationOverridesRepository
    {
        void Insert(ICollection<LocalizationData> overrides);

        ICollection<LocalizationData> GetBySettingsId(Guid id);

        void Delete(Guid localizationSettingsId);
    }
}