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
        //Reference A2: Externally Sourced algorithms
        //Purpose: To GET and POST information to and from API
        //Date: 
        //Source: Lecture Slides and Lab material
        //Author: Unknown
        //URL: None
        //Adaption Required: Had to adapt the algorithms to work with our API
        //and change different aspects of it to work with our application e.g. Posting and getting lists. 
        //=============================================
        // PLEASE NOTE: WE MAY NEED TO ADD THE SOURCE WHERE WE GOT HOW TO POST TO THE SERVER HERE AS WELL

        //const String API_KEY = "";
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

        // Posting Note for Patient to the API BY ID
        public void PostNote( int patientId, string title, String text, String incomingPicture)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL+ "SaveNote");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{ \"PatientID\": " + Convert.ToString(patientId) +", \"Title\": \"" + title +"\", \"Text\": \"" + text +"\", \"Picture\": \"" + incomingPicture + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            /*String searchUrl = API_URL + "SaveNote{ \"PatientID\": " + Convert.ToString(patientId) +
                                                        ", \"Title\": \"" + title +
                                                        "\", \"Text\": \"" + text +
                                                        "\", \"Picture\": \"" + Picture + "\"}";*/

            /*HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }*/

        }
        //============================================= 
        //End reference A2
        //============================================= 

    }
}

