using Microsoft.AspNetCore.SignalR;
using real_time_employee_app.Interfaces;

namespace real_time_employee_app.Services
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
