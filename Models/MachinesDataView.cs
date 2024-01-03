using Microsoft.EntityFrameworkCore;

namespace JadeNET5SignalR.Models
{
    [Keyless]
    public class MachinesDataView
    {
        public string Workcenter { get; set; }
        public string Description { get; set; }
        //public string Department { get; set; }
        //public bool isMemex { get; set; }
        public string MachineState { get; set; }
        public string Operator { get; set; }
        public string OperatorId { get; set; }
        public string WorkOrder { get; set; }
        public int OpStep { get; set; }
        public string SonoOpno { get; set; }
        public int OID { get; set; }
        public string Product { get; set; }
        public int PartsRequired { get; set; }
        public int CycleCount { get; set; }
        public int PartsMade { get; set; }
        public int TotalPartsProduced { get; set; }
        public int FinalTotalPartsProduced { get; set; }
        public int CompletedQty { get; set; }
        public int PartsToGo { get; set; }
        public int LastGoodPartsId { get; set; }
        public int pk { get; set; }
    }
}
