using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Customer")]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Column(TypeName = "NVARCHAR")]
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    
    public ICollection<PurchasedTicket> PurchasedTickets { get; set; }
}