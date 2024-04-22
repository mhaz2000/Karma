using Karma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Repositories
{
    public class RepositoryTest : IDisposable
    {
        protected DataContext _dataContext { get; private set; }

        protected RepositoryTest()
        {
            InitializeDataContext();
        }

        private static Random random = new Random();

        private void InitializeDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase" + Guid.NewGuid().ToString())
                .Options;

            _dataContext = new DataContext(options);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
