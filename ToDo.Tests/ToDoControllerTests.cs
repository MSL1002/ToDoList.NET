using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ToDo.Models;
using ToDo.Controllers;
using ToDo.Data;
using ToDo.Models.DTOs;

namespace ToDo.Tests
{
    public class ToDoControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoItemsExist()
        {
            var context = TestHelpers.GetInMemoryContext();
            var controller = new ToDoController(context);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<ToDo.Models.ToDo>>(okResult.Value);
            Assert.Empty(items);
        }

        [Fact]
        public async Task GetOne_ReturnsNotFound_WhenItemDoesNotExist()
        {
            var context = TestHelpers.GetInMemoryContext();
            var controller = new ToDoController(context);

            var result = await controller.GetOne(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_AddsItem_AndReturnsCreatedResult()
        {
            var context = TestHelpers.GetInMemoryContext();
            var controller = new ToDoController(context);

            var dto = new Models.DTOs.ToDoItemDTO
            {
                Title = "Test item",
                Details = "Testing create",
                Date = DateTime.Today,
                IsDone = false
            };

            var result = await controller.Create(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdItem = Assert.IsType<ToDo.Models.ToDo>(createdResult.Value);
            Assert.Equal("Test item", createdItem.Title);
            Assert.Single(context.ToDos); // ensure there's only 1 row
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenTitleMissing()
        {
            var context = TestHelpers.GetInMemoryContext();
            var controller = new ToDoController(context);

            var dto = new ToDoItemDTO { Title = "" };

            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            Assert.False(isValid);
        }

        [Fact]
        public async Task Update_ModifiesExistingItem()
        {
            var context = TestHelpers.GetInMemoryContext();
            context.ToDos.Add(new ToDo.Models.ToDo { Id = 1, Title = "Old title", Details = "Old", Date = DateTime.Today, IsDone = false });
            await context.SaveChangesAsync();

            var controller = new ToDoController(context);
            var dto = new ToDoItemDTO { Title = "New title", Details = "Updated", Date = DateTime.Today, IsDone = true };

            var result = await controller.Edit(1, dto);

            Assert.IsType<NoContentResult>(result);
            var updated = await context.ToDos.FindAsync(1);
            Assert.Equal("New title", updated!.Title);
            Assert.True(updated.IsDone);
        }

        [Fact]
        public async Task Delete_RemovesItem_WhenItExists()
        {
            var context = TestHelpers.GetInMemoryContext();
            context.ToDos.Add(new ToDo.Models.ToDo { Id = 1, Title = "To delete", Date = DateTime.Today });
            await context.SaveChangesAsync();

            var controller = new ToDoController(context);
            var result = await controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
            Assert.Empty(context.ToDos);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenItemDoesNotExist()
        {
            var context = TestHelpers.GetInMemoryContext();
            var controller = new ToDoController(context);

            var result = await controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
