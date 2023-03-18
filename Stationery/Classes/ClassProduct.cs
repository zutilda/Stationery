using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Stationery
{
    public partial class Product
    {
        public SolidColorBrush Brush
        {
            get
            {
                var brushConverter = new BrushConverter();

                if (ProductDiscountAmount < 15 || ProductDiscountAmount == null)
                {
                    return Brushes.White;
                }
                else
                {
                    return (SolidColorBrush)(Brush)brushConverter.ConvertFrom("#7fff00");

                }

            }
        }
        public string Price
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
        public string Cost

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
        public double PriceOrder
        {
            get
            {
                if (ProductDiscountAmount != null)
                {
                    double b = (Convert.ToDouble(ProductCost) / 100) * (100 - ProductDiscountAmount.Value);
                    return b;
                }
                else
                {
                    double a = Convert.ToDouble(ProductCost);
                    return a;
                }
            }
        }
        public double CostOrder

        {
            get
            {
                if (ProductDiscountAmount != null)
                {

                    return Math.Round(Convert.ToDouble(ProductCost));
                }

                return 0;
            }
        }

        public string Discount
        {
            get
            {
                return ProductDiscountAmount + "%";
            }
        }
    }
}
