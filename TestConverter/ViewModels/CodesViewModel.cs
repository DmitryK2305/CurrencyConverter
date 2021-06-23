using System.Windows.Input;
using TestConverter.Lib;
using TestConverter.Model;

namespace TestConverter.ViewModels
{
    public class CodesViewModel : NotifyBase
    {
        public CodesViewModel(CurrenciesViewModel currenciesViewModel)
        {
            MultiConverter = new MultiConverter();
            CurrenciesViewModel = currenciesViewModel;
        }

        public CurrenciesViewModel CurrenciesViewModel { get; set; }


        public MultiConverter MultiConverter { get; set; }


        #region SelectedCurrencyName : string - Выбранная валюта
        private string _SelectedCurrencyName;
        /// <summary>Выбранная валюта</summary>
        public string SelectedCurrencyName { get => _SelectedCurrencyName; set => SetProperty(ref _SelectedCurrencyName, value); }
        #endregion

        #region CheckCurrencyCommand : ICommand - Проверить валюту
        private ICommand _CheckCurrencyCommand;
        /// <summary>Проверить валюту</summary>
        public ICommand CheckCurrencyCommand => _CheckCurrencyCommand ??= new RelayCommand(OnCheckCurrencyCommandExecuted, CanCheckCurrencyCommandExecute);

        private void OnCheckCurrencyCommandExecuted(object p)
        {
            try
            {
                var rub = CurrenciesViewModel.Currencies["RUB"];
                var eng = CurrenciesViewModel.Currencies["USD"];
                MultiConverter.Converters.Clear();
                MultiConverter.Converters.Add(new Converter(MultiConverter) { Currency = rub });
                MultiConverter.Converters.Add(new Converter(MultiConverter) { Currency = eng });
                var cur = p as Currency;
                MultiConverter.ValueRUB = cur.Value / cur.Nominal;
                SelectedCurrencyName = cur.CharCode;
            }
            catch { }
        }

        private bool CanCheckCurrencyCommandExecute(object p)
        {
            return true;
        }
        #endregion
    }
}
