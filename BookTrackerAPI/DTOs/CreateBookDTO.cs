using BookTrackerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BookTrackerAPI.DTOs;

public class CreateBookDTO
{
    [Required]
    public string BookName {get; set;} = string.Empty;

    [Required]
    public string BookAuthor {get; set;} = string.Empty;

    [Required]
    public ReadingStatus Status {get; set;} = ReadingStatus.PlanToRead;
}