using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CarSalesApp
{
    public partial class Prodani : Window
    {
        public Prodani()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        private void SaveSaleButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCar = CarComboBox.SelectedItem.ToString();
            string selectedCustomer = CustomerComboBox.SelectedItem.ToString();
            string selectedSeller = SellerComboBox.SelectedItem.ToString();
            DateTime saleDate = SaleDatePicker.SelectedDate ?? DateTime.Now;

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO SellTbl (licensePlate, CustName, SellerName, SellDate) " +
                    "VALUES (@licensePlate, @CustName, @SellerName, @SellDate)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@licensePlate", selectedCar);
                insertCommand.Parameters.AddWithValue("@CustName", selectedCustomer);
                insertCommand.Parameters.AddWithValue("@SellerName", selectedSeller);
                insertCommand.Parameters.AddWithValue("@SellDate", saleDate);

                try
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Информация о продаже сохранена успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при сохранении информации о продаже.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении информации о продаже: " + ex.Message);
                }
            }
        }

            private void LoadComboBoxData()
        {
            LoadCarData();
            LoadCustomerData();
            LoadSellerData();
        }

        private void LoadCarData()
        {
            List<string> carData = new List<string>();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT licensePlate FROM CarTbl";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carData.Add(reader["licensePlate"].ToString());
                    }
                }
            }

            CarComboBox.ItemsSource = carData;
        }

        private void LoadCustomerData()
        {
            List<string> customerData = new List<string>();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CustName FROM CustomerTbl";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customerData.Add(reader["CustName"].ToString());
                    }
                }
            }

            CustomerComboBox.ItemsSource = customerData;
        }

        private void LoadSellerData()
        {
            List<string> sellerData = new List<string>();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SellerName FROM SellersTbl";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sellerData.Add(reader["SellerName"].ToString());
                    }
                }
            }

            SellerComboBox.ItemsSource = sellerData;
        }
    }
}
