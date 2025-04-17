using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DBUtility;

namespace DAO
{
    public class AdoptionEventDAO
    {
        private string connectionString;

        public AdoptionEventDAO(string connString)
        {
            connectionString = connString;
        }

        public void RegisterParticipant(string participantName, int eventId)
        {
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Insert into Participants (ParticipantName, EventId) Values (@ParticipantName, @EventId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ParticipantName", participantName);
                cmd.Parameters.AddWithValue("@EventId", eventId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddAdoptionEvent(string eventName, DateTime eventDate)
        {
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Insert into AdoptionEvents (EventName, EventDate) Values (@EventName, @EventDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EventName", eventName);
                cmd.Parameters.AddWithValue("@EventDate", eventDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<string> GetParticipantsForEvent(int eventId)
        {
            List<string> participants = new List<string>();
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "Select ParticipantName from Participants Where EventId = @EventId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EventId", eventId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    participants.Add(reader["ParticipantName"].ToString());
                }

                conn.Close();
            }
            return participants;
        }

        public List<string> GetUpcomingEvents()
        {
            List<string> events = new List<string>();
            using (SqlConnection conn = DBConnection.GetConnection(connectionString))
            {
                string query = "SELECT * FROM AdoptionEvents";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string eventDetail = $"Event ID: {reader["EventId"]}, Event Name: {reader["EventName"]}, Date: {reader["EventDate"]}";
                    events.Add(eventDetail);
                }

                conn.Close();
            }
            return events;
        }
    }
}
