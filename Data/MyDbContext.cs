using Microsoft.EntityFrameworkCore;
using JadeNET5SignalR.Models;

namespace JadeNET5SignalR.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Employee> Employee { get; set; }
        public DbSet<MachinesDataView> MachinesDataView { get; set; }
        public DbSet<Machines> Machines { get; set; }
        public DbSet<Notification> Notification { get; set; }
    }
}
