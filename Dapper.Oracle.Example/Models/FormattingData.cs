using System;

namespace Dapper.Oracle.Example.Models
{
    public class FormattingData
    {
        public Guid Id { get; set; }

        public Guid LocalizationSettingsId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}