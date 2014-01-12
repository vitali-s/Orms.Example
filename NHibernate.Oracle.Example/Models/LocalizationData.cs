using System;

namespace NHibernate.Oracle.Example.Models
{
    public class LocalizationData
    {
        public virtual Guid Id { get; set; }

        public virtual Guid LocalizationSettingsId { get; set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }
    }
}