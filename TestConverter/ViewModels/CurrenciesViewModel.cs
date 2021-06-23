using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using TestConverter.Lib;
using TestConverter.Model;

namespace TestConverter.ViewModels
{
    public class CurrenciesViewModel : NotifyBase
    {
        public CurrenciesViewModel()
        {
            Currencies = new();
        }

        #region Constants
        private readonly string CurrenciesSourceJSON = "https://www.cbr-xml-daily.ru/daily_json.js";
        private readonly Currency CurrencyRUB = new Currency()
        {
            ID = "R01090B",
            NumCode = 643,
            CharCode = "RUB",
            Nominal = 1,
            Name = "Российский рубль",
            Value = 1M,
            Previous = 1M
        };
        #endregion

        #region UpdateTime : DateTime - Дата обновления
        private DateTime _UpdateTime;
        /// <summary>Дата обновления</summary>
        public DateTime UpdateTime { get => _UpdateTime; set => SetProperty(ref _UpdateTime, value); }
        #endregion
        #region Currencies : Dictionary<string, Currency> - Курсы валют
        private Dictionary<string, Currency> _Currencies;
        /// <summary>Курсы валют</summary>
        public Dictionary<string, Currency> Currencies { get => _Currencies; set => SetProperty(ref _Currencies, value); }
        #endregion   

        #region UpdateCurrenciesCommand : ICommand - Обновить курсы
        private ICommand _UpdateCurrenciesCommand;
        /// <summary>Обновить курсы</summary>
        public ICommand UpdateCurrenciesCommand => _UpdateCurrenciesCommand ??= new RelayCommand(OnUpdateCurrenciesCommandExecuted, CanUpdateCurrenciesCommandExecute);

        private async void OnUpdateCurrenciesCommandExecuted(object p)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response =
                (await httpClient.GetAsync(CurrenciesSourceJSON)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(responseBody);

            JToken list = jObject["Valute"];
            UpdateTime = jObject["Date"].ToObject<DateTime>();
            Dictionary<string, Currency> currencies = list.ToObject<Dictionary<string, Currency>>();
            currencies.Add("RUB", CurrencyRUB);
            Currencies = currencies;
        }

        private bool CanUpdateCurrenciesCommandExecute(object p)
        {
            return true;
        }
        #endregion
    }
}
