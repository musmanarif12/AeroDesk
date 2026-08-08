namespace AeroDesk.Application.Features.Reports.Common
{
    public static class TravelClassPricing
    {
        public static readonly Dictionary<string, decimal> PriceByClass = new()
        {
            { "Economy", 15000m },
            { "Business", 45000m },
            { "First Class", 90000m }
        };

        public static decimal GetPrice(string travelClass)
        {
            return PriceByClass.TryGetValue(travelClass, out var price) ? price : 0m;
        }
    }
}