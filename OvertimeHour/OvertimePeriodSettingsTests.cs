using FluentAssertions;

namespace OvertimeHour;

public class OvertimePeriodSettingsTests
{
    [Fact]
    public void Ctor()
    {
        var overtimePeriodSettings = new OvertimePeriodSettings(new Period("01:00", "02:00"),
                                                                new Period("02:00", "03:00"));

        overtimePeriodSettings.Count.Should().Be(2);
    }
}