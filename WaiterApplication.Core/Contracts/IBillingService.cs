using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Contracts
{
    public interface IBillingService
    {
        Task<BillViewModel> CalculateBill(int orderId);
        Task<Bill> CreateBill(BillViewModel model);
        Task<bool> BillExistsAsync(string billId);
        Task RemoveBill(int billId);
        Task<BillViewModel> BillDetailsByIdAsync(int billId);
        Task IsOrderPaid(int orderId);
    }
}
