using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MediClip.Models;
using System.Collections.Generic;
using System.IO;

namespace MediClip.Client
{
    public class MediClipClient
    {
        //============================================= 
        //Reference B1: Externally Sourced algorithms
        //Purpose: To GET and POST information to and from API
        //Date: 23/10/2018
        //Source: Lecture Slides and Lab material
        //Author: Unknown
        //URL: None
        //Adaption Required: Had to adapt the algorithms to work with our API
        //and change different aspects of it to work with our application e.g. Posting and getting lists. 
        //=============================================

        //API URL that is used to GET/POST all information
        const String API_URL = "https://mediclipwebapi.azurewebsites.net/"; 

        // Get list of all wards from the API
        public async Task<List<Ward>> ListWard()
        {

            String searchUrl = API_URL + "GetAllWards";
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                List<Ward> Wards = JsonConvert.DeserializeObject<List<Ward>>(content);

                return Wards;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        // Get list of all Patients from the API
        public async Task<List<Patient>> ListPatient(int id)
        {

            String searchUrl = API_URL + "GetWardPatients?id=" + Convert.ToString(id);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                List<Patient> Patients = JsonConvert.DeserializeObject<List<Patient>>(content);

                return Patients;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        // Get Patient from the API BY ID
        public async Task<Patient> PatientByID(int wardId, int patientId)
        {

            String searchUrl = API_URL + "GetPatient?wId=" + Convert.ToString(wardId) + "&pId=" + Convert.ToString(patientId);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                Patient patient = JsonConvert.DeserializeObject<Patient>(content);

                return patient;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        // Get Notes for Patient from the API BY ID
        public async Task<List<Note>> PatientByID(int patientId)
        {

            String searchUrl = API_URL + "GetPatientNotes?id=" + Convert.ToString(patientId);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                List<Note> notes = JsonConvert.DeserializeObject<List<Note>>(content);

                return notes;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        // Get Note for Patient from the API BY ID
        public async Task<Note> GetNote(int noteId, int patientId)
        {

            String searchUrl = API_URL + "GetNote?nId=" + Convert.ToString(noteId) + "&pId=" + Convert.ToString(patientId);
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                Note singleNote = JsonConvert.DeserializeObject<Note>(content);

                return singleNote;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        // Get Nurse from the API by UserName
        public async Task<Nurse> GetNurse(string sNurseUserName)
        {

            String searchUrl = API_URL + "GetNurse?uname=" + sNurseUserName;
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                Nurse singleNurse = JsonConvert.DeserializeObject<Nurse>(content);

                return singleNurse;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }
        }

        //============================================= 
        //End reference B1
        //============================================= 

        //============================================= 
        //Reference B2: Externally Sourced algorithms
        //Purpose: To POST information to the API
        //Date: 25/10/2018
        //Source: stackoverflow
        //Author: Ademar
        //URL: https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
        //Adaption Required: Had to adapt the algorithms to work with our API
        //=============================================

        // Posting Note for Patient to the API BY ID
        public void PostNote( int patientId, string title, String text, String incomingPicture)
        {
            // Create the query to be sent to the server
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL+ "SaveNote");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // The query in JSON format to be sent to the API
                string json = "{ \"PatientID\": " + Convert.ToString(patientId) +", \"Title\": \"" + title +"\", \"Text\": \"" + text +"\", \"Picture\": \"" + incomingPicture + "\"}";

                // Send the query to the API and close the connection
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        //============================================= 
        //End reference B2
        //============================================= 
    }
}

