using EquipmentService.BLL.Models;
using EquipmentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Interfaces
{
    public interface IEquipmentManager
    {
        Task CreateEquipment(Equipment equipment);
        Task UpdateEquipment(Equipment equipment);
        Task DeleteEquipment(int id);
        Task<bool> CheckEquipments();
        Task<bool> UpdateStock(UpdateStockModel updateStockModel);

    }
}
