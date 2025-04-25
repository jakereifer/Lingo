using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class TestSessionDTO
    {
        public required Guid LanguageID { get; set; }
        public List<Guid> CategoryIDs { get; set; } = new List<Guid>();
        public required int SessionLength { get; set; }
    }
}