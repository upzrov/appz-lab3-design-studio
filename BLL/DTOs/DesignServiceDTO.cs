namespace BLL.DTOs
{
    public class DesignServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public bool IsCustom { get; set; }
    }
}