using FluentAssertions;

namespace OvertimeHour;

public class OvertimeTests
{

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