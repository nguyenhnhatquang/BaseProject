namespace BaseProject.Domain.Shares;

public class AppSettings
{
    public required string WebApiUrl { get; init; }
    public required string Secret { get; init; }
    public required int RefreshTokenTTL { get; init; }
}