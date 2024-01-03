using System.Threading.Tasks;

namespace JadeNET5SignalR.Hubs
{
    public interface IHubClient
    {
        //Task BroadcastMessage();
        Task BroadcastMessage(string workCenter);
    }
}
