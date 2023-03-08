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

        public SolidColorBrush colorBrush
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


        public string Naimenovanie
        {
            get
            {
                return "Название товара: " + ProductName;
            }
        }

        public string Manufacture
        {
            get
            {
                return "Производитель: " + ProductManufacturer;
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
