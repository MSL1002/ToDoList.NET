using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Tests
{
    public static class TestHelpers
    {
        /// <summary>
        /// Creates new In Memory DB for each test
        /// </summary>
        /// <returns>An in memory Database</returns>
        public static ApplicationDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
