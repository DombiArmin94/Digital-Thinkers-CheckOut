

using Checkout.Core.Extensions;

namespace Checkout.Model
{
    public struct HungarianForint
    {
        private int _five;
        private int _ten;
        private int _twenty;
        private int _fifty;
        private int _hundred;
        private int _twoHundred;
        private int _fiveHundred;
        private int _thousand;
        private int _twoThousand;
        private int _fiveThousand;
        private int _tenThousand;
        private int _twentyThousand;


        public int Five
        {
            get
            {
                return _five;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Five));

                _five = value;
            }
        }
        public int Ten
        {
            get
            {
                return _ten;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Ten));

                _ten = value;
            }
        }
        public int Twenty
        {
            get
            {
                return _twenty;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Twenty));

                _twenty = value;
            }
        }
        public int Fifty
        {
            get
            {
                return _fifty;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Fifty));

                _fifty = value;
            }
        }
        public int Hundred
        {
            get
            {
                return _hundred;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Hundred));

                _hundred = value;
            }
        }
        public int TwoHundred
        {
            get
            {
                return _twoHundred;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(TwoHundred));

                _twoHundred = value;
            }
        }
        public int FiveHundred
        {
            get
            {
                return _fiveHundred;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(FiveHundred));

                _fiveHundred = value;
            }
        }
        public int Thousand
        {
            get
            {
                return _thousand;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(Thousand));

                _thousand = value;
            }
        }
        public int TwoThousand
        {
            get
            {
                return _twoThousand;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(TwoThousand));

                _twoThousand = value;
            }
        }
        public int FiveThousand
        {
            get
            {
                return _fiveThousand;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(FiveThousand));

                _fiveThousand = value;
            }
        }
        public int TenThousand
        {
            get
            {
                return _tenThousand;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(TenThousand));

                _tenThousand = value;
            }
        }
        public int TwentyThousand
        {
            get
            {
                return _twentyThousand;
            }
            set
            {
                value.ThrowIfLessThanZero(nameof(TwentyThousand));

                _twentyThousand = value;
            }
        }

        public int Sum
        {
            get
            {
                var sum = 0;

                sum += Five * 5;
                sum += Ten * 10;
                sum += Twenty * 20;
                sum += Fifty * 50;
                sum += Hundred * 100;
                sum += TwoHundred * 200;
                sum += FiveHundred * 500;
                sum += Thousand * 1000;
                sum += TwoThousand * 2000;
                sum += FiveThousand * 5000;
                sum += TenThousand * 10000;
                sum += TwentyThousand * 20000;

                return sum;
            }
        }

        public void FillUpStock(HungarianForint additionalStock)
        {
            Five += additionalStock.Five;
            Ten += additionalStock.Ten;
            Twenty += additionalStock.Twenty;
            Fifty += additionalStock.Fifty;
            Hundred += additionalStock.Hundred;
            TwoHundred += additionalStock.TwoHundred;
            FiveHundred += additionalStock.FiveHundred;
            Thousand += additionalStock.Thousand;
            TwoThousand += additionalStock.TwoThousand;
            FiveThousand += additionalStock.FiveThousand;
            TenThousand += additionalStock.TenThousand;
            TwentyThousand += additionalStock.TwentyThousand;
        }

        public (HungarianForint? change, string errorMessage) CalculateChange(int changeSum)
        {
            if(Sum < changeSum)
            {
                return (null, "Not enough money to give back change");
            }

            var change = new HungarianForint();

            if(TwentyThousand > 0 )
            {
                var amount = changeSum / 20000;
                change.TwentyThousand = amount < TwentyThousand ? amount : TwentyThousand;
                changeSum = changeSum - change.TwentyThousand * 20000;
                TwentyThousand -= change.TwentyThousand;
            }

            if (TenThousand > 0)
            {
                var amount = changeSum / 10000;
                change.TenThousand = amount < TenThousand ? amount : TenThousand;
                changeSum = changeSum - change.TenThousand * 10000;
                TenThousand -= change.TenThousand;
            }

            if (FiveThousand > 0)
            {
                var amount = changeSum / 5000;
                change.FiveThousand = amount < FiveThousand ? amount : FiveThousand;
                changeSum = changeSum - change.FiveThousand * 5000;
                FiveThousand -= change.FiveThousand;
            }

            if (TwoThousand > 0)
            {
                var amount = changeSum / 2000;
                change.TwoThousand = amount < TwoThousand ? amount : TwoThousand;
                changeSum = changeSum - change.TwoThousand * 2000;
                TwoThousand -= change.TwoThousand;
            }

            if (Thousand > 0)
            {
                var amount = changeSum / 1000;
                change.Thousand = amount < Thousand ? amount : Thousand;
                changeSum = changeSum - change.Thousand * 1000;
                Thousand -= change.Thousand;
            }

            if (FiveHundred > 0)
            {
                var amount = changeSum / 500;
                change.FiveHundred = amount < FiveHundred ? amount : FiveHundred;
                changeSum = changeSum - change.FiveHundred * 500;
                FiveHundred -= change.FiveHundred;
            }

            if (TwoHundred > 0)
            {
                var amount = changeSum / 200;
                change.TwoHundred = amount < TwoHundred ? amount : TwoHundred;
                changeSum = changeSum - change.TwoHundred * 200;
                TwoHundred -= change.TwoHundred;
            }

            if (Hundred > 0)
            {
                var amount = changeSum / 100;
                change.Hundred = amount < Hundred ? amount : Hundred;
                changeSum = changeSum - change.Hundred * 100;
                Hundred -= change.Hundred;
            }

            if (Fifty > 0)
            {
                var amount = changeSum / 50;
                change.Fifty = amount < Fifty ? amount : Fifty;
                changeSum = changeSum - change.Fifty * 50;
                Fifty -= change.Fifty;
            }

            if (Twenty > 0)
            {
                var amount = changeSum / 20;
                change.Twenty = amount < Twenty ? amount : Twenty;
                changeSum = changeSum - change.Twenty * 20;
                Twenty -= change.Twenty;
            }

            if (Ten > 0)
            {
                var amount = changeSum / 10;
                change.Ten = amount < Ten ? amount : Ten;
                changeSum = changeSum - change.Ten * 10;
                Ten -= change.Ten;
            }

            if (Five > 0)
            {
                var amount = changeSum / 5;
                change.Five = amount < Five ? amount : Five;
                changeSum = changeSum - change.Five * 5;
                Five -= change.Five;
            }

            if(changeSum > 5)
            {
                return (null, "No change available!");
            }
            else if (changeSum < 5 && changeSum >= 3)
            {
                if(Five == 0)
                {
                    return (null, "No change available!");
                }
                else
                {
                    change.Five += 1;
                }
            }

            return (change, null);
        }
    }
}
