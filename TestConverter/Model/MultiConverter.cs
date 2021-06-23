using System.Collections.ObjectModel;
using System.ComponentModel;
using TestConverter.Lib;

namespace TestConverter.Model
{
    public class MultiConverter : NotifyBase
    {
        public MultiConverter()
        {
            Converters = new ObservableCollection<Converter>();
            PropertyChanged += MultiConverter_PropertyChanged;
        }

        private void MultiConverter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "ValueRUB")
            {
                updateAllConverters();
            }
        }

        private void updateAllConverters()
        {
            foreach (var converter in Converters)
                converter.UpdateCurrency();
        }

        private decimal valueRUB;
        public decimal ValueRUB {
            get => valueRUB;
            set => SetProperty(ref valueRUB, value);
        }

        public ObservableCollection<Converter> Converters { get; set; }

        public void AddConverter() => Converters.Add(new Converter(this));
    }
}
