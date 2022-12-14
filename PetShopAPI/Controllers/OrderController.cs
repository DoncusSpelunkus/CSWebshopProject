﻿using System.ComponentModel.DataAnnotations;
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
    public class OrderController : ControllerBase
    {
        
        private IOrderService _orderService;
        public OrderController(IOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
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
        public ActionResult<List<Order>> GetAllOrdersByUser(Guid userId)
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
        public ActionResult<Order> GetOrderById(int orderId)
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
        public ActionResult<Order> CreateOrder(OrderDTO dto)
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

        public ActionResult<Order> UpdateOrder([FromRoute] int orderId, [FromBody] OrderDTO dto)
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

        public ActionResult<Order> DeleteOrdersById(int orderId)
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