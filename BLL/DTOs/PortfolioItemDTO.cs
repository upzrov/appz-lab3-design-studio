namespace BLL.DTOs
{
    public class PortfolioItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int DesignServiceId { get; set; }
    }
}
