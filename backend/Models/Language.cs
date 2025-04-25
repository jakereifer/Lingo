using System;

namespace Lingo.Models
{
    public class Language
    {
        public required Guid ID { get; set; } // Primary key
        public string LanguageName { get; set; } // Required

    }
}