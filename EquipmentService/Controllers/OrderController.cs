﻿using EquipmentService.BLL.Interfaces;
using MassTransit.Riders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.BLL.Models;
using System.Threading.Tasks;

namespace EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] OrderModel order)
        {
            await orderManager.CreateOrder(order);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await orderManager.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelAsync([FromRoute] int id)
        {
            var response = await orderManager.CancelOrder(id);
            return response == true ? Ok() : BadRequest();
        }

        [HttpPost("success/{id}")]
        public async Task<IActionResult> SuccessAsync([FromRoute] int id)
        {
            var response = await orderManager.SuccessOrder(id);
            return response == true ? Ok() : BadRequest();
        }

        [HttpPost("fail/{id}")]
        public async Task<IActionResult> FailAsync([FromRoute] int id)
        {
            var response = await orderManager.FailOrder(id);
            return response == true ? Ok() : BadRequest();
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var (success, list) = await orderManager.GetAllOrders();
            return success ? Ok(list) : NotFound("There are no orders.");
        }

        [HttpGet("get-order/{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            var (success, list) = await orderManager.GetAllOrders();
            return success ? Ok(list) : NotFound("There are no orders.");
        }
    }
}
