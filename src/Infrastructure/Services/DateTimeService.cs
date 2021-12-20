using Application.Interfaces;

namespace Tech.CleanArchitecture.Infrastructure.Persistence.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
