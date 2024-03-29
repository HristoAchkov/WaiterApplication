using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterApplication.Infrastructure.Constants
{
    public static class DataConstants
    {
        public const string TableNumbersErrorMessage = "Table numbers {0} must be between {1} and {2}";

        public const int MinNameLength = 3;
        public const int MaxNameLength = 30;

        public const int MinDescriptionLength = 2;
        public const int MaxDescriptionLength = 200;

        public const int MaxUrlLength = 200;

        public const int MaxTableNameLength = 20;

        public const int MaxOrderDishQuantity = 9;
    }
}
