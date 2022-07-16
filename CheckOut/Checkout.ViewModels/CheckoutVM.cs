using System.Text.Json.Serialization;

namespace Checkout.ViewModels
{
    public class CheckoutVM
    {
        [JsonPropertyName("inserted")]
        public CurrencyVM InsertedMoney { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }
    }
}
