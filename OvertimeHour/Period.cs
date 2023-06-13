using System.Globalization;

namespace OvertimeHour;

public class Period
{
    public Period(DateTime baseDate, string start, string end) : this(baseDate, TimeSpan.Parse(start),
        TimeSpan.Parse(end))
    {
        OriginStart = start;
        OriginEnd = end;

        // var endDate = end == "00:00" ? baseDate.AddDays(1) : baseDate;
        //
        // Start = ParseToDateTime(baseDate, start);
        // End = ParseToDateTime(endDate, end);
    }

    public Period(DateTime start, DateTime end)
    {
        BaseDate = start.Date;
        OriginStart = start.ToString("HH:mm");
        OriginEnd = end.ToString("HH:mm");

        Start = start;
        End = end;
    }

    private Period(DateTime baseDate, TimeSpan start, TimeSpan end)
    {
        BaseDate = baseDate.Date;
        Start = BaseDate.Add(start);
        End = BaseDate.Add(end);
        if (end < start)
        {
            End = End.AddDays(1);
        }
    }

    public DateTime BaseDate { get; set; }

    public string OriginStart { get; set; }

    public string OriginEnd { get; set; }

    public DateTime Start { get; }

    public DateTime End { get; }

    // public bool IsCrossDay => End <= Start;
    public bool IsCrossDay => End.Date != Start.Date && End > Start;

    public Period OverlapPeriod(Period another)
    {
        if (IsTimeOverlap(another) == false)
        {
            return default;
        }

        var start = Start > another.Start ? OriginStart : another.OriginStart;
        var end = End < another.End ? OriginEnd : another.OriginEnd;

        return new Period(BaseDate, start, end);
    }

    private static DateTime ParseToDateTime(DateTime date, string start)
    {
        return DateTime.ParseExact($"{date:yyyy/MM/dd} {start}", "yyyy/MM/dd HH:mm", new DateTimeFormatInfo());
    }

    private bool IsTimeOverlap(Period another)
    {
        return Start < another.End && another.Start < End;
    }
}