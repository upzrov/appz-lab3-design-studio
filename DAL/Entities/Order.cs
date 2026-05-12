namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerContact { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public bool IsCustomDesignOrder { get; set; }

        public int? DesignServiceId { get; set; }
        public DesignService? DesignService { get; set; }
    }
}
