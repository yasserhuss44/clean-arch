using Core.Interfaces;

namespace Core.Services;

public class ClockService : IClockService, ISingltonService
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
