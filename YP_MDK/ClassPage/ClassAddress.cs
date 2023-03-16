using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP_MDK
{
    public partial class PointOfIssue
    {
        public string AddressAndIndex
        {
            get
            {
                return "индекс: " + Index_zakaz + "; Адрес: " + Address;
            }
        }

    }
}
