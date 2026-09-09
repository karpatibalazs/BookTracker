using Microsoft.EntityFrameworkCore;
using BookTrackerAPI.Data;
using BookTrackerAPI.Models;
using System.Reflection;
using BookTrackerAPI.DTOs;

namespace BookTrackerAPI.Services;

public class BookTrackerService : IBookTrackerService
{
    private readonly AppDbContext _context;
    public BookTrackerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetByNameAsync(string name)
    {
        return await _context.Books.Where(b => EF.Functions.Like(b.BookName, $"%{name}%")).ToListAsync();
    }
    public async Task<Book> AddBookAsync(CreateBookDTO dto)
    {
        var book = new Book
        {
            BookName = dto.BookName,
            BookAuthor = dto.BookAuthor,
            Status = dto.Status
        };
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if(book is null) return false;
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateBookAsync(int id, UpdateBookDTO updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if(book is null) return false;
        if (updatedBook.BookRating is not null) book.BookRating = updatedBook.BookRating.Value;
        if (updatedBook.Status is not null) book.Status = updatedBook.Status.Value;
        await _context.SaveChangesAsync();
        return true;
    }


}