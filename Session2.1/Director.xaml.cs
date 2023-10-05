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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Window
    {
        DataBase dataBase = new DataBase();
        string reas = "";
        List<string> categories = new List<string>();
        Dictionary<string, int> repairTimes = new Dictionary<string, int>();
        public Director()
        {
            InitializeComponent();
            dataBase.openConnection();
            foreach (ComboBoxItem item in Reason.Items)
            {
                if (item.Content.ToString() == "Никакое")
                {
                    Reason.SelectedItem = item;
                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            failesFill();
        }

        public void failesFill()
        {
            dataBase.connection_Checking();

            string sqlQuery = $"SELECT [Id_Equip_Fail] as 'Номер', [Equip] as 'Оборудование', " +
                $"Concat([Start_Date], ' ', [Start_Time]) AS 'Начало', " +
                $"Concat([End_Date], ' ', [End_Time]) AS 'Устранено', " +
                $"[Reason] AS 'Причина', " +
                $"concat(DATEDIFF(MINUTE, " +
                $"CAST([Start_Date] AS DATETIME) + CAST([Start_Time] AS DATETIME), " +
                $"CAST([End_Date] AS DATETIME) + CAST([End_Time] AS DATETIME)), ' ', 'мин') " +
                $"AS 'Длительность устранения' " +
                $"FROM [dbo].[Equipment_Failures] " +
                $"INNER JOIN [dbo].[Equipment] ON [Equip] = [Marking] ";

            if (reas != "Никакое")
            {
                sqlQuery += $"WHERE [Reason] = '{reas}'"; 

            }
            else if (DateFirst.SelectedDate != null && DateSecond.SelectedDate != null)
            {
                DateTime startDate = DateFirst.SelectedDate.Value;
                DateTime endDate = DateSecond.SelectedDate.Value;

                sqlQuery += $"WHERE [Start_Date] >= '{startDate:yyyy-MM-dd}' OR [End_Date] <= '{endDate:yyyy-MM-dd}'";
            }

            dataBase.DataSetFill(sqlQuery, "Equipment_Failures", DataBase.Function.select, null);

            Equipfails_View.ItemsSource = DataBase.dataSet.Tables["Equipment_Failures"].DefaultView;
        }

        private void SerachDate_Click(object sender, RoutedEventArgs e)
        {
            failesFill();
            UpdateRepairTime();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Reason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reas = Reason.SelectedValue.ToString();
            string[] parts = reas.Split(':');
            if (parts.Length > 1)
                reas = parts[1].Trim();
            failesFill();
            UpdateRepairTime();
        }

        private void RepairTime()
        {
            // Проходим по каждой строке в DataGrid и суммируем длительности устранения
            foreach (DataRowView row in Equipfails_View.ItemsSource)
            {
                string reason = row["Причина"].ToString();
                string durationString = row["Длительность устранения"].ToString();

                // Извлекаем длительность из строки
                int duration;
                if (int.TryParse(durationString.Replace("мин", "").Trim(), out duration))
                {
                    if (repairTimes.ContainsKey(reason))
                        repairTimes[reason] += duration;
                    else
                        repairTimes[reason] = duration;
                }
            }

            foreach (var entry in repairTimes)
            {
                categories.Add($"{entry.Key}: {entry.Value} мин");
            }

            

            // Выводим суммарное время для всех типов сбоя
            int totalRepairTime = repairTimes.Values.Sum();
        }

        private void UpdateRepairTime()
        {
            int totalRepairTime = 0;

            // Проходим по каждой строке в DataGrid и суммируем длительности устранения
            foreach (DataRowView row in Equipfails_View.ItemsSource)
            {
                string durationString = row["Длительность устранения"].ToString();
                int duration;
                if (int.TryParse(durationString.Replace("мин", "").Trim(), out duration))
                {
                    totalRepairTime += duration;
                }
            }

            ItogTime.Text = "Суммарное время: " + totalRepairTime + " мин";
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true; // Сделать Excel видимым

            // Добавляем новую книгу
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.Sheets[1];


            RepairTime();

            int row = 1;
            foreach (var entry in repairTimes)
            {
                worksheet.Cells[row, 1].Value = entry.Key;
                worksheet.Cells[row, 2].Value = entry.Value; 
                row++;
            }

            Excel.Range chartRange = worksheet.Range["A1", $"B{categories.Count}"];

            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 300);
            Excel.Chart chart = chartObject.Chart;
            chart.ChartType = Excel.XlChartType.xlPie;
            chart.SetSourceData(chartRange);

            Marshal.ReleaseComObject(chart);
            Marshal.ReleaseComObject(chartObject);
            Marshal.ReleaseComObject(chartObjects);
            Marshal.ReleaseComObject(chartRange);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
