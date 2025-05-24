using Microsoft.AspNetCore.Mvc;
using NotificationService.DTOs;
using NotificationService.Services;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly JobNotificationService _notificationService;

        public NotificationController(JobNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotificationDto dto)
        {
            await _notificationService.HandleNotificationAsync(dto);
            return Ok(new { message = "Notification sent" });
        }
    }
}
