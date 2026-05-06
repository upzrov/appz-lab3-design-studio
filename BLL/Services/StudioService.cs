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

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = uow.GetRepository<Order>().GetAll();
            var orderDtos = mapper.Map<IEnumerable<OrderDTO>>(orders).ToList();

            var services = uow.GetRepository<DesignService>().GetAll().ToList();

            foreach (var dto in orderDtos)
            {
                if (dto.DesignServiceId.HasValue)
                {
                    var service = services.FirstOrDefault(s => s.Id == dto.DesignServiceId.Value);
                    if (service != null)
                    {
                        dto.ServiceName = service.Name;
                    }
                }
            }

            return orderDtos;
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            if (!string.IsNullOrEmpty(orderDto.ServiceName) && orderDto.DesignServiceId == null)
            {
                var service = uow.GetRepository<DesignService>()
                                 .GetAll()
                                 .FirstOrDefault(s => s.Name == orderDto.ServiceName);

                if (service != null)
                {
                    orderDto.DesignServiceId = service.Id;
                }
            }

            if (orderDto.DesignServiceId == null)
            {
                throw new ArgumentException("Для замовлення необхідно вибрати послугу.");
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