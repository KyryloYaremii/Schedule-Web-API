namespace Schedule_Web_API.Parsers;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Schedule_Web_API.Models;

public class NuLpParser : IUniversityScheduleParser
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://student.lpnu.ua/students_schedule";

    public NuLpParser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LessonSchedule>> GetScheduleAsync(string group, int semester)
    {
        var url = $"{BaseUrl}?studygroup_abbrname={group}&semestr={semester}&semestrduration=1";
        var response = await _httpClient.GetStringAsync(url);

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response);

        var schedule = new List<LessonSchedule>();

        var days = htmlDoc.DocumentNode.SelectNodes("//span[@class='view-grouping-header']");
        var lessons = htmlDoc.DocumentNode.SelectNodes("//div[@class='views-row']");

        if (days == null || lessons == null)
        {
            return schedule;
        }

        string currentDay = "";
        foreach (var lesson in lessons)
        {
            var prevElement = lesson.PreviousSibling;
            while (prevElement != null && prevElement.Name != "span")
                prevElement = prevElement.PreviousSibling;

            if (prevElement != null && prevElement.HasClass("view-grouping-header"))
            {
                currentDay = prevElement.InnerText.Trim();
            }

            var lessonNumberNode = lesson.SelectSingleNode(".//h3[@class='stud_schedule']");
            int lessonNumber = int.TryParse(lessonNumberNode?.InnerText.Trim(), out var num) ? num : 0;

            var subjectNode = lesson.SelectSingleNode(".//div[@class='group_content']");
            string subjectInfo = subjectNode?.InnerText.Trim() ?? "Нет данных";

            var linkNode = lesson.SelectSingleNode(".//span[@class='schedule_url_link']/a");
            string? lessonLink = linkNode?.GetAttributeValue("href", null);

            schedule.Add(new LessonSchedule
            {
                DayOfWeek = currentDay,
                LessonNumber = lessonNumber,
                SubjectInfo = subjectInfo,
                OnlineLink = lessonLink
            });
        }

        return schedule;
    }
}
