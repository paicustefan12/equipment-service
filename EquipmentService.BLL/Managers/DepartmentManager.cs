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
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IRepository<Department> repository;
        private readonly IRepository<Equipment> repositoryEquipment;
        private readonly IRepository<EquipmentDepartment> repositoryEquipmentDeparment;

        public DepartmentManager(IRepository<Department> repository,
            IRepository<Equipment> repositoryEquipment,
            IRepository<EquipmentDepartment> repositoryEquipmentDeparment)
        {
            this.repository = repository;
            this.repositoryEquipment = repositoryEquipment;
            this.repositoryEquipmentDeparment = repositoryEquipmentDeparment;
        }

        public async Task CreateDepartment(Department department)
        {
            var i = 0;
            await repository.Insert(department);
        }

        public async Task UpdateDepartment(Department department)
        {
            var entity = await repository.Get(department.Id);
            if (entity == null)
            {
                return;
            }
            entity.Name = department.Name;
            await repository.Update(entity);
        }

        public async Task DeleteDepartment(int id)
        {
            var entity = await repository.Get(id);
            await repository.Delete(entity);
        }

        public async Task<bool> MoveDepartment(MoveEquipmentModel moveEquipmentModel)
        {
            if (moveEquipmentModel == null)
                return false;

            var equipment = await repositoryEquipment.Get(moveEquipmentModel.EquipmentId);
            if (equipment == null)
                return false;

            if (equipment.WarehouseQuantity < moveEquipmentModel.Quantity)
                equipment.WarehouseQuantity = 0;
            else equipment.WarehouseQuantity -= moveEquipmentModel.Quantity;

            var equipmentDepartment = new EquipmentDepartment
            {
                EquipmentId = moveEquipmentModel.EquipmentId,
                DepartmentId = moveEquipmentModel.DepartmentId,
                Quantity = moveEquipmentModel.Quantity,
            };

            await repositoryEquipmentDeparment.Insert(equipmentDepartment);

            return true;
        }
    }
}
