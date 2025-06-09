using System.ComponentModel.DataAnnotations;

namespace Test2.Dto;

public class PurchaseCreateDto
{
    [Required]
    public int SeatNumber { get; set; }
    [Required]
    public string ConcertName { get; set; }
    [Required]
    public decimal Price { get; set; }
}