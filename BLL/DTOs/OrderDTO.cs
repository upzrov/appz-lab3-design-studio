namespace BLL.DTOs
{
    public class OrderDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerContact { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public bool IsCustomDesignOrder { get; set; } = false;
        public string Description { get; set; } = string.Empty;
        public int? DesignServiceId { get; set; }

    }
}