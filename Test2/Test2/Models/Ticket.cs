using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    public int TicketId { get; set; }
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string SerialNumber { get; set; }
    [Required]
    public int SeatNumber { get; set; }
    
    public ICollection<TicketConcert> TicketConcerts{ get; set; }
}