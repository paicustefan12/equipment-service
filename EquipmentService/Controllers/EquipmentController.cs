using EquipmentService.BLL.Interfaces;
using EquipmentService.BLL.Models;
using OrderService.BLL.Models;
using EquipmentService.DAL.Entities;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentManager equipmentManager;

        public EquipmentController(IEquipmentManager equipmentManager)
        {
            this.equipmentManager = equipmentManager;
        }

        [HttpPost("create")]
        [Authorize("AdminManager")]
        public async Task<IActionResult> CreateAsync([FromBody] Equipment equipment)
        {
            await equipmentManager.CreateEquipment(equipment);
            return NoContent();
        }

        [HttpPut("update")]
        [Authorize("AdminManager")]
        public async Task<IActionResult> UpdateAsync([FromBody] Equipment equipment)
        {
            await equipmentManager.UpdateEquipment(equipment);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize("AdminManager")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await equipmentManager.DeleteEquipment(id);
            return NoContent();
        }

        [HttpPost("check-equipments")]
        [Authorize("All")]
        public async Task<IActionResult> CheckEquipmentsAsync()
        {
            var response = await equipmentManager.CheckEquipments();
            return response == true ? Ok() : BadRequest();
        }

        [HttpPost("update-stock")]

        public async Task<IActionResult> UpdateStockAsync([FromBody] UpdateStockModel updateStockModel)
        {
            var response = await equipmentManager.UpdateStock(updateStockModel);
            return response == true ? Ok() : BadRequest();
        }

        [HttpGet("get-equipmets")]
        [Authorize("All")]
        public async Task<IActionResult> GetEquipments()
        {
            var (Success, list) = await equipmentManager.GetEquipments();
            return Success == true ? Ok(list) : NotFound("There is no equipment");
        }

        [HttpGet("get-equipmet/{id}")]
        [Authorize("All")]
        public async Task<IActionResult> GetEquipments([FromRoute] int id)
        {
            var (Success, equipment) = await equipmentManager.GetEquipmentWithOrder(id);
            return Success == true ? Ok(equipment) : NotFound("There is no equipment");
        }
    }
}
