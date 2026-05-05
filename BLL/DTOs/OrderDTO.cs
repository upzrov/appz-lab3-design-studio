namespace BLL.DTOs
{
    public class OrderDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerContact { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCustomDesignOrder { get; set; }
        public int? DesignServiceId { get; set; }
    }
}