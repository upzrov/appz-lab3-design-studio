using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IStudioService
    {
        IEnumerable<DesignServiceDTO> GetServices();
        IEnumerable<OrderDTO> GetOrders();
        IEnumerable<PortfolioItemDTO> GetPortfolioItems();

        void MakeOrder(OrderDTO orderDto);
        void AddService(DesignServiceDTO serviceDto);
    }
}