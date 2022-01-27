using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Models;
using OrderService.BLL.Models;
using EquipmentService.DAL.Entities;
using EquipmentService.DAL.Interfaces;
using MassTransit;
using MassTransit.Riders;
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
        private readonly IOrderManager orderManager;
        private readonly IEquipmentRepository equipmentRepository;

        public EquipmentManager(IRepository<Equipment> repository,
            IOrderManager orderManager,
            IEquipmentRepository equipmentRepository)
        {
            this.repository = repository;
            this.orderManager = orderManager;
            this.equipmentRepository = equipmentRepository;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            await repository.Insert(equipment);
        }

        public async Task UpdateEquipment(Equipment equipment)
        {
            var entity = await repository.Get(equipment.Id);
            if (equipment.Name != null)
                entity.Name = equipment.Name;

            entity.IsInWarehouse = equipment.IsInWarehouse;
            entity.WarehouseQuantity = equipment.WarehouseQuantity;

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
                    var order = new OrderModel()
                    {
                        EquipmentId = entity.Id,
                        OrderType = "Order",
                        Quantity = 50
                    };
                    await orderManager.CreateOrder(order);
                }
            }

            return true;
        }

        public async Task<bool> UpdateStock(UpdateStockModel updateStockModel)
        {
            Console.WriteLine("Update-Stock");
            var entity = await repository.Get(updateStockModel.EquipmentId.GetValueOrDefault());
            if (entity == null)
                return false;

            entity.WarehouseQuantity += updateStockModel.Quantity.GetValueOrDefault();
            await UpdateEquipment(entity);
            return true;
        }

        public async Task<(bool Success, List<EquipmentModel> list)> GetEquipments()
        {
            var entities = await repository.GetAll();
            if (entities == null || entities.Count == 0)
                return (false, null);

            var equimentModels = entities.Select(e => new EquipmentModel()
            {
                Id = e.Id,
                IsInWarehouse = e.IsInWarehouse,
                Name = e.Name,
                WarehouseQuantity = e.WarehouseQuantity
            }).ToList();

            return (true, equimentModels);
        }

        public async Task<(bool Success, Equipment list)> GetEquipmentWithOrder(int id)
        {
            var entity = await equipmentRepository.GetWithOrders(id);
            if (entity == null)
                return (false, null);

            return (true, entity);
        }
    }
}
