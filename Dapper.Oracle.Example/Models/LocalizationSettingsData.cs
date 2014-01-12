using System;

namespace Dapper.Oracle.Example.Models
{
    public class LocalizationSettingsData
    {
        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public Guid ParentId { get; set; }

        public string Language { get; set; }

        public string Formatting { get; set; }
    }
}
