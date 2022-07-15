using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.ViewModels
{
    public class CheckoutVM
    {
        [JsonPropertyName("inserted")]
        public HungarianForintVM InsertedMoney { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }
    }
}
