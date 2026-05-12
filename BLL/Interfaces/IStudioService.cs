using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IStudioService
    {
        IEnumerable<DesignServiceDTO> GetAllServices();
        IEnumerable<OrderDTO> GetOrders();
        IEnumerable<PortfolioItemDTO> GetPortfolioItems();

        void MakeOrder(OrderDTO orderDto);
        void AddService(DesignServiceDTO serviceDto);
        void CreatePortfolioItem(PortfolioItemDTO itemDto);
    }
}