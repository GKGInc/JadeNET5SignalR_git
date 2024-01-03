using JadeNET5SignalR.Models;
using System.Threading.Tasks;

namespace JadeNET5SignalR.Hubs
{
    public interface IHubObjectClient
    {
        Task BroadcastMessage(BroadcastObject broadcastObject);
    }
}
