using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Session2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
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

namespace Session2._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase dataBase = new DataBase();
        int attempt = 0;
        public MainWindow()
        {
            InitializeComponent();
            dataBase.openConnection();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(Pass.Text))
            {
                string queryString = "SELECT [Login], [Password], [Role] FROM [dbo].[User]";
                SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                List<string> userList = new List<string>();
                List<string> userPassList = new List<string>();
                List<string> userRoleList = new List<string>();

                while (dataReader.Read())
                {
                    string username = dataReader["Login"].ToString();
                    string userpass = dataReader["Password"].ToString();
                    string userRole = dataReader["Role"].ToString();
                    userList.Add(username);
                    userPassList.Add(userpass);
                    userRoleList.Add(userRole);
                }

                dataReader.Close();

                bool foundUser = userList.Contains(Login.Text);

                if (foundUser)
                {
                    int userIndex = userList.IndexOf(Login.Text);
                    if (userPassList[userIndex] == Pass.Text)
                    {
                        string userRole = userRoleList[userIndex];
                        string dataToSend = Login.Text;
                        if (userRole == "Заказчик")
                        {
                            Client client = new Client(dataToSend);
                            client.Show();
                            Close();
                        }
                        else if (userRole == "Менеджер по работе с клиентами")
                        {
                            Manager1 manager = new Manager1(dataToSend);
                            manager.Show();
                            Close();
                        }
                        else if (userRole == "Менеджер по закупкам")
                        {
                            ManagerPurchases manager = new ManagerPurchases();
                            manager.Show();
                            Close();
                        }
                        else if (userRole == "Менеджер по продажам")
                        {
                            ManagerSales manager = new ManagerSales();
                            manager.Show();
                            Close();
                        }
                        else if (userRole == "Мастер")
                        {
                            Master master = new Master();
                            master.Show();
                            Close();
                        }
                        else if (userRole == "Директор")
                        {
                            Director master = new Director();
                            master.Show();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                        BlockForm();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин");
                    BlockForm();
                }
            }
        }

        private async void BlockForm()
        {
            attempt++;
            if (attempt == 3)
            {
                LogIn.IsEnabled = false;
                Login.IsEnabled = false;
                Pass.IsEnabled = false;
                await Task.Delay(5000);
                LogIn.IsEnabled = true;
                Login.IsEnabled = true;
                Pass.IsEnabled = true;
                attempt = 0;
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }
    }
    
}
