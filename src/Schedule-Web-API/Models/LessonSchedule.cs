namespace Schedule_Web_API.Models;
public class LessonSchedule
{
    public string DayOfWeek { get; set; }   // День недели (например, "Вт")
    public int LessonNumber { get; set; }   // Номер пары
    public string SubjectInfo { get; set; } // Название и детали
    public string? OnlineLink { get; set; } // Ссылка на онлайн-занятие (если есть)
}
