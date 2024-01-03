using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using JadeNET5SignalR.Data;
using JadeNET5SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JadeNET5SignalR.Hubs;

namespace JadeNET5SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReloadController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<ReloadHub, IHubClient> _hubContext;

        public ReloadController(MyDbContext context, IHubContext<ReloadHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        //[HttpPost]
        //public async Task<IActionResult> BroadcastMessage()
        //{
        //    try
        //    {
        //        await _hubContext.Clients.All.BroadcastMessage("");
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw;
        //    }
        //    return NoContent();
        //}

        [HttpPost]
        public async Task<IActionResult> BroadcastMessage()
        {
            try
            {
                await _hubContext.Clients.All.BroadcastMessage("");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpPost]
        [Route("{reloadId}")]
        public async Task<IActionResult> BroadcastRelloadIdMessage(string reloadId)
        {
            try
            {
                if (reloadId == null) 
                    reloadId = "";
                await _hubContext.Clients.All.BroadcastMessage(reloadId);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

    }
}
