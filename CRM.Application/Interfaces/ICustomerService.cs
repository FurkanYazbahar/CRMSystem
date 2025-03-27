using CRM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerDto>> FilterAsync(string? name, string? region, DateTime? registrationDate);
    Task AddAsync(CustomerDto customer);
    Task UpdateAsync(CustomerDto customer);
    Task DeleteAsync(Guid id);
}