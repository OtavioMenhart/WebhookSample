using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebhookSample.Data.Context;

namespace WebhookSample.Tests.Repository
{
    public class BaseTest : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<ClientContext> _contextOptions;
        protected readonly ClientContext _context;

        public BaseTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<ClientContext>()
            .UseSqlite(_connection)
            .Options;

            _context = new ClientContext(_contextOptions);
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _connection.Dispose();
        }
    }
}
