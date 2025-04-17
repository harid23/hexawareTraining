using System;
using System.Data.SqlClient;
using PetPals.Entity;
using DBUtility;

namespace DAO
{
    public class DonationDAO
    {
        private string connectionString;

        public DonationDAO(string connString)
        {
            connectionString = connString;
        }

        public void RecordCashDonation(CashDonation donation)
        {
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Insert into Donations (DonorName, Amount, DonationDate, Type) Values (@DonorName, @Amount, @DonationDate, @Type)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DonorName", donation.DonorName);
                cmd.Parameters.AddWithValue("@Amount", donation.Amount);
                cmd.Parameters.AddWithValue("@DonationDate", donation.DonationDate);
                cmd.Parameters.AddWithValue("@Type", "Cash");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void RecordItemDonation(ItemDonation donation)
        {
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Insert into Donations (DonorName, Amount, DonationDate, Type) Values (@DonorName, @Amount, @DonationDate, @Type)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DonorName", donation.DonorName);
                cmd.Parameters.AddWithValue("@Amount", donation.Amount);
                cmd.Parameters.AddWithValue("@DonationDate", donation.DonationDate);
                cmd.Parameters.AddWithValue("@Type", "Item");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}