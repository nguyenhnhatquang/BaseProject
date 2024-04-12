namespace BaseProject.Domain.Abstractions.Caching;

public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
}