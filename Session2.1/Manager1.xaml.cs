using Microsoft.Data.SqlClient;
using System;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Manager1.xaml
    /// </summary>
    public partial class Manager1 : Window
    {
        DataBase dataBase = new DataBase();
        string manager = null;
        public Manager1(string data)
        {
            InitializeComponent();
            manager = data;
            foreach (ComboBoxItem item in SizeUnit.Items)
            {
                if (item.Content.ToString() == "см")
                {
                    SizeUnit.SelectedItem = item;
                    break;
                }
            }
        }

        private void FindUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersView.ItemsSource = null;
            string searchString = $"select [Login],[FIO] from [dbo].[User] where concat ([Login],[FIO]) like '%" + FindUser.Text + "%' and [Role] = 'Заказчик'";
            dataBase.DataSetFill(string.Format(searchString), "User", DataBase.Function.select, null);
            UsersView.ItemsSource = DataBase.dataSet.Tables["User"].DefaultView;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (Check.IsChecked == true)
            {
                LblUser.Visibility = Visibility.Hidden;
                FindUser.Visibility = Visibility.Hidden;
                UsersView.Visibility = Visibility.Hidden;
                Login.Visibility = Visibility.Visible;
                LblLogin.Visibility = Visibility.Visible;
                Pass.Visibility = Visibility.Visible;
                LblPass.Visibility = Visibility.Visible;
                FIOtb.Visibility = Visibility.Visible;
                LblFio.Visibility = Visibility.Visible;
            }
            else
            {
                LblUser.Visibility = Visibility.Visible;
                FindUser.Visibility = Visibility.Visible;
                UsersView.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Hidden;
                LblLogin.Visibility = Visibility.Hidden;
                Pass.Visibility = Visibility.Hidden;
                LblPass.Visibility = Visibility.Hidden;
                FIOtb.Visibility = Visibility.Hidden;
                LblFio.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataBase.connection_Checking();
            UsersFill();
            OrdersFill();
        }

        private void UsersFill()
        {
            dataBase.DataSetFill(string.Format("select [Login] as 'Логин',[FIO] as 'ФИО' from [dbo].[User] where [Role] = 'Заказчик'"), "User", DataBase.Function.select, null);
            UsersView.ItemsSource = DataBase.dataSet.Tables["User"].DefaultView;
        }

        private void OrdersFill()
        {
            dataBase.DataSetFill(string.Format($"SELECT [Ord_Numb] AS 'Номер', [Name_Ord] AS 'Наименование', [Customer] AS 'Заказчик', [Ord_Date] AS 'Дата заказа', [Status] as 'Статус заказа', [Manager] as 'Менеджер' FROM [dbo].[Order] INNER JOIN [dbo].[User] AS [CustomerUser] ON [Order].[Customer] = [CustomerUser].[Login] WHERE [Order].[Manager] = '{manager}' OR [Order].[Manager] IS NULL"), "Order", DataBase.Function.select, null);
            OrdersView.ItemsSource = DataBase.dataSet.Tables["Order"].DefaultView;
        }

        private void UsersView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowView = UsersView.SelectedItem as DataRowView;
        }

        private void OrdersView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowView = dg.SelectedItem as DataRowView;

            if (rowView != null)
            {
                string orderNumber = rowView[0].ToString();
                string productName = "";
                string query1 = $"SELECT [Name_Ord], [Ord_Product], [Examples], [Price] FROM [dbo].[Order] WHERE [Ord_Numb] = '{orderNumber}'";

                dataBase.openConnection();

                using (var command = new SqlCommand(query1, dataBase.getConnection()))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Assuming the query returns a single result
                    {
                        NameOrd.Text = reader["Name_Ord"].ToString();
                        productName = reader["Ord_Product"].ToString();
                        NameProduct.Text = productName;
                        Examples.Text = reader["Examples"].ToString();
                        Priсe.Text = reader["Price"].ToString();
                    }
                }

                string query3 = $"SELECT [Discriprion], [Prodict_Size] FROM [dbo].[Product] WHERE [Name_Product] = '{productName}'";
                using (var command1 = new SqlCommand(query3, dataBase.getConnection()))
                using (var reader1 = command1.ExecuteReader())
                {
                    if (reader1.Read()) // Assuming the query returns a single result
                    {
                        Discr.Text = reader1["Discriprion"].ToString();
                        string size = reader1["Prodict_Size"].ToString();
                        string[] sizes = size.Split('х');
                        SizeX.Text = sizes[0].ToString();
                        SizeY.Text = sizes[1].ToString();
                        string zWithUnit = sizes[2].Trim().Split(' ')[0];
                        SizeZ.Text = zWithUnit;
                        string unit = sizes[2].Trim().Substring(zWithUnit.Length);
                        foreach (ComboBoxItem item in SizeUnit.Items)
                        {
                            if (item.Content.ToString() == unit)
                            {
                                SizeUnit.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
                string login = rowView[2].ToString();

                foreach (var item in UsersView.Items)
                {
                    if (item is DataRowView rowView1 && rowView1[0].ToString() == login)
                    {
                        UsersView.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersView.SelectedItem == null)
            {
                string todayDate = DateTime.Now.ToString("dd.MM.yyyy");
                string fio = "", login = "", fn = "", pass;
                if (Check.IsChecked == true && !string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(FIOtb.Text) && !string.IsNullOrEmpty(Pass.Text))
                {
                    login = Login.Text;
                    fio = FIOtb.Text;
                    pass = Pass.Text;
                    dataBase.openConnection();
                    var addQuery = $"insert into [dbo].[User] values ('{login}', '{pass}', 'Заказчик', '{fio}', null)";
                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                else if (UsersView.SelectedItem != null)
                {
                    DataRowView rowView = UsersView.SelectedItem as DataRowView;
                    if (rowView != null)
                    {
                        login = rowView["Логин"].ToString();
                        fio = rowView["ФИО"].ToString();
                    }
                }
                else if (Check.IsChecked == false && UsersView.SelectedItem == null)
                {
                    MessageBox.Show("Создайте или выбирите заказчика!");
                    return;
                }
                string data = todayDate.Replace(".", "");
                string[] FIO = new string[0];
                if (!string.IsNullOrEmpty(fio))
                {
                    FIO = fio.Split(' ');
                }
                for (int i = 0; i < Math.Min(FIO.Length, 2); i++)
                {
                    string word = FIO[i];
                    if (!string.IsNullOrEmpty(word))
                    {
                        char firstChar = char.ToUpper(word[0]);
                        fn += firstChar;
                    }
                }
                int currentNumber = GetTodayOrderCount(todayDate) + 1;
                if (currentNumber > 99)
                {
                    currentNumber = 1;
                }

                string formattedNumber = currentNumber.ToString().PadLeft(2, '0');

                string number = data + fn + formattedNumber;
                string nameOrd = NameOrd.Text;
                string nameProd = NameProduct.Text;
                string price = Priсe.Text;
                if (!string.IsNullOrEmpty(nameProd) && !string.IsNullOrEmpty(nameOrd) && !string.IsNullOrEmpty(SizeX.Text) && !string.IsNullOrEmpty(SizeY.Text) && !string.IsNullOrEmpty(SizeZ.Text))
                {
                    string discr = Discr.Text;
                    string sizes = SizeX.Text+"х"+SizeY.Text + "х" + SizeZ.Text + " " + SizeUnit.Text;
                    string exmpl = Examples.Text;
                    if (string.IsNullOrEmpty(discr))
                        discr = "NULL";
                    else
                        discr = $"'{discr}'";
                    if (string.IsNullOrEmpty(sizes))
                        sizes = "NULL";
                    else
                        sizes = $"'{sizes}'";

                    if (string.IsNullOrEmpty(exmpl))
                        exmpl = "NULL";
                    else
                        exmpl = $"'{exmpl}'";

                    if (string.IsNullOrEmpty(price))
                        price = "NULL";
                    else
                        price = $"'{price}'";
                    var addQuery = $"INSERT INTO [dbo].[Product] VALUES ('{nameProd}', {sizes}, {discr})";
                    dataBase.openConnection();
                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    addQuery = $"INSERT INTO [dbo].[Order] VALUES ('{number}', '{todayDate}', '{nameOrd}', '{login}', '{manager}', {price}, NULL, {exmpl}, '{nameProd}', 'Составление спецификации')";
                    command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    OrdersFill();
                }
                else
                {
                    MessageBox.Show("Введите названия изделия и заказа, а также его размеры!");
                }
            }
            else
            {
                string nameOrd = NameOrd.Text;
                string nameProd = NameProduct.Text;
                if (!string.IsNullOrEmpty(nameProd) && !string.IsNullOrEmpty(nameOrd) && !string.IsNullOrEmpty(SizeX.Text) && !string.IsNullOrEmpty(SizeY.Text) && !string.IsNullOrEmpty(SizeZ.Text))
                {
                    DataRowView rowView = OrdersView.SelectedItem as DataRowView;

                    string originalProductName = string.Empty;
                    string getProductQuery = $"SELECT [Ord_Product] FROM [dbo].[Order] WHERE [Ord_Numb] = '{rowView[0].ToString()}' and [Status] = 'Новый'";
                    using (var getProductCommand = new SqlCommand(getProductQuery, dataBase.getConnection()))
                    using (var reader = getProductCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            originalProductName = reader["Ord_Product"].ToString();
                        }
                    }
                    if (originalProductName != string.Empty && originalProductName != null)
                    {
                        string discr = Discr.Text;
                        string sizes = SizeX.Text + "х" + SizeY.Text + "х" + SizeZ.Text + " " + SizeUnit.Text;
                        string exmpl = Examples.Text;
                        string price = Priсe.Text;
                        if (string.IsNullOrEmpty(discr))
                            discr = "NULL";
                        else
                            discr = $"'{discr}'";

                        if (string.IsNullOrEmpty(exmpl))
                            exmpl = "NULL";
                        else
                            exmpl = $"'{exmpl}'";
                        if (string.IsNullOrEmpty(price))
                            price = "NULL";
                        else
                            price = $"{price}";
                        var updateProductQuery = "";
                        var addQuery = "";
                        var deleteFrom = "";
                        if (originalProductName == nameProd)
                        {
                            updateProductQuery = $"UPDATE [dbo].[Product] SET [Prodict_Size] = '{sizes}', [Discriprion] = {discr} " +
                                 $"WHERE [Name_Product] = '{originalProductName}'";
                            addQuery = $"update [dbo].[Order] set [Name_Ord] = '{NameOrd.Text}', [Examples] = {exmpl}, [Price] = {price}, [Status] = 'Составление спецификации', [Manager] = '{manager}' where " +
                                $"[Ord_Numb] = '{rowView[0].ToString()}'";
                        }
                        else
                        {
                            updateProductQuery = $"INSERT INTO [dbo].[Product] VALUES ('{nameProd}', '{sizes}', {discr})";
                            addQuery = $"update [dbo].[Order] set [Name_Ord] = '{NameOrd.Text}', [Examples] = {exmpl}, [Price] = {price}, [Status] = 'Составление спецификации', [Manager] = '{manager}'," +
                                $"[Ord_Product] = '{nameProd}' where [Ord_Numb] = '{rowView[0].ToString()}'";
                            deleteFrom = $"DELETE FROM [dbo].[Product] where [Name_Product] = '{originalProductName}'";
                        }
                        var command = new SqlCommand(updateProductQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                        command = new SqlCommand(addQuery, dataBase.getConnection());
                        command.ExecuteNonQuery();
                        if (deleteFrom != "")
                        {
                            command = new SqlCommand(deleteFrom, dataBase.getConnection());
                            command.ExecuteNonQuery();
                        }
                        OrdersFill();
                    }
                    else
                        MessageBox.Show("Только новые заказы!");
                }
            }
        }

        private int GetTodayOrderCount(string todayDate)
        {
            int orderCount = 0;

            try
            {
                dataBase.openConnection();

                string query = $"SELECT COUNT(*) FROM [dbo].[Order] WHERE [Ord_Date] = '{todayDate}'";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());

                orderCount = Convert.ToInt32(command.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                dataBase.closeConnection();
            }

            return orderCount;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = OrdersView.SelectedItem as DataRowView;
            if (rowView != null)
            {
                if (rowView[4].ToString() == "Новый")
                {
                    DataBase dataBase = new DataBase();
                    dataBase.openConnection();
                    var delete = $"delete from [dbo].[Order] where [Ord_Numb] = '{rowView[0].ToString()}' and [Status] = 'Новый'";
                    var command = new SqlCommand(delete, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    OrdersFill();
                }
                else
                    MessageBox.Show("Удалять можно только новые заказы!");
            }
            else
            {
                MessageBox.Show("Не выбран заказ");
            }
        }

        private void Priсe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string newText = textBox.Text;
                foreach (char c in newText)
                {
                    if (!char.IsDigit(c) && c != '.')
                    {
                        textBox.Text = newText.Remove(newText.Length - 1); // Удаляет недопустимый символ
                        textBox.CaretIndex = newText.Length; // Перемещает курсор в конец текста
                        break;
                    }
                }
            }
        }

        private void SizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string newText = textBox.Text;
                foreach (char c in newText)
                {
                    if (!char.IsDigit(c))
                    {
                        textBox.Text = newText.Remove(newText.Length - 1); 
                        textBox.CaretIndex = newText.Length; 
                        break;
                    }
                }
            }
        }

        private void SizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string newText = textBox.Text;
                foreach (char c in newText)
                {
                    if (!char.IsDigit(c))
                    {
                        textBox.Text = newText.Remove(newText.Length - 1); 
                        textBox.CaretIndex = newText.Length; 
                        break;
                    }
                }
            }
        }

        private void SizeZ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string newText = textBox.Text;
                foreach (char c in newText)
                {
                    if (!char.IsDigit(c))
                    {
                        textBox.Text = newText.Remove(newText.Length - 1);
                        textBox.CaretIndex = newText.Length;
                        break;
                    }
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
