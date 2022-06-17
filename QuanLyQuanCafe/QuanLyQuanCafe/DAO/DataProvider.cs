using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;//đặt con trỏ vào biến cần đóng gói ctrl+R+E

        public static DataProvider Instance { get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; } set => instance = value; }
        private DataProvider() { }// class Singleton tồn tại một thể hiện trong một chương trình nếu ta có nhu cầu xài đi xài lại

        private string connectionstr = @"Data Source=DESKTOP-I6QJMI9\SQLEXPRESS;Initial Catalog=QuanLyCafe;Integrated Security=True";// cho thêm @ (h) \\ để pm định nghĩ đc đấu \ trong câu lệnh

        public DataTable ExecuteQuery(string query, object[] Parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // command.Parameters.AddWithValue("@username", id);
                if (Parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
        public int ExecuteNomQuery(string query, object[] Parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // command.Parameters.AddWithValue("@username", id);
                if (Parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();

                connection.Close();
            }
            return data;
        }
        public object ExecuteScalar(string query, object[] Parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // command.Parameters.AddWithValue("@username", id);
                if (Parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, Parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }
            return data;
        }

    }
}
