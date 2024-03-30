using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;

namespace WaiterApplication.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;

        public OrderService(IRepository repository)
        {
            _repository = repository;
        }


    }
}
