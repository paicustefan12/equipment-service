using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Models;
using OrderService.BLL.Models;
using EquipmentService.DAL.Entities;
using EquipmentService.DAL.Interfaces;
using EquipmentService.DAL.Repositories;
using MassTransit;
using MassTransit.Riders;
using OrderService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EquipmentService.BLL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IBus busService;
        private readonly IRepository<Equipment> repositoryEquipment;
        private readonly IOrderRepository orderRepository;

        public OrderManager(IBus busService,
            IRepository<Equipment> repositoryEquipment,
            IOrderRepository orderRepository)
        {
            this.busService = busService;
            this.repositoryEquipment = repositoryEquipment;
            this.orderRepository = orderRepository;
        }

        public async Task CreateOrder(OrderModel order)
        {
            order.MessageType = "Create";
            Uri uri = new Uri("amqps://host.docker.internal:5672/orderQueue");
            var endPoint = await busService.GetSendEndpoint(uri);
            await endPoint.Send(order);
        }

        public async Task DeleteOrder(int id)
        {
            var order = new OrderModel
            {
                MessageType = "Delete",
                OrderId = id
            };
            Uri uri = new Uri("amqps://host.docker.internal:5672/orderQueue");
            var endPoint = await busService.GetSendEndpoint(uri);
            await endPoint.Send(order);
        }

        public async Task<bool> CancelOrder(int id)
        {
            var order = new OrderModel
            {
                MessageType = "Cancel",
                OrderId = id
            };
            Uri uri = new Uri("amqps://host.docker.internal:5672/orderQueue");
            var endPoint = await busService.GetSendEndpoint(uri);
            await endPoint.Send(order);
            return true;
        }

        public async Task<bool> SuccessOrder(int id)
        {
            var order = new OrderModel
            {
                MessageType = "Success",
                OrderId = id
            };
            Uri uri = new Uri("amqps://host.docker.internal:5672/orderQueue");
            var endPoint = await busService.GetSendEndpoint(uri);
            await endPoint.Send(order);
            return true;
        }
        public async Task<bool> FailOrder(int id)
        {
            var order = new OrderModel
            {
                MessageType = "Fail",
                OrderId = id
            };
            Uri uri = new Uri("amqps://host.docker.internal:5672/orderQueue");
            var endPoint = await busService.GetSendEndpoint(uri);
            await endPoint.Send(order);
            return true;
        }

        public async Task<(bool Success, List<Order> orders)> GetAllOrders()
        {
            var orders = await orderRepository.GetAllWithEquipment();
            if (orders == null || orders.Count == 0)
                return (false, null);

            return (true, orders);
        }

        public async Task<(bool Success, Order orders)> GetOrder(int id)
        {
            var order = await orderRepository.GetWithEquipment(id);
            if (order == null)
                return (false, null);

            return (true, order);
        }

        private async Task<bool> UpdateStock(SuccessOrderModel updateStockModel)
        {
            var entity = await repositoryEquipment.Get(updateStockModel.EquipmentId);
            if (entity == null)
                return false;

            entity.WarehouseQuantity += updateStockModel.Quantity;
            await repositoryEquipment.Update(entity);
            return true;
        }

    }
}
