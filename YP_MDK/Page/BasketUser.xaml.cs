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
        }

    }
}
