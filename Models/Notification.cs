using System.ComponentModel.DataAnnotations.Schema;

namespace JadeNET5SignalR.Models
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TranType { get; set; }
    }
}
