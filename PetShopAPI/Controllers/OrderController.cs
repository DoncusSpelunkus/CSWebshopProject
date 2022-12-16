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
            return Ok(_orderService.GetCurrentOrderByUserId(userId));
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
    [HttpGet]
    [Route("OrderHistory")]
    public ActionResult<List<Order>> GetOrdersHistoryByUserId(Guid userId)
    {
        try
        {
            return Ok(_orderService.GetOrdersHistoryByUserId(userId));
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
    public ActionResult<Order> CreateOrder(OrderDTO dto, Guid userId)
    
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
    }[HttpPost]
    [Route("OrderHistory,{userId}")]
    public ActionResult<List<Order>> AddDateOfOrder(Guid userId)
    
    {
        try
        {
            var result = _orderService.AddDateOfOrder(userId);
            return Created("Orders of this user" + userId  +" was moved to orders history ", result);
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
    public ActionResult<Order> UpdateOrder([FromRoute] Guid userId, [FromBody] OrderDTO orderDto)
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
    public ActionResult<Order> DeleteOrderById(int productId, Guid userId)
    {
        try
        {
            return Ok(_orderService.DeleteOrder(productId, userId));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No order found with product ID " + productId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }

    [HttpPost]
    [Route("sendEmail")]
    public IActionResult SendOrderConfirmationEmail(String userEmail)
    {
        try
        {
            _orderService.SendEmailtoUser(userEmail);
            return Ok("Email sent");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}