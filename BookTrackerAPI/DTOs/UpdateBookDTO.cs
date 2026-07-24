using System.ComponentModel.DataAnnotations;
using BookTrackerAPI.Models;

namespace BookTrackerAPI.DTOs;

public class UpdateBookDTO
{
    [Range(1, 5)]
    public int? BookRating {get; set;}
    public ReadingStatus? Status {get; set;}
}