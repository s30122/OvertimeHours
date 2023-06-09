using FluentAssertions;

namespace OvertimeHour;

public class OvertimePeriodSettingsTests
{
    [Fact]
    public void Ctor()
    {
        new OvertimePeriodSettings(new Period("01:00", "02:00"),
                                   new Period("02:00", "03:00"))
            .Count.Should().Be(2);
    }

    [Fact]
    public void Ctor_split_cross_day()
    {
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period("23:00", "02:00"));

        overtimePeriodSettings.Count.Should().Be(2);

        overtimePeriodSettings.Should().BeEquivalentTo(new List<Period>
        {
            new("23:00", "00:00"),
            new("00:00", "02:00")
        });
    }

    [Fact]
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