using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace OvertimeHour;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class OvertimePeriodSettingsTests
{
    [Fact]
    public void ctor()
    {
        var overtimeFormStartDateTime = new DateTime(2023, 06, 01);
        
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period("01:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(1);

        var overtimePeriodSetting = overtimePeriodSettings[0];
        overtimePeriodSetting.StartTimeSpan.Should().Be(TimeSpan.Parse("01:00"));
        overtimePeriodSetting.EndTimeSpan.Should().Be(TimeSpan.Parse("02:00"));
        overtimePeriodSetting.StartDateTime.Should().Be(new DateTime(2023, 06, 01, 01, 00, 00));
        overtimePeriodSetting.EndDateTime.Should().Be(new DateTime(2023, 06, 01, 02, 00, 00));
    }

    [Fact(Skip = "skip")]
    public void ctor_split_cross_day()
    {
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period("23:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(2);

        overtimePeriodSettings.Should().BeEquivalentTo(new List<Period>
        {
            new("23:00", "00:00"),
            new("00:00", "02:00")
        });
    }

    [Fact(Skip = "skip")]
    public void split_overlap_1_period()
    {
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period("06:00", "22:00"),
                                                                new Period("22:00", "06:00"));

        var overTimePeriod = new Period(new DateTime(2023, 10, 08, 18, 00, 00),
                                        new DateTime(2023, 10, 08, 20, 00, 00));

        var realOvertimePeriods = overtimePeriodSettings.SplitPeriod(overTimePeriod).ToList();

        realOvertimePeriods.Should().BeEquivalentTo(new List<Period>
        {
            new("18:00", "20:00"),
        }, options => options.Excluding(a => a.StartDateTime)
                             .Excluding(a => a.EndDateTime));
    }
}