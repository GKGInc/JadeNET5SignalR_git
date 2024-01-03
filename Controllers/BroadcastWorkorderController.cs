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
    public class BroadcastWorkorderController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<BroadcastWorkorderHub, IHubObjectClient> _hubContext;
        //private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public BroadcastWorkorderController(MyDbContext context, IHubContext<BroadcastWorkorderHub, IHubObjectClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        //public BroadcastWorkorderController(MyDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        //{
        //    _context = context;
        //    _hubContext = hubContext;
        //}

        [HttpPost]
        [Route("broadcastobject")]
        public async Task<IActionResult> BroadcastMessage(BroadcastObject broadcastobject) 
        {
            try
            {
                await _hubContext.Clients.All.BroadcastMessage(broadcastobject);

                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(workorder);
                //await _hubContext.Clients.All.BroadcastMessage(json);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
    }
}
