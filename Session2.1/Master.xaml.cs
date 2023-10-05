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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Master.xaml
    /// </summary>
    public partial class Master : Window
    {
        DataBase dataBase = new DataBase();
        public Master()
        {
            InitializeComponent();
            foreach (ComboBoxItem item in Reason.Items)
            {
                if (item.Content.ToString() == "Естественный износ")
                {
                    Reason.SelectedItem = item;
                    break;
                }
            }
            string query = "SELECT [Marking] FROM [dbo].[Equipment]";
            dataBase.openConnection();
            using (var command = new SqlCommand(query, dataBase.getConnection()))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read()) // Если удалось прочитать первую запись
                {
                    string productName = reader["Marking"].ToString();
                    Equipment.Items.Add(productName);
                    Equipment.SelectedItem = productName;
                }
            }
        }

    private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void EquipFail_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)EquipFail_View.SelectedItem;
            if (selectedRow != null)
            {
                int idEquipFail = (int)selectedRow["Номер"];
                dataBase.openConnection();
                string query1 = $"SELECT [Start_Date], [Start_Time], [Reason], [Equip] FROM [dbo].[Equipment_Failures] WHERE [Id_Equip_Fail] = {idEquipFail}";
                using (var command = new SqlCommand(query1, dataBase.getConnection()))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Assuming the query returns a single result
                    {
                        DateFail.Text = reader["Start_Date"].ToString();
                        string productName = reader["Start_Time"].ToString();
                        string[] times = productName.Split(":");
                        hh.Text = times[0];
                        mm.Text = times[1];

                        string reasonValue = reader["Reason"].ToString();
                        Reason.Text = reasonValue;

                        string equipmentValue = reader["Equip"].ToString();
                        Equipment.Text = equipmentValue;
                    }
                }
            }
        }

        private void Save_Fail_Click(object sender, RoutedEventArgs e)
        {
            if (EquipFail_View.SelectedItem == null)
            {
                string date = DateFail.Text;
                string time = hh.Text+ ":" + mm.Text;
                dataBase.openConnection();
                var addQuery = $"INSERT INTO [dbo].[Equipment_Failures] VALUES ('{date}', null, '{time}', null, '{Reason.Text}', '{Equipment.Text}')";
                var command = new SqlCommand(addQuery, dataBase.getConnection());
                command.ExecuteNonQuery();
                failesFill();
            }
            else
            {
                DataRowView selectedRow = (DataRowView)EquipFail_View.SelectedItem;
                int idEquipFail = (int)selectedRow["Номер"];
                string date = DateFail.Text;
                string time = hh.Text + ":" + mm.Text;
                dataBase.openConnection();
                var updateQuery = $"UPDATE [dbo].[Equipment_Failures] SET [Start_Date] = '{date}', [Start_Time] = '{time}', [Reason] = '{Reason.Text}', [Equip] = '{Equipment.Text}' WHERE [Id_Equip_Fail] = {idEquipFail}";
                var command = new SqlCommand(updateQuery, dataBase.getConnection());
                command.ExecuteNonQuery();
                failesFill();
            }
        }

        private void Del_Fail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataBase.connection_Checking();
            EquipFill();
            failesFill();
            dataBase.DataSetFill(string.Format(
        $"SELECT [Ord_Numb] AS 'Номер', [Name_Ord] AS 'Наименование', [Product].[Prodict_Size] AS 'Размеры продукта', [Product].[Discriprion] AS 'Описание', [Customer] AS 'Заказчик', [Ord_Date] AS 'Дата заказа', [Status] as 'Статус заказа', [Manager] as 'Менеджер' " +
        $"FROM [dbo].[Order] " +
        $"INNER JOIN [dbo].[User] AS [CustomerUser] ON [Order].[Customer] = [CustomerUser].[Login] " +
        $"INNER JOIN [dbo].[Product] ON [Order].[Ord_Product] = [Product].[Name_Product]"),
        "Order", DataBase.Function.select, null);

            OrdersView.ItemsSource = DataBase.dataSet.Tables["Order"].DefaultView;
        }

        public void failesFill()
        {
            dataBase.DataSetFill(string.Format($"SELECT [Id_Equip_Fail] as 'Номер', [Equip] as 'Оборудование', Concat([Start_Date], ' ', [Start_Time]) AS 'Начало', Concat ([End_Date], ' ', [End_Time]) as 'Устранено', [Reason] AS 'Причина' FROM [dbo].[Equipment_Failures] INNER JOIN [dbo].[Equipment] ON [Equip] = [Marking]"), "Equipment_Failures", DataBase.Function.select, null);
            EquipFail_View.ItemsSource = DataBase.dataSet.Tables["Equipment_Failures"].DefaultView;
        }

        private void EquipFill()
        {
            string command = "select [Marking] from [dbo].[Equipment]";
            SqlDataAdapter da = new SqlDataAdapter(command, dataBase.getConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Equipment.Items.Add(dt.Rows[i]["Marking"].ToString());
            }
        }

        private void Repair_Fail_Click(object sender, RoutedEventArgs e)
        {
            if (EquipFail_View.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)EquipFail_View.SelectedItem;
                int idEquipFail = (int)selectedRow["Номер"];
                DateTime dateT = DateTime.Now;
                string[] date = dateT.ToString().Split(" ");
                dataBase.openConnection();
                var updateQuery = $"UPDATE [dbo].[Equipment_Failures] SET [End_Date] = '{date[0]}', [End_Time] = '{date[1]}' WHERE [Id_Equip_Fail] = {idEquipFail}";
                var command = new SqlCommand(updateQuery, dataBase.getConnection());
                command.ExecuteNonQuery();
                failesFill();
            }
        }

        private void mm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(mm.Text, out int value) || value > 59)
            {
                if (mm.Text.Length > 0)
                {
                    mm.Text = mm.Text.Substring(0, mm.Text.Length - 1);
                    mm.SelectionStart = mm.Text.Length;
                }
            }
        }

        private void hh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(hh.Text, out int value) || value > 23)
            {
                if (hh.Text.Length > 0)
                {
                    hh.Text = hh.Text.Substring(0, hh.Text.Length - 1);
                    hh.SelectionStart = hh.Text.Length;
                }
            }
        }

        private void OrdersView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = OrdersView.SelectedItems;
            List<string> dataList = new List<string>();

            foreach (var selectedItem in selectedItems)
            {
                string data = GetRequiredDataFromSelectedItem(selectedItem);

                dataList.Add(data);
            }

            string dataToSend = string.Join(",", dataList);

            Spec spec = new Spec(dataToSend);
            spec.Show();
            Close();
        }
        private string GetRequiredDataFromSelectedItem(object selectedItem)
        {
            if (selectedItem is DataRowView item)
            {
                string column1Data = item.Row[0].ToString();
                string column2Data = item.Row[1].ToString();
                string column3Data = item.Row[2].ToString();
                string column4Data = item.Row[3].ToString();
                string column5Data = item.Row[4].ToString();

                // Здесь вы можете объединить данные в строку или вернуть их как угодно
                string rowData = $"{column1Data}, {column2Data}, {column3Data}, {column4Data}, {column5Data}";

                return rowData;
            }
            return string.Empty;
        }

    }
}
