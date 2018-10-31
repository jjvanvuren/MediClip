using MediClipWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediClipWebAPI.Controllers
{
    public class WardController : ApiController
    {
        // String used to connect to the Azure SQL Server
        static String DATABASE_CONNECTION = "Server=tcp:mediclip.database.windows.net,1433;Initial Catalog=MediClipDB;Persist Security Info=False;User ID={userid};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Retrieve all wards from the database
        //GET /GetAllWards
        [HttpGet]
        [Route("GetAllWards")]
        public IEnumerable<Ward> Get()
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Ward";

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new list of wards that will contain data from server
                List<Ward> qresults = new List<Ward>();

                // Read the data from the server to the List of wards "qresults"
                // row by row
                while (reader.Read())
                {
                    Ward qr = new Ward();
                    qr.WardID = Convert.ToInt32(reader["WardID"].ToString());
                    qr.Name = reader["Name"].ToString();
                    qr.Description = reader["Description"].ToString();
                    qresults.Add(qr);
                }
                connection.Close(); // Close the connection to the server

                return qresults;
            }
        }

        // Retrieve a single ward from the database using the ward id
        //GET /GetWard?id={id}
        [HttpGet]
        [Route("GetWard")]
        public Ward Get(int id)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Ward WHERE WardID = '@WardID'";

            //Replace @WardID with the requested ward id
            query = query.Replace("@WardID", Convert.ToString(id));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new ward that will contain data from server
                Ward qresult = new Ward();

                // Read the data from the server to qresults
                while (reader.Read())
                {
                    qresult.WardID = Convert.ToInt32(reader["WardID"].ToString());
                    qresult.Name = reader["Name"].ToString();
                    qresult.Description = reader["Description"].ToString();
                }
                connection.Close(); // Close the connection to the server

                return qresult;
            }
        }
    }
}
