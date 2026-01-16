namespace Schedule_Web_API.Models;
public class LessonSchedule
{
    public string DayOfWeek { get; set; }
    public int LessonNumber { get; set; }   
    public string SubjectInfo { get; set; } 
    public string? OnlineLink { get; set; }
}