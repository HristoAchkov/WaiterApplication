using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Contracts
{
    public interface IOrderService
    {
        Task<bool> ExistsByIdAsync(string id);
        Task CreateAsync(ICollection<OrderDish> ordered);
    }
}
