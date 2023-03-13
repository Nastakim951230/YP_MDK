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

namespace YP_MDK
{
    /// <summary>
    /// Логика взаимодействия для BasketUser.xaml
    /// </summary>
    public partial class BasketUser 
    {
        User user;
        List<ClassProductBasket> baskets;
        List <PointOfIssue> points=ClassPage.ClassBase.BD.PointOfIssue.ToList();
        public BasketUser(User user, List<ClassProductBasket> baskets)
        {
            InitializeComponent();
            if(user != null)
            {
                FIOUser.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }

           this.user = user;
            this.baskets = baskets;
            ListBasket.ItemsSource = baskets;
            address.Items.Add("Не выбрано");
            for(int i = 0; i < points.Count; i++)
            {
                address.Items.Add(points[i].AddressAndIndex);
            }
            address.SelectedIndex = 0;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void order_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletProductBasket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
