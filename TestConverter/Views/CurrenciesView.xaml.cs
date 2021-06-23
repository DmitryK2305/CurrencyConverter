using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestConverter.ViewModels;

namespace TestConverter.Views
{
    /// <summary>
    /// Логика взаимодействия для CurrenciesView.xaml
    /// </summary>
    public partial class CurrenciesView : UserControl
    {
        public CurrenciesView()
        {
            InitializeComponent();
            var currenciesViewModel = new CurrenciesViewModel();
            DataContext = currenciesViewModel;            
            ConverterView.DataContext = new ConverterViewModel(currenciesViewModel);            
            CodesView.DataContext = new CodesViewModel(currenciesViewModel);
        }
    }
}
