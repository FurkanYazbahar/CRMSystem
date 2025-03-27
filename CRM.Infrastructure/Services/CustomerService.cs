using CRM.Application.DTOs;
using CRM.Application.Interfaces;
using CRM.Domain.Entities;

namespace CRM.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> _repo;

    public CustomerService(IGenericRepository<Customer> repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _repo.GetAllAsync();
        return customers.Select(c => ToDto(c));
    }

    public async Task<CustomerDto?> GetByIdAsync(Guid id)
    {
        var customer = await _repo.GetByIdAsync(id);
        return customer == null ? null : ToDto(customer);
    }

    public async Task<IEnumerable<CustomerDto>> FilterAsync(string? name, string? region, DateTime? date)
    {
        var customers = await _repo.FindAsync(c =>
            (string.IsNullOrEmpty(name) || c.FirstName.Contains(name) || c.LastName.Contains(name)) &&
            (string.IsNullOrEmpty(region) || c.Region == region) &&
            (!date.HasValue || c.RegistrationDate.Date == DateTime.SpecifyKind(date.Value.Date, DateTimeKind.Utc))
        );

        return customers.Select(ToDto);
    }

    public async Task AddAsync(CustomerDto dto)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Region = dto.Region,
            RegistrationDate = DateTime.SpecifyKind(dto.RegistrationDate, DateTimeKind.Utc)
        };

        await _repo.AddAsync(customer);
    }

    public async Task UpdateAsync(CustomerDto dto)
    {
        var customer = await _repo.GetByIdAsync(dto.Id);
        if (customer == null) return;

        customer.FirstName = dto.FirstName;
        customer.LastName = dto.LastName;
        customer.Email = dto.Email;
        customer.Region = dto.Region;
        customer.RegistrationDate = DateTime.SpecifyKind(dto.RegistrationDate, DateTimeKind.Utc);

        await _repo.UpdateAsync(customer);
    }

    public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

    private static CustomerDto ToDto(Customer c) => new()
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        Email = c.Email,
        Region = c.Region,
        RegistrationDate = DateTime.SpecifyKind(c.RegistrationDate, DateTimeKind.Utc)
    };
}
