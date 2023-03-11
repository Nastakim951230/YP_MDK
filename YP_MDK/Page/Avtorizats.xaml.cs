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
using System.Windows.Threading;

namespace YP_MDK.Page
{
    /// <summary>
    /// Логика взаимодействия для Avtorizats.xaml
    /// </summary>
    public partial class Avtorizats
    {
        
        public static int coint;
        string text = String.Empty;
        DispatcherTimer disTimer = new DispatcherTimer();
        int sec = 10;
        public Avtorizats()
        {
            InitializeComponent();

            if (coint == 1)
            {
                
                CaptchaSP.Visibility = Visibility.Visible;
                Random random = new Random();
                int kolvoPolyline = random.Next(7, 20);

                for (int i = 0; i < kolvoPolyline; i++)
                {
                    SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
                    Line line = new Line()
                    {
                        X1 = random.Next((int)Captcha.Width),
                        Y1 = random.Next((int)Captcha.Height),
                        X2 = random.Next((int)Captcha.Width),
                        Y2 = random.Next((int)Captcha.Height),
                        Stroke = brush,
                    };
                    Captcha.Children.Add(line);
                }
                text = String.Empty;
                string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
                for (int i = 0; i < 4; ++i)
                    text += ALF[random.Next(ALF.Length)];

                int vubor = random.Next(0, 3);

                int fontSize = random.Next(16, 33);
            perehod: int height = random.Next((int)Captcha.Height - 20);
                int width = random.Next((int)Captcha.Width - 20);
                if (width < 120)
                {
                    if (height < 50)
                    {
                        if (vubor == 0)
                        {
                            TextBlock textBlock = new TextBlock()
                            {

                                Text = text,
                                FontSize = fontSize,
                                Padding = new Thickness(width, height, 0, 0),
                                TextDecorations = TextDecorations.Strikethrough,

                            };
                            Captcha.Children.Add(textBlock);
                        }
                        else if (vubor == 1)
                        {
                            TextBlock textBlock = new TextBlock()
                            {

                                Text = text,
                                FontSize = fontSize,
                                Padding = new Thickness(width, height, 0, 0),
                                FontWeight = FontWeights.Bold,
                                TextDecorations = TextDecorations.Strikethrough,
                            };
                            Captcha.Children.Add(textBlock);
                        }
                        else if (vubor == 2)
                        {
                            TextBlock textBlock = new TextBlock()
                            {

                                Text = text,
                                FontSize = fontSize,
                                Padding = new Thickness(width, height, 0, 0),
                                FontWeight = FontWeights.Bold,
                                FontStyle = FontStyles.Italic,
                                TextDecorations = TextDecorations.Strikethrough,
                            };
                            Captcha.Children.Add(textBlock);
                        }
                        else if (vubor == 3)
                        {
                            TextBlock textBlock = new TextBlock()
                            {

                                Text = text,
                                FontSize = fontSize,
                                Padding = new Thickness(width, height, 0, 0),
                                TextDecorations = TextDecorations.Strikethrough,
                                FontStyle = FontStyles.Italic,

                            };
                            Captcha.Children.Add(textBlock);
                        }
                    }
                    else
                    {
                        goto perehod;
                    }
                }
                else
                {
                    goto perehod;
                }
            }
            else if(coint==2)
            {
                CaptchaSP.Visibility = Visibility.Collapsed;
                vhod.Visibility = Visibility.Collapsed;
                time.Visibility = Visibility.Visible;
                disTimer.Interval = TimeSpan.FromSeconds(1);
                disTimer.Tick += dtTicker;
                disTimer.Start();
            }
        }


        private void dtTicker(object sender, EventArgs e)
        {
            if (sec != 0)
            {
                sec--;
                time.Text = "Авторизироваться можно через: "+sec.ToString();

            }
            else
            {
                vhod.Visibility = Visibility.Visible;
                time.Visibility = Visibility.Collapsed;
                disTimer.Stop();
                sec = 10;
            }
        }
        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaSP.Visibility == Visibility.Visible)
            {
                if (tbLogin.Text == "" || tbPassword.Text == "" || CaptchaTB.Text=="")
                {
                    MessageBox.Show("Обязательные поля не заполнены!");
                }
                else
                {
                    if (CaptchaTB.Text == text)
                    {
                        User user = ClassPage.ClassBase.BD.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == tbPassword.Text);
                        if (user != null)
                        {
                            if (user.UserRole == 1)
                            {
                                ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                            }
                            else if (user.UserRole == 2)
                            {
                                ClassPage.FrameNavigate.perehod.Navigate(new Page.Admin(user));
                            }
                            else if (user.UserRole == 3)
                            {
                                ClassPage.FrameNavigate.perehod.Navigate(new Page.Manager(user));
                            }
                        }
                        else
                        {
                            Page.Avtorizats.coint = 2;
                            
                            MessageBox.Show("Произошла ошибка при авторизации, попробуйте еще раз");
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Avtorizats());

                        }
                    }
                    else
                    {
                        Page.Avtorizats.coint = 2;
                      
                        MessageBox.Show("Произошла ошибка при авторизации, попробуйте еще раз");
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Avtorizats());
                    }
                }
            }
            else
            {
                if (tbLogin.Text == "" || tbPassword.Text == "")
                {
                    MessageBox.Show("Обязательные поля не заполнены!");
                }
                else
                {
                    User user = ClassPage.ClassBase.BD.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == tbPassword.Text);
                    if (user != null)
                    {
                        if (user.UserRole == 1)
                        {
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Client(user));
                        }
                        else if (user.UserRole == 2)
                        {
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Admin(user));
                        }
                        else if (user.UserRole == 3)
                        {
                            ClassPage.FrameNavigate.perehod.Navigate(new Page.Manager(user));
                        }
                    }
                    else
                    {
                        Page.Avtorizats.coint = 1;
                        
                        MessageBox.Show("Произошла ошибка при авторизации, попробуйте еще раз");
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Avtorizats());

                    }
                }
            }
        }

        private void gost_Click(object sender, RoutedEventArgs e)
        {
            ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
        }

        
    }
}
