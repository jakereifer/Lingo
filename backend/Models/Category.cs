using System;

namespace backend.Models
{
    public class Category
    {
        public required Guid ID { get; set; }
        public required string Name { get; set; }
        public string? Notes { get; set; }
        public Guid LanguageID { get; set; }
    }
}