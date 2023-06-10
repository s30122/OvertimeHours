namespace OvertimeHour;

public class OvertimePeriodSettings : List<Period>
{
    public OvertimePeriodSettings(params Period[] periods)
    {
        foreach (var period in periods)
        {
            if (period.IsCrossDay)
            {
                Add(new Period(period.BaseDate, period.OriginStart, "00:00"));
                Add(new Period(period.BaseDate.AddDays(1), "00:00", period.OriginEnd));
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