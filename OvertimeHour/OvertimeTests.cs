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
    public void First()
    {
        var dayOverTimeStartSetting = "06:00";
        var dayOverTimeEndSetting = "22:00";
        var dayOverTimeRateSetting = 150;

        var dayOverTimeStart = ConvertTimeToDateTime(dayOverTimeStartSetting);
        var dayOverTimeEnd = ConvertTimeToDateTime(dayOverTimeEndSetting);

        var dayOverTimePeriod = new OverTimeSettingPeriod(dayOverTimeStart, dayOverTimeEnd, dayOverTimeRateSetting);

        var nightOverTimeStartSetting = "22:00";
        var nightOverTimeEndSetting = "06:00";
        var nightOverTimeDayNotOvertimeRateSetting = 200;
        var nightOverTimeDayHaveOvertimeRateSetting = 210;

        var nightOverTimeStart = ConvertTimeToDateTime(nightOverTimeStartSetting);
        var nightOverTimeEnd = ConvertTimeToDateTime(nightOverTimeEndSetting);

        var nightOverTimePeriod = new OverTimeSettingPeriod(nightOverTimeStart, nightOverTimeEnd, nightOverTimeDayNotOvertimeRateSetting, nightOverTimeDayHaveOvertimeRateSetting);

        var overtimeStart = ConvertTimeToDateTime("18:00");
        var overtimeEnd = ConvertTimeToDateTime("20:00");

        var overTimePeriod = new OverTimeSettingPeriod(overtimeStart, overtimeEnd);

        dayOverTimePeriod.IsTimeOverlap(overTimePeriod).Should().Be(true);
        nightOverTimePeriod.IsTimeOverlap(overTimePeriod).Should().Be(false);

        var overlap = overTimePeriod.Overlap(dayOverTimePeriod);
        overlap.Start.Should().Be(ConvertTimeToDateTime("18:00"));
        overlap.End.Should().Be(ConvertTimeToDateTime("20:00"));
    }

    private DateTime ConvertTimeToDateTime(string dayOverTimeSettingStart)
    {
        return DateTime.ParseExact($"{_year}/{_month}/{_day} {dayOverTimeSettingStart}",
                                   "yyyy/MM/dd HH:mm",
                                   new DateTimeFormatInfo());
    }
}

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