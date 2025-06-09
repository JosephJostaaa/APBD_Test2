namespace Test2.Dto;

public class CustomerPurchaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    
    public List<PurchaseDto> Purchases { get; set; }
    
}