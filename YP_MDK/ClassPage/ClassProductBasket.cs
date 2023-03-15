using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP_MDK
{
    public class ClassProductBasket
    {
        public Product productBasket { get; set; }
        public int count { get; set; }

        public string cost
        {
            get
            {
                double costProduct = Convert.ToDouble(productBasket.ProductCost) * count;
                return "Цена: "+ costProduct+"руб.";
            }
        }
        public string skidka
        {
            get
            {
                double skidka = Convert.ToDouble(productBasket.ProductDiscountAmount);
                if (skidka==0)
                {
                    return "0";
                }
                else
                {
                    return "" + skidka;
                }
               
            }
        }
        public string skidkacost2
        {
            get
            {
                double skidka = Convert.ToDouble(productBasket.ProductDiscountAmount);
                if (skidka == 0)
                {
                    return "0 руб.";
                }
                else
                {
                    double cost_skidka = (double)(Convert.ToDouble(productBasket.ProductCost)*count * productBasket.ProductDiscountAmount / 100);
                    return "-" + cost_skidka+"руб.";
                }

            }
        }
        public string skidkaCost
        {
            get
            {
                double skidka = Convert.ToDouble(productBasket.ProductDiscountAmount);

                if (skidka == 0)
                {
                    double costProduct = Convert.ToDouble(productBasket.ProductCost) * count;
                    return "Цена со скидкой: " + costProduct+"руб.";
                }
                else
                {
                    double cost_skidka = (double)((Convert.ToDouble(productBasket.ProductCost)*count) - (Convert.ToDouble(productBasket.ProductCost)*count * productBasket.ProductDiscountAmount / 100));
                    return "Цена со скидкой:" + cost_skidka + "руб.";
                }
            }
        }

    }
}
