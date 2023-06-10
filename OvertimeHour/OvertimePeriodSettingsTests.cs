using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace OvertimeHour;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class OvertimePeriodSettingsTests
{
    [Fact]
    public void ctor_1_setting()
    {
        var baseDate = new DateTime(2023, 06, 01);
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(baseDate, "01:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(1);
    }

    [Fact]
    public void ctor_1_setting_cross_day()
    {
        var baseDate = new DateTime(2023, 06, 01);
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(baseDate, "23:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(2);

        var overtimePeriodSettingFirst = overtimePeriodSettings[0];
        overtimePeriodSettingFirst.Start.Should().Be(new DateTime(2023, 06, 01, 23, 00, 00));
        overtimePeriodSettingFirst.End.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));

        var overtimePeriodSettingSecond = overtimePeriodSettings[1];
        overtimePeriodSettingSecond.Start.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));
        overtimePeriodSettingSecond.End.Should().Be(new DateTime(2023, 06, 02, 02, 00, 00));
    }

    [Fact]
    public void ctor_2_setting()
    {
        var baseDate = new DateTime(2023, 06, 01);

        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(baseDate, "06:00", "17:00"),
                                                                new Period(baseDate, "17:00", "06:00"));

        overtimePeriodSettings.Count.Should().Be(3);

        var overtimePeriodSettingFirst = overtimePeriodSettings[0];
        overtimePeriodSettingFirst.Start.Should().Be(new DateTime(2023, 06, 01, 06, 00, 00));
        overtimePeriodSettingFirst.End.Should().Be(new DateTime(2023, 06, 01, 17, 00, 00));

        var overtimePeriodSettingSecond = overtimePeriodSettings[1];
        overtimePeriodSettingSecond.Start.Should().Be(new DateTime(2023, 06, 01, 17, 00, 00));
        overtimePeriodSettingSecond.End.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));

        var overtimePeriodSettingThird = overtimePeriodSettings[2];
        overtimePeriodSettingThird.Start.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));
        overtimePeriodSettingThird.End.Should().Be(new DateTime(2023, 06, 02, 06, 00, 00));
    }

    [Fact]
    public void split_period_have_1_overlap_not_cross_day()
    {
        var overtimeStart = new DateTime(2023, 06, 01, 18, 00, 00);
        var overtimeEnd = new DateTime(2023, 06, 01, 20, 00, 00);

        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(overtimeStart, "06:00", "17:00"),
                                                                new Period(overtimeStart, "17:00", "06:00"));

        var overTimePeriod = new Period(overtimeStart, overtimeEnd);

        var overTimePeriods = overtimePeriodSettings.SplitPeriod(overTimePeriod).ToList();

        overTimePeriods.Count.Should().Be(1);

        overTimePeriods.Should().BeEquivalentTo(new List<Period>
        {
            new(overtimeStart, overtimeEnd)
        });
    }
}