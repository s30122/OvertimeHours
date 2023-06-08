namespace OvertimeHour;

public class Period
{
    protected Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; }

    public DateTime End { get; }
}