using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInWarehouse { get; set; }
        public int WarehouseQuantity { get; set; }
    }
}
