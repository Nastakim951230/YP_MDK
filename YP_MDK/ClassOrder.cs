using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP_MDK
{
    public partial class Order
    {
        double summaCoint;
        double summaSkidki;
        string itogskidka;
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
                        product += "Номер продукта: "+ order.Product.ProductArticleNumber + "Название продукта: " + order.Product.ProductName + ", Количества продуктов: "+order.kolvo+"\n";
                    }
                    product = product.Substring(0, product.Length - 1);
                }
                return product;
            }
        }

        public string cost
        {
            get
            {

                List<OrderProduct> orderProducts=ClassPage.ClassBase.BD.OrderProduct.Where(x=>x.OrderID == OrderID).ToList();
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
                itogskidka = string.Format("{0:F2}", skidka);
                return "Итоговая цена: " + summaCoint+ "     Итоговая скидка: " + itogskidka + "%";
              
            }
        }
    }
}
