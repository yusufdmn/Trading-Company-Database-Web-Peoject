using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Entities;
using Web.Handlers;
using Web.Repositories;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{

    private readonly OrderCreationHandler _orderCreationHandler;

    public OrderController(OrderCreationHandler orderCreationHandler)
    {
        _orderCreationHandler = orderCreationHandler;
    }


    [HttpPost("mkorder")]
    public async Task<IActionResult> Insert([FromBody] OrderDto orderDto)
    {
        await _orderCreationHandler.Handle(orderDto.OrderLines,orderDto.CustomerId,orderDto.PlatformUrl);
        return Ok();
    }



}
public class OrderDto
{

    public List<Orderline> OrderLines { get; set; }
    public int CustomerId { get; set; }
    public string PlatformUrl { get; set; }

}