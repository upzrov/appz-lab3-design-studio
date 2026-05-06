using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IStudioService
    {
        IEnumerable<DesignServiceDTO> GetServices();
        IEnumerable<OrderDTO> GetOrders();

        void MakeOrder(OrderDTO orderDto);
        void AddService(DesignServiceDTO serviceDto);
    }
}