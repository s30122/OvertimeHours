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
        _day = "08";
    }

    [Fact]
    public void day_overlap_1_period()
    {
        var dayOverTimePeriod = new Period("06:00", "22:00");
        var nightOverTimePeriod = new Period("22:00", "06:00");
        
        var settingPeriods = new OvertimePeriodSettings
        {
            dayOverTimePeriod,
            nightOverTimePeriod
        };

        var overTimePeriod = new Period(new DateTime(2023, 10, 08, 18, 00, 00),
                                        new DateTime(2023, 10, 08, 20, 00, 00));

        var result = SplitOvertimePeriod(settingPeriods, overTimePeriod).ToList();
        
        result.Should().BeEquivalentTo(new List<Period>
        {
            OverTimePeriod("18:00", "20:00"),
        });
    }

    [Fact(Skip = "skip")]
    public void day_overlap_2_period()
    {
        // var dayOverTimePeriod = OverTimePeriod("06:00", "22:00");
        // var nightOverTimePeriod = OverTimePeriod("22:00", "06:00");
        //
        // var settingPeriods = new List<Period> { dayOverTimePeriod, nightOverTimePeriod };
        //
        // var overTimePeriod = OverTimePeriod("20:00", "23:00");
        // var result = SplitOvertimePeriod(settingPeriods, overTimePeriod).ToList();
        //
        // result.Count.Should().Be(2);
        //
        // result.Should().BeEquivalentTo(new List<Period>
        // {
        //     OverTimePeriod("20:00", "22:00"),
        //     OverTimePeriod("22:00", "23:00"),
        // });
    }

    private IEnumerable<Period> SplitOvertimePeriod(OvertimePeriodSettings settingPeriods, Period overTimePeriod)
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