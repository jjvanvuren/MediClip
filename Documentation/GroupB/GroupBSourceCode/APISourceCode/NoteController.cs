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
    public class NoteController : ApiController
    {
        // String used to connect to the Azure SQL Server
        static String DATABASE_CONNECTION = "Server=tcp:mediclip.database.windows.net,1433;Initial Catalog=MediClipDB;Persist Security Info=False;User ID={userid};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Get all notes for Patient
        // GET /GetPatientNotes?id={id}
        [HttpGet]
        [Route("GetPatientNotes")]
        public IEnumerable<Note> Get(int id)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;

            // The SQL query to be sent to the server
            string query = "SELECT * FROM Note WHERE PatientID = '@PatientID'";

            //Replace @PatientID in the SQL query string with the specified patient ID
            query = query.Replace("@PatientID", Convert.ToString(id));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new list of Notes that will contain data from server
                List<Note> qresults = new List<Note>();

                // Read the data from the server to the List of Notes "qresults"
                // row by row
                while (reader.Read())
                {
                    Note qr = new Note();
                    qr.NoteID = Convert.ToInt32(reader["NoteID"].ToString());
                    qr.PatientID = Convert.ToInt32(reader["PatientID"].ToString());
                    qr.Title = reader["Title"].ToString();
                    qr.Text = reader["Text"].ToString();
                    qr.Picture = reader["Picture"].ToString();
                    qresults.Add(qr);
                }
                connection.Close(); // Close the connection to the server

                return qresults;
            }
        }

        // Get single Note by NoteID and PatientID
        // GET /GetNote?nId={nId}&pId={pId}
        [HttpGet]
        [Route("GetNote")]
        public Note Get(int nId, int pId)
        {
            string connectionString = DATABASE_CONNECTION;
            SqlDataReader reader = null;
            string query = "";

            // The SQL query to be sent to the server
            query = "SELECT * FROM Note WHERE NoteID = '@NoteID' AND PatientID = '@PatientID'";

            //Replace the values in the SQL query string with the specified NoteID and PatientID
            query = query.Replace("@NoteID", Convert.ToString(nId))
            .Replace("@PatientID", Convert.ToString(pId));

            // Establish connection with the SQL server
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection to the server

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the SQL query and connection details to the reader
                reader = command.ExecuteReader();

                // Create a new note that will contain data from server
                Note qresult = new Note();

                // Read the data from the server to qresults
                while (reader.Read())
                {
                    qresult.NoteID = Convert.ToInt32(reader["NoteID"].ToString());
                    qresult.PatientID = Convert.ToInt32(reader["PatientID"].ToString());
                    qresult.Title = reader["Title"].ToString();
                    qresult.Text = reader["Text"].ToString();
                    qresult.Picture = reader["Picture"].ToString();
                }
                connection.Close(); // Close the connection to the server

                return qresult;
            }
        }

        // Save a new note to the database
        // POST /SaveNote
        [HttpPost]
        [Route("SaveNote")]
        public bool SaveNote(PostNote note)
        {
            if (note == null) //Check if the note contains any data
            {
                return false;
            }
            else
            {
                string connectionString = DATABASE_CONNECTION;

                // The SQL query to be sent to the server
                string query = "INSERT INTO Note (PatientID, Title, Text, Picture) VALUES ('@PatientID', '@Title', '@Text', '@Picture');";

                //Replace the values in the SQL query string with the data to be added to the server
                query = query.Replace("@PatientID", Convert.ToString(note.PatientID))
                    .Replace("@Title", note.Title)
                    .Replace("@Text", note.Text)
                    .Replace("@Picture", note.Picture);

                // Establish connection with the SQL server
                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    connection.Open(); // Open the connection to the server

                    // Create the command to be sent to the server with the query and connection info
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery(); // Execute the command on the server
                    command.Dispose();
                    connection.Close(); // Close the connection to the server
                    return true;
                }
                catch (Exception)
                {
                    // If the command fails return false
                    return false;
                }
            }

        }
    }
}
