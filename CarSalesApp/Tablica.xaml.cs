using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CarSalesApp
{
    public partial class Tablica : Window
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

        public Tablica()
        {
            InitializeComponent();
            LoadSoldCarsData();
            LoadFreeCarsData();
        }

        private void LoadSoldCarsData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT [SellDate], [licensePlate], [CustName], [SellerName] FROM [dbo].[SellTbl]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable soldCarsTable = new DataTable();
                soldCarsTable.Load(reader);

                SoldCarsListView.ItemsSource = soldCarsTable.DefaultView;
            }
        }

        private void LoadFreeCarsData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT [brand], [model], [licensePlate], [Price], [available] FROM [dbo].[FreeTbl]";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable freeCarsTable = new DataTable();
                freeCarsTable.Load(reader);

                FreeCarsListView.ItemsSource = freeCarsTable.DefaultView;
            }
        }
    }
}