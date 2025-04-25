using System;

namespace Lingo.Models
{
    public class Language
    {
        public required Guid ID { get; set; } // Primary key
        public required string Name { get; set; } // Required

    }
}