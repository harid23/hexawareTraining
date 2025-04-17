using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity;
using DBUtility;
using PetPals.Entity;
using Exceptions;

namespace DAO
{
    public class PetDAO
    {
        private string connectionString;

        public PetDAO(string connString)
        {
            connectionString = connString;
        }

        public void AddPet(Pet pet)
        {
         

            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "INSERT INTO Pets (Name, Age, Breed, Type) VALUES (@Name, @Age, @Breed, @Type)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", pet.Name);
                cmd.Parameters.AddWithValue("@Age", pet.Age);
                cmd.Parameters.AddWithValue("@Breed", pet.Breed);
                cmd.Parameters.AddWithValue("@Type", pet.Type);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void RemovePet(int petId)
        {
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "DELETE from Pets Where PetID = @PetId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PetId", petId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<Pet> GetAllPets()
        {
            List<Pet> pets = new List<Pet>();
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Select * from Pets";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pet pet = new Pet(
                        Convert.ToInt32(reader["PetId"]),
                        reader["Name"].ToString(),
                        Convert.ToInt32(reader["Age"]),
                        reader["Breed"].ToString(),
                        reader["Type"].ToString()
                    );
                    pets.Add(pet);
                }

                conn.Close();
            }
            return pets;
        }
    }
}