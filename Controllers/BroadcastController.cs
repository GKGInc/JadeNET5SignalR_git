using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using JadeNET5SignalR.Data;
using JadeNET5SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using JadeNET5SignalR.Hubs;

namespace JadeNET5SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public BroadcastController(MyDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("{workcenter}")]
        public async Task<IActionResult> BroadcastMessage(string workcenter)
        {
            try 
            {
                if (workcenter == null)
                    workcenter = "";
                await _hubContext.Clients.All.BroadcastMessage(workcenter);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
    }
}
