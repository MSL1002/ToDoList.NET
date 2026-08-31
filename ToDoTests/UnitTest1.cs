using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using ToDo.Controllers;
using ToDo.Data;
using ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ToDoTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetOne_Returns_Item()
        {
            // Arrange: create in-memory db context and seed a single ToDo item
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var context = new ApplicationDbContext(options))
            {
                var todo = new ToDo.Models.DTOs.ToDoItemDTO
                {
                    Title = "Test Item",
                    Details = "Details",
                    Date = DateTime.UtcNow,
                    IsDone = false
                };

                var controller = new ToDoController(context);

                await controller.Create(todo);

                // Act
                var actionResult = await controller.GetOne(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(actionResult);
                var returned = Assert.IsType<ToDo.Models.ToDo>(okResult.Value);
                Assert.Equal(1, returned.Id);
                Assert.Equal("Test Item", returned.Title);
            }
        }
    }
}
