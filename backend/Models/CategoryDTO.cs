namespace backend.Models
{
    public class CategoryDTO
    {
        public required string Name { get; set; }
        public string? Notes { get; set; }
        public required Guid LanguageID { get; set; }
    }
}