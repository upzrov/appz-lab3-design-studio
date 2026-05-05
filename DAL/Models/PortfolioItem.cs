namespace DAL.Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int DesignServiceId { get; set; }
        public DesignService? DesignService { get; set; }
    }
}
