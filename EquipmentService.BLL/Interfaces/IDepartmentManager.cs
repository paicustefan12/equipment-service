using EquipmentService.BLL.Models;
using EquipmentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Interfaces
{
    public interface IDepartmentManager
    {
        Task CreateDepartment(Department department);
        Task UpdateDepartment(Department department);
        Task DeleteDepartment(int id);
        Task<bool> MoveDepartment(MoveEquipmentModel moveEquipmentModel);
    }
}
