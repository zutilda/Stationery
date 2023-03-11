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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            User user = ClassDBase.DB.User.FirstOrDefault(x => x.UserID == id);
            ClassFrame.basicFrame.Navigate(new ListViewProduct(user));
        }
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Login.Text != "")
                {
                    try
                    {
                        List<User> user = ClassDBase.DB.User.Where(x => x.UserLogin == Login.Text).ToList();
                        if (user.Count != 0)
                        {                            
                            Password.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Логин не найден");
                            return;
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Что-то пошло не по плану");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Введите логин");
                    return;
                }
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Password.Password != "")
                {
                    try
                    {
                        List<User> user = ClassDBase.DB.User.Where(x => x.UserLogin == Login.Text && x.UserPassword == Password.Password).ToList();

                        if (user.Count == 0)
                        {
                            MessageBox.Show("Неправильно введен пароль");
                        }
                      
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Что-то пошло не по плану");                       
                    }

                }
                else
                {
                    MessageBox.Show("Введите пароль");                    
                }
            }
        }

        private void EntranceGuest_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.basicFrame.Navigate(new ListViewProduct());
        }
    }
}
