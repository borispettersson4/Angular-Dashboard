using Core.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomerController(ApiContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var cs = _context.Customers.FirstOrDefault(c => c.Id==id);

            return Ok(cs);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            _context.Add(customer);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

    }
}
