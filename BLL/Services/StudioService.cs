using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class StudioService(IUnitOfWork uow, IMapper mapper) : IStudioService
    {
        public IEnumerable<DesignServiceDTO> GetServices()
        {
            var services = uow.GetRepository<DesignService>().GetAll();
            return mapper.Map<IEnumerable<DesignServiceDTO>>(services);
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            if (!orderDto.IsCustomDesignOrder && orderDto.DesignServiceId == null)
            {
                throw new ArgumentException("Для замовлення з каталогу необхідно вибрати послугу.");
            }

            var order = mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.Now;

            uow.GetRepository<Order>().Create(order);
            uow.Save();
        }

        public void AddService(DesignServiceDTO serviceDto)
        {
            var service = mapper.Map<DesignService>(serviceDto);
            uow.GetRepository<DesignService>().Create(service);
            uow.Save();
        }
    }
}
