using EquipmentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllWithEquipment();
        Task<Order> GetWithEquipment(int id);
    }
}
