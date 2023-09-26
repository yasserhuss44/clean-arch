namespace Core.Interfaces;

public interface IClockService
{
    DateTime Now { get; }

    DateTime UtcNow { get; }
}