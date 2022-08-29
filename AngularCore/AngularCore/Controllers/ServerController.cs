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
    public class ServerController : ControllerBase
    {
        private readonly ApiContext _context;

        public ServerController(ApiContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _context.Servers.OrderBy(x => x.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}",Name ="GetServer")]
        public IActionResult Get(int id)
        {
            var response = _context.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _context.Servers.Find(id);

            if (server == null)
                return NotFound();

            if(msg.Payload == "activate")
            {
                server.isOnline = true;
                _context.SaveChanges();
            }
            else if (msg.Payload == "deactivate")
            {
                server.isOnline = false;
                _context.SaveChanges();
            }
            return new NoContentResult();
        }
    }
}
