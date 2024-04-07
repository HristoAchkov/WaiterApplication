using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Common;

namespace WaiterApplication.Core.Contracts
{
    public interface IMenuService
    {
        Task<bool> DishExistsAsync(string dishId);
        Task AddDishAsync(string name, string description, string? imageUrl, decimal price, string? ingredients);
    }
}
