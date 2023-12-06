using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CarSalesApp
{
    public partial class Free : Window
    {
        public Free()
        {
            InitializeComponent();
            LoadFreeCarsData();
        }

        private void LoadFreeCarsData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";
            string query = "SELECT brand, model, licensePlate, Price, available FROM FreeTbl WHERE available = 'Free'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable freeCarsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(freeCarsTable);
                    FreeCarsListView.ItemsSource = freeCarsTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }
    }
}
