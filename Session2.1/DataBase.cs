using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Session2._1
{
    internal class DataBase
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=DESKTOP-3IGPDRB\SQLEXPRESS;Initial Catalog=Conditer_DataBase; Integrated Security=True");
        public static DataSet dataSet = new DataSet();
        private DataTable dtSupplyer = new DataTable("Supplyer");
        private DataTable dtDecoration = new DataTable("Decoration");
        private DataTable dtIngridient = new DataTable("Ingridient");
        private DataTable dtProduct = new DataTable("Product");
        private DataTable dtCakeDecSpec = new DataTable("Cake_Decoration_Specification");
        private DataTable dtIngrSpec = new DataTable("Ingridients_Specification");
        private DataTable dtSemimSpec = new DataTable("Semimanufactures_Specification");
        private DataTable dtTypeEquip = new DataTable("Type_Equipment");
        private DataTable dtEquipment = new DataTable("Equipment");
        private DataTable dtOperSpec = new DataTable("Operation_Specification");
        private DataTable dtUser = new DataTable("User");
        private DataTable dtOrder = new DataTable("Order");
        private DataTable dtEquipFail = new DataTable("Equipment_Failures");
        private DataTable dtOrdSpec = new DataTable("Order_Specification");
        public enum Function { select, insert, update, delete };

        public void openConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void closeConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return _connection;
        }
        public bool connection_Checking()
        {
            try
            {
                openConnection();
                dataSet.Tables.Clear();
                dataSet.Tables.Add(dtSupplyer);
                dataSet.Tables.Add(dtDecoration);
                dataSet.Tables.Add(dtIngridient);
                dataSet.Tables.Add(dtProduct);
                dataSet.Tables.Add(dtCakeDecSpec);
                dataSet.Tables.Add(dtIngrSpec);
                dataSet.Tables.Add(dtSemimSpec);
                dataSet.Tables.Add(dtTypeEquip);
                dataSet.Tables.Add(dtEquipment);
                dataSet.Tables.Add(dtOperSpec);
                dataSet.Tables.Add(dtUser);
                dataSet.Tables.Add(dtOrder);
                dataSet.Tables.Add(dtEquipFail);
                dataSet.Tables.Add(dtOrdSpec);
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Кондитерская");
                return false;
            }
            finally
            {
                closeConnection();
            }
        }

        public void DataSetFill(string SQLQuery, string TableName, Function function, ArrayList valueList)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(SQLQuery, _connection);
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand("", _connection);
            try
            {
                openConnection();
                dataSet.Tables[TableName].Columns.Clear();
                dataSet.Tables[TableName].Rows.Clear();
                switch (function)
                {
                    case Function.select:
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.insert:
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}') and is_identity <> 1", TableName);
                        table.Load(command.ExecuteReader());
                        string insertquery = string.Format("insert into [dbo].[{0}] (", TableName);
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            insertquery += string.Format(" [{0}]", table.Rows[i][0]);
                            if (i < table.Rows.Count - 1)
                                insertquery += ",";
                        }
                        insertquery += ") values (";
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            insertquery += string.Format(" @{0}", table.Rows[i][0]);
                            if (i < table.Rows.Count - 1)
                                insertquery += ",";
                        }
                        insertquery += ")";
                        adapter.InsertCommand = new SqlCommand(insertquery);
                        adapter.InsertCommand.Connection = _connection;
                        adapter.InsertCommand.Parameters.Clear();
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            adapter.InsertCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[i][0]), valueList[i]);
                        }
                        adapter.InsertCommand.ExecuteNonQuery();
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.update:
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}')", TableName);
                        table.Load(command.ExecuteReader());
                        string updatequery = string.Format("update [dbo].[{0}] set", TableName);
                        for (int i = 1; i <= table.Rows.Count - 1; i++)
                        {
                            updatequery += string.Format(" {0} = @{0}", table.Rows[i][0]);
                            if (i < table.Rows.Count - 1)
                                updatequery += ",";
                        }
                        updatequery += string.Format(" where {0} = @{0}", table.Rows[0][0]);
                        adapter.UpdateCommand = new SqlCommand(updatequery);
                        adapter.UpdateCommand.Connection = _connection;
                        adapter.UpdateCommand.Parameters.Clear();
                        //Организация цикла для присвоения полученного списка значений в параметры запроса на изменение данных
                        for (int i = 0; i <= table.Rows.Count - 1; i++)
                        {
                            //Добавление, в коллекцию свойства UpdateCommand, значений в параметры по его названию
                            adapter.UpdateCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[i][0]), valueList[i]);
                        }
                        adapter.UpdateCommand.ExecuteNonQuery();
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                    case Function.delete:
                        command.CommandText = string.Format("select name from sys.columns where object_id = (select object_id from sys.tables where name = '{0}') and is_identity = 1", TableName);
                        table.Load(command.ExecuteReader());
                        string deletequery = string.Format("delete from [dbo].[{0}] where [{1}] = @{1}", TableName, table.Rows[0][0]);
                        adapter.DeleteCommand = new SqlCommand(deletequery);
                        adapter.DeleteCommand.Connection = _connection;
                        adapter.DeleteCommand.Parameters.Clear();
                        adapter.DeleteCommand.Parameters.AddWithValue(string.Format("@{0}", table.Rows[0][0]), valueList[0]);
                        adapter.DeleteCommand.ExecuteNonQuery();
                        adapter.Fill(dataSet.Tables[TableName]);
                        break;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Кондитерская");
            }
            finally
            {
                closeConnection();
            }

        }
    }
}
