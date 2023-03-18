using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Stationery
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        /// <summary>
        /// <param name="time">время работы таймера, блокирующего возможность нажатия на кнопки для повторной авторизации</param>
        /// <param name="CAPTCHA">переменная хранит сгенерированное значение captcha</param>
        /// </summary>

        private DispatcherTimer Timer;
        private int time = 10;      
        private string CAPTCHA;
        public Authorization()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);

            CheckEntrance.Visibility = Visibility.Collapsed;
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" && Password.Password == "")
            {
                MessageBox.Show("Обязательные поля не заполнены");
            }
            else
            {
                User user = ClassDBase.DB.User.FirstOrDefault(x => x.UserLogin == Login.Text && x.UserPassword == Password.Password);
                if (user == null)
                {
                    MessageBox.Show("Пользователя с таким логином и паролем не существует");
                    Entrance.IsEnabled = false;
                    GetCAPTHA();
                    CheckEntrance.Visibility = Visibility.Visible;
                }
                else
                {
                    ClassFrame.basicFrame.Navigate(new ListViewProduct(user));
                }
            }
        }
        public string GetCAPTHA() //создание капчи
        {
            Canvas.Children.Clear();
            Random rand = new Random();

            string result = "";
            char c = '0';

            for (int i = 0; i < 4; i++)
            {
                switch (rand.Next(0, 3))
                {
                    case 0:
                        c = (char)rand.Next(49, 58);
                        break;
                    case 1:
                        c = (char)rand.Next(65, 91);
                        break;
                    case 2:
                        c = (char)rand.Next(97, 123);
                        break;
                }
                result += c;

                TextBlock tb = new TextBlock()
                {
                    Text = c.ToString(),
                    Padding = new Thickness(i * 25 + 5, rand.Next(21), rand.Next(21), 10),
                    FontSize = rand.Next(20, 26)
                };

                switch (rand.Next(0, 3))
                {
                    case 0:
                        tb.FontStyle = FontStyles.Italic;
                        break;
                    case 1:
                        tb.FontWeight = FontWeights.Bold;
                        break;
                    case 2:
                        tb.FontStyle = FontStyles.Italic;
                        tb.FontWeight = FontWeights.Bold;
                        break;
                }

                Canvas.Children.Add(tb);
            }

            for (int i = 0; i < 15; i++)
            {
                Line line = new Line()
                {
                    X1 = rand.Next(251),
                    Y1 = rand.Next(51),
                    X2 = rand.Next(251),
                    Y2 = rand.Next(51),
                    Stroke = new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)))
                };
                Canvas.Children.Add(line);
            }
            CAPTCHA = result;
            return result;
        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
            time--;
            Time.Text =  "Повторите попытку через " + time + " с";

            if (time == 0)
            {
                Timer.Stop();
                Time.Text = "";
                Entrance.IsEnabled = true;
                Captha.Text = "";
            }
        }
        private void Captha_KeyDown(object sender, KeyEventArgs e)
        {
            if (Captha.Text.Length == 4)
            {
                if (Captha.Text.ToLower() == CAPTCHA.ToLower())
                {
                    Entrance.IsEnabled = true;
                    CheckEntrance.Visibility=Visibility.Collapsed;
                    Captha.Text = "";
                }
                else
                {
                    Timer.Start();
                    time = 10;
                    Time.Text = "Повторите попытку через " + time + " с";
                    Login.Text = "";
                    Password.Password = "";
                    CheckEntrance.Visibility = Visibility.Collapsed;
                    Captha.Text = "";
                }
            }
        }
        private void EntranceGuest_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.basicFrame.Navigate(new ListViewProduct());
        }

       
    }
}
