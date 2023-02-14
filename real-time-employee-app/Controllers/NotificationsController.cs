using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using real_time_employee_app.Interfaces;
using real_time_employee_app.Models;
using real_time_employee_app.Services;

namespace real_time_employee_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public NotificationsController(DbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Notifications/notificationcount  
        [Route("notificationcount")]
        [HttpGet]
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
        {
            //var count = (from not in _context.Notification
            //             select not).CountAsync();

            var count = _context.Notification.CountAsync();

            NotificationCountResult result = new()
            {
                Count = await count
            };
            return result;
        }

        // GET: api/Notifications/notificationresult  
        [Route("notificationresult")]
        [HttpGet]
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()
        {
            //var results = from message in _context.Notification
            //              orderby message.Id descending
            //              select new NotificationResult
            //              {
            //                  EmployeeName = message.EmployeeName,
            //                  TranType = message.TranType
            //              };


            var results = _context.Notification
                  .OrderBy(x => x.Id)
                  .Select(n => new NotificationResult { EmployeeName = n.EmployeeName, TranType = n.TranType });

            return await results.ToListAsync();
        }

        // DELETE: api/Notifications/deletenotifications  
        [HttpDelete]
        [Route("deletenotifications")]
        public async Task<IActionResult> DeleteNotifications()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notification");
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.BroadcastMessage();

            return NoContent();
        }

    }
}
