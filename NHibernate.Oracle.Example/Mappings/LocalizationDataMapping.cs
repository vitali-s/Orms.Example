using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Mappings
{
    public class LocalizationDataMapping : ClassMapping<LocalizationData>
    {
        public LocalizationDataMapping()
        {
            Table("LOCALE_OVERRIDES");
            Id(x => x.Id, m => m.Column("ID"));
            Property(x => x.LocalizationSettingsId, m => m.Column("LOCALIZATION_SETTINGS_ID"));
            Property(x => x.Key, m => m.Column("KEY"));
            Property(x => x.Value, m => m.Column("VALUE"));
        }
    }
}