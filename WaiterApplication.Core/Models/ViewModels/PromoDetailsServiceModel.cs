using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Models.ViewModels
{
    public class PromoDetailsServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<PromoDish> Dishes { get; set; } = null!;
    }
}
