using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MediClip.Objects;

namespace MediClip.Client
{
    public class MediClipClient
    {
            const String API_KEY = "";
            const String API_URL = "" + API_KEY;

            //Searching for a patient using name
        public async Task<Patient> Search(String patientName)
            {
                var url = API_URL + $"& ???? {WebUtility.UrlEncode(patientName)}";
                var client = new HttpClient();

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Patient foundPatient = JsonConvert.DeserializeObject<Patient>(content);

                    return foundPatient;
                }
                else
                {
                    throw new Exception($"Client returned response code of {response.StatusCode}");
                }
            }

            //Searching for a patient using ID
            public async Task<Patient> FindPatient(String id)
        {

                var searchUrl = API_URL + $"& ????{WebUtility.UrlEncode(id)}";
                var client = new HttpClient();

                var response = await client.GetAsync(searchUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Patient foundPatient = JsonConvert.DeserializeObject<Patient>(content);

                    return foundPatient;
                }
                else
                {
                    throw new Exception($"Client returned response code of {response.StatusCode}");
                }

            }

            //
            public async Task<Ward> FindWard(String id)
            {

            var searchUrl = API_URL + $"& ????{WebUtility.UrlEncode(id)}";
            var client = new HttpClient();

            var response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var foundWard = JsonConvert.DeserializeObject<Ward>(content);

                return foundWard;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }

        }

        public async Task<Notes> FindNote(String id)
        {

            var searchUrl = API_URL + $"& ????{WebUtility.UrlEncode(id)}";
            var client = new HttpClient();

            var response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Notes FoundNote = JsonConvert.DeserializeObject<Notes>(content);

                return FoundNote;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }

        }

        public async Task<Nurse> Login(String id)
        {

            var searchUrl = API_URL + $"& ????{WebUtility.UrlEncode(id)}";
            var client = new HttpClient();

            var response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Nurse FoundNurse = JsonConvert.DeserializeObject<Nurse>(content);

                return FoundNurse;
            }
            else
            {
                throw new Exception($"Client returned response code of {response.StatusCode}");
            }

        }

    }
}

