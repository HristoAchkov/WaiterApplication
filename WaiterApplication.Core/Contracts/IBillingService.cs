using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Contracts
{
    public interface IBillingService
    {
        Task<decimal> CalculateBill(decimal total);
        Task InitiatePayment();
    }
}
