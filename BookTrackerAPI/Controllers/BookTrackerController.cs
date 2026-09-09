using BookTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BookTrackerAPI.Models;
using BookTrackerAPI.DTOs;

namespace BookTrackerAPI.Controllers;
[ApiController]
[Route("api/books")]
public class BookTrackerController : ControllerBase
{
    private readonly IBookTrackerService _bookTrackerService;

    public BookTrackerController(IBookTrackerService bookTrackerService)
    {
        _bookTrackerService = bookTrackerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _bookTrackerService.GetByIdAsync(id);
        if(book is null) return NotFound();
        return Ok(book);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll([FromQuery] string? name)
    {
        if (name is not null)
        {
            var books = await _bookTrackerService.GetByNameAsync(name);
            if (!books.Any()) return NotFound();
            return Ok(books);
        }
        return Ok(await _bookTrackerService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(CreateBookDTO book)
    {
        var created = await _bookTrackerService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.BookId}, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if(!await _bookTrackerService.DeleteBookAsync(id)) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDTO dto)
    {
        
        if(!await _bookTrackerService.UpdateBookAsync(id, dto)) return NotFound();
        return NoContent();
    }
}