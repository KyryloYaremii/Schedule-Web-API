using Microsoft.AspNetCore.Mvc;

namespace Schedule_Web_API.Controllers;

[Route("university")]
[ApiController]
public class UniversityController : ControllerBase
{
    [HttpGet("{university}")]
    public async Task<IActionResult> GetSchedule(string university)
    {
        if (!string.IsNullOrWhiteSpace(university))
        {
            return BadRequest("Некорректные параметры запроса.");
        }

        switch (university.ToLower())
        {
            case "lpnu":
                schedule = await _parser.GetScheduleAsync(group, semester);
                break;
            default:
                return NotFound("Расписание для данного университета не поддерживается.");
        }

        if (schedule.Count == 0)
        {
            return NotFound("Расписание не найдено.");
        }

        return Ok(schedule);
    }
}