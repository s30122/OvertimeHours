namespace OvertimeHour;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; }

    public DateTime End { get; }

    private bool IsTimeOverlap(Period another)
    {
        return Start.TimeOfDay < another.End.TimeOfDay && another.Start.TimeOfDay < End.TimeOfDay;
    }

    public Period Overlap(Period another)
    {
        if (IsTimeOverlap(another))
        {
            var start = Start.TimeOfDay > another.Start.TimeOfDay ? Start : another.Start;

            var end = End.TimeOfDay < another.End.TimeOfDay ? End : another.End;

            return new Period(start, end);
        }

        return default;
    }
}