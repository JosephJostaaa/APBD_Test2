using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.DAL;

public class TicketDbContext : DbContext
{
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    
    protected TicketDbContext()
    {
    }

    public TicketDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Ticket>().HasData(
                new Ticket{TicketId = 1, SerialNumber = "TK2034/S4531/12", SeatNumber = 124},
                new Ticket{TicketId = 2, SerialNumber = "TK2034/S4831/133", SeatNumber = 124}
            );

        modelBuilder.Entity<Concert>().HasData(
                new Concert { ConcertId = 1, Name = "Concert 1", Date = DateTime.Today, AvailableTickets = 12 },
                new Concert { ConcertId = 2, Name = "Concert 14", Date = DateTime.Today, AvailableTickets = 23 }
            );

        modelBuilder.Entity<TicketConcert>().HasData(
                new TicketConcert{TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = new decimal(33.4)},
                new TicketConcert{TicketConcertId = 2, TicketId = 2, ConcertId = 2, Price = new decimal(48.4)}
            );
        modelBuilder.Entity<Customer>().HasData(
                new Customer{CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "1232423"}
            );

        modelBuilder.Entity<PurchasedTicket>()
            .HasData(
                new PurchasedTicket{TicketConcertId = 1, CustomerId = 1, PurchasedDate = DateTime.Today}
            );
    }
}