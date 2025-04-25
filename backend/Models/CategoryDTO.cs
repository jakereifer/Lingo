namespace backend.Models
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public Guid LanguageID { get; set; }
    }
}