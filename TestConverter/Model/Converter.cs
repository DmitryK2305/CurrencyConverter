using TestConverter.Lib;

namespace TestConverter.Model
{
    public class Converter : NotifyBase
    {
        public Converter(MultiConverter owner)
        {
            Owner = owner;
            PropertyChanged += Converter_PropertyChanged;
        }

        private void Converter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Currency")
            {
                OnPropertyChanged("Value");
            }
        }

        public MultiConverter Owner { get; }

        private Currency currency;
        public Currency Currency {
            get => currency;
            set => SetProperty(ref currency, value);
        }
        public decimal Value {
            get => Currency is null ? 0 : Owner.ValueRUB / Currency.Value * Currency.Nominal;
            set {
                if (Currency is not null)
                {
                    Owner.ValueRUB = value * Currency.Value / Currency.Nominal;
                }
            }
        }

        public void UpdateCurrency() => OnPropertyChanged("Value");
    }
}
