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

        var dayOverTimePeriod = OverTimeSettingPeriod(dayOverTimeStartSetting, dayOverTimeEndSetting, dayOverTimeRateSetting);

        var nightOverTimeStartSetting = "22:00";
        var nightOverTimeEndSetting = "06:00";
        var nightOverTimeDayNotOvertimeRateSetting = 200;
        var nightOverTimeDayHaveOvertimeRateSetting = 210;

        var nightOverTimePeriod = NightOverTimePeriod(nightOverTimeStartSetting, nightOverTimeEndSetting, nightOverTimeDayNotOvertimeRateSetting, nightOverTimeDayHaveOvertimeRateSetting);

        var overTimePeriod = OverTimePeriod("18:00", "20:00");

        var overlap = overTimePeriod.Overlap(dayOverTimePeriod);
        overlap.Start.Should().Be(ConvertTimeToDateTime("18:00"));
        overlap.End.Should().Be(ConvertTimeToDateTime("20:00"));

        overTimePeriod.Overlap(nightOverTimePeriod).Should().BeNull();
    }

    private OverTimeSettingPeriod OverTimePeriod(string start, string end)
    {
        var overtimeStart = ConvertTimeToDateTime(start);
        var overtimeEnd = ConvertTimeToDateTime(end);

        return new OverTimeSettingPeriod(overtimeStart, overtimeEnd);
    }

    private OverTimeSettingPeriod NightOverTimePeriod(string nightOverTimeStartSetting,
                                                      string nightOverTimeEndSetting,
                                                      int nightOverTimeDayNotOvertimeRateSetting,
                                                      int nightOverTimeDayHaveOvertimeRateSetting)
    {
        var nightOverTimeStart = ConvertTimeToDateTime(nightOverTimeStartSetting);
        var nightOverTimeEnd = ConvertTimeToDateTime(nightOverTimeEndSetting);

        return new OverTimeSettingPeriod(nightOverTimeStart, nightOverTimeEnd, nightOverTimeDayNotOvertimeRateSetting, nightOverTimeDayHaveOvertimeRateSetting);
    }

    private OverTimeSettingPeriod OverTimeSettingPeriod(string dayOverTimeStartSetting, string dayOverTimeEndSetting, int dayOverTimeRateSetting)
    {
        var dayOverTimeStart = ConvertTimeToDateTime(dayOverTimeStartSetting);
        var dayOverTimeEnd = ConvertTimeToDateTime(dayOverTimeEndSetting);

        return new OverTimeSettingPeriod(dayOverTimeStart, dayOverTimeEnd, dayOverTimeRateSetting);
    }

    private DateTime ConvertTimeToDateTime(string dayOverTimeSettingStart)
    {
        return DateTime.ParseExact($"{_year}/{_month}/{_day} {dayOverTimeSettingStart}",
                                   "yyyy/MM/dd HH:mm",
                                   new DateTimeFormatInfo());
    }
}