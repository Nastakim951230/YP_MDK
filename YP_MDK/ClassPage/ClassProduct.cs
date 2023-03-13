using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YP_MDK
{
    public partial class Product
    {

        public SolidColorBrush colorBackground
        { get
            {
                var brush = new BrushConverter();
                double skidka = Convert.ToDouble(ProductDiscountAmount);
                if (skidka>=15)
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#7fff00");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#ffffff");
                }
            }



         }
        public SolidColorBrush colorBrush
        {
            get
            {
                var brush = new BrushConverter();
                double skidka = Convert.ToDouble(ProductDiscountAmount);
                if (skidka > 0)
                {
                    if(skidka > 0 && skidka <= 9.99)
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#7fff00");
                    }
                    else if(skidka >9.99 && skidka <= 14.99)
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#FF498C51");
                    }
                    else
                    {
                        return (SolidColorBrush)(Brush)brush.ConvertFrom("#7fff00");
                    }
                }
                else
                {
                    return (SolidColorBrush)(Brush)brush.ConvertFrom("#ffffff");
                }
            }

        }

        public string Naimenovanie
        {
            get
            {
                return "Название товара: " + ProductName;
            }
        }
        public string UnitTovara
        {
            get
            {
                return "Единица: " + Unit.Unit1;
            }
        }
        public string CategoriaTovara
        {
            get
            {
                return "Категория товара: " + Category.Category1;
            }
        }
        public string Manufacture
        {
            get
            {
                return "Производитель: " + Manufacturer.Manufacturer1;
            }
        }
        public string Skidka
        {


            get
            {
                double skidka = Convert.ToDouble(ProductDiscountAmount);

                if (skidka == 0)
                {
                    return "";
                }
                else
                {
                    double cost_skidka = (double)(Convert.ToDouble(ProductCost) - (Convert.ToDouble(ProductCost) * ProductDiscountAmount / 100));
                    return " "+cost_skidka;
                }

            }
         
        }
      
        public string Decoration
        {
            get
            {
                if (ProductDiscountAmount != 0)
                {
                    return "Strikethrough";
                }
                else
                {
                    return "Baseline";
                }
            }
        }
    }
}
