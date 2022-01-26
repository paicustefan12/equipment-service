using EquipmentService.DAL.Entities;
using MassTransit.Riders;
using OrderService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Interfaces
{
    public interface IOrderManager
    {
        Task CreateOrder(OrderModel order);
        Task DeleteOrder(int id);
        Task<bool> CancelOrder(int id);
        Task<bool> SuccessOrder(int id);
        Task<bool> FailOrder(int id);
        Task<(bool Success, List<Order> orders)> GetAllOrders();
        Task<(bool Success, Order orders)> GetOrder(int id);
    }
}
