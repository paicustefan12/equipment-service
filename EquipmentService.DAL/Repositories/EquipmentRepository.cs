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
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext context;

        public EquipmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Equipment> GetWithOrders(int id)
        {
            var equipment = await context.Equipments.Include(x => x.Orders).FirstOrDefaultAsync();
            return equipment;
        }
    }
}
