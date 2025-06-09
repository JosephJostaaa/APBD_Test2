using System.ComponentModel.DataAnnotations;

namespace Test2.Dto;

public class CustomerAddDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    
    public List<PurchaseCreateDto> Purchases {get;set;}
}