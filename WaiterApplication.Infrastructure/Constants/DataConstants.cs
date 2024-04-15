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
        public const string PriceRange = "Price {0} must be between {1} and {2}";

        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public const int MinDescriptionLength = 2;
        public const int MaxDescriptionLength = 200;

        public const int MaxUrlLength = 200;

        public const int MaxTableNameLength = 30;

        public const int MaxOrderDishQuantity = 9;

        public const int UserFirstNameMaxLength = 30;
        public const int UserFirstNameMinLength = 1;

        public const int UserLastNameMaxLength = 40;
        public const int UserLastNameMinLength = 3;

        public const string AdminRole = "Administrator";

        public const int TableNameMinLength = 1;

        public const int MinTableCapacity = 2;
        public const int MaxTableCapacity = 20;
    }
}
