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
        string itogskidka;
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
             itogskidka = string.Format("{0:F2}", skidka);
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
            try
            {
                List<Product> products = ClassPage.ClassBase.BD.Product.ToList();
               DateTime date= DateTime.Today;
                string dateDelivery = "";
                bool position = false;
                for(int i = 0; i < baskets.Count; i++)
                {
                    Product product = products.FirstOrDefault(x => x.ProductArticleNumber == baskets[i].productBasket.ProductArticleNumber);
                    if (product.ProductQuantityInStock<=3)
                    {
                       position = true;
                    }
                   
                }
                if(position==true)
                {
                   date= date.AddDays(6);
                    dateDelivery = "6 дней";
                }
                else
                {
                    date = date.AddDays(3);
                    dateDelivery = "3 дня";
                }
                List<Order> kodOrder = ClassPage.ClassBase.BD.Order.ToList();
                int kolvoProductov=kodOrder.Count;
                int kod = (kodOrder[kolvoProductov-1].CodeToReceive)+1;

               perehod: List<Order> orders = kodOrder.Where(x => x.CodeToReceive == kod && x.OrderStatus==1).ToList();
                if(orders.Count>0)
                {
                    kod = kod + 1;
                    goto perehod;
                }
                else
                {
                    if (address.SelectedIndex > 0)
                    {
                        Order order = new Order();
                        order.OrderStatus = 1;
                        order.OrderDeliveryDate = DateTime.Today;
                        order.OrderPickupPoint = address.SelectedIndex;
                        order.OrderDate = date;
                        order.CodeToReceive = kod;
                        if(user==null)
                        {
                            order.OrderUser = null;
                        }
                        else
                        {
                            order.OrderUser = user.UserID;
                        }
                        ClassPage.ClassBase.BD.Order.Add(order);
                       


                        for (int i = 0; i < baskets.Count; i++)
                        {
                            OrderProduct orderProduct = new OrderProduct();
                            orderProduct.OrderID=order.OrderID;
                            orderProduct.ProductArticleNumber = baskets[i].productBasket.ProductArticleNumber;
                            orderProduct.kolvo = baskets[i].count;
                            ClassPage.ClassBase.BD.OrderProduct.Add(orderProduct);
                           
                        }
                        ClassPage.ClassBase.BD.SaveChanges();
                        MessageBox.Show("Заказ успешно создан");
                        Ticket ticket = new Ticket(order, baskets, summaCoint, itogskidka, dateDelivery);
                        ticket.ShowDialog();
                        baskets.Clear();
                        if (user != null)
                        {
                            if (baskets.Count == 0)
                            {
                                baskets = null;
                            }
                            if (user.UserRole == 1)
                            {
                                Page.Str_Gost.Basket = baskets;
                                ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                            }
                            else if (user.UserRole == 2)
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
                            if (baskets.Count == 0)
                            {
                                baskets = null;
                            }
                            Page.Str_Gost.Basket = baskets;
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберить пункт выдачи");
                    }
                }
               

            }
            catch
            {
                MessageBox.Show("При создание заказа возникла ошибка!");
            }
        }

        private void DeletProductBasket_Click(object sender, RoutedEventArgs e)
        {
            ClassProductBasket productBasket = new ClassProductBasket();
            Button btn = (Button)sender;
            string id = btn.Uid;
            if (id != null)
            {
                 productBasket=baskets.FirstOrDefault(x=>x.productBasket.ProductArticleNumber == id);
                baskets.Remove(productBasket);
                if (baskets.Count > 0)
                {
                    ClassPage.FrameNavigate.perehod.Navigate(new BasketUser(user, baskets));
                }
                else
                {
                    if (user != null)
                    {
                        if(baskets.Count==0)
                        {
                            baskets = null;
                        }
                        if (user.UserRole == 1)
                        {
                            Page.Str_Gost.Basket = baskets;
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                        }
                        else if (user.UserRole == 2)
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
                        if (baskets.Count == 0)
                        {
                            baskets = null;
                        }
                        Page.Str_Gost.Basket = baskets;
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void CountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClassProductBasket productBasket = new ClassProductBasket();
            TextBox btn = (TextBox)sender;
            string id = btn.Uid;
            if(id!=null)
            {
                productBasket = baskets.FirstOrDefault(x => x.productBasket.ProductArticleNumber == id);
                if(btn.Text.Replace(" ", "") == "")
                {
                    productBasket.count = 0;
                }
                else
                {
                    
                    productBasket.count = Convert.ToInt32(btn.Text);
                    
                }
               if(productBasket.count == 0)
                {
                    baskets.Remove(productBasket);
                    if (baskets.Count > 0)
                    {
                        ClassPage.FrameNavigate.perehod.Navigate(new BasketUser(user, baskets));
                    }
                    else
                    {
                        if (user != null)
                        {
                            if (baskets.Count == 0)
                            {
                                baskets = null;
                            }
                            if (user.UserRole == 1)
                            {
                                Page.Str_Gost.Basket = baskets;
                                ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                            }
                            else if (user.UserRole == 2)
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
                            if (baskets.Count == 0)
                            {
                                baskets = null;
                            }
                            Page.Str_Gost.Basket = baskets;
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
                        }
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
