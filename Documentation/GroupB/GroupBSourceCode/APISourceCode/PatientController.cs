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
    public class PatientController : ApiController
    {
        // String used to connect to the Azure SQL Server
        static String DATABASE_CONNECTION = "Server=tcp:mediclip.database.windows.net,1433;Initial Catalog=MediClipDB;Persist Security Info=False;User ID={userid};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Retrieve all patients
        // GET /GetAllPatients
        [HttpGet]
        [Route("GetAllPatients")]
        public IEnumerable<Patient> Get()
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Patient";

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new list of patients that will contain data from server
                List<Patient> qresults = new List<Patient>();

                // Read the data from the server to the List of patients "qresults"
                // row by row
                while (reader.Read())
                {
                    Patient qr = new Patient();
                    qr.PatientID = Convert.ToInt32(reader["PatientID"].ToString());
                    qr.WardID = Convert.ToInt32(reader["WardID"].ToString());
                    qr.AssignDateFrom = reader["AssignDateFrom"].ToString();
                    qr.AssignDateTo = reader["AssignDateTo"].ToString();
                    qr.FirstName = reader["FirstName"].ToString();
                    qr.LastName = reader["LastName"].ToString();
                    qr.Dob = reader["Dob"].ToString();
                    qr.Sex = reader["Sex"].ToString();
                    qr.Dosage = reader["Dosage"].ToString();
                    qr.Picture = reader["Picture"].ToString();
                    qresults.Add(qr);
                }
                connection.Close(); // Close the connection to the server

                return qresults;
            }
        }

        // Retrieve all patients in a specific ward
        // GET /GetWardPatients?id={id}
        [HttpGet]
        [Route("GetWardPatients")]
        public IEnumerable<Patient> Get(int id)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Patient WHERE WardID = '@WardID'";

            //Replace @WardID with the requested ward id
            query = query.Replace("@WardID", Convert.ToString(id));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new list of patients that will contain data from server
                List<Patient> qresults = new List<Patient>();

                // Read the data from the server to the List of patients "qresults"
                // row by row
                while (reader.Read())
                {
                    Patient qr = new Patient();
                    qr.PatientID = Convert.ToInt32(reader["PatientID"].ToString());
                    qr.WardID = Convert.ToInt32(reader["WardID"].ToString());
                    qr.AssignDateFrom = reader["AssignDateFrom"].ToString();
                    qr.AssignDateTo = reader["AssignDateTo"].ToString();
                    qr.FirstName = reader["FirstName"].ToString();
                    qr.LastName = reader["LastName"].ToString();
                    qr.Dob = reader["Dob"].ToString();
                    qr.Sex = reader["Sex"].ToString();
                    qr.Dosage = reader["Dosage"].ToString();
                    qr.Picture = reader["Picture"].ToString();
                    qresults.Add(qr);
                }
                connection.Close(); // Close the connection to the server

                return qresults;
            }
        }

        // Retrieve single patient by WardID and PatientID
        //GET /GetPatient?wId={wId}&pId={pId}
        [HttpGet]
        [Route("GetPatient")]
        public Patient Get(int wId, int pId)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Patient WHERE WardID = '@WardID' AND PatientID = '@PatientID'";

            //Replace @WardID and @PatientID with the requested ward and patient IDs
            query = query.Replace("@WardID", Convert.ToString(wId))
                .Replace("@PatientID", Convert.ToString(pId));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new patient that will contain data from server
                Patient qresult = new Patient();

                // Read the data from the server to qresults
                while (reader.Read())
                {
                    qresult.PatientID = Convert.ToInt32(reader["PatientID"].ToString());
                    qresult.WardID = Convert.ToInt32(reader["WardID"].ToString());
                    qresult.AssignDateFrom = reader["AssignDateFrom"].ToString();
                    qresult.AssignDateTo = reader["AssignDateTo"].ToString();
                    qresult.FirstName = reader["FirstName"].ToString();
                    qresult.LastName = reader["LastName"].ToString();
                    qresult.Dob = reader["Dob"].ToString();
                    qresult.Sex = reader["Sex"].ToString();
                    qresult.Dosage = reader["Dosage"].ToString();
                    qresult.Picture = reader["Picture"].ToString();
                }
                connection.Close(); // Close the connection to the server

                return qresult;
            }
        }
    }
}
