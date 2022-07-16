using Checkout.Core.Extensions;
using Checkout.Model;
using System.Text.Json.Serialization;

namespace Checkout.ViewModels
{
    public class HungarianForintVM
    {
        [JsonPropertyName("5")]
        public int Five { get; set; }

        [JsonPropertyName("10")]
        public int Ten { get; set; }

        [JsonPropertyName("20")]
        public int Twenty { get; set; }
        [JsonPropertyName("50")]
        public int Fifty { get; set; }

        [JsonPropertyName("100")]
        public int Hundred { get; set; }
        [JsonPropertyName("200")]
        public int TwoHundred { get; set; }

        [JsonPropertyName("500")]
        public int FiveHundred { get; set; }

        [JsonPropertyName("1000")]
        public int Thousand { get; set; }

        [JsonPropertyName("2000")]
        public int TwoThousand { get; set; }

        [JsonPropertyName("5000")]
        public int FiveThousand { get; set; }

        [JsonPropertyName("10000")]
        public int TenThousand { get; set; }

        [JsonPropertyName("20000")]
        public int TwentyThousand { get; set; }

        public bool IsPositive 
        {
            get
            {
                return Five >= 0 &&
                    Ten >= 0 &&
                    Twenty >= 0 &&
                    Fifty >= 0 &&
                    Hundred >= 0 &&
                    TwoHundred >= 0 &&
                    FiveHundred >= 0 &&
                    Thousand >= 0 &&
                    TwoThousand >= 0 &&
                    FiveThousand >= 0 &&
                    TenThousand >= 0 &&
                    TwentyThousand >= 0;
            } 
        }


        public HungarianForintVM() { }

        public HungarianForintVM(HungarianForint currency)
        {
            Five = currency.GetCount(5);
            Ten = currency.GetCount(10);
            Twenty = currency.GetCount(20);
            Fifty = currency.GetCount(50);
            Hundred = currency.GetCount(100);
            TwoHundred = currency.GetCount(200);
            FiveHundred = currency.GetCount(500);
            Thousand = currency.GetCount(1000);
            TwoThousand = currency.GetCount(2000);
            FiveThousand = currency.GetCount(5000);
            TenThousand = currency.GetCount(10000);
            TwentyThousand = currency.GetCount(20000);
        }
        public HungarianForint GetModel()
        {
            var model = new HungarianForint();

            model.AddCount(5, Five);
            model.AddCount(10, Ten);
            model.AddCount(20, Twenty);
            model.AddCount(50, Fifty);
            model.AddCount(100, Hundred);
            model.AddCount(200, TwoHundred);
            model.AddCount(500, FiveHundred);
            model.AddCount(1000, Thousand);
            model.AddCount(2000, TwoThousand);
            model.AddCount(5000, FiveThousand);
            model.AddCount(10000, TenThousand);
            model.AddCount(20000, TwentyThousand);

            return model;
        }    
    }
}
