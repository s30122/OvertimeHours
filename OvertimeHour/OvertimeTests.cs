using FluentAssertions;

namespace OvertimeHour;

public class OvertimeTests
{
    [Fact]
    public void day_overlap_1_period()
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
}