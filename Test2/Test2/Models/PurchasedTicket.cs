using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Purchased_Ticket")]
[PrimaryKey(nameof(TicketConcertId), nameof(CustomerId))]
public class PurchasedTicket
{
    public int TicketConcertId { get; set; }
    public int CustomerId { get; set; }
    public DateTime PurchasedDate { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public TicketConcert TicketConcert { get; set; }
    
    [ForeignKey(nameof(TicketConcertId))]
    public Customer Customer { get; set; }
    
}