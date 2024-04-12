using WaiterApplication.Core.Contracts;

namespace WaiterApplication.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IDishModel dish)
        {
            string info = dish.Name.Replace(" ", "-") + GetDescription(dish.Description);

            return info;
        }

        private static string GetDescription(string description)
        {
            description = string.Join
                ("-", description.Split(" "));

            return description;
        }
    }
}
