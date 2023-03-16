using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YP_MDK
{
    public partial class Order
    {
        
        
        public string Nomer
        {
            get
            {
                return "Номер заказа: " + OrderID;
            }
        }
        public string date
        {
            get
            {
                return "Дата заказа: " + string.Format("{0:dd MMMM yyyy}", OrderDeliveryDate) +"   Дата доставки: "+ string.Format("{0:dd MMMM yyyy}", OrderDate);
            }
        }
        public string Name
        {
            get
            {
                User user = ClassPage.ClassBase.BD.User.FirstOrDefault(x => x.UserID == OrderUser);
                if(user == null)
                {
                    return "";
                }
                else
                {
                    return "ФИО: " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                }
            }
        }
        public string product
        {
            get
            {
                
                List<OrderProduct> orderProducts = ClassPage.ClassBase.BD.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                int i = orderProducts.Count;
                string product = "";
                if (i == 0)
                {
                    product = "Никаких продуктов нет в заказе";
                }
                else
                {
                    foreach (OrderProduct order in orderProducts)
                    {
                        product += "Номер продукта: "+ order.Product.ProductArticleNumber + ",  Название продукта: " + order.Product.ProductName + ",  Количества продуктов: "+order.kolvo+"\n";
                    }
                    product = product.Substring(0, product.Length - 1);
                }
                return product;
            }
        }

        public SolidColorBrush colorBackground
        {
            get
            {

                List<OrderProduct> orderProducts = ClassPage.ClassBase.BD.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                var brush = new BrushConverter();
                string product = "#ffffff";
               
                foreach (OrderProduct order in orderProducts)
                { 
                    if (order.Product.ProductQuantityInStock > 3)
                    {
                        product = "#20b2aa";
                    }
                    else if (order.Product.ProductQuantityInStock == 0)
                    {
                        product = "#ff8c00";
                    }
                    else
                    {
                        product = "#ffffff";
                        
                    }
                }
                return (SolidColorBrush)(Brush)brush.ConvertFrom(product);
            }



        }
        public double costSkidka
        {
            get
            {
                double summaCoint = 0;
              
                List<OrderProduct> orderProducts = ClassPage.ClassBase.BD.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                int coint = orderProducts.Count;
                for (int i = 0; i < coint; i++)
                {
                    if (orderProducts[i].Product.ProductDiscountAmount != 0)
                    {
                       
                        summaCoint = summaCoint + ((double)(Convert.ToDouble(orderProducts[i].Product.ProductCost) - (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].Product.ProductDiscountAmount / 100)) * orderProducts[i].kolvo);
                    }
                    else
                    {
                       
                        summaCoint = summaCoint + (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].kolvo);
                    }
                }
              
                return  summaCoint ;
            }
        }

        public double Skidka
        {
            get
            {
                double summaCoint=0;
                double summaSkidki=0;
                List<OrderProduct> orderProducts = ClassPage.ClassBase.BD.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                int coint = orderProducts.Count;
                for (int i = 0; i < coint; i++)
                {
                    if (orderProducts[i].Product.ProductDiscountAmount != 0)
                    {
                        summaSkidki = summaSkidki + (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].kolvo);
                        summaCoint = summaCoint + ((double)(Convert.ToDouble(orderProducts[i].Product.ProductCost) - (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].Product.ProductDiscountAmount / 100)) * orderProducts[i].kolvo);
                    }
                    else
                    {
                        summaSkidki = summaSkidki + (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].kolvo);
                        summaCoint = summaCoint + (Convert.ToDouble(orderProducts[i].Product.ProductCost) * orderProducts[i].kolvo);
                    }
                }
                double skidka = (summaSkidki - summaCoint) * 100 / summaSkidki;

                return skidka;
            }
        }
    }
}
