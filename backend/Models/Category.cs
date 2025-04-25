using System;

namespace backend.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public Guid LanguageID { get; set; }
    }
}