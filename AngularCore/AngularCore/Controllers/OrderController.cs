using Core.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApiContext _context;

        public OrderController(ApiContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _context.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _context.Orders.Include(x => x.Customer).ToList();

            var groupedResult = orders.GroupBy(x => x.Customer.State)
                .ToList().Select(grp => new
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total).ToList();

            return Ok(groupedResult);
        }

        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            var orders = _context.Orders.Include(x => x.Customer).ToList();

            var groupedResult = orders.GroupBy(x => x.Customer.Id)
                .ToList().Select(grp => new
                {
                    Name = _context.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total).Take(n).ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.Include(x => x.Customer).FirstOrDefault(x => x.Id == id);
            return Ok(order);
        }
    }
}
