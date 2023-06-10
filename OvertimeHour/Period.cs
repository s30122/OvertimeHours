using System.Globalization;

namespace OvertimeHour;

public class Period
{
    public Period(DateTime startDateTime, DateTime endDateTime)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;

        StartTimeSpan = TimeSpan.Parse(startDateTime.ToString("HH:mm"));
        EndTimeSpan = TimeSpan.Parse(endDateTime.ToString("HH:mm"));
    }

    public Period(DateTime overtimeStartDateTime, string start, string end)
    {
        StartDateTime = DateTime.ParseExact($"{overtimeStartDateTime:yyyy/MM/dd} {start}", "yyyy/MM/dd HH:mm", new DateTimeFormatInfo());
        EndDateTime = DateTime.ParseExact($"{overtimeStartDateTime:yyyy/MM/dd} {end}", "yyyy/MM/dd HH:mm", new DateTimeFormatInfo());

        StartTimeSpan = TimeSpan.Parse(start);
        EndTimeSpan = TimeSpan.Parse(end);
    }

    public Period(string start, string end)
        : this(DateTime.UtcNow, start, end)
    {
    }

    public Period(TimeSpan start, TimeSpan end)
    {
        StartDateTime = DateTime.ParseExact($"{start.Hours:00}:{start.Minutes:00}", "HH:mm", new DateTimeFormatInfo());
        EndDateTime = DateTime.ParseExact($"{end.Hours:00}:{end.Minutes:00}", "HH:mm", new DateTimeFormatInfo());

        StartTimeSpan = start;
        EndTimeSpan = end;
    }

    public TimeSpan EndTimeSpan { get; }

    public TimeSpan StartTimeSpan { get; }

    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }

    public bool IsCrossDay => EndTimeSpan <= StartTimeSpan;

    public Period Overlap(Period another)
    {
        if (IsTimeOverlap(another))
        {
            var start = StartTimeSpan > another.StartTimeSpan ? StartDateTime : another.StartDateTime;
            var end = EndTimeSpan < another.EndTimeSpan ? EndDateTime : another.EndDateTime;

            return new Period(start, end);
        }

        return default;
    }

    private bool IsTimeOverlap(Period another)
    {
        return StartTimeSpan < another.EndTimeSpan && another.StartTimeSpan < EndTimeSpan;
    }
}