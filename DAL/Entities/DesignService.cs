namespace DAL.Entities
{
    public class DesignService
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public bool IsCustom { get; set; }

        public ICollection<PortfolioItem> PortfolioItems { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}
