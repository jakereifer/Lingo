using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class TermDTO
    {
        public required string English { get; set; }
        public required string Translation { get; set; }
        public string? Notes { get; set; }
        public Guid LanguageID { get; set; }
        public List<Guid> CategoryIDs { get; set; } = new List<Guid>();
        public bool Flag { get; set; } = false;
    }
}