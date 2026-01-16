using Microsoft.AspNetCore.Mvc;
using Schedule_Web_API.Models;
using Schedule_Web_API.Parsers;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IUniversityScheduleParser _parser;

    public ScheduleController(IUniversityScheduleParser parser)
    {
        _parser = parser;
    }

    [HttpGet("{university}/{group}/{semester}")]
    public async Task<IActionResult> GetSchedule(string university, string group, int semester)
    {
        if (string.IsNullOrWhiteSpace(group) || semester <= 0)
        {
            return BadRequest("Некорректные параметры запроса.");
        }

        List<LessonSchedule> schedule;
        switch (university.ToLower())
        {
            case "lpnu":
                schedule = await _parser.GetScheduleAsync(group, semester);
                break;
            // В будущем можно добавить case для других университетов
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
