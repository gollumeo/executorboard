using Testcontainers.PostgreSql;

namespace ExecutorBoard.SystemTests.TestHost;

/// <summary>
/// Ephemeral PostgreSQL per test class (system/bootstrap only).
/// </summary>
public class PostgreSqlDatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public PostgreSqlDatabaseFixture()
    {
        var databaseName = $"systemtests_{Guid.NewGuid():N}";
        _container = new PostgreSqlBuilder()
            .WithDatabase(databaseName)
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    public string ConnectionString => _container.GetConnectionString();

    public Task InitializeAsync() => _container.StartAsync();

    public Task DisposeAsync() => _container.DisposeAsync().AsTask();
}
