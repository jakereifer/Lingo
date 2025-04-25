using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class TestSession
    {
        public required  Guid ID { get; set; }
        public required Guid LanguageID { get; set; }
        public List<Guid> CategoryIDs { get; set; } = new List<Guid>();
        public required List<Guid> TermIDs { get; set; } = new List<Guid>();
        public required int SessionLength { get; set; }
        public int SessionAttemptCount { get; set; } = 0;
        public int SessionCorrectCount { get; set; } = 0;
        public DateTime CreationDateTime { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}