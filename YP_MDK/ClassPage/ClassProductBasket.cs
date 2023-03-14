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

        public double cost
        {
            get
            {
                double costProduct = Convert.ToDouble(productBasket.ProductCost) *count;
                return costProduct;
            }
        }

    }
}
