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
    public class NurseController : ApiController
    {
        // String used to connect to the Azure SQL Server
        static String DATABASE_CONNECTION = "Server=tcp:mediclip.database.windows.net,1433;Initial Catalog=MediClipDB;Persist Security Info=False;User ID=jacques;Password=gd3*#4XZ3iyFSD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Retrieve all nurses from the database
        //GET /GetAllNurses
        [HttpGet]
        [Route("GetAllNurses")]
        public IEnumerable<Nurse> Get()
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Nurse";

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            //===================================================================================================
            //Reference D2: Externally Sourced algorithm
            //Purpose: Used to read the data from the SQL server to into the list of nurses.
            //         This method of reading data from the SQL server was also used in other controllers.
            //Date: 19/10/2018
            //Source: stackoverflow
            //Author: trx
            //URL: https://stackoverflow.com/questions/41965076/web-api-to-return-result-from-sql-database
            //Adaption Required: Change the List to suit the required Model. Change the reader while loop to suit
            //                   the SQL required table and Model. 
            //===================================================================================================

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new list of Nurses that will contain data from server
                List<Nurse> qresults = new List<Nurse>();

                // Read the data from the server to the List of nurses "qresults"
                // row by row
                while (reader.Read())
                {
                    Nurse qr = new Nurse();
                    qr.NurseID = Convert.ToInt32(reader["NurseID"].ToString());
                    qr.UserName = reader["UserName"].ToString();
                    qr.FirstName = reader["FirstName"].ToString();
                    qr.LastName = reader["LastName"].ToString();
                    qr.Password = reader["Password"].ToString();
                    qresults.Add(qr);
                }
                connection.Close(); // Close the connection to the server

                return qresults;
            }
            //============================================= 
            //End reference D1
            //============================================= 
        }


        // Retrieve a single nurse from the database using their username
        //GET /GetNurse?uname={uname}
        [HttpGet]
        [Route("GetNurse")]
        public Nurse Get(String uname)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Nurse WHERE UserName = '@UserName'";

            //Replace @UserName with the requested nurses username
            query = query.Replace("@UserName", Convert.ToString(uname));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new nurse that will contain data from server
                Nurse qresult = new Nurse();

                // Read the data from the server to qresult
                while (reader.Read())
                {
                    qresult.NurseID = Convert.ToInt32(reader["NurseID"].ToString());
                    qresult.UserName = reader["UserName"].ToString();
                    qresult.FirstName = reader["FirstName"].ToString();
                    qresult.LastName = reader["LastName"].ToString();
                    qresult.Password = reader["Password"].ToString();
                }
                connection.Close(); // Close the connection to the server

                return qresult;
            }
        }
    }
}
