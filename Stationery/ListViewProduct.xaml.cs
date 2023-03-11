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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stationery
{
    /// <summary>
    /// Логика взаимодействия для ListViewProduct.xaml
    /// </summary>
    public partial class ListViewProduct : Page
    {
        public ListViewProduct()
        {
            InitializeComponent();
            ListService.ItemsSource = ClassDBase.DB.Product.ToList();
            Sorting.SelectedIndex = 0;
            Filtering.SelectedIndex = 0;
        }
        void Filter()
        {
            List<Product> products = new List<Product>();
            products = ClassDBase.DB.Product.ToList();

            //Фильтрация по размеру скидки
            switch (Filtering.SelectedIndex)
            {
                case 0:
                    {
                        products = products.ToList();
                    }
                    break;
                case 1:
                    {
                        products = products.Where(x => ((x.ProductDiscountAmount >= 0) && (x.ProductDiscountAmount < 10))).ToList();
                    }
                    break;
                case 2:
                    {
                        products = products.Where(x => ((x.ProductDiscountAmount >= 10) && (x.ProductDiscountAmount < 15))).ToList();
                    }
                    break;
                case 3:
                    {
                        products = products.Where(x => (x.ProductDiscountAmount>= 15)).ToList();

                    }
                    break;
                
            }
            //сортировка
            switch (Sorting.SelectedIndex)
            {
                case 0:
                    {
                        products.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    }
                    break;
                case 1:
                    {
                        products.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                        products.Reverse();
                    }
                    break;
            }

            //Поиск по названию
            if (!string.IsNullOrWhiteSpace(SearchName.Text))  // Проверка пустую запись и запись состоящую из пробелов
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(SearchName.Text.ToLower())).ToList();

            }
            else
            {
                MessageBox.Show("Записей с таким названием нет");
                SearchName.Text = "";
            }

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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            //Service serv = DBase.DB.Service.FirstOrDefault(x => x.ID == id);
            //List<ClientService> clientproducts = DBase.DB.ClientService.Where(x => x.ServiceID == serv.ID).ToList();
            //if (clientproducts.Count > 0)
            //{
            //    MessageBox.Show("Данную услугу нельзя удалить");
            //}
            //else
            //{
            //    //DBase.DB.Service.Remove(serv);
            //    //DBase.DB.SaveChanges();
            //    //ClassFrame.newFrame.Navigate(new PageListOfService());
            //}

        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            //Service service = DBase.DB.Service.FirstOrDefault(x => x.ID == id);
            //ClassFrame.newFrame.Navigate(new AddPage(service));
        }

        private void SingUp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            //Service service = DBase.DB.Service.FirstOrDefault(x => x.ID == id);
            //ClassFrame.newFrame.Navigate(new PageAddNote(service));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.basicFrame.Navigate(new Authorization());
        }
    }
}
