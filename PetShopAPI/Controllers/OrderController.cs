using Factory.Application.PostProdDTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShopApi.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
        
    }
    
    [HttpGet]
    public ActionResult<List<Order>> GetAllOrdersByUserId(Guid userId)
    {
        try
        {
            return Ok(_orderService.GetAllOrderByUserId(userId));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No orders found for this user");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpPost]
    [Route("{userId}")]
    public ActionResult<Specs> CreateOrder(OrderDTO dto, Guid userId)
    
    {
        try
        {
            var result = _orderService.CreateOrder(dto, userId);
            return Created("order for this user" + result.UserId  +" was created ", result);
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
    [Route("{userId}")]
    public ActionResult<Order> UpdateSpecs([FromRoute] Guid userId, [FromBody] OrderDTO orderDto)
    {
        try
        {
            return Ok(_orderService.UpdateOrder(userId, orderDto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No order found at ID " + userId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpDelete("{userId}")]
    public ActionResult<Specs> DeleteSpecsById(int orderId, Guid userId)
    {
        try
        {
            return Ok(_orderService.DeleteOrder(orderId, userId));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No specification found at ID " + orderId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    
    
}