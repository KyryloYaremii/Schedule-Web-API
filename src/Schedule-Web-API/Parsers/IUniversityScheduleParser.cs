using Schedule_Web_API.Models;

namespace Schedule_Web_API.Parsers;

public interface IUniversityScheduleParser
{
    Task<List<LessonSchedule>> GetScheduleAsync(string group, int semester);
}
