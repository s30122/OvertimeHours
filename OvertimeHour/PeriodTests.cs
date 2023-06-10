using FluentAssertions;

namespace OvertimeHour;

public class PeriodTests
{
    [Fact]
    public void ctor_2_string_and_overtime_startDateTime()
    {
        var startDateTime = new DateTime(2023, 06, 01);

        var period = new Period(startDateTime, "01:00", "02:00");

        period.StartTimeSpan.Should().Be(TimeSpan.Parse("01:00"));
        period.EndTimeSpan.Should().Be(TimeSpan.Parse("02:00"));
        period.StartDateTime.Should().Be(new DateTime(2023, 06, 01, 01, 00, 00));
        period.EndDateTime.Should().Be(new DateTime(2023, 06, 01, 02, 00, 00));
    }

    [Fact]
    public void end_date_is_0000()
    {
        var startDateTime = new DateTime(2023, 06, 01);
        var period = new Period(startDateTime, "23:00", "00:00");

        period.StartTimeSpan.Should().Be(TimeSpan.Parse("23:00"));
        period.EndTimeSpan.Should().Be(TimeSpan.Parse("00:00"));
        period.StartDateTime.Should().Be(new DateTime(2023, 06, 01, 23, 00, 00));
        period.EndDateTime.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));
    }

    [Fact]
    public void start_date_is_0000()
    {
        var startDateTime = new DateTime(2023, 06, 01);
        var period = new Period(startDateTime, "00:00", "01:00");

        period.StartTimeSpan.Should().Be(TimeSpan.Parse("00:00"));
        period.EndTimeSpan.Should().Be(TimeSpan.Parse("01:00"));
        period.StartDateTime.Should().Be(new DateTime(2023, 06, 01, 00, 00, 00));
        period.EndDateTime.Should().Be(new DateTime(2023, 06, 01, 01, 00, 00));
    }
}