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

namespace YP_MDK.Page
{
    /// <summary>
    /// Логика взаимодействия для Zakaz.xaml
    /// </summary>
    public partial class Zakaz 
    {
        public Zakaz()
        {
            InitializeComponent();
            listoRDER.ItemsSource = ClassPage.ClassBase.BD.Order.ToList();
            Sortirov.SelectedIndex = 0;
            Filter.SelectedIndex = 0;

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sortirov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
