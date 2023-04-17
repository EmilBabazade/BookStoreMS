using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Data;
using OrderManagerAPI.Order.Dtos;

namespace OrderManagerAPI.Order;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AppDbContext appDbContext;
    private readonly IMapper mapper;

    public OrderController(AppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    /// <param name="userId">Orders belonging to user</param>
    /// <param name="createdAt">Orders created at and after this date</param>
    /// <param name="cancelledAt">Orders cancelled at and after this date</param>
    /// <param name="cancelled">Only cancelled orders</param>
    /// <param name="confirmedAt">Orders confirmed at and after this date</param>
    /// <param name="confirmed">Only confirmed orders</param>
    /// <param name="shippedAt">Orders shipped at and after this date</param>
    /// <param name="shipped">Only shipped orders</param>
    /// <param name="completedAt">Orders completed at and after this date</param>
    /// <param name="completed">Only completed orders</param>
    /// <param name="includeBooks">Include books as well</param>
    /// <returns></returns>
    [HttpGet("GetOrders")]
    public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetAllOrders(
        [FromQuery] int? userId,
        [FromQuery] DateTime? createdAt,
        [FromQuery] DateTime? cancelledAt, [FromQuery] bool? cancelled,
        [FromQuery] DateTime? confirmedAt, [FromQuery] bool? confirmed,
        [FromQuery] DateTime? shippedAt, [FromQuery] bool? shipped,
        [FromQuery] DateTime? completedAt, [FromQuery] bool? completed,
        [FromQuery] bool includeBooks = false)
    {
        var query = this.appDbContext.Orders as IQueryable<Order>;

        query = FilterOrdersByDate(createdAt, cancelledAt, confirmedAt, shippedAt, completedAt, query);
        query = FilterByStatus(cancelled, confirmed, shipped, completed, query);
        query = includeBooks ? query.Include(o => o.Books) : query;

        var orders = await query.ToListAsync();
        return Ok(this.mapper.Map<List<GetOrderDto>>(orders));
    }

    private static IQueryable<Order> FilterByStatus(bool? cancelled, bool? confirmed, bool? shipped, bool? completed, IQueryable<Order> query)
    {
        if (cancelled != null)
        {
            query = query.Where(o => (bool)cancelled ? o.CancelledAt != null : o.CancelledAt == null);
        }

        if (confirmed != null)
        {
            query = query.Where(o => (bool)confirmed ? o.ConfirmedAt != null : o.ConfirmedAt == null);
        }

        if (shipped != null)
        {
            query = query.Where(o => (bool)shipped ? o.ShippedAt != null : o.ShippedAt == null);
        }

        if (completed != null)
        {
            query = query.Where(o => (bool)completed ? o.ConfirmedAt != null : o.ConfirmedAt == null);
        }

        return query;
    }

    private static IQueryable<Order> FilterOrdersByDate(DateTime? createdAt, DateTime? cancelledAt, DateTime? confirmedAt, DateTime? shippedAt, DateTime? completedAt, IQueryable<Order> query)
    {
        if (createdAt != null)
            query = query.Where(o => o.CreatedAt >= createdAt);

        if (cancelledAt != null)
            query = query.Where(o => o.CancelledAt >= cancelledAt);

        if (confirmedAt != null)
            query = query.Where(o => o.ConfirmedAt >= confirmedAt);

        if (shippedAt != null)
            query = query.Where(o => o.ShippedAt >= shippedAt);

        if (completedAt != null)
            query = query.Where(o => o.CompletedAt >= completedAt);
        return query;
    }
}
