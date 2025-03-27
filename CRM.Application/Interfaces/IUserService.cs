using CRM.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> AuthenticateAsync(string username, string password);
    Task RegisterAsync(UserDto user);
}
