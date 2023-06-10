using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace OvertimeHour;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PeriodTests
{
    [Fact]
    public void ctor()
    {
        var baseDate = new DateTime(2023, 06, 01);
        var period = new Period(baseDate, "01:00", "02:00");

        period.Start.Should().Be(new DateTime(2023, 06, 01, 01, 00, 00));
        period.End.Should().Be(new DateTime(2023, 06, 01, 02, 00, 00));
    }

    [Fact]
    public void end_date_is_0000()
    {
        var baseDate = new DateTime(2023, 06, 01);
        var period = new Period(baseDate, "23:00", "00:00");

        period.Start.Should().Be(new DateTime(2023, 06, 01, 23, 00, 00));
        period.End.Should().Be(new DateTime(2023, 06, 02, 00, 00, 00));
    }

    [Fact]
    public void start_date_is_0000()
    {
        var baseDate = new DateTime(2023, 06, 01);
        var period = new Period(baseDate, "00:00", "01:00");

        period.Start.Should().Be(new DateTime(2023, 06, 01, 00, 00, 00));
        period.End.Should().Be(new DateTime(2023, 06, 01, 01, 00, 00));
    }
}