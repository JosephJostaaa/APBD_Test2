using ExampleTest2_2025.Exceptions;
using Microsoft.EntityFrameworkCore;
using Test2.DAL;
using Test2.Dto;
using Test2.Models;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly TicketDbContext _context;

    public DbService(TicketDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerPurchaseDto> GetCustomerPurchaseAsync(int customerId, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Include(c => c.PurchasedTickets)
            .ThenInclude(pt => pt.TicketConcert).ThenInclude(ticketConcert => ticketConcert.Ticket)
            .Include(customer => customer.PurchasedTickets)
            .ThenInclude(purchasedTicket => purchasedTicket.TicketConcert)
            .ThenInclude(ticketConcert => ticketConcert.Concert)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);

        if (customer == null)
        {
            return new CustomerPurchaseDto();
        }

        return new CustomerPurchaseDto
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Purchases = customer.PurchasedTickets
                .Select(pt =>
                    new PurchaseDto
                    {
                        Date = pt.PurchasedDate,
                        Price = pt.TicketConcert.Price,
                        Ticket = new TicketDto
                        {
                            SerialNumber = pt.TicketConcert.Ticket.SerialNumber,
                            SeatNumber = pt.TicketConcert.Ticket.SeatNumber
                        },
                        Concert = new ConcertDto
                        {
                            Name = pt.TicketConcert.Concert.Name,
                            Date = pt.TicketConcert.Concert.Date
                        }
                    }
                )
                .ToList()
        };
    }

    public async Task<CustomerAddResponse> AddCustomer(CustomerAddDto customerAddDto,
        CancellationToken cancellationToken)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.PhoneNumber == customerAddDto.PhoneNumber);

        if (customer == null)
        {
            var addCustomer = new Customer
            {
                FirstName = customerAddDto.FirstName,
                LastName = customerAddDto.LastName,
                PhoneNumber = customerAddDto.PhoneNumber,
                CustomerId = customerAddDto.Id
            };
            _context.Customers.Add(
                    addCustomer
                );
            customer = addCustomer;
        }
        
        foreach (var purch in customerAddDto.Purchases)
        {
            var concert = await _context.Concerts
                .FirstOrDefaultAsync(c => c.Name.Equals(purch.ConcertName), cancellationToken);

            if (concert == null)
            {
                throw new NotFoundException("Concert not found");
            }
        }

        foreach (var purch in customerAddDto.Purchases)
        {
            var concert = await _context.Concerts
                .FirstOrDefaultAsync(c => c.Name.Equals(purch.ConcertName), cancellationToken);
            
            var concertCount = await _context.Concerts.CountAsync(c => c.Name.Equals(purch.ConcertName), cancellationToken);

            if (concertCount > 5)
            {
                return new CustomerAddResponse{Success = false, Message = "Can not purchase more then 5 tickets"};
            }
            Ticket ticket = new Ticket
            {
                SerialNumber = "JDHFKJSD/" + new Random().Next(1000, 9999),
                SeatNumber = purch.SeatNumber
            };
            
            _context.Tickets.Add(ticket);

            TicketConcert ticketConcert = new TicketConcert
            {
                Concert = concert,
                Ticket = ticket,
                Price = purch.Price
            };
            
            _context.TicketConcerts.Add(ticketConcert);

            PurchasedTicket purchasedTicket = new PurchasedTicket
            {
                CustomerId = customer.CustomerId,
                TicketConcert = ticketConcert,
                PurchasedDate = DateTime.Now
            };
            
            _context.PurchasedTickets.Add(purchasedTicket);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CustomerAddResponse {Success = true};
    }
}