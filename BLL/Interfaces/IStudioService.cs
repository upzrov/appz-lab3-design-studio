using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IStudioService
    {
        IEnumerable<DesignServiceDTO> GetServices();
        void MakeOrder(OrderDTO orderDto);
        void AddService(DesignServiceDTO serviceDto);
    }
}