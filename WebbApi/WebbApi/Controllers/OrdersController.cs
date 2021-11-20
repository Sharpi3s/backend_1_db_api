using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbApi.Data;
using WebbApi.Entities;
using WebbApi.Models.Orders;
using WebbApi.Models.products;

namespace WebbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrders>>> GetOrders()
        {
            var AllOrders = await _context.Orders.Include(x => x.OrderLines).ToListAsync();

            var Orders = new List<GetOrders>();

            foreach (var item in AllOrders)
            {

                var order = new GetOrders()
                {
                    Id = item.Id,
                    UsersId = item.UsersId,
                    UserAdressesId = item.UserAdressesId,
                    OrderDate = item.OrderDate,
                    Status = item.Status,
                    TotalAmount = item.TotalAmount
                };

                foreach (var orderline in item.OrderLines)
                {
                    var _product = await _context.Products.FindAsync(orderline.ProductsId);

                    order.OrderLines.Add(new GetOrderLine
                    {
                        Id = orderline.Id,
                        OrdersId = orderline.OrdersId,
                        ProductsId = orderline.ProductsId,
                        Quantity = orderline.Quantity,
                        UnitPrice = orderline.UnitPrice,
                        Product = _product
                    });
                }
                Orders.Add(order);
            }

            return new OkObjectResult(Orders);

        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(x => x.OrderLines).FirstOrDefaultAsync(x => x.Id == id);
            var _orderline = await _context.OrderLines.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);


            if (order == null)
            {
                return NotFound();
            }

                var _order = new GetOrders()
                {
                    Id = order.Id,
                    UsersId = order.UsersId,
                    UserAdressesId = order.UserAdressesId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount
                };


                foreach (var orderline in order.OrderLines)
                {
                    var _product = await _context.Products.FindAsync(orderline.ProductsId);

                    _order.OrderLines.Add(new GetOrderLine
                    {
                        Id = orderline.Id,
                        OrdersId = orderline.OrdersId,
                        ProductsId = orderline.ProductsId,
                        Quantity = orderline.Quantity,
                        UnitPrice = orderline.UnitPrice,
                        Product = _product
                    });

                }

            return new OkObjectResult(_order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(AddOrder model)
        {

            var user = await _context.Users.FindAsync(model.UsersId);

            var order = new Order
            {
                UsersId = user.Id,
                OrderDate = model.OrderDate,
                Status = model.Status,
                UserAdressesId = user.UserAdressesId,
                TotalAmount = model.TotalAmount

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            var orderLines = new List<OrderLine>();

            foreach(var item in model.CartProducts)
            {
                orderLines.Add(new OrderLine
                {
                    OrdersId = order.Id,
                    ProductsId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price

                });
            }
            _context.OrderLines.AddRange(orderLines);
            await _context.SaveChangesAsync();

            //_context.OrderLines.Add(new OrderLine { OrdersId = order.Id });
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
