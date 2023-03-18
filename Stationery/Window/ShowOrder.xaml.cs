using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stationery
{
    /// <summary>
    /// Логика взаимодействия для ShowOrder.xaml
    /// </summary>
    public partial class ShowOrder : Window
    {
        User user;
        public ShowOrder(User user)
        {
            InitializeComponent();
            this.user = user;
            ListProduct.ItemsSource = ClassDBase.products;
            List<Order> countOrders = ClassDBase.DB.Order.ToList();
            int count = 0;
            foreach (Order order in countOrders)
            {
                count = order.OrderID;
            }
            OrderNumber.Text = "Заказ " + (count + 1);
            AmountDiscount();
            List<PickupPoint> pickupPoints = ClassDBase.DB.PickupPoint.ToList();
            OrderPickupPoint.Items.Add("Выберите пункт выдачи");
            foreach (PickupPoint pickup in pickupPoints)
            {
                OrderPickupPoint.Items.Add(pickup.Adress);
            }
            OrderPickupPoint.SelectedIndex = 0;

            if (user != null)
            {
                FullName.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            else
            {
                FullName.Text = "Гость";
            }
        }

        private void AmountDiscount()
        {
            double startAmount = 0, discount, endAmount = 0;
            foreach (Product product in ClassDBase.products)
            {
                endAmount += (double)product.PriceOrder;
                startAmount += (double)product.CostOrder;
            }
            discount = 100 - 100 * endAmount / startAmount;
            ResultDiscount.Text = "Общая скидка: " + Math.Round(discount,1) + "%";
            ResultAmount.Text = "Общая стоимость: " + string.Format("{0:C2}", endAmount);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Подтвердите удаление товара", "Удаление", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    string index = btn.Uid;
                    Product product = ClassDBase.products.FirstOrDefault(z => z.ProductArticleNumber == index);
                    ClassDBase.products.Remove(product);
                    ListProduct.Items.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string index = tb.Uid;  
            if (tb.Text == "0")
            {
                Product product = ClassDBase.products.FirstOrDefault(z => z.ProductArticleNumber == index);
                ClassDBase.products.Remove(product);
                ListProduct.Items.Refresh();
            }            
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();

                bool stockProduct = true; //количество товара на складе
                foreach (Product product in ClassDBase.products)
                {
                    if (product.ProductQuantityInStock < 3)
                    {
                        stockProduct = false;
                    }
                }

                Order order = new Order(); // создание нового заказа
                order.OrderStatus = 1;
                if (stockProduct == false)
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                }
                else
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                }
                if (OrderPickupPoint.SelectedIndex != 0)
                {
                    order.OrderPickupPoint = OrderPickupPoint.SelectedIndex;
                    order.OrderDate = DateTime.Now;
                    if (user != null)
                    {
                        order.Client = user.UserID;
                    }
                    order.Code = Convert.ToString(random.Next(100, 1000)); //создание кода заказа

                    ClassDBase.DB.Order.Add(order);

                    foreach (Product product in ClassDBase.products) //создание новых элементов таблицы OrderProduct
                    {
                        OrderProduct orderProduct = new OrderProduct()
                        {
                            OrderID = order.OrderID,
                            ProductArticleNumber = product.ProductArticleNumber,
                            ProductQuantity = 1
                        };
                        ClassDBase.DB.OrderProduct.Add(orderProduct);
                    }
                    ClassDBase.DB.SaveChanges();
                    MessageBox.Show("Заказ успешно добавлен");
                }
                else
                {
                    MessageBox.Show("Не выбран пункт выдачи заказа");
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }
     }
}
