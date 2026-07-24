using Microsoft.EntityFrameworkCore;
using BookTrackerAPI.Models;

namespace BookTrackerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Book> Books {get; set;}

}