﻿using System;
using System.Collections.Generic;
using System.Data;
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
        User user;
        
        public ListViewProduct()
        {
            InitializeComponent();
            ListService.ItemsSource = ClassDBase.DB.Product.ToList();
            Sorting.SelectedIndex = 0;
            Filtering.SelectedIndex = 0;
            CountService.Text = ClassDBase.DB.Product.ToList().Count + "/" + ClassDBase.DB.Product.ToList().Count;           
        }
        public ListViewProduct(User user)
        {
            InitializeComponent();
            this.user = user;
            authUser.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
        }
        void Filter()
        {
            List<Product> products = new List<Product>();
            products = ClassDBase.DB.Product.ToList();

            //Поиск по названию
            if (!string.IsNullOrWhiteSpace(SearchName.Text))  // Проверка пустую запись и запись состоящую из пробелов
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(SearchName.Text.ToLower())).ToList();
                if (products.Count == 0)
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

           ListService.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("нет записей");
                CountService.Text = ClassDBase.DB.Product.ToList().Count + "/" + ClassDBase.DB.Product.ToList().Count;
                SearchName.Text = "";
                Sorting.SelectedIndex = 0;
                Filtering.SelectedIndex = 0;

            }
            CountService.Text = products.Count + "/" + ClassDBase.DB.Product.ToList().Count;
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

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (user.UserRole == 2 || user.UserRole == 3)
            {
                btn.Visibility = Visibility.Visible;
            }

        }     
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string index = mi.Uid;
            Product product = ClassDBase.DB.Product.FirstOrDefault(x => x.ProductArticleNumber == index);
            ClassDBase.products.Add(product);
            //btnShowOrder.Visibility = Visibility.Visible;
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            ShowOrder windowShowOrder = new ShowOrder();
            windowShowOrder.ShowDialog();
           
            if (ClassDBase.products.Count == 0)
            {
                ShowOrders.Visibility = Visibility.Collapsed;
            }
        }
    }
}
