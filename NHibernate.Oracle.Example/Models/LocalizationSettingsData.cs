using System;
using System.Collections.Generic;

namespace NHibernate.Oracle.Example.Models
{
    public class LocalizationSettingsData
    {
        public virtual Guid Id { get; set; }

        public virtual Guid EntityId { get; set; }

        public virtual Guid ParentId { get; set; }

        public virtual string Language { get; set; }

        public virtual string Formatting { get; set; }

        public virtual ICollection<LocalizationData> Localizations { get; set; }

        public virtual ICollection<FormattingData> Formattings { get; set; }
    }
}
