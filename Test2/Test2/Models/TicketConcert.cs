using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Ticket_Concert")]
public class TicketConcert
{
    [Key]
    public int TicketConcertId { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public int ConcertId { get; set; }
    [Precision(10, 2)]
    [Required]
    public decimal Price { get; set; }
    
    [ForeignKey(nameof(TicketId))]
    public Ticket Ticket { get; set; }
    [ForeignKey(nameof(ConcertId))]
    public Concert Concert { get; set; }
    
    public ICollection<PurchasedTicket> PurchasedTickets { get; set; }
}