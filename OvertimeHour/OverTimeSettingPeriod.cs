namespace OvertimeHour;

public class OverTimeSettingPeriod : Period
{
    public OverTimeSettingPeriod(DateTime start, DateTime end, int normalRate)
        : base(start, end)
    {
        NormalRate = normalRate;
    }

    public OverTimeSettingPeriod(DateTime start, DateTime end, int notOvertimeRate, int haveOvertimeRate)
        : base(start, end)
    {
        NotOvertimeRate = notOvertimeRate;
        HaveOvertimeRate = haveOvertimeRate;
    }

    public OverTimeSettingPeriod(DateTime start, DateTime end)
        : base(start, end)
    {
    }

    public int NotOvertimeRate { get; }

    public int HaveOvertimeRate { get; }

    public int NormalRate { get; }

    public bool IsTimeOverlap(OverTimeSettingPeriod another)
    {
        return Start.TimeOfDay < another.End.TimeOfDay && another.Start.TimeOfDay < End.TimeOfDay;
    }

    public OverTimeSettingPeriod Overlap(OverTimeSettingPeriod another)
    {
        if (IsTimeOverlap(another))
        {
            var start = Start.TimeOfDay > another.Start.TimeOfDay ? Start : another.Start;
            
            var end = End.TimeOfDay < another.End.TimeOfDay ? End : another.End;

            return new OverTimeSettingPeriod(start, end);
        }

        return default;
    }
}