using System.Collections.Generic;

namespace CQRSRentACar.Models
{
    public class FuelPriceViewModel
    {
        public bool Success { get; set; }
        public List<CountryFuelPrice>? Result { get; set; }
    }

    public class CountryFuelPrice
    {
        public string? Country { get; set; }
        public string? Currency { get; set; }
        public string? Gasoline { get; set; }
        public string? Diesel { get; set; }
        public string? LPG { get; set; }
        public string? E85 { get; set; }
        
        public double GasolinePrice => ParsePrice(Gasoline);
        public double DieselPrice => ParsePrice(Diesel);
        
        private double ParsePrice(string? price)
        {
            if (string.IsNullOrEmpty(price) || price == "-" || price == "0,000")
                return 0.0;
            
            return double.Parse(price.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
