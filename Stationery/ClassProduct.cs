using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    public partial class Product
    {
        public string price
        {
            get
            {
                if (ProductDiscountAmount != null)
                {
                    double b = (Convert.ToDouble(ProductCost) / 100) * (100 - ProductDiscountAmount.Value);                   
                    return b + "";
                }
                else
                {
                    double a = Convert.ToDouble(ProductCost);                   
                    return a + "";
                }
            }
        }
        public string cost

        {
            get
            {
                if (ProductDiscountAmount != null)
                {
                   
                    return Math.Round(Convert.ToDouble(ProductCost)) + " ";
                }
                return "";

            }
        }
    }
}
