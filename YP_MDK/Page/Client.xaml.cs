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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client 
    {
        User user;
        List<ClassPage.ClassProductBasket> basket = new List<ClassPage.ClassProductBasket>();

        public Client(User user)
        {
            InitializeComponent();
            ListProduct.ItemsSource = ClassPage.ClassBase.BD.Product.ToList();

            Sortirov.SelectedIndex = 0;
            Filter.SelectedIndex = 0;
            kolvo.Text = "" + ClassPage.ClassBase.BD.Product.ToList().Count() + " из " + ClassPage.ClassBase.BD.Product.ToList().Count();
            this.user = user;
            UsersFio.Text=user.UserSurname+" "+user.UserName+" "+user.UserPatronymic;
        }

        void filter()
        {
            List<Product> products = ClassPage.ClassBase.BD.Product.ToList();

            //Поиск
            if (!string.IsNullOrWhiteSpace(Poisk.Text))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(Poisk.Text.ToLower())).ToList();
            }

            //Сортировка
            if (Sortirov.SelectedIndex > 0)
            {
                switch (Sortirov.SelectedIndex)
                {
                    case 1:
                        {
                            products.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                        }
                        break;
                    case 2:
                        {
                            products.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                            products.Reverse();
                        }
                        break;
                }
            }
            //Филтрация
            if (Filter.SelectedIndex > 0)
            {
                switch (Filter.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.ProductDiscountAmount > 0 && x.ProductDiscountAmount < 9.99).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.ProductDiscountAmount > 10 && x.ProductDiscountAmount < 14.99).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.ProductDiscountAmount > 15).ToList();
                        break;
                }
            }
            ListProduct.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("Нет записей");
                Poisk.Text = "";
                Filter.SelectedIndex = 0;
            }
            kolvo.Text = "" + products.Count() + " из " + ClassPage.ClassBase.BD.Product.ToList().Count();

        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();

        }

        private void Sortirov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Page.Avtorizats.coint = 0;
            ClassPage.FrameNavigate.perehod.Navigate(new Avtorizats());
        }

        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuItem btn = (MenuItem)sender;
            string id = btn.Uid;

            Product index = ClassPage.ClassBase.BD.Product.FirstOrDefault(x => x.ProductArticleNumber == id);
            bool kolvo = false;
            foreach (ClassPage.ClassProductBasket productBasket in basket)
            {
                if (productBasket.productBasket == index)
                {
                    productBasket.count = productBasket.count += 1;
                    kolvo = true;
                }
            }
            if (!kolvo)
            {
                ClassPage.ClassProductBasket product = new ClassPage.ClassProductBasket();
                product.productBasket = index;
                product.count = 1;
                basket.Add(product);
            }
            BasketButton.Visibility = Visibility.Visible;
        }

        private void BasketButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
