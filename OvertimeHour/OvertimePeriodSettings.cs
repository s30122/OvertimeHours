namespace OvertimeHour;

public class OvertimePeriodSettings : List<Period>
{
    public OvertimePeriodSettings(params Period[] periods)
    {
        foreach (var period in periods)
        {
            // cross day
            if (period.EndTimeSpan <= period.StartTimeSpan)
            {
                Add(new Period(period.StartTimeSpan, TimeSpan.Zero));
                Add(new Period(TimeSpan.Zero, period.EndTimeSpan));
            }
            else
            {
                Add(period);
            }
        }
    }

    public IEnumerable<Period> SplitPeriod(Period overTimePeriod)
    {
        foreach (var settingPeriod in this)
        {
            var overTimeSettingPeriod = settingPeriod.Overlap(overTimePeriod);

            if (overTimeSettingPeriod != null)
            {
                yield return overTimeSettingPeriod;
            }
        }
    }
}