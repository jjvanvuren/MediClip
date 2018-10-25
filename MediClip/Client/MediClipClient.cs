using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MediClip.Models;
using System.Collections.Generic;

namespace MediClip.Client
{
    public class MediClipClient
    {
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
        public async Task<bool> PostNote( int patientId, string title, String text, String Picture)
        {

            String searchUrl = API_URL + "SaveNote { \"PatientID\": " + Convert.ToString(patientId) +
                                                        ", \"Title\": \"" + title +
                                                        "\", \"Text\": \"" + text +
                                                        "\", \"Picture\": \"" + Picture + "\"}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }

        }


    }
}

