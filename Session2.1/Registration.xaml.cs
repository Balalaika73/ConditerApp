using Microsoft.Data.SqlClient;
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
using System.Xml.Linq;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(Pass.Text) && !string.IsNullOrEmpty(FIO.Text))
            {
                if (Pass.Text.Length >= 5 && !Pass.Text.Contains(Login.Text))
                {
                    bool hasUppercase = false;
                    bool hasLowercase = false;

                    foreach (char c in Pass.Text)
                    {
                        if (char.IsLetter(c) && char.IsLower(c))
                        {
                            hasLowercase = true;
                        }
                        else if (char.IsLetter(c) && char.IsUpper(c))
                        {
                            hasUppercase = true;
                        }
                    }

                    if (hasUppercase && hasLowercase)
                    {
                        DataBase dataBase = new DataBase();
                        dataBase.openConnection();
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
                            MessageBox.Show("Пользователь уже есть в системе!");
                        }
                        else
                        {
                            var addQuery = $"insert into [dbo].[User] values ('{Login.Text}', '{Pass.Text}', 'Заказчик', '{FIO.Text}', null)";
                            var command = new SqlCommand(addQuery, dataBase.getConnection());
                            command.ExecuteNonQuery();
                            dataBase.closeConnection();
                            MessageBox.Show("Регистрация прошла успешно!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать заглавные и маленькие буквы!");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен быть от 5 до 20 симвовлов и не содержать логин!");
                }

            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow enter = new MainWindow();
            enter.Show();
            Close();
        }
    }
}
