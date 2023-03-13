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
        double summaCoint;
        double summaSkidki;
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
            int coint=baskets.Count;
            for(int i = 0; i < coint; i++)
            {
                if(baskets[i].productBasket.ProductDiscountAmount != 0)
                {
                    summaSkidki= summaSkidki + (Convert.ToDouble(baskets[i].productBasket.ProductCost) * baskets[i].count);
                    summaCoint = summaCoint + ((double)(Convert.ToDouble(baskets[i].productBasket.ProductCost) - (Convert.ToDouble(baskets[i].productBasket.ProductCost) * baskets[i].productBasket.ProductDiscountAmount / 100)) * baskets[i].count);
                }
                else
                {
                    summaSkidki = summaSkidki + (Convert.ToDouble(baskets[i].productBasket.ProductCost) * baskets[i].count);
                    summaCoint = summaCoint + (Convert.ToDouble(baskets[i].productBasket.ProductCost) * baskets[i].count);
                }
            }
            double skidka = (summaSkidki - summaCoint) * 100 / summaSkidki;
            string itogskidka = string.Format("{0:F2}", skidka);
            ItogCoint.Text = "Итоговая цена: " + summaCoint;
            ItogSkidka.Text = "Итоговая скидка: " + itogskidka + "%";
        }

       

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            if(user!=null)
            {
               if(user.UserRole==1)
                {
                    Page.Str_Gost.Basket = baskets;
                    ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                }
               else if(user.UserRole==2)
                {
                    Page.Str_Gost.Basket = baskets;
                    ClassPage.FrameNavigate.perehod.Navigate(new Page.Admin(user));
                }
                else if (user.UserRole == 3)
                {
                    Page.Str_Gost.Basket = baskets;
                    ClassPage.FrameNavigate.perehod.Navigate(new Page.Manager(user));
                }
            }
            else
            {
                Page.Str_Gost.Basket = baskets;
                ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
            }

        }

        private void order_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletProductBasket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClassProductBasket x=(ClassProductBasket)ListBasket.SelectedItem;
            
        }
    }
}
