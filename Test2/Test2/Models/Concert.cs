using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

public class Concert
{
    [Key]
    public int ConcertId { get; set; }
    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string Name { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int AvailableTickets { get; set; }
    
    public ICollection<TicketConcert> TicketConcerts{ get; set; }
}