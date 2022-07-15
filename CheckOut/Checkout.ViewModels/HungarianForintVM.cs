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

        public HungarianForintVM() { }

        public HungarianForintVM(HungarianForint currency)
        {
            currency.ThrowIfNull();

            Five = currency.Five;
            Ten = currency.Ten;
            Twenty = currency.Twenty;
            Fifty = currency.Fifty;
            Hundred = currency.Hundred;
            TwoHundred = currency.TwoHundred;
            FiveHundred = currency.FiveHundred;
            Thousand = currency.Thousand;
            TwoThousand = currency.TwoThousand;
            FiveThousand = currency.FiveThousand;
            TenThousand = currency.TenThousand;
            TwentyThousand = currency.TwentyThousand;
        }
        public HungarianForint GetModel()
        {
            var model = new HungarianForint();

            model.Five = Five;
            model.Ten = Ten;
            model.Twenty = Twenty;
            model.Fifty = Fifty;
            model.Hundred = Hundred;
            model.TwoHundred = TwoHundred;
            model.FiveHundred = FiveHundred;
            model.Thousand = Thousand;
            model.TwoThousand = TwoThousand;
            model.FiveThousand = FiveThousand;
            model.TenThousand = TenThousand;
            model.TwentyThousand = TwentyThousand;

            return model;
        }
    }
}
