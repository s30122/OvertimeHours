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
        var startDateTime = new DateTime(2023, 06, 01);

        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(startDateTime, "01:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(1);
    }

    [Fact]
    public void ctor_split_cross_day()
    {
        var startDateTime = new DateTime(2023, 06, 01);

        var overtimePeriodSettings = new OvertimePeriodSettings(new Period(startDateTime, "23:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(2);

        var overtimePeriodSettingFirst = overtimePeriodSettings[0];
        overtimePeriodSettingFirst.StartTimeSpan.Should().Be(TimeSpan.Parse("23:00"));
        overtimePeriodSettingFirst.EndTimeSpan.Should().Be(TimeSpan.Parse("00:00"));
        overtimePeriodSettingFirst.StartDateTime.Should().Be(new DateTime(2023, 06, 01, 23, 00, 00));
        overtimePeriodSettingFirst.EndDateTime.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));

        // overtimePeriodSettings.Should().BeEquivalentTo(new List<Period>
        // {
        //     new(startDateTime, "23:00", "00:00"),
        //     new(startDateTime.AddDays(1),"00:00", "02:00")
        // });
    }

    [Fact(Skip = "skip")]
    public void split_overlap_1_period()
    {
        // var overtimePeriodSettings = new OvertimePeriodSettings(new Period("06:00", "22:00"),
        //                                                         new Period("22:00", "06:00"));
        //
        // var overTimePeriod = new Period(new DateTime(2023, 10, 08, 18, 00, 00),
        //                                 new DateTime(2023, 10, 08, 20, 00, 00));
        //
        // var realOvertimePeriods = overtimePeriodSettings.SplitPeriod(overTimePeriod).ToList();
        //
        // realOvertimePeriods.Should().BeEquivalentTo(new List<Period>
        // {
        //     new("18:00", "20:00"),
        // }, options => options.Excluding(a => a.StartDateTime)
        //                      .Excluding(a => a.EndDateTime));
    }
}