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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        DataBase dataBase = new DataBase();
        string customer = null;
        public Client(string data)
        {
            InitializeComponent();
            customer = data;
            foreach (ComboBoxItem item in SizeUnit.Items)
            {
                if (item.Content.ToString() == "см")
                {
                    SizeUnit.SelectedItem = item;
                    break;
                }
            }
        }

        private void OrdersFill()
        {
            dataBase.DataSetFill(string.Format($"SELECT [Ord_Numb] AS 'Номер', [Name_Ord] AS 'Наименование', [Customer] AS 'Заказчик', [Manager] as 'Менеджер', [Ord_Date] AS 'Дата заказа', [Price] as 'Цена', [Status] as 'Статус заказа' FROM [dbo].[Order] INNER JOIN [dbo].[User] AS [CustomerUser] ON [Order].[Customer] = [CustomerUser].[Login] WHERE [Customer] = '{customer}'"), "Order", DataBase.Function.select, null);
            OrdersView.ItemsSource = DataBase.dataSet.Tables["Order"].DefaultView;
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

        private void OrdersView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowView = dg.SelectedItem as DataRowView;

            if (rowView != null)
            {
                string orderNumber = rowView[0].ToString();
                string productName = "";
                string query1 = $"SELECT [Name_Ord], [Ord_Product], [Examples] FROM [dbo].[Order] WHERE [Ord_Numb] = '{orderNumber}'";

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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersView.SelectedItem == null)
            {
                string todayDate = DateTime.Now.ToString("dd.MM.yyyy");
                string fio = "", login = "", fn = "", pass;
                dataBase.openConnection();
                string getProductQuery = $"SELECT [FIO] FROM [dbo].[User] WHERE [Login] = '{customer}'";
                using (var getProductCommand = new SqlCommand(getProductQuery, dataBase.getConnection()))
                using (var reader = getProductCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fio = reader["FIO"].ToString();
                    }
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
                if (!string.IsNullOrEmpty(nameProd) && !string.IsNullOrEmpty(nameOrd) && !string.IsNullOrEmpty(SizeX.Text) && !string.IsNullOrEmpty(SizeY.Text) && !string.IsNullOrEmpty(SizeZ.Text))
                {
                    string discr = Discr.Text;
                    string sizes = SizeX.Text + "х" + SizeY.Text + "х" + SizeZ.Text + " " + SizeUnit.Text;
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

                    dataBase.openConnection();
                    var addQuery = $"INSERT INTO [dbo].[Product] VALUES ('{nameProd}', {sizes}, {discr})";
                    var command = new SqlCommand(addQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    addQuery = $"INSERT INTO [dbo].[Order] VALUES ('{number}', '{todayDate}', '{nameOrd}', '{customer}', null, null, NULL, {exmpl}, '{nameProd}', 'Новый')";
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
                        if (string.IsNullOrEmpty(discr))
                            discr = "NULL";
                        else
                            discr = $"'{discr}'";

                        if (string.IsNullOrEmpty(exmpl))
                            exmpl = "NULL";
                        else
                            exmpl = $"'{exmpl}'";
                        var updateProductQuery = "";
                        var addQuery = "";
                        var deleteFrom = "";
                        if (originalProductName == nameProd)
                        {
                            updateProductQuery = $"UPDATE [dbo].[Product] SET [Prodict_Size] = '{sizes}', [Discriprion] = {discr} " +
                                 $"WHERE [Name_Product] = '{originalProductName}'";
                            addQuery = $"update [dbo].[Order] set [Name_Ord] = '{NameOrd.Text}', [Examples] = {exmpl}, [Status] = 'Составление спецификации' where " +
                                $"[Ord_Numb] = '{rowView[0].ToString()}'";
                        }
                        else
                        {
                            updateProductQuery = $"INSERT INTO [dbo].[Product] VALUES ('{nameProd}', '{sizes}', {discr})";
                            addQuery = $"update [dbo].[Order] set [Name_Ord] = '{NameOrd.Text}', [Examples] = {exmpl}," +
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

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = OrdersView.SelectedItem as DataRowView;
            if (rowView != null)
            {
                if (rowView[6].ToString() == "Новый")
                {
                    DataBase dataBase = new DataBase();
                    dataBase.openConnection();
                    var update = $"update [dbo].[Order] set [Status] = 'Отменен' where " +
                    $"[Ord_Numb] = '{rowView[0].ToString()}'";
                    var command = new SqlCommand(update, dataBase.getConnection());
                    command.ExecuteNonQuery();
                    OrdersFill();
                }
                else
                    MessageBox.Show("Удалять можно только новые заказы!");
            }
            else
                MessageBox.Show("Не выбран заказ");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataBase.connection_Checking();
            OrdersFill();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            dataBase.closeConnection();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
