using EquipmentService.DAL.Entities;
using EquipmentService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllWithEquipment()
        {
            var orders = await context.Orders.Include(x => x.Equipment).ToListAsync();
            return orders;
        }
        public async Task<Order> GetWithEquipment(int id)
        {
            var order = await context.Orders.Include(x => x.Equipment).FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

    }
}
