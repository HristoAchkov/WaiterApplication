using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;

namespace WaiterApplication.Core.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRepository repository;

        public BillingService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<decimal> CalculateBill(decimal total)
        {
            return 0;
        }

        public Task InitiatePayment()
        {
            return null;
        }
    }
}
