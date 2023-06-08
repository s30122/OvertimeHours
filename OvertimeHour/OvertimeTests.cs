using System.Globalization;
using FluentAssertions;

namespace OvertimeHour;

public class OvertimeTests
{
    private readonly string _year;
    private readonly string _month;
    private readonly string _day;

    public OvertimeTests()
    {
        _year = "2023";
        _month = "10";
        _day = "09";
    }

    [Fact]
    public void day_overlap_1_period()
    {
        var dayOverTimePeriod = OverTimePeriod("06:00", "22:00");
        var nightOverTimePeriod = OverTimePeriod("22:00", "06:00");

        var settingPeriods = new List<Period> { dayOverTimePeriod, nightOverTimePeriod };

        var overTimePeriod = OverTimePeriod("18:00", "20:00");
        var result = SplitOvertimePeriod(settingPeriods, overTimePeriod).ToList();

        result.Count.Should().Be(1);

        var overlap = result[0];
        overlap.Start.Should().Be(ConvertTimeToDateTime("18:00"));
        overlap.End.Should().Be(ConvertTimeToDateTime("20:00"));
    }

    private IEnumerable<Period> SplitOvertimePeriod(List<Period> settingPeriods, Period overTimePeriod)
    {
        foreach (var settingPeriod in settingPeriods)
        {
            var overTimeSettingPeriod = settingPeriod.Overlap(overTimePeriod);

            if (overTimeSettingPeriod != null)
            {
                yield return overTimeSettingPeriod;
            }
        }
    }

    private Period OverTimePeriod(string start, string end)
    {
        var overtimeStart = ConvertTimeToDateTime(start);
        var overtimeEnd = ConvertTimeToDateTime(end);

        return new Period(overtimeStart, overtimeEnd);
    }

    private DateTime ConvertTimeToDateTime(string dayOverTimeSettingStart)
    {
        return DateTime.ParseExact($"{_year}/{_month}/{_day} {dayOverTimeSettingStart}",
                                   "yyyy/MM/dd HH:mm",
                                   new DateTimeFormatInfo());
    }
}