using Test2.Dto;

namespace Test2.Services;

public interface IDbService
{
    public Task<CustomerPurchaseDto> GetCustomerPurchaseAsync(int customerId, CancellationToken cancellationToken);

    public Task<CustomerAddResponse> AddCustomer(CustomerAddDto customerAddDto, CancellationToken cancellationToken);
}