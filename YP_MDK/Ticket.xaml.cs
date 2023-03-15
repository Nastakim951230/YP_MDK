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
using System.Windows.Shapes;

namespace YP_MDK
{
    /// <summary>
    /// Логика взаимодействия для Ticket.xaml
    /// </summary>
    public partial class Ticket : Window
    {
        Order order; // 
        List<ClassProductBasket> baskets;
        PointOfIssue issues;
        double summaCoint;
        string summaDiscount;
        string dateDelivery;
        public Ticket(Order order, List<ClassProductBasket> baskets, double summaCoint, string summaDiscount, string dateDelivery)
        {
            InitializeComponent();
            this.order = order;
            this.baskets = baskets;
            this.summaCoint = summaCoint;
            this.summaDiscount = summaDiscount;
            this.dateDelivery = dateDelivery;
            NomerZakaza.Text= "Нормер заказа: "+order.OrderID.ToString();
            dataZakaz.Text = "Дата заказа: " + string.Format("{0:dd MMMM yyyy}", order.OrderDeliveryDate);
            dataOfReceiving.Text = "Дата получения: " + string.Format("{0:dd MMMM yyyy}", order.OrderDate);
            day.Text = "Срок доставки: " + dateDelivery;
            issues = ClassPage.ClassBase.BD.PointOfIssue.FirstOrDefault(x => x.OrderPickupPointID == order.OrderPickupPoint);
            PickupPoint.Text = "Пункт выдачи: " + issues.Address + "  Индекс пункта: " + issues.Index_zakaz;
            Kod.Text = "Код получения: " + order.CodeToReceive;
            Summa.Text = "Итог:"+summaCoint.ToString();
            Skidka.Text = "Скидка: "+summaDiscount+"%";
            listZakaz.ItemsSource = baskets;
        }

        private void document_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
