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



    }
}

