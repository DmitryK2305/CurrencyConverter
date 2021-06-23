using System.Windows.Input;
using TestConverter.Lib;
using TestConverter.Model;

namespace TestConverter.ViewModels
{
    public class ConverterViewModel : NotifyBase
    {
        public ConverterViewModel(CurrenciesViewModel currenciesViewModel)
        {
            CurrenciesViewModel = currenciesViewModel;
            MultiConverter = new();
            MultiConverter.AddConverter();
            MultiConverter.AddConverter();
        }

        public CurrenciesViewModel CurrenciesViewModel { get; set; }

        public MultiConverter MultiConverter { get; set; }

        #region DeleteConverterCommand : ICommand - Удалить конвертер
        private ICommand _DeleteConverterCommand;
        /// <summary>Удалить конвертер</summary>
        public ICommand DeleteConverterCommand => _DeleteConverterCommand ??= new RelayCommand(OnDeleteConverterCommandExecuted, CanDeleteConverterCommandExecute);

        private void OnDeleteConverterCommandExecuted(object p)
        {
            MultiConverter.Converters.Remove(p as Converter);
        }

        private bool CanDeleteConverterCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region AddConverterCommand : ICommand - Добавить конвертер
        private ICommand _AddConverterCommand;
        /// <summary>Добавить конвертер</summary>
        public ICommand AddConverterCommand => _AddConverterCommand ??= new RelayCommand(OnAddConverterCommandExecuted, CanAddConverterCommandExecute);

        private void OnAddConverterCommandExecuted(object p)
        {
            MultiConverter.AddConverter();
        }

        private bool CanAddConverterCommandExecute(object p)
        {
            return true;
        }
        #endregion
    }
}
