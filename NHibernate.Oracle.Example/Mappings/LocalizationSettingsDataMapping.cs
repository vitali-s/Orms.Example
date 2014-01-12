using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Oracle.Example.Models;

namespace NHibernate.Oracle.Example.Mappings
{
    public class LocalizationSettingsDataMapping : ClassMapping<LocalizationSettingsData>
    {
        public LocalizationSettingsDataMapping()
        {
            Table("LOCALIZATION_SETTINGS");
            Id(x => x.Id, m => m.Column("ID"));
            Property(x => x.EntityId, m => m.Column("ENTITY_ID"));
            Property(x => x.ParentId, m => m.Column("PARENT_ID"));
            Property(x => x.Language, m => m.Column("LANGUAGE"));
            Property(x => x.Formatting, m => m.Column("FORMATTING"));

            Bag(
                x => x.Localizations,
                m =>
                {
                    m.Key(k => k.Column("LOCALIZATION_SETTINGS_ID"));
                    m.Cascade(Cascade.None);
                    m.Lazy(CollectionLazy.NoLazy);
                },
                ce => ce.OneToMany());
        }
    }
}