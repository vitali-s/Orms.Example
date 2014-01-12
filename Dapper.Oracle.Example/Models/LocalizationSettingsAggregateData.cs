using System.Collections.Generic;

namespace Dapper.Oracle.Example.Models
{
    public class LocalizationSettingsAggregateData : LocalizationSettingsData
    {
        public ICollection<LocalizationData> Localizations { get; set; }

        public ICollection<FormattingData> Formattings { get; set; }
    }
}
