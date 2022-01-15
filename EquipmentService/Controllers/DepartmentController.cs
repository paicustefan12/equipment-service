using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Models;
using EquipmentService.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager departmentManager;

        public DepartmentController(IDepartmentManager departmentManager)
        {
            this.departmentManager = departmentManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] Department department)
        {
            await departmentManager.CreateDepartment(department);
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Department department)
        {
            await departmentManager.UpdateDepartment(department);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await departmentManager.DeleteDepartment(id);
            return NoContent();
        }

        [HttpPost("move-to-department")]
        public async Task<IActionResult> MoveToDepartment([FromBody] MoveEquipmentModel moveEquipmentModel)
        {
            var response = await departmentManager.MoveDepartment(moveEquipmentModel);
            return response == true ? Ok() : BadRequest();
        }
    }
}
