using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PersonalTrainer.API.Data;

namespace PersonalTrainer.Tests.Infrastructure;

/// <summary>
/// Creates a real SQLite in-memory database for each test class.
/// The connection must stay open for the in-memory database to persist —
/// closing it destroys all data. Dispose() closes it when the test class finishes.
/// </summary>
public sealed class TestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public AppDbContext Context { get; }

    public TestDatabase()
    {
        // Named in-memory DB so the same connection string returns the same DB
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new AppDbContext(options);

        // Creates all tables from the current EF model (no migrations needed).
        // Also runs HasData() seed — exercise templates will be pre-seeded.
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
