using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CarSalesApp
{
    public partial class Cars : Window
    {
        public Cars()
        {
            InitializeComponent();
        }

        private void SaveCarButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = BrandTextBox.Text;
            string model = ModelTextBox.Text;
            string licensePlate = LicensePlateTextBox.Text;
            double price;

            if (double.TryParse(PriceTextBox.Text, out price))
            {
                string status = ((ComboBoxItem)StatusComboBox.SelectedItem).Content.ToString();

                if (SaveCarToDatabase(brand, model, licensePlate, price, status))
                {
                    if (SaveCarToFreeTable(brand, model, licensePlate, price, status))
                    {
                        MessageBox.Show("Машина сохранена успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при сохранении машины в FreeTbl.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении машины.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка при вводе цены.");
            }
        }

        private bool SaveCarToDatabase(string brand, string model, string licensePlate, double price, string status)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO CarTbl (brand, model, licensePlate, price, available) VALUES (@Brand, @Model, @LicensePlate, @Price, @Available)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                command.Parameters.AddWithValue("@Price", price);

                // Установка значения "Free" или "Sold" в зависимости от выбранного статуса
                if (status == "Free" || status == "Sold")
                {
                    command.Parameters.AddWithValue("@Available", status);
                }
                else
                {
                    command.Parameters.AddWithValue("@Available", DBNull.Value);
                }

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        private bool SaveCarToFreeTable(string brand, string model, string licensePlate, double price, string status)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO FreeTbl (brand, model, licensePlate, Price, available) VALUES (@Brand, @Model, @LicensePlate, @Price, @Available)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                command.Parameters.AddWithValue("@Price", price);

                if (status == "Free" || status == "Sold")
                {
                    command.Parameters.AddWithValue("@Available", status);
                }
                else
                {
                    command.Parameters.AddWithValue("@Available", DBNull.Value);
                }

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}