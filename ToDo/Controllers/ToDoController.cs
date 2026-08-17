using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the list of to-do items
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_context.ToDos.ToList());
        }


        #region SingleGet
        [HttpGet("{id}")]
        // gets a single ToDo item
        public async Task<IActionResult> GetOne(int id)
        {
            var item = await _context.ToDos.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        #endregion

        #region Create

        // Handles the HTTP POST request to create a new to-do item
        [HttpPost]
        public async Task<IActionResult> Create(ToDo.Models.DTOs.CreateItemToDo dto)
        {
            var todoItem = new ToDo.Models.ToDo
            {
                Title = dto.Title,
                Details = dto.Details,
                Date = dto.Date,
                IsDone = dto.IsDone,
            };

            _context.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = todoItem.Id }, todoItem);
        }

        #endregion


        #region Edit

        // Handles the HTTP PUT request to edit a specific to-do item
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int? id, ToDo.Models.DTOs.CreateItemToDo dto)
        {
            var todoItem = await _context.ToDos.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Title = dto.Title;
            todoItem.Details = dto.Details;
            todoItem.Date = dto.Date;
            todoItem.IsDone = dto.IsDone;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Delete

        // Handles the HTTP DELETE request to delete a specific to-do item
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ToDos.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(item);

            await _context.SaveChangesAsync();

            return Ok(item);
        }

        #endregion
    }
}
