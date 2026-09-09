using BookTrackerAPI.Models;
using BookTrackerAPI.DTOs;

namespace BookTrackerAPI.Services;
public interface IBookTrackerService
{
    public Task<IEnumerable<Book>> GetAllAsync();
    public Task<Book?> GetByIdAsync(int id);
    public Task<IEnumerable<Book>> GetByNameAsync(string name);
    public Task<Book> AddBookAsync(CreateBookDTO book);
    public Task<bool> DeleteBookAsync(int id);
    public Task<bool> UpdateBookAsync(int id, UpdateBookDTO updatedBook);
}
