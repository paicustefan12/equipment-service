using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<EquipmentDepartment> EquipmentDepartments { get; set; }
    }
}
