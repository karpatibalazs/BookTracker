namespace BookTrackerAPI.Models;

public class Book
{
    public string BookName {get; set;} = string.Empty;
    public string BookAuthor {get; set;} = string.Empty;
    public int BookId {get; set;}
    public int BookRating {get; set;}
    public ReadingStatus Status {get; set;} = ReadingStatus.PlanToRead;
}