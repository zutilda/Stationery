using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Stationery
{
    public partial class Order
    {
        public string FullName
        {
            get
            {
                return User.UserSurname + " " + User.UserName + " " + User.UserPatronymic;
            }
        }
                
        public string OrderComposition
        {
            get
            {
                string str = "Состав заказа: ";
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    str += product.ProductName + " (" + product.ProductArticleNumber + "), ";
                }
                str = str.Substring(0, str.Length - 2);
                return str;
            }
        }

        public string OrderAmountPrint
        {
            get
            {
                double resultCost = 0;
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    resultCost += product.CostOrder;
                }
                return "Стоимость заказа: " + resultCost;
            }
        }

        public double OrderAmount
        {
            get
            {
                double resultCost = 0;
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct orderProduct in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == orderProduct.ProductArticleNumber);
                    resultCost += product.CostOrder;
                }
                return resultCost;
            }
        }

        public string DiscountOrderPrint
        {
            get
            {
                double discount = 0;
                double withoutDiscount = 0;
                double newAmount = 0;
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    withoutDiscount += (double)product.ProductCost;
                    newAmount += product.CostOrder;
                }
                discount = 100 - 100 * newAmount / withoutDiscount;
                return "Общая скидка: " + discount + "%";
            }
        }

        public double DiscountOrder
        {
            get
            {
                double discount = 0;
                double withoutDiscount = 0;
                double newAmount = 0;
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    withoutDiscount += (double)product.ProductCost;
                    newAmount += product.CostOrder;
                }
                discount = 100 - 100 * newAmount / withoutDiscount;
                return discount;
            }
        }

        public SolidColorBrush ColorOrder
        {
            get
            {
                bool moreThree = false;
                bool zero = false;
                List<OrderProduct> orderProducts = ClassDBase.DB.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct prod in orderProducts)
                {
                    Product product = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == prod.ProductArticleNumber);
                    if (product.ProductQuantityInStock > 3)
                    {
                        moreThree = true;
                    }
                    else
                    {
                        moreThree = false;
                        if (product.ProductQuantityInStock == 0)
                        {
                            moreThree = false;
                            zero = true;
                            goto met;
                        }
                    }
                }
            met: if (moreThree == true)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#20b2aa");
                }
                else if (zero == true)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#ff8c00");
                }
                else
                {
                    return Brushes.White;
                }
            }
        }
    }
}
