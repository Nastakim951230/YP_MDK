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
        User user;
        public Zakaz(User user)
        {
            InitializeComponent();
            this.user = user;
            listoRDER.ItemsSource = ClassPage.ClassBase.BD.Order.ToList();
            Sortirov.SelectedIndex = 0;
            Filter.SelectedIndex = 0;

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            if(user.UserRole==3)
            {
                ClassPage.FrameNavigate.perehod.Navigate(new Page.Manager(user));
            }
            else if(user.UserRole==2)
            {
                ClassPage.FrameNavigate.perehod.Navigate(new Page.Admin(user));
            }
        }

        /// <summary>
        /// метод поиска, фильтрации и сортировки
        /// </summary>
        void Filt()
        {

            List<Order> orders = ClassPage.ClassBase.BD.Order.ToList();

            //Сортировка
            if (Sortirov.SelectedIndex > 0)
            {
                try
                {


                    switch (Sortirov.SelectedIndex)
                    {
                        case 1:
                            orders.Sort((x, y) => x.costSkidka.CompareTo(y.costSkidka));
                            break;
                        case 2:
                            orders.Sort((x, y) => x.costSkidka.CompareTo(y.costSkidka));
                            orders.Reverse();
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }

            if (Filter.SelectedIndex > 0)
            {
                switch (Filter.SelectedIndex)
                {
                    case 1:
                        orders = orders.Where(x => ((x.Skidka >0) && (x.Skidka <= 10))).ToList();
                        break;
                    case 2:
                        orders = orders.Where(x => ((x.Skidka > 11) && (x.Skidka <= 14))).ToList();
                        break;
                    case 3:
                        orders = orders.Where(x => x.Skidka > 15).ToList();
                        break;
                }

            }

            if(orders.Count== 0)
            {
                Filter.SelectedIndex = 0;
                MessageBox.Show("Данных нет");
            }
            listoRDER.ItemsSource=orders;
        }

        private void Sortirov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Filt();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Filt();
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            Order index = (Order)listoRDER.SelectedItem;
            Izmenenie izmenenie = new Izmenenie(index);
            izmenenie.ShowDialog();
            ClassPage.FrameNavigate.perehod.Navigate(new Zakaz(user));
        }
    }
}
