using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Core.Contracts
{
    public interface IDishModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
