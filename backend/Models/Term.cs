using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class Term
    {
        public required Guid ID { get; set; }
        public required string English { get; set; }
        public required string Translation { get; set; }
        public string? Notes { get; set; }
        public required Guid LanguageID { get; set; }
        public List<Guid> CategoryIDs { get; set; } = new List<Guid>();
        public DateTime CreationDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int TestAttemptCount { get; set; } = 0;
        public int TestCorrectCount { get; set; } = 0;
        public bool Flag { get; set; } = false;
    }
}