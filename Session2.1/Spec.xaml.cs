using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using Session2.Models;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Configuration;
using System.Data.Common;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace Session2._1
{
    /// <summary>
    /// Логика взаимодействия для Spec.xaml
    /// </summary>
    public partial class Spec : Window
    {
        string order = null;
        DataBase dataBase = new DataBase();
        string ingridients = "";
        string decorations = "";
        string orderNumb = "";
        public Spec(string data)
        {
            InitializeComponent();
            order = data;
            if (!string.IsNullOrWhiteSpace(order))
            {
                string[] orderParts = order.Split(',');
                if (orderParts.Length > 0)
                {
                    orderNumb = orderParts[0].Trim();
                }
            }
            foreach (ComboBoxItem item in IngerUnit.Items)
            {
                if (item.Content.ToString() == "г")
                {
                    IngerUnit.SelectedItem = item;
                    break;
                }
            }
            foreach (ComboBoxItem item in DecorUnit.Items)
            {
                if (item.Content.ToString() == "г")
                {
                    DecorUnit.SelectedItem = item;
                    break;
                }
            }
        }

        Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog()
        {
            FileName = "Document", // Default file name
            DefaultExt = ".docx", // Default file extension
            Filter = "Word documents (*.doc,*.docx)|*.doc;*.docx" // FilterLiter files by extension
        };

        private void OrdersFill()
        {
            dataBase.DataSetFill(string.Format($"SELECT [Indridients] as 'Ингридиенты', [Decorations] AS 'Декорации' FROM [dbo].[Order_Specification] inner join [dbo].[Order] on [Order].[Ord_Numb] = [Order_Specification].[Ord_Numb] where [Order_Specification].[Ord_Numb] = '{orderNumb}'"), "Order_Specification", DataBase.Function.select, null);
            OrderSpecView.ItemsSource = DataBase.dataSet.Tables["Order_Specification"].DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderName.Text = order;
            dataBase.DataSetFill(string.Format($"SELECT [Name_Ingr] as 'Название', [Type_Ingr] AS 'Тип' FROM [dbo].[Ingridient]"), "Ingridient", DataBase.Function.select, null);
            IngridientsView.ItemsSource = DataBase.dataSet.Tables["Ingridient"].DefaultView;

            dataBase.DataSetFill(string.Format($"SELECT [Name_Dec] as 'Название', [Type_Dec] AS 'Тип' FROM [dbo].[Decoration]"), "Decoration", DataBase.Function.select, null);
            DecorsView.ItemsSource = DataBase.dataSet.Tables["Decoration"].DefaultView;

            dataBase.DataSetFill(string.Format($"SELECT [Marking] as 'Маркировка', [Type_Equip] AS 'Тип' FROM [dbo].[Equipment] inner join [Type_Equipment] on [Type_Equip] = [Name_Type_Equipment]"), "Equipment", DataBase.Function.select, null);
            EquipView.ItemsSource = DataBase.dataSet.Tables["Equipment"].DefaultView;

            OrdersFill();
        }

        private void SearchIngridient_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dataView = DataBase.dataSet.Tables["Ingridient"].DefaultView;

            dataView.RowFilter = $"Название LIKE '%{SearchIngridient.Text}%'";

            IngridientsView.ItemsSource = dataView;
        }

        private void SearchDecor_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dataView = DataBase.dataSet.Tables["Decoration"].DefaultView;

            dataView.RowFilter = $"Название LIKE '%{SearchDecor.Text}%'";

            DecorsView.ItemsSource = dataView;
        }

        private void SearchEquip_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IngridientsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)IngridientsView.SelectedItem;
            if (selectedRow != null)
            {
                Ingridients.Text = selectedRow[0].ToString();
            }
        }

        private void DecorsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)DecorsView.SelectedItem;
            if (selectedRow != null)
            {
                Decorations.Text = selectedRow[0].ToString();
            }
        }

        private void plusIngr_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Ingridients.Text) && !string.IsNullOrWhiteSpace(IngrInput.Text))
            {
                if (!string.IsNullOrWhiteSpace(ingridients))
                {
                    ingridients += ", ";
                }

                ingridients += Ingridients.Text + IngrInput.Text + IngerUnit.Text;
            }
            Ingridients.Text = null;
            IngrInput.Text = null;
        }

        private void plusDecor_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Decorations.Text) && !string.IsNullOrWhiteSpace(DecorInput.Text))
            {
                if (!string.IsNullOrWhiteSpace(decorations))
                {
                    decorations += ", ";
                }

                decorations += Decorations.Text + DecorInput.Text + DecorUnit.Text;
            }
            Decorations.Text = null;
            DecorInput.Text = null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            dataBase.openConnection();
            var addQuery = $"INSERT INTO [dbo].[Order_Specification] ([Indridients], [Decorations], [Operations], [Ord_Numb]) " +
               $"VALUES ('{ingridients}', '{decorations}', '{Decorations_Copy.Text}', '{orderNumb}')";
            var command = new SqlCommand(addQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
            OrdersFill();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var Application = new Word.Application();

            Word.Document Document = Application.Documents.Add();
            Word.Paragraph Paragraph = Document.Paragraphs.Add();

            Word.Paragraph TitleParagraph = Document.Paragraphs.Add();
            Word.Range TitleRange = TitleParagraph.Range;
            TitleRange.Text = "Спецификация заказа";

            TitleRange.Font.Bold = 1;
            TitleRange.Font.Color = Word.WdColor.wdColorRed;
            TitleParagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            TitleRange.InsertParagraphAfter();

            Paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            Dictionary<string, string> fieldDescriptions = new Dictionary<string, string>
            {
                { "Login", "Логин:" },
                { "Customer", "Заказчик:" },
                { "Name_Ord", "Продукт:" },
                { "Indridients", "Ингридиенты:" },
                { "Decorations", "Декор:" },
                { "Operations", "Операции:" },
                { "Finish_Date", "Дедлайн:" }
            };
            string queryString = $"SELECT U.[Login] AS 'Customer', " +
                $"U.[FIO], O.[Name_Ord], OS.[Decorations],OS.[Indridients], " +
                $"OS.[Operations], O.[Finish_Date] FROM [dbo].[Order_Specification] AS OS " +
                $"INNER JOIN [dbo].[Order] AS O ON OS.[Ord_Numb] = O.[Ord_Numb] " +
                $"INNER JOIN [dbo].[User] AS U ON O.[Customer] = U.[Login] " +
                $"WHERE OS.[Ord_Numb] = '{orderNumb}'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Получаем описание поля
                    string description;
                    if (fieldDescriptions.TryGetValue(column.ColumnName, out description))
                    {
                        // Создаем параграф с описанием поля и его значением
                        Word.Paragraph DataParagraph = Document.Paragraphs.Add();
                        Word.Range DataRange = DataParagraph.Range;
                        DataRange.Text = $"{description} {row[column]}";
                        DataRange.InsertParagraphAfter();
                    }
                }
            }


            bool? result = saveFileDialog.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = saveFileDialog.FileName;
                Document.SaveAs(filename);
            }

            Application.Documents.Close();
            Application.Quit();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
