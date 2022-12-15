using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application;
using PetShop.Application.Validators;

namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HistyoryOrderController : ControllerBase
    {
        
        private IHistyoryOrderService _orderService;
        public HistyoryOrderController(IHistyoryOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public ActionResult<List<HistoryOrder>> GetAllOrders()
        {
            try
            {
                return Ok(_orderService.GetAllOrders());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No orders found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        [HttpGet]
        [Route("GetOrderByUser/{userId}")]
        public ActionResult<List<HistoryOrder>> GetAllOrdersByUser(Guid userId)
        {
            try
            {
                return Ok(_orderService.GetAllOrdersByUser(userId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No orders found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public ActionResult<HistoryOrder> GetOrderById(int orderId)
        {
            try
            {
                return Ok(_orderService.GetOrderById(orderId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No order found at ID " + orderId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpPost]
        public ActionResult<HistoryOrder> CreateOrder(HistoryOrderDTO dto)
        {
            try
            {
                var result = _orderService.CreateOrder(dto);
                return Created("Order/" + result.Id, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{orderId}")]

        public ActionResult<HistoryOrder> UpdateOrder([FromRoute] int orderId, [FromBody] HistoryOrderDTO dto)
        {
            try
            {
                return Ok(_orderService.UpdateOrder(orderId, dto));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No order found at ID " + orderId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{orderId}")]

        public ActionResult<HistoryOrder> DeleteOrdersById(int orderId)
        {
            try
            {
                return Ok(_orderService.DeleteOrderById(orderId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No order found at ID " + orderId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
    }
    
}