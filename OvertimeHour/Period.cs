using System.Globalization;

namespace OvertimeHour;

public class Period
{
    public Period(DateTime startDateTime, DateTime endDateTime)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public Period(string start, string end)
    {
        StartDateTime = DateTime.ParseExact($"{start}", "HH:mm", new DateTimeFormatInfo());
        EndDateTime = DateTime.ParseExact($"{end}", "HH:mm", new DateTimeFormatInfo());
        
        StartTimeSpan = TimeSpan.Parse(start);
        EndTimeSpan = TimeSpan.Parse(end);
    }

    public TimeSpan EndTimeSpan { get; }

    public TimeSpan StartTimeSpan { get; }

    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }

    public Period Overlap(Period another)
    {
        if (IsTimeOverlap(another))
        {
            var start = StartDateTime.TimeOfDay > another.StartDateTime.TimeOfDay ? StartDateTime : another.StartDateTime;

            var end = EndDateTime.TimeOfDay < another.EndDateTime.TimeOfDay ? EndDateTime : another.EndDateTime;

            return new Period(start, end);
        }

        return default;
    }

    private bool IsTimeOverlap(Period another)
    {
        return StartDateTime.TimeOfDay < another.EndDateTime.TimeOfDay && another.StartDateTime.TimeOfDay < EndDateTime.TimeOfDay;
    }
}