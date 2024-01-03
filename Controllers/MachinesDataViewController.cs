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
    public class MachineDataViewController : ControllerBase
    {
        #region Variables/Constructor

        private readonly MyDbContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public MachineDataViewController(MyDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        #endregion

        #region API Points

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachinesDataView>>> GetMachineData()
        {
            //return await _context.MachinesDataView.ToListAsync(); ;
            return (await _context.MachinesDataView.ToListAsync()).OrderBy(x => x.Workcenter).ToList();
        }

        // GET: api/MachinesData/999
        //[HttpGet("{id}")] // id = pk ??
        //public async Task<ActionResult<MachinesDataView>> GetMachineData(string id)
        //{
        //    //var machinesData = await _context.MachinesDataView.FindAsync(id);
        //    if (machinesData == null)
        //        return NotFound();
        //    return machinesData;
        //}
        [HttpGet("{id}")] // id = Workcenter
        public async Task<ActionResult<MachinesDataView>> GetMachineData(string id)
        {
            var machinesData = await _context.MachinesDataView.FromSqlRaw<MachinesDataView>(string.Format(@"SELECT * FROM [JAM].[dbo].MachinesDataView WHERE Workcenter = '{0}' ", id)).ToListAsync();

            if (machinesData == null || machinesData.Count == 0)
            {
                return NotFound();
            }

            return machinesData[0];
        }

        // PUT: api/MachinesData/999
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{workCenter}")]
        public async Task<IActionResult> PutMachineData(string workCenter, MachinesDataView machinesDataView)
        {
            if (workCenter != machinesDataView.Workcenter)
            {
                return BadRequest();
            }

            Notification notification = new Notification()
            {
                Name = machinesDataView.Workcenter,
                TranType = "Edit"
            };
            //_context.Notification.Add(notification);

            var machines = await _context.Machines.FromSqlRaw<Machines>(string.Format(@"SELECT * FROM [JAM].[dbo].Machines WHERE WC_Number = '{0}' ", workCenter)).ToListAsync();
            if (machines.Count > 0)
            {
                var machine = machines[0];
                int pk = machine.pk;

                var machineData = await _context.Machines.FindAsync(pk);
                if (machineData != null)
                {
                    machineData.Description = machinesDataView.Description;

                    //if (!string.IsNullOrWhiteSpace(machinesDataView.Department))
                    //    machineData.Department = machinesDataView.Department;
                    //machineData.isMemex = machinesDataView.isMemex;

                    machineData.MachineState = machinesDataView.MachineState;
                    machineData.Operator = machinesDataView.Operator;
                    machineData.OperatorId = machinesDataView.OperatorId;
                    machineData.WorkOrder = machinesDataView.WorkOrder;
                    machineData.OpStep = machinesDataView.OpStep;
                    machineData.OID = machinesDataView.OID;
                    machineData.Product = machinesDataView.Product;
                    machineData.CycleCount = machinesDataView.CycleCount;
                    machineData.PartsMade = machinesDataView.PartsMade;
                    machineData.PartsRequired = machinesDataView.PartsRequired;
                    machineData.TotalPartsProduced = machinesDataView.TotalPartsProduced;

                    _context.Entry(machineData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            try
            {             
                await _hubContext.Clients.All.BroadcastMessage(workCenter);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineDataExists(workCenter))
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

        // POST: api/MachinesData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MachinesDataView>> PostMachineData(MachinesDataView machinesDataView)
        {
            if (string.IsNullOrWhiteSpace(machinesDataView.Workcenter))
            {
                return BadRequest();
            }

            string workCenter = machinesDataView.Workcenter;
            bool canCreateNew = false;

            Notification notification = new Notification()
            {
                Name = machinesDataView.Workcenter,
                TranType = "Edit"
            };
            //_context.Notification.Add(notification);

            var machines = await _context.Machines.FromSqlRaw<Machines>(string.Format(@"SELECT * FROM [JAM].[dbo].Machines WHERE WC_Number = '{0}' ", workCenter)).ToListAsync();
            if (machines.Count > 0)
            {
                // update
                var machine = machines[0];
                int pk = machine.pk;

                var machineData = await _context.Machines.FindAsync(pk);
                if (machineData != null)
                {
                    machineData.Description = machinesDataView.Description;

                    //if (!string.IsNullOrWhiteSpace(machinesDataView.Department))
                    //    machineData.Department = machinesDataView.Department;
                    //machineData.isMemex = machinesDataView.isMemex;

                    machineData.MachineState = machinesDataView.MachineState;
                    machineData.Operator = machinesDataView.Operator;
                    machineData.OperatorId = machinesDataView.OperatorId;
                    machineData.WorkOrder = machinesDataView.WorkOrder;
                    machineData.OpStep = machinesDataView.OpStep;
                    machineData.OID = machinesDataView.OID;
                    machineData.Product = machinesDataView.Product;
                    machineData.CycleCount = machinesDataView.CycleCount;
                    machineData.PartsMade = machinesDataView.PartsMade;
                    machineData.PartsRequired = machinesDataView.PartsRequired;
                    machineData.TotalPartsProduced = machinesDataView.TotalPartsProduced;

                    _context.Entry(machineData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                //create new
                if (canCreateNew) // Note: Works
                {
                    Machines m = new Machines();
                    m.WC_Number = workCenter;
                    m.Description = machinesDataView.Description;
                    m.Serial_Number = "";
                    m.Alternates = "";
                    m.Department = "";
                    m.isMemex = false;
                    //m.Department = machinesDataView.Department;
                    //m.isMemex = machinesDataView.isMemex;
                    m.AssetId = "";
                    m.MachineState = machinesDataView.MachineState;

                    m.Operator = machinesDataView.Operator;
                    m.OperatorId = machinesDataView.OperatorId;
                    m.WorkOrder = machinesDataView.WorkOrder;
                    m.OpStep = machinesDataView.OpStep;
                    m.OID = machinesDataView.OID;
                    m.Product = machinesDataView.Product;
                    m.CycleCount = machinesDataView.CycleCount;
                    m.PartsMade = machinesDataView.PartsMade;
                    m.PartsRequired = machinesDataView.PartsRequired;
                    m.TotalPartsProduced = machinesDataView.TotalPartsProduced;
                    m.LastGoodPartsId = 0;

                    _context.Machines.Add(m);

                    //Notification notification = new Notification()
                    //{
                    //    Name = m.WC_Number,
                    //    TranType = "Add"
                    //};
                    ////_context.Notification.Add(notification);

                    try
                    {
                        await _context.SaveChangesAsync();
                        await _hubContext.Clients.All.BroadcastMessage(workCenter);
                    }
                    catch (DbUpdateException)
                    {
                        if (MachineDataExists(m.WC_Number))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            try
            {             
                await _hubContext.Clients.All.BroadcastMessage(workCenter);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineDataExists(workCenter))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return CreatedAtAction("GetMachineData", new { id = machinesDataView.Workcenter }, machinesDataView);
        }

        private bool MachineDataExists(string id)
        {
            return _context.MachinesDataView.Any(e => e.Workcenter == id);
        }

        #endregion

        #region Unused

        //// PUT: api/MachinesData/999
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMachineData(string id, MachinesDataView machinesDataView)
        //{
        //    if (id != machinesDataView.Workcenter)
        //    {
        //        return BadRequest();
        //    }
        //
        //    _context.Entry(machinesDataView).State = EntityState.Modified;
        //
        //    Notification notification = new Notification()
        //    {
        //        Name = machinesDataView.Workcenter,
        //        TranType = "Edit"
        //    };
        //    //_context.Notification.Add(notification);
        //
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        await _hubContext.Clients.All.BroadcastMessage();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MachineDataExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //
        //    return NoContent();
        //}

        //// POST: api/MachinesData
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MachinesDataView>> PostMachineData(MachinesDataView machinesData)
        //{
        //    //employee.Id = Guid.NewGuid().ToString();
        //    _context.MachinesDataView.Add(machinesData);
        //
        //    Notification notification = new Notification()
        //    {
        //        Name = machinesData.Workcenter,
        //        TranType = "Add"
        //    };
        //    //_context.Notification.Add(notification);
        //
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        await _hubContext.Clients.All.BroadcastMessage();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (MachineDataExists(machinesData.Workcenter))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //
        //    return CreatedAtAction("GetMachinesData", new { id = machinesData.Workcenter }, machinesData);
        //}

        //// DELETE: api/MachinesData/999
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMachineData(string id)
        //{
        //    var machinesData = await _context.MachinesData.FindAsync(id);
        //    if (machinesData == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    Notification notification = new Notification()
        //    {
        //        Name = machinesData.Workcenter,
        //        TranType = "Delete"
        //    };
        //
        //    _context.MachinesData.Remove(machinesData);
        //    _context.Notification.Add(notification);
        //
        //    await _context.SaveChangesAsync();
        //    await _hubContext.Clients.All.BroadcastMessage();
        //
        //    return NoContent();
        //}

        #endregion
    }
}
