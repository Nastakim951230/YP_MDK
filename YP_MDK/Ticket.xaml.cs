using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

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
            //Создаем новый PDF документ
            PdfDocument pdfDocument = new PdfDocument();
            int height = 30;
            pdfDocument.Info.Title = "Талон для получения заказа";
            // Создаем пустую страницу
            PdfPage page = pdfDocument.AddPage();
            // Получаем объект XGraphics для "рисования" элементов на странице
            XGraphics gfx = XGraphics.FromPdfPage(page);
            // Создаем шрифта
            XFont font = new XFont("Comic Sans MS", 14);
            gfx.DrawString("Нормер заказа: "+order.OrderID,font,XBrushes.Black,new XRect(10,height ,page.Width, page.Height),XStringFormat.TopCenter);
            height += 40;
            gfx.DrawString("Дата заказа: " + string.Format("{0:dd MMMM yyyy}", order.OrderDeliveryDate)+ "  Дата получения: " + string.Format("{0:dd MMMM yyyy}", order.OrderDate)+ " Срок доставки: " + dateDelivery, font, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormat.TopCenter);
            height += 40;
            gfx.DrawString("Пункт выдачи: " + issues.Address + "  Индекс пункта: " + issues.Index_zakaz, font, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormat.TopLeft);
            height += 40;
            XFont talon = new XFont("Comic Sans MS", 18, XFontStyle.Bold);
            gfx.DrawString("Код получения: " + order.CodeToReceive, talon, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormat.TopLeft);
            height += 40;
            gfx.DrawString("Состав заказа", font, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormat.TopCenter);
            for(int i=0;i<baskets.Count;i++)
            {
                height += 40;
                if(i!=baskets.Count-1)
                {
                    gfx.DrawString(baskets[i].productBasket.ProductName, font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString(baskets[i].count + "X" + baskets[i].productBasket.ProductCost, font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    gfx.DrawString(baskets[i].cost, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString("Скидка: " + string.Format("{0:F2}%", baskets[i].skidka), font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    gfx.DrawString(baskets[i].skidkacost2, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString(baskets[i].skidkaCost, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                }
                else
                {
                    gfx.DrawString(baskets[i].productBasket.ProductName, font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString(baskets[i].count + "X" + baskets[i].productBasket.ProductCost, font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    gfx.DrawString(baskets[i].cost, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString("Скидка: " + string.Format("{0:F2}%", baskets[i].skidka), font, XBrushes.Black, new XRect(30, height, page.Width, page.Height), XStringFormat.TopLeft);
                    gfx.DrawString(baskets[i].skidkacost2, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                    height += 20;
                    gfx.DrawString(baskets[i].skidkaCost, font, XBrushes.Black, new XRect(300, height, page.Width, page.Height), XStringFormat.TopLeft);
                }
            }
            height += 30;
            gfx.DrawString("Итого: " + summaCoint + "Скидка: " + summaDiscount + "%", font, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormat.TopLeft);
            height += 40;
            string filename = "Ticket.pdf";
            // Сохраняем файл под названием Test.pdf
            pdfDocument.Save(filename);
            // ... и запускам сразу в программе просмотра pdf файлов
            Process.Start(filename);

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
