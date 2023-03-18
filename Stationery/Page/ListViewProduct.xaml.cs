using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stationery
{
    /// <summary>
    /// Логика взаимодействия для ListViewProduct.xaml
    /// </summary>
    public partial class ListViewProduct : Page
    {
        User user;
        
        public ListViewProduct()
        {
            InitializeComponent();
            ListProduct.ItemsSource = ClassDBase.DB.Product.ToList();
            Sorting.SelectedIndex = 0;
            Filtering.SelectedIndex = 0;
            CountProduct.Text = ClassDBase.DB.Product.ToList().Count + "/" + ClassDBase.DB.Product.ToList().Count;  
            ShowOrders.Visibility= Visibility.Collapsed;
        }
        public ListViewProduct(User user)
        {
            InitializeComponent();
            ListProduct.ItemsSource = ClassDBase.DB.Product.ToList();
            this.user = user;
            authUser.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            Sorting.SelectedIndex = 0;
            Filtering.SelectedIndex = 0;
            CountProduct.Text = ClassDBase.DB.Product.ToList().Count + "/" + ClassDBase.DB.Product.ToList().Count;
            ShowOrders.Visibility = Visibility.Collapsed;
        }
        void Filter()
        {
            List<Product> product = new List<Product>();
            product = ClassDBase.DB.Product.ToList();

            //Поиск по названию
            if (!string.IsNullOrWhiteSpace(SearchName.Text))  // Проверка пустую запись и запись состоящую из пробелов
            {
                product = product.Where(x => x.ProductName.ToLower().Contains(SearchName.Text.ToLower())).ToList();
                if (product.Count == 0)
                {
                    MessageBox.Show("Записей с таким названием нет");
                    SearchName.Text = "";
                }
            }
            //Фильтрация по размеру скидки
            switch (Filtering.SelectedIndex)
            {
                case 0:
                    {
                        product = product.ToList();
                    }
                    break;
                case 1:
                    {
                        product = product.Where(x => ((x.ProductDiscountAmount >= 0) && (x.ProductDiscountAmount < 10))).ToList();
                    }
                    break;
                case 2:
                    {
                        product = product.Where(x => ((x.ProductDiscountAmount >= 10) && (x.ProductDiscountAmount < 15))).ToList();
                    }
                    break;
                case 3:
                    {
                        product = product.Where(x => (x.ProductDiscountAmount>= 15)).ToList();

                    }
                    break;
                
            }
            //сортировка
            switch (Sorting.SelectedIndex)
            {
                case 0:
                    {
                        product.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    }
                    break;
                case 1:
                    {
                        product.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                        product.Reverse();
                    }
                    break;
            }

            ListProduct.ItemsSource = product;
            if (product.Count == 0)
            {
                MessageBox.Show("нет записей");
                CountProduct.Text = ClassDBase.DB.Product.ToList().Count + "/" + ClassDBase.DB.Product.ToList().Count;
                SearchName.Text = "";
                Sorting.SelectedIndex = 0;
                Filtering.SelectedIndex = 0;

            }
            CountProduct.Text = product.Count + "/" + ClassDBase.DB.Product.ToList().Count;
        }
        private void SearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
             
        private void Filtering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Delete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (user.UserRole == 2 || user.UserRole == 3)
            {
                btn.Visibility = Visibility.Visible;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Подтвердите удаление", "", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    string index = btn.Uid;

                    List<OrderProduct> products = ClassDBase.DB.OrderProduct.Where(x => x.ProductArticleNumber == index).ToList();
                    if (products.Count == 0)
                    {
                        Product prod = ClassDBase.DB.Product.FirstOrDefault(z => z.ProductArticleNumber == index);
                        ClassDBase.DB.Product.Remove(prod);
                        ClassDBase.DB.SaveChanges();
                        ClassFrame.basicFrame.Navigate(new ListViewProduct(user));
                    }
                    else
                    {
                        MessageBox.Show("Удаление товара запрещено");
                    }
                    break;
                default:
                    break;
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string index = mi.Uid;
            Product product = ClassDBase.DB.Product.FirstOrDefault(x => x.ProductArticleNumber == index);
            ClassDBase.products.Add(product);
            ShowOrders.Visibility = Visibility.Visible;
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            ShowOrder windowShowOrder = new ShowOrder(user);
            windowShowOrder.ShowDialog();
           
            if (ClassDBase.products.Count == 0)
            {
                ShowOrders.Visibility = Visibility.Collapsed;
            }
        }
    }
}
