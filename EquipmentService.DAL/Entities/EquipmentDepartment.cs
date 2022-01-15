using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Entities
{
    public class EquipmentDepartment : BaseEntity
    {
        public int DepartmentId { get; set;}
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Department Department { get; set; }
    }
}
