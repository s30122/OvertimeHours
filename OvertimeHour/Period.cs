using System.Globalization;

namespace OvertimeHour;

public class Period
{
    public Period(DateTime baseDate, string start, string end)
    {
        BaseDate = baseDate;
        OriginStartString = start;
        OriginEndString = end;

        StartDateTime = DateTimeParseExact(baseDate, start);

        var endDate = end == "00:00" ? baseDate.AddDays(1) : baseDate;
        EndDateTime = DateTimeParseExact(endDate, end);

        StartTimeSpan = TimeSpan.Parse(start);
        EndTimeSpan = TimeSpan.Parse(end);
    }

    public string OriginEndString { get; set; }

    public string OriginStartString { get; set; }

    public DateTime BaseDate { get; set; }

    public TimeSpan EndTimeSpan { get; }

    public TimeSpan StartTimeSpan { get; }

    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }

    public bool IsCrossDay => EndTimeSpan <= StartTimeSpan;

    public Period Overlap(Period another)
    {
        if (IsTimeOverlap(another))
        {
            var start = StartTimeSpan > another.StartTimeSpan ? OriginStartString : another.OriginStartString;
            var end = EndTimeSpan < another.EndTimeSpan ? OriginEndString : another.OriginEndString;

            return new Period(BaseDate, start, end);
        }

        return default;
    }

    private static DateTime DateTimeParseExact(DateTime date, string start)
    {
        return DateTime.ParseExact($"{date:yyyy/MM/dd} {start}", "yyyy/MM/dd HH:mm", new DateTimeFormatInfo());
    }

    private bool IsTimeOverlap(Period another)
    {
        return StartTimeSpan < another.EndTimeSpan && another.StartTimeSpan < EndTimeSpan;
    }
}