using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Entities
{
    public class Equipment : BaseEntity
    {
        public string Name { get; set; }
        public bool IsInWarehouse { get; set; }
        public int WarehouseQuantity { get; set; }
        public virtual ICollection<EquipmentDepartment> EquipmentDepartments { get; set; }
    }
}
