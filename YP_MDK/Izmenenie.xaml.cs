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
    /// Логика взаимодействия для Izmenenie.xaml
    /// </summary>
    public partial class Izmenenie : Window
    {
        Order order;
        public Izmenenie(Order order)
        {
            InitializeComponent();
            this.order = order;
            List<Status> status = ClassPage.ClassBase.BD.Status.ToList();
            for(int i = 0; i < status.Count; i++)
            {
                statusCB.Items.Add(status[i].Status1);
            }
            statusCB.SelectedIndex = order.OrderStatus-1;
            date.SelectedDate = order.OrderDate;

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sohran_Click(object sender, RoutedEventArgs e)
        {
          
            order.OrderStatus = statusCB.SelectedIndex + 1;
            order.OrderDate = (DateTime)date.SelectedDate;
            ClassPage.ClassBase.BD.SaveChanges();
            MessageBox.Show("Данные изменены");
            this.Close();
        }
    }
}
