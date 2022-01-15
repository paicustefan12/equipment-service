using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Models;
using EquipmentService.DAL.Entities;
using EquipmentService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Managers
{
    public class EquipmentManager : IEquipmentManager
    {
        private readonly IRepository<Equipment> repository;

        public EquipmentManager(IRepository<Equipment> repository)
        {
            this.repository = repository;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            await repository.Insert(equipment);
        }

        public async Task UpdateEquipment(Equipment equipment)
        {
            await repository.Update(equipment);
        }

        public async Task DeleteEquipment(int id)
        {
            var entity = await repository.Get(id);
            await repository.Delete(entity);
        }

        public async Task<bool> CheckEquipments()
        {
            var entities = await repository.GetAll();
            foreach (var entity in entities)
            {
                if (entity.WarehouseQuantity == 0)
                {
                    // create order for 50 pieces
                }
            }

            return true;
        }

        public async Task<bool> UpdateStock(UpdateStockModel updateStockModel)
        {
            var entity = await repository.Get(updateStockModel.EquipmentId);
            if (entity == null)
                return false;

            entity.WarehouseQuantity += updateStockModel.Quantity;
            await UpdateEquipment(entity);
            return true;
        }
    }
}
