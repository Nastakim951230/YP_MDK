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

namespace YP_MDK.Page
{
    /// <summary>
    /// Логика взаимодействия для Avtorizats.xaml
    /// </summary>
    public partial class Avtorizats 
    {
        public Avtorizats()
        {
            InitializeComponent();

            
        }

        


        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if(tbLogin.Text==""||tbPassword.Text=="")
            {
                MessageBox.Show("Обязательные поля не заполнены!");
            }
            else
            {
                User user=ClassPage.ClassBase.BD.User.FirstOrDefault(x=>x.UserLogin==tbLogin.Text && x.UserPassword==tbPassword.Text);
                if(user!=null)
                {
                    if(user.UserRole==1)
                    {
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Client());
                    }
                    else if(user.UserRole==2)
                    {
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Admin());
                    }
                    else if(user.UserRole==3)
                    {
                        ClassPage.FrameNavigate.perehod.Navigate(new Page.Manager());
                    }
                }
                else
                {
                    tbLogin.Text = "";
                    tbPassword.Text = "";
                    MessageBox.Show("Произошла ошибка при авторизации, попробуйте еще раз");
                }
            }
        }

        private void gost_Click(object sender, RoutedEventArgs e)
        {
            ClassPage.FrameNavigate.perehod.Navigate(new Page.Str_Gost());
        }
    }
}
