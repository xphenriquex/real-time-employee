namespace real_time_employee_app.Interfaces
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
