namespace Test2.Dto;

public class PurchaseDto
{
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public TicketDto Ticket { get; set; }
    public ConcertDto Concert { get; set; }
}