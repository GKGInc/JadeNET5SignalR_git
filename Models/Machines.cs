using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JadeNET5SignalR.Models
{
    public class Machines
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pk { get; set; }
        public string WC_Number { get; set; } // Workcenter
        public string Description { get; set; }
        public string Serial_Number { get; set; }
        public string Alternates { get; set; }
        public string Department { get; set; }
        public bool isMemex { get; set; }
        public string AssetId { get; set; }
        public string MachineState { get; set; }
        public string Operator { get; set; }
        public string OperatorId { get; set; }
        public string WorkOrder { get; set; }
        public int OpStep { get; set; }
        public int OID { get; set; }
        public string Product { get; set; }
        public int CycleCount { get; set; }
        public int PartsMade { get; set; }
        public int PartsRequired { get; set; }
        public int TotalPartsProduced { get; set; }
        public int LastGoodPartsId { get; set; }
    }
}
